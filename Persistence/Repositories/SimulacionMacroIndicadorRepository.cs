using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SimulacionMacroIndicadorRepository : ISimulacionMacroIndicadorRepository
    {


        private readonly ApplicationDbContext _context;

        public SimulacionMacroIndicadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SimulacionMacroIndicador>> GetAllAsync()
        {
            return await _context.SimulacionesMacroIndicadores
                .Include(s => s.MacroIndicadores)
                .ToListAsync();
        }

        public async Task<SimulacionMacroIndicador?> GetByIdAsync(int id)
        {
            return await _context.SimulacionesMacroIndicadores
                .Include(s => s.MacroIndicadores)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(SimulacionMacroIndicador entidad)
        {
            _context.SimulacionesMacroIndicadores.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SimulacionMacroIndicador entidad)
        {
            _context.SimulacionesMacroIndicadores.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.SimulacionesMacroIndicadores.FindAsync(id);
            if (entidad != null)
            {
                _context.SimulacionesMacroIndicadores.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<double> ObtenerSumaPesosAsync()
        {
            return await _context.SimulacionesMacroIndicadores.SumAsync(s => s.Peso);
        }

        public async Task<bool> ExisteMacroIndicadorEnSimulacionAsync(int macroIndicadorId)
        {
            return await _context.SimulacionesMacroIndicadores
                .AnyAsync(s => s.MacroIndicadorId == macroIndicadorId);
        }

        
    }
}

