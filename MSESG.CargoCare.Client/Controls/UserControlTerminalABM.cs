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
  public class UserControlTerminalABM : UserControlCareCargoBase
  {
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxNombre;
    private KryptonTextBox kryptonTextBoxComentario;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBoxDir;
    private KryptonLabel kryptonLabel2;
    private TerminalWraper objTerminalWraper;
    private terminal currentTerminal;

    public UserControlTerminalABM(List<SearchResult> objSearch = null)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlTerminalABM));
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonButtonAccept = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.kryptonPanel2 = new KryptonPanel();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonTextBoxNombre = new KryptonTextBox();
      this.kryptonTextBoxComentario = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.kryptonTextBoxDir = new KryptonTextBox();
      this.kryptonLabel2 = new KryptonLabel();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanel2.BeginInit();
      this.kryptonPanel2.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonPanel2);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonAccept);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(397, 322);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(397, 28);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "";
     // this.kryptonHeader1.Values.Image = (Image) componentResourceManager.GetObject("kryptonHeader1.Values.Image");
      this.kryptonButtonAccept.Location = new Point(179, 252);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(288, 252);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxDir);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxNombre);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxComentario);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Location = new Point(17, 44);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanel2.Size = new Size(361, 192);
      this.kryptonPanel2.TabIndex = 10;
      this.kryptonLabel3.Location = new Point(43, 22);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(56, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Nombre ";
      this.kryptonTextBoxNombre.Location = new Point(100, 22);
      this.kryptonTextBoxNombre.Name = "kryptonTextBoxNombre";
      this.kryptonTextBoxNombre.Size = new Size(234, 20);
      this.kryptonTextBoxNombre.TabIndex = 1;
      this.kryptonTextBoxComentario.Location = new Point(100, 111);
      this.kryptonTextBoxComentario.Multiline = true;
      this.kryptonTextBoxComentario.Name = "kryptonTextBoxComentario";
      this.kryptonTextBoxComentario.Size = new Size(234, 57);
      this.kryptonTextBoxComentario.TabIndex = 2;
      this.kryptonLabel1.Location = new Point(25, 111);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(75, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Comentario";
      this.kryptonTextBoxDir.Location = new Point(100, 48);
      this.kryptonTextBoxDir.Multiline = true;
      this.kryptonTextBoxDir.Name = "kryptonTextBoxDir";
      this.kryptonTextBoxDir.Size = new Size(234, 57);
      this.kryptonTextBoxDir.TabIndex = 7;
      this.kryptonLabel2.Location = new Point(37, 48);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(62, 20);
      this.kryptonLabel2.TabIndex = 8;
      this.kryptonLabel2.Values.Text = "Direccion";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlTerminalABM";
      this.Size = new Size(397, 322);
      this.Load += new EventHandler(this.UserControlTerminalABM_Load);
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
      this.currentTerminal = this.objTerminalWraper.Save(this.currentTerminal != null ? new int?(this.currentTerminal.IdTerminal) : new int?(), this.kryptonTextBoxNombre.Text, this.kryptonTextBoxDir.Text, this.kryptonTextBoxComentario.Text);
    }

    private void prepareControls()
    {
      this.objTerminalWraper = new TerminalWraper();
      this.currentTerminal = this.objTerminalWraper.GetTerminal(this.ObjSearch);
      this.fillForm();
    }

    private void fillForm()
    {
      if (this.currentTerminal == null)
        return;
      this.kryptonTextBoxNombre.Text = this.currentTerminal.NombreTerminal;
      this.kryptonTextBoxDir.Text = this.currentTerminal.Dir;
      this.kryptonTextBoxComentario.Text = this.currentTerminal.Comentario;
    }

    private void UserControlTerminalABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }
  }
}
