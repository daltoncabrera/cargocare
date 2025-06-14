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
  public class FormCamion : KryptonForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchCamion;
    private cargocareEntities db;

    public FormCamion()
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
      this.userControlGridSearchCamion = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchCamion);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(783, 348);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchCamion.CloseOnSelect = false;
      this.userControlGridSearchCamion.Dock = DockStyle.Fill;
      this.userControlGridSearchCamion.Location = new Point(0, 0);
      this.userControlGridSearchCamion.Name = "userControlGridSearchCamion";
      this.userControlGridSearchCamion.Size = new Size(783, 348);
      this.userControlGridSearchCamion.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(783, 348);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormCamion";
      this.Text = "FormCamion";
      this.Load += new EventHandler(this.FormCamion_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchCamion.SetEntity<camion>(this.db.camions.Where<camion>((Expression<Func<camion, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchCamion.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchCamion_OnAddEntity);
      this.userControlGridSearchCamion.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchCamion_OnEditEntity);
      this.userControlGridSearchCamion.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchCamion_OnDeleteEntity);
      this.userControlGridSearchCamion.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchCamion_OnContentDoubleClick);
    }

    private void userControlGridSearchCamion_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchCamion_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchCamion_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new CamionWraper().DeleteCamion(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchCamion.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchCamion_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlCamionABM(selectedResult), "Editando Camion", new Size?());
      this.userControlGridSearchCamion.Refresh();
    }

    private void userControlGridSearchCamion_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlCamionABM((List<SearchResult>) null), "Insertando Camion", new Size?());
      this.userControlGridSearchCamion.Refresh();
    }

    private void FormCamion_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }
  }
}
