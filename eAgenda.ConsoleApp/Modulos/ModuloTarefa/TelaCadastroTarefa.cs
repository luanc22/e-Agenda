using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado.Validacao;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Modulos.ModuloItem;


namespace eAgenda.ConsoleApp.Modulos.ModuloTarefa
{

    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly Notificador _notificar;
        private readonly IRepositorio<Tarefa> _repositorioTarefa;
        private readonly List<Item> itens = new List<Item>();

        public TelaCadastroTarefa(IRepositorio<Tarefa> repositorioTarefa, Notificador notificar) : base("Cadastro de Tarefa")
        {
            _notificar = notificar;
            _repositorioTarefa = repositorioTarefa;
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

            Console.WriteLine("[4] - Visualizar Tarefas Pendentes.");
            Console.WriteLine("[5] - Visualizar Tarefas Concluídas.");
            Console.WriteLine("[6] - Visualizar Todas as Tarefas.");
            Console.WriteLine();
            Console.WriteLine("[S] - Sair.");
            Console.WriteLine();

            Console.Write("Opção escolhida: ");
            string opcaoEscolhida = Console.ReadLine();

            return opcaoEscolhida;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Criando Tarefa.");

            Tarefa novaTarefa = ObterTarefa();

            string resultadoValidacao = _repositorioTarefa.Inserir(novaTarefa);

            if (resultadoValidacao == "REGISTRO_VALIDO")
            {
                _notificar.ApresentarMensagem("Tarefa inserida com sucesso", TipoMensagem.Sucesso);
            }
            else
            {
                _notificar.ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
            }
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                _notificar.ApresentarMensagem("Nenhuma tarefa cadastrada para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int idTarefa = ObterIDTarefa();

            Tarefa tarefaAEditar = _repositorioTarefa.SelecionarRegistro(idTarefa);

            Tarefa tarefaAtualizada = ObterTarefaEditada(tarefaAEditar.DataCriacao);

            bool conseguiuEditar = _repositorioTarefa.Editar(x => x.id == idTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                _notificar.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificar.ApresentarMensagem("Tarefa editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                _notificar.ApresentarMensagem("Nenhuma tarefa cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int idTarefa = ObterIDTarefa();

            bool conseguiuExcluir = _repositorioTarefa.Excluir(x => x.id == idTarefa);

            if (!conseguiuExcluir)
                _notificar.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                _notificar.ApresentarMensagem("Tarefa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Pendente");

            List<Tarefa> tarefas = _repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                if (tarefa.Prioridade == 3)
                    Console.WriteLine(tarefa.ToString());

            foreach (Tarefa tarefa in tarefas)
                if (tarefa.Prioridade == 2)
                    Console.WriteLine(tarefa.ToString());

            foreach (Tarefa tarefa in tarefas)
                if (tarefa.Prioridade == 1)
                    Console.WriteLine(tarefa.ToString());

            Console.Write("Aperte ENTER para prosseguir.");
            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosFinalizados(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Pendente");

            List<Tarefa> tarefasFinalizadas = _repositorioTarefa.SelecionarTodos();

            if (tarefasFinalizadas.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefaConcluida in tarefasFinalizadas)
                if (tarefaConcluida.PercentualConcluido == 100 && tarefaConcluida.Prioridade == 3)
                    Console.WriteLine(tarefaConcluida.ToString());

            foreach (Tarefa tarefaConcluida in tarefasFinalizadas)
                if (tarefaConcluida.PercentualConcluido == 100 && tarefaConcluida.Prioridade == 2)
                    Console.WriteLine(tarefaConcluida.ToString());

            foreach (Tarefa tarefaConcluida in tarefasFinalizadas)
                if (tarefaConcluida.PercentualConcluido == 100 && tarefaConcluida.Prioridade == 1)
                    Console.WriteLine(tarefaConcluida.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistrosPendentes(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefa Pendente");

            List<Tarefa> tarefasPendentes = _repositorioTarefa.SelecionarTodos();

            if (tarefasPendentes.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefaPendente in tarefasPendentes)
                if (tarefaPendente.PercentualConcluido < 100 && tarefaPendente.Prioridade == 3)
                    Console.WriteLine(tarefaPendente.ToString());

            foreach (Tarefa tarefaPendente in tarefasPendentes)
                if (tarefaPendente.PercentualConcluido < 100 && tarefaPendente.Prioridade == 2)
                    Console.WriteLine(tarefaPendente.ToString());

            foreach (Tarefa tarefaPendente in tarefasPendentes)
                if (tarefaPendente.PercentualConcluido < 100 && tarefaPendente.Prioridade == 1)
                    Console.WriteLine(tarefaPendente.ToString());

            Console.ReadLine();

            return true;
        }

        private Tarefa ObterTarefa()
        {
            bool concluido;

            Console.WriteLine("Defina a prioridade da tarefa.");
            Console.WriteLine("[1] - Baixa");
            Console.WriteLine("[2] - Média");
            Console.WriteLine("[3] - Alta");
            Console.Write("Prioridade: ");
            int prioridade = int.Parse(Console.ReadLine());

            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de criação da tarefa: ");
            DateTime dataCriacao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a data de conclusão da tarefa: ");
            DateTime? dataConclusao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite o percentual concluído da tarefa: ");
            int percentualConcluido = int.Parse(Console.ReadLine());

            if (percentualConcluido == 100)
            {
                concluido = true;
                dataConclusao = DateTime.Now;
            }
            else
            {
                concluido = false;
            }

            Console.WriteLine();

            Console.Write("Cadastrar items para esta tarefa? (Sim ou Nao): ");
            string cadastrarItems = Console.ReadLine();
            cadastrarItems = cadastrarItems.ToUpper();

            if (cadastrarItems == "NAO")
                return new Tarefa(titulo, dataCriacao, dataConclusao, percentualConcluido, prioridade, concluido);
            else
            {
                List<Item> itens = new List<Item>();

                do
                {
                    RepositorioItem repositorioItem = new RepositorioItem();

                    TelaCadastroItem telaCadastroItem = new TelaCadastroItem(repositorioItem, _notificar);

                    Item item = telaCadastroItem.ObterItem();
                    itens.Add(item);

                    Console.WriteLine();
                    Console.Write("Adicionar outro item a esta tarefa? (Sim ou Nao): ");
                    cadastrarItems = Console.ReadLine();
                    cadastrarItems = cadastrarItems.ToUpper();

                } while (cadastrarItems == "SIM");

                return new Tarefa(titulo, dataCriacao, dataConclusao, percentualConcluido, prioridade, concluido, itens);
            }

            
        }

        private Tarefa ObterTarefaEditada(DateTime dataCriacao)
        {
            bool concluido;

            Console.WriteLine("Defina a prioridade da tarefa.");
            Console.WriteLine("[1] - Baixa");
            Console.WriteLine("[2] - Média");
            Console.WriteLine("[3] - Alta");
            Console.Write("Prioridade: ");
            int prioridade = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de conclusão da tarefa: ");
            DateTime dataConclusao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite o percentual concluído da tarefa: ");
            int percentualConcluido = int.Parse(Console.ReadLine());

            if (percentualConcluido == 100)
            {
                concluido = true;
                dataConclusao = DateTime.Now;
            }
            else
            {
                concluido = false;
            }

            Console.WriteLine();

            Console.Write("Cadastrar items para esta tarefa? (Sim ou Nao): ");
            string cadastrarItems = Console.ReadLine();
            cadastrarItems = cadastrarItems.ToUpper();

            if (cadastrarItems == "NAO")
                return new Tarefa(titulo, dataCriacao, dataConclusao, percentualConcluido, prioridade, concluido);
            else
            {
                List<Item> itens = new List<Item>();

                do
                {
                    RepositorioItem repositorioItem = new RepositorioItem();

                    TelaCadastroItem telaCadastroItem = new TelaCadastroItem(repositorioItem, _notificar);

                    Item item = telaCadastroItem.ObterItem();
                    itens.Add(item);

                    Console.WriteLine();
                    Console.Write("Adicionar outro item a esta tarefa? (Sim ou Nao): ");
                    cadastrarItems = Console.ReadLine();
                    cadastrarItems = cadastrarItems.ToUpper();

                } while (cadastrarItems == "SIM");

                return new Tarefa(titulo, dataCriacao, dataConclusao, percentualConcluido, prioridade, concluido, itens);
            }
        }

        private int ObterIDTarefa()
        {
            int idTarefa;
            bool idTarefaEncontrado;

            do
            {
                Console.Write("Digite o número da tarefa que deseja selecionar: ");
                idTarefa = Convert.ToInt32(Console.ReadLine());

                idTarefaEncontrado = _repositorioTarefa.ExisteRegistro(x => x.id == idTarefa);

                if (idTarefaEncontrado == false)
                    _notificar.ApresentarMensagem("Número da tarefa não encontrado, digite novamente.", TipoMensagem.Atencao);

            } while (idTarefaEncontrado == false);
            return idTarefa;
        }

    }
}
