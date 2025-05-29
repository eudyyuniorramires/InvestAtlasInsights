using Application.Dtos.Pais;
using Application.Services;
using Application.ViewModels.Pais;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace InvestAtlasInsights.Controllers
{
    public class PaisController : Controller
    {

        private readonly PaisService _paisService;


        public PaisController(ApplicationDbContext context)
        {
            _paisService = new PaisService(context);
        }


        public async  Task <IActionResult> Index()
        {
          var dtos =  await _paisService.GetAllWithInclude();

            var listEntityVms = dtos.Select(entity => new PaisViewModels
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                CodigoISO = entity.CodigoISO,
                IndicadoresPaises = entity.IndicadoresPaises
            }).ToList();

            return View(listEntityVms);

        }

        public IActionResult Create()
        {
            return View("Save", new SavePaisViewModels() { Nombre = "" ,CodigoISO =""});
        }



        [HttpPost]

        public async Task<IActionResult> Create(SavePaisViewModels vm)
        {

            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
      
            
             PaisDto  dto = new()
               {
                Id = 0,
                Nombre = vm.Nombre,
                CodigoISO = vm.CodigoISO,
                IndicadoresPaises = vm.IndicadoresPaises
              };

            await _paisService.AddAsync(dto);
            return RedirectToRoute(new { Controller = "Pais", Action = "Index" });


        }


        public async Task <IActionResult> Edit(int id)
        {
            ViewBag.EditMode = true;
             var dto = await _paisService.GetById(id);

            if(dto == null)
            {
                return RedirectToRoute(new { Controller = "Pais", Action = "Index" });
            }

            SavePaisViewModels vm = new () {Id = dto.Id, Nombre = dto.Nombre,CodigoISO = dto.CodigoISO };

            return View("Save" ,vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id,string Nombre)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int Id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int Id)
        {
            return View();
        }
    }
}
