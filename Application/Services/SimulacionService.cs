using Application.Dtos.SimulacionMacroIndicadorDto;
using Application.ViewModels.SimulacionMacroIndicado;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SimulacionService : ISimulacionService
    {
        private readonly MacroIndicadorRepository _macroIndicadorRepository;
        private readonly PaisRepository _paisRepository;
        private readonly IndicadorPaisRepository _indicadorPaisRepository;
        private readonly ConfiguracionRetornoRepository _tasaRetornoRepository;
        private readonly SimulacionMacroIndicadorRepository _simulacionRepo;
        public SimulacionService(ApplicationDbContext applicationDbContext)
        {
            _macroIndicadorRepository = new MacroIndicadorRepository(applicationDbContext);
            _paisRepository = new PaisRepository(applicationDbContext);
            _indicadorPaisRepository = new IndicadorPaisRepository(applicationDbContext);
            _tasaRetornoRepository = new ConfiguracionRetornoRepository(applicationDbContext);
            _simulacionRepo = new SimulacionMacroIndicadorRepository(applicationDbContext);
        }

        public async Task<SimulacionViewModel> SimularRankingAsync(int anio)
        {
            var simulacionMacros = await _simulacionRepo.GetAllAsync();

            var sumaPesos = simulacionMacros.Sum(m => m.Peso);
            if (Math.Abs(sumaPesos - 1) > 0.0001)
            {
                return new SimulacionViewModel
                {
                    AñoSeleccionado = anio,
                    MensajeValidacion = "Se deben ajustar los pesos de los macroindicadores agregado a la simulación hasta que la suma de lo mismo sea igual a 1"
                };
            }

            var paises = await _paisRepository.GetAllList();
            var indicadores = await _indicadorPaisRepository.GetByAnioAsync(anio); // Obtener todos los indicadores para el año

            var paisesElegibles = new List<(Pais pais, List<IndicadorPais> indicadores)>();

            foreach (var pais in paises)
            {
                var indicadoresPais = indicadores
                    .Where(i => i.PaisId == pais.Id)
                    .ToList();

                var validos = simulacionMacros
                    .Where(s => s.Peso > 0)
                    .All(sm => indicadoresPais.Any(ip => ip.MacroIndicadorId == sm.MacroIndicadorId));

                if (validos)
                    paisesElegibles.Add((pais, indicadoresPais));
            }

            if (paisesElegibles.Count < 2)
            {
                var mensaje = paisesElegibles.Any()
                    ? $"No hay suficiente países para poder calcular la simulación del ranking y la tasa de retorno, el único país que cumple con los requisitos es {paisesElegibles.First().pais.Nombre}"
                    : "No hay países que cumplan con los requisitos para calcular la simulación del ranking y la tasa de retorno.";

                return new SimulacionViewModel
                {
                    AñoSeleccionado = anio,
                    MensajeValidacion = mensaje
                };
            }

            var resultados = new List<SimulacionRankingDto>();

            foreach (var sm in simulacionMacros)
            {
                var valores = paisesElegibles.Select(pe =>
                    pe.indicadores.First(i => i.MacroIndicadorId == sm.MacroIndicadorId).Valor
                ).ToList();

                var min = valores.Min();
                var max = valores.Max();

                foreach (var (pais, indicadoresPais) in paisesElegibles)
                {
                    var valor = indicadoresPais.First(i => i.MacroIndicadorId == sm.MacroIndicadorId).Valor;
                    double norm;

                    var indicador = indicadoresPais.First(i => i.MacroIndicadorId == sm.MacroIndicadorId).MacroIndicadores;
                    if (min == max)
                    {
                        norm = 0.5;
                    }
                    else
                    {
                        norm = (double)(indicador.MasAltoEsMejor
                            ? (valor - min) / (max - min)
                            : 1 - ((valor - min) / (max - min)));
                    }

                    if (!resultados.Any(r => r.CodigoIso == pais.CodigoISO))
                    {
                        resultados.Add(new SimulacionRankingDto
                        {
                            CodigoIso = pais.CodigoISO,
                            NombrePais = pais.Nombre,
                            Scoring = 0
                        });
                    }

                    resultados.First(r => r.CodigoIso == pais.CodigoISO).Scoring += norm * (double)sm.Peso;
                }
            }

            var tasas = await _tasaRetornoRepository.GetTasaRetornoAsync();
            var rmin = tasas.Item1;
            var rmax = tasas.Item2;

            foreach (var r in resultados)
            {
                r.TasaRetorno = Math.Round(rmin + (rmax - rmin) * r.Scoring, 2);
            }

            var resultadosOrdenados = resultados.OrderByDescending(r => r.Scoring).ToList();

            return new SimulacionViewModel
            {
                AñoSeleccionado = anio,
                ResultadosRanking = resultadosOrdenados
            };
        }
    }
}
