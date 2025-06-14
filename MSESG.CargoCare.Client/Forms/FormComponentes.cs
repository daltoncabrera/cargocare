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
  public class FormComponentes : KryptonForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private UserControlGridSearch userControlGridSearchComponent;
    private cargocareEntities db;

    public FormComponentes()
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
      this.userControlGridSearchComponent = new UserControlGridSearch();
      this.SuspendLayout();
      this.userControlGridSearchComponent.CloseOnSelect = false;
      this.userControlGridSearchComponent.Dock = DockStyle.Fill;
      this.userControlGridSearchComponent.Location = new Point(0, 0);
      this.userControlGridSearchComponent.Name = "userControlGridSearchComponent";
      this.userControlGridSearchComponent.Size = new Size(751, 412);
      this.userControlGridSearchComponent.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(751, 412);
      this.Controls.Add((Control) this.userControlGridSearchComponent);
      this.Name = "FormComponentes";
      this.Text = "Componentes";
      this.Load += new EventHandler(this.FormComponentes_Load);
      this.ResumeLayout(false);
    }

    private void FormComponentes_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchComponent.SetEntity<componente>(this.db.componentes.Where<componente>((Expression<Func<componente, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchComponent.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchComponent_OnAddEntity);
      this.userControlGridSearchComponent.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchComponent_OnEditEntity);
      this.userControlGridSearchComponent.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchComponent_OnDeleteEntity);
      this.userControlGridSearchComponent.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchComponent_OnContentDoubleClick);
    }

    private void userControlGridSearchComponent_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchComponent_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchComponent_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new ComponentWraper().DeleteComponente(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchComponent.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchComponent_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlComponetABM(selectedResult), "Editando Componente", new Size?());
      this.userControlGridSearchComponent.Refresh();
    }

    private void userControlGridSearchComponent_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlComponetABM((List<SearchResult>) null), "Insertando Componente", new Size?());
    }
  }
}
