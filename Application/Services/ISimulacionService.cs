using Application.ViewModels.SimulacionMacroIndicado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ISimulacionService
    {
        Task<SimulacionViewModel> SimularRankingAsync(int anio);
        Task<SimulacionViewModel> GetRankingAsync(int anio); 

    }
}
