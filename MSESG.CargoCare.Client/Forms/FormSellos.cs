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
  public class FormSellos : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchSello;

    public FormSellos()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchSello.SetEntity<sello>(this.db.selloes.Where<sello>((Expression<Func<sello, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchSello.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchSello_OnAddEntity);
      this.userControlGridSearchSello.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchSello_OnDeleteEntity);
    }

    private void userControlGridSearchSello_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchSello_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchSello_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new SelloWraper().DeleteSello(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchSello.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchSello_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlSelloABM(selectedResult), "Editando Sello", new Size?());
      this.userControlGridSearchSello.Refresh();
    }

    private void userControlGridSearchSello_OnAddEntity()
    {
      UserControlSelloABM userControlSelloAbm = new UserControlSelloABM((List<SearchResult>) null);
      userControlSelloAbm.OnAgregateSello += new UserControlSelloABM.AgregateSello(this.objSelloABM_OnAgregateSello);
      GUtils.ShowControl((Control) userControlSelloAbm, "Insertando Sello", new Size?());
      this.userControlGridSearchSello.Refresh();
    }

    private void objSelloABM_OnAgregateSello()
    {
      this.userControlGridSearchSello.Refresh();
    }

    private void FormSellos_Load(object sender, EventArgs e)
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
      this.userControlGridSearchSello = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchSello);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(704, 467);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchSello.CloseOnSelect = false;
      this.userControlGridSearchSello.Dock = DockStyle.Fill;
      this.userControlGridSearchSello.Location = new Point(0, 0);
      this.userControlGridSearchSello.Name = "userControlGridSearchSello";
      this.userControlGridSearchSello.Size = new Size(704, 467);
      this.userControlGridSearchSello.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(704, 467);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormSellos";
      this.Text = "FormSellos";
      this.Load += new EventHandler(this.FormSellos_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
