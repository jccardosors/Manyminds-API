namespace Manyminds.Application.ViewModels
{
    public class ProdutoVM
    {
        public int Codigo { get; set; }
        public int FornecedorCodigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
