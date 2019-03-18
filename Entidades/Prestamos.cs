using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaId { get; set; }
        public double Capital { get; set; }
        public double TasaInteres { get; set; }
        public int Tiempo { get; set; }
        public double Monto { get; set; }

        public virtual List<PrestamosDetalles> Detalle { get; set; }


        public Prestamos()
        {
            PrestamoId = 0;
            Fecha = DateTime.Now;
            CuentaId = 0;
            Capital = 0;
            TasaInteres = 0;
            Tiempo = 0;
            Monto = 0;
            Detalle = new List<PrestamosDetalles>();
        }

        public Prestamos(int prestamoId, DateTime fecha, int cuentaId, double capital, double tasaInteres, int tiempo, double monto, List<PrestamosDetalles> detalle)
        {
            PrestamoId = prestamoId;
            Fecha = fecha;
            CuentaId = cuentaId;
            Capital = capital;
            TasaInteres = tasaInteres;
            Tiempo = tiempo;
            Monto = monto;
            Detalle = detalle;
        }

        public Prestamos(int prestamoId, int cuentaId, double capital, double tasaInteres, int tiempo, double monto, List<PrestamosDetalles> detalle)
        {
            PrestamoId = prestamoId;
            CuentaId = cuentaId;
            Capital = capital;
            TasaInteres = tasaInteres;
            Tiempo = tiempo;
            Monto = monto;
            Detalle = detalle;
        }
    }
}
