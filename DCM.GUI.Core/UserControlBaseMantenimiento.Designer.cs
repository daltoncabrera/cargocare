using System.ComponentModel;
using System.Windows.Forms;


namespace DCM.Core.UI
{
  public partial class UserControlBaseMantenimiento : UserControlBase
  {
    private IContainer components;
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer)new Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }
  }
}
