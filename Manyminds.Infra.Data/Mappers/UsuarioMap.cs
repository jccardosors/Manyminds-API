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
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(p => p.Email);
            builder.Property(p => p.Senha);

            builder.ToTable("Tab_Usuario");
        }
    }
}
