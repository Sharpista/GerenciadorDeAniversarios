using System;

namespace DAL.Modelos
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get;  set; }
        public string Sobrenome { get;  set; }
        public DateTime Aniversario { get;  set; }

        public Pessoa(string nome, string sobrenome, DateTime aniversario)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Aniversario = aniversario;
        }
        public Pessoa(int id, string nome, string sobrenome, DateTime aniversario)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Aniversario = aniversario;
        }

        public string NomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }

        public string ProximoAniversario()
        {
            var aniversarioAtual = new DateTime(DateTime.Today.Year, Aniversario.Month, Aniversario.Day);

            return $"Faltam {CalculaIntervaloAniversário(aniversarioAtual)} dias para esse aniversário ! Parabéééns";
        }

        private static int CalculaIntervaloAniversário(DateTime aniversarioAtual)
        {
            var resultado = aniversarioAtual.Subtract(DateTime.Today);
            return resultado.Days < 0 ? CalculaIntervaloAniversário(aniversarioAtual.AddYears(1)) : resultado.Days;
        }

        public override string ToString()
        {
            return $"{Id};{Nome};{Sobrenome};{Aniversario.ToString("dd/MM/yyyy")}";
        }
















    }
}
