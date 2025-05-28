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
    public class PaisRepository
    {


        private readonly ApplicationDbContext _context; 


        public PaisRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Pais> AddAsync(Pais pais)
        {
            if (pais == null) throw new ArgumentNullException(nameof(pais));

            await _context.Paises.AddAsync(pais);
            await _context.SaveChangesAsync();
            return pais;
        }


        public async Task<Pais?> UpdateAsync(int id,Pais pais)
        {
          var entry = await _context.Set<Pais>().FindAsync(id);


            if (entry != null) 
            {

                _context.Entry(entry).CurrentValues.SetValues(pais);
                await _context.SaveChangesAsync();
                return entry;

            }

            return default;

           
        }

        public async Task DeletecAsync(int id)
        {
            var entity = await _context.Set<Pais>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<Pais>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Pais>> GetAllList() 
        {
            return await _context.Set<Pais>().ToListAsync();

        }
         


        public IQueryable<Pais> GetAllQuery() 
        {
           return _context.Set<Pais>().AsQueryable();
        }


        public async Task<Pais?> GetByIdAsync(int id)
        {
            return await _context.Set<Pais>().FindAsync(id);
        }



    }
}
