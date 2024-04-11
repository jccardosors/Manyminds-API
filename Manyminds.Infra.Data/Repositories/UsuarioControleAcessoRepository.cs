using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class UsuarioControleAcessoRepository : IUsuarioControleAcessoRepository
    {
        public AppDbContext _context;

        public UsuarioControleAcessoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioControleAcesso> Adicionar(UsuarioControleAcesso entity)
        {
            await _context.Set<UsuarioControleAcesso>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<UsuarioControleAcesso> Alterar(UsuarioControleAcesso entity)
        {
            var registro = await Task.FromResult(_context.Set<UsuarioControleAcesso>().Update(entity));
            registro.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task<bool> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioControleAcesso> RetornarItem(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioControleAcesso> RetornarItem(string email)
        {
            var usuarioControleAcesso = await _context.usuarioControleAcessos.FirstOrDefaultAsync(p => p.UsuarioEmail == email);
            return usuarioControleAcesso!;
        }

        public Task<IEnumerable<UsuarioControleAcesso>> RetornarTodos()
        {
            throw new NotImplementedException();
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
