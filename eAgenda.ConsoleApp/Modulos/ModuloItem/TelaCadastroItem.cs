using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgenda.ConsoleApp.Compartilhado;

namespace eAgenda.ConsoleApp.Modulos.ModuloItem
{
    public class TelaCadastroItem : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioItem _repositorioItem;
        private readonly Notificador _notificar;

        public TelaCadastroItem(RepositorioItem repositorioItem, Notificador notificador) : base("Cadastro de Item")
        {
            _repositorioItem = repositorioItem;
            _notificar = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastro de Item");

            Item novoItem = ObterItem();

            _repositorioItem.Inserir(novoItem);

            _notificar.ApresentarMensagem("Item cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Item");

            bool temItemsCadastrados = VisualizarRegistros("Pesquisando");

            if (temItemsCadastrados == false)
            {
                _notificar.ApresentarMensagem("Nenhum item cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int idItem = ObterNumeroRegistro();

            Item ItemAtualizado = ObterItem();

            bool conseguiuEditar = _repositorioItem.Editar(idItem, ItemAtualizado);

            if (!conseguiuEditar)
                _notificar.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificar.ApresentarMensagem("Item editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Item");

            bool temItemsRegistrados = VisualizarRegistros("Pesquisando");

            if (temItemsRegistrados == false)
            {
                _notificar.ApresentarMensagem("Nenhum item cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroItem = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioItem.Excluir(numeroItem);

            if (!conseguiuExcluir)
                _notificar.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificar.ApresentarMensagem("Item excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Item");

            List<Item> items = _repositorioItem.SelecionarTodos();

            if (items.Count == 0)
            {
                _notificar.ApresentarMensagem("Nenhum item disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Item item in items)
                Console.WriteLine(item.ToString());

            Console.ReadLine();

            return true;
        }

        public Item ObterItem()
        {
            Console.Write("Digite a descrição do item: ");
            string descricao = Console.ReadLine();

            Console.Write("O item está concluido (Sim ou Nao): ");
            string conclusao = Console.ReadLine().ToUpper();

            bool concluido;

            if (conclusao == "SIM")
            {
                concluido = true;
            }
            else
            {
                concluido = false;
            }

            return new Item(descricao, concluido);
        }

        public int ObterNumeroRegistro()
        {
            int idRegistro;
            bool idRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do item que deseja editar: ");
                idRegistro = int.Parse(Console.ReadLine());

                idRegistroEncontrado = _repositorioItem.ExisteRegistro(idRegistro);

                if (idRegistroEncontrado == false)
                    _notificar.ApresentarMensagem("ID do item não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (idRegistroEncontrado == false);

            return idRegistro;
        }
    }
}
