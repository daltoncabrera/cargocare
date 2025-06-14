using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class InspeccionForm : KryptonForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private ReportViewer reportViewer1;
    private KryptonPanel kryptonPanel1;
    private KryptonLabel kryptonLabel1;
    private UserControlSingleSearch userControlSingleSearchCliente;
    private cargocareEntities dbEnt;

    public InspeccionForm()
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
      this.kryptonPanel1 = new KryptonPanel();
      this.reportViewer1 = new ReportViewer();
      this.kryptonLabel1 = new KryptonLabel();
      this.userControlSingleSearchCliente = new UserControlSingleSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.reportViewer1);
      this.kryptonPanel.Controls.Add((Control) this.kryptonPanel1);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(692, 450);
      this.kryptonPanel.TabIndex = 0;
      this.kryptonPanel1.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel1.Controls.Add((Control) this.userControlSingleSearchCliente);
      this.kryptonPanel1.Dock = DockStyle.Top;
      this.kryptonPanel1.Location = new Point(0, 0);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.Size = new Size(692, 67);
      this.kryptonPanel1.TabIndex = 0;
      this.reportViewer1.Dock = DockStyle.Fill;
      this.reportViewer1.Location = new Point(0, 67);
      this.reportViewer1.Name = "reportViewer1";
      this.reportViewer1.Size = new Size(692, 383);
      this.reportViewer1.TabIndex = 1;
      this.kryptonLabel1.Location = new Point(14, 27);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(48, 20);
      this.kryptonLabel1.TabIndex = 9;
      this.kryptonLabel1.Values.Text = "Cliente";
      this.userControlSingleSearchCliente.BorderStyle = BorderStyle.FixedSingle;
      this.userControlSingleSearchCliente.Location = new Point(72, 22);
      this.userControlSingleSearchCliente.Name = "userControlSingleSearchCliente";
      this.userControlSingleSearchCliente.NameEntity = "";
      this.userControlSingleSearchCliente.Size = new Size(344, 30);
      this.userControlSingleSearchCliente.TabIndex = 8;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(692, 450);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "InspeccionForm";
      this.Text = "InspeccionForm";
      this.Load += new EventHandler(this.InspeccionForm_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    private void InspeccionForm_Load(object sender, EventArgs e)
    {
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
    }
  }
}
