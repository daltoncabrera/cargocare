using ComponentFactory.Krypton.Toolkit;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public class UserControlClienteABM : UserControlCareCargoBase
  {
    private string imgFolder = "imagenes";
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxClienteName;
    private KryptonLabel kryptonLabel2;
    private KryptonTextBox kryptonTextBoxTelefono;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBoxButtonFax;
    private KryptonLabel kryptonLabel5;
    private KryptonTextBox kryptonTextBoxDir;
    private KryptonLabel kryptonLabel4;
    private KryptonTextBox kryptonTextBoxEmail;
    private PictureBox pictureBox1;
    private KryptonButton kryptonButtonFindLogo;
    private KryptonTextBox kryptonTextBoxLogo;
    private KryptonLabel kryptonLabel6;
    private OpenFileDialog openFileDialog1;
    private cliente currentCliente;
    private ClienteWraper objWraper;
    private bool logoChanged;

    public UserControlClienteABM(List<DCM.Core.Data.SearchResult> objSearch = null)
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
      this.kryptonTextBoxClienteName = new KryptonTextBox();
      this.kryptonTextBoxTelefono = new KryptonTextBox();
      this.kryptonTextBoxButtonFax = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.kryptonLabel2 = new KryptonLabel();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonPanel2 = new KryptonPanel();
      this.pictureBox1 = new PictureBox();
      this.kryptonButtonFindLogo = new KryptonButton();
      this.kryptonTextBoxLogo = new KryptonTextBox();
      this.kryptonLabel6 = new KryptonLabel();
      this.kryptonLabel5 = new KryptonLabel();
      this.kryptonTextBoxDir = new KryptonTextBox();
      this.kryptonLabel4 = new KryptonLabel();
      this.kryptonTextBoxEmail = new KryptonTextBox();
      this.kryptonButtonAccept = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.openFileDialog1 = new OpenFileDialog();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanel2.BeginInit();
      this.kryptonPanel2.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonAccept);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonPanel2);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(528, 399);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(528, 31);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = ".";
      this.kryptonHeader1.Values.Image = (Image) null;
      this.kryptonTextBoxClienteName.Location = new Point(139, 22);
      this.kryptonTextBoxClienteName.Name = "kryptonTextBoxClienteName";
      this.kryptonTextBoxClienteName.Size = new Size(324, 20);
      this.kryptonTextBoxClienteName.TabIndex = 1;
      this.kryptonTextBoxTelefono.Location = new Point(139, 48);
      this.kryptonTextBoxTelefono.Name = "kryptonTextBoxTelefono";
      this.kryptonTextBoxTelefono.Size = new Size(324, 20);
      this.kryptonTextBoxTelefono.TabIndex = 2;
      this.kryptonTextBoxButtonFax.Location = new Point(139, 74);
      this.kryptonTextBoxButtonFax.Name = "kryptonTextBoxButtonFax";
      this.kryptonTextBoxButtonFax.Size = new Size(324, 20);
      this.kryptonTextBoxButtonFax.TabIndex = 3;
      this.kryptonLabel1.Location = new Point(78, 51);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(58, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Telefono";
      this.kryptonLabel2.Location = new Point(103, 77);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(28, 20);
      this.kryptonLabel2.TabIndex = 5;
      this.kryptonLabel2.Values.Text = "Fax";
      this.kryptonLabel3.Location = new Point(27, 25);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(113, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Nombre Comercial";
      this.kryptonPanel2.Controls.Add((Control) this.pictureBox1);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonButtonFindLogo);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxLogo);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel6);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel5);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxDir);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel4);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxEmail);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxClienteName);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxTelefono);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxButtonFax);
      this.kryptonPanel2.Location = new Point(14, 54);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanel2.Size = new Size(500, 267);
      this.kryptonPanel2.TabIndex = 7;
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBox1.Location = new Point(363, 189);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(100, 72);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 14;
      this.pictureBox1.TabStop = false;
      this.kryptonButtonFindLogo.Location = new Point(140, 219);
      this.kryptonButtonFindLogo.Name = "kryptonButtonFindLogo";
      this.kryptonButtonFindLogo.Size = new Size(169, 25);
      this.kryptonButtonFindLogo.TabIndex = 13;
      this.kryptonButtonFindLogo.Values.Text = "Buscar Logo";
      this.kryptonButtonFindLogo.Click += new EventHandler(this.kryptonButtonFindLogo_Click);
      this.kryptonTextBoxLogo.Location = new Point(140, 193);
      this.kryptonTextBoxLogo.Name = "kryptonTextBoxLogo";
      this.kryptonTextBoxLogo.Size = new Size(169, 20);
      this.kryptonTextBoxLogo.TabIndex = 12;
      this.kryptonLabel6.Location = new Point(93, 193);
      this.kryptonLabel6.Name = "kryptonLabel6";
      this.kryptonLabel6.Size = new Size(38, 20);
      this.kryptonLabel6.TabIndex = 11;
      this.kryptonLabel6.Values.Text = "Logo";
      this.kryptonLabel5.Location = new Point(74, 130);
      this.kryptonLabel5.Name = "kryptonLabel5";
      this.kryptonLabel5.Size = new Size(62, 20);
      this.kryptonLabel5.TabIndex = 10;
      this.kryptonLabel5.Values.Text = "Direccion";
      this.kryptonTextBoxDir.Location = new Point(140, 126);
      this.kryptonTextBoxDir.Multiline = true;
      this.kryptonTextBoxDir.Name = "kryptonTextBoxDir";
      this.kryptonTextBoxDir.Size = new Size(324, 60);
      this.kryptonTextBoxDir.TabIndex = 9;
      this.kryptonLabel4.Location = new Point(93, 104);
      this.kryptonLabel4.Name = "kryptonLabel4";
      this.kryptonLabel4.Size = new Size(40, 20);
      this.kryptonLabel4.TabIndex = 8;
      this.kryptonLabel4.Values.Text = "Email";
      this.kryptonTextBoxEmail.Location = new Point(139, 100);
      this.kryptonTextBoxEmail.Name = "kryptonTextBoxEmail";
      this.kryptonTextBoxEmail.Size = new Size(324, 20);
      this.kryptonTextBoxEmail.TabIndex = 7;
      this.kryptonButtonAccept.Location = new Point(328, 327);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(424, 327);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.openFileDialog1.FileName = "openFileDialog1";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlClienteABM";
      this.Size = new Size(528, 399);
      this.Load += new EventHandler(this.UserControlClienteABM_Load);
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.kryptonPanel2.EndInit();
      this.kryptonPanel2.ResumeLayout(false);
      this.kryptonPanel2.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
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
        if (this.logoChanged)
          this.copyFile();
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
      this.currentCliente = this.objWraper.Save(this.currentCliente != null ? new int?(this.currentCliente.IdCliente) : new int?(), this.kryptonTextBoxClienteName.Text, this.kryptonTextBoxTelefono.Text, this.kryptonTextBoxButtonFax.Text, this.kryptonTextBoxEmail.Text, this.kryptonTextBoxDir.Text, this.kryptonTextBoxLogo.Text);
    }

    private void prepareControls()
    {
      this.objWraper = new ClienteWraper();
      this.currentCliente = this.objWraper.GetCliente(this.ObjSearch);
      this.fillForm();
    }

    private void fillForm()
    {
      if (this.currentCliente == null)
        return;
      this.kryptonTextBoxClienteName.Text = this.currentCliente.NombreCliente;
      this.kryptonTextBoxTelefono.Text = this.currentCliente.Telefono;
      this.kryptonTextBoxButtonFax.Text = this.currentCliente.Telefono;
      this.kryptonTextBoxEmail.Text = this.currentCliente.Email;
      this.kryptonTextBoxDir.Text = this.currentCliente.Dir;
      this.kryptonTextBoxLogo.Text = this.currentCliente.Logo;
      this.pictureBox1.ImageLocation = this.imgFolder + (object) Path.DirectorySeparatorChar + this.currentCliente.Logo;
    }

    private void UserControlClienteABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void kryptonButtonFindLogo_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog1.ShowDialog() != DialogResult.OK || !File.Exists(this.openFileDialog1.FileName))
        return;
      this.kryptonTextBoxLogo.Text = Path.GetFileName(this.openFileDialog1.FileName);
      this.logoChanged = true;
    }

    private void copyFile()
    {
      try
      {
        string str = this.imgFolder + (object) Path.DirectorySeparatorChar + Path.GetFileName(this.openFileDialog1.FileName);
        if (!File.Exists(str))
          File.Copy(this.openFileDialog1.FileName, str, false);
        else
          GUtils.ShowMessage("El archivo existe..");
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error copiando logo", ex);
      }
    }
  }
}
