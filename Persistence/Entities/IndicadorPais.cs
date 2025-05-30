using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class IndicadorPais
    {
        public  required int Id { get; set; }

        public int PaisId { get; set; }

        [ForeignKey("PaisId")]
        public virtual Pais Pais { get; set; }


        public int MacroIndicadorId { get; set; }

        [ForeignKey("MacroIndicadorId")]

        public virtual MacroIndicador MacroIndicadores { get; set; }

        public decimal Valor { get; set; }

        public int Anio { get; set; }
    }
}
