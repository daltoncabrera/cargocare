using ComponentFactory.Krypton.Toolkit;
using DCM.Core;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public class UserControlChangePass : UserControlCareCargoBase
  {
    private user objCurentUser;
    private bool enableOdl;
    private IContainer components;
    private KryptonHeader kryptonHeader1;
    private KryptonPanel kryptonPanel2;
    private KryptonLabel kryptonLabel3;
    private KryptonTextBox kryptonTextBoxOldPass;
    private KryptonLabel kryptonLabel2;
    private KryptonTextBox kryptonTextBoxNewPass;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBoxButtonRepeatPass;
    private KryptonButton kryptonButtonCancel;
    private KryptonButton kryptonButtonAccept;

    public UserControlChangePass(user objUser)
    {
      this.InitializeComponent();
      this.objCurentUser = objUser;
    }

    public void SetOld()
    {
      this.kryptonTextBoxOldPass.Enabled = false;
      this.kryptonTextBoxOldPass.Text = "Aa123";
    }

    private void kryptonButtonCancel_Click(object sender, EventArgs e)
    {
      this.ParentForm.Close();
    }

    private void kryptonButtonAccept_Click(object sender, EventArgs e)
    {
      try
      {
        this.ChangePass(this.kryptonTextBoxOldPass.Text, this.kryptonTextBoxNewPass.Text, this.kryptonTextBoxButtonRepeatPass.Text);
        GUtils.ShowMessage("Clave cambiada con exito..");
        this.ParentForm.DialogResult = DialogResult.OK;
        Application.Restart();
        Environment.Exit(0);
        this.ParentForm.Close();
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error Cambiando Clave", ex);
      }
    }

    public void ChangePass(string oldPass, string newPass, string repeatPass)
    {
      if (!newPass.Equals(repeatPass))
        throw new Exception("La confirmacion de la clave no conincide");
      new UserWraper().ChangePassWord(Global.CurrentUserID, oldPass, newPass);
    }

    private void UserControlChangePass_Load(object sender, EventArgs e)
    {
      if (this.objCurentUser == null || this.objCurentUser.IdUser != Global.CurrentUserID)
        throw new Exception("Solo puede cambiar su propia clave");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlChangePass));
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonTextBoxOldPass = new KryptonTextBox();
      this.kryptonTextBoxNewPass = new KryptonTextBox();
      this.kryptonTextBoxButtonRepeatPass = new KryptonTextBox();
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
      this.kryptonPanel1.PaletteMode = PaletteMode.Office2007Silver;
      this.kryptonPanel1.Size = new Size(438, 267);
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(438, 31);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "Cambio de Password";
      //this.kryptonHeader1.Values.Image = (Image) componentResourceManager.GetObject("kryptonHeader1.Values.Image");
      this.kryptonTextBoxOldPass.Location = new Point(143, 22);
      this.kryptonTextBoxOldPass.Name = "kryptonTextBoxOldPass";
      this.kryptonTextBoxOldPass.PasswordChar = '●';
      this.kryptonTextBoxOldPass.Size = new Size(234, 20);
      this.kryptonTextBoxOldPass.TabIndex = 1;
      this.kryptonTextBoxOldPass.UseSystemPasswordChar = true;
      this.kryptonTextBoxNewPass.Location = new Point(143, 48);
      this.kryptonTextBoxNewPass.Name = "kryptonTextBoxNewPass";
      this.kryptonTextBoxNewPass.PasswordChar = '●';
      this.kryptonTextBoxNewPass.Size = new Size(234, 20);
      this.kryptonTextBoxNewPass.TabIndex = 2;
      this.kryptonTextBoxNewPass.UseSystemPasswordChar = true;
      this.kryptonTextBoxButtonRepeatPass.Location = new Point(143, 74);
      this.kryptonTextBoxButtonRepeatPass.Name = "kryptonTextBoxButtonRepeatPass";
      this.kryptonTextBoxButtonRepeatPass.PasswordChar = '●';
      this.kryptonTextBoxButtonRepeatPass.Size = new Size(234, 20);
      this.kryptonTextBoxButtonRepeatPass.TabIndex = 3;
      this.kryptonTextBoxButtonRepeatPass.UseSystemPasswordChar = true;
      this.kryptonLabel1.Location = new Point(48, 48);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new Size(78, 20);
      this.kryptonLabel1.TabIndex = 4;
      this.kryptonLabel1.Values.Text = "Clave Nueva";
      this.kryptonLabel2.Location = new Point(44, 74);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new Size(82, 20);
      this.kryptonLabel2.TabIndex = 5;
      this.kryptonLabel2.Values.Text = "Repetir Clave ";
      this.kryptonLabel3.Location = new Point(40, 22);
      this.kryptonLabel3.Name = "kryptonLabel3";
      this.kryptonLabel3.Size = new Size(86, 20);
      this.kryptonLabel3.TabIndex = 6;
      this.kryptonLabel3.Values.Text = "Clave Antigua";
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel3);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxOldPass);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel2);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxNewPass);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonLabel1);
      this.kryptonPanel2.Controls.Add((Control) this.kryptonTextBoxButtonRepeatPass);
      this.kryptonPanel2.Location = new Point(14, 47);
      this.kryptonPanel2.Name = "kryptonPanel2";
      this.kryptonPanel2.Size = new Size(411, 128);
      this.kryptonPanel2.TabIndex = 7;
      this.kryptonButtonAccept.Location = new Point(226, 182);
      this.kryptonButtonAccept.Name = "kryptonButtonAccept";
      this.kryptonButtonAccept.Size = new Size(90, 25);
      this.kryptonButtonAccept.TabIndex = 8;
      this.kryptonButtonAccept.Values.Text = "Aceptar";
      this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
      this.kryptonButtonCancel.Location = new Point(333, 182);
      this.kryptonButtonCancel.Name = "kryptonButtonCancel";
      this.kryptonButtonCancel.Size = new Size(90, 25);
      this.kryptonButtonCancel.TabIndex = 9;
      this.kryptonButtonCancel.Values.Text = "Cancelar";
      this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlChangePass";
      this.Size = new Size(438, 267);
      this.Load += new EventHandler(this.UserControlChangePass_Load);
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
