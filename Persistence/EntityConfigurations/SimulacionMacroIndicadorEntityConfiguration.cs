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
    public class SimulacionMacroIndicadorEntityConfiguration : IEntityTypeConfiguration <SimulacionMacroIndicador>
    {
        public void Configure(EntityTypeBuilder<SimulacionMacroIndicador> builder) 
        {


            #region Basic Configuration

            builder.ToTable("SimulacionMacroIndicadores");

            builder.HasKey(p => p.Id);

            #endregion


            #region Properties Configurations 
            builder.Property(p => p.Peso)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            #endregion

            #region Relationships

            builder.HasOne<MacroIndicador>(mi => mi.MacroIndicadores)
                .WithMany()
                .HasForeignKey(mi => mi.MacroIndicadorId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion


        }
    }
}
