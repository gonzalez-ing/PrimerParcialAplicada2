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

            if (!Page.IsPostBack)
            {
                LlenarCombos();
                int id = Utils.ToInt(Request.QueryString["id"]);
                if (id > 0)
                {
                    RepositorioBase<Depositos> repositorio = new RepositorioBase<Depositos>();
                    var cuenta = repositorio.Buscar(id);

                    if (cuenta == null)
                        Utils.ShowToastr(this, "No Existe en la Base de datos", "Error", "error");

                    else
                        LlenaCampos(cuenta);
                }
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

        private Depositos LlenaClase(Depositos depositos)
        {

            depositos.DepositoId = Utils.ToInt(depositoIdTextBox.Text);
            depositos.Fecha = Utils.ToDateTime(fechaTextBox.Text);
            depositos.CuentaId = Utils.ToInt(cuentaDropDownList.SelectedValue);
            depositos.Concepto = conceptoTextBox.Text;
            depositos.Monto = Utils.ToDecimal(montoTextBox.Text);

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
            BLL.RepositorioBase<Depositos> repositorio = new BLL.RepositorioBase<Depositos>();
            Depositos depositos = new Depositos();
            bool paso = false;

            LlenaClase(depositos);

            if (IsValid)
            {
                if (depositos.DepositoId == 0)
                {
                    if (paso = repositorio.Guardar(depositos))

                        Utils.ShowToastr(this, "saved successfully", "Success", "success");

                    else
                    {
                        Utils.ShowToastr(this, "Error Al Guardar", "Error", "error");
                    }
                }

                else
                {
                    if (paso = repositorio.Modificar(depositos))
                    {
                        Utils.ShowToastr(this, "saved successfully Modificar", "Success", "success");
                    }
                    else
                    {
                        Utils.ShowToastr(this, "Error Al Modificar", "Error", "error");

                    }
                }
            }
        }

        protected void eliminarButton_Click(object sender, EventArgs e)
        {
            BLL.RepositorioBase<Depositos> repositorio = new BLL.RepositorioBase<Depositos>();
            int id = Utils.ToInt(depositoIdTextBox.Text);

            var depositos = repositorio.Buscar(id);

            if (depositos == null)
                Utils.ShowToastr(this, "No Existe en la BD", "Error", "error");

            else
                repositorio.Eliminar(id);
            Utils.ShowToastr(this, "Eliminado Correctamente ", "Success", "success");
            Limpiar();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Depositos> repositorio = new RepositorioBase<Depositos>();
            Depositos depositos = repositorio.Buscar(Utils.ToInt(depositoIdTextBox.Text));

            if (depositos != null)
            {
                LlenaCampos(depositos);
            }
            else
            {

                Utils.ShowToastr(this, "No Existe en la BD", "Error", "error");
                Limpiar();
            }
        }
    }
}