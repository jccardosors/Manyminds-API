namespace Manyminds.Application.ViewModels.Request.PedidoCompra
{
    public class PedidoCompraVMRequest
    {
        public PedidoCompraVM Pedido { get; set; }

        public IEnumerable<PedidoCompraItemVM> Items { get; set; }
    }
}
