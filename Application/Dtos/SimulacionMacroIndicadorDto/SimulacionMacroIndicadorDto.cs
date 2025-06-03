using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SimulacionMacroIndicadorDto
{
    public class SimulacionMacroIndicadorDto
    {
        public int Id { get; set; }
        public string NombrePais { get; set; }
        public decimal Scoring { get; set; }

        public decimal peso { get; set; }
        public decimal TasaRetornoEstimada { get; set; }
    }
}
