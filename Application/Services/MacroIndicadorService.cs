using Application.Dtos.MacroIndicador;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MacroIndicadorService
    {

        private readonly MacroIndicadorRepository _macroIndicadorRepository;
        private readonly ApplicationDbContext _context; 


        public MacroIndicadorService(ApplicationDbContext applicationDbContext)
        {

            _context = applicationDbContext;

            _macroIndicadorRepository = new MacroIndicadorRepository(applicationDbContext);

        }


        public async Task<bool> AddAsync(MacroIndicadorDto dto)
        {
            try
            {

                var MacroIndicadorExistente = await _macroIndicadorRepository.GetAllList();

                var totalPesoActual = MacroIndicadorExistente.Sum(m => m.Peso);

                if (totalPesoActual + dto.Peso > 1)
                {
                    return false;
                }

                MacroIndicador entity = new()
                {
                    Id = 0,
                    Nombre = dto.Nombre,
                    Peso = dto.Peso,
                    MasAltoEsMejor = dto.MasAltoEsMejor,
                };
                entity = await _macroIndicadorRepository.AddAsync(entity);

                if (entity == null)
                {
                    return false;
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsyc(int id)
        {
            try
            {
                // Primero buscar el MacroIndicador
                var entity = await _macroIndicadorRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return false;
                }

                // Eliminar los SimulacionMacroIndicadores relacionados
                var relacionados = await _context.SimulacionesMacroIndicadores
                    .Where(s => s.MacroIndicadorId == id)
                    .ToListAsync();

                if (relacionados.Any())
                {
                    _context.SimulacionesMacroIndicadores.RemoveRange(relacionados);
                    await _context.SaveChangesAsync();
                }

                // Ahora eliminar el MacroIndicador
                return await _macroIndicadorRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<MacroIndicadorDto> GetById(int id)
        {
            try
            {

                var entity = await _macroIndicadorRepository.GetByIdAsync(id);

                if (entity == null)
                {
                    return null;
                }

                return new MacroIndicadorDto
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    Peso = entity.Peso,
                    MasAltoEsMejor = entity.MasAltoEsMejor,
                };

            }
            catch (Exception)
            {
                return null;

            }


        }

        public async Task<List<MacroIndicadorDto>> GetAll()
        {

            try
            {

                var listaEntities = await _macroIndicadorRepository.GetAllList();

                var listaEntiesDto = listaEntities.Select(entity => new MacroIndicadorDto
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    Peso = entity.Peso,
                    MasAltoEsMejor = entity.MasAltoEsMejor,
                }).ToList();


                return listaEntiesDto;

            }

            catch (Exception)
            {

                return [];
            }

        }


        public async Task<List<MacroIndicador>> GetAllWithInclude()
        {
            try
            {
                var listsEntitiesQuery = _macroIndicadorRepository.GetAllQuery();

                var listaEntities = await listsEntitiesQuery.Include(m => m.IndicadoresPaises).ToListAsync();

                var listaEntitiesDto = listaEntities.Select(entity => new MacroIndicador
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    Peso = entity.Peso,
                    MasAltoEsMejor = entity.MasAltoEsMejor,
                    IndicadoresPaises = entity.IndicadoresPaises
                }).ToList();

                return listaEntitiesDto;



            }
            catch (Exception)
            {
                return [];
            }

        }




        public async Task<bool> UpdateAsyncEntie(MacroIndicadorDto dto, int Id)
        {
            try
            {
                var entity = await _macroIndicadorRepository.GetByIdAsync(Id);

                if (entity == null)
                {
                    return false;
                }

                var otrosIndicadores = (await _macroIndicadorRepository.GetAllList())
                    .Where(m => m.Id != Id);

                var totalPesoOtros = otrosIndicadores.Sum(m => m.Peso);

                if (totalPesoOtros + dto.Peso > 1)
                {
                    return false;
                }

                entity.Nombre = dto.Nombre;
                entity.Peso = dto.Peso;
                entity.MasAltoEsMejor = dto.MasAltoEsMejor;

                var updatedEntity = await _macroIndicadorRepository.UpdateAsync(Id, entity);

                return updatedEntity != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
