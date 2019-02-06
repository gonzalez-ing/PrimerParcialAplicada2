﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cuentas
    {
        [Key]
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public int Balance { get; set; }

        public Cuentas()
        {
            CuentaId = 0;
            Fecha = DateTime.Now;
            Nombre = string.Empty;
            Balance = 0;
        }
    }
}
