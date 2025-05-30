﻿using Application.Dtos.IndicadorPais;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Dtos.Pais
{
    public class PaisDto
    {
        public int Id { get; set; }

        public  string Nombre { get; set; }

        public  string CodigoISO { get; set; }

        public ICollection<IndicadorPaisDto>? IndicadoresPaises { get; set; }
    }
}
