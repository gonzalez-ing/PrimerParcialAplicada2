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
    public partial class cDepositos : System.Web.UI.Page
    {
        Expression<Func<Depositos, bool>> filtro = c => true;
        BLL.RepositorioBase<Depositos> repositorios = new BLL.RepositorioBase<Depositos>();
        List<Depositos> depositos = new List<Depositos>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                depositos = repositorios.GetList(filtro);
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
                    filtro = (c => c.DepositoId == id);
                    break;

                case 2:
                    int.TryParse(CriterioTextBox.Text, out id);
                    filtro = c => c.CuentaId == id && c.Fecha >= desde && c.Fecha <= hasta;
                    break;

                case 3:

                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = (c => c.CuentaId == id);
                    break;

                case 4:
                    decimal monto = Utils.ToInt(CriterioTextBox.Text);
                    filtro = (c => c.Monto == monto);
                    break;

            }

            CuentaGridView.DataSource = repositorios.GetList(filtro);
            CuentaGridView.DataBind();
        }
    }
}