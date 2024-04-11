using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IProdutoRepository : IDisposable
    {
        Task<IEnumerable<Produto>> RetornarTodos();

        Task<Produto> RetornarItem(int codigo);

        Task<Produto> Adicionar(Produto entity);

        Task<Produto> Alterar(Produto entity);

        Task<Produto> AtivarDesativar(int codigo);
    }
}
