using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public class UserControlComponetABM : UserControlCareCargoBase
  {
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxDescription;
    private KryptonTextBox kryptonTextBoxName;
    private KryptonLabel kryptonLabel1;
    private ComponentWraper objComponentWraper;
    private componente currentComponent;

    public UserControlComponetABM(List<SearchResult> objSearch = null)
    {
      this.ObjSearch = objSearch;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlComponetABM));
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonTextBoxDescription = new KryptonTextBox();
      this.kryptonTextBoxName = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
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
      this.kryptonPanel1.Size = new Size(437, 212);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(437, 28);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "";
      //this.kryptonHeader1.Values.Image = (Image) componentResourceManager.GetObject("kryptonHeader1.Values.Image");
      this.kryptonTextBoxDescription.Location = new Point(164, 53);
      this.kryptonTextBoxDescription.Name = "kryptonTextBoxDescription";
      this.kryptonTextBoxDescription.Size = new Size(234, 20);
      this.kryptonTextBoxDescription.TabIndex = 1;
      this.kryptonTextBoxName.Location = new Point(164, 26);
      this.kryptonTextBoxName.Name = "kryptonTextBoxName";
      this.kryptonTextBoxName.Size = new Size(234, 20);
      this.kryptonTextBoxName.TabIndex = 2;
      this.kryptonLabel1.Location = new Point(23, 27);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(137, 16);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Nombre del Componente";
      this.kryptonLabel3.Location = new Point(91, 54);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(69, 16);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Descripcion";
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxDescription);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxName);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Location = new Point(12, 42);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanel2.Size = new Size(411, 100);
      this.kryptonPanel2.TabIndex = 7;
      this.kryptonButtonAccept.Location = new Point(224, 157);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(333, 157);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlComponetABM";
      this.Size = new Size(437, 212);
      this.Load += new EventHandler(this.UserControlComponetABM_Load);
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
      this.ParentForm.Close();
    }

    private void kryptonButtonAccept_Click(object sender, EventArgs e)
    {
      try
      {
        this.save();
        GUtils.ShowMessage("Registro guardado");
        this.ParentForm.Close();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Erro guardando registro", ex);
      }
    }

    private void save()
    {
      this.currentComponent = this.objComponentWraper.Save(this.currentComponent != null ? new int?(this.currentComponent.IdComponente) : new int?(), this.kryptonTextBoxName.Text, this.kryptonTextBoxDescription.Text);
    }

    private void UserControlComponetABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void prepareControls()
    {
      this.objComponentWraper = new ComponentWraper();
      this.currentComponent = this.objComponentWraper.GetComponent(this.ObjSearch);
      this.fillForm();
    }

    private void fillForm()
    {
      if (this.currentComponent == null)
        return;
      this.kryptonTextBoxName.Text = this.currentComponent.ComponentName;
      this.kryptonTextBoxDescription.Text = this.currentComponent.Description;
    }
  }
}
