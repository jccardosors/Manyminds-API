using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> Adicionar(Produto entity)
        {
            await _context.Set<Produto>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Produto> Alterar(Produto entity)
        {
            var registro = await Task.FromResult(_context.Set<Produto>().Update(entity));
            registro.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Produto> AtivarDesativar(int codigo)
        {
            var produto = await _context.produtos.FirstOrDefaultAsync(p => p.Codigo == codigo);

            produto!.Ativo = produto.Ativo ? false : true;

            await this.Alterar(produto);

            return produto;
        }

        public async Task<Produto> RetornarItem(int codigo)
        {
            var produto = await _context.produtos.FirstOrDefaultAsync(p => p.Codigo == codigo);

            return produto!;
        }

        public async Task<IEnumerable<Produto>> RetornarTodos()
        {
            var lista = await _context.produtos.ToListAsync();
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
