﻿using Application.Dtos.Pais;
using Application.Services;
using Application.ViewModels.Pais;
using Humanizer;
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
        public async Task<IActionResult> Edit(SavePaisViewModels vm)
        {
            PaisDto dto = new()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                CodigoISO = vm.CodigoISO,
            };

            await _paisService.UpdateAsync(dto);
            return RedirectToRoute(new { Controller = "Pais", Action = "Index" });

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var dto = await _paisService.GetById(Id);

            if (dto == null)
            {
                return RedirectToRoute(new { Controller = "Pais", Action = "Index" });
            }

            DeletePaisViewModels vm = new() { Id = dto.Id, Nombre = dto.Nombre };

            return View(vm);
          
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePaisViewModels vm)
        {

            if (!ModelState.IsValid) 
            {

                return View(vm);
            
            }
            await _paisService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { Controller = "Pais", Action = "Index" });

        }
    }
}
