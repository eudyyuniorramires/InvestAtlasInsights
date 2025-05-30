using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class IndicadorPaisRepository
    {
        private readonly ApplicationDbContext _context;

        public IndicadorPaisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IndicadorPais> AddAsync(IndicadorPais indicador)
        {
            await _context.IndicadoresPaises.AddAsync(indicador);
            await _context.SaveChangesAsync();
            return indicador;
        }

        public async Task<List<IndicadorPais>> GetAllWithIncludeAsync()
        {
            return await _context.IndicadoresPaises
                .Include(i => i.Pais)
                .Include(i => i.MacroIndicadores)
                .ToListAsync();
        }

        public async Task<IndicadorPais?> GetByIdAsync(int id)
        {
            return await _context.IndicadoresPaises
                .Include(i => i.Pais)
                .Include(i => i.MacroIndicadores)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> ExistsAsync(int paisId, int macroindicadorId, int anio)
        {
            return await _context.IndicadoresPaises
                .AnyAsync(i => i.PaisId == paisId &&
                               i.MacroIndicadorId == macroindicadorId &&
                               i.Anio == anio);



        }

        public async Task<IndicadorPais?> UpdateValorAsync(int id, decimal nuevoValor)
        {
            var indicador = await _context.IndicadoresPaises.FindAsync(id);
            if (indicador != null)
            {
                indicador.Valor = nuevoValor;
                await _context.SaveChangesAsync();
                return indicador;
            }
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var indicador = await _context.IndicadoresPaises.FindAsync(id);
            if (indicador != null)
            {
                _context.IndicadoresPaises.Remove(indicador);
                await _context.SaveChangesAsync();
            }
        }
    }
}
