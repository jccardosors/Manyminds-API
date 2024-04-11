using Manyminds.Application.ViewModels.Request.Produto;
using Manyminds.Application.ViewModels.Response.Produto;

namespace Manyminds.Application.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task<RetornarListaResponse> RetornarLista();

        Task<ProdutoResponse> RetornarItem(int codigo);

        Task<ProdutoResponse> Adicionar(ProdutoVMRequest produtoVMRequest);

        Task<ProdutoResponse> Alterar(ProdutoVMRequest produtoVMRequest);

        Task<ProdutoResponse> AtivarDesativar(int codigo);
    }
}
