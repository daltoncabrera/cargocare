using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DCM.Core.UI
{
  public partial class UserControlPaginator 
  {
    private int recordsPerPage = 50;
    private int currentPage;
    private int totalPages;
    private int totalRecors;
    private bool firstAdvice;


    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FirstAdvice
    {
      get
      {
        return this.firstAdvice;
      }
      set
      {
        this.firstAdvice = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TotalRecords
    {
      get
      {
        return this.totalRecors;
      }
      set
      {
        this.totalRecors = value;
        this.configPages();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TotalPages
    {
      get
      {
        return this.totalPages;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int RecordsPerPage
    {
      get
      {
        return this.recordsPerPage;
      }
      set
      {
        if (value <= 0)
          return;
        this.recordsPerPage = value;
        this.configPages();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int CurrentPage
    {
      get
      {
        return this.currentPage;
      }
    }

    public event UserControlPaginator.ChangePage OnChangePage;

    public UserControlPaginator()
    {
      this.InitializeComponent();
    }

    private void configPages()
    {
      this.totalPages = (int) Math.Ceiling((Decimal) this.totalRecors / (Decimal) this.recordsPerPage);
      this.currentPage = this.totalPages > 0 ? 1 : 0;
      this.updatePaging(this.FirstAdvice);
    }

    private void buttonFirst_Click(object sender, EventArgs e)
    {
      if (this.currentPage <= 0)
        return;
      this.currentPage = 1;
      this.updatePaging(true);
    }

    private void updatePaging(bool advice = true)
    {
      this.buttonFirst.Enabled = this.buttonPrior.Enabled = this.currentPage > 1;
      this.buttonLast.Enabled = this.buttonNext.Enabled = this.currentPage < this.TotalPages;
      if (this.OnChangePage != null && advice)
        this.OnChangePage(this.currentPage);
      this.updateLabel();
    }

    private void updateLabel()
    {
      this.labelPagesInfo.Text = string.Format("Pag {0} de {1} ({2})", (object) this.currentPage, (object) this.TotalPages, (object) this.TotalRecords);
    }

    private void buttonPrior_Click(object sender, EventArgs e)
    {
      if (this.currentPage <= 0)
        return;
      --this.currentPage;
      this.updatePaging(true);
    }

    private void buttonNext_Click(object sender, EventArgs e)
    {
      if (this.currentPage >= this.TotalPages)
        return;
      ++this.currentPage;
      this.updatePaging(true);
    }

    private void buttonLast_Click(object sender, EventArgs e)
    {
      if (this.currentPage >= this.TotalPages)
        return;
      this.currentPage = this.TotalPages;
      this.updatePaging(true);
    }


    public delegate void ChangePage(int page);
  }
}
