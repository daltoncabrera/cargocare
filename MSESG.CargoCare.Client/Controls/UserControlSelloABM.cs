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
  public class UserControlSelloABM : UserControlCareCargoBase
  {
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAgregar;
    private KryptonButton kryptonButtonAgregarRango;
    private KryptonLabel kryptonLabel3;
    private KryptonLabel kryptonLabel2;
    private KryptonNumericUpDown kryptonNumericUpDownDesde;
    private KryptonNumericUpDown kryptonNumericUpDownHasta;
    private KryptonTextBox kryptonTextBoxButtonName;
    private KryptonTextBox kryptonTextBoxTitle;
    private KryptonGroupBox kryptonGroupBoxRange;
    private KryptonGroupBox kryptonGroupBoxSingle;
    private KryptonTextBox kryptonTextBoxCode;
    private KryptonLabel kryptonLabel4;
    private KryptonLabel kryptonLabel1;
    private UserControlSingleSearch userControlSingleSearchCliente;
    private KryptonLabel kryptonLabelLastSello;
    private KryptonLabel kryptonLabel5;
    private KryptonLabel kryptonLabel6;
    private KryptonCheckBox kryptonCheckBox1;
    private KryptonNumericUpDown kryptonNumericUpDownLote;
    private KryptonNumericUpDown kryptonNumericUpDownCodSello;
    private SelloWraper objSelloWraper;
    private sello lastSello;
    private cargocareEntities dbEnt;

    public event UserControlSelloABM.AgregateSello OnAgregateSello;

    public UserControlSelloABM(List<SearchResult> objSearch = null)
    {
      this.InitializeComponent();
      this.ObjSearch = objSearch;
      this.objSelloWraper = new SelloWraper();
      this.dbEnt = new cargocareEntities();
      this.kryptonNumericUpDownDesde.Maximum = new Decimal(-1, -1, -1, false, (byte) 0);
      this.kryptonNumericUpDownHasta.Maximum = new Decimal(-1, -1, -1, false, (byte) 0);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonButtonAgregar = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonLabel2 = new KryptonLabel();
      this.kryptonNumericUpDownDesde = new KryptonNumericUpDown();
      this.kryptonNumericUpDownHasta = new KryptonNumericUpDown();
      this.kryptonTextBoxButtonName = new KryptonTextBox();
      this.kryptonTextBoxTitle = new KryptonTextBox();
      this.kryptonButtonAgregarRango = new KryptonButton();
      this.kryptonGroupBoxSingle = new KryptonGroupBox();
      this.kryptonLabel4 = new KryptonLabel();
      this.kryptonTextBoxCode = new KryptonTextBox();
      this.kryptonGroupBoxRange = new KryptonGroupBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.userControlSingleSearchCliente = new UserControlSingleSearch();
      this.kryptonLabel5 = new KryptonLabel();
      this.kryptonLabelLastSello = new KryptonLabel();
      this.kryptonCheckBox1 = new KryptonCheckBox();
      this.kryptonLabel6 = new KryptonLabel();
      this.kryptonNumericUpDownLote = new KryptonNumericUpDown();
      this.kryptonNumericUpDownCodSello = new KryptonNumericUpDown();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonGroupBoxSingle.BeginInit();
      this.kryptonGroupBoxSingle.Panel.SuspendLayout();
      this.kryptonGroupBoxSingle.SuspendLayout();
      this.kryptonGroupBoxRange.BeginInit();
      this.kryptonGroupBoxRange.Panel.SuspendLayout();
      this.kryptonGroupBoxRange.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonNumericUpDownCodSello);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonNumericUpDownLote);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabel6);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonCheckBox1);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabelLastSello);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabel5);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel1.Controls.Add((Control) this.userControlSingleSearchCliente);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonGroupBoxRange);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonGroupBoxSingle);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(440, 381);
      this.kryptonPanel1.Paint += new PaintEventHandler(this.kryptonPanel1_Paint);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(440, 31);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "Mantenimientos Sellos";
      this.kryptonHeader1.Values.Image = (Image) null;
      this.kryptonButtonAgregar.Location = new Point(227, 15);
      this.kryptonButtonAgregar.Name = "kryptonButtonAgregar";
      this.kryptonButtonAgregar.Size = new Size(90, 25);
      this.kryptonButtonAgregar.TabIndex = 8;
      this.kryptonButtonAgregar.Values.Text = "Agregar";
      this.kryptonButtonAgregar.Click += new EventHandler(this.kryptonButtonAgregar_Click);
      this.kryptonButtonCancel.Location = new Point(339, 313);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cerrar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.kryptonLabel3.Location = new Point(144, 15);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(41, 20);
      this.kryptonLabel3.TabIndex = 5;
      this.kryptonLabel3.Values.Text = "Hasta";
      this.kryptonLabel2.Location = new Point(4, 15);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(45, 20);
      this.kryptonLabel2.TabIndex = 4;
      this.kryptonLabel2.Values.Text = "Desde";
      this.kryptonNumericUpDownDesde.Location = new Point(43, 13);
      this.kryptonNumericUpDownDesde.Name = "kryptonNumericUpDownDesde";
      this.kryptonNumericUpDownDesde.Size = new Size(104, 22);
      this.kryptonNumericUpDownDesde.TabIndex = 1;
      this.kryptonNumericUpDownHasta.Location = new Point(191, 13);
      this.kryptonNumericUpDownHasta.Name = "kryptonNumericUpDownHasta";
      this.kryptonNumericUpDownHasta.Size = new Size(104, 22);
      this.kryptonNumericUpDownHasta.TabIndex = 0;
      this.kryptonTextBoxButtonName.Location = new Point(164, 74);
      this.kryptonTextBoxButtonName.Name = "kryptonTextBoxButtonName";
      this.kryptonTextBoxButtonName.Size = new Size(234, 17);
      this.kryptonTextBoxButtonName.TabIndex = 3;
      this.kryptonTextBoxTitle.Location = new Point(164, 22);
      this.kryptonTextBoxTitle.Name = "kryptonTextBoxTitle";
      this.kryptonTextBoxTitle.Size = new Size(234, 17);
      this.kryptonTextBoxTitle.TabIndex = 1;
      this.kryptonButtonAgregarRango.Location = new Point(312, 13);
      this.kryptonButtonAgregarRango.Name = "kryptonButtonAgregarRango";
      this.kryptonButtonAgregarRango.Size = new Size(97, 25);
      this.kryptonButtonAgregarRango.TabIndex = 12;
      this.kryptonButtonAgregarRango.Values.Text = "Agregar Rango";
      this.kryptonButtonAgregarRango.Click += new EventHandler(this.kryptonButtonAgregarRango_Click);
      this.kryptonGroupBoxSingle.Enabled = false;
      this.kryptonGroupBoxSingle.Location = new Point(7, 124);
      this.kryptonGroupBoxSingle.Name = "kryptonGroupBoxSingle";
      this.kryptonGroupBoxSingle.Panel.Controls.Add((Control) this.kryptonLabel4);
      this.kryptonGroupBoxSingle.Panel.Controls.Add((Control) this.kryptonTextBoxCode);
      this.kryptonGroupBoxSingle.Panel.Controls.Add((Control) this.kryptonButtonAgregar);
      this.kryptonGroupBoxSingle.Size = new Size(422, 87);
      this.kryptonGroupBoxSingle.TabIndex = 14;
      this.kryptonGroupBoxSingle.Text = "Caption";
      this.kryptonLabel4.Location = new Point(8, 17);
      this.kryptonLabel4.Name = "kryptonLabel4";
      this.kryptonLabel4.Size = new Size(37, 20);
      this.kryptonLabel4.TabIndex = 17;
      this.kryptonLabel4.Values.Text = "Sello";
      this.kryptonTextBoxCode.Location = new Point(54, 17);
      this.kryptonTextBoxCode.Name = "kryptonTextBoxCode";
      this.kryptonTextBoxCode.Size = new Size(161, 20);
      this.kryptonTextBoxCode.TabIndex = 14;
      this.kryptonGroupBoxRange.CaptionStyle = LabelStyle.NormalPanel;
      this.kryptonGroupBoxRange.Enabled = false;
      this.kryptonGroupBoxRange.Location = new Point(7, 217);
      this.kryptonGroupBoxRange.Name = "kryptonGroupBoxRange";
      this.kryptonGroupBoxRange.Panel.Controls.Add((Control) this.kryptonNumericUpDownDesde);
      this.kryptonGroupBoxRange.Panel.Controls.Add((Control) this.kryptonButtonAgregarRango);
      this.kryptonGroupBoxRange.Panel.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonGroupBoxRange.Panel.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonGroupBoxRange.Panel.Controls.Add((Control) this.kryptonNumericUpDownHasta);
      this.kryptonGroupBoxRange.Size = new Size(426, 81);
      this.kryptonGroupBoxRange.TabIndex = 15;
      this.kryptonGroupBoxRange.Text = "Caption";
      this.kryptonLabel1.Location = new Point(11, 28);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(48, 20);
      this.kryptonLabel1.TabIndex = 18;
      this.kryptonLabel1.Values.Text = "Cliente";
      this.userControlSingleSearchCliente.BorderStyle = BorderStyle.FixedSingle;
      this.userControlSingleSearchCliente.Location = new Point(13, 51);
      this.userControlSingleSearchCliente.Name = "userControlSingleSearchCliente";
      this.userControlSingleSearchCliente.NameEntity = "";
      this.userControlSingleSearchCliente.Size = new Size(420, 30);
      this.userControlSingleSearchCliente.TabIndex = 17;
      this.kryptonLabel5.Location = new Point(13, 99);
      this.kryptonLabel5.Name = "kryptonLabel5";
      this.kryptonLabel5.Size = new Size(77, 20);
      this.kryptonLabel5.TabIndex = 19;
      this.kryptonLabel5.Values.Text = "Ultimo Sello";
      this.kryptonLabelLastSello.AutoSize = false;
      this.kryptonLabelLastSello.Location = new Point(97, 99);
      this.kryptonLabelLastSello.Name = "kryptonLabelLastSello";
      this.kryptonLabelLastSello.Size = new Size(332, 25);
      this.kryptonLabelLastSello.TabIndex = 20;
      this.kryptonLabelLastSello.Values.Text = ".";
      this.kryptonCheckBox1.Enabled = false;
      this.kryptonCheckBox1.LabelStyle = LabelStyle.NormalControl;
      this.kryptonCheckBox1.Location = new Point(160, 315);
      this.kryptonCheckBox1.Name = "kryptonCheckBox1";
      this.kryptonCheckBox1.Size = new Size(55, 20);
      this.kryptonCheckBox1.TabIndex = 22;
      this.kryptonCheckBox1.Text = "Editar";
      this.kryptonCheckBox1.Values.Text = "Editar";
      this.kryptonCheckBox1.CheckStateChanged += new EventHandler(this.kryptonCheckBox1_CheckStateChanged);
      this.kryptonLabel6.LabelStyle = LabelStyle.BoldControl;
      this.kryptonLabel6.Location = new Point(13, 313);
      this.kryptonLabel6.Name = "kryptonLabel6";
      this.kryptonLabel6.Size = new Size(39, 20);
      this.kryptonLabel6.TabIndex = 23;
      this.kryptonLabel6.Values.Text = "Lote:";
      this.kryptonNumericUpDownLote.Enabled = false;
      this.kryptonNumericUpDownLote.Location = new Point(52, 313);
      this.kryptonNumericUpDownLote.Name = "kryptonNumericUpDownLote";
      this.kryptonNumericUpDownLote.Size = new Size(104, 22);
      this.kryptonNumericUpDownLote.TabIndex = 24;
      this.kryptonNumericUpDownCodSello.Enabled = false;
      this.kryptonNumericUpDownCodSello.Location = new Point(222, 401);
      this.kryptonNumericUpDownCodSello.Name = "kryptonNumericUpDownCodSello";
      this.kryptonNumericUpDownCodSello.Size = new Size(104, 22);
      this.kryptonNumericUpDownCodSello.TabIndex = 25;
      this.kryptonNumericUpDownCodSello.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlSelloABM";
      this.Size = new Size(440, 381);
      this.Load += new EventHandler(this.UserControlSelloABM_Load);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.kryptonGroupBoxSingle.Panel.ResumeLayout(false);
      this.kryptonGroupBoxSingle.Panel.PerformLayout();
      this.kryptonGroupBoxSingle.EndInit();
      this.kryptonGroupBoxSingle.ResumeLayout(false);
      this.kryptonGroupBoxRange.Panel.ResumeLayout(false);
      this.kryptonGroupBoxRange.Panel.PerformLayout();
      this.kryptonGroupBoxRange.EndInit();
      this.kryptonGroupBoxRange.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void dispachatAgregate()
    {
      if (this.OnAgregateSello == null)
        return;
      this.OnAgregateSello();
    }

    private bool ValidateSello()
    {
      if (this.kryptonNumericUpDownCodSello.Value <= new Decimal(0))
        throw new Exception("El numero de sello debe ser mayor que cero.");
      return this.objSelloWraper.ValidateSelloNotExiste(this.kryptonNumericUpDownCodSello.Value.ToInt());
    }

    private void prepareControls()
    {
      this.objSelloWraper = new SelloWraper();
      this.kryptonGroupBoxRange.Text = "Insertar Rango de Sellos";
      this.kryptonGroupBoxSingle.Text = "Insertar Sellos";
      this.userControlSingleSearchCliente.SetEntity<cliente>((IQueryable<cliente>) this.dbEnt.clientes, cliente.Meta.Columns.NombreCliente, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchCliente.SetMantenimiento((UserControlBase) new UserControlClienteABM((List<SearchResult>) null), "Cliente");
      this.userControlSingleSearchCliente.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchCliente_OnValueChanged);
      this.fillForm();
    }

    private void userControlSingleSearchCliente_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.kryptonCheckBox1.Enabled = this.kryptonGroupBoxRange.Enabled = this.kryptonGroupBoxSingle.Enabled = resultList.Count > 0;
      this.fillForm();
    }

    private void fillForm()
    {
      this.kryptonTextBoxCode.Clear();
      List<SearchResult> keyValues = this.userControlSingleSearchCliente.KeyValues;
      if (keyValues.Count > 0)
        this.kryptonNumericUpDownLote.Value = (Decimal) (this.objSelloWraper.GetCurrentLote(this.getAsInt(keyValues, true)) + 1);
      else
        this.kryptonNumericUpDownLote.Value = new Decimal(0);
    }

    private void UserControlSelloABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void kryptonButtonAgregar_Click(object sender, EventArgs e)
    {
      try
      {
        this.save();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Erro guardando registro", ex);
      }
    }

    private void save()
    {
      this.objSelloWraper = new SelloWraper();
      if (string.IsNullOrEmpty(this.kryptonTextBoxCode.Text))
        throw new Exception("Favor inserte un valor para el sello");
      this.objSelloWraper.SaveSello(this.kryptonTextBoxCode.Text, (int) this.kryptonNumericUpDownLote.Value, this.getAsInt(this.userControlSingleSearchCliente.GetKeyValues(), true), true);
      this.fillForm();
      this.dispachatAgregate();
    }

    private int getAsInt(List<SearchResult> list, bool fireExcepction = true)
    {
      int result = 0;
      if ((list.Count <= 0 || !int.TryParse(list[0].Value.ToString(), out result)) && fireExcepction)
        throw new Exception("Asegurese de llenar el Cliente, Chofer, Camion y Terminal");
      return result;
    }

    private void saveRange()
    {
      if (this.kryptonNumericUpDownDesde.Value <= new Decimal(0))
        throw new Exception(string.Format("El valor de inicio debe ser mayor a cero"));
      if (!this.validateRange())
        throw new Exception("El rango que desea generar no es valido.\r\n        1- asegurese que no contenga sellos existentes\r\n        2- asegurese que 'desde' sea mayor que 'hasta'\r\n        3- asegurese que 'desde' y 'hasta' sean mayor que cero");
      this.objSelloWraper.SaveSelloRange(this.kryptonNumericUpDownDesde.Value, this.kryptonNumericUpDownHasta.Value, (int) this.kryptonNumericUpDownLote.Value, this.getAsInt(this.userControlSingleSearchCliente.GetKeyValues(), true));
      this.fillForm();
      this.dispachatAgregate();
    }

    private bool validateRange()
    {
      return this.objSelloWraper.ValidateRange(new Decimal?(this.kryptonNumericUpDownDesde.Value), new Decimal?(this.kryptonNumericUpDownHasta.Value));
    }

    private void kryptonButtonAgregarRango_Click(object sender, EventArgs e)
    {
      try
      {
        this.saveRange();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error guardando registro", ex);
      }
    }

    private void kryptonButtonCancel_Click(object sender, EventArgs e)
    {
      this.ParentForm.Close();
    }

    private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private void kryptonCheckBox1_CheckStateChanged(object sender, EventArgs e)
    {
      this.kryptonNumericUpDownLote.Enabled = this.kryptonCheckBox1.Checked;
    }

    public delegate void AgregateSello();
  }
}
