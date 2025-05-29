using Application.Dtos.Pais;
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
    public class PaisService
    {
        private readonly PaisRepository _paisRepository;


        public PaisService(ApplicationDbContext applicationDbContext) 
        {
            _paisRepository = new PaisRepository(applicationDbContext);
        }

        public async Task<bool> AddAsync(PaisDto dto) 
        {


            try
            {
              Pais entity = new() { Id = 0, Nombre = dto.Nombre, CodigoISO = dto.CodigoISO , IndicadoresPaises = dto.IndicadoresPaises };
              entity = await _paisRepository.AddAsync(entity);

               if (entity == null)
               {
                return false;
               }

                return true;

            }
            catch (Exception )
            {
                return false;
            }
           
        }



        public async Task<bool> UpdateAsync(PaisDto dto)
        {


            try
            {
                Pais? entity = new() { Id = dto.Id, Nombre = dto.Nombre, CodigoISO = dto.CodigoISO, IndicadoresPaises = dto.IndicadoresPaises };
                entity = await _paisRepository.UpdateAsync(entity.Id,entity);

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

        public async Task<bool> DeleteAsync(int id)
        {

            try
            {

               await _paisRepository.DeletecAsync(id);
               return true;

            }
            catch (Exception)
            {
                return false;
            }

        }


        public async Task<PaisDto> GetById(int id)
        {

            try
            {

                var entity = await _paisRepository.GetByIdAsync(id);

                if (entity == null)
                {
                    return null;
                }

                PaisDto dto = new () {   Id = entity.Id, Nombre = entity.Nombre,  CodigoISO = entity.CodigoISO, IndicadoresPaises = entity.IndicadoresPaises };

                return dto;

            }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<List<PaisDto>> GetAll()
        {

            try
            {

                var listEntities = await _paisRepository.GetAllList();

                var listEntitiesDto = listEntities.Select(entity => new PaisDto
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    CodigoISO = entity.CodigoISO,
                    IndicadoresPaises = entity.IndicadoresPaises
                }).ToList();

                return listEntitiesDto;


            }
            catch (Exception)
            {
                return [];
            }

        }



        public async Task<List<PaisDto>> GetAllWithInclude()
        {

            try
            {

                var listEntitiesQuery =  _paisRepository.GetAllQuery();

                var listEntity =  await listEntitiesQuery.Include(at => at.IndicadoresPaises).ToListAsync();

                var listEntitiesDto = listEntity.Select(entity => new PaisDto
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    CodigoISO = entity.CodigoISO,
                    IndicadoresPaises = entity.IndicadoresPaises
                }).ToList();

                return listEntitiesDto;


            }
            catch (Exception)
            {
                return [];
            }

        }


    }
}
