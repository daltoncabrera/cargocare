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
  public partial class UserControlConditionLine : UserControl
  {
    private IContainer components;
    private FlowLayoutPanel flowLayoutPanel1;
    private KryptonComboBox comboBoxFields;
    private KryptonComboBox comboBoxOperators;
    private KryptonTextBox textBoxStringValue;
    private KryptonComboBox comboBoxBolean;
    private KryptonDateTimePicker dateTimePickerDateValue;
    private KryptonNumericUpDown numericUpDownNumericValue;
    private KryptonButton buttonAND;
    private KryptonDateTimePicker dateTimePickerDateValue2;
    private KryptonNumericUpDown numericUpDownNumericValue2;
    private KryptonPanel kryptonPanel1;


    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.comboBoxFields = new KryptonComboBox();
      this.comboBoxOperators = new KryptonComboBox();
      this.textBoxStringValue = new KryptonTextBox();
      this.comboBoxBolean = new KryptonComboBox();
      this.dateTimePickerDateValue = new KryptonDateTimePicker();
      this.numericUpDownNumericValue = new KryptonNumericUpDown();
      this.buttonAND = new KryptonButton();
      this.dateTimePickerDateValue2 = new KryptonDateTimePicker();
      this.numericUpDownNumericValue2 = new KryptonNumericUpDown();
      this.kryptonPanel1 = new KryptonPanel();
      this.flowLayoutPanel1.SuspendLayout();
      this.comboBoxFields.BeginInit();
      this.comboBoxOperators.BeginInit();
      this.comboBoxBolean.BeginInit();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.SuspendLayout();
      this.flowLayoutPanel1.BackColor = Color.Transparent;
      this.flowLayoutPanel1.Controls.Add((Control)this.comboBoxFields);
      this.flowLayoutPanel1.Controls.Add((Control)this.comboBoxOperators);
      this.flowLayoutPanel1.Controls.Add((Control)this.textBoxStringValue);
      this.flowLayoutPanel1.Controls.Add((Control)this.comboBoxBolean);
      this.flowLayoutPanel1.Controls.Add((Control)this.dateTimePickerDateValue);
      this.flowLayoutPanel1.Controls.Add((Control)this.numericUpDownNumericValue);
      this.flowLayoutPanel1.Controls.Add((Control)this.buttonAND);
      this.flowLayoutPanel1.Controls.Add((Control)this.dateTimePickerDateValue2);
      this.flowLayoutPanel1.Controls.Add((Control)this.numericUpDownNumericValue2);
      this.flowLayoutPanel1.Dock = DockStyle.Fill;
      this.flowLayoutPanel1.Location = new Point(0, 0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new Size(949, 28);
      this.flowLayoutPanel1.TabIndex = 3;
      this.comboBoxFields.DropDownWidth = 121;
      this.comboBoxFields.Location = new Point(3, 3);
      this.comboBoxFields.Name = "comboBoxFields";
      this.comboBoxFields.Size = new Size(147, 21);
      this.comboBoxFields.TabIndex = 7;
      this.comboBoxFields.SelectedIndexChanged += new EventHandler(this.comboBoxFields_SelectedValueChanged);
      this.comboBoxOperators.DropDownWidth = 121;
      this.comboBoxOperators.Location = new Point(156, 3);
      this.comboBoxOperators.Name = "comboBoxOperators";
      this.comboBoxOperators.Size = new Size(91, 21);
      this.comboBoxOperators.TabIndex = 8;
      this.comboBoxOperators.SelectedIndexChanged += new EventHandler(this.comboBoxOperators_SelectedValueChanged);
      this.textBoxStringValue.Location = new Point(253, 3);
      this.textBoxStringValue.Name = "textBoxStringValue";
      this.textBoxStringValue.Size = new Size(113, 20);
      this.textBoxStringValue.TabIndex = 9;
      this.comboBoxBolean.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxBolean.DropDownWidth = 121;
      this.comboBoxBolean.Items.AddRange(new object[2]
      {
        (object) "True",
        (object) "False"
      });
      this.comboBoxBolean.Location = new Point(372, 3);
      this.comboBoxBolean.Name = "comboBoxBolean";
      this.comboBoxBolean.Size = new Size(91, 21);
      this.comboBoxBolean.TabIndex = 10;
      this.dateTimePickerDateValue.Location = new Point(469, 3);
      this.dateTimePickerDateValue.Name = "dateTimePickerDateValue";
      this.dateTimePickerDateValue.Size = new Size(121, 21);
      this.dateTimePickerDateValue.TabIndex = 11;
      this.numericUpDownNumericValue.Location = new Point(596, 3);
      this.numericUpDownNumericValue.Maximum = new Decimal(new int[4]
      {
        1874919424,
        2328306,
        0,
        0
      });
      this.numericUpDownNumericValue.Name = "numericUpDownNumericValue";
      this.numericUpDownNumericValue.Size = new Size(87, 22);
      this.numericUpDownNumericValue.TabIndex = 12;
      this.buttonAND.Location = new Point(689, 3);
      this.buttonAND.Name = "buttonAND";
      this.buttonAND.Size = new Size(31, 24);
      this.buttonAND.TabIndex = 13;
      this.buttonAND.Values.Text = "Y";
      this.dateTimePickerDateValue2.Location = new Point(726, 3);
      this.dateTimePickerDateValue2.Name = "dateTimePickerDateValue2";
      this.dateTimePickerDateValue2.Size = new Size(121, 21);
      this.dateTimePickerDateValue2.TabIndex = 15;
      this.numericUpDownNumericValue2.Location = new Point(853, 3);
      this.numericUpDownNumericValue2.Maximum = new Decimal(new int[4]
      {
        1874919424,
        2328306,
        0,
        0
      });
      this.numericUpDownNumericValue2.Name = "numericUpDownNumericValue2";
      this.numericUpDownNumericValue2.Size = new Size(87, 22);
      this.numericUpDownNumericValue2.TabIndex = 14;
      this.kryptonPanel1.Controls.Add((Control)this.flowLayoutPanel1);
      this.kryptonPanel1.Dock = DockStyle.Fill;
      this.kryptonPanel1.Location = new Point(0, 0);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.Size = new Size(949, 28);
      this.kryptonPanel1.TabIndex = 4;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control)this.kryptonPanel1);
      this.Name = "UserControlConditionLine";
      this.Size = new Size(949, 28);
      this.Load += new EventHandler(this.UserControlConditionLine_Load);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.comboBoxFields.EndInit();
      this.comboBoxOperators.EndInit();
      this.comboBoxBolean.EndInit();
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
