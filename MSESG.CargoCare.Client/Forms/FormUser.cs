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
  public class FormUser : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchUser;

    public FormUser()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchUser.SetEntity<user>(this.db.users.Where<user>((Expression<Func<user, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchUser.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchUser_OnAddEntity);
      this.userControlGridSearchUser.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchUser_OnEditEntity);
      this.userControlGridSearchUser.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchUser_OnDeleteEntity);
      this.userControlGridSearchUser.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchUser_OnContentDoubleClick);
    }

    private void userControlGridSearchUser_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchUser_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchUser_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new UserWraper().DeleteUser(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchUser.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchUser_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlUserABM(selectedResult), "Editando Usuario", new Size?());
      this.userControlGridSearchUser.Refresh();
    }

    private void userControlGridSearchUser_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlUserABM((List<SearchResult>) null), "Insertando Usuario", new Size?());
      this.userControlGridSearchUser.Refresh();
    }

    private void FormUser_Load(object sender, EventArgs e)
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
      this.userControlGridSearchUser = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchUser);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(746, 414);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchUser.CloseOnSelect = false;
      this.userControlGridSearchUser.Dock = DockStyle.Fill;
      this.userControlGridSearchUser.Location = new Point(0, 0);
      this.userControlGridSearchUser.Name = "userControlGridSearchUser";
      this.userControlGridSearchUser.Size = new Size(746, 414);
      this.userControlGridSearchUser.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(746, 414);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormUser";
      this.Text = "FormUser";
      this.Load += new EventHandler(this.FormUser_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
