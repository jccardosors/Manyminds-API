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
    public class RegistroLogsMap : IEntityTypeConfiguration<RegistroLogs>
    {
        public void Configure(EntityTypeBuilder<RegistroLogs> builder)
        {
            builder.HasKey(p => p.Codigo);
            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(p => p.UsuarioCodigo);
            builder.Property(p => p.Data);
            builder.Property(p => p.Descricao);

            builder.ToTable("Tab_RegistroLogs");
        }
    }
}
