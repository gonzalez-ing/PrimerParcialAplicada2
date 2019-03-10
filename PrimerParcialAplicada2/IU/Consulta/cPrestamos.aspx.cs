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
    public partial class cPrestamos : System.Web.UI.Page
    {
        Expression<Func<Prestamos, bool>> filtro = c => true;
        BLL.RepositorioBase<Prestamos> repositorios = new BLL.RepositorioBase<Prestamos>();
        List<Prestamos> prestamos = new List<Prestamos>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prestamos = repositorios.GetList(filtro);
                DesdeTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
                HastaTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
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
                    filtro = (c => c.PrestamoId == id);
                    break;

                case 2:
                    int.TryParse(CriterioTextBox.Text, out id);
                    filtro = c => c.PrestamoId == id && c.Fecha >= desde && c.Fecha <= hasta;
                    break;

                case 3:

                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = (c => c.CuentaId == id);
                    break;

                case 4:
                    decimal capital = Utils.ToInt(CriterioTextBox.Text);
                    filtro = (c => c.Capital == capital);
                    break;
            }
            CuentaGridView.DataSource = repositorios.GetList(filtro);
            CuentaGridView.DataBind();
        }
    }
}