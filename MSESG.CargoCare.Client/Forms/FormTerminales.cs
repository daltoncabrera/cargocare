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
  public class FormTerminales : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchTerminales;

    public FormTerminales()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchTerminales.SetEntity<terminal>(this.db.terminals.Where<terminal>((Expression<Func<terminal, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchTerminales.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchTerminales_OnAddEntity);
      this.userControlGridSearchTerminales.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchTerminales_OnEditEntity);
      this.userControlGridSearchTerminales.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchTerminales_OnDeleteEntity);
      this.userControlGridSearchTerminales.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchTerminales_OnContentDoubleClick);
    }

    private void userControlGridSearchTerminales_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchTerminales_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchTerminales_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new TerminalWraper().DeleteTerminal(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchTerminales.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchTerminales_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlTerminalABM(selectedResult), "Editando Terminal", new Size?());
      this.userControlGridSearchTerminales.Refresh();
    }

    private void userControlGridSearchTerminales_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlTerminalABM((List<SearchResult>) null), "Insertando Terminal", new Size?());
      this.userControlGridSearchTerminales.Refresh();
    }

    private void FormProducts_Load(object sender, EventArgs e)
    {
      this.prepareControls();
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
      this.userControlGridSearchTerminales = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchTerminales);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(653, 429);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchTerminales.CloseOnSelect = false;
      this.userControlGridSearchTerminales.Dock = DockStyle.Fill;
      this.userControlGridSearchTerminales.Location = new Point(0, 0);
      this.userControlGridSearchTerminales.Name = "userControlGridSearchProduct";
      this.userControlGridSearchTerminales.Size = new Size(653, 429);
      this.userControlGridSearchTerminales.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(653, 429);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormProducts";
      this.Text = "FormProducts";
      this.Load += new EventHandler(this.FormProducts_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
