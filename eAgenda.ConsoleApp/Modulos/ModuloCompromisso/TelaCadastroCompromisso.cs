using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Modulos.ModuloContato;

namespace eAgenda.ConsoleApp.Modulos.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Compromisso> _repositorioCompromisso;

        private readonly IRepositorio<Contato> _repositorioContato;
        private readonly TelaCadastroContato _telaCadastroContato;

        private readonly Notificador _notificar;

        public TelaCadastroCompromisso(IRepositorio<Compromisso> repositorioCompromisso, IRepositorio<Contato> repositorioContato, TelaCadastroContato telaCadastroContato, Notificador notificar) : base("Cadastro de Compromisso")
        {
            _repositorioCompromisso = repositorioCompromisso;
            _repositorioContato = repositorioContato;
            _telaCadastroContato = telaCadastroContato;
            _notificar = notificar;
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

            Console.WriteLine("[4] - Visualizar Compromissos do Dia.");
            Console.WriteLine("[5] - Visualizar Compromissos da Semana.");
            Console.WriteLine("[6] - Visualizar Compromissos encerrados.");
            Console.WriteLine("[7] - Visualizar Compromissos futuros.");
            Console.WriteLine("[8] - Visualizar Todos os Compromissos.");
            Console.WriteLine();
            Console.WriteLine("[S] - Sair.");
            Console.WriteLine();

            Console.Write("Opção escolhida: ");
            string opcaoEscolhida = Console.ReadLine();

            return opcaoEscolhida;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastro de Compromisso");

            Compromisso novoCompromisso = ObterCompromisso();
            if (novoCompromisso == null)
                return;

            _repositorioCompromisso.Inserir(novoCompromisso);

            _notificar.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Compromisso");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosCadastrados == false)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterIDCompromisso();

            Compromisso compromissoAtualizado = ObterCompromisso();

            bool conseguiuEditar = _repositorioCompromisso.Editar(numeroCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                _notificar.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificar.ApresentarMensagem("Compromisso editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosRegistrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosRegistrados == false)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterIDCompromisso();

            bool conseguiuExcluir = _repositorioCompromisso.Excluir(numeroCompromisso);

            if (!conseguiuExcluir)
                _notificar.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificar.ApresentarMensagem("Compromisso excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Compromisso> compromissos = _repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.Write("Aperte ENTER para prosseguir.");
            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosDoDia(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromissos do Dia");

            List<Compromisso> compromissosDoDia = _repositorioCompromisso.SelecionarTodos();

            if (compromissosDoDia.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso marcado.", TipoMensagem.Atencao);
                return false;
            }

            DateTime data = DateTime.Now;

            foreach (Compromisso compromisso in compromissosDoDia)
                if (compromisso.Data.Day == data.Day && compromisso.Data.Month == data.Month && compromisso.Data.Year == data.Year)
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosDaSemana(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromissos da Semana");

            List<Compromisso> compromissosDaSemana = _repositorioCompromisso.SelecionarTodos();

            if (compromissosDaSemana.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            int dia = (int)DateTime.Now.DayOfWeek;

            foreach (Compromisso compromisso in compromissosDaSemana)
                if ((int)compromisso.Data.DayOfWeek >= dia && (int)compromisso.Data.DayOfWeek <= 6)
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosEncerrados(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromissos Encerrados");

            List<Compromisso> compromissosEncerrados = _repositorioCompromisso.SelecionarTodos();

            if (compromissosEncerrados.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            DateTime data = DateTime.Now;

            foreach (Compromisso compromisso in compromissosEncerrados)
                if (compromisso.Data < data)
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosFuturos(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromissos Futuros");

            List<Compromisso> compromissosFuturos = _repositorioCompromisso.SelecionarTodos();

            if (compromissosFuturos.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            DateTime data = DateTime.Now;

            Console.Write("Digite o mês: ");
            int mesDoCompromisso = int.Parse(Console.ReadLine());
            Console.WriteLine();

            while (mesDoCompromisso < 0 || mesDoCompromisso > 12)
            {
                Console.Write("Digite um mês válido entre 1 e 12: ");
                mesDoCompromisso = int.Parse(Console.ReadLine());
            }

            foreach (Compromisso compromisso in compromissosFuturos)
                if (compromisso.Data > data && compromisso.Data.Month == mesDoCompromisso)
                    Console.WriteLine(compromisso.ToString());

            Console.ReadLine();
            return true;
        }

        private int ObterIDCompromisso()
        {
            int idCompromisso;
            bool idCompromissoEncontrado;

            do
            {
                Console.Write("Digite o ID do compromisso: ");
                idCompromisso = int.Parse(Console.ReadLine());

                idCompromissoEncontrado = _repositorioCompromisso.ExisteRegistro(x => x.id == idCompromisso);

                if (idCompromissoEncontrado == false)
                    _notificar.ApresentarMensagem("ID do compromisso não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (idCompromissoEncontrado == false);

            return idCompromisso;
        }

        private Compromisso ObterCompromisso()
        {
            Console.Write("Digite o assunto do compromisso: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite o local do compromisso: ");
            string local = Console.ReadLine();

            Console.Write("Digite a data do compromisso: ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a hora de inicio do compromisso: ");
            DateTime horaInicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a hora de termino do compromisso: ");
            DateTime horaTermino = DateTime.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Selecione um contato para anexar ao compromisso: ");
            Console.WriteLine();

            Contato contatoSelecionado = ObtemContato();
            if (contatoSelecionado == null)
                return null;

            return new Compromisso(assunto, local, data, horaInicio, horaTermino, contatoSelecionado);
        }

        private Contato ObtemContato()
        {
            bool temContatoDisponivel = _telaCadastroContato.VisualizarRegistros("Pesquisando");

            if (!temContatoDisponivel)
            {
                _notificar.ApresentarMensagem("Não há nenhum contato cadastrado.", TipoMensagem.Atencao);
                return null;
            }

            Console.WriteLine();
            Console.Write("Digite o ID do contato: ");
            int idContato = int.Parse(Console.ReadLine());

            Contato contatoSelecionado = _repositorioContato.SelecionarRegistro(idContato);

            return contatoSelecionado;
        }
    }
}
