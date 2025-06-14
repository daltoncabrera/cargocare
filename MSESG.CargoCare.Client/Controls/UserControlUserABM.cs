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
  public class UserControlUserABM : UserControlCareCargoBase
  {
    private UserWraper objUserWraper;
    private user currentUser;
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxLogin;
    private KryptonLabel kryptonLabel2;
    private KryptonTextBox kryptonTextBoxNombre;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBoxApellido;
    private KryptonLabel kryptonLabel6;
    private KryptonTextBox kryptonTextBoxMail;
    private KryptonLabel kryptonLabel5;
    private KryptonTextBox kryptonTextBoxCell;
    private KryptonLabel kryptonLabel4;
    private KryptonTextBox kryptonTextBoxTelefono;
    private KryptonLabel Direccion;
    private KryptonTextBox kryptonTextBoxDir;
    private KryptonButton kryptonButtonResetPassWord;

    public UserControlUserABM(List<SearchResult> objSearch = null)
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
      this.currentUser = this.objUserWraper.Save(this.currentUser != null ? new int?(this.currentUser.IdUser) : new int?(), this.kryptonTextBoxLogin.Text, this.kryptonTextBoxNombre.Text, this.kryptonTextBoxApellido.Text, this.kryptonTextBoxTelefono.Text, this.kryptonTextBoxCell.Text, this.kryptonTextBoxMail.Text, this.kryptonTextBoxDir.Text, 1);
    }

    private void prepareControls()
    {
      this.objUserWraper = new UserWraper();
      this.currentUser = this.objUserWraper.GetUser(this.ObjSearch);
      this.fillForm();
    }

    private void fillForm()
    {
      if (this.currentUser != null)
      {
        this.kryptonTextBoxLogin.Text = this.currentUser.Login;
        this.kryptonTextBoxNombre.Text = this.currentUser.Nombre;
        this.kryptonTextBoxApellido.Text = this.currentUser.Apellido;
        this.kryptonTextBoxTelefono.Text = this.currentUser.Telefono;
        this.kryptonTextBoxCell.Text = this.currentUser.Celular;
        this.kryptonTextBoxMail.Text = this.currentUser.Correo;
        this.kryptonTextBoxDir.Text = this.currentUser.Direccion;
      }
      this.kryptonButtonResetPassWord.Enabled = this.currentUser != null;
    }

    private void UserControlUserABM_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
    {
    }

    private void kryptonButtonResetPassWord_Click(object sender, EventArgs e)
    {
      if (this.currentUser == null)
        throw new Exception("Elija un usuario para cambiarle la clave");
      try
      {
        if (KryptonMessageBox.Show("Confirmar reinicio de clave", "Esta seguro que desde reinicar la clave del usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) != DialogResult.Yes)
          return;
        this.objUserWraper.ResetPassWord(this.currentUser.IdUser);
        GUtils.ShowMessage("La clave fue reinicida");
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error Reiniciando clave", ex);
      }
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
      this.kryptonTextBoxLogin = new KryptonTextBox();
      this.kryptonTextBoxNombre = new KryptonTextBox();
      this.kryptonTextBoxApellido = new KryptonTextBox();
      this.kryptonLabel1 = new KryptonLabel();
      this.kryptonLabel2 = new KryptonLabel();
      this.kryptonLabel3 = new KryptonLabel();
      this.kryptonPanel2 = new KryptonPanel();
      this.Direccion = new KryptonLabel();
      this.kryptonTextBoxDir = new KryptonTextBox();
      this.kryptonLabel6 = new KryptonLabel();
      this.kryptonTextBoxMail = new KryptonTextBox();
      this.kryptonLabel5 = new KryptonLabel();
      this.kryptonTextBoxCell = new KryptonTextBox();
      this.kryptonLabel4 = new KryptonLabel();
      this.kryptonTextBoxTelefono = new KryptonTextBox();
      this.kryptonButtonAccept = new KryptonButton();
      this.kryptonButtonCancel = new KryptonButton();
      this.kryptonButtonResetPassWord = new KryptonButton();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonPanel2.BeginInit();
      this.kryptonPanel2.SuspendLayout();
      this.SuspendLayout();
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonResetPassWord);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonCancel);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonButtonAccept);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonPanel2);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanel1.Size = new Size(449, 375);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(449, 20);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "";
      this.kryptonTextBoxLogin.Location = new Point(122, 22);
      this.kryptonTextBoxLogin.Name = "kryptonTextBoxLogin";
      this.kryptonTextBoxLogin.Size = new Size(234, 20);
      this.kryptonTextBoxLogin.TabIndex = 1;
      this.kryptonTextBoxNombre.Location = new Point(122, 48);
      this.kryptonTextBoxNombre.Name = "kryptonTextBoxNombre";
      this.kryptonTextBoxNombre.Size = new Size(234, 20);
      this.kryptonTextBoxNombre.TabIndex = 2;
      this.kryptonTextBoxApellido.Location = new Point(122, 74);
      this.kryptonTextBoxApellido.Name = "kryptonTextBoxApellido";
      this.kryptonTextBoxApellido.Size = new Size(234, 20);
      this.kryptonTextBoxApellido.TabIndex = 3;
      this.kryptonLabel1.Location = new Point(55, 48);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(56, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Nombre";
      this.kryptonLabel2.Location = new Point(55, 74);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(56, 20);
      this.kryptonLabel2.TabIndex = 5;
      this.kryptonLabel2.Values.Text = "Apellido";
      this.kryptonLabel3.Location = new Point(70, 22);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(41, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Login";
      this.kryptonPanel2.Controls.Add((Control) this.Direccion);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxDir);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel6);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxMail);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel5);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxCell);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel4);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxTelefono);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxLogin);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxNombre);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxApellido);
      this.kryptonPanel2.Location = new Point(14, 59);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanel2.Size = new Size(418, 256);
      this.kryptonPanel2.TabIndex = 7;
      this.kryptonPanel2.Paint += new PaintEventHandler(this.kryptonPanel2_Paint);
      this.Direccion.Location = new Point(49, 178);
      this.Direccion.Name = "Direccion";
      this.Direccion.Size = new Size(62, 20);
      this.Direccion.TabIndex = 14;
      this.Direccion.Values.Text = "Direccion";
      this.kryptonTextBoxDir.Location = new Point(122, 178);
      this.kryptonTextBoxDir.Multiline = true;
      this.kryptonTextBoxDir.Name = "kryptonTextBoxDir";
      this.kryptonTextBoxDir.Size = new Size(234, 61);
      this.kryptonTextBoxDir.TabIndex = 13;
      this.kryptonLabel6.Location = new Point(71, 152);
      this.kryptonLabel6.Name = "kryptonLabel6";
      this.kryptonLabel6.Size = new Size(40, 20);
      this.kryptonLabel6.TabIndex = 12;
      this.kryptonLabel6.Values.Text = "Email";
      this.kryptonTextBoxMail.Location = new Point(122, 152);
      this.kryptonTextBoxMail.Name = "kryptonTextBoxMail";
      this.kryptonTextBoxMail.Size = new Size(234, 20);
      this.kryptonTextBoxMail.TabIndex = 11;
      this.kryptonLabel5.Location = new Point(63, 126);
      this.kryptonLabel5.Name = "kryptonLabel5";
      this.kryptonLabel5.Size = new Size(48, 20);
      this.kryptonLabel5.TabIndex = 10;
      this.kryptonLabel5.Values.Text = "Celular";
      this.kryptonTextBoxCell.Location = new Point(122, 126);
      this.kryptonTextBoxCell.Name = "kryptonTextBoxCell";
      this.kryptonTextBoxCell.Size = new Size(234, 20);
      this.kryptonTextBoxCell.TabIndex = 9;
      this.kryptonLabel4.Location = new Point(53, 100);
      this.kryptonLabel4.Name = "kryptonLabel4";
      this.kryptonLabel4.Size = new Size(58, 20);
      this.kryptonLabel4.TabIndex = 8;
      this.kryptonLabel4.Values.Text = "Telefono";
      this.kryptonTextBoxTelefono.Location = new Point(122, 100);
      this.kryptonTextBoxTelefono.Name = "kryptonTextBoxTelefono";
      this.kryptonTextBoxTelefono.Size = new Size(234, 20);
      this.kryptonTextBoxTelefono.TabIndex = 7;
      this.kryptonButtonAccept.Location = new Point(233, 321);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(342, 321);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.kryptonButtonResetPassWord.ButtonStyle = ButtonStyle.Alternate;
      this.kryptonButtonResetPassWord.Enabled = false;
      this.kryptonButtonResetPassWord.Location = new Point(233, 28);
      this.kryptonButtonResetPassWord.Name = "kryptonButtonResetPassWord";
      this.kryptonButtonResetPassWord.Size = new Size(199, 25);
      this.kryptonButtonResetPassWord.TabIndex = 10;
      this.kryptonButtonResetPassWord.Values.Text = "Resetear Clave  de Usuario";
      this.kryptonButtonResetPassWord.Click += new EventHandler(this.kryptonButtonResetPassWord_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlUserABM";
      this.Size = new Size(449, 375);
      this.Load += new EventHandler(this.UserControlUserABM_Load);
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
