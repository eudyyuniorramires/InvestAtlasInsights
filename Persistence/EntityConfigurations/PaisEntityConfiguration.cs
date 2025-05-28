using Microsoft.EntityFrameworkCore;
using Persistence.Entities;


namespace Persistence.EntityConfigurations
{
    public class PaisEntityConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pais> builder)
        {


            #region Basic Configuration

            builder.ToTable("Paises");

            builder.HasKey(p => p.Id);

            #endregion 

            #region Properties Configurations
            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CodigoISO)
                .IsRequired()
                .HasMaxLength(3);
            #endregion



            #region Relationships
            builder.HasMany<IndicadorPais>(u => u.IndicadoresPaises)
                .WithOne(i => i.Pais)
                .HasForeignKey(i => i.PaisId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }  
}
