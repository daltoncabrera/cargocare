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
  public class FormDespacho : KryptonForm
  {
    private const int TIPO_INSPECCION_DESPACHO = 1;
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchDespacho;

    public FormDespacho()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchDespacho.SetEntity<inspeccionview>(this.db.inspeccionviews.Where<inspeccionview>((Expression<Func<inspeccionview, bool>>) (s => s.IdStatus != (int?) -1 && s.IdTipoInspeccion == (int?) 1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchDespacho.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchDespacho_OnAddEntity);
      this.userControlGridSearchDespacho.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchDespacho_OnEditEntity);
      this.userControlGridSearchDespacho.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchDespacho_OnDeleteEntity);
      this.userControlGridSearchDespacho.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchDespacho_OnContentDoubleClick);
    }

    private void userControlGridSearchDespacho_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchDespacho_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchDespacho_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new DespachoWraper().DeleteDespacho(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchDespacho.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchDespacho_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlDespacho(selectedResult), "Editando Inspeccion", new Size?());
      this.userControlGridSearchDespacho.Refresh();
    }

    private void userControlGridSearchDespacho_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlDespacho((List<SearchResult>) null), "Nueva Inspeccion", new Size?());
      this.userControlGridSearchDespacho.Refresh();
    }

    private void FormCarga_Load(object sender, EventArgs e)
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
      this.userControlGridSearchDespacho = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchDespacho);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(672, 451);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchDespacho.CloseOnSelect = false;
      this.userControlGridSearchDespacho.Dock = DockStyle.Fill;
      this.userControlGridSearchDespacho.Location = new Point(0, 0);
      this.userControlGridSearchDespacho.Name = "userControlGridSearchDespacho";
      this.userControlGridSearchDespacho.Size = new Size(672, 451);
      this.userControlGridSearchDespacho.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(672, 451);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormDespacho";
      this.Text = "Despacho de Carga";
      this.Load += new EventHandler(this.FormCarga_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
