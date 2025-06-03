using Application.Dtos.MacroIndicador;
using Application.Services;
using Application.ViewModels.SimulacionMacroIndicado;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Repositories;

public class SimulacionController : Controller
{
    private readonly ApplicationDbContext _context;

    private readonly SimulacionMacroIndicadorRepository _simulacionRepo;

    private readonly PaisRepository _PaisRepository;


    private readonly SimulacionService _simulacionService;

    public SimulacionController(
     ApplicationDbContext context,
     SimulacionMacroIndicadorRepository simulacionRepo,
     PaisRepository paisRepository,
     SimulacionService simulacionService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _simulacionRepo = simulacionRepo ?? throw new ArgumentNullException(nameof(simulacionRepo));
        _PaisRepository = paisRepository ?? throw new ArgumentNullException(nameof(paisRepository));
        _simulacionService = simulacionService ?? throw new ArgumentNullException(nameof(simulacionService));
    }

    public async Task<IActionResult> Index()
    {
        var lista = await _context.SimulacionesMacroIndicadores
            .Include(s => s.MacroIndicadores)
            .ToListAsync();

        ViewBag.TotalPeso = lista.Sum(s => s.Peso);
        return View(lista);
    }

    public async Task<IActionResult> Create()
    {
        var usadosIds = _context.SimulacionesMacroIndicadores.Select(s => s.MacroIndicadorId).ToList();

        var disponibles = await _context.MacroIndicadores
            .Where(m => !usadosIds.Contains(m.Id))
            .Select(m => new MacroIndicadorDto { Id = m.Id, Nombre = m.Nombre })
            .ToListAsync();

        var viewModel = new SimulacionMacroIndicadorCreateViewModel
        {
            MacroIndicadoresDisponibles = disponibles
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SimulacionMacroIndicadorCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.MacroIndicadoresDisponibles = await _context.MacroIndicadores
                .Select(m => new MacroIndicadorDto { Id = m.Id, Nombre = m.Nombre })
                .ToListAsync();
            return View(model);
        }

        var sumaActual = await _context.SimulacionesMacroIndicadores.SumAsync(s => s.Peso);
        if (sumaActual + model.Peso > 1)
        {
            ModelState.AddModelError("", "La suma de los pesos no puede superar 1.");
            model.MacroIndicadoresDisponibles = await _context.MacroIndicadores
                .Select(m => new MacroIndicadorDto { Id = m.Id, Nombre = m.Nombre })
                .ToListAsync();
            return View(model);
        }

        _context.SimulacionesMacroIndicadores.Add(new SimulacionMacroIndicador
        {
            MacroIndicadorId = model.MacroIndicadorId,
            Peso = (double)model.Peso
        });

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var sim = await _context.SimulacionesMacroIndicadores
            .Include(s => s.MacroIndicadores)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sim == null) return NotFound();

        return View(new SimulacionMacroIndicadorEditViewModel
        {
            Id = sim.Id,
            Peso = sim.Peso,
            NombreMacroIndicador = sim.MacroIndicadores.Nombre ?? "Nombre no disponible"
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(SimulacionMacroIndicadorEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var sim = await _context.SimulacionesMacroIndicadores.FindAsync(model.Id);
        if (sim == null) return NotFound();

        var sumaSinEste = await _context.SimulacionesMacroIndicadores
            .Where(s => s.Id != model.Id)
            .SumAsync(s => s.Peso);

        if (sumaSinEste + model.Peso > 1)
        {
            ModelState.AddModelError("", "La suma de los pesos no puede superar 1.");
            return View(model);
        }

        sim.Peso = (double)model.Peso;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var sim = await _context.SimulacionesMacroIndicadores
            .Include(s => s.MacroIndicadores)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sim == null) return NotFound();

        return View(sim);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sim = await _context.SimulacionesMacroIndicadores.FindAsync(id);
        if (sim != null)
        {
            _context.SimulacionesMacroIndicadores.Remove(sim);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ConfiguracionSimulacion(int id, int? anio)
    {
        var simulacion = await _simulacionRepo.GetAllAsync();
        var viewModel = new SimulacionViewModel
        {
            Id = id,
            AñoSeleccionado = anio ?? DateTime.Now.Year,
            AñosDisponibles = await _PaisRepository.GetAniosDisponiblesAsync()
        };

        if (anio.HasValue)
        {
            var simulacionResultado = await _simulacionService.SimularRankingAsync(anio.Value);
            viewModel.ResultadosRanking = simulacionResultado.ResultadosRanking;
            viewModel.MensajeValidacion = simulacionResultado.MensajeValidacion;
        }

        ViewBag.TotalPeso = simulacion.Sum(s => s.Peso);

        return View("ConfiguracionSimulacion", Tuple.Create(simulacion, viewModel));
    }

    [HttpPost]
    public async Task<IActionResult> SimularRanking(int Id, int anio)
    {
        var viewModel = await _simulacionService.SimularRankingAsync(anio);
        return View("SimularRanking", viewModel); 
    }

}
