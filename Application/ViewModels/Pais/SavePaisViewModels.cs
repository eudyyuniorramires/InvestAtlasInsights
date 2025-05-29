using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Pais
{
    public class SavePaisViewModels
    {
        public  int Id { get; set; }

        [Required(ErrorMessage ="Debes ingresar el nombre del Pais")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar el Codigo ISO del pais")]

        public required string CodigoISO { get; set; }

        public ICollection<IndicadorPais>? IndicadoresPaises { get; set; }
    }
}

