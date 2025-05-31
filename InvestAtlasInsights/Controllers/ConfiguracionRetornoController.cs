using Application.Dtos.ConfiguracionRetorno;
using Application.Services;
using Application.ViewModels.ConfiguracionRetorno;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using Persistence.Entities;
using System.Security.Policy;

namespace InvestAtlasInsights.Controllers
{
    public class ConfiguracionRetornoController : Controller
    {

        private readonly ConfiguracionRetornoService _configuracionRetornoService;


        public ConfiguracionRetornoController(ApplicationDbContext context)
        {
            _configuracionRetornoService = new ConfiguracionRetornoService(context);
        }

        public async Task <IActionResult> Index()
        {

            var dtos = await _configuracionRetornoService.GetAll();


            var listEntityVms = dtos.Select(entity => new ConfiguracionRetornoViewModels
            {
                Id = entity.Id,
                TasaMinima = entity.TasaMinima,
                TasaMaxima = entity.TasaMaxima
            }).ToList();

            return View(listEntityVms);
        }


        public async Task<IActionResult> Create()
        {
            return View("Save", new SaveConfiguracionRetornoViewModel() { Id = 0, TasaMaxima = 0, TasaMinima = 0 });
        }


        [HttpPost]

        public async Task<IActionResult> Create(SaveConfiguracionRetornoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            ConfiguracionRetornoDto dto = new()
            {
                Id = vm.Id,
                TasaMinima = vm.TasaMinima,
                TasaMaxima = vm.TasaMaxima
            };

            await _configuracionRetornoService.AddAsync(dto);

            return RedirectToRoute(new { Controller = "ConfiguracionRetorno", Action = "Index" });

        }



        public async Task<IActionResult> Edit(int id)
        {

            ViewBag.EditMode = true;
            var dto = await _configuracionRetornoService.GetByIdAsync(id);

            if (dto == null)
            {

                return RedirectToRoute(new { controller = "ConfiguracionRetorno", Action = "Index" });
            }

            SaveConfiguracionRetornoViewModel vm = new()
            {
                Id = dto.Id,
                TasaMinima = dto.TasaMinima,
                TasaMaxima = dto.TasaMaxima
            };

            return View("Save", vm);


        }



        [HttpPost]


        public async Task<IActionResult> Edit(SaveConfiguracionRetornoViewModel vm)
        {

            ConfiguracionRetornoDto dto = new()
            {
                Id = vm.Id,
                TasaMinima = vm.TasaMinima,
                TasaMaxima = vm.TasaMaxima
            };

            await _configuracionRetornoService.UdpateAsync(dto);
            return RedirectToRoute(new { Controller = "ConfiguracionRetorno", Action = "Index" });


        }



        public async Task<IActionResult> Delete(int id)
        {

            var dto = await _configuracionRetornoService.GetByIdAsync(id);

            if (dto == null)
            {

                return RedirectToRoute(new { Controller = "ConfiguracionRetorno", Action = "Index" });

            }


            DeleteConfiguracionRetornoViewModels vm = new()
            {

                Id = dto.Id,
                TasaMinima = dto.TasaMinima,
                TasaMaxima = dto.TasaMaxima
            };

            return View(vm);



        }

        [HttpPost]


        public async Task<IActionResult> Delete(DeleteConfiguracionRetornoViewModels vm)
        {

            if (!ModelState.IsValid)
            {

                return View(vm);
            }

            await _configuracionRetornoService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { Controller = "ConfiguracionRetorno", Action = "Index" });

        }

    }
}

