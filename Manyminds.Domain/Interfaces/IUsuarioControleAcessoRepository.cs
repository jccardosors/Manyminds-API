using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IUsuarioControleAcessoRepository : IDisposable
    {
        Task<IEnumerable<UsuarioControleAcesso>> RetornarTodos();

        Task<UsuarioControleAcesso> RetornarItem(int codigo);

        Task<UsuarioControleAcesso> Adicionar(UsuarioControleAcesso entity);

        Task<UsuarioControleAcesso> Alterar(UsuarioControleAcesso entity);

        Task<bool> Excluir(int codigo);

        Task<UsuarioControleAcesso> RetornarItem(string email);
    }
}
