using DAL;
using DAL.Modelos;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LeoAT
{
    public class Program
    {
        static IRepositorio _repositorio = new FileRepositorio();

        static void Main(string[] args)
        {           

            int opcao;
            AniversariantedoDia();

            do
            {
                Console.WriteLine("--- Gerenciador de Aniversários ---");
                Console.WriteLine("[ 1 ] Adicionar pessoas");
                Console.WriteLine("[ 2 ] Pesquisar pessoas");
                Console.WriteLine("[ 3 ] Editar Pessoa");
                Console.WriteLine("[ 4 ] Excluir Pessoa");
                Console.WriteLine("[ 5 ] Listar tudo");
                Console.WriteLine("[ 6 ] Listar aniversariantes do Dia");
                Console.WriteLine("[ 7 ] Sair");
                Console.WriteLine("-------------------------------------");
                Console.Write("Selecione uma opção digitando o número correspondente: ");
               

                opcao = Int32.Parse(Console.ReadLine());
                
                switch (opcao)
                {
                    case 1:
                        AddPessoa();
                        
                        break;
                    case 2:
                        BuscarPessoa();
                        break;
                    case 3:
                        EditPessoa();
                        break;
                    case 4:
                        ExcluirPessoa();
                        break;
                    case 5:
                        ListaTudo();
                        break;
                    case 6:
                        AniversariantedoDia();
                        break;
                    case 7:
                        Console.WriteLine("Tem certeza que você quer sair do aplicativo? (digite sim ou não):");
                        var resposta = Console.ReadLine();
                        
                        if(Regex.IsMatch(resposta, "sim", RegexOptions.IgnoreCase))
                        {
                            Environment.Exit(0);
                        }
                        break;
                    default:

                        Environment.Exit(0);
                        break;
                }
                
                
            }
            while (opcao != 0);
        }
            


        public static void AniversariantedoDia()
        {
            var mes = DateTime.Today.Month;
            var dia = DateTime.Today.Day;

            var resultadoLista = _repositorio.GetPessoas().Where(p => p.Aniversario.Month == mes && p.Aniversario.Day == dia);

            if (resultadoLista.Any())
            {
                Console.WriteLine("Resultado:");
                foreach (var pessoa in resultadoLista)
                {
                    Console.WriteLine($"\t{pessoa}");
                }
            }
        }
        public static void ListaTudo()
        {
            var resultadoLista = _repositorio.GetPessoas();

            if (resultadoLista.Any())
            {
                Console.WriteLine("Resultado:");
                foreach (var pessoa in resultadoLista)
                {
                    Console.WriteLine($"\t{pessoa} -- Aniversario: {pessoa.ProximoAniversario()}");
                }
            }
        }
        public static void BuscarPessoa()
        {
            Console.WriteLine("Entre com o nome que deseja procurar: ");
            var parteNome = Console.ReadLine();

            var resultadoLista = _repositorio.GetPessoasPorNome(parteNome);

            if (!resultadoLista.Any())
            {
                Console.WriteLine("Nenhum resultado encontrado");
                return;
            }

            Console.WriteLine("resultado:");

            foreach (var item in resultadoLista)
            {
                Console.WriteLine($"\t {item}");
            }

        }

        public static void AddPessoa()
        {
                       
            Console.WriteLine("Escreva o nome: ");
            var nome = Console.ReadLine();

            Console.WriteLine("Escreva o Sobrenome: ");
            var sobrenome = Console.ReadLine();

            Console.WriteLine("Escreva o aniversário no formato Dia/Mês/Ano: ");
            string aniversarioDate = Console.ReadLine();

            DateTime birthDate;

            if (DateTime.TryParseExact(aniversarioDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
            {
                var pessoa = new Pessoa(nome, sobrenome, birthDate);
                _repositorio.Adiciona(pessoa);

                Console.WriteLine("Registro adicionado");
                

            }
            
            

        }
        public static void EditPessoa()
        {
            Console.WriteLine("Digite parte do nome de quem você quer editar os dados:");
            var parteNome = Console.ReadLine();

            var resultadoLista = _repositorio.GetPessoasPorNome(parteNome);

            if (!resultadoLista.Any())
            {
                Console.WriteLine("Nenhum resultado encontrado");
                return;
            }

            Console.WriteLine("resultado:");

            foreach(var item in resultadoLista)
            {
                Console.WriteLine($"\t {item}");
            }
            Console.WriteLine("Digite o Nº do ID do registro que você deseja alterar:");
            int.TryParse(Console.ReadLine(), out int id);

            var resultadoPessoa = resultadoLista.FirstOrDefault(p => p.Id == id);

            if(resultadoPessoa == null)
            {
                Console.WriteLine("Nenhum resultado encontrado");
                return;
            }

            Console.WriteLine("Digite o novo nome para pessoa :");
            var nome = Console.ReadLine();

            Console.WriteLine("Digite o novo Sobrenome para pessoa :");
            var sobrenome = Console.ReadLine();

            Console.WriteLine("Digite a nova data de aniversário no formato Dia/Mês/Ano:");
            string aniversarioDate = Console.ReadLine();

            if (DateTime.TryParseExact(aniversarioDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                resultadoPessoa.Nome = nome;
                resultadoPessoa.Sobrenome = sobrenome;
                resultadoPessoa.Aniversario = birthDate;
                _repositorio.Altera(resultadoPessoa);

                Console.WriteLine("Registro alterado");

            }
        }
        public static void ExcluirPessoa()
        {

            var resultadoLista = _repositorio.GetPessoas();

            Console.WriteLine("Digite o Nº do ID do registro que você deseja excluir:");

            int.TryParse(Console.ReadLine(), out int id);

            var resultadoPessoa = resultadoLista.FirstOrDefault(p => p.Id == id);

            if (resultadoPessoa == null)
            {
                Console.WriteLine("Nenhum resultado encontrado");
                return;
            }


            Console.WriteLine($"Tem certeza que deseja excluir o registro abaixo?(digite sim ou não)\n{resultadoPessoa}");

            var resposta = Console.ReadLine();

            if (Regex.IsMatch(resposta, "sim", RegexOptions.IgnoreCase))
            {

                _repositorio.Exclui(resultadoPessoa);
                Console.WriteLine("Registro apagado");

            }

            
        }

    }   
}
