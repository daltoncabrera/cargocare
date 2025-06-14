using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using DCM.Core;
using DCM.Core.Data;
using DCM.Core.UI;
using MSESG.CargoCare.Client.Reports;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public class UserControlRecepcion : UserControlCareCargoBase
  {
    private const string VALIDAR_SELLOS_SETTINGS_KEY = "validarSellosBlancos";
    private const int STATUS_COMPLETE = 3;
    private cargocareEntities dbEnt;
    private RecepcionWraper objRecepcionWraper;
    private inspeccion currentDespacho;
    private camion currentCamion;
    private chofer currentChofer;
    private cliente currentCliente;
    private terminal currentTerminal;
    private ClienteWraper objClienteWraper;
    private CamionWraper objCamionWraper;
    private ChoferWraper objChoferWraper;
    private TerminalWraper objTerminalWraper;
    private List<DetalleCargaObject> currentDetaSelloCarga;
    private List<VerificacionTabObject> currentVeriCarga;
    private List<ProductMedidaTabObject> currentMediaCarga;
    private IQueryable<producto> listaProductos;
    private IQueryable<sello> listaSellos;
    private DataGridViewComboBoxColumn kcomboSelloChapa;
    private DataGridViewComboBoxColumn kcomboSelloBocaCarga;
    private DataGridViewComboBoxColumn kcomboselloBocaDescarga;
    private DataGridViewComboBoxColumn kcomboProducto;
    private DataGridViewComboBoxColumn kcomboProductoMedida;
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonPanel kryptonPanel2;
    private KryptonDockableNavigator kryptonDockableNavigatorTab;
    private KryptonPage kryptonPageDeltalleCarga;
    private KryptonPage kryptonPageMedidaCarga;
    private KryptonPage kryptonNavigatorVerificacion;
    private KryptonPanel kryptonPanel3;
    private KryptonPanel kryptonPanelClienteTerminal;
    private UserControlSingleSearch userControlSingleSearchCliente;
    private KryptonLabel kryptonLabel1;
    private UserControlSingleSearch userControlSingleSearchChofer;
    private UserControlSingleSearch userControlSingleSearchCamion;
    private KryptonLabel kryptonLabel3;
    private UserControlSingleSearch userControlSingleSearchTerminal;
    private KryptonPanel kryptonPanelComentario;
    private KryptonLabel kryptonLabel5;
    private KryptonTextBox kryptonTextBoxComentario;
    private KryptonLabel kryptonLabel4;
    private KryptonLabel kryptonLabel2;
    private KryptonDateTimePicker kryptonDateTimePickerEndTime;
    private KryptonDateTimePicker kryptonDateTimePickerInitTime;
    private KryptonGroupBox kryptonGroupBoxChofer;
    private KryptonGroupBox kryptonGroupBoxCamion;
    private KryptonButton kryptonButtonCancelar;
    private KryptonButton kryptonButtonGuardar;
    private ToolStrip toolStrip1;
    private ToolStripButton toolStripButtonPrint;
    private KryptonDataGridView kryptonDataGridViewDetalleSellos;
    private KryptonDataGridView kryptonDataGridViewVeri;
    private KryptonComboBox kryptonComboBoxMedia;
    private KryptonLabel kryptonLabel6;
    private KryptonDataGridView kryptonDataGridViewMedida;
    private KryptonLabel kryptonLabel9;
    private KryptonLabel kryptonLabel10;
    private KryptonLabel kryptonLabel8;
    private KryptonLabel kryptonLabel7;
    private KryptonLabel kryptonLabelCelular;
    private KryptonLabel kryptonLabelLicencia;
    private KryptonLabel kryptonLabelCedula;
    private KryptonLabel kryptonLabel11;
    private KryptonLabel kryptonLabelPlaca;
    private KryptonLabel kryptonLabelFicha;
    private DataGridViewTextBoxColumn cItem;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewCheckBoxColumn dataGridViewTextBoxColumn2;
    private KryptonDataGridViewCheckBoxColumn noColumn;
    private KryptonDataGridViewCheckBoxColumn naColumn;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private ToolStripButton toolStripButtonVerifi;
    private ToolStripButton toolStripButtonMedidas;
    private DataGridViewTextBoxColumn columnCompartimentos;
    private DataGridViewComboBoxColumn columnProducto;
    private KryptonDataGridViewNumericUpDownColumn columnCantidad;
    private DataGridViewComboBoxColumn columnChapa;
    private DataGridViewComboBoxColumn columnCarga;
    private DataGridViewComboBoxColumn columnDescarga;
    private KryptonTextBox kryptonTextBoxReferencia;
    private KryptonLabel kryptonLabel12;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewComboBoxColumn kryptonDataGridViewComboBoxColumnproducto1;
    private KryptonDataGridViewTextBoxColumn kryptonDataGridViewDomainUpDownColumn2;
    private KryptonDataGridViewNumericUpDownColumn kryptonDataGridViewDomainUpDownColumn1;
    private KryptonDataGridViewNumericUpDownColumn kryptonDataGridViewComboBoxColumn2;
    private KryptonDataGridViewNumericUpDownColumn Diferencia;

    public UserControlRecepcion(List<SearchResult> objSearch = null)
    {
      this.InitializeComponent();
      this.ObjSearch = objSearch;
      this.dbEnt = new cargocareEntities();
      this.objRecepcionWraper = new RecepcionWraper();
      this.objCamionWraper = new CamionWraper();
      this.objChoferWraper = new ChoferWraper();
      this.objClienteWraper = new ClienteWraper();
      this.objTerminalWraper = new TerminalWraper();
    }

    private void UserControlDespacho_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void prepareControls()
    {
      this.kryptonGroupBoxCamion.Text = "Camion";
      this.kryptonGroupBoxChofer.Text = "Chofer";
      this.userControlSingleSearchCliente.SetEntity<cliente>(this.dbEnt.clientes.Where<cliente>((Expression<Func<cliente, bool>>) (s => s.IdStatus != (int?) -1)), cliente.Meta.Columns.NombreCliente, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchCliente.SetMantenimiento((UserControlBase) new UserControlClienteABM((List<SearchResult>) null), "Cliente");
      this.userControlSingleSearchCliente.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchCliente_OnValueChanged);
      this.userControlSingleSearchTerminal.SetEntity<terminal>(this.dbEnt.terminals.Where<terminal>((Expression<Func<terminal, bool>>) (s => s.IdStatus != (int?) -1)), terminal.Meta.Columns.NombreTerminal, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchTerminal.SetMantenimiento((UserControlBase) new UserControlTerminalABM((List<SearchResult>) null), "Terminal");
      this.userControlSingleSearchTerminal.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchTerminal_OnValueChanged);
      this.prepareGrids();
      if (this.ObjSearch != null && this.ObjSearch.Count > 0)
        this.currentDespacho = this.objRecepcionWraper.GetDespachoById(new int?(this.getAsInt(this.ObjSearch, true)));
      this.kryptonComboBoxMedia.DataSource = (object) this.objRecepcionWraper.GetUnits();
      this.kryptonComboBoxMedia.DisplayMember = "IdUnidadmedida";
      this.kryptonComboBoxMedia.ValueMember = "IdUnidadmedida";
      this.fillForm();
    }

    private void userControlSingleSearchTerminal_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.currentTerminal = this.objTerminalWraper.GetTerminal(resultList);
    }

    private void userControlSingleSearchCliente_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.userControlSingleSearchCamion.Enabled = this.userControlSingleSearchChofer.Enabled = resultList.Count > 0;
      this.currentCamion = (camion) null;
      this.userControlSingleSearchCamion.Clean(true);
      this.userControlSingleSearchChofer.Clean(true);
      this.prepareGrids();
      this.currentCliente = this.objClienteWraper.GetClienteById(new int?(this.getAsInt(resultList, false)));
      this.listaSellos = this.objRecepcionWraper.GetSellosNamedValue(this.currentDespacho, this.currentCliente);
      this.kcomboSelloBocaCarga.DataSource = (object) this.listaSellos;
      this.kcomboselloBocaDescarga.DataSource = (object) this.listaSellos;
      this.kcomboSelloChapa.DataSource = (object) this.listaSellos;
      this.kcomboSelloBocaCarga.ValueMember = "CodSello";
      this.kcomboselloBocaDescarga.ValueMember = this.kcomboSelloChapa.ValueMember = "CodSello";
      this.kcomboSelloBocaCarga.DisplayMember = "CodSello";
      this.kcomboselloBocaDescarga.DisplayMember = this.kcomboSelloChapa.DisplayMember = "CodSello";
      this.prepareOtherSingleSearch();
    }

    private void prepareOtherSingleSearch()
    {
      int idDummyCliente = -50;
      idDummyCliente = this.currentCliente != null ? this.currentCliente.IdCliente : idDummyCliente;
      this.userControlSingleSearchCamion.SetEntity<camion>(this.dbEnt.camions.Where<camion>((Expression<Func<camion, bool>>) (s => s.IdStatus != (int?) -1 && s.IdCliente == (int?) idDummyCliente)), camion.Meta.Columns.Marca, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchCamion.SetMantenimiento((UserControlBase) new UserControlCamionABM((List<SearchResult>) null), "Camion");
      this.userControlSingleSearchCamion.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchCamion_OnValueChanged);
      this.userControlSingleSearchChofer.SetEntity<chofer>(this.dbEnt.chofers.Where<chofer>((Expression<Func<chofer, bool>>) (s => s.IdStatus != (int?) -1 && s.IdCliente == (int?) idDummyCliente)), chofer.Meta.Columns.Nombre, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchChofer.SetMantenimiento((UserControlBase) new UserControlChoferABM((List<SearchResult>) null), "Chofer");
      this.userControlSingleSearchChofer.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchChofer_OnValueChanged);
      if (this.currentDespacho == null)
        return;
      List<SearchResult> selectedResult1 = new List<SearchResult>()
      {
        new SearchResult(inspeccion.Meta.Columns.IdCamion, (object) this.currentDespacho.IdCamion)
      };
      List<SearchResult> selectedResult2 = new List<SearchResult>()
      {
        new SearchResult(inspeccion.Meta.Columns.IdChofer, (object) this.currentDespacho.IdChofer)
      };
      this.userControlSingleSearchCamion.SetValues(selectedResult1);
      this.userControlSingleSearchChofer.SetValues(selectedResult2);
    }

    private void userControlSingleSearchChofer_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.currentChofer = this.objChoferWraper.GetChofer(resultList);
      if (this.currentChofer == null)
        return;
      this.kryptonLabelCedula.Text = this.currentChofer.Cedula;
      this.kryptonLabelCelular.Text = this.currentChofer.Telefono;
      this.kryptonLabelLicencia.Text = this.currentChofer.Licencia;
    }

    private void fillForm()
    {
      this.objRecepcionWraper = new RecepcionWraper();
      if (this.currentDespacho == null)
        return;
      List<SearchResult> selectedResult = new List<SearchResult>()
      {
        new SearchResult(inspeccion.Meta.Columns.IdCliente, (object) this.currentDespacho.IdCliente)
      };
      this.userControlSingleSearchTerminal.SetValues(new List<SearchResult>()
      {
        new SearchResult(inspeccion.Meta.Columns.IdTerminal, (object) this.currentDespacho.IdTerminal)
      });
      this.userControlSingleSearchCliente.SetValues(selectedResult);
      this.kryptonTextBoxComentario.Text = this.currentDespacho.Comentario;
      if (this.currentDespacho.FechaInicio.HasValue)
        this.kryptonDateTimePickerInitTime.Value = this.currentDespacho.FechaInicio.Value;
      if (this.currentDespacho.FechaFin.HasValue)
        this.kryptonDateTimePickerEndTime.Value = this.currentDespacho.FechaFin.Value;
      this.kryptonComboBoxMedia.SelectedValue = (object) this.currentDespacho.IdUnidad;
      this.kryptonTextBoxReferencia.Text = this.currentDespacho.Referencia;
      this.disableControls();
    }

    private void prepareGrids()
    {
      DataGridViewComboBoxColumn column = this.kryptonDataGridViewDetalleSellos.Columns[1] as DataGridViewComboBoxColumn;
      this.kcomboSelloChapa = this.kryptonDataGridViewDetalleSellos.Columns[3] as DataGridViewComboBoxColumn;
      this.kcomboSelloBocaCarga = this.kryptonDataGridViewDetalleSellos.Columns[4] as DataGridViewComboBoxColumn;
      this.kcomboselloBocaDescarga = this.kryptonDataGridViewDetalleSellos.Columns[5] as DataGridViewComboBoxColumn;
      this.kcomboProductoMedida = this.kryptonDataGridViewMedida.Columns[1] as DataGridViewComboBoxColumn;
      this.listaProductos = this.objRecepcionWraper.GetProductNamedValue();
      column.DataSource = (object) this.listaProductos;
      this.kcomboProductoMedida.DataSource = (object) this.listaProductos;
      column.ValueMember = "IdProducto";
      this.kcomboProductoMedida.ValueMember = "IdProducto";
      column.DisplayMember = "ProductName";
      this.kcomboProductoMedida.DisplayMember = "ProductName";
    }

    private void userControlSingleSearchCamion_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.currentCamion = this.objCamionWraper.GetCamionById(new int?(this.getAsInt(resultList, false)));
      if (this.currentCamion != null)
      {
        this.kryptonLabelPlaca.Text = this.currentCamion.Placa;
        this.kryptonLabelFicha.Text = this.currentCamion.Ficha;
      }
      this.fillDetails();
    }

    private void kryptonButtonGuardar_Click(object sender, EventArgs e)
    {
      try
      {
        this.saveHeader();
        this.saveDetails();
        this.fillForm();
        this.fillDetails();
        GUtils.ShowMessage("Datos guardados.");
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error guardando  datos", ex);
      }
    }

    private void fillDetails()
    {
      this.currentDetaSelloCarga = this.objRecepcionWraper.GetDetalleObject(this.currentDespacho, this.currentCamion, this.currentCliente);
      this.currentMediaCarga = this.objRecepcionWraper.GetProductDetalleObject(this.currentDespacho, this.currentCamion);
      this.currentVeriCarga = this.objRecepcionWraper.GetVerificacionObject(this.currentDespacho);
      this.kryptonDataGridViewDetalleSellos.DataSource = (object) this.currentDetaSelloCarga;
      this.kryptonDataGridViewVeri.DataSource = (object) this.currentVeriCarga;
      this.kryptonDataGridViewMedida.DataSource = (object) this.currentMediaCarga;
    }

    private void saveDetails()
    {
      if (!this.valideteGridSellos())
        throw new Exception("Revisar que no se uso el mismo sello dos veces");
      this.objRecepcionWraper.SaveDetalleSellos(this.currentDetaSelloCarga, this.currentDespacho);
      this.objRecepcionWraper.SaveDetalleMedida(this.currentMediaCarga, this.currentDespacho);
      this.objRecepcionWraper.SaveDetalleVerificacion(this.currentVeriCarga, this.currentDespacho);
    }

    private void saveHeader()
    {
      int? idDespacho = this.currentDespacho != null ? new int?(this.currentDespacho.IdInspeccion) : new int?();
      int asInt1 = this.getAsInt(this.userControlSingleSearchTerminal.GetKeyValues(), true);
      int asInt2 = this.getAsInt(this.userControlSingleSearchCamion.GetKeyValues(), true);
      int asInt3 = this.getAsInt(this.userControlSingleSearchCliente.GetKeyValues(), true);
      int asInt4 = this.getAsInt(this.userControlSingleSearchChofer.GetKeyValues(), true);
      object selectedValue = this.kryptonComboBoxMedia.SelectedValue;
      string idUnidad = selectedValue != null ? selectedValue.ToString() : (string) null;
      DateTime? horaInicio = this.kryptonDateTimePickerInitTime.Checked ? new DateTime?(this.kryptonDateTimePickerInitTime.Value) : new DateTime?();
      DateTime? horaFin = this.kryptonDateTimePickerEndTime.Checked ? new DateTime?(this.kryptonDateTimePickerEndTime.Value) : new DateTime?();
      this.currentDespacho = this.objRecepcionWraper.Save(idDespacho, Global.CurrentUserID, asInt4, asInt2, asInt3, asInt1, idUnidad, horaInicio, horaFin, this.kryptonTextBoxComentario.Text, this.kryptonTextBoxReferencia.Text) ?? this.currentDespacho;
    }

    private int getAsInt(List<SearchResult> list, bool fireExcepction = true)
    {
      int result = 0;
      if ((list.Count <= 0 || !int.TryParse(list[0].Value.ToString(), out result)) && fireExcepction)
        throw new Exception("Asegurese de llenar el Cliente, Chofer, Camion y Terminal");
      return result;
    }

    private void toolStripButtonPrint_Click(object sender, EventArgs e)
    {
      if (!this.ValidateComplete())
      {
        GUtils.ShowMessage("No valido, favor revisar conduce");
      }
      else
      {
        string reportPath = "ConduceDespachoSellos.rdlc";
        dsDataCareCargo dsDataCareCargo = new dsDataCareCargo();
        PrintReport printReport = new PrintReport(reportPath);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspeccionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadDetalleInspeccionByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.detalleinspeccionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionVerificationByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspeccionverificacionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionTotalProductByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspecciontotalproductosview.TableName);
        int num = (int) printReport.ShowDialog();
      }
    }

    private bool valideteGridSellos()
    {
      List<DataGridViewCell> gridSellos = this.getGridSellos();
      IEnumerable<object> CellsValues = gridSellos.Select<DataGridViewCell, object>((Func<DataGridViewCell, object>) (s => s.Value));
      IEnumerable<DataGridViewCell> source = gridSellos.Where<DataGridViewCell>((Func<DataGridViewCell, bool>) (c =>
      {
        if (CellsValues.Where<object>((Func<object, bool>) (s =>
        {
          if (s != null)
            return s.Equals(c.Value);
          return false;
        })).DefaultIfEmpty<object>().Count<object>() > 1 && c.Value != null && !c.Value.Equals((object) ""))
          return !c.Value.Equals((object) "N/A");
        return false;
      }));
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      gridViewCellStyle.BackColor = Color.Red;
      gridViewCellStyle.SelectionBackColor = Color.Red;
      foreach (DataGridViewCell dataGridViewCell in source)
        this.kryptonDataGridViewDetalleSellos[dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex].Style = gridViewCellStyle;
      return source.Count<DataGridViewCell>() <= 0;
    }

    private List<DataGridViewCell> getGridSellos()
    {
      List<DataGridViewCell> dataGridViewCellList = new List<DataGridViewCell>();
      foreach (DataGridViewRow row in (IEnumerable) this.kryptonDataGridViewDetalleSellos.Rows)
      {
        dataGridViewCellList.Add(row.Cells["columnChapa"]);
        dataGridViewCellList.Add(row.Cells["columnCarga"]);
        dataGridViewCellList.Add(row.Cells["columnDescarga"]);
        row.Cells["columnChapa"].Style = row.Cells["columnCarga"].Style = row.Cells["columnDescarga"].Style = row.Cells[0].Style;
      }
      return dataGridViewCellList;
    }

    private void kryptonButtonCancelar_Click(object sender, EventArgs e)
    {
      this.ParentForm.Close();
    }

    private void toolStripButtonComplete_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.ValidateComplete())
          return;
        this.objRecepcionWraper.MakeComplete(this.currentDespacho.IdInspeccion);
        GUtils.ShowMessage("Transaccion completada");
        if (this.ObjSearch != null && this.ObjSearch.Count > 0)
          this.currentDespacho = this.objRecepcionWraper.GetDespachoById(new int?(this.getAsInt(this.ObjSearch, true)));
        this.fillForm();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error completando transaccion", ex);
      }
    }

    private void disableControls()
    {
      bool flag = true;
      this.kryptonPanelClienteTerminal.Enabled = flag;
      this.kryptonGroupBoxCamion.Enabled = flag;
      this.kryptonGroupBoxChofer.Enabled = flag;
      this.kryptonPanelComentario.Enabled = flag;
      this.kryptonDataGridViewDetalleSellos.Enabled = flag;
      this.kryptonDataGridViewMedida.Enabled = flag;
      this.kryptonDataGridViewVeri.Enabled = flag;
      this.kryptonButtonGuardar.Enabled = flag;
      this.kryptonTextBoxReferencia.Enabled = flag;
    }

    private bool ValidateComplete()
    {
      bool flag = true;
      if (this.currentTerminal == null || this.currentDespacho == null || (this.currentCamion == null || this.currentCliente == null) || (this.currentChofer == null || !this.kryptonDateTimePickerInitTime.Enabled || !this.kryptonDateTimePickerEndTime.Enabled))
        throw new Exception("Favor completar los datos :\n cliente, chofer,camion, terminal, hora inicio, hora fin");
      if (!this.valideteGridSellos())
        throw new Exception("Verificar los sellos utilizados, no son validos");
      string str = ConfigurationSettings.AppSettings.Get("validarSellosBlancos");
      bool result = false;
      bool.TryParse(str, out result);
      if (!this.validateSellosBlancos() && result)
        throw new Exception("Le faltan sellos por llenar");
      return flag;
    }

    private bool validateSellosBlancos()
    {
      IEnumerable<DataGridViewCell> source = this.getGridSellos().Where<DataGridViewCell>((Func<DataGridViewCell, bool>) (c =>
      {
        if (c.Value != null)
          return c.Value.Equals((object) "");
        return true;
      }));
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      gridViewCellStyle.BackColor = Color.Beige;
      gridViewCellStyle.SelectionBackColor = Color.Beige;
      foreach (DataGridViewCell dataGridViewCell in source)
        this.kryptonDataGridViewDetalleSellos[dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex].Style = gridViewCellStyle;
      return source.Count<DataGridViewCell>() <= 1;
    }

    private void kryptonDataGridViewDetalleSellos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
      if (!(e.Control is DataGridViewComboBoxEditingControl))
        return;
      ((ComboBox) e.Control).DropDownStyle = ComboBoxStyle.DropDown;
      ((ComboBox) e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
      ((ComboBox) e.Control).AutoCompleteMode = AutoCompleteMode.Suggest;
    }

    private void kryptonDataGridViewMedida_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
    }

    private void toolStripButtonVerifi_Click(object sender, EventArgs e)
    {
      if (!this.ValidateComplete())
      {
        GUtils.ShowMessage("No valido, favor revisar conduce");
      }
      else
      {
        string reportPath = "ConduceDespachoVerifica.rdlc";
        dsDataCareCargo dsDataCareCargo = new dsDataCareCargo();
        PrintReport printReport = new PrintReport(reportPath);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspeccionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionVerificationByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspeccionverificacionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionTotalProductByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspecciontotalproductosview.TableName);
        int num = (int) printReport.ShowDialog();
      }
    }

    private void toolStripButtonMedidas_Click(object sender, EventArgs e)
    {
      if (!this.ValidateComplete())
      {
        GUtils.ShowMessage("No valido, favor revisar conduce");
      }
      else
      {
        string reportPath = "ConduceDespachoMedida.rdlc";
        dsDataCareCargo dsDataCareCargo = new dsDataCareCargo();
        PrintReport printReport = new PrintReport(reportPath);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspeccionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadDetalleInspeccionByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.detalleinspeccionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionVerificationByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspeccionverificacionview.TableName);
        printReport.AddDataSource((DataTable) dsDataCareCargo.LoadInspeccionTotalProductByID(this.currentDespacho.IdInspeccion), dsDataCareCargo.inspecciontotalproductosview.TableName);
        int num = (int) printReport.ShowDialog();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonVerifi = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMedidas = new System.Windows.Forms.ToolStripButton();
            this.kryptonGroupBoxChofer = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabelCelular = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelLicencia = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelCedula = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.userControlSingleSearchChofer = new DCM.Core.UI.UserControlSingleSearch();
            this.kryptonGroupBoxCamion = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabelPlaca = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelFicha = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.userControlSingleSearchCamion = new DCM.Core.UI.UserControlSingleSearch();
            this.kryptonPanelComentario = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonTextBoxReferencia = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonComboBoxMedia = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonTextBoxComentario = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonDateTimePickerEndTime = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonDateTimePickerInitTime = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanelClienteTerminal = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.userControlSingleSearchTerminal = new DCM.Core.UI.UserControlSingleSearch();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.userControlSingleSearchCliente = new DCM.Core.UI.UserControlSingleSearch();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonDockableNavigatorTab = new ComponentFactory.Krypton.Docking.KryptonDockableNavigator();
            this.kryptonPageDeltalleCarga = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonDataGridViewDetalleSellos = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.columnCompartimentos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProducto = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnCantidad = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.columnChapa = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnCarga = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnDescarga = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.kryptonPageMedidaCarga = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonDataGridViewMedida = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonDataGridViewComboBoxColumnproducto1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.kryptonDataGridViewDomainUpDownColumn2 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.kryptonDataGridViewDomainUpDownColumn1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.kryptonDataGridViewComboBoxColumn2 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.Diferencia = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.kryptonNavigatorVerificacion = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonDataGridViewVeri = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.cItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.noColumn = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.naColumn = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonButtonCancelar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonGuardar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxChofer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxChofer.Panel)).BeginInit();
            this.kryptonGroupBoxChofer.Panel.SuspendLayout();
            this.kryptonGroupBoxChofer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCamion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCamion.Panel)).BeginInit();
            this.kryptonGroupBoxCamion.Panel.SuspendLayout();
            this.kryptonGroupBoxCamion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelComentario)).BeginInit();
            this.kryptonPanelComentario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBoxMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelClienteTerminal)).BeginInit();
            this.kryptonPanelClienteTerminal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableNavigatorTab)).BeginInit();
            this.kryptonDockableNavigatorTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageDeltalleCarga)).BeginInit();
            this.kryptonPageDeltalleCarga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewDetalleSellos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageMedidaCarga)).BeginInit();
            this.kryptonPageMedidaCarga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewMedida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorVerificacion)).BeginInit();
            this.kryptonNavigatorVerificacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewVeri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonDockableNavigatorTab);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonHeader1);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel1.Size = new System.Drawing.Size(781, 597);
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(781, 31);
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Values.Description = "Conduce para  Recepcion de Camiones";
            this.kryptonHeader1.Values.Heading = "Recepcion de Camiones";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.toolStrip1);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBoxChofer);
            this.kryptonPanel2.Controls.Add(this.kryptonGroupBoxCamion);
            this.kryptonPanel2.Controls.Add(this.kryptonPanelComentario);
            this.kryptonPanel2.Controls.Add(this.kryptonPanelClienteTerminal);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 31);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(781, 260);
            this.kryptonPanel2.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrint,
            this.toolStripButtonVerifi,
            this.toolStripButtonMedidas});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(781, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonVerifi
            // 
            this.toolStripButtonVerifi.Image = global::MSESG.CargoCare.Client.Properties.Resources.toolStripButtonVerifi_Image;
            this.toolStripButtonVerifi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonVerifi.Name = "toolStripButtonVerifi";
            this.toolStripButtonVerifi.Size = new System.Drawing.Size(137, 22);
            this.toolStripButtonVerifi.Text = "Imprimir Verificacion";
            this.toolStripButtonVerifi.Click += new System.EventHandler(this.toolStripButtonVerifi_Click);
            // 
            // toolStripButtonMedidas
            // 
            this.toolStripButtonMedidas.Image = global::MSESG.CargoCare.Client.Properties.Resources.toolStripButtonMedidas_Image;
            this.toolStripButtonMedidas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMedidas.Name = "toolStripButtonMedidas";
            this.toolStripButtonMedidas.Size = new System.Drawing.Size(121, 22);
            this.toolStripButtonMedidas.Text = "Imprimir Medidas";
            this.toolStripButtonMedidas.Click += new System.EventHandler(this.toolStripButtonMedidas_Click);
            // 
            // kryptonGroupBoxChofer
            // 
            this.kryptonGroupBoxChofer.Location = new System.Drawing.Point(478, 129);
            this.kryptonGroupBoxChofer.Name = "kryptonGroupBoxChofer";
            // 
            // kryptonGroupBoxChofer.Panel
            // 
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.kryptonLabelCelular);
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.kryptonLabelLicencia);
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.kryptonLabelCedula);
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.kryptonLabel11);
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.kryptonLabel9);
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.kryptonLabel10);
            this.kryptonGroupBoxChofer.Panel.Controls.Add(this.userControlSingleSearchChofer);
            this.kryptonGroupBoxChofer.Size = new System.Drawing.Size(290, 125);
            this.kryptonGroupBoxChofer.TabIndex = 10;
            // 
            // kryptonLabelCelular
            // 
            this.kryptonLabelCelular.Location = new System.Drawing.Point(68, 78);
            this.kryptonLabelCelular.Name = "kryptonLabelCelular";
            this.kryptonLabelCelular.Size = new System.Drawing.Size(13, 20);
            this.kryptonLabelCelular.TabIndex = 14;
            this.kryptonLabelCelular.Values.Text = ".";
            // 
            // kryptonLabelLicencia
            // 
            this.kryptonLabelLicencia.Location = new System.Drawing.Point(68, 58);
            this.kryptonLabelLicencia.Name = "kryptonLabelLicencia";
            this.kryptonLabelLicencia.Size = new System.Drawing.Size(13, 20);
            this.kryptonLabelLicencia.TabIndex = 12;
            this.kryptonLabelLicencia.Values.Text = ".";
            // 
            // kryptonLabelCedula
            // 
            this.kryptonLabelCedula.Location = new System.Drawing.Point(68, 36);
            this.kryptonLabelCedula.Name = "kryptonLabelCedula";
            this.kryptonLabelCedula.Size = new System.Drawing.Size(13, 20);
            this.kryptonLabelCedula.TabIndex = 11;
            this.kryptonLabelCedula.Values.Text = ".";
            // 
            // kryptonLabel11
            // 
            this.kryptonLabel11.Location = new System.Drawing.Point(4, 74);
            this.kryptonLabel11.Name = "kryptonLabel11";
            this.kryptonLabel11.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel11.TabIndex = 10;
            this.kryptonLabel11.Values.Text = "Telefono";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(9, 53);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(53, 20);
            this.kryptonLabel9.TabIndex = 9;
            this.kryptonLabel9.Values.Text = "Licencia";
            // 
            // kryptonLabel10
            // 
            this.kryptonLabel10.Location = new System.Drawing.Point(14, 35);
            this.kryptonLabel10.Name = "kryptonLabel10";
            this.kryptonLabel10.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel10.TabIndex = 8;
            this.kryptonLabel10.Values.Text = "Cedula";
            // 
            // userControlSingleSearchChofer
            // 
            this.userControlSingleSearchChofer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControlSingleSearchChofer.Enabled = false;
            this.userControlSingleSearchChofer.Location = new System.Drawing.Point(5, 3);
            this.userControlSingleSearchChofer.Name = "userControlSingleSearchChofer";
            this.userControlSingleSearchChofer.NameEntity = "";
            this.userControlSingleSearchChofer.Size = new System.Drawing.Size(275, 30);
            this.userControlSingleSearchChofer.TabIndex = 0;
            // 
            // kryptonGroupBoxCamion
            // 
            this.kryptonGroupBoxCamion.Location = new System.Drawing.Point(478, 22);
            this.kryptonGroupBoxCamion.Name = "kryptonGroupBoxCamion";
            // 
            // kryptonGroupBoxCamion.Panel
            // 
            this.kryptonGroupBoxCamion.Panel.Controls.Add(this.kryptonLabelPlaca);
            this.kryptonGroupBoxCamion.Panel.Controls.Add(this.kryptonLabelFicha);
            this.kryptonGroupBoxCamion.Panel.Controls.Add(this.kryptonLabel8);
            this.kryptonGroupBoxCamion.Panel.Controls.Add(this.kryptonLabel7);
            this.kryptonGroupBoxCamion.Panel.Controls.Add(this.userControlSingleSearchCamion);
            this.kryptonGroupBoxCamion.Size = new System.Drawing.Size(290, 106);
            this.kryptonGroupBoxCamion.TabIndex = 9;
            // 
            // kryptonLabelPlaca
            // 
            this.kryptonLabelPlaca.Location = new System.Drawing.Point(60, 60);
            this.kryptonLabelPlaca.Name = "kryptonLabelPlaca";
            this.kryptonLabelPlaca.Size = new System.Drawing.Size(13, 20);
            this.kryptonLabelPlaca.TabIndex = 9;
            this.kryptonLabelPlaca.Values.Text = ".";
            // 
            // kryptonLabelFicha
            // 
            this.kryptonLabelFicha.Location = new System.Drawing.Point(60, 36);
            this.kryptonLabelFicha.Name = "kryptonLabelFicha";
            this.kryptonLabelFicha.Size = new System.Drawing.Size(13, 20);
            this.kryptonLabelFicha.TabIndex = 8;
            this.kryptonLabelFicha.Values.Text = ".";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(14, 59);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel8.TabIndex = 7;
            this.kryptonLabel8.Values.Text = "Placa";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(14, 36);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel7.TabIndex = 6;
            this.kryptonLabel7.Values.Text = "Ficha";
            // 
            // userControlSingleSearchCamion
            // 
            this.userControlSingleSearchCamion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControlSingleSearchCamion.Enabled = false;
            this.userControlSingleSearchCamion.Location = new System.Drawing.Point(5, 3);
            this.userControlSingleSearchCamion.Name = "userControlSingleSearchCamion";
            this.userControlSingleSearchCamion.NameEntity = "";
            this.userControlSingleSearchCamion.Size = new System.Drawing.Size(278, 30);
            this.userControlSingleSearchCamion.TabIndex = 5;
            // 
            // kryptonPanelComentario
            // 
            this.kryptonPanelComentario.Controls.Add(this.kryptonTextBoxReferencia);
            this.kryptonPanelComentario.Controls.Add(this.kryptonComboBoxMedia);
            this.kryptonPanelComentario.Controls.Add(this.kryptonLabel6);
            this.kryptonPanelComentario.Controls.Add(this.kryptonLabel5);
            this.kryptonPanelComentario.Controls.Add(this.kryptonTextBoxComentario);
            this.kryptonPanelComentario.Controls.Add(this.kryptonDateTimePickerEndTime);
            this.kryptonPanelComentario.Controls.Add(this.kryptonDateTimePickerInitTime);
            this.kryptonPanelComentario.Controls.Add(this.kryptonLabel2);
            this.kryptonPanelComentario.Controls.Add(this.kryptonLabel4);
            this.kryptonPanelComentario.Controls.Add(this.kryptonLabel12);
            this.kryptonPanelComentario.Location = new System.Drawing.Point(9, 120);
            this.kryptonPanelComentario.Name = "kryptonPanelComentario";
            this.kryptonPanelComentario.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanelComentario.Size = new System.Drawing.Size(465, 135);
            this.kryptonPanelComentario.TabIndex = 8;
            // 
            // kryptonTextBoxReferencia
            // 
            this.kryptonTextBoxReferencia.Location = new System.Drawing.Point(341, 108);
            this.kryptonTextBoxReferencia.Name = "kryptonTextBoxReferencia";
            this.kryptonTextBoxReferencia.Size = new System.Drawing.Size(100, 23);
            this.kryptonTextBoxReferencia.TabIndex = 15;
            // 
            // kryptonComboBoxMedia
            // 
            this.kryptonComboBoxMedia.DropDownWidth = 56;
            this.kryptonComboBoxMedia.Location = new System.Drawing.Point(278, 107);
            this.kryptonComboBoxMedia.Name = "kryptonComboBoxMedia";
            this.kryptonComboBoxMedia.Size = new System.Drawing.Size(56, 21);
            this.kryptonComboBoxMedia.TabIndex = 13;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(278, 88);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(30, 20);
            this.kryptonLabel6.TabIndex = 12;
            this.kryptonLabel6.Values.Text = "UM";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(10, 5);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(75, 20);
            this.kryptonLabel5.TabIndex = 11;
            this.kryptonLabel5.Values.Text = "Comentario";
            // 
            // kryptonTextBoxComentario
            // 
            this.kryptonTextBoxComentario.Location = new System.Drawing.Point(10, 25);
            this.kryptonTextBoxComentario.Multiline = true;
            this.kryptonTextBoxComentario.Name = "kryptonTextBoxComentario";
            this.kryptonTextBoxComentario.Size = new System.Drawing.Size(443, 61);
            this.kryptonTextBoxComentario.TabIndex = 10;
            // 
            // kryptonDateTimePickerEndTime
            // 
            this.kryptonDateTimePickerEndTime.CalendarTodayDate = new System.DateTime(2012, 5, 13, 0, 0, 0, 0);
            this.kryptonDateTimePickerEndTime.Checked = false;
            this.kryptonDateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.kryptonDateTimePickerEndTime.Location = new System.Drawing.Point(150, 107);
            this.kryptonDateTimePickerEndTime.Name = "kryptonDateTimePickerEndTime";
            this.kryptonDateTimePickerEndTime.ShowCheckBox = true;
            this.kryptonDateTimePickerEndTime.Size = new System.Drawing.Size(113, 21);
            this.kryptonDateTimePickerEndTime.TabIndex = 7;
            // 
            // kryptonDateTimePickerInitTime
            // 
            this.kryptonDateTimePickerInitTime.CalendarTodayDate = new System.DateTime(2012, 5, 13, 0, 0, 0, 0);
            this.kryptonDateTimePickerInitTime.Checked = false;
            this.kryptonDateTimePickerInitTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.kryptonDateTimePickerInitTime.Location = new System.Drawing.Point(10, 107);
            this.kryptonDateTimePickerInitTime.Name = "kryptonDateTimePickerInitTime";
            this.kryptonDateTimePickerInitTime.ShowCheckBox = true;
            this.kryptonDateTimePickerInitTime.Size = new System.Drawing.Size(119, 21);
            this.kryptonDateTimePickerInitTime.TabIndex = 6;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(10, 88);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(70, 20);
            this.kryptonLabel2.TabIndex = 8;
            this.kryptonLabel2.Values.Text = "Hora Inicio";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(150, 88);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(57, 20);
            this.kryptonLabel4.TabIndex = 9;
            this.kryptonLabel4.Values.Text = "Hora Fin";
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(340, 88);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(90, 20);
            this.kryptonLabel12.TabIndex = 14;
            this.kryptonLabel12.Values.Text = "Referencia No.";
            // 
            // kryptonPanelClienteTerminal
            // 
            this.kryptonPanelClienteTerminal.Controls.Add(this.userControlSingleSearchTerminal);
            this.kryptonPanelClienteTerminal.Controls.Add(this.kryptonLabel3);
            this.kryptonPanelClienteTerminal.Controls.Add(this.userControlSingleSearchCliente);
            this.kryptonPanelClienteTerminal.Controls.Add(this.kryptonLabel1);
            this.kryptonPanelClienteTerminal.Location = new System.Drawing.Point(9, 34);
            this.kryptonPanelClienteTerminal.Name = "kryptonPanelClienteTerminal";
            this.kryptonPanelClienteTerminal.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanelClienteTerminal.Size = new System.Drawing.Size(465, 80);
            this.kryptonPanelClienteTerminal.TabIndex = 0;
            // 
            // userControlSingleSearchTerminal
            // 
            this.userControlSingleSearchTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControlSingleSearchTerminal.Location = new System.Drawing.Point(233, 24);
            this.userControlSingleSearchTerminal.Name = "userControlSingleSearchTerminal";
            this.userControlSingleSearchTerminal.NameEntity = "";
            this.userControlSingleSearchTerminal.Size = new System.Drawing.Size(216, 30);
            this.userControlSingleSearchTerminal.TabIndex = 5;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.AutoSize = false;
            this.kryptonLabel3.Location = new System.Drawing.Point(233, 3);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(58, 20);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "Terminal";
            // 
            // userControlSingleSearchCliente
            // 
            this.userControlSingleSearchCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControlSingleSearchCliente.Location = new System.Drawing.Point(10, 24);
            this.userControlSingleSearchCliente.Name = "userControlSingleSearchCliente";
            this.userControlSingleSearchCliente.NameEntity = "";
            this.userControlSingleSearchCliente.Size = new System.Drawing.Size(216, 30);
            this.userControlSingleSearchCliente.TabIndex = 3;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.AutoSize = false;
            this.kryptonLabel1.Location = new System.Drawing.Point(10, 6);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(48, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Cliente";
            // 
            // kryptonDockableNavigatorTab
            // 
            this.kryptonDockableNavigatorTab.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.kryptonDockableNavigatorTab.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonDockableNavigatorTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableNavigatorTab.Location = new System.Drawing.Point(0, 291);
            this.kryptonDockableNavigatorTab.Name = "kryptonDockableNavigatorTab";
            this.kryptonDockableNavigatorTab.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPageDeltalleCarga,
            this.kryptonPageMedidaCarga,
            this.kryptonNavigatorVerificacion});
            this.kryptonDockableNavigatorTab.SelectedIndex = 1;
            this.kryptonDockableNavigatorTab.Size = new System.Drawing.Size(781, 273);
            this.kryptonDockableNavigatorTab.TabIndex = 10;
            this.kryptonDockableNavigatorTab.Text = "kryptonDockableNavigator1";
            // 
            // kryptonPageDeltalleCarga
            // 
            this.kryptonPageDeltalleCarga.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageDeltalleCarga.Controls.Add(this.kryptonDataGridViewDetalleSellos);
            this.kryptonPageDeltalleCarga.Flags = 65534;
            this.kryptonPageDeltalleCarga.LastVisibleSet = true;
            this.kryptonPageDeltalleCarga.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageDeltalleCarga.Name = "kryptonPageDeltalleCarga";
            this.kryptonPageDeltalleCarga.Size = new System.Drawing.Size(779, 246);
            this.kryptonPageDeltalleCarga.Text = "Detalle Sellos";
            this.kryptonPageDeltalleCarga.ToolTipTitle = "Page ToolTip";
            this.kryptonPageDeltalleCarga.UniqueName = "1EFA288D16C3466302B1395A7F18E733";
            // 
            // kryptonDataGridViewDetalleSellos
            // 
            this.kryptonDataGridViewDetalleSellos.AllowUserToAddRows = false;
            this.kryptonDataGridViewDetalleSellos.AllowUserToDeleteRows = false;
            this.kryptonDataGridViewDetalleSellos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCompartimentos,
            this.columnProducto,
            this.columnCantidad,
            this.columnChapa,
            this.columnCarga,
            this.columnDescarga});
            this.kryptonDataGridViewDetalleSellos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDataGridViewDetalleSellos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.kryptonDataGridViewDetalleSellos.Location = new System.Drawing.Point(0, 0);
            this.kryptonDataGridViewDetalleSellos.Name = "kryptonDataGridViewDetalleSellos";
            this.kryptonDataGridViewDetalleSellos.RowHeadersVisible = false;
            this.kryptonDataGridViewDetalleSellos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.kryptonDataGridViewDetalleSellos.Size = new System.Drawing.Size(779, 246);
            this.kryptonDataGridViewDetalleSellos.TabIndex = 0;
            this.kryptonDataGridViewDetalleSellos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.kryptonDataGridViewDetalleSellos_EditingControlShowing);
            // 
            // columnCompartimentos
            // 
            this.columnCompartimentos.DataPropertyName = "Compartimento";
            this.columnCompartimentos.HeaderText = "C.No.";
            this.columnCompartimentos.Name = "columnCompartimentos";
            this.columnCompartimentos.ReadOnly = true;
            this.columnCompartimentos.Width = 60;
            // 
            // columnProducto
            // 
            this.columnProducto.DataPropertyName = "Producto";
            this.columnProducto.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.columnProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnProducto.HeaderText = "Producto";
            this.columnProducto.Name = "columnProducto";
            this.columnProducto.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProducto.Width = 250;
            // 
            // columnCantidad
            // 
            this.columnCantidad.DataPropertyName = "Cantidad";
            this.columnCantidad.HeaderText = "Cantidad";
            this.columnCantidad.Maximum = new decimal(new int[] {
            -1981284352,
            -1966660860,
            0,
            0});
            this.columnCantidad.Name = "columnCantidad";
            this.columnCantidad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnCantidad.Width = 110;
            // 
            // columnChapa
            // 
            this.columnChapa.DataPropertyName = "Chapa";
            this.columnChapa.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.columnChapa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnChapa.HeaderText = "Chapa/Manhole";
            this.columnChapa.Name = "columnChapa";
            this.columnChapa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnChapa.Width = 110;
            // 
            // columnCarga
            // 
            this.columnCarga.DataPropertyName = "BocaCarga";
            this.columnCarga.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.columnCarga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnCarga.HeaderText = "Boca de Carga";
            this.columnCarga.Name = "columnCarga";
            this.columnCarga.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnCarga.Width = 110;
            // 
            // columnDescarga
            // 
            this.columnDescarga.DataPropertyName = "BacaDescarga";
            this.columnDescarga.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.columnDescarga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnDescarga.HeaderText = "Boca de Descarga";
            this.columnDescarga.Name = "columnDescarga";
            this.columnDescarga.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnDescarga.Width = 110;
            // 
            // kryptonPageMedidaCarga
            // 
            this.kryptonPageMedidaCarga.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageMedidaCarga.Controls.Add(this.kryptonDataGridViewMedida);
            this.kryptonPageMedidaCarga.Flags = 65534;
            this.kryptonPageMedidaCarga.LastVisibleSet = true;
            this.kryptonPageMedidaCarga.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageMedidaCarga.Name = "kryptonPageMedidaCarga";
            this.kryptonPageMedidaCarga.Size = new System.Drawing.Size(779, 246);
            this.kryptonPageMedidaCarga.Text = "Medida Carga";
            this.kryptonPageMedidaCarga.ToolTipTitle = "Page ToolTip";
            this.kryptonPageMedidaCarga.UniqueName = "B07998A0ED644F2BE6A311E7B0206B10";
            // 
            // kryptonDataGridViewMedida
            // 
            this.kryptonDataGridViewMedida.AllowUserToAddRows = false;
            this.kryptonDataGridViewMedida.AllowUserToDeleteRows = false;
            this.kryptonDataGridViewMedida.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.kryptonDataGridViewComboBoxColumnproducto1,
            this.kryptonDataGridViewDomainUpDownColumn2,
            this.kryptonDataGridViewDomainUpDownColumn1,
            this.kryptonDataGridViewComboBoxColumn2,
            this.Diferencia});
            this.kryptonDataGridViewMedida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDataGridViewMedida.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.kryptonDataGridViewMedida.Location = new System.Drawing.Point(0, 0);
            this.kryptonDataGridViewMedida.Name = "kryptonDataGridViewMedida";
            this.kryptonDataGridViewMedida.RowHeadersVisible = false;
            this.kryptonDataGridViewMedida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.kryptonDataGridViewMedida.Size = new System.Drawing.Size(779, 246);
            this.kryptonDataGridViewMedida.TabIndex = 1;
            this.kryptonDataGridViewMedida.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.kryptonDataGridViewMedida_EditingControlShowing);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Compartimento";
            this.dataGridViewTextBoxColumn3.HeaderText = "C.No.";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // kryptonDataGridViewComboBoxColumnproducto1
            // 
            this.kryptonDataGridViewComboBoxColumnproducto1.DataPropertyName = "Producto";
            this.kryptonDataGridViewComboBoxColumnproducto1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.kryptonDataGridViewComboBoxColumnproducto1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kryptonDataGridViewComboBoxColumnproducto1.HeaderText = "Producto";
            this.kryptonDataGridViewComboBoxColumnproducto1.Name = "kryptonDataGridViewComboBoxColumnproducto1";
            this.kryptonDataGridViewComboBoxColumnproducto1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kryptonDataGridViewComboBoxColumnproducto1.Width = 300;
            // 
            // kryptonDataGridViewDomainUpDownColumn2
            // 
            this.kryptonDataGridViewDomainUpDownColumn2.DataPropertyName = "Unidad";
            this.kryptonDataGridViewDomainUpDownColumn2.HeaderText = "MED./Comp";
            this.kryptonDataGridViewDomainUpDownColumn2.Name = "kryptonDataGridViewDomainUpDownColumn2";
            this.kryptonDataGridViewDomainUpDownColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kryptonDataGridViewDomainUpDownColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kryptonDataGridViewDomainUpDownColumn2.Width = 80;
            // 
            // kryptonDataGridViewDomainUpDownColumn1
            // 
            this.kryptonDataGridViewDomainUpDownColumn1.DataPropertyName = "Cantidad";
            this.kryptonDataGridViewDomainUpDownColumn1.HeaderText = "Cantidad Medida";
            this.kryptonDataGridViewDomainUpDownColumn1.Maximum = new decimal(new int[] {
            -1981284352,
            -1966660860,
            0,
            0});
            this.kryptonDataGridViewDomainUpDownColumn1.Name = "kryptonDataGridViewDomainUpDownColumn1";
            this.kryptonDataGridViewDomainUpDownColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kryptonDataGridViewDomainUpDownColumn1.Width = 110;
            // 
            // kryptonDataGridViewComboBoxColumn2
            // 
            this.kryptonDataGridViewComboBoxColumn2.DataPropertyName = "Medida";
            this.kryptonDataGridViewComboBoxColumn2.HeaderText = "Cantidad Terminal";
            this.kryptonDataGridViewComboBoxColumn2.Maximum = new decimal(new int[] {
            -1981284352,
            -1966660860,
            0,
            0});
            this.kryptonDataGridViewComboBoxColumn2.Name = "kryptonDataGridViewComboBoxColumn2";
            this.kryptonDataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kryptonDataGridViewComboBoxColumn2.Width = 110;
            // 
            // Diferencia
            // 
            this.Diferencia.DataPropertyName = "Diferencia";
            this.Diferencia.HeaderText = "Diferencia";
            this.Diferencia.Maximum = new decimal(new int[] {
            -1981284352,
            -1966660860,
            0,
            0});
            this.Diferencia.Name = "Diferencia";
            this.Diferencia.ReadOnly = true;
            this.Diferencia.Width = 100;
            // 
            // kryptonNavigatorVerificacion
            // 
            this.kryptonNavigatorVerificacion.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonNavigatorVerificacion.Controls.Add(this.kryptonDataGridViewVeri);
            this.kryptonNavigatorVerificacion.Flags = 65534;
            this.kryptonNavigatorVerificacion.LastVisibleSet = true;
            this.kryptonNavigatorVerificacion.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonNavigatorVerificacion.Name = "kryptonNavigatorVerificacion";
            this.kryptonNavigatorVerificacion.Size = new System.Drawing.Size(779, 246);
            this.kryptonNavigatorVerificacion.Text = "Verificacion";
            this.kryptonNavigatorVerificacion.ToolTipTitle = "Page ToolTip";
            this.kryptonNavigatorVerificacion.UniqueName = "E3DB39416EC344791DB643023348A715";
            // 
            // kryptonDataGridViewVeri
            // 
            this.kryptonDataGridViewVeri.AllowUserToAddRows = false;
            this.kryptonDataGridViewVeri.AllowUserToDeleteRows = false;
            this.kryptonDataGridViewVeri.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cItem,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.noColumn,
            this.naColumn,
            this.dataGridViewTextBoxColumn6});
            this.kryptonDataGridViewVeri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDataGridViewVeri.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.kryptonDataGridViewVeri.Location = new System.Drawing.Point(0, 0);
            this.kryptonDataGridViewVeri.Name = "kryptonDataGridViewVeri";
            this.kryptonDataGridViewVeri.RowHeadersVisible = false;
            this.kryptonDataGridViewVeri.Size = new System.Drawing.Size(779, 246);
            this.kryptonDataGridViewVeri.TabIndex = 2;
            // 
            // cItem
            // 
            this.cItem.DataPropertyName = "ItemId";
            this.cItem.HeaderText = "ItemID";
            this.cItem.Name = "cItem";
            this.cItem.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Item";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Condicion a Verificar";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 370;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SI";
            this.dataGridViewTextBoxColumn2.HeaderText = "      SI";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // noColumn
            // 
            this.noColumn.DataPropertyName = "NO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            this.noColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.noColumn.FalseValue = null;
            this.noColumn.HeaderText = "     NO";
            this.noColumn.IndeterminateValue = null;
            this.noColumn.Name = "noColumn";
            this.noColumn.TrueValue = null;
            this.noColumn.Width = 60;
            // 
            // naColumn
            // 
            this.naColumn.DataPropertyName = "NA";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = false;
            this.naColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.naColumn.FalseValue = null;
            this.naColumn.HeaderText = "    N/A";
            this.naColumn.IndeterminateValue = null;
            this.naColumn.Name = "naColumn";
            this.naColumn.TrueValue = null;
            this.naColumn.Width = 60;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Comentario";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn6.HeaderText = "            Comentario";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 220;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonButtonCancelar);
            this.kryptonPanel3.Controls.Add(this.kryptonButtonGuardar);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 564);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(781, 33);
            this.kryptonPanel3.TabIndex = 11;
            // 
            // kryptonButtonCancelar
            // 
            this.kryptonButtonCancelar.Location = new System.Drawing.Point(678, 4);
            this.kryptonButtonCancelar.Name = "kryptonButtonCancelar";
            this.kryptonButtonCancelar.Size = new System.Drawing.Size(90, 25);
            this.kryptonButtonCancelar.TabIndex = 1;
            this.kryptonButtonCancelar.Values.Text = "Cerrar";
            this.kryptonButtonCancelar.Click += new System.EventHandler(this.kryptonButtonCancelar_Click);
            // 
            // kryptonButtonGuardar
            // 
            this.kryptonButtonGuardar.Location = new System.Drawing.Point(571, 4);
            this.kryptonButtonGuardar.Name = "kryptonButtonGuardar";
            this.kryptonButtonGuardar.Size = new System.Drawing.Size(90, 25);
            this.kryptonButtonGuardar.TabIndex = 0;
            this.kryptonButtonGuardar.Values.Text = "Guardar";
            this.kryptonButtonGuardar.Click += new System.EventHandler(this.kryptonButtonGuardar_Click);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.Image = global::MSESG.CargoCare.Client.Properties.Resources.toolStripButtonPrint_Image;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(124, 22);
            this.toolStripButtonPrint.Text = "Imprimir Conduce";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // UserControlRecepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UserControlRecepcion";
            this.Size = new System.Drawing.Size(781, 597);
            this.Load += new System.EventHandler(this.UserControlDespacho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxChofer.Panel)).EndInit();
            this.kryptonGroupBoxChofer.Panel.ResumeLayout(false);
            this.kryptonGroupBoxChofer.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxChofer)).EndInit();
            this.kryptonGroupBoxChofer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCamion.Panel)).EndInit();
            this.kryptonGroupBoxCamion.Panel.ResumeLayout(false);
            this.kryptonGroupBoxCamion.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBoxCamion)).EndInit();
            this.kryptonGroupBoxCamion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelComentario)).EndInit();
            this.kryptonPanelComentario.ResumeLayout(false);
            this.kryptonPanelComentario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBoxMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelClienteTerminal)).EndInit();
            this.kryptonPanelClienteTerminal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableNavigatorTab)).EndInit();
            this.kryptonDockableNavigatorTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageDeltalleCarga)).EndInit();
            this.kryptonPageDeltalleCarga.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewDetalleSellos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageMedidaCarga)).EndInit();
            this.kryptonPageMedidaCarga.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewMedida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigatorVerificacion)).EndInit();
            this.kryptonNavigatorVerificacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridViewVeri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

    }
  }
}
