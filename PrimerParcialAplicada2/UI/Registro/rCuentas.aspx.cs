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
        private Cuentas LlenaClase(Cuentas cuenta)
        {


            cuenta.CuentaId = Utils.ToInt(cuentaIdTextBox.Text);
            cuenta.Fecha = Convert.ToDateTime(fechaTextBox.Text).Date;
            cuenta.Nombre = nombreTextBox.Text;
            cuenta.Balance = Utils.ToInt(balanceTextBox.Text);

            return cuenta;

        }
        private void Limpiar()
        {
            cuentaIdTextBox.Text = "0";
            fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                // Limpiar();
                Utils.ShowToastr(this, "No Se Encontro", "Error", "error");

            }
        }

        protected void guadarButton_Click(object sender, EventArgs e)
        {

        }

        protected void guadarButton_Click1(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Cuentas> repositorio = new BLL.RepositorioBase<Cuentas>();
            Cuentas cuentas = new Cuentas();
            bool paso = false;

            LlenaClase(cuentas);

            if (IsValid)
            {
                if (cuentas.CuentaId == 0)
                {
                    if (paso = repositorio.Guardar(cuentas))

                        Utils.ShowToastr(this, "Guardado", "Success", "success");

                    else
                    {
                        Response.Write("<script>alert('Error al Guardar');</script>");
                    }
                    Limpiar();
                }

                else
                {
                    if (paso = repositorio.Modificar(cuentas))
                    {
                        Response.Write("<script>alert('Modificado Correctamente');</script>");
                        Limpiar();
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al Modificar');</script>");
                    }
                }
            }

        }

        protected void nuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void eliminarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Cuentas> repositorio = new BLL.RepositorioBase<Cuentas>();
            int id = Utils.ToInt(cuentaIdTextBox.Text);

            var cuenta = repositorio.Buscar(id);

            if (cuenta != null)
            {
                if (repositorio.Eliminar(id))
                {
                    Response.Write("<script>alert('Eliminado');</script>");
                    Limpiar();
                }
                else
                    Response.Write("<script>alert('No se pudo eliminar');</script>");
            }
            else
                Response.Write("<script>alert('No existe');</script>");
        }
    }
}