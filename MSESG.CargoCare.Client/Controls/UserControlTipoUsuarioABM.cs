using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public class UserControlTipoUsuarioABM : UserControlCareCargoBase
  {
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxTitle;
    private KryptonLabel kryptonLabel2;
    private KryptonTextBox kryptonTextBoxName;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBoxButtonName;

    public UserControlTipoUsuarioABM(List<SearchResult> objSearch = null)
    {
      this.InitializeComponent();
      this.ObjSearch = objSearch;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonTextBoxTitle = new KryptonTextBox();
      this.kryptonTextBoxName = new KryptonTextBox();
      this.kryptonTextBoxButtonName = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.kryptonLabel2 = new KryptonLabel();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonPanel2 = new KryptonPanel();
      this.kryptonButtonAccept = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanel2.BeginInit();
      this.kryptonPanel2.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonAccept);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonPanel2);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(437, 219);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(437, 31);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = ".";
      this.kryptonHeader1.Values.Image = (Image) null;
      this.kryptonTextBoxTitle.Location = new Point(164, 22);
      this.kryptonTextBoxTitle.Name = "kryptonTextBoxTitle";
      this.kryptonTextBoxTitle.Size = new Size(234, 20);
      this.kryptonTextBoxTitle.TabIndex = 1;
      this.kryptonTextBoxName.Location = new Point(164, 48);
      this.kryptonTextBoxName.Name = "kryptonTextBoxName";
      this.kryptonTextBoxName.Size = new Size(234, 20);
      this.kryptonTextBoxName.TabIndex = 2;
      this.kryptonTextBoxButtonName.Location = new Point(164, 74);
      this.kryptonTextBoxButtonName.Name = "kryptonTextBoxButtonName";
      this.kryptonTextBoxButtonName.Size = new Size(234, 20);
      this.kryptonTextBoxButtonName.TabIndex = 3;
      this.kryptonLabel1.Location = new Point(8, 48);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(150, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Nombre del Componente";
      this.kryptonLabel2.Location = new Point(60, 74);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(98, 20);
      this.kryptonLabel2.TabIndex = 5;
      this.kryptonLabel2.Values.Text = "Boton Asignado";
      this.kryptonLabel3.Location = new Point(22, 22);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(136, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Titulo del Componente";
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxTitle);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxName);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxButtonName);
      this.kryptonPanel2.Location = new Point(14, 47);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.Size = new Size(411, 119);
      this.kryptonPanel2.TabIndex = 7;
      this.kryptonButtonAccept.Location = new Point(226, 179);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(335, 179);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlPermisosABM";
      this.Size = new Size(437, 219);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.kryptonPanel2.EndInit();
      this.kryptonPanel2.ResumeLayout(false);
      this.kryptonPanel2.PerformLayout();
      this.ResumeLayout(false);
    }

    private void kryptonButtonCancel_Click(object sender, EventArgs e)
    {
    }

    private void kryptonButtonAccept_Click(object sender, EventArgs e)
    {
    }
  }
}
