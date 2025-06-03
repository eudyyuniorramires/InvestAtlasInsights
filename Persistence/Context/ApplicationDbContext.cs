using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext 
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
       
        public DbSet<Entities.Pais> Paises { get; set; }

        public DbSet<Entities.IndicadorPais> IndicadoresPaises { get; set; }

        public DbSet<Entities.MacroIndicador> MacroIndicadores { get; set; }

        public DbSet<Entities.ConfiguracionRetorno> ConfiguracionesRetorno { get; set; }


        public DbSet<Entities.SimulacionMacroIndicador> SimulacionesMacroIndicadores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new PaisEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IndicadorPaisEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MacroIndicadorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaisEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SimulacionMacroIndicadorEntityConfiguration());



        }



    }
}
