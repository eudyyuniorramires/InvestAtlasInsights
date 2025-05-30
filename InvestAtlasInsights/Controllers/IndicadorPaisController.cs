using Application.Dtos.IndicadorPais;
using Application.Services;
using Application.ViewModels.IndicadorPais;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.Context;

namespace InvestAtlasInsights.Controllers
{
    public class IndicadorPaisController : Controller
    {
        private readonly IndicadorPaisService _indicadorService;
        private readonly PaisService _paisService;
        private readonly MacroIndicadorService _macroService;

        public IndicadorPaisController(ApplicationDbContext context)
        {
            _indicadorService = new IndicadorPaisService(context);
            _paisService = new PaisService(context);
            _macroService = new MacroIndicadorService(context);
        }

        private async Task<SaveIndicadorPaisViewModel> LoadSelectLists(SaveIndicadorPaisViewModel vm)
        {
            var paises = await _paisService.GetAll();
            var macros = await _macroService.GetAll();

            vm.Paises = paises.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();
            vm.Macroindicadores = macros.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }).ToList();

            return vm;
        }

        public async Task<IActionResult> Index()
        {
            var paises = await _paisService.GetAll();
            ViewBag.Paises = paises.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();

            var dtos = await _indicadorService.GetAllWithInclude();

            return View(dtos);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new SaveIndicadorPaisViewModel();
            return View("Save", await LoadSelectLists(vm));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveIndicadorPaisViewModel vm)
        {
            if (await _indicadorService.ExistsAsync(vm.PaisId, vm.MacroindicadorId, vm.Anio))
            {
                ModelState.AddModelError("", "Ya existe un indicador con esta combinación.");
            }

            if (!ModelState.IsValid)
                return View("Save", await LoadSelectLists(vm));

            var dto = new IndicadorPaisDto
            {
                PaisId = vm.PaisId,
                MacroIndicadorId = vm.MacroindicadorId,
                Anio = vm.Anio,
                Valor = vm.Valor
                

            };

            await _indicadorService.AddAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _indicadorService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new SaveIndicadorPaisViewModel
            {
                Id = dto.Id,
                PaisId = dto.PaisId,
                MacroindicadorId = dto.MacroIndicadorId,
                Anio = dto.Anio,
                Valor = dto.Valor
            };

            return View("Save", await LoadSelectLists(vm));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveIndicadorPaisViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", await LoadSelectLists(vm));

            var dto = new IndicadorPaisDto
            {
                Id = vm.Id,
                Valor = vm.Valor
            };

            await _indicadorService.UpdateValorAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
