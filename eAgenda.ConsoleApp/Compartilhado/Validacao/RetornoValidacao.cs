using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.ConsoleApp.Compartilhado.Validacao
{
    public class RetornoValidacao
    {
        public RetornoValidacao(List<string> erros)
        {
            _erros = erros;
        }

        private readonly List<string> _erros;

        public TipoValidacao Status
        {
            get
            {
                return _erros.Count == 0 ? TipoValidacao.VALIDO : TipoValidacao.INVALIDO;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string erro in _erros)
            {
                if (!string.IsNullOrEmpty(erro))
                    sb.AppendLine(erro);
            }

            return sb.ToString();
        }
    }
}
