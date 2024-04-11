using Manyminds.Domain.Entities;

namespace Manyminds.Domain.Interfaces
{
    public interface IRegistroLogsRepository : IDisposable
    {
        Task<IEnumerable<RegistroLogs>> RetornarTodos();

        Task<RegistroLogs> RetornarItem(int codigo);

        Task<RegistroLogs> Adicionar(RegistroLogs entity);

        Task<RegistroLogs> Alterar(RegistroLogs entity);

        Task<bool> Excluir(int codigo);
    }
}
