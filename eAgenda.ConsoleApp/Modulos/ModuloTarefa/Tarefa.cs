using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Compartilhado.Validacao;
using eAgenda.ConsoleApp.Modulos.ModuloItem;

namespace eAgenda.ConsoleApp.Modulos.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly string _titulo;
        private readonly DateTime _dataCriacao;
        private readonly DateTime? _dataConclusao;
        private readonly int _percentualConcluido;
        private readonly int _prioridade;
        private readonly bool _concluida;
        

        public List<Tarefa> tarefasCadastradas = new List<Tarefa>();
        public List<Item> _itens = new List<Item>();

        public Tarefa(string titulo, DateTime dataCriacao, DateTime? dataConclusao, int percentualConcluido, int prioridade, bool concluida)
        {
            _titulo = titulo;
            _dataCriacao = dataCriacao;
            _dataConclusao = dataConclusao;
            _percentualConcluido = percentualConcluido;
            _prioridade = prioridade;
            _concluida = concluida;
            
        }

        public Tarefa(string titulo, DateTime dataCriacao, DateTime? dataConclusao, int percentualConcluido, int prioridade, bool concluida, List<Item> itens)
        {
            _titulo = titulo;
            _dataCriacao = dataCriacao;
            _dataConclusao = dataConclusao;
            _percentualConcluido = percentualConcluido;
            _prioridade = prioridade;
            _concluida = concluida;
            _itens = itens;
        }

        public int Id { get => id; }

        public string Titulo { get => _titulo; }

        public DateTime DataCriacao { get => _dataCriacao; }

        public DateTime? DataConclusao { get => _dataConclusao; }

        public int PercentualConcluido { get => _percentualConcluido; }

        public int Prioridade { get => _prioridade; }

        public bool Concluida { get => _concluida; }

        public override string ToString()
        {
            string prioridadeString = "";

            switch (_prioridade)
            {
                case 1:
                    prioridadeString = "Baixa";
                    break;
                case 2:
                    prioridadeString = "Média";
                    break;
                case 3:
                    prioridadeString = "Alta";
                    break;
            }

            if (_concluida == false)
            {
                return
                    "ID: " + id + Environment.NewLine +
                    "Prioridade: " + prioridadeString + Environment.NewLine +
                    "Título: " + Titulo + Environment.NewLine +
                    "Data de criação: " + DataCriacao + Environment.NewLine +
                    "Percentual Concluido: " + PercentualConcluido + Environment.NewLine +
                    "Items: " + ListarItensTarefa();
            }
            else
            {
                return
                    "ID: " + id + Environment.NewLine +
                    "Prioridade: " + prioridadeString + Environment.NewLine +
                    "Título: " + Titulo + Environment.NewLine +
                    "Data de criação: " + DataCriacao + Environment.NewLine +
                    "Data de conclusão: " + DataConclusao + Environment.NewLine +
                    "Items: " + ListarItensTarefa();
            }
        }

        public string ListarItensTarefa()
        {
            string itensString = "";

            foreach (Item item in _itens)
            {
               itensString += "\n" + item.ToString() + "\n";
            }

            return itensString;
        }

        public override RetornoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (_percentualConcluido > 100 || _percentualConcluido < 0)
            {
                erros.Add("Percentual não pode ser menor que 0 ou maior que 100.");
            }

            return new RetornoValidacao(erros);
        }
    }
}
