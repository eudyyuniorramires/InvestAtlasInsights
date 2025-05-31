using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ConfiguracionRetorno
{
    public class ConfiguracionRetornoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La tasa mínima es requerida.")]
        public decimal TasaMinima { get; set; }

        [Required(ErrorMessage = "La tasa máxima es requerida.")]
        public decimal TasaMaxima { get; set; } 
    }
}
