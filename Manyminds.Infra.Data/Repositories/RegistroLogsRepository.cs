using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class RegistroLogsRepository : IRegistroLogsRepository
    {
        public AppDbContext _context;

        public RegistroLogsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroLogs> Adicionar(RegistroLogs registroLogs)
        {
            await _context.Set<RegistroLogs>().AddAsync(registroLogs);
            await _context.SaveChangesAsync();

            return registroLogs;
        }

        public Task<RegistroLogs> Alterar(RegistroLogs pedido)
        {
            throw new NotImplementedException();
        }


        public Task<bool> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        public Task<RegistroLogs> RetornarItem(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RegistroLogs>> RetornarTodos()
        {
            var lista = await _context.registros.ToListAsync();
            return lista;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
