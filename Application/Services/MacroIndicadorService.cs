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

        public MacroIndicadorService(ApplicationDbContext applicationDbContext) 
        {
        
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

        public async Task<bool> Delete(int Id) 
        {

            try 
            {
              await _macroIndicadorRepository.DeleteAsync(Id);
              return true;
            }
            catch(Exception)
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

                MacroIndicadorDto dto = new ()
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    Peso = entity.Peso,
                    MasAltoEsMejor = entity.MasAltoEsMejor,
                };

                return dto;

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
                var listsEntitiesQuery =  _macroIndicadorRepository.GetAllQuery();

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
    }
}
