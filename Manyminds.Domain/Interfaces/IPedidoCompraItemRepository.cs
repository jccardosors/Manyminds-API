using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IPedidoCompraItemRepository : IDisposable
    {
        Task<IEnumerable<PedidoCompraItem>> RetornarTodos();

        Task<PedidoCompraItem> RetornarItem(int codigo);

        Task<PedidoCompraItem> Adicionar(PedidoCompraItem entity);

        Task<PedidoCompraItem> Alterar(PedidoCompraItem entity);

        Task<bool> Excluir(int codigo);

        Task<IEnumerable<PedidoCompraItem>> RetornarItensPorCodigoPedidoCompra(int pedidoCompraCodigo);
    }
}
