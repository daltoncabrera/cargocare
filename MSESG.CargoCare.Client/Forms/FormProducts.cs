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
  public class FormProducts : KryptonForm
  {
    private cargocareEntities db;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private UserControlGridSearch userControlGridSearchProduct;

    public FormProducts()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.userControlGridSearchProduct.SetEntity<producto>(this.db.productoes.Where<producto>((Expression<Func<producto, bool>>) (s => s.IdStatus != -1)), (List<ColumnFilter>) null, (List<ColumnModel>) null, (List<ColumnModel>) null, (ColumnModel) null);
      this.userControlGridSearchProduct.OnAddEntity += new UserControlGridSearch.AddEntity(this.userControlGridSearchProduct_OnAddEntity);
      this.userControlGridSearchProduct.OnEditEntity += new UserControlGridSearch.EditEntity(this.userControlGridSearchProduct_OnEditEntity);
      this.userControlGridSearchProduct.OnDeleteEntity += new UserControlGridSearch.DeleteEntity(this.userControlGridSearchProduct_OnDeleteEntity);
      this.userControlGridSearchProduct.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.userControlGridSearchProduct_OnContentDoubleClick);
    }

    private void userControlGridSearchProduct_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      this.userControlGridSearchProduct_OnEditEntity(selectedResult);
    }

    private void userControlGridSearchProduct_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      if (GUtils.ConfirmDgl("Seguro que desea eliminar el registro") != DialogResult.Yes)
        return;
      try
      {
        new ProductWraper().DeleteProducto(selectedResult);
        GUtils.ShowMessage("Registro Eliminado");
        this.userControlGridSearchProduct.Refresh();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error eliminando retistro", ex);
      }
    }

    private void userControlGridSearchProduct_OnEditEntity(List<SearchResult> selectedResult)
    {
      GUtils.ShowControl((Control) new UserControlProductABM(selectedResult), "Editando Producto", new Size?());
      this.userControlGridSearchProduct.Refresh();
    }

    private void userControlGridSearchProduct_OnAddEntity()
    {
      GUtils.ShowControl((Control) new UserControlProductABM((List<SearchResult>) null), "Insertando Producto", new Size?());
      this.userControlGridSearchProduct.Refresh();
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
      this.userControlGridSearchProduct = new UserControlGridSearch();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.userControlGridSearchProduct);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(653, 429);
      this.kryptonPanel.TabIndex = 0;
      this.userControlGridSearchProduct.CloseOnSelect = false;
      this.userControlGridSearchProduct.Dock = DockStyle.Fill;
      this.userControlGridSearchProduct.Location = new Point(0, 0);
      this.userControlGridSearchProduct.Name = "userControlGridSearchProduct";
      this.userControlGridSearchProduct.Size = new Size(653, 429);
      this.userControlGridSearchProduct.TabIndex = 0;
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
