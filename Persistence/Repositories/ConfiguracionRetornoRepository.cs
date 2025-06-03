using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.Context;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ConfiguracionRetornoRepository
    {
        private readonly ApplicationDbContext _context;


        public ConfiguracionRetornoRepository(ApplicationDbContext context)
        {

            _context = context;

        }


        public async Task<ConfiguracionRetorno> AddAsync(ConfiguracionRetorno configuracionRetorno)
        {
            if (configuracionRetorno == null) throw new ArgumentNullException(nameof(configuracionRetorno));

            await _context.ConfiguracionesRetorno.AddAsync(configuracionRetorno);
            await _context.SaveChangesAsync();
            return configuracionRetorno;




        }



        public async Task<ConfiguracionRetorno?> UpdateAsync(int id, ConfiguracionRetorno configuracionRetorno)
        {
            var entry = await _context.ConfiguracionesRetorno.FindAsync(id);

            if (entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(configuracionRetorno);
                await _context.SaveChangesAsync();
                return entry;
            }

            return default;
        }



        public async Task DeleteAsync(int id)
        {
            var retorno = await _context.ConfiguracionesRetorno.FindAsync(id);

            if (retorno != null)
            {

                _context.ConfiguracionesRetorno.Remove(retorno);
                await _context.SaveChangesAsync();

            }

        }




        public async Task<List<ConfiguracionRetorno>> GetAllLList()
        {

            return await _context.ConfiguracionesRetorno.ToListAsync();

        }



        public async Task<ConfiguracionRetorno?> GetByIdAsync(int id)
        {
            return await _context.ConfiguracionesRetorno
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public IQueryable<ConfiguracionRetorno> GetAllQuery()
        {
            return _context.Set<ConfiguracionRetorno>().AsQueryable();




        }

        public async Task<ConfiguracionRetorno?> GetLatestAsync()
        {
            return await _context.ConfiguracionesRetorno
                .OrderByDescending(c => c.Id) 
                .FirstOrDefaultAsync();
        }

        public async Task<(double Min, double Max)> GetTasaRetornoAsync()
        {
            var configuracion = await _context.ConfiguracionesRetorno.FirstOrDefaultAsync();

            if (configuracion == null || configuracion.TasaMinima <= 0 || configuracion.TasaMaxima <= 0)
            {
                return (2.0, 15.0);
            }

            return ((double)configuracion.TasaMinima, (double)configuracion.TasaMaxima);
        }
    }
}
