using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class PrestamosDetalles
    {
        [Key]
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int NoCuota { get; set; }
        public double Interes { get; set; }
        public double Capital { get; set; }
        public double ValorPrestamo { get; set; }
        public double Balance { get; set; }

        public PrestamosDetalles()
        {
            Id = 0;
            NoCuota = 0;
            Interes = 0;
            Capital = 0;
            ValorPrestamo = 0;
            Balance = 0;
        }

        public PrestamosDetalles(int id, int prestamoId, int noCuota, double interes, double capital, double valorPrestamo, double balance)
        {
            Id = id;
            PrestamoId = prestamoId;
            NoCuota = noCuota;
            Interes = interes;
            Capital = capital;
            ValorPrestamo = valorPrestamo;
            Balance = balance;
        }
    }
}
