namespace Manyminds.Domain.Entities
{
    public class PedidoCompraItem
    {
        public int Codigo { get; set; }
        public int PedidoCompraCodigo { get; set; }
        public int ProdutoCodigo { get; set; }
        public int Quantidade { get; set; }
    }
}
