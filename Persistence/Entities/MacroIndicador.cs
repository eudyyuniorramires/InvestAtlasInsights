using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class MacroIndicador
    {
        public int Id { get; set; }

        public string Nombre { get; set; } 

        public decimal Peso { get; set; }

        public bool MasAltoEsMejor { get; set; }

        public virtual ICollection<IndicadorPais> ?IndicadoresPaises { get; set; }
      
    }
}
