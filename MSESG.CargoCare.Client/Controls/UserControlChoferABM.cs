using ComponentFactory.Krypton.Toolkit;
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
  public partial class UserControlChoferABM : UserControlCareCargoBase
  {
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonHeader kryptonHeader2;
    private KryptonPanel kryptonPanelInner;
    private KryptonTextBox kryptonTextBoxComentario;
    private KryptonLabel kryptonLabel8;
    private KryptonLabel kryptonLabel4;
    private KryptonTextBox kryptonTextBoxTelefono;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxNombre;
    private KryptonLabel kryptonLabel2;
    private KryptonTextBox kryptonTextBoxCedula;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBoxLicencia;
    private KryptonLabel kryptonLabel5;
    private UserControlSingleSearch userControlSingleSearchCliente;
    private ChoferWraper objChoferWraper;
    private ClienteWraper objClienteWraper;
    private chofer currentChofer;
    private cliente currentCliente;
    private cargocareEntities dbEnt;

    public UserControlChoferABM(List<SearchResult> objSearch = null)
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

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlChoferABM));
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonButtonAccept = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.kryptonHeader2 = new KryptonHeader();
      this.kryptonPanelInner = new KryptonPanel();
      this.kryptonTextBoxComentario = new KryptonTextBox();
      this.kryptonLabel8 = new KryptonLabel();
      this.kryptonLabel4 = new KryptonLabel();
      this.kryptonTextBoxTelefono = new KryptonTextBox();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonTextBoxNombre = new KryptonTextBox();
      this.kryptonLabel2 = new KryptonLabel();
      this.kryptonTextBoxCedula = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.kryptonTextBoxLicencia = new KryptonTextBox();
      this.kryptonLabel5 = new KryptonLabel();
      this.userControlSingleSearchCliente = new UserControlSingleSearch();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanelInner.BeginInit();
      this.kryptonPanelInner.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabel5);
      this.kryptonPanel1.Controls.Add((Control) this.userControlSingleSearchCliente);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonPanelInner);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader2);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonAccept);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(428, 397);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(428, 4);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "";
      this.kryptonHeader1.Values.Image = (Image) null;
      this.kryptonButtonAccept.Location = new Point(212, 331);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(321, 331);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.kryptonHeader2.Dock = DockStyle.Top;
      this.kryptonHeader2.Location = new Point(0, 4);
      this.kryptonHeader2.Name = "kryptonHeader2";
      this.kryptonHeader2.Size = new Size(428, 28);
      this.kryptonHeader2.TabIndex = 10;
      this.kryptonHeader2.Values.Description = "";
      this.kryptonHeader2.Values.Heading = "";
      //this.kryptonHeader2.Values.Image = (Image) componentResourceManager.GetObject("kryptonHeader2.Values.Image");
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonTextBoxComentario);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonLabel8);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonLabel4);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonTextBoxTelefono);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonTextBoxNombre);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonTextBoxCedula);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanelInner.Controls.Add((Control) this.kryptonTextBoxLicencia);
      this.kryptonPanelInner.Enabled = false;
      this.kryptonPanelInner.Location = new Point(15, 104);
      this.kryptonPanelInner.Name = "kryptonPanelInner";
      this.kryptonPanelInner.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanelInner.Size = new Size(396, 209);
      this.kryptonPanelInner.TabIndex = 11;
      this.kryptonTextBoxComentario.Location = new Point(122, 123);
      this.kryptonTextBoxComentario.Multiline = true;
      this.kryptonTextBoxComentario.Name = "kryptonTextBoxComentario";
      this.kryptonTextBoxComentario.Size = new Size(234, 57);
      this.kryptonTextBoxComentario.TabIndex = 21;
      this.kryptonLabel8.Location = new Point(43, 123);
      this.kryptonLabel8.Name = "kryptonLabel8";
      this.kryptonLabel8.Size = new Size(75, 20);
      this.kryptonLabel8.TabIndex = 16;
      this.kryptonLabel8.Values.Text = "Comentario";
      this.kryptonLabel4.Location = new Point(60, 100);
      this.kryptonLabel4.Name = "kryptonLabel4";
      this.kryptonLabel4.Size = new Size(58, 20);
      this.kryptonLabel4.TabIndex = 8;
      this.kryptonLabel4.Values.Text = "Telefono";
      this.kryptonTextBoxTelefono.Location = new Point(122, 97);
      this.kryptonTextBoxTelefono.Name = "kryptonTextBoxTelefono";
      this.kryptonTextBoxTelefono.Size = new Size(234, 20);
      this.kryptonTextBoxTelefono.TabIndex = 7;
      this.kryptonLabel3.Location = new Point(62, 22);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(56, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Nombre";
      this.kryptonTextBoxNombre.Location = new Point(122, 22);
      this.kryptonTextBoxNombre.Name = "kryptonTextBoxNombre";
      this.kryptonTextBoxNombre.Size = new Size(234, 20);
      this.kryptonTextBoxNombre.TabIndex = 1;
      this.kryptonLabel2.Location = new Point(65, 74);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(53, 20);
      this.kryptonLabel2.TabIndex = 5;
      this.kryptonLabel2.Values.Text = "Licencia";
      this.kryptonTextBoxCedula.Location = new Point(122, 47);
      this.kryptonTextBoxCedula.Name = "kryptonTextBoxCedula";
      this.kryptonTextBoxCedula.Size = new Size(234, 20);
      this.kryptonTextBoxCedula.TabIndex = 2;
      this.kryptonLabel1.Location = new Point(70, 48);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(48, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Cedula";
      this.kryptonTextBoxLicencia.Location = new Point(122, 72);
      this.kryptonTextBoxLicencia.Name = "kryptonTextBoxLicencia";
      this.kryptonTextBoxLicencia.Size = new Size(234, 20);
      this.kryptonTextBoxLicencia.TabIndex = 3;
      this.kryptonLabel5.Location = new Point(11, 42);
      this.kryptonLabel5.Name = "kryptonLabel5";
      this.kryptonLabel5.Size = new Size(48, 20);
      this.kryptonLabel5.TabIndex = 20;
      this.kryptonLabel5.Values.Text = "Cliente";
      this.userControlSingleSearchCliente.BorderStyle = BorderStyle.FixedSingle;
      this.userControlSingleSearchCliente.Location = new Point(13, 65);
      this.userControlSingleSearchCliente.Name = "userControlSingleSearchCliente";
      this.userControlSingleSearchCliente.NameEntity = "";
      this.userControlSingleSearchCliente.Size = new Size(399, 30);
      this.userControlSingleSearchCliente.TabIndex = 19;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlChoferABM";
      this.Size = new Size(428, 397);
      this.Load += new EventHandler(this.UserControlChoferABM_Load);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.kryptonPanelInner.EndInit();
      this.kryptonPanelInner.ResumeLayout(false);
      this.kryptonPanelInner.PerformLayout();
      this.ResumeLayout(false);
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
        this.userControlSingleSearchCliente.Enabled = false;
        this.ParentForm.Close();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Erro guardando registro", ex);
      }
    }

    private void save()
    {
      this.currentChofer = this.objChoferWraper.Save(this.currentChofer != null ? new int?(this.currentChofer.IdChofer) : new int?(), this.kryptonTextBoxNombre.Text, this.kryptonTextBoxCedula.Text, this.kryptonTextBoxLicencia.Text, this.kryptonTextBoxTelefono.Text, this.kryptonTextBoxComentario.Text, this.currentCliente.IdCliente);
    }

    private void prepareControls()
    {
      this.objChoferWraper = new ChoferWraper();
      this.currentChofer = this.objChoferWraper.GetChofer(this.ObjSearch);
      this.userControlSingleSearchCliente.SetEntity<cliente>((IQueryable<cliente>) this.dbEnt.clientes, cliente.Meta.Columns.NombreCliente, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchCliente.SetMantenimiento((UserControlBase) new UserControlClienteABM((List<SearchResult>) null), "Cliente");
      this.userControlSingleSearchCliente.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchCliente_OnValueChanged);
      if (this.currentChofer != null && this.currentChofer.IdCliente.HasValue)
      {
        this.userControlSingleSearchCliente.SetValues(new List<SearchResult>()
        {
          new SearchResult(cliente.Meta.Columns.IdCliente, (object) this.currentChofer.IdCliente)
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
      if (this.currentChofer == null)
        return;
      this.kryptonTextBoxNombre.Text = this.currentChofer.Nombre;
      this.kryptonTextBoxCedula.Text = this.currentChofer.Cedula;
      this.kryptonTextBoxLicencia.Text = this.currentChofer.Licencia;
      this.kryptonTextBoxTelefono.Text = this.currentChofer.Telefono;
      this.kryptonTextBoxComentario.Text = this.currentChofer.Comentario;
    }

    private void UserControlChoferABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }
  }
}
