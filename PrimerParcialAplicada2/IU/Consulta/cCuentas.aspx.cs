using Entidades;
using PrimerParcialAplicada2.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrimerParcialAplicada2.IU.Consulta
{
    public partial class cCuentas : System.Web.UI.Page
    {

        Expression<Func<Cuentas, bool>> filtro = c => true;
        BLL.RepositorioBase<Cuentas> repositorio = new BLL.RepositorioBase<Cuentas>();
        List<Cuentas> cuentas = new List<Cuentas>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cuentas = repositorio.GetList(filtro);
                DesdeTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
                HastaTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void buscarButton_Click(object sender, EventArgs e)
        {
            DateTime desde = Utils.ToDateTime(DesdeTextBox.Text);
            DateTime hasta = Utils.ToDateTime(HastaTextBox.Text);

            int id;
            switch (FiltroDropDownList.SelectedIndex)
            {
                case 0:
                    filtro = c => true;
                    break;

                case 1:

                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = (c => c.CuentaId == id);
                    break;

                case 2:
                    int.TryParse(CriterioTextBox.Text, out id);
                    filtro = c => c.CuentaId == id && c.Fecha >= desde && c.Fecha <= hasta;
                    break;

                case 3:
                    filtro = (c => c.Nombre.Contains(CriterioTextBox.Text) && c.Fecha >= desde && c.Fecha <= hasta);
                    break;

                case 4:
                    decimal balance = Utils.ToDecimal(CriterioTextBox.Text);
                    filtro = (c => c.Balance == balance);
                    break;

            }

            CuentaGridView.DataSource = repositorio.GetList(filtro);
            CuentaGridView.DataBind();
        }
    }
}