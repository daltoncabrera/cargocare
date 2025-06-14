using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  public partial class UserControlGridSearch : UserControl
  {
    private IContainer components;
    private UserControlConditionLine userControlConditionLine1;
    private Panel panel1;
    private UserControlPaginator userControlPaginator1;
    private KryptonPanel kryptonPanel1;
    private KryptonDataGridView dataGridViewContent;
    private KryptonButton buttonEdit;
    private KryptonButton buttonAdd;
    private KryptonButton buttonRefresh;
    private ToolTip toolTip1;
    private KryptonButton buttonDelete;
    private KryptonButton buttonFind;
    private KryptonButton buttonClear;
  }
}
