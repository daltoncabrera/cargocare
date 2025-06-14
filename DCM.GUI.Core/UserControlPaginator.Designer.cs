using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  public partial class UserControlPaginator : UserControl
  {
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private KryptonPanel kryptonPanel2;
    private KryptonPanel kryptonPanel1;
    private Label labelPagesInfo;
    private KryptonPanel kryptonPanel4;
    private KryptonPanel kryptonPanel3;
    private KryptonButton buttonFirst;
    private KryptonButton buttonPrior;
    private KryptonButton buttonLast;
    private KryptonButton buttonNext;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.kryptonPanel2 = new KryptonPanel();
      this.buttonFirst = new KryptonButton();
      this.buttonPrior = new KryptonButton();
      this.kryptonPanel1 = new KryptonPanel();
      this.buttonLast = new KryptonButton();
      this.buttonNext = new KryptonButton();
      this.kryptonPanel4 = new KryptonPanel();
      this.labelPagesInfo = new Label();
      this.kryptonPanel3 = new KryptonPanel();
      this.tableLayoutPanel1.SuspendLayout();
      this.kryptonPanel2.BeginInit();
      this.kryptonPanel2.SuspendLayout();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanel4.BeginInit();
      this.kryptonPanel4.SuspendLayout();
      this.kryptonPanel3.BeginInit();
      this.kryptonPanel3.SuspendLayout();
      this.SuspendLayout();
      this.tableLayoutPanel1.BackColor = Color.Transparent;
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control)this.kryptonPanel2, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control)this.kryptonPanel1, 2, 0);
      this.tableLayoutPanel1.Controls.Add((Control)this.kryptonPanel4, 1, 0);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.Size = new Size(648, 37);
      this.tableLayoutPanel1.TabIndex = 2;
      this.kryptonPanel2.Controls.Add((Control)this.buttonFirst);
      this.kryptonPanel2.Controls.Add((Control)this.buttonPrior);
      this.kryptonPanel2.Dock = DockStyle.Fill;
      this.kryptonPanel2.Location = new Point(3, 3);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.Size = new Size(253, 31);
      this.kryptonPanel2.TabIndex = 3;
      this.buttonFirst.Dock = DockStyle.Right;
      this.buttonFirst.Location = new Point(73, 0);
      this.buttonFirst.Name = "buttonFirst";
      this.buttonFirst.Size = new Size(90, 31);
      this.buttonFirst.TabIndex = 2;
      this.buttonFirst.Values.Text = "|<";
      this.buttonFirst.Click += new EventHandler(this.buttonFirst_Click);
      this.buttonPrior.Dock = DockStyle.Right;
      this.buttonPrior.Location = new Point(163, 0);
      this.buttonPrior.Name = "buttonPrior";
      this.buttonPrior.Size = new Size(90, 31);
      this.buttonPrior.TabIndex = 1;
      this.buttonPrior.Values.Text = "<";
      this.buttonPrior.Click += new EventHandler(this.buttonPrior_Click);
      this.kryptonPanel1.Controls.Add((Control)this.buttonLast);
      this.kryptonPanel1.Controls.Add((Control)this.buttonNext);
      this.kryptonPanel1.Dock = DockStyle.Fill;
      this.kryptonPanel1.Location = new Point(392, 3);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.Size = new Size(253, 31);
      this.kryptonPanel1.TabIndex = 0;
      this.buttonLast.Dock = DockStyle.Left;
      this.buttonLast.Location = new Point(90, 0);
      this.buttonLast.Name = "buttonLast";
      this.buttonLast.Size = new Size(90, 31);
      this.buttonLast.TabIndex = 1;
      this.buttonLast.Values.Text = ">|";
      this.buttonLast.Click += new EventHandler(this.buttonLast_Click);
      this.buttonNext.Dock = DockStyle.Left;
      this.buttonNext.Location = new Point(0, 0);
      this.buttonNext.Name = "buttonNext";
      this.buttonNext.Size = new Size(90, 31);
      this.buttonNext.TabIndex = 0;
      this.buttonNext.Values.Text = ">";
      this.buttonNext.Click += new EventHandler(this.buttonNext_Click);
      this.kryptonPanel4.Controls.Add((Control)this.labelPagesInfo);
      this.kryptonPanel4.Dock = DockStyle.Fill;
      this.kryptonPanel4.Location = new Point(262, 3);
      this.kryptonPanel4.Name = "kryptonPanel4";
      this.kryptonPanel4.Size = new Size(124, 31);
      this.kryptonPanel4.TabIndex = 4;
      this.labelPagesInfo.BackColor = Color.Transparent;
      this.labelPagesInfo.Dock = DockStyle.Fill;
      this.labelPagesInfo.Location = new Point(0, 0);
      this.labelPagesInfo.Name = "labelPagesInfo";
      this.labelPagesInfo.Size = new Size(124, 31);
      this.labelPagesInfo.TabIndex = 4;
      this.labelPagesInfo.Text = "0 de 0";
      this.labelPagesInfo.TextAlign = ContentAlignment.MiddleCenter;
      this.kryptonPanel3.Controls.Add((Control)this.tableLayoutPanel1);
      this.kryptonPanel3.Dock = DockStyle.Fill;
      this.kryptonPanel3.Location = new Point(0, 0);
      this.kryptonPanel3.Name = "kryptonPanel3";
      this.kryptonPanel3.Size = new Size(648, 37);
      this.kryptonPanel3.TabIndex = 3;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control)this.kryptonPanel3);
      this.Name = "UserControlPaginator";
      this.Size = new Size(648, 37);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.kryptonPanel2.EndInit();
      this.kryptonPanel2.ResumeLayout(false);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel4.EndInit();
      this.kryptonPanel4.ResumeLayout(false);
      this.kryptonPanel3.EndInit();
      this.kryptonPanel3.ResumeLayout(false);
      this.ResumeLayout(false);
    }

  }
}
