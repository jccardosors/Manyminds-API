namespace Manyminds.Application.ViewModels.Response.PedidoCompra
{
    public class RetornarListaPedidosResponse : ResultBase
    {
        public IEnumerable<PedidoCompraVM> Data { get; set; }
    }
}
