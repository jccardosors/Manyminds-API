namespace Manyminds.Application.ViewModels.Response.Produto
{
    public class RetornarListaResponse : ResultBase
    {
        public IEnumerable<ProdutoVM> Data { get; set; }
    }
}
