using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.ConsoleApp.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        string Inserir(T entidade);
        bool Editar(int idSelecionado, T entidade);
        bool Excluir(int idSelecionado);
        bool Editar(Predicate<T> condicao, T novaEntidade);
        bool Excluir(Predicate<T> condicao);
        bool ExisteRegistro(Predicate<T> condicao);
        List<T> SelecionarTodos();
        T SelecionarRegistro(int idSelecionado);

    }
}
