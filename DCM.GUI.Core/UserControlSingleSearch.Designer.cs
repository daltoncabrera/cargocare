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
  public partial class UserControlSingleSearch : UserControl
  {
    private IContainer components;
    private Panel panel1;
    private Label labelDisplay;
    private KryptonButton buttonClear;
    private KryptonButton buttonFind;
    private KryptonPanel kryptonPanel1;
    private KryptonPanel panelInputs;
    private UserControlBase controlMaintenimiento;
    private string maintenimientoTitle;
    private List<ColumnModel> keyList;
    private List<ColumnModel> columns;
    private List<ColumnFilter> FilterStatic;
    private ColumnModel orderBy;
    private ColumnModel displayColumn;
    private List<Control> listaControles;
    private GenericSearch objGenericSearch;
    private List<ColumnFilter> filterList;
    private UserControlGridSearch gridSearch;


    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonFind = new ComponentFactory.Krypton.Toolkit.KryptonButton();
      this.buttonClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
      this.labelDisplay = new System.Windows.Forms.Label();
      this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
      this.panelInputs = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
      this.kryptonPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.panelInputs)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.buttonFind);
      this.panel1.Controls.Add(this.buttonClear);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel1.Location = new System.Drawing.Point(377, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(70, 30);
      this.panel1.TabIndex = 2;
      // 
      // buttonFind
      // 
      this.buttonFind.Cursor = System.Windows.Forms.Cursors.Hand;
      this.buttonFind.Dock = System.Windows.Forms.DockStyle.Right;
      this.buttonFind.Location = new System.Drawing.Point(2, 0);
      this.buttonFind.Name = "buttonFind";
      this.buttonFind.Size = new System.Drawing.Size(36, 30);
      this.buttonFind.TabIndex = 0;
      this.buttonFind.Values.Image = global::DCM.Core.UI.Properties.Resources._1336071055_search;
      this.buttonFind.Values.Text = "";
      this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
      // 
      // buttonClear
      // 
      this.buttonClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.buttonClear.Dock = System.Windows.Forms.DockStyle.Right;
      this.buttonClear.Location = new System.Drawing.Point(38, 0);
      this.buttonClear.Name = "buttonClear";
      this.buttonClear.Size = new System.Drawing.Size(32, 30);
      this.buttonClear.TabIndex = 1;
      this.buttonClear.Values.Image = global::DCM.Core.UI.Properties.Resources._1336071103_clear_left;
      this.buttonClear.Values.Text = "";
      this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
      // 
      // labelDisplay
      // 
      this.labelDisplay.BackColor = System.Drawing.Color.White;
      this.labelDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.labelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelDisplay.Location = new System.Drawing.Point(49, 0);
      this.labelDisplay.Name = "labelDisplay";
      this.labelDisplay.Size = new System.Drawing.Size(328, 30);
      this.labelDisplay.TabIndex = 5;
      this.labelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.labelDisplay.Click += new System.EventHandler(this.labelDisplay_Click);
      // 
      // kryptonPanel1
      // 
      this.kryptonPanel1.Controls.Add(this.labelDisplay);
      this.kryptonPanel1.Controls.Add(this.panelInputs);
      this.kryptonPanel1.Controls.Add(this.panel1);
      this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.Size = new System.Drawing.Size(447, 30);
      this.kryptonPanel1.TabIndex = 6;
      // 
      // panelInputs
      // 
      this.panelInputs.Dock = System.Windows.Forms.DockStyle.Left;
      this.panelInputs.Location = new System.Drawing.Point(0, 0);
      this.panelInputs.Name = "panelInputs";
      this.panelInputs.Size = new System.Drawing.Size(49, 30);
      this.panelInputs.TabIndex = 6;
      // 
      // UserControlSingleSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.kryptonPanel1);
      this.Name = "UserControlSingleSearch";
      this.Size = new System.Drawing.Size(447, 30);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.panelInputs)).EndInit();
      this.ResumeLayout(false);

    }

  }
}
