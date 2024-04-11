namespace Manyminds.Application.ViewModels
{
    public class PedidoCompraVM
    {
        public int Codigo { get; set; }
        public int FornecedorCodigo { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }
        public int Status { get; set; }
        public decimal ValorTotal { get; set; }
        public string FornecedorNome { get; set; }
    }
}
