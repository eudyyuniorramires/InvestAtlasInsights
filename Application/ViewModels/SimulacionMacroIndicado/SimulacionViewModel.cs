using Application.Dtos.SimulacionMacroIndicadorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SimulacionMacroIndicado
{
    public class SimulacionViewModel
    {
        public int Id { get; set; }
        public int AñoSeleccionado { get; set; }
        public List<int> AñosDisponibles { get; set; } = new();
        public List<SimulacionRankingDto> ResultadosRanking { get; set; } = new();
        public string? MensajeValidacion { get; set; }
    }
}
