using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SimulacionMacroIndicado
{
    public class SimulacionMacroIndicadorEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "El peso debe estar entre 0 y 1")]
        public double Peso { get; set; }

        public string NombreMacroIndicador { get; set; } = string.Empty;
    }
}
