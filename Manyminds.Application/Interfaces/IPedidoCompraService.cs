using Manyminds.Application.ViewModels.Request.PedidoCompra;
using Manyminds.Application.ViewModels.Response.PedidoCompra;

namespace Manyminds.Application.Interfaces
{
    public interface IPedidoCompraService : IDisposable
    {
        Task<RetornarListaPedidoCompraResponse> RetornarLista();

        Task<PedidoCompraResponse> RetornarItem(int codigo);

        Task<PedidoCompraResponse> Adicionar(PedidoCompraVMRequest pedidoCompraVMRequest);

        Task<PedidoCompraResponse> Alterar(PedidoCompraVMRequest pedidoCompraVMRequest);

        Task<PedidoCompraResponse> Excluir(int codigo);

        Task<RetornarListaPedidosResponse> RetornarPedidosLista();
    }
}
