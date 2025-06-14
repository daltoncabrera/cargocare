using ComponentFactory.Krypton.Toolkit;
using DCM.Core.UI;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public class UserControlCareCargoBase : UserControlBase
  {
    private IContainer components;
    protected KryptonPanel kryptonPanel1;

    public UserControlCareCargoBase()
    {
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
      this.kryptonPanel1 = new KryptonPanel();
      this.kryptonPanel1.BeginInit();
      this.SuspendLayout();
      this.kryptonPanel1.Dock = DockStyle.Fill;
      this.kryptonPanel1.Location = new Point(0, 0);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.Size = new Size(596, 330);
      this.kryptonPanel1.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.kryptonPanel1);
      this.Name = "UserControlBase";
      this.Size = new Size(596, 330);
      this.kryptonPanel1.EndInit();
      this.ResumeLayout(false);
    }
  }
}
