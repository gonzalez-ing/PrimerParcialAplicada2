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
                int id = Utils.ToInt(Request.QueryString["id"]);
                if (id > 0)
                {
                    RepositorioBase<Depositos> repositorio = new RepositorioBase<Depositos>();
                    var cuentas = repositorio.Buscar(id);

                    if (cuenta == null)
                        Response.Write("<script>alert('Guardado Correctamente');</script>");
                    else
                        LlenaCampos(cuentas);
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
            depositos.CuentaId = Utils.ToInt(cuentaDropDownList.Text);
            depositos.Concepto = conceptoTextBox.Text;
            depositos.Monto = Utils.ToDecimal(montoTextBox.Text);

            return depositos;
        }

        protected void guadarButton_Click(object sender, EventArgs e)
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
                        Response.Write("<script>alert('Error al Guardar');</script>");
                    }
                }

                else
                {
                    if (paso = repositorio.Modificar(depositos))
                    {
                        Response.Write("<script>alert('Modificado Correctamente');</script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('Error al Modificar');</script>");
                    }
                }
            }
        }
    }
}