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
using System.Linq.Expressions;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class FormClientes : KryptonForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchCliente;
    private cargocareEntities db;

    public FormClientes()
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
      this.userControlGridSearchCliente = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchCliente);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(790, 331);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchCliente.CloseOnSelect = false;
      this.userControlGridSearchCliente.Dock = DockStyle.Fill;
      this.userControlGridSearchCliente.Location = new Point(0, 0);
      this.userControlGridSearchCliente.Name = "userControlGridSearchCliente";
      this.userControlGridSearchCliente.Size = new Size(790, 331);
      this.userControlGridSearchCliente.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(790, 331);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormClientes";
      this.Text = "FormClientes";
      this.Load += new EventHandler(this.FormClientes_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchCliente.SetEntity<cliente>(this.db.clientes.Where<cliente>((Expression<Func<cliente, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchCliente.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearch1_OnAddEntity);
      this.userControlGridSearchCliente.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearch1_OnEditEntity);
      this.userControlGridSearchCliente.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearch1_OnDeleteEntity);
      this.userControlGridSearchCliente.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearch1_OnContentDoubleClick);
    }

    private void userControlGridSearch1_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearch1_OnEditEntity(selectedResult);
    }

    private void userControlGridSearch1_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new ClienteWraper().DeleteCliente(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchCliente.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearch1_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlClienteABM(selectedResult), "Editando Cliente", new Size?());
      this.userControlGridSearchCliente.Refresh();
    }

    private void userControlGridSearch1_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlClienteABM((List<SearchResult>) null), "Insertando Cliente", new Size?());
      this.userControlGridSearchCliente.Refresh();
    }

    private void FormClientes_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }
  }
}
