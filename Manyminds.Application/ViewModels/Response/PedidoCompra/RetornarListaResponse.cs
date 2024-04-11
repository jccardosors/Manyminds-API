namespace Manyminds.Application.ViewModels.Response.PedidoCompra
{
    public class RetornarListaResponse : ResultBase
    {
        public IEnumerable<PedidoCompraVM> Data { get; set; }
    }
}
