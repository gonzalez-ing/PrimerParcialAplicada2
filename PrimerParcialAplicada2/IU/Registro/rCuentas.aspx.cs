using BLL;
using Entidades;
using PrimerParcialAplicada2.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrimerParcialAplicada2.IU.Registro
{
    public partial class rCuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            balanceTextBox.Text = "0";

        }
        private Cuentas LlenaClase()
        {
            Cuentas cuenta = new Cuentas();

            cuenta.CuentaId = Utils.ToInt(cuentaIdTextBox.Text);
            cuenta.Fecha = Convert.ToDateTime(fechaTextBox.Text).Date;
            cuenta.Nombre = nombreTextBox.Text;
            cuenta.Balance = Utils.ToInt(balanceTextBox.Text);

            return cuenta;

        }
        private void Limpiar()
        {
            cuentaIdTextBox.Text = "0";
            fechaTextBox.Text = " ";
            nombreTextBox.Text = " ";
            balanceTextBox.Text = "0";
        }
        private void LlenaCampos(Cuentas cuentas)
        {
            cuentaIdTextBox.Text = cuentas.CuentaId.ToString();
            fechaTextBox.Text = cuentas.Fecha.ToString();
            nombreTextBox.Text = cuentas.Nombre.ToString();
            balanceTextBox.Text = cuentas.Balance.ToString();
        }


        protected void buscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Cuentas> repositorio = new RepositorioBase<Cuentas>();
            Cuentas cuentas = repositorio.Buscar(Utils.ToInt(cuentaIdTextBox.Text));

            if (cuentas != null)
            {
                LlenaCampos(cuentas);
            }
            else
            {
                Limpiar();
                Utils.ShowToastr(this, "No Se Encontro En La BD", "Error", "error");

            }
        }

        protected void guadarButton_Click(object sender, EventArgs e)
        {

        }

        protected void guadarButton_Click1(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Cuentas> repositorio = new BLL.RepositorioBase<Cuentas>();
            Cuentas cuenta = new Cuentas();
            bool paso = false;

            cuenta = LlenaClase();

            if (cuenta.CuentaId == 0)
            {
                paso = repositorio.Guardar(cuenta);
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                Limpiar();
            }
            else
            {
                int id = Utils.ToInt(cuentaIdTextBox.Text);
                BLL.RepositorioBase<Cuentas> repository = new BLL.RepositorioBase<Cuentas>();
                cuenta = repository.Buscar(id);

                if (cuenta != null)
                {
                    paso = repositorio.Modificar(LlenaClase());
                    Utils.ShowToastr(this, "Modificado Correctamente", "Exito", "success");
                }
                else
                    Utils.ShowToastr(this, "Id no existe", "Error", "error");
            }

            if (paso)
            {
                Limpiar();
            }
            else
                Utils.ShowToastr(this, "No se pudo guardar", "Error", "error");

        }

        protected void nuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void eliminarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Cuentas> repositorio = new BLL.RepositorioBase<Cuentas>();
            int id = Utils.ToInt(cuentaIdTextBox.Text);

            var cuentas = repositorio.Buscar(id);

            if (cuentas == null)
                Utils.ShowToastr(this, "No Se Pudo Elliminar ", "Error", "error");

            else
                repositorio.Eliminar(id);

            Utils.ShowToastr(this, " Eliminado Correctamente ", "Success", "success");

            Limpiar();
        }
    }
}