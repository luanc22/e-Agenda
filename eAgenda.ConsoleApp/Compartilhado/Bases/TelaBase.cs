using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.ConsoleApp.Compartilhado
{
    public class TelaBase
    {
        private readonly string _titulo;

        public string Titulo { get { return _titulo; } }

        public TelaBase(string titulo)
        {
            _titulo = titulo;
        }

        public virtual string Opcoes()
        {
            Console.WriteLine("[1] - Inserir.");
            Console.WriteLine("[2] - Visualizar.");
            Console.WriteLine("[3] - Editar.");
            Console.WriteLine("[4] - Excluir.");
            Console.WriteLine("[S] - Sair.");
            Console.WriteLine();

            Console.Write("Opção escolhida: ");
            string opcaoEscolhida = Console.ReadLine();

            return opcaoEscolhida;
        }

        protected void MostrarTitulo(string titulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();
        }

    }
}
