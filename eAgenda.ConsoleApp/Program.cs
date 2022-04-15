using System;
using eAgenda.ConsoleApp.Compartilhado;
using eAgenda.ConsoleApp.Modulos.ModuloTarefa;
using eAgenda.ConsoleApp.Modulos.ModuloContato;
using eAgenda.ConsoleApp.Modulos.ModuloCompromisso;

namespace eAgenda.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Notificador notificar = new Notificador();
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificar);

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.Opcoes();

                if (telaSelecionada is TelaCadastroContato)
                    GerenciarCadastroContato(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroTarefa)
                    GerenciarCadastroTarefa(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroCompromisso)
                    GerenciarCadastroCompromisso(telaSelecionada, opcaoSelecionada);

            }
            
            static void GerenciarCadastroContato(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroContato telaCadastroContato = telaSelecionada as TelaCadastroContato;

                if (telaCadastroContato is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroContato.InserirRegistro();

                else if (opcaoSelecionada == "2")
                    telaCadastroContato.EditarRegistro();

                else if (opcaoSelecionada == "3")
                    telaCadastroContato.ExcluirRegistro();

                else if (opcaoSelecionada == "4")
                    telaCadastroContato.VisualizarRegistrosPorCargo("Tela");

                else if (opcaoSelecionada == "5")
                    telaCadastroContato.VisualizarRegistros("Tela");
            }
            

            static void GerenciarCadastroTarefa(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroTarefa telaCadastroTarefa = telaSelecionada as TelaCadastroTarefa;

                if (telaCadastroTarefa is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroTarefa.InserirRegistro();

                else if (opcaoSelecionada == "2")
                    telaCadastroTarefa.EditarRegistro();

                else if (opcaoSelecionada == "3")
                    telaCadastroTarefa.ExcluirRegistro();

                else if (opcaoSelecionada == "4")
                    telaCadastroTarefa.VisualizarRegistrosPendentes("Tela");

                else if (opcaoSelecionada == "5")
                    telaCadastroTarefa.VisualizarRegistrosFinalizados("Tela");

                else if (opcaoSelecionada == "6")
                    telaCadastroTarefa.VisualizarRegistros("Tela");

            }

            
            static void GerenciarCadastroCompromisso(TelaBase telaSelecionada, string opcaoSelecionada)
            {
                TelaCadastroCompromisso telaCadastroCompromisso = telaSelecionada as TelaCadastroCompromisso;

                if (telaCadastroCompromisso is null)
                    return;

                if (opcaoSelecionada == "1")
                    telaCadastroCompromisso.InserirRegistro();

                else if (opcaoSelecionada == "2")
                    telaCadastroCompromisso.EditarRegistro();

                else if (opcaoSelecionada == "3")
                    telaCadastroCompromisso.ExcluirRegistro();

                else if (opcaoSelecionada == "4")
                    telaCadastroCompromisso.VisualizarCompromissosDoDia("Tela");
                
                else if (opcaoSelecionada == "5")
                    telaCadastroCompromisso.VisualizarCompromissosDaSemana("Tela");

                else if (opcaoSelecionada == "6")
                    telaCadastroCompromisso.VisualizarCompromissosEncerrados("Tela");

                else if (opcaoSelecionada == "7")
                    telaCadastroCompromisso.VisualizarCompromissosFuturos("Tela");      

                else if (opcaoSelecionada == "8")
                    telaCadastroCompromisso.VisualizarRegistros("Tela");
                
            }
            

        }
    }
}
