using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public partial class LoginForm : KryptonForm
  {
    private IContainer components;
    private BindingSource camarasBindingSource;
    private PictureBox pbxLogin;
    private KryptonGroupBox kryptonGroupBox2;
    private KryptonWrapLabel kryptonWrapLabel1;
    private KryptonPanel pnlCredenciales;
    private KryptonLabel kryptonLabel2;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox txtNombreUsuario;
    private KryptonTextBox txtContrasena;
    private KryptonGroupBox kryptonGroupBox1;
    private KryptonButton btnCancelar;
    private KryptonButton btnIniciarSesion;
    private readonly bool m_CanClose;


    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
      this.camarasBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.pbxLogin = new System.Windows.Forms.PictureBox();
      this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
      this.kryptonWrapLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonWrapLabel();
      this.pnlCredenciales = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
      this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
      this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
      this.txtNombreUsuario = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
      this.txtContrasena = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
      this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
      this.btnCancelar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
      this.btnIniciarSesion = new ComponentFactory.Krypton.Toolkit.KryptonButton();
      ((System.ComponentModel.ISupportInitialize)(this.camarasBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
      this.kryptonGroupBox2.Panel.SuspendLayout();
      this.kryptonGroupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pnlCredenciales)).BeginInit();
      this.pnlCredenciales.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
      this.kryptonGroupBox1.Panel.SuspendLayout();
      this.kryptonGroupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pbxLogin
      // 
      this.pbxLogin.BackColor = System.Drawing.Color.Transparent;
      this.pbxLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogin.Image")));
      this.pbxLogin.Location = new System.Drawing.Point(17, 9);
      this.pbxLogin.Name = "pbxLogin";
      this.pbxLogin.Size = new System.Drawing.Size(128, 128);
      this.pbxLogin.TabIndex = 0;
      this.pbxLogin.TabStop = false;
      // 
      // kryptonGroupBox2
      // 
      this.kryptonGroupBox2.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlToolTip;
      this.kryptonGroupBox2.Location = new System.Drawing.Point(12, 12);
      this.kryptonGroupBox2.Name = "kryptonGroupBox2";
      // 
      // kryptonGroupBox2.Panel
      // 
      this.kryptonGroupBox2.Panel.Controls.Add(this.pbxLogin);
      this.kryptonGroupBox2.Size = new System.Drawing.Size(167, 208);
      this.kryptonGroupBox2.TabIndex = 4;
      this.kryptonGroupBox2.Values.Heading = "";
      // 
      // kryptonWrapLabel1
      // 
      this.kryptonWrapLabel1.AutoSize = false;
      this.kryptonWrapLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.kryptonWrapLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
      this.kryptonWrapLabel1.Location = new System.Drawing.Point(23, 11);
      this.kryptonWrapLabel1.Name = "kryptonWrapLabel1";
      this.kryptonWrapLabel1.Size = new System.Drawing.Size(248, 33);
      this.kryptonWrapLabel1.Text = "Ingrese su nombre de usuario y contraseña para iniciar sesión.";
      // 
      // pnlCredenciales
      // 
      this.pnlCredenciales.Controls.Add(this.kryptonLabel2);
      this.pnlCredenciales.Controls.Add(this.kryptonLabel1);
      this.pnlCredenciales.Controls.Add(this.txtNombreUsuario);
      this.pnlCredenciales.Controls.Add(this.txtContrasena);
      this.pnlCredenciales.Location = new System.Drawing.Point(4, 58);
      this.pnlCredenciales.Name = "pnlCredenciales";
      this.pnlCredenciales.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
      this.pnlCredenciales.Size = new System.Drawing.Size(285, 142);
      this.pnlCredenciales.TabIndex = 4;
      // 
      // kryptonLabel2
      // 
      this.kryptonLabel2.Location = new System.Drawing.Point(16, 79);
      this.kryptonLabel2.Name = "kryptonLabel2";
      this.kryptonLabel2.Size = new System.Drawing.Size(83, 19);
      this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kryptonLabel2.TabIndex = 0;
      this.kryptonLabel2.Values.Text = "Contraseña";
      // 
      // kryptonLabel1
      // 
      this.kryptonLabel1.Location = new System.Drawing.Point(16, 10);
      this.kryptonLabel1.Name = "kryptonLabel1";
      this.kryptonLabel1.Size = new System.Drawing.Size(131, 19);
      this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kryptonLabel1.TabIndex = 0;
      this.kryptonLabel1.Values.Text = "Nombre de Usuario";
      // 
      // txtNombreUsuario
      // 
      this.txtNombreUsuario.Location = new System.Drawing.Point(16, 37);
      this.txtNombreUsuario.Name = "txtNombreUsuario";
      this.txtNombreUsuario.Size = new System.Drawing.Size(261, 26);
      this.txtNombreUsuario.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtNombreUsuario.TabIndex = 0;
      // 
      // txtContrasena
      // 
      this.txtContrasena.Location = new System.Drawing.Point(16, 104);
      this.txtContrasena.Name = "txtContrasena";
      this.txtContrasena.PasswordChar = '●';
      this.txtContrasena.Size = new System.Drawing.Size(261, 26);
      this.txtContrasena.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtContrasena.TabIndex = 1;
      this.txtContrasena.UseSystemPasswordChar = true;
      // 
      // kryptonGroupBox1
      // 
      this.kryptonGroupBox1.CaptionEdge = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
      this.kryptonGroupBox1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
      this.kryptonGroupBox1.Location = new System.Drawing.Point(185, 12);
      this.kryptonGroupBox1.Name = "kryptonGroupBox1";
      // 
      // kryptonGroupBox1.Panel
      // 
      this.kryptonGroupBox1.Panel.Controls.Add(this.pnlCredenciales);
      this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonWrapLabel1);
      this.kryptonGroupBox1.Size = new System.Drawing.Size(298, 209);
      this.kryptonGroupBox1.TabIndex = 5;
      this.kryptonGroupBox1.Values.Heading = "";
      // 
      // btnCancelar
      // 
      this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancelar.Location = new System.Drawing.Point(353, 235);
      this.btnCancelar.Name = "btnCancelar";
      this.btnCancelar.Size = new System.Drawing.Size(126, 25);
      this.btnCancelar.TabIndex = 1;
      this.btnCancelar.Values.Text = "Cancelar";
      this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
      // 
      // btnIniciarSesion
      // 
      this.btnIniciarSesion.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnIniciarSesion.Location = new System.Drawing.Point(221, 235);
      this.btnIniciarSesion.Name = "btnIniciarSesion";
      this.btnIniciarSesion.Size = new System.Drawing.Size(126, 25);
      this.btnIniciarSesion.TabIndex = 0;
      this.btnIniciarSesion.Values.Text = "Iniciar Sesión";
      this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
      // 
      // LoginForm
      // 
      this.AcceptButton = this.btnIniciarSesion;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.CancelButton = this.btnCancelar;
      this.ClientSize = new System.Drawing.Size(492, 286);
      this.Controls.Add(this.btnIniciarSesion);
      this.Controls.Add(this.btnCancelar);
      this.Controls.Add(this.kryptonGroupBox1);
      this.Controls.Add(this.kryptonGroupBox2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "LoginForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Inicio Sesión";
      ((System.ComponentModel.ISupportInitialize)(this.camarasBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
      this.kryptonGroupBox2.Panel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
      this.kryptonGroupBox2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pnlCredenciales)).EndInit();
      this.pnlCredenciales.ResumeLayout(false);
      this.pnlCredenciales.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
      this.kryptonGroupBox1.Panel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
      this.kryptonGroupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

  }
}
