using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public AppDbContext _context;

        public FornecedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Fornecedor> Adicionar(Fornecedor pedidoCompra)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> Alterar(Fornecedor pedido)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<Fornecedor> RetornarItem(int codigo)
        {
            var entity = await _context.fornecedors.FirstOrDefaultAsync(p => p.Codigo == codigo);

            return entity!;
        }

        public async Task<IEnumerable<Fornecedor>> RetornarTodos()
        {
            var entity = await _context.fornecedors.ToListAsync();

            return entity!;
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
