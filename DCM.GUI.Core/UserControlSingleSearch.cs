using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DCM.Core.UI
{
  public partial class UserControlSingleSearch 
  {
    private string nameEntity = string.Empty;
    private Size? searchSize = new Size?();
    private Size? mantenimientoSize = new Size?();
  

    public string NameEntity
    {
      get
      {
        return this.nameEntity;
      }
      set
      {
        this.nameEntity = value;
      }
    }

    public string TextValue
    {
      get
      {
        return this.labelDisplay.Text;
      }
    }

    public List<SearchResult> KeyValues
    {
      get
      {
        return this.GetKeyValues();
      }
    }

    public event UserControlSingleSearch.ValueChanged OnValueChanged;

    public event UserControlSingleSearch.CleanValues OnCleanValues;

    public UserControlSingleSearch()
    {
      this.InitializeComponent();
      this.filterList = new List<ColumnFilter>();
      this.prepareControls();
    }

    public void SetEntity<T>(IQueryable<T> query, ColumnModel displayColumn, List<ColumnModel> keyList = null, List<ColumnFilter> filterStatic = null, List<ColumnModel> columns = null, ColumnModel orderBy = null)
    {
      this.objGenericSearch = new GenericSearch();
      this.objGenericSearch.SetEntity<T>(query, filterStatic, columns, keyList, orderBy);
      this.gridSearch = new UserControlGridSearch();
      this.searchSize = new Size?(this.gridSearch.Size);
      this.gridSearch.CloseOnSelect = true;
      this.gridSearch.OnContentDoubleClick += new UserControlGridSearch.ContentDoubleClick(this.gridSearch_OnContentDoubleClick);
      this.gridSearch.CloseOnSelect = true;
      this.gridSearch.OnAddEntity += new UserControlGridSearch.AddEntity(this.gridSearch_OnAddEntity);
      this.gridSearch.OnEditEntity += new UserControlGridSearch.EditEntity(this.gridSearch_OnEditEntity);
      this.gridSearch.SearchObject = this.objGenericSearch;
      this.FilterStatic = filterStatic;
      this.keyList = this.objGenericSearch.KeyList;
      this.nameEntity = !string.IsNullOrEmpty(this.nameEntity) ? this.nameEntity : this.objGenericSearch.Meta.TableName;
      this.fixKeys();
      this.displayColumn = displayColumn;
    }

    private void gridSearch_OnEditEntity(List<SearchResult> selectedResult)
    {
      if (this.controlMaintenimiento == null)
        return;
      this.controlMaintenimiento.ObjSearch = selectedResult;
      GUtils.ShowControl((Control) this.controlMaintenimiento, "Editando " + this.maintenimientoTitle, new Size?());
      this.gridSearch.Refresh();
    }

    private void gridSearch_OnAddEntity()
    {
      if (this.controlMaintenimiento == null)
        return;
      GUtils.ShowControl((Control) this.controlMaintenimiento, "Insertando " + this.maintenimientoTitle, new Size?());
      this.gridSearch.Refresh();
    }

    private void fixKeys()
    {
      this.panelInputs.Controls.Clear();
      int num1 = this.objGenericSearch.KeyList.Count > 0 ? this.objGenericSearch.KeyList.Count : 1;
      int num2 = num1 * 60 - 5;
      int itemsWidth = num2 / num1;
      this.panelInputs.Width = num2;
      this.listaControles = new List<Control>();
      foreach (ColumnModel key in this.objGenericSearch.KeyList)
        this.listaControles.Add(this.getControl(key, itemsWidth));
      this.panelInputs.Controls.AddRange(this.listaControles.ToArray());
    }

    private Control getControl(ColumnModel key, int itemsWidth)
    {
      TextBox textBox = new TextBox();
      textBox.Multiline = true;
      textBox.Dock = DockStyle.Left;
      textBox.Name = key.Nombre;
      textBox.Enabled = true;
      textBox.ReadOnly = false;
      textBox.Width = itemsWidth;
      textBox.KeyPress += new KeyPressEventHandler(this.txtKeyPressEnter);
      textBox.LostFocus += new EventHandler(this.tempTextBox_LostFocus);
      if (MarhexTypes.IsNumeric(key.Tipo))
        textBox.KeyPress += new KeyPressEventHandler(this.txtKeyPressNumerico);
      return (Control) textBox;
    }

    private void tempTextBox_LostFocus(object sender, EventArgs e)
    {
      this.makeSearch();
    }

    private void makeSearch()
    {
      if (this.valuesAreCached())
        return;
      this.filterList.Clear();
      foreach (ColumnModel key in this.objGenericSearch.KeyList)
      {
        TextBox textBox = ((IEnumerable<Control>) this.panelInputs.Controls.Find(key.Nombre, false)).FirstOrDefault<Control>() as TextBox;
        if (!string.IsNullOrEmpty(textBox.Text))
          this.filterList.Add(new ColumnFilter()
          {
            Column = key,
            Comparison = Compare.Equal,
            Value1 = (object) textBox.Text
          });
      }
      this.makeSearch(this.filterList);
    }

    private bool valuesAreCached()
    {
      int num = 0;
      foreach (ColumnFilter filter in this.filterList)
      {
        if ((((IEnumerable<Control>) this.panelInputs.Controls.Find(filter.Column.Nombre, false)).FirstOrDefault<Control>() as TextBox).Text.Equals(filter.Value1.ToString()))
          ++num;
      }
      if (this.filterList.Count > 0)
        return num >= this.filterList.Count;
      return false;
    }

    private void makeSearch(List<ColumnFilter> filterList)
    {
      if (filterList.Count != this.objGenericSearch.KeyList.Count)
        return;
      IQueryable source = this.objGenericSearch.OriginalQuery.Where(filterList.GetFiltersInList(Compare.And)).Select(this.displayColumn.Nombre).Take(1);
      if (source.Count() > 0)
      {
        this.labelDisplay.Text = Queryable.Cast<string>(source).ToArray<string>()[0];
        this.keyChanged();
      }
      else
        this.ClearInputsKeyValues();
    }

    public static T GetValueFromAnonymousType<T>(object dataitem, string itemkey)
    {
      return (T) dataitem.GetType().GetProperty(itemkey).GetValue(dataitem, (object[]) null);
    }

    private void ClearInputsKeyValues()
    {
      this.filterList.Clear();
      foreach (object control in (ArrangedElementCollection) this.panelInputs.Controls)
        (control as TextBox).Clear();
      this.labelDisplay.Text = string.Empty;
      this.keyChanged();
    }

    private void gridSearch_OnContentDoubleClick(List<SearchResult> selectedResult)
    {
      foreach (SearchResult searchResult in selectedResult)
        (((IEnumerable<Control>) this.panelInputs.Controls.Find(searchResult.Column.Nombre, false)).FirstOrDefault<Control>() as TextBox).Text = searchResult.Value.ToString();
      this.makeSearch();
    }

    private void prepareControls()
    {
    }

    public void SetValues(List<SearchResult> selectedResult)
    {
      this.gridSearch_OnContentDoubleClick(selectedResult);
    }

    public void SetMantenimiento(UserControlBase controlMantenimiento, string title)
    {
      this.controlMaintenimiento = controlMantenimiento;
      this.mantenimientoSize = new Size?(this.controlMaintenimiento.Size);
      this.maintenimientoTitle = title;
    }

    private void buttonFind_Click(object sender, EventArgs e)
    {
      if (this.gridSearch == null)
        return;
      GUtils.ShowControl((Control) this.gridSearch, "Busqueda de " + this.NameEntity, this.searchSize);
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      this.ClearInputsKeyValues();
      if (this.OnCleanValues == null)
        return;
      this.OnCleanValues();
    }

    private void buttonMantenaince_Click(object sender, EventArgs e)
    {
      if (this.controlMaintenimiento == null)
        return;
      GUtils.ShowControl((Control) this.controlMaintenimiento, "Mantenimiento de " + this.NameEntity, new Size?());
    }

    private void keyChanged()
    {
      if (this.OnValueChanged == null)
        return;
      this.OnValueChanged(this.GetKeyValues(), this.labelDisplay.Text);
    }

    private void labelDisplay_Click(object sender, EventArgs e)
    {
    }

    public List<SearchResult> GetKeyValues()
    {
      List<SearchResult> searchResultList = new List<SearchResult>();
      foreach (ColumnFilter filter in this.filterList)
        searchResultList.Add(new SearchResult(filter.Column, filter.Value1));
      return searchResultList;
    }

    private void txtKeyPressEnter(object sender, KeyPressEventArgs e)
    {
      if ((int) e.KeyChar != 13)
        return;
      e.Handled = true;
      this.labelDisplay.Focus();
    }

    private void txtKeyPressNumerico(object sender, KeyPressEventArgs e)
    {
      if ((int) e.KeyChar == 13)
      {
        e.Handled = true;
        this.labelDisplay.Focus();
      }
      else if (char.IsDigit(e.KeyChar))
        e.Handled = false;
      else if (char.IsControl(e.KeyChar))
        e.Handled = false;
      else if (char.IsSeparator(e.KeyChar))
        e.Handled = false;
      else
        e.Handled = true;
    }

    public void Clean(bool notifique = true)
    {
      this.ClearInputsKeyValues();
      if (this.OnCleanValues == null || !notifique)
        return;
      this.OnCleanValues();
    }

    public delegate void ValueChanged(List<SearchResult> resultList, string displayValue);

    public delegate void CleanValues();
  }
}
