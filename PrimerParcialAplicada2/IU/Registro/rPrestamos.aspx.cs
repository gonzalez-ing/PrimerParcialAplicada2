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
    public partial class rPrestamos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LlenarCombos();
                int id = Utils.ToInt(Request.QueryString["id"]);
                FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (id > 0)
                {
                    //RepositorioBase<Prestamos> repositorio = new RepositorioBase<Prestamos>();
                    PrestamoRepositorio repositorio = new PrestamoRepositorio();
                    Prestamos prestamo = repositorio.Buscar(id);
                    LlenaCampos(prestamo);

                }
            }
        }

        private Prestamos LlenaClase()
        {
            Prestamos prestamo = new Prestamos();

            prestamo.PrestamoId = Utils.ToInt(PrestamoIdTextBox.Text);
            prestamo.Fecha = Utils.ToDateTime(FechaTextBox.Text);
            prestamo.CuentaId = Utils.ToInt(CuentaDropDownList.SelectedValue);
            prestamo.TasaInteres = Utils.ToInt(InteresTextBox.Text);
            prestamo.Capital = Utils.ToDecimal(CapitalTextBox.Text);
            prestamo.Tiempo = Utils.ToInt(TiempoTextBox.Text);
            prestamo.Detalle = (List<PrestamosDetalles>)ViewState["PrestamosDetalles"];

            return prestamo;
        }

        private void LlenaCampos(Prestamos prestamo)
        {
            PrestamoIdTextBox.Text = prestamo.PrestamoId.ToString();
            FechaTextBox.Text = prestamo.Fecha.ToString("yyyy-MM-dd");
            CuentaDropDownList.Text = prestamo.CuentaId.ToString();
            CapitalTextBox.Text = prestamo.Capital.ToString();
            InteresTextBox.Text = prestamo.TasaInteres.ToString();
            TiempoTextBox.Text = prestamo.Tiempo.ToString();
            this.BindGrid();
        }

        protected void BindGrid()
        {
            DetalleGridView.DataSource = ((Prestamos)ViewState["PrestamosDetalles"]).Detalle;
            DetalleGridView.DataBind();
        }

        private void Limpiar()
        {
            PrestamoIdTextBox.Text = "";
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            CuentaDropDownList.SelectedIndex = 0;
            CapitalTextBox.Text = "";
            InteresTextBox.Text = "";
            TiempoTextBox.Text = "";
            ViewState["PrestamosDetalles"] = null;
        }

        private void LlenarCombos()
        {
            RepositorioBase<Cuentas> repositorio = new RepositorioBase<Cuentas>();
            CuentaDropDownList.DataSource = repositorio.GetList(c => true);
            CuentaDropDownList.DataValueField = "CuentaId";
            CuentaDropDownList.DataTextField = "Nombre";
            CuentaDropDownList.DataBind();
            CuentaDropDownList.Items.Insert(0, new ListItem("", ""));
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == string.Empty)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            Prestamos prestamo = repositorio.Buscar(Utils.ToInt(PrestamoIdTextBox.Text));

            if (prestamo != null)
            {
                Limpiar();
                LlenaCampos(prestamo);
            }
            else
            {
                Limpiar();
            }
        }

        protected void CalcularButton_Click(object sender, EventArgs e)
        {
            int id = 0;
            PrestamoRepositorio repositorio = new PrestamoRepositorio();

            if (PrestamoIdTextBox.Text == string.Empty)
                ViewState["PrestamosDetalles"] = repositorio.CalcularCuotas(Utils.ToInt(TiempoTextBox.Text), Utils.ToDouble(CapitalTextBox.Text), (Utils.ToDouble(InteresTextBox.Text)) / 100 / 12);
            else
                ViewState["PrestamosDetalles"] = repositorio.CalcularCuotasModificadas((List<PrestamosDetalles>)ViewState["PrestamosDetalles"], id, Utils.ToInt(TiempoTextBox.Text), Utils.ToDouble(CapitalTextBox.Text), (Utils.ToDouble(InteresTextBox.Text) / 100 / 12));

            DetalleGridView.DataSource = ViewState["PrestamosDetalles"];
            DetalleGridView.DataBind();
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            Prestamos prestamo = repositorio.Buscar(Utils.ToInt(PrestamoIdTextBox.Text));

            if (prestamo == null)
            {
                if (repositorio.Guardar(LlenaClase()))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Popup", "alert('Guardado Correctamente')", true);

                    Limpiar();
                }
                else
                {
                    Limpiar();
                }
            }
            else
            {
                if (repositorio.Modificar(LlenaClase()))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Popup", "alert('Modificado Correctamente')", true);
                    Limpiar();
                }
                else
                {
                    Limpiar();
                }
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            Prestamos prestamo = repositorio.Buscar(Utils.ToInt(PrestamoIdTextBox.Text));

            if (prestamo != null)
            {
                repositorio.Eliminar(prestamo.PrestamoId);
                Limpiar();
            }
            else
            {

                Limpiar();
            }
        }
    }
}