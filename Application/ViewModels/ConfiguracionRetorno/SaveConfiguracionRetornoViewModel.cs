﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ConfiguracionRetorno
{
    public class SaveConfiguracionRetornoViewModel
    {
        public  int Id { get; set; }

        public  required decimal TasaMinima { get; set; }

        public  required decimal TasaMaxima { get; set; }

    }
}
