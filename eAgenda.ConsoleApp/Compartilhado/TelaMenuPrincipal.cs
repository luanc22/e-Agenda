using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Modulos.ModuloTarefa;
using eAgenda.ConsoleApp.Modulos.ModuloContato;
using eAgenda.ConsoleApp.Modulos.ModuloCompromisso;

namespace eAgenda.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private string _opcaoSelecionada;

        private IRepositorio<Tarefa> _repositorioTarefa;
        private TelaCadastroTarefa _telaCadastroTarefa;

        private IRepositorio<Contato> _repositorioContato;
        private TelaCadastroContato _telaCadastroContato;

        private IRepositorio<Compromisso> _repositorioCompromisso;
        private TelaCadastroCompromisso _telaCadastroCompromisso;

        public TelaMenuPrincipal(Notificador notificar)
        {
            _repositorioTarefa = new RepositorioTarefa();
            _repositorioContato = new RepositorioContato();
            _repositorioCompromisso = new RepositorioCompromisso();

            _telaCadastroTarefa = new TelaCadastroTarefa(_repositorioTarefa, notificar);
            _telaCadastroContato = new TelaCadastroContato(_repositorioContato, notificar);
            _telaCadastroCompromisso = new TelaCadastroCompromisso(_repositorioCompromisso, _repositorioContato, _telaCadastroContato, notificar);
        } 

            public string Opcoes()
        {
            Console.Clear();

            Console.WriteLine("e-Agenda 1.0");

            Console.WriteLine();

            Console.WriteLine("[1] - Acessar Tarefas.");
            Console.WriteLine("[2] - Acessar Contatos.");
            Console.WriteLine("[3] - Acessar Compromissos.");
            Console.WriteLine("[S] - Sair.");
            Console.WriteLine();

            Console.Write("Opção escolhida: ");
            _opcaoSelecionada = Console.ReadLine();

            return _opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = Opcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = _telaCadastroTarefa;

            else if (opcao == "2")
                tela = _telaCadastroContato;

            else if (opcao == "3")
                tela = _telaCadastroCompromisso;

            return tela;
        }
    }
}
