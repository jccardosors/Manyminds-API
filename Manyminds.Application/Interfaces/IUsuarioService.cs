using Manyminds.Application.ViewModels.Request.Usuario;
using Manyminds.Application.ViewModels.Response;
using Manyminds.Application.ViewModels.Response.Usuario;

namespace Manyminds.Application.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<RetornarTodosResponse> RetornarTodos();

        Task<LoginResponse> LogarUsuario(LoginRequest loginRequest);

        Task<UsuarioResponse> Adicionar(UsuarioVMRequest usuarioVMRequest);

        Task<UsuarioResponse> Alterar(UsuarioVMRequest usuarioVMRequest);

        Task<UsuarioResponse> RetornarItem(int codigo);

        Task<UsuarioResponse> Excluir(int codigo);
    }
}
