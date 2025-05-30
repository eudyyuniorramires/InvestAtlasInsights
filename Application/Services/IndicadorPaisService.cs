using Application.Dtos.IndicadorPais;
using Application.ViewModels.IndicadorPais;
using Application.ViewModels.Pais;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class IndicadorPaisService
    {
        private readonly IndicadorPaisRepository _repo;

        public IndicadorPaisService(ApplicationDbContext context)
        {
            _repo = new IndicadorPaisRepository(context);
        }

        public async Task<List<IndicadorPaisDto>> GetAllWithInclude()
        {
            var list = await _repo.GetAllWithIncludeAsync();

            return list.Select(i => new IndicadorPaisDto
            {
                Id = i.Id,
                PaisId = i.PaisId,
                PaisNombre = i.Pais?.Nombre, // Incluye el nombre del país
                MacroIndicadorId = i.MacroIndicadorId,
                MacroIndicadorNombre = i.MacroIndicadores.Nombre, // Incluye el nombre del macroindicador
                Anio = i.Anio,
                Valor = i.Valor
            }).ToList();
        }

        public async Task<IndicadorPaisDto?> GetByIdAsync(int id)
        {
            var i = await _repo.GetByIdAsync(id);
            if (i == null) return null;

            return new IndicadorPaisDto
            {
                Id = i.Id,
                PaisId = i.PaisId,
                MacroIndicadorId = i.MacroIndicadorId,
                Anio = i.Anio,
                Valor = i.Valor
            };
        }

        public async Task<bool> ExistsAsync(int paisId, int macroId, int anio)
        {
         
            
                return await _repo.ExistsAsync(paisId, macroId, anio);
            
        }

        public async Task AddAsync(IndicadorPaisDto dto)
        {
            var entity = new IndicadorPais
            {
                Id = dto.Id,
                PaisId = dto.PaisId,
                MacroIndicadorId = dto.MacroIndicadorId,
                Anio = dto.Anio,
                Valor = dto.Valor
            };


            await _repo.AddAsync(entity);
        }

        public async Task UpdateValorAsync(IndicadorPaisDto dto)
        {
            await _repo.UpdateValorAsync(dto.Id, dto.Valor, dto.Anio, dto.MacroIndicadorId);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }



    }
}
