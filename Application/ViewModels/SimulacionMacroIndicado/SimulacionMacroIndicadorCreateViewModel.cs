using Application.Dtos.MacroIndicador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SimulacionMacroIndicado
{
    public class SimulacionMacroIndicadorCreateViewModel
    {
        [Required]
        [Display(Name = "Macroindicador")]
        public int MacroIndicadorId { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "El peso debe estar entre 0 y 1")]
        public double Peso { get; set; }

        public List<MacroIndicadorDto> MacroIndicadoresDisponibles { get; set; } = new();
    }
}
