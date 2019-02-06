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
    public partial class rDepositos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Cuentas cuenta = new Cuentas();

            if (!Page.IsPostBack)
            {
                LlenarCombos();
                ViewState["Deposito"] = new Depositos();
            }
        }

        private void LlenaCampos(Depositos depositos)
        {
            depositoIdTextBox.Text = depositos.DepositoId.ToString();
            fechaTextBox.Text = depositos.Fecha.ToString();
            cuentaDropDownList.Text = depositos.CuentaId.ToString();
            conceptoTextBox.Text = depositos.Concepto;
            montoTextBox.Text = depositos.Monto.ToString();
        }

        void LlenarCombos()
        {
            RepositorioBase<Cuentas> repositorio = new RepositorioBase<Cuentas>();
            cuentaDropDownList.DataSource = repositorio.GetList(c => true);
            cuentaDropDownList.DataValueField = "CuentaId";
            cuentaDropDownList.DataTextField = "Nombre";
            cuentaDropDownList.DataBind();
            cuentaDropDownList.Items.Insert(0, new ListItem("", ""));
        }

        private Depositos LlenaClase()
        {
            Depositos depositos = new Depositos();

            depositos.DepositoId = Utils.ToInt(depositoIdTextBox.Text);
            depositos.Fecha = Utils.ToDateTime(fechaTextBox.Text);
            depositos.CuentaId = Utils.ToInt(cuentaDropDownList.SelectedValue);
            depositos.Concepto = conceptoTextBox.Text;
            depositos.Monto = Utils.ToInt(montoTextBox.Text);

            return depositos;
        }

        private void Limpiar()
        {
            depositoIdTextBox.Text = "";
            fechaTextBox.Text = "";
            cuentaDropDownList.SelectedIndex = 0;
            conceptoTextBox.Text = "";
            montoTextBox.Text = "";

        }

        protected void guadarButton_Click(object sender, EventArgs e)
        {

        }

        protected void nuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void guadarButton_Click1(object sender, EventArgs e)
        {
            bool paso = false;
            Repositorio repositorio = new Repositorio();
            Depositos deposito = new Depositos();

            deposito = LlenaClase();

            if (deposito.DepositoId == 0)
            {
                paso = repositorio.Guardar(deposito);
                Utils.ShowToastr(this, "Guardado", "Exito", "success");
                Limpiar();
            }
            else
            {
                Repositorio repository = new Repositorio();
                int id = Utils.ToInt(depositoIdTextBox.Text);
                deposito = repository.Buscar(id);

                if (deposito != null)
                {
                    paso = repository.Modificar(LlenaClase());
                    Utils.ShowToastr(this, "Modificado", "Exito", "success");
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

        protected void eliminarButton_Click(object sender, EventArgs e)
        {
            Repositorio repositorio = new Repositorio();
            int id = Utils.ToInt(depositoIdTextBox.Text);

            var deposito = repositorio.Buscar(id);

            if (deposito != null)
            {
                if (repositorio.Eliminar(id))
                {
                    Utils.ShowToastr(this, "Eliminado", "Exito", "success");
                    Limpiar();
                }
                else
                    Utils.ShowToastr(this, "No se pudo eliminar", "Error", "error");
            }
            else
                Utils.ShowToastr(this, "No existe en la BD", "Error", "error");
        }
    

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Repositorio repositorio = new Repositorio();

            var deposito = repositorio.Buscar(Utilidades.Utils.ToInt(depositoIdTextBox.Text));
            if (deposito != null)
            {
                LlenaCampos(deposito);
                Utils.ShowToastr(this, "Busqueda Correcta", "Exito", "success");
            }
            else
            {
                Limpiar();
                Utils.ShowToastr(this, "No Hay Resultado", "Error", "error");
            }
        }
    }
}