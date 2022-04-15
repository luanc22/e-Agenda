using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;

namespace eAgenda.ConsoleApp.Modulos.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly Notificador _notificar;
        private readonly IRepositorio<Contato> _repositorioContato;

        public TelaCadastroContato(IRepositorio<Contato> repositorioContato, Notificador notificar) : base("Cadastro de Contato")
        {
            _notificar = notificar;
            _repositorioContato = repositorioContato;
        }
        public override string Opcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Manutenção");
            Console.WriteLine();

            Console.WriteLine("[1] - Inserir.");
            Console.WriteLine("[2] - Editar.");
            Console.WriteLine("[3] - Excluir.");

            Console.WriteLine();
            Console.WriteLine("Visualização");
            Console.WriteLine();

            Console.WriteLine("[4] - Visualizar Contatos por Cargo.");
            Console.WriteLine("[5] - Visualizar Todos os Contatos.");
            Console.WriteLine();
            Console.WriteLine("[S] - Sair.");
            Console.WriteLine();

            Console.Write("Opção escolhida: ");
            string opcaoEscolhida = Console.ReadLine();

            return opcaoEscolhida;
        }


        public void InserirRegistro()
        {
            MostrarTitulo("Cadastro de Contato");

            Contato novoContato = ObterContato();

            _repositorioContato.Inserir(novoContato);

            _notificar.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                _notificar.ApresentarMensagem("Nenhum contato cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int idContato = ObterIDContato();

            Contato contatoAtualizado = ObterContato();

            bool conseguiuEditar = _repositorioContato.Editar(x => x.id == idContato, contatoAtualizado);

            if (!conseguiuEditar)
                _notificar.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificar.ApresentarMensagem("Contato editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                _notificar.ApresentarMensagem("Nenhum contato cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int idContato = ObterIDContato();

            bool conseguiuExcluir = _repositorioContato.Excluir(x => x.id == idContato);

            if (!conseguiuExcluir)
                _notificar.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                _notificar.ApresentarMensagem("Contato excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Pendente");

            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.Write("Aperte ENTER para prosseguir.");
            Console.ReadLine();


            return true;
        }

        public bool VisualizarRegistrosPorCargo(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contato por Cargo");

            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            Console.WriteLine("Cargos Cadastrados na Agenda.");
            Console.WriteLine();

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.Cargo);

            Console.WriteLine();

            Console.Write("Qual cargo você deseja visualizar contatos: ");
            string cargoSelecionado = Console.ReadLine();
            Console.WriteLine();

            foreach (Contato contato in contatos)
                if (contato.Cargo == cargoSelecionado)
                    Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        private Contato ObterContato()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            Console.Write("Digite o e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            return new Contato(nome, empresa, cargo, email, telefone);
        }

        private int ObterIDContato()
        {
            int idContato;
            bool idContatoEncontrado;

            do
            {
                Console.Write("Digite o número do contato que deseja selecionar: ");
                idContato = Convert.ToInt32(Console.ReadLine());

                idContatoEncontrado = _repositorioContato.ExisteRegistro(x => x.id == idContato);

                if (idContatoEncontrado == false)
                    _notificar.ApresentarMensagem("Número do contato não encontrado, digite novamente.", TipoMensagem.Atencao);

            } while (idContatoEncontrado == false);
            return idContato;
        }
    }
}
