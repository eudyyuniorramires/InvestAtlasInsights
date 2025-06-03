using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SimulacionMacroIndicadorDto
{
    public class SimulacionRankingDto
    {
        public string NombrePais { get; set; } = null!;
        public string CodigoIso { get; set; } = null!;
        public double Scoring { get; set; }
        public double TasaRetorno { get; set; }
    }
}
