using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Compartilhado.Validacao;

namespace eAgenda.ConsoleApp.Modulos.ModuloItem
{
    public class Item : EntidadeBase
    {

        private readonly string _descricao;
        private readonly bool _concluido;

        public Item(string descricao, bool concluido)
        {
            _descricao = descricao;
            _concluido = concluido;
        }

        public string Descricao { get => _descricao; }
        public bool Concluido { get => _concluido; }

        public override string ToString()
        {
            string conclusao;

            if (Concluido == true)
            {
                conclusao = "Sim.";
            }
            else
            {
                conclusao = "Não.";
            }

            return 
                "Id do Item: " + id + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Concluido: " + conclusao + Environment.NewLine;
        }

        public override RetornoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (Descricao == "")
            {
                erros.Add("Descrição não pode ser nula!");
            }

            return new RetornoValidacao(erros);
        }
    }
}
