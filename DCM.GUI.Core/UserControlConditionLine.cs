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
  public partial class UserControlConditionLine 
  {
    private NameValueObject operIgualQue = new NameValueObject()
    {
      Name = "Igual que",
      Value = (object) Compare.Equal
    };
    private NameValueObject operDiferente = new NameValueObject()
    {
      Name = "Diferente de",
      Value = (object) Compare.NotEqual
    };
    private NameValueObject operMayorQue = new NameValueObject()
    {
      Name = "Mayor que",
      Value = (object) Compare.GreaterThan
    };
    private NameValueObject operMayorIgualQue = new NameValueObject()
    {
      Name = "Mayor/Igual que",
      Value = (object) Compare.GreaterThanOrEqual
    };
    private NameValueObject operMenorQue = new NameValueObject()
    {
      Name = "Menor que",
      Value = (object) Compare.LessThan
    };
    private NameValueObject operMenorIgualQue = new NameValueObject()
    {
      Name = "Menor/Igual que",
      Value = (object) Compare.LessThanOrEqual
    };
    private NameValueObject operContiene = new NameValueObject()
    {
      Name = "Contiene",
      Value = (object) Compare.Like
    };
    private NameValueObject operEntre = new NameValueObject()
    {
      Name = "Entre",
      Value = (object) Compare.Between
    };
    private List<NameValueObject> operators = new List<NameValueObject>();
    private const string NAME_OPERATOR_STRING = "Name";
    private const string VALUE_OPERATOR_STRING = "Value";
    private const string IGUAL_QUE_STRING = "Igual que";
    private const string DIFERENTE_DE_STRING = "Diferente de";
    private const string MAYOR_QUE_STRING = "Mayor que";
    private const string MAYOR_IGUAL_QUE_STRING = "Mayor/Igual que";
    private const string MENOR_QUE_STRING = "Menor que";
    private const string MENOR_IGUAL_QUE_STRING = "Menor/Igual que";
    private const string CONTIENE_STRING = "Contiene";
    private const string ENTRE_STRING = "Entre";
    private const int TRUE_INT_VALUE = 0;
    private List<ColumnModel> fields;
    private ColumnModel currentColumn;


    public List<ColumnModel> Fields
    {
      get
      {
        return this.fields;
      }
      set
      {
        this.fields = value;
        this.prepareControls();
      }
    }

    public UserControlConditionLine()
    {
      this.InitializeComponent();
    }

    private void UserControlConditionLine_Load(object sender, EventArgs e)
    {
    }

    private void prepareControls()
    {
      this.fillFields();
      this.prepareComboOperators();
    }

    private void fillFields()
    {
      if (this.Fields == null)
        return;
      this.comboBoxFields.DataSource = (object) this.Fields.Select(s => new
      {
        Name = s.Nombre,
        Value = s
      }).ToList();
      this.comboBoxFields.ValueMember = "Value";
      this.comboBoxFields.DisplayMember = "Name";
      this.comboBoxFields.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    private void prepareComboOperators()
    {
      this.comboBoxOperators.ValueMember = "Value";
      this.comboBoxOperators.DisplayMember = "Name";
      this.comboBoxOperators.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxOperators.DataSource = (object) this.operators;
      if (this.operators.Count <= 0)
        return;
      this.comboBoxOperators.SelectedIndex = 0;
    }

    private void comboBoxFields_SelectedValueChanged(object sender, EventArgs e)
    {
      this.changeVisibility();
    }

    private void changeVisibility()
    {
      this.currentColumn = this.comboBoxFields.SelectedValue as ColumnModel;
      if (this.currentColumn == null)
        return;
      this.setVisibility(this.currentColumn.Tipo);
    }

    private void setVisibility(Type type)
    {
      this.clearControls(false);
      if (MarhexTypes.IsBool(type))
      {
        this.comboBoxBolean.Visible = true;
        this.comboBoxBolean.SelectedIndex = 0;
        this.operators.Add(this.operIgualQue);
      }
      if (MarhexTypes.IsString(type))
      {
        this.textBoxStringValue.Visible = true;
        this.operators.Add(this.operContiene);
        this.operators.Add(this.operIgualQue);
      }
      if (MarhexTypes.IsNumeric(type))
      {
        this.numericUpDownNumericValue.Visible = true;
        this.operators.Add(this.operIgualQue);
        this.operators.Add(this.operMayorQue);
        this.operators.Add(this.operMayorIgualQue);
        this.operators.Add(this.operMenorQue);
        this.operators.Add(this.operMenorIgualQue);
        this.operators.Add(this.operEntre);
      }
      if (MarhexTypes.IsDateTime(type))
      {
        this.dateTimePickerDateValue.Visible = true;
        this.operators.Add(this.operIgualQue);
        this.operators.Add(this.operMayorQue);
        this.operators.Add(this.operMayorIgualQue);
        this.operators.Add(this.operMenorQue);
        this.operators.Add(this.operMenorIgualQue);
        this.operators.Add(this.operEntre);
      }
      this.prepareComboOperators();
    }

    private void clearControls(bool visible = false)
    {
      this.numericUpDownNumericValue.Value = new Decimal(0);
      this.numericUpDownNumericValue.Visible = visible;
      this.numericUpDownNumericValue2.Value = new Decimal(0);
      this.numericUpDownNumericValue2.Visible = visible;
      this.comboBoxOperators.DataSource = (object) null;
      this.textBoxStringValue.Clear();
      this.textBoxStringValue.Visible = visible;
      this.operators.Clear();
      this.dateTimePickerDateValue.Visible = visible;
      this.dateTimePickerDateValue2.Visible = visible;
      this.comboBoxBolean.Visible = false;
      this.buttonAND.Visible = false;
    }

    public ColumnFilter GetFilter()
    {
      ColumnModel selectedValue1 = this.comboBoxFields.SelectedValue as ColumnModel;
      Compare selectedValue2 = (Compare) this.comboBoxOperators.SelectedValue;
      object[] values = this.getValues(selectedValue1.Tipo);
      ColumnFilter columnFilter = new ColumnFilter()
      {
        Column = selectedValue1,
        Comparison = selectedValue2,
        Value1 = values[0],
        Value2 = values[1]
      };
      if (selectedValue1 != null)
        return columnFilter;
      return (ColumnFilter) null;
    }

    private object[] getValues(Type type)
    {
      object[] objArray = new object[2];
      if (MarhexTypes.IsBool(type))
      {
        bool? nullable = new bool?();
        nullable = new bool?(this.comboBoxBolean.SelectedIndex == 0);
        objArray[0] = (object) nullable;
      }
      if (MarhexTypes.IsString(type))
        objArray[0] = (object) this.textBoxStringValue.Text;
      if (MarhexTypes.IsInt(type))
      {
        objArray[0] = (object) (int) this.numericUpDownNumericValue.Value;
        objArray[1] = (object) (int) this.numericUpDownNumericValue2.Value;
      }
      if (MarhexTypes.IsDecimal(type))
      {
        objArray[0] = (object) this.numericUpDownNumericValue.Value;
        objArray[1] = (object) this.numericUpDownNumericValue2.Value;
      }
      if (MarhexTypes.IsDateTime(type))
      {
        objArray[0] = (object) this.dateTimePickerDateValue.Value;
        objArray[1] = (object) this.dateTimePickerDateValue2.Value;
      }
      return objArray;
    }

    public void ClearFilter()
    {
      this.comboBoxFields.SelectedIndex = this.comboBoxFields.Items.Count >= 0 ? 0 : -1;
      this.changeVisibility();
    }

    private void comboBoxOperators_SelectedValueChanged(object sender, EventArgs e)
    {
      this.currentColumn = this.comboBoxFields.SelectedValue as ColumnModel;
      this.buttonAND.Visible = false;
      this.numericUpDownNumericValue2.Visible = false;
      this.dateTimePickerDateValue2.Visible = false;
      if (this.currentColumn == null || this.comboBoxOperators.SelectedValue == null)
        return;
      if ((Compare) this.comboBoxOperators.SelectedValue == Compare.Between && MarhexTypes.IsDateTime(this.currentColumn.Tipo))
      {
        this.buttonAND.Visible = true;
        this.dateTimePickerDateValue2.Visible = true;
        this.numericUpDownNumericValue2.Visible = false;
      }
      if ((Compare) this.comboBoxOperators.SelectedValue != Compare.Between || !MarhexTypes.IsNumeric(this.currentColumn.Tipo))
        return;
      this.buttonAND.Visible = true;
      this.numericUpDownNumericValue2.Visible = true;
      this.dateTimePickerDateValue2.Visible = false;
    }

  }
}
