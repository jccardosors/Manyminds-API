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
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(p => p.FornecedorCodigo);
            builder.Property(p => p.Nome);
            builder.Property(p => p.Valor);
            builder.Property(p => p.Ativo);

            builder.ToTable("Tab_Produto");
        }
    }
}
