using Application.Services;
using Application.ViewModels.SimulacionMacroIndicado;
using InvestAtlasInsights.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using Persistence.Repositories;
using System.Threading.Tasks;

namespace InvestAtlasInsights.Controllers
{
    public class HomeController : Controller
    {
        private readonly SimulacionService _simulacionService;
        private readonly MacroIndicadorRepository _macroIndicadorRepository; 

        public HomeController(SimulacionService simulacionService, MacroIndicadorRepository macroIndicadorRepository)
        {
            _simulacionService = simulacionService;
            _macroIndicadorRepository = macroIndicadorRepository;
        }

        public async Task<IActionResult> Index(int? anio)
        {
            int añoSeleccionado = anio ?? DateTime.Now.Year;

            var viewModel = await _simulacionService.GetRankingAsync(añoSeleccionado);

            var macroIndicadores = await _macroIndicadorRepository.GetAllAsync();
            var model = new Tuple<List<MacroIndicador>, SimulacionViewModel>(
                macroIndicadores.ToList(),
                viewModel
            );

            return View(model);
        }
    }
}
