using ComponentFactory.Krypton.Toolkit;
using Marhex.CargoCare.DAC;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class FormChangePass : KryptonForm
  {
    private user currentUser;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;

    public bool FillPass { get; set; }

    public FormChangePass(user currentUser)
    {
      this.InitializeComponent();
      this.currentUser = currentUser;
    }

    private void prepareControl()
    {
      UserControlChangePass controlChangePass = new UserControlChangePass(this.currentUser);
      this.DialogResult = DialogResult.No;
      this.Width = controlChangePass.Width + 10;
      this.Height = controlChangePass.Height + 20;
      controlChangePass.Dock = DockStyle.Fill;
      if (this.FillPass)
        controlChangePass.SetOld();
      this.kryptonPanel.Controls.Add((Control) controlChangePass);
      if (this.currentUser == null)
        return;
      this.Text = string.Format("{0} {1}", (object) this.currentUser.Nombre, (object) this.currentUser.Apellido);
    }

    private void FormChangePass_Load(object sender, EventArgs e)
    {
      this.prepareControl();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormChangePass));
      this.kryptonManager = new KryptonManager(this.components);
      this.kryptonPanel = new KryptonPanel();
      this.kryptonPanel.BeginInit();
      this.SuspendLayout();
      this.kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(408, 258);
      this.kryptonPanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(408, 258);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FormChangePass";
      this.Load += new EventHandler(this.FormChangePass_Load);
      this.kryptonPanel.EndInit();
      this.ResumeLayout(false);
    }
  }
}
