using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class SimulacionMacroIndicador
    {
        public int Id { get; set; }

         [Required]
        public int MacroIndicadorId { get; set; }

        [Required]
        [Range(0.01,1.0)]
        public double Peso { get; set; }

       

        public MacroIndicador? MacroIndicadores { get; set; }
    }
}
