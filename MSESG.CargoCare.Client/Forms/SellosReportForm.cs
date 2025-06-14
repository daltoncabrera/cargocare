using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class SellosReportForm : KryptonForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private ReportViewer reportViewer1;
    private KryptonPanel kryptonPanel1;
    private KryptonRadioButton kryptonRadioButtonAll;
    private KryptonRadioButton kryptonRadioButtonNotUsed;
    private KryptonRadioButton kryptonRadioButtonUsed;
    private KryptonLabel kryptonLabel1;
    private UserControlSingleSearch userControlSingleSearchCliente;
    private cargocareEntities dbEnt;

    public SellosReportForm()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.kryptonManager = new KryptonManager(this.components);
      this.kryptonPanel = new KryptonPanel();
      this.reportViewer1 = new ReportViewer();
      this.kryptonPanel1 = new KryptonPanel();
      this.kryptonLabel1 = new KryptonLabel();
      this.userControlSingleSearchCliente = new UserControlSingleSearch();
      this.kryptonRadioButtonAll = new KryptonRadioButton();
      this.kryptonRadioButtonNotUsed = new KryptonRadioButton();
      this.kryptonRadioButtonUsed = new KryptonRadioButton();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Controls.Add((Control) this.reportViewer1);
      this.kryptonPanel.Controls.Add((Control) this.kryptonPanel1);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(759, 452);
      this.kryptonPanel.TabIndex = 0;
      this.reportViewer1.Dock = DockStyle.Fill;
      this.reportViewer1.Location = new Point(0, 67);
      this.reportViewer1.Name = "reportViewer1";
      this.reportViewer1.Size = new Size(759, 385);
      this.reportViewer1.TabIndex = 3;
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel1.Controls.Add((Control) this.userControlSingleSearchCliente);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonRadioButtonAll);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonRadioButtonNotUsed);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonRadioButtonUsed);
      this.kryptonPanel1.Dock = DockStyle.Top;
      this.kryptonPanel1.Location = new Point(0, 0);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.Size = new Size(759, 67);
      this.kryptonPanel1.TabIndex = 2;
      this.kryptonLabel1.Location = new Point(23, 26);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(48, 20);
      this.kryptonLabel1.TabIndex = 7;
      this.kryptonLabel1.Values.Text = "Cliente";
      this.userControlSingleSearchCliente.BorderStyle = BorderStyle.FixedSingle;
      this.userControlSingleSearchCliente.Location = new Point(81, 21);
      this.userControlSingleSearchCliente.Name = "userControlSingleSearchCliente";
      this.userControlSingleSearchCliente.NameEntity = "";
      this.userControlSingleSearchCliente.Size = new Size(344, 30);
      this.userControlSingleSearchCliente.TabIndex = 6;
      this.kryptonRadioButtonAll.Checked = true;
      this.kryptonRadioButtonAll.Location = new Point(508, 26);
      this.kryptonRadioButtonAll.Name = "kryptonRadioButtonAll";
      this.kryptonRadioButtonAll.Size = new Size(51, 20);
      this.kryptonRadioButtonAll.TabIndex = 5;
      this.kryptonRadioButtonAll.Values.Text = "Todo";
      this.kryptonRadioButtonAll.Click += new EventHandler(this.kryptonRadioButtonNotUsed_Click);
      this.kryptonRadioButtonNotUsed.Location = new Point(665, 26);
      this.kryptonRadioButtonNotUsed.Name = "kryptonRadioButtonNotUsed";
      this.kryptonRadioButtonNotUsed.Size = new Size(67, 20);
      this.kryptonRadioButtonNotUsed.TabIndex = 4;
      this.kryptonRadioButtonNotUsed.Values.Text = "Sin Usar";
      this.kryptonRadioButtonNotUsed.Click += new EventHandler(this.kryptonRadioButtonNotUsed_Click);
      this.kryptonRadioButtonUsed.Location = new Point(585, 26);
      this.kryptonRadioButtonUsed.Name = "kryptonRadioButtonUsed";
      this.kryptonRadioButtonUsed.Size = new Size(57, 20);
      this.kryptonRadioButtonUsed.TabIndex = 3;
      this.kryptonRadioButtonUsed.Values.Text = "Usado";
      this.kryptonRadioButtonUsed.Click += new EventHandler(this.kryptonRadioButtonNotUsed_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(759, 452);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "SellosReportForm";
      this.Text = "SellosReportForm";
      this.Load += new EventHandler(this.SellosReportForm_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    private void SellosReportForm_Load(object sender, EventArgs e)
    {
      this.dbEnt = new cargocareEntities();
      this.prepareControls();
    }

    private void prepareControls()
    {
      this.userControlSingleSearchCliente.SetEntity<cliente>(this.dbEnt.clientes.Where<cliente>((Expression<Func<cliente, bool>>) (s => s.IdStatus != (int?) -1)), cliente.Meta.Columns.NombreCliente, (List<ColumnModel>) null, (List<ColumnFilter>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlSingleSearchCliente.SetMantenimiento((UserControlBase) new UserControlClienteABM((List<SearchResult>) null), "Cliente");
      this.userControlSingleSearchCliente.OnValueChanged += new UserControlSingleSearch.ValueChanged(this.userControlSingleSearchCliente_OnValueChanged);
    }

    private void userControlSingleSearchCliente_OnValueChanged(List<SearchResult> resultList, string displayValue)
    {
      this.fillReport();
    }

    private void fillReport()
    {
      string str = "ReporteSellos.rdlc";
      this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", this.GetDataTable()));
      this.reportViewer1.LocalReport.ReportPath = str;
      this.reportViewer1.ProcessingMode = ProcessingMode.Local;
      this.reportViewer1.RefreshReport();
    }

    private DataTable GetDataTable()
    {
      string str = " SELECT s.CodSello, \r\n                                   s.IdCliente,\r\n                                   c.NombreCliente,\r\n                                   s.idInspeccion,\r\n                                   s.IdCompartimento,\r\n                                   s.Ubicacion,       \r\n                                   s.CreatedDate,\r\n                                   us2.Nombre usuarioCreo,\r\n                                   i.CreatedDate as usedDate,\r\n                                   us.Nombre  usuarioUso       \r\n                             FROM sello s                           \r\n                            left join inspeccion i on s.IdInspeccion = i.IdInspeccion \r\n                            left join cliente c on s.IdCliente = c.IdCliente\r\n                            left join user us on i.CreatedUser = us.idUser\r\n                            left join user us2 on s.CreatedUser = us2.idUser" + string.Format(" where s.IdCliente = {0} ", (object) GUtils.GetAsInt(this.userControlSingleSearchCliente.GetKeyValues(), true));
      if (this.kryptonRadioButtonUsed.Checked)
        str += string.Format(" and s.IdInspeccion is not null ");
      if (this.kryptonRadioButtonNotUsed.Checked)
        str += string.Format(" and s.IdInspeccion is null ");
      DataTable dataTable = new DataTable("DataSet1");
      using (MySqlConnection mySqlConnection = new MySqlConnection("Server=localhost;Database=cargocare;Uid=root;Pwd=;"))
      {
        using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter())
        {
          MySqlCommand mySqlCommand = new MySqlCommand();
          mySqlCommand.Connection = mySqlConnection;
          mySqlCommand.CommandText = str;
          mySqlCommand.CommandType = CommandType.Text;
          mySqlDataAdapter.SelectCommand = mySqlCommand;
          mySqlDataAdapter.Fill(dataTable);
        }
      }
      return dataTable;
    }

    private void kryptonRadioButtonNotUsed_Click(object sender, EventArgs e)
    {
      this.fillReport();
    }
  }
}
