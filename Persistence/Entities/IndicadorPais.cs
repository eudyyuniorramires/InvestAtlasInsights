using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class IndicadorPais
    {
        public int Id { get; set; }

        public int PaisId { get; set; }

        public required Pais Pais { get; set; }

        public int MacroIndicadorId { get; set; }

        public MacroIndicador MacroIndicadores { get; set; }

        public decimal Valor { get; set; }

        public DateTime Año { get; set; }


      


    }
}
