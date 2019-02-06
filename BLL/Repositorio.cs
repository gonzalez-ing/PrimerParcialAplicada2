using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Repositorio : RepositorioBase<Depositos>
    {
        public override bool Guardar(Depositos deposito)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.depositos.Add(deposito);
                contexto.cuentas.Find(deposito.CuentaId).Balance += deposito.Monto;
                contexto.SaveChanges();
                paso = true;

            }
            catch (Exception)
            {
                throw;
            }


            return paso;
        }

        public override bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Depositos deposito = contexto.depositos.Find(id);
                contexto.cuentas.Find(deposito.CuentaId).Balance -= deposito.Monto;
                contexto.depositos.Remove(deposito);
                contexto.SaveChanges();
                paso = true;
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        public static void CambiarBalances(Depositos deposito, Depositos depositoAnt)
        {
            RepositorioBase<Cuentas> repositorio = new RepositorioBase<Cuentas>();
            RepositorioBase<Cuentas> repository = new RepositorioBase<Cuentas>();
            Contexto contexto = new Contexto();
            var Cuenta = contexto.cuentas.Find(deposito.CuentaId);
            var CuentaAnt = contexto.cuentas.Find(depositoAnt.CuentaId);

            Cuenta.Balance += deposito.Monto;
            CuentaAnt.Balance -= depositoAnt.Monto;
            repositorio.Modificar(Cuenta);
            repository.Modificar(CuentaAnt);
        }

        public override bool Modificar(Depositos deposito)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                Depositos DepAnt = contexto.depositos.Find(deposito.DepositoId);

                var cuenta = contexto.cuentas.Find(deposito.CuentaId);

                if (deposito.CuentaId != DepAnt.CuentaId)
                {
                    CambiarBalances(deposito, DepAnt);
                }
                else
                {
                    int diferencia = deposito.Monto - DepAnt.Monto;
                    cuenta.Balance += diferencia;
                }
                contexto = new Contexto();
                contexto.Entry(deposito).State = EntityState.Modified;

                contexto.SaveChanges();
                paso = true;

            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

    }
}
