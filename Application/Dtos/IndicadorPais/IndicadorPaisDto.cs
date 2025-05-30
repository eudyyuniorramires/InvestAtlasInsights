using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.IndicadorPais
{
    public class IndicadorPaisDto
    {
        public int Id { get; set; }
        public int PaisId { get; set; }
        public string PaisNombre { get; set; } // Para mostrar en vistas
        public int MacroIndicadorId { get; set; }
        public string MacroIndicadorNombre { get; set; } // Para mostrar en vistas
        public int Anio { get; set; }
        public decimal Valor { get; set; }
    }
}
