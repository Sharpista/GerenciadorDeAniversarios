using DAL.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    public class FileRepositorio : IRepositorio
    {
        public List<Pessoa> _pessoas = new List<Pessoa>();

        private readonly string _diretorio;
        private readonly string _arquivo = "Pessoa.txt";
        
        public FileRepositorio()
        {
            
            _diretorio = Directory.GetCurrentDirectory();
            CriadorDeArquivos();
            LerArquivo();
        }


        public void LerArquivo()
        {
            var pessoas = new List<Pessoa>();

            var path = $@"{_diretorio}\{_arquivo}";
            if (File.Exists(path))
            {
                using (var openFile = File.OpenRead(path))
                {
                    using (var readText = new StreamReader(openFile))
                    {

                        while (readText.EndOfStream == false)
                        {
                            var read = readText.ReadLine();

                            string[] aux = read.Split(';');

                            pessoas.Add(new Pessoa(int.Parse(aux[0]), aux[1], aux[2], DateTime.ParseExact(aux[3],"dd/MM/yyyy",null)));
                        }
                        _pessoas = pessoas;
                    }
                }
            }
        }


        private void CriadorDeArquivos()
        {
            var path = $@"{_diretorio}\{_arquivo}";

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }


        public void Save()
        {
            string caminhoArquivo = $@"{_diretorio}\{_arquivo}";
            var way = new List<string>();
            
            foreach (Pessoa pessoa in _pessoas)
            {
                way.Add(pessoa.ToString());
            }

            if (File.Exists(caminhoArquivo))
            {
                File.WriteAllLines(caminhoArquivo, way, Encoding.UTF8);
                LerArquivo();
            }
        }

        
        public void Adiciona(Pessoa pessoa)
        {
            var contador = _pessoas.Any() ? _pessoas.Max(p => p.Id) + 1 : 1;
            pessoa.Id = contador;
            _pessoas.Add(pessoa);
            Save();

        }

        public void Altera(Pessoa pessoa)
        {
            var resultado = _pessoas.FirstOrDefault(p => p.Id == pessoa.Id);
            if(resultado != null)
            {
                _pessoas.Remove(resultado);
                _pessoas.Add(pessoa);
                Save();
            }
        }

        public void Exclui(Pessoa pessoa)
        {
            var resultado = GetPorId(pessoa.Id);

            if(resultado != null)
            {
                _pessoas.Remove(resultado);
                Save();                
            }

        }

        public IEnumerable<Pessoa> GetPessoas()
        {
            return _pessoas;
        }

        public Pessoa GetPorId(int id)
        {
            return _pessoas.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pessoa> GetPessoasPorNome(string nome)
        {
            

            return _pessoas.Where(p => Regex.IsMatch(p.Nome,nome, RegexOptions.IgnoreCase) || Regex.IsMatch(p.Sobrenome, nome, RegexOptions.IgnoreCase));
        }
    }
}
