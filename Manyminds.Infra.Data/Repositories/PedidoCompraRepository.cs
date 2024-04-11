using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class PedidoCompraRepository : IPedidoCompraRepository
    {
        public AppDbContext _context;

        public PedidoCompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoCompra> Adicionar(PedidoCompra entity)
        {
            await _context.Set<PedidoCompra>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PedidoCompra> Alterar(PedidoCompra entity)
        {
            var registro = await Task.FromResult(_context.Set<PedidoCompra>().Update(entity));
            registro.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Excluir(int codigo)
        {
            var entity = await RetornarItem(codigo);
            _context.Set<PedidoCompra>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PedidoCompra> RetornarItem(int codigo)
        {
            var entity = await _context.pedidoCompras.FirstOrDefaultAsync(p => p.Codigo == codigo);

            return entity!;
        }

        public async Task<IEnumerable<PedidoCompra>> RetornarTodos()
        {
            var lista = await _context.pedidoCompras.ToListAsync();
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
