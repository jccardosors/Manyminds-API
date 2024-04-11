using Manyminds.Application.ViewModels.Response.RegistroLogs;

namespace Manyminds.Application.Interfaces
{
    public interface IRegistroLogsService : IDisposable
    {
        Task RegistrarLogs(string email, string classe, string metodo);

        Task<RetornarListaLogsResponse> RetornarLista();
    }
}
