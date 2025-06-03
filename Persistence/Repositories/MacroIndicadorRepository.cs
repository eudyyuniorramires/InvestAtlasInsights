using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.Context;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class MacroIndicadorRepository
    {

        private readonly ApplicationDbContext _context;

        public MacroIndicadorRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<MacroIndicador> AddAsync(MacroIndicador macroIndicador)
        {
            if (macroIndicador == null) throw new ArgumentNullException(nameof(macroIndicador));

            await _context.MacroIndicadores.AddAsync(macroIndicador);
            await _context.SaveChangesAsync();
            return macroIndicador;
        }


        public async Task<MacroIndicador?> UpdateAsync(int Id, MacroIndicador macroindicador)
        {

            var entry = await _context.Set<MacroIndicador>().FindAsync(Id);

            if (entry != null)
            {

                _context.Entry(entry).CurrentValues.SetValues(macroindicador);
                await _context.SaveChangesAsync();
                return entry;


            }

            return default;

        }



        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var entity = await _context.Set<MacroIndicador>().FindAsync(Id);

                if (entity == null)
                {
                    Console.WriteLine("MacroIndicador no encontrado.");
                    return false;
                }

                _context.Set<MacroIndicador>().Remove(entity);
                await _context.SaveChangesAsync();

                Console.WriteLine("MacroIndicador eliminado.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar MacroIndicador: {ex.Message}");
                throw;
            }
        }






        public async Task<List<MacroIndicador>> GetAllList()
        {

            return await _context.Set<MacroIndicador>().ToListAsync();
        }


        public IQueryable<MacroIndicador> GetAllQuery()
        {
            return _context.Set<MacroIndicador>().AsQueryable();

        }


        public async Task<MacroIndicador?> GetByIdAsync(int Id) 
        {

            return await _context.Set<MacroIndicador>().FindAsync(Id);

        }

        public async Task<IEnumerable<MacroIndicador>> GetAllAsync()
        {
            return await _context.MacroIndicadores.ToListAsync();
        }
    }
}
