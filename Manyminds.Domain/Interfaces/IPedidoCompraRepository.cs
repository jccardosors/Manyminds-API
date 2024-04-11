using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IPedidoCompraRepository : IDisposable
    {
        Task<IEnumerable<PedidoCompra>> RetornarTodos();

        Task<PedidoCompra> RetornarItem(int codigo);

        Task<PedidoCompra> Adicionar(PedidoCompra entity);

        Task<PedidoCompra> Alterar(PedidoCompra entity);

        Task<bool> Excluir(int codigo);
    }
}
