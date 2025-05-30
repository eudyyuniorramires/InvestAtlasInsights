using Application.Dtos.IndicadorPais;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.IndicadorPais
{
    public class IndicadorPaisViewModels
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Pais")]
        public int PaisId { get; set; }


        [Required]
        [Display(Name = "Macro Indicador")]
        public int MacroIndicadorId { get; set; }


        [Required]
        [Range(1900,2100)]
        public int Año { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public List<IndicadorPaisDto>? IndicadoresPaises { get; set; } 




    }
}
