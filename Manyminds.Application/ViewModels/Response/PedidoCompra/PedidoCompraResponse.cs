namespace Manyminds.Application.ViewModels.Response.PedidoCompra
{
    public class PedidoCompraResponse : ResultBase
    {
        public PedidoCompraVM Pedido { get; set; }

        public IEnumerable<PedidoCompraItemVM> Items { get; set; }
    }
}
