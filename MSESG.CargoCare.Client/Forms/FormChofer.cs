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
  public class FormChofer : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchChofer;

    public FormChofer()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchChofer.SetEntity<chofer>(this.db.chofers.Where<chofer>((Expression<Func<chofer, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchChofer.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchChofer_OnAddEntity);
      this.userControlGridSearchChofer.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchChofer_OnEditEntity);
      this.userControlGridSearchChofer.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchChofer_OnDeleteEntity);
      this.userControlGridSearchChofer.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchChofer_OnContentDoubleClick);
    }

    private void userControlGridSearchChofer_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchChofer_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchChofer_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new ChoferWraper().DeleteChofer(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchChofer.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchChofer_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlChoferABM(selectedResult), "Editando Chofer", new Size?());
      this.userControlGridSearchChofer.Refresh();
    }

    private void userControlGridSearchChofer_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlChoferABM((List<SearchResult>) null), "Insertando Chofer", new Size?());
      this.userControlGridSearchChofer.Refresh();
    }

    private void FormChofer_Load(object sender, EventArgs e)
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
      this.userControlGridSearchChofer = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchChofer);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(750, 381);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchChofer.CloseOnSelect = false;
      this.userControlGridSearchChofer.Dock = DockStyle.Fill;
      this.userControlGridSearchChofer.Location = new Point(0, 0);
      this.userControlGridSearchChofer.Name = "userControlGridSearchChofer";
      this.userControlGridSearchChofer.Size = new Size(750, 381);
      this.userControlGridSearchChofer.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(750, 381);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormChofer";
      this.Text = "FormChofer";
      this.Load += new EventHandler(this.FormChofer_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
