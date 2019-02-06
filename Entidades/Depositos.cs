using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Depositos
    {
        [Key]
        public int DepositoId { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaId { get; set; }
        public string Concepto { get; set; }
        public int Monto { get; set; }

        public Depositos()
        {
            DepositoId = 0;
            Fecha = DateTime.Now;
            CuentaId = 0;
            Concepto = string.Empty;
            Monto = 0;
        }
    }
}
