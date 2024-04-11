namespace Manyminds.Application.ViewModels
{
    public class PedidoCompraItemVM
    {
        public int Codigo { get; set; }
        public int PedidoCompraCodigo { get; set; }
        public int ProdutoCodigo { get; set; }
        public int Quantidade { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
    }
}
