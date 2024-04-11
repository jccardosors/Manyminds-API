using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class PedidoCompraItemRepository : IPedidoCompraItemRepository
    {
        public AppDbContext _context;

        public PedidoCompraItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoCompraItem> Adicionar(PedidoCompraItem entity)
        {
            await _context.Set<PedidoCompraItem>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PedidoCompraItem> Alterar(PedidoCompraItem entity)
        {
            var registro = await Task.FromResult(_context.Set<PedidoCompraItem>().Update(entity));
            registro.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Excluir(int codigo)
        {
            var entity = await RetornarItem(codigo);
            _context.Set<PedidoCompraItem>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PedidoCompraItem> RetornarItem(int codigo)
        {
            var entity = await _context.pedidoComprasItem.FirstOrDefaultAsync(p => p.Codigo == codigo);

            return entity!;
        }

        public async Task<IEnumerable<PedidoCompraItem>> RetornarItensPorCodigoPedidoCompra(int pedidoCompraCodigo)
        {
            var entity = await _context.pedidoComprasItem.Where(p => p.PedidoCompraCodigo == pedidoCompraCodigo).ToListAsync();

            return entity!;
        }

        public async Task<IEnumerable<PedidoCompraItem>> RetornarTodos()
        {
            var lista = await _context.pedidoComprasItem.ToListAsync();
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
