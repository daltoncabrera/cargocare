using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  public static class GUtils
  {
    public static void ShowControl(Control ctr, string title, Size? size = null)
    {
      if (!size.HasValue || !size.HasValue)
        size = new Size?(ctr.Size);
      Form form = new Form();
      form.Text = title;
      form.Width = size.Value.Width + 10;
      form.Height = size.Value.Height + 20;
      ctr.Dock = DockStyle.Fill;
      form.Controls.Add(ctr);
      form.StartPosition = FormStartPosition.CenterScreen;
      int num = (int) form.ShowDialog();
    }

    public static void ShowError(string msg, Exception ex)
    {
      int num = (int) KryptonMessageBox.Show(ex.Message, msg, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    public static void ShowMessage(string msg)
    {
      int num = (int) KryptonMessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    public static DialogResult ConfirmDgl(string msg)
    {
      return KryptonMessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
    }

    public static int GetAsInt(List<SearchResult> list, bool fireExcepction = true)
    {
      int result = 0;
      if ((list.Count <= 0 || !int.TryParse(list[0].Value.ToString(), out result)) && fireExcepction)
        throw new Exception("Asegurese de llenar el Cliente, Chofer, Camion y Terminal");
      return result;
    }
  }
}
