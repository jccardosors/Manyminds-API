using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IUsuarioRepository : IDisposable
    {
        Task<IEnumerable<Usuario>> RetornarLista();

        Task<Usuario> RetornarItem(int codigo);

        Task<Usuario> Adicionar(Usuario entity);

        Task<Usuario> Alterar(Usuario entity);

        Task<bool> Excluir(int codigo);

        Task<Usuario> RetornarItem(string email, string senha);

        Task<bool> RetornarUsuarioDesbloqueado(string email);

        Task<Usuario> RetornarItem(string email);

    }
}
