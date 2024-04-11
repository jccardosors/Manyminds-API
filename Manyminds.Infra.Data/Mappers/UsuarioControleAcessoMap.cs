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
    public class UsuarioControleAcessoMap : IEntityTypeConfiguration<UsuarioControleAcesso>
    {
        public void Configure(EntityTypeBuilder<UsuarioControleAcesso> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(p => p.UsuarioEmail);
            builder.Property(p => p.UltimoAcesso);
            builder.Property(p => p.NumeroTentativa);
            builder.Property(p => p.Bloqueado);

            builder.ToTable("Tab_UsuarioControleAcesso");
        }
    }
}
