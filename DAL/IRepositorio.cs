using DAL.Modelos;
using System.Collections.Generic;

namespace DAL
{
    public interface IRepositorio
    {
        IEnumerable<Pessoa> GetPessoas();

        Pessoa GetPorId(int id);
        IEnumerable<Pessoa> GetPessoasPorNome(string nome);

        void Adiciona(Pessoa pessoa);
        void Altera(Pessoa pessoa);
        void Exclui(Pessoa pessoa);
    }
}
