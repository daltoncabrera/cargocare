using DCM.Core.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  [ToolboxItem(false)]
  public partial class UserControlBase
  {
   
    public List<SearchResult> ObjSearch { get; set; }

    public UserControlBase()
    {
      this.InitializeComponent();
    }

   
  }
}
