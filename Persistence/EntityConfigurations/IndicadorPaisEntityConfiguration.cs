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
    public class IndicadorPaisEntityConfiguration : IEntityTypeConfiguration<IndicadorPais>
    {
        public void Configure(EntityTypeBuilder<IndicadorPais> builder)
        {

            builder.ToTable("IndicadoresPaises"); 

            builder.HasKey(ip => ip.Id);

            #region Configuración de propiedades

            builder.Property(ip => ip.Valor)
                .IsRequired()  
                .HasColumnType("decimal(18,2)");


            builder.Property(ip => ip.Año)
                .IsRequired();

            #endregion

            #region Configuración de relaciones

            
            builder.HasOne(ip => ip.Pais)  
                .WithMany(p => p.IndicadoresPaises)  
                .HasForeignKey(ip => ip.PaisId)  
                .IsRequired()  
                .OnDelete(DeleteBehavior.Cascade);  

            
            builder.HasOne(ip => ip.MacroIndicadores) 
                .WithMany()  
                .HasForeignKey(ip => ip.MacroIndicadorId) 
                .IsRequired()  
                .OnDelete(DeleteBehavior.Restrict); 

            #endregion

        }
    }
}
