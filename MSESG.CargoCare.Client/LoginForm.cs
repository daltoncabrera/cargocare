// Decompiled with JetBrains decompiler
// Type: Marhex.CargoCare.Begin.LoginForm
// Assembly: CargoCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 10AF5501-14E5-4B62-980B-B0E074D7AD20
// Assembly location: C:\Users\dalto\Desktop\Update_Marhex_ CargoCare_19-01-2016\CargoCare.exe

using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Client
{
  public partial class LoginForm 
  {
   
    private MainForm mainForm;
    private UpdaterTrack currentUserObject;

    public LoginForm(MainForm mainForm)
    {
      this.InitializeComponent();
      this.DialogResult = DialogResult.No;
      this.mainForm = mainForm;
    }

 

  
    protected override void OnClosing(CancelEventArgs e)
    {
    }

    private void btnIniciarSesion_Click(object sender, EventArgs e)
    {
      //PermisoWraper permisoWraper = new PermisoWraper();
      try
      {
        //  if (string.IsNullOrEmpty(this.txtNombreUsuario.Text) || string.IsNullOrEmpty(this.txtContrasena.Text))
        //  {
        //    int num1 = (int) KryptonMessageBox.Show("Verique que ni el usuario, ni la contraseña este en blanco", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        //  }
        //  else
        //  {
        //    user userByCredenciales = permisoWraper.GetUserByCredenciales(this.txtNombreUsuario.Text, this.txtContrasena.Text);
        //    if (userByCredenciales != null)
        //    {
        //      this.setUserObject(userByCredenciales);
        //      this.DialogResult = DialogResult.OK;
        //      this.mainForm.CurrentUser = userByCredenciales;
        //      this.DialogResult = DialogResult.OK;
        //      this.Close();
        //    }
        //    else
        //    {
        //      int num2 = (int) KryptonMessageBox.Show("Este usuario no tiene acceso, verifique su nombre de usuario y contraseña y vuelva a intentarlo.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        //      this.txtContrasena.Clear();
        //      this.txtContrasena.Focus();
        //    }
        //  }
      }
      catch (Exception ex)
      {
        int num = (int) KryptonMessageBox.Show("Este usuario no tiene acceso, verifique su nombre de usuario y contraseña y vuelva a intentarlo.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void setUserObject(UpdaterTrack user)
    {
     // this.currentUserObject = new UserObject();
      //this.currentUserObject.IDUsuario = user.IdUser;
      //this.currentUserObject.Nombre = user.Nombre;
      //this.currentUserObject.Apellido = user.Apellido;
      //this.currentUserObject.Pass = user.Password;
      //this.currentUserObject.ChangePass = user.ChangePass;
      //this.mainForm.UserObject = this.currentUserObject;
      //Global.SetCurrentUser(this.currentUserObject);
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
