using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  public partial class UserControlGridSearch 
  {
    private int recordPerPage = 50;
    private Size? mantenimientoSize = new Size?();
    private const int RECORDS_PER_PAGE = 50;
   
    private List<ColumnModel> keyList;
    private List<ColumnFilter> FilterStatic;
    private UserControlBase controlMaintenimiento;
    private GenericSearch objGenericSearch;
    private bool closeOnSelect;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GenericSearch SearchObject
    {
      get
      {
        return this.objGenericSearch;
      }
      set
      {
        if (value == null)
          return;
        this.objGenericSearch = value;
        this.keyList = this.objGenericSearch.KeyList;
        this.userControlConditionLine1.Fields = this.objGenericSearch.Columns;
        this.filter(true);
      }
    }

    public bool CloseOnSelect
    {
      get
      {
        return this.closeOnSelect;
      }
      set
      {
        this.closeOnSelect = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<ColumnModel> KeyList
    {
      get
      {
        return this.keyList;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RecordPerPage
    {
      get
      {
        return this.recordPerPage;
      }
      set
      {
        this.recordPerPage = value > 0 ? value : 50;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<SearchResult> SelectedValue
    {
      get
      {
        return this.getSelectedValue();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataGridView GridContainer
    {
      get
      {
        return (DataGridView) this.dataGridViewContent;
      }
    }

    public event UserControlGridSearch.SelectionChanged OnSelectionChanged;

    public event UserControlGridSearch.ContentDoubleClick OnContentDoubleClick;

    public event UserControlGridSearch.EditEntity OnEditEntity;

    public event UserControlGridSearch.AddEntity OnAddEntity;

    public event UserControlGridSearch.DeleteEntity OnDeleteEntity;

    public UserControlGridSearch(GenericSearch objSearch)
      : this()
    {
      this.objGenericSearch = objSearch;
    }

    public UserControlGridSearch()
    {
      this.InitializeComponent();
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.userControlConditionLine1 = new DCM.Core.UI.UserControlConditionLine();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.buttonFind = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonRefresh = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonEdit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonAdd = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dataGridViewContent = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.userControlPaginator1 = new DCM.Core.UI.UserControlPaginator();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.userControlConditionLine1);
            this.panel1.Controls.Add(this.kryptonPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 29);
            this.panel1.TabIndex = 6;
            // 
            // userControlConditionLine1
            // 
            this.userControlConditionLine1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlConditionLine1.Fields = null;
            this.userControlConditionLine1.Location = new System.Drawing.Point(0, 0);
            this.userControlConditionLine1.Name = "userControlConditionLine1";
            this.userControlConditionLine1.Size = new System.Drawing.Size(482, 29);
            this.userControlConditionLine1.TabIndex = 5;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.buttonFind);
            this.kryptonPanel1.Controls.Add(this.buttonClear);
            this.kryptonPanel1.Controls.Add(this.buttonRefresh);
            this.kryptonPanel1.Controls.Add(this.buttonEdit);
            this.kryptonPanel1.Controls.Add(this.buttonAdd);
            this.kryptonPanel1.Controls.Add(this.buttonDelete);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel1.Location = new System.Drawing.Point(482, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(195, 29);
            this.kryptonPanel1.TabIndex = 7;
            // 
            // buttonFind
            // 
            this.buttonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFind.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonFind.Location = new System.Drawing.Point(3, 0);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(32, 29);
            this.buttonFind.TabIndex = 9;
            this.buttonFind.Tag = "";
            this.toolTip1.SetToolTip(this.buttonFind, "Aplicar filtros");
            this.buttonFind.Values.Image = global::DCM.Core.UI.Properties.Resources._1336071055_search;
            this.buttonFind.Values.Text = "";
            this.buttonFind.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClear.Location = new System.Drawing.Point(35, 0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(32, 29);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Tag = "";
            this.toolTip1.SetToolTip(this.buttonClear, "Limpiar filtros");
            this.buttonClear.Values.Image = global::DCM.Core.UI.Properties.Resources._1336071103_clear_left;
            this.buttonClear.Values.Text = "";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonRefresh.Location = new System.Drawing.Point(67, 0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(32, 29);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Tag = "";
            this.toolTip1.SetToolTip(this.buttonRefresh, "Refrescar datos");
            this.buttonRefresh.Values.Image = global::DCM.Core.UI.Properties.Resources._1336140773_reload_all_tabs;
            this.buttonRefresh.Values.Text = "";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonEdit.Location = new System.Drawing.Point(99, 0);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(32, 29);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.Tag = "";
            this.toolTip1.SetToolTip(this.buttonEdit, "Editar registro");
            this.buttonEdit.Values.Image = global::DCM.Core.UI.Properties.Resources._1336071944_database_edit;
            this.buttonEdit.Values.Text = "";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonAdd.Location = new System.Drawing.Point(131, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(32, 29);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Tag = "";
            this.toolTip1.SetToolTip(this.buttonAdd, "Agregar registro");
            this.buttonAdd.Values.Image = global::DCM.Core.UI.Properties.Resources._1336071423_database_add;
            this.buttonAdd.Values.Text = "";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDelete.Location = new System.Drawing.Point(163, 0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(32, 29);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Tag = "";
            this.toolTip1.SetToolTip(this.buttonDelete, "Eliminar Record");
            this.buttonDelete.Values.Image = global::DCM.Core.UI.Properties.Resources._1336141510_database_delete;
            this.buttonDelete.Values.Text = "";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataGridViewContent
            // 
            this.dataGridViewContent.AllowUserToAddRows = false;
            this.dataGridViewContent.AllowUserToDeleteRows = false;
            this.dataGridViewContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewContent.Location = new System.Drawing.Point(0, 29);
            this.dataGridViewContent.MultiSelect = false;
            this.dataGridViewContent.Name = "dataGridViewContent";
            this.dataGridViewContent.ReadOnly = true;
            this.dataGridViewContent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewContent.Size = new System.Drawing.Size(677, 240);
            this.dataGridViewContent.TabIndex = 8;
            this.dataGridViewContent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewContent_CellDoubleClick);
            // 
            // userControlPaginator1
            // 
            this.userControlPaginator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userControlPaginator1.Location = new System.Drawing.Point(0, 269);
            this.userControlPaginator1.Name = "userControlPaginator1";
            this.userControlPaginator1.Size = new System.Drawing.Size(677, 39);
            this.userControlPaginator1.TabIndex = 7;
            // 
            // UserControlGridSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewContent);
            this.Controls.Add(this.userControlPaginator1);
            this.Controls.Add(this.panel1);
            this.Name = "UserControlGridSearch";
            this.Size = new System.Drawing.Size(677, 308);
            this.Load += new System.EventHandler(this.UserControlLQSearch_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContent)).EndInit();
            this.ResumeLayout(false);

    }

    public void SetMantenimiento(UserControlBase controlMantenimiento)
    {
      this.controlMaintenimiento = controlMantenimiento;
      this.mantenimientoSize = new Size?(this.controlMaintenimiento.Size);
    }

    private List<SearchResult> getSelectedValue()
    {
      List<SearchResult> searchResultList = (List<SearchResult>) null;
      if (this.dataGridViewContent.SelectedRows.Count > 0)
      {
        searchResultList = new List<SearchResult>();
        foreach (ColumnModel key in this.keyList)
        {
          object obj = this.dataGridViewContent.SelectedRows[0].Cells[key.Nombre].Value;
          searchResultList.Add(new SearchResult(key, obj));
        }
      }
      return searchResultList;
    }

    public void SetEntity<T>(IQueryable<T> query, List<ColumnFilter> filterStatic = null, List<ColumnModel> columns = null, List<ColumnModel> keyList = null, ColumnModel orderBy = null)
    {
      this.objGenericSearch = new GenericSearch();
      this.objGenericSearch.SetEntity<T>(query, filterStatic, columns, keyList, orderBy);
      this.FilterStatic = filterStatic;
      this.keyList = this.objGenericSearch.KeyList;
      this.userControlConditionLine1.Fields = this.objGenericSearch.Columns;
      this.filter(true);
    }

    private void prepareControls()
    {
      this.userControlPaginator1.OnChangePage += new UserControlPaginator.ChangePage(this.userControlPaginator1_OnChangePage);
    }

    private void userControlPaginator1_OnChangePage(int page)
    {
      this.updateGrid(page);
    }

    private void formatGrid()
    {
    }

    private void UserControlLQSearch_Load(object sender, EventArgs e)
    {
    }

    private void buttonFilter_Click(object sender, EventArgs e)
    {
      this.filter(false);
    }

    private void filter(bool noFilter = false)
    {
      ColumnFilter filter1 = this.userControlConditionLine1.GetFilter();
      List<ColumnFilter> filter2 = new List<ColumnFilter>();
      if (filter1 != null && !noFilter)
        filter2.Add(filter1);
      this.userControlPaginator1.TotalRecords = this.objGenericSearch.SetFilter(filter2);
      this.updateGrid(1);
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      this.clearFilter();
    }

    private void clearFilter()
    {
      this.userControlPaginator1.TotalRecords = this.objGenericSearch.ClearFilter();
      this.updateGrid(1);
    }

    public new void Refresh()
    {
      this.updateGrid(this.userControlPaginator1.CurrentPage);
    }

    private void updateGrid(int page = 1)
    {
      page = page > 0 ? page : 1;
      this.dataGridViewContent.DataSource = (object) this.objGenericSearch.GetResult(page);
      this.formatGrid();
    }

    private void dataGridViewContent_SelectionChanged(object sender, EventArgs e)
    {
      if (this.OnSelectionChanged == null)
        return;
      this.OnSelectionChanged(this.getSelectedValue());
    }

    private void dataGridViewContent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.OnContentDoubleClick != null)
        this.OnContentDoubleClick(this.getSelectedValue());
      if (!this.closeOnSelect)
        return;
      this.ParentForm.Close();
    }

    private void buttonRefresh_Click(object sender, EventArgs e)
    {
      this.Refresh();
    }

    private void buttonAdd_Click(object sender, EventArgs e)
    {
      if (this.OnAddEntity == null)
        return;
      this.OnAddEntity();
    }

    private void buttonDelete_Click(object sender, EventArgs e)
    {
      if (this.OnDeleteEntity == null)
        return;
      this.OnDeleteEntity(this.SelectedValue);
    }

    private void buttonEdit_Click(object sender, EventArgs e)
    {
      if (this.OnEditEntity == null)
        return;
      this.OnEditEntity(this.SelectedValue);
    }

    public delegate void SelectionChanged(List<SearchResult> selectedResult);

    public delegate void ContentDoubleClick(List<SearchResult> selectedResult);

    public delegate void EditEntity(List<SearchResult> selectedResult);

    public delegate void DeleteEntity(List<SearchResult> selectedResult);

    public delegate void AddEntity();
  }
}
