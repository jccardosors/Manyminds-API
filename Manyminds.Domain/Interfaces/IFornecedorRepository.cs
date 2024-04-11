using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IFornecedorRepository : IDisposable
    {
        Task<IEnumerable<Fornecedor>> RetornarTodos();

        Task<Fornecedor> RetornarItem(int codigo);

        Task<Fornecedor> Adicionar(Fornecedor entity);

        Task<Fornecedor> Alterar(Fornecedor entity);

        Task<bool> Excluir(int codigo);
    }
}
