using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class Pais
    {
        public int Id { get; set; } 

        public required string Nombre { get; set; } 

        public required string CodigoISO { get; set; }

        public ICollection<IndicadorPais>? IndicadoresPaises { get; set; } 
    }
}
