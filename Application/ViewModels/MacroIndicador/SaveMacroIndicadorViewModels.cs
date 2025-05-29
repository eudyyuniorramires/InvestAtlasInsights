using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MacroIndicador
{
    public class SaveMacroIndicadorViewModels
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del Macro Indicador ")]

        public  required string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el peso del Macro Indicador ")]

        public required decimal Peso { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una condicion")]


        public required bool MasAltoEsMejor { get; set; }

        public ICollection<IndicadorPais>? IndicadoresPaises { get; set; }
    }
}
