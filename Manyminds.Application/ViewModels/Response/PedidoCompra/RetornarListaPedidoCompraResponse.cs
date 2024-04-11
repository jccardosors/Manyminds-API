namespace Manyminds.Application.ViewModels.Response.PedidoCompra
{
    public class RetornarListaPedidoCompraResponse : ResultBase
    {
        public IEnumerable<RetornarListaPedidoCompraItemResponse> Data { get; set; }
    }
}
