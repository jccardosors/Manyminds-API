using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Adicionar(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Usuario> Alterar(Usuario entity)
        {
            var registro = await Task.FromResult(_context.Set<Usuario>().Update(entity));
            registro.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Excluir(int codigo)
        {
            var entity = await RetornarItem(codigo);
            _context.Set<Usuario>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Usuario> RetornarItem(int codigo)
        {
            var entity = await _context.usuarios.FirstOrDefaultAsync(p => p.Codigo == codigo);

            return entity!;
        }

        public async Task<Usuario> RetornarItem(string email)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(p => p.Email.Trim() == email);
            return usuario!;
        }

        public async Task<Usuario> RetornarItem(string email, string senha)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(p => p.Email.Trim() == email && p.Senha.Trim() == senha);
            return usuario!;
        }

        public async Task<IEnumerable<Usuario>> RetornarLista()
        {
            return await _context.usuarios.ToListAsync();
        }

        public async Task<bool> RetornarUsuarioDesbloqueado(string email)
        {
            var usuarioAcesso = await _context.usuarioControleAcessos.FirstOrDefaultAsync(p => p.UsuarioEmail.Trim() == email);
            if (usuarioAcesso is not null && usuarioAcesso.Bloqueado)
            {
                return false;
            }
            else
            {
                return true;
            }
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
