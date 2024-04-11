using Manyminds.Domain.Entities;
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
            var tab0 = modelBuilder.Entity<Fornecedor>();
            tab0.ToTable("Tab_Fornecedor");
            tab0.HasKey("Codigo");

            var tab1 = modelBuilder.Entity<PedidoCompra>();
            tab1.ToTable("Tab_PedidoCompra");
            tab1.HasKey("Codigo");

            var tab2 = modelBuilder.Entity<PedidoCompraItem>();
            tab2.ToTable("Tab_PedidoCompraItem");
            tab2.HasKey("Codigo");

            var tab3 = modelBuilder.Entity<Produto>();
            tab3.ToTable("Tab_Produto");
            tab3.HasKey("Codigo");

            var tab4 = modelBuilder.Entity<RegistroLogs>();
            tab4.ToTable("Tab_RegistroLogs");
            tab4.HasKey("Codigo");

            var tab5 = modelBuilder.Entity<Usuario>();
            tab5.ToTable("Tab_Usuario");
            tab5.HasKey("Codigo");

            var tab6 = modelBuilder.Entity<UsuarioControleAcesso>();
            tab6.ToTable("Tab_UsuarioControleAcesso");
            tab6.HasKey("Codigo");


            base.OnModelCreating(modelBuilder);
        }

    }
}
