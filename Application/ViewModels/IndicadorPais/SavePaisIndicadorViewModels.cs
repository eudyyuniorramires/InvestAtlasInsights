using Application.Dtos.IndicadorPais;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.IndicadorPais
{
    public class SaveIndicadorPaisViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "País")]
        public int PaisId { get; set; }

        [Required]
        [Display(Name = "Macroindicador")]
        public int MacroindicadorId { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Anio { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public List<SelectListItem>? Paises { get; set; }
        public List<SelectListItem>? Macroindicadores { get; set; }

        public ICollection<IndicadorPaisDto>? IndicadoresPaises { get; set; } 


        public bool IsEdit => Id > 0;
    }
}
