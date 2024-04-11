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
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.ToTable("Tab_Fornecedor");
        }
    }
}
