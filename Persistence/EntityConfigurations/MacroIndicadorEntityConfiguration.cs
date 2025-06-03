using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;


namespace Persistence.EntityConfigurations
{
    public class MacroIndicadorEntityConfiguration : IEntityTypeConfiguration<MacroIndicador>
    {
        public void Configure(EntityTypeBuilder<MacroIndicador> builder)
        {
            #region Basic Configuration

            builder.ToTable("MacroIndicadores"); 

            builder.HasKey(p => p.Id);

            #endregion 

            #region Properties Configurations
            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Peso)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); 

            builder.Property(p => p.MasAltoEsMejor)
                .IsRequired()
                .HasDefaultValue(false);

            #endregion

            #region Relationships
            builder.HasMany<IndicadorPais>(mi => mi.IndicadoresPaises)
                .WithOne(ip => ip.MacroIndicadores) 
                .HasForeignKey(ip => ip.MacroIndicadorId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}