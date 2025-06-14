using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class FormSystem : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchSystem;

    public FormSystem()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchSystem.SetEntity<systemvar>((IQueryable<systemvar>) this.db.systemvars, (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchSystem.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchSystem_OnAddEntity);
      this.userControlGridSearchSystem.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchSystem_OnEditEntity);
      this.userControlGridSearchSystem.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchSystem_OnDeleteEntity);
      this.userControlGridSearchSystem.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchSystem_OnContentDoubleClick);
    }

    private void userControlGridSearchSystem_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchSystem_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchSystem_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      throw new NotImplementedException();
    }

    private void userControlGridSearchSystem_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlSystemVarsABM(selectedResult), "Editando Variable del Sistema", new Size?());
      this.userControlGridSearchSystem.Refresh();
    }

    private void userControlGridSearchSystem_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlSystemVarsABM((List<SearchResult>) null), "Insertando Variable del Sistema", new Size?());
      this.userControlGridSearchSystem.Refresh();
    }

    private void FormSystem_Load(object sender, EventArgs e)
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
      this.userControlGridSearchSystem = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchSystem);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(604, 403);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchSystem.CloseOnSelect = false;
      this.userControlGridSearchSystem.Dock = DockStyle.Fill;
      this.userControlGridSearchSystem.Location = new Point(0, 0);
      this.userControlGridSearchSystem.Name = "userControlGridSearchSystem";
      this.userControlGridSearchSystem.Size = new Size(604, 403);
      this.userControlGridSearchSystem.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(604, 403);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormSystem";
      this.Text = "FormSystem";
      this.Load += new EventHandler(this.FormSystem_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
