using Manyminds.Domain.Entities;
using Manyminds.Infra.Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Manyminds.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Fornecedor> fornecedors { get; set; }
        public DbSet<PedidoCompra> pedidoCompras { get; set; }
        public DbSet<PedidoCompraItem> pedidoComprasItem { get; set; }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<RegistroLogs> registros { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<UsuarioControleAcesso> usuarioControleAcessos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new PedidoCompraMap());
            modelBuilder.ApplyConfiguration(new PedidoCompraItemMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new RegistroLogsMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioControleAcessoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
