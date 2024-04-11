namespace Manyminds.Application.ViewModels.Response.PedidoCompra
{
    public class RetornarListaPedidoCompraItemResponse
    {
        public PedidoCompraVM Pedido { get; set; }

        public IEnumerable<PedidoCompraItemVM> Items { get; set; }
    }
}
