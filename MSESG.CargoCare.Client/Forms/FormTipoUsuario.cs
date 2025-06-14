using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class FormTipoUsuario : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchTipoUsurio;

    public FormTipoUsuario()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchTipoUsurio.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchTipoUsurio_OnAddEntity);
      this.userControlGridSearchTipoUsurio.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchTipoUsurio_OnEditEntity);
      this.userControlGridSearchTipoUsurio.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchTipoUsurio_OnDeleteEntity);
      this.userControlGridSearchTipoUsurio.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchTipoUsurio_OnContentDoubleClick);
    }

    private void userControlGridSearchTipoUsurio_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchTipoUsurio_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchTipoUsurio_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      throw new NotImplementedException();
    }

    private void userControlGridSearchTipoUsurio_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlTipoUsuarioABM(selectedResult), "Editando Tipo Usuario", new Size?());
      this.userControlGridSearchTipoUsurio.Refresh();
    }

    private void userControlGridSearchTipoUsurio_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlTipoUsuarioABM((List<SearchResult>) null), "Insertando Tipo Usuario", new Size?());
      this.userControlGridSearchTipoUsurio.Refresh();
    }

    private void FormTipoUsuario_Load(object sender, EventArgs e)
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
      this.userControlGridSearchTipoUsurio = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchTipoUsurio);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(578, 339);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchTipoUsurio.CloseOnSelect = false;
      this.userControlGridSearchTipoUsurio.Dock = DockStyle.Fill;
      this.userControlGridSearchTipoUsurio.Location = new Point(0, 0);
      this.userControlGridSearchTipoUsurio.Name = "userControlGridSearchTipoUsurio";
      this.userControlGridSearchTipoUsurio.Size = new Size(578, 339);
      this.userControlGridSearchTipoUsurio.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(578, 339);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormTipoUsuario";
      this.Text = "FormTipoUsuario";
      this.Load += new EventHandler(this.FormTipoUsuario_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
