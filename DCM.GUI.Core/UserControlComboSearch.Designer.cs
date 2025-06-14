using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  public partial class UserControlComboSearch : UserControl
  {
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControlComboSearch";
      this.Size = new Size(636, 30);
      this.ResumeLayout(false);
    }
  }
}
