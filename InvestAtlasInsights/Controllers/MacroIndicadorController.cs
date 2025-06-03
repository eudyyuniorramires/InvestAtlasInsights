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
            }).ToList();

            return View(listEntityVms);

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
            };

            await _macroIndicadorService.AddAsync(dto);
            return RedirectToRoute(new { Controller = "MacroIndicador", Action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.EditMode = true;

            var dto = await _macroIndicadorService.GetById(id);

            if (dto == null) return NotFound();

            var vm = new SaveMacroIndicadorViewModels
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Peso = dto.Peso,
                MasAltoEsMejor = dto.MasAltoEsMejor
            };


            return View("Save", vm);

        }

        [HttpPost]


        public async Task<IActionResult> Edit(SaveMacroIndicadorViewModels vm, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View("Save");
            }

            var dto = new MacroIndicadorDto
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Peso = vm.Peso,
                MasAltoEsMejor = vm.MasAltoEsMejor
            };

            await _macroIndicadorService.UpdateAsyncEntie(dto, Id);
            return RedirectToRoute(new { Controller = "MacroIndicador", Action = "Index" });
        }



        public async Task<IActionResult> Delete(int Id)
        {
            var dto = await _macroIndicadorService.GetById(Id);

            if (dto == null)
            {
                return RedirectToRoute(new { Controller = "MacroIndicador", Action = "Index" });
            }

            DeleteMacroIndicadorViewModels vm = new()
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Peso = dto.Peso,
                MasAltoEsMejor = dto.MasAltoEsMejor
            };

            return View(vm);
        }




        [HttpPost]
        public async Task<IActionResult> DeleteConfirmacion(int id)
        {
            var result = await _macroIndicadorService.DeleteAsyc(id);

            if (!result)
            {
                TempData["Error"] = "No se pudo eliminar el MacroIndicador.";
            }
            else
            {
                TempData["Success"] = "MacroIndicador eliminado correctamente.";
            }

            return RedirectToAction("Index");
        }
    }
}
