using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Compartilhado.Validacao;

namespace eAgenda.ConsoleApp.Modulos.ModuloContato
{
    public class Contato : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _empresa;
        private readonly string _cargo;
        private readonly string _email;
        private readonly string _telefone;

        public Contato(string nome, string empresa, string cargo, string email, string telefone)
        {
            _nome = nome;
            _empresa = empresa;
            _cargo = cargo;
            _email = email;
            _telefone = telefone;
        }

        public string Nome { get => _nome; }

        public string Empresa { get => _empresa; }

        public string Cargo { get => _cargo; }

        public string Email { get => _email; }

        public string Telefone { get => _telefone; }

        public override string ToString()
        {
            return 
                "Id do Contato: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "E-mail: " + Email + Environment.NewLine +
                "Empresa: " + Empresa + Environment.NewLine +
                "Cargo: " + Cargo + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine;
        }


        public override RetornoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (_email.Length <= 0)           
                erros.Add("E-mail deve ser um campo válido e preenchido!");
           
            if (_telefone.Length <= 0)
                erros.Add("Telefone deve ser um campo válido e preenchido!");

            return new RetornoValidacao(erros);
        }

    }
}
