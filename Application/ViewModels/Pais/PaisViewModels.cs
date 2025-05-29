using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Pais
{
    public class PaisViewModels
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required string CodigoISO { get; set; }

        public ICollection<IndicadorPais>? IndicadoresPaises { get; set; }
    }
}
