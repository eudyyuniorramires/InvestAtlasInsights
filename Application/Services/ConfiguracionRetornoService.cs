using Application.Dtos.ConfiguracionRetorno;
using Persistence.Context;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ConfiguracionRetornoService
    {

        private readonly ConfiguracionRetornoRepository configuracionRetornoRepository;


        public ConfiguracionRetornoService(ApplicationDbContext applicationDbContext) 
        {

            configuracionRetornoRepository = new ConfiguracionRetornoRepository(applicationDbContext);

        }



        public async Task<bool> AddAsync(ConfiguracionRetornoDto dto) 
        {
        
            if(dto.TasaMinima > dto.TasaMaxima)
            {
                throw new ArgumentException("La tasa mínima no puede ser mayor que la tasa máxima.");
            }

            try
            {
                var entity = new Persistence.Entities.ConfiguracionRetorno
                {
                    Id = 0,
                    TasaMinima = dto.TasaMinima,
                    TasaMaxima = dto.TasaMaxima
                };

                entity = await configuracionRetornoRepository.AddAsync(entity);

                return entity != null;
            }
            catch (Exception)
            {
                return false;
            }

        } 



        public async Task<bool> UdpateAsync(ConfiguracionRetornoDto dto) 
        {


            if (dto.TasaMinima > dto.TasaMaxima)
            {
                throw new ArgumentException("La tasa mínima no puede ser mayor que la tasa máxima.");
            }

            try
            {
                var entity = new Persistence.Entities.ConfiguracionRetorno
                {
                    Id = dto.Id,
                    TasaMinima = dto.TasaMinima,
                    TasaMaxima = dto.TasaMaxima
                };

                entity = await configuracionRetornoRepository.UpdateAsync(dto.Id, entity);

                return entity != null;
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
            
                await configuracionRetornoRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }


        public async Task<List<ConfiguracionRetornoDto>> GetAll() 
        {
            try 
            {
            
                var listaEntities = await configuracionRetornoRepository.GetAllLList();

                var listaDtos = listaEntities.Select(x => new ConfiguracionRetornoDto
                {
                    Id = x.Id,
                    TasaMinima = x.TasaMinima,
                    TasaMaxima = x.TasaMaxima
                }).ToList();


                return listaDtos;


            }

            catch (Exception)
            {
                return [];
            }


        }

        public async Task <ConfiguracionRetornoDto> GetByIdAsync(int id)
        {
            var i = await configuracionRetornoRepository.GetByIdAsync(id);
            if (i == null)  return null;


            return new ConfiguracionRetornoDto 
            {
                Id = i.Id,
                TasaMinima = i.TasaMinima,
                TasaMaxima = i.TasaMaxima
            };

        }

        
    }
}
