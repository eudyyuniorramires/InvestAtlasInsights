using Application.Dtos.IndicadorPais;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.MacroIndicador
{
    public class MacroIndicadorDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Peso { get; set; }

        public bool MasAltoEsMejor { get; set; }

        public ICollection<IndicadorPaisDto>? IndicadoresPaises { get; set; }


    }
}
