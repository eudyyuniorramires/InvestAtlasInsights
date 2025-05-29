using Application.Dtos.MacroIndicador;
using Application.Services;
using Application.ViewModels.MacroIndicador;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace InvestAtlasInsights.Controllers
{
    public class MacroIndicadorController : Controller
    {
        private readonly MacroIndicadorService _macroIndicadorService;

        public MacroIndicadorController(ApplicationDbContext context) 
        {

            _macroIndicadorService = new MacroIndicadorService(context);

        }
        public async Task<IActionResult> Index() 
        {
        
            var dtos = await _macroIndicadorService.GetAllWithInclude();

            var listEntityVms = dtos.Select(entity => new MacroIndicadorViewModels
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Peso = entity.Peso,
                MasAltoEsMejor = entity.MasAltoEsMejor,
                IndicadoresPaises = entity.IndicadoresPaises
            }).ToList();

            return View (listEntityVms);

        }

        public IActionResult Create()
        {
            return View("Save", new SaveMacroIndicadorViewModels() { Nombre = "", Peso = 0, MasAltoEsMejor = false });
        }



        [HttpPost]

        public async Task<IActionResult> Create(SaveMacroIndicadorViewModels vm) 
        {

            if (!ModelState.IsValid) 
            {
                return View("Save", vm);
            }

            MacroIndicadorDto dto = new()
            {
                Id = 0,
                Nombre = vm.Nombre,
                Peso = vm.Peso,
                MasAltoEsMejor = vm.MasAltoEsMejor,
                IndicadoresPaises = vm.IndicadoresPaises
            };

            await _macroIndicadorService.AddAsync(dto);
            return RedirectToRoute(new { Controller = "MacroIndicador", Action = "Index" });
        }
    }
}
