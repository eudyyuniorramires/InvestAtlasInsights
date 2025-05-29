using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MacroIndicador
{
    public class MacroIndicadorViewModels
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Peso { get; set; }

        public bool MasAltoEsMejor { get; set; }

        public ICollection<IndicadorPais>? IndicadoresPaises { get; set; }
    }
}
