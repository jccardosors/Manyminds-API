using Manyminds.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manyminds.Infra.Data.Mappers
{
    public class PedidoCompraItemMap : IEntityTypeConfiguration<PedidoCompraItem>
    {
        public void Configure(EntityTypeBuilder<PedidoCompraItem> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(p => p.PedidoCompraCodigo);
            builder.Property(p => p.ProdutoCodigo);
            builder.Property(p => p.Quantidade);

            builder.ToTable("Tab_PedidoCompraItem");
        }
    }
}
