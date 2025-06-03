using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ISimulacionMacroIndicadorRepository
    {
        Task<List<SimulacionMacroIndicador>> GetAllAsync();
        Task<SimulacionMacroIndicador?> GetByIdAsync(int id);
        Task AddAsync(SimulacionMacroIndicador entidad);
        Task UpdateAsync(SimulacionMacroIndicador entidad);
        Task DeleteAsync(int id);
        Task<double> ObtenerSumaPesosAsync();
        Task<bool> ExisteMacroIndicadorEnSimulacionAsync(int macroIndicadorId);
    }
}
