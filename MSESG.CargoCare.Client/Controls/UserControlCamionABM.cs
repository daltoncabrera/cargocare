using ComponentFactory.Krypton.Toolkit;
using DCM.Core;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public partial class UserControlCamionABM : UserControlCareCargoBase
  {
    private CamionWraper objCamionWraper;
    private ClienteWraper objClienteWraper;
    private camion currentCamion;
    private cargocareEntities dbEnt;
    private cliente currentCliente;

    public UserControlCamionABM(List<SearchResult> objSearch = null)
    {
      this.InitializeComponent();
      this.ObjSearch = objSearch;
      this.dbEnt = new cargocareEntities();
      this.objClienteWraper = new ClienteWraper();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void kryptonButtonCancel_Click(object sender, EventArgs e)
    {
      this.ParentForm.Close();
    }

    private void kryptonButtonAccept_Click(object sender, EventArgs e)
    {
      try
      {
        this.save();
        GUtils.ShowMessage("Registro guardado");
        this.ParentForm.Close();
        this.userControlSingleSearchCliente.Enabled = false;
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Erro guardando registro", ex);
      }
    }

    private void save()
    {
      this.currentCamion = this.objCamionWraper.Save(this.currentCamion != null ? new int?(this.currentCamion.IdCamion) : new int?(), this.kryptonTextBoxMarca.Text, this.kryptonTextBoxModelo.Text, this.kryptonTextBoxPlaca.Text, this.kryptonTextBoxFicha.Text, this.kryptonNumericUpDownCompartimentos.Value.ToInt(), this.kryptonNumericUpDownCapacidad.Value, this.kryptonNumericUpDownEntradas.Value.ToInt(), this.kryptonNumericUpDownSalidas.Value.ToInt(), this.kryptonTextBoxComentario.Text, this.currentCliente.IdCliente);
    }

    private void prepareControls()
    {
      this.objCamionWraper = new CamionWraper();
      this.currentCamion = this.objCamionWraper.GetCamion(this.ObjSearch);
      this.userControlSingleSearchCliente.SetEntity<cliente>((IQueryable<cliente>) this.dbEnt.clientes, cliente.Meta.Columns.NombreCliente, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchCliente.SetMantenimiento((UserControlBase) new UserControlClienteABM((List<SearchResult>) null), "Cliente");
      this.userControlSingleSearchCliente.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchCliente_OnValueChanged);
      if (this.currentCamion != null && this.currentCamion.IdCliente.HasValue)
      {
        this.userControlSingleSearchCliente.SetValues(new List<SearchResult>()
        {
          new SearchResult(cliente.Meta.Columns.IdCliente, (object) this.currentCamion.IdCliente)
        });
        this.userControlSingleSearchCliente.Enabled = false;
      }
      this.fillForm();
    }

    private void userControlSingleSearchCliente_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.kryptonPanelInner.Enabled = resultList.Count > 0;
      this.currentCliente = this.objClienteWraper.GetCliente(resultList);
    }

    private void fillForm()
    {
      if (this.currentCamion == null)
        return;
      this.kryptonTextBoxMarca.Text = this.currentCamion.Marca;
      this.kryptonTextBoxModelo.Text = this.currentCamion.Modelo;
      this.kryptonTextBoxPlaca.Text = this.currentCamion.Placa;
      this.kryptonTextBoxFicha.Text = this.currentCamion.Ficha;
      this.kryptonNumericUpDownCompartimentos.Value = (Decimal) this.currentCamion.Compartimentos.Value;
      this.kryptonNumericUpDownCapacidad.Value = this.currentCamion.Capacidad.Value;
      this.kryptonNumericUpDownEntradas.Value = (Decimal) this.currentCamion.Entradas.Value;
      this.kryptonNumericUpDownSalidas.Value = (Decimal) this.currentCamion.Salidas.Value;
      this.kryptonTextBoxComentario.Text = this.currentCamion.Comentario;
    }

    private void UserControlCamionABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }
  }
}
