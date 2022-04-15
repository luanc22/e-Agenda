using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Compartilhado.Validacao;   
using eAgenda.ConsoleApp.Modulos.ModuloContato;

namespace eAgenda.ConsoleApp.Modulos.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        private readonly string _assunto;
        private readonly string _local;
        private readonly DateTime _dataCompromisso;
        private readonly DateTime _horaInicio;
        private readonly DateTime _horaTermino;

        public Contato _contato;

        public Compromisso(string assunto, string local, DateTime dataCompromisso, DateTime horaInicio, DateTime horaTermino, Contato contato)
        {
            _assunto = assunto;
            _local = local;
            _dataCompromisso = dataCompromisso;
            _horaInicio = horaInicio;
            _horaTermino = horaTermino;
            _contato = contato;
        }
        public string Assunto { get => _assunto; }

        public string Local { get => _local; }

        public DateTime Data { get => _dataCompromisso; }

        public DateTime HoraInicio { get => _horaInicio; }

        public DateTime HoraTermino { get => _horaTermino; }

        public override string ToString()
        {
            return 
                "Id do Compromisso: " + id + Environment.NewLine +
                "Assunto: " + Assunto + Environment.NewLine +
                "Local: " + Local + Environment.NewLine +
                "Data: " + Data.Day + "/" + Data.Month + "/" + Data.Year + Environment.NewLine +
                "Hora de inicio: " + HoraInicio.Hour + ":" + HoraInicio.Minute + Environment.NewLine +
                "Hora de termino: " + HoraTermino.Hour + ":" + HoraTermino.Minute + Environment.NewLine + "\n" +
                "Contato: " + "\n" + _contato.ToString();
        }

        public override RetornoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (_assunto.Length <= 0)
                erros.Add("Compromisso deve ser um campo válido e preenchido!");

            if (_local.Length <= 0)
                erros.Add("Local deve ser um campo válido e preenchido!");

            return new RetornoValidacao(erros);
        }
    }
}
