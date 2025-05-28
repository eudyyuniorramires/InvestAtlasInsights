using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class ConfiguracionRetornoEntityConfiguration : IEntityTypeConfiguration<ConfiguracionRetorno>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionRetorno> builder)
        {


            #region Basic Configuration

            builder.ToTable("CofiguracionRetorno");

            builder.HasKey(p => p.Id);

            #endregion 

            #region Properties Configurations
            builder.Property(p => p.TasaMinima)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            builder.Property(p => p.TasaMaxima)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            #endregion
        }
    }
}
