using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MacroIndicador
{
    public class DeleteMacroIndicadorViewModels
    {

        public int Id { get; set; }


        public  string Nombre { get; set; }


        public  decimal Peso { get; set; }


        public  bool MasAltoEsMejor { get; set; }
    }
}
