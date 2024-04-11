using Manyminds.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manyminds.Infra.Data.Mappers
{
    public class PedidoCompraMap : IEntityTypeConfiguration<PedidoCompra>
    {
        public void Configure(EntityTypeBuilder<PedidoCompra> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.HasIndex(p => p.Codigo).IsUnique();
           
            builder.Property(p => p.FornecedorCodigo);   
            builder.Property(p => p.Data);
            builder.Property(p => p.Observacao);
            builder.Property(p => p.Status);
            builder.Property(p => p.ValorTotal);

            builder.ToTable("Tab_PedidoCompra");
        }
    }
}
