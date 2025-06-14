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
  public class FormItemsVerificacion : KryptonForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchItemsVerificacion;
    private cargocareEntities db;

    public FormItemsVerificacion()
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
      this.userControlGridSearchItemsVerificacion = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchItemsVerificacion);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(653, 429);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchItemsVerificacion.CloseOnSelect = false;
      this.userControlGridSearchItemsVerificacion.Dock = DockStyle.Fill;
      this.userControlGridSearchItemsVerificacion.Location = new Point(0, 0);
      this.userControlGridSearchItemsVerificacion.Name = "userControlGridSearchItemsVerificacion";
      this.userControlGridSearchItemsVerificacion.Size = new Size(653, 429);
      this.userControlGridSearchItemsVerificacion.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(653, 429);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormItemsVerificacion";
      this.Text = "FormProducts";
      this.Load += new EventHandler(this.FormItemsVerificacion_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchItemsVerificacion.SetEntity<itemsverificacion>(this.db.itemsverificacions.Where<itemsverificacion>((Expression<Func<itemsverificacion, bool>>) (s => s.IdStatus != (int?) -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchItemsVerificacion.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchItemsVerificacion_OnAddEntity);
      this.userControlGridSearchItemsVerificacion.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchItemsVerificacion_OnEditEntity);
      this.userControlGridSearchItemsVerificacion.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchItemsVerificacion_OnDeleteEntity);
      this.userControlGridSearchItemsVerificacion.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchItemsVerificacion_OnContentDoubleClick);
    }

    private void userControlGridSearchItemsVerificacion_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new ItemsVerificacionWraper().DeleteItemVerification(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchItemsVerificacion.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchItemsVerificacion_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      throw new NotImplementedException();
    }

    private void userControlGridSearchItemsVerificacion_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlItemsVerificacionABM(selectedResult), "Editando Item de Verificacion", new Size?());
      this.userControlGridSearchItemsVerificacion.Refresh();
    }

    private void userControlGridSearchItemsVerificacion_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlItemsVerificacionABM((List<SearchResult>) null), "Editando Item de Verificacion", new Size?());
      this.userControlGridSearchItemsVerificacion.Refresh();
    }

    private void FormItemsVerificacion_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }
  }
}
