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
  public class UserControlItemsVerificacionABM : UserControlCareCargoBase
  {
    private ItemsVerificacionWraper objItemsVerificacionWraper;
    private itemsverificacion currentItemsVerificacion;
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxNombre;
    private KryptonTextBox kryptonTextBoxComentario;
    private KryptonLabel kryptonLabel1;

    public UserControlItemsVerificacionABM(List<SearchResult> objSearch = null)
    {
      this.InitializeComponent();
      this.ObjSearch = objSearch;
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
      this.currentItemsVerificacion = this.objItemsVerificacionWraper.Save(this.currentItemsVerificacion != null ? new int?(this.currentItemsVerificacion.IdItemsVerificacion) : new int?(), this.kryptonTextBoxNombre.Text, this.kryptonTextBoxComentario.Text);
    }

    private void prepareControls()
    {
      this.objItemsVerificacionWraper = new ItemsVerificacionWraper();
      this.currentItemsVerificacion = this.objItemsVerificacionWraper.GetItemsVerificacion(this.ObjSearch);
      this.fillForm();
    }

    private void fillForm()
    {
      if (this.currentItemsVerificacion == null)
        return;
      this.kryptonTextBoxNombre.Text = this.currentItemsVerificacion.Descripcion;
      this.kryptonTextBoxComentario.Text = this.currentItemsVerificacion.Comentario;
    }

    private void UserControlItemsVerificacionABM_Load(object sender, EventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlItemsVerificacionABM));
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonButtonAccept = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.kryptonPanel2 = new KryptonPanel();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonTextBoxNombre = new KryptonTextBox();
      this.kryptonTextBoxComentario = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanel2.BeginInit();
      this.kryptonPanel2.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonPanel2);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonAccept);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(389, 268);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(389, 28);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "";
      this.kryptonHeader1.Values.Image = (Image) componentResourceManager.GetObject("kryptonHeader1.Values.Image");
      this.kryptonButtonAccept.Location = new Point(174, 180);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(283, 180);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxNombre);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxComentario);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Location = new Point(12, 51);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanel2.Size = new Size(361, 119);
      this.kryptonPanel2.TabIndex = 10;
      this.kryptonLabel3.Location = new Point(9, 22);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(102, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Item Descripcion";
      this.kryptonTextBoxNombre.Location = new Point(111, 22);
      this.kryptonTextBoxNombre.Name = "kryptonTextBoxNombre";
      this.kryptonTextBoxNombre.Size = new Size(234, 20);
      this.kryptonTextBoxNombre.TabIndex = 1;
      this.kryptonTextBoxComentario.Location = new Point(111, 48);
      this.kryptonTextBoxComentario.Multiline = true;
      this.kryptonTextBoxComentario.Name = "kryptonTextBoxComentario";
      this.kryptonTextBoxComentario.Size = new Size(234, 57);
      this.kryptonTextBoxComentario.TabIndex = 2;
      this.kryptonLabel1.Location = new Point(36, 48);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(75, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Comentario";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlItemsVerificacionABM";
      this.Size = new Size(389, 268);
      this.Load += new EventHandler(this.UserControlItemsVerificacionABM_Load);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.kryptonPanel2.EndInit();
      this.kryptonPanel2.ResumeLayout(false);
      this.kryptonPanel2.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
