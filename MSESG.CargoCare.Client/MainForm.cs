using ComponentFactory.Krypton.Ribbon;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Client
{
  public partial class MainForm : KryptonForm
  {

    //private FormComponentes frmComponentes;
    //private FormClientes frmClientes;
    //private FormCamion frmCamion;
    //private FormChofer frmChofer;
    //private FormDespacho frmDespacho;
    //private FormProducts frmProducts;
    //private FormPermisos frmPermisos;
    //private FormSellos frmSellos;
    //private FormSystem frmSystem;
    //private FormTipoUsuario frmTipoUsuario;
    //private FormUser frmUser;
    //private FormStatus frmStatus;
    //private FormRecepcion frmRecepcion;
    //private FormSync frmSync;
    //private FormChangePass frmChangePass;
    //private FormTerminales frmTerminales;
    //private FormItemsVerificacion frmItemsVerificacion;

    private UpdaterTrack currentUser;

    ///public UserObject UserObject { get; set; }

    public UpdaterTrack CurrentUser
    {
      get
      {
        return this.currentUser;
      }
      set
      {
        this.currentUser = value;
        this.grandMenu(this.currentUser.Id);
      }
    }

    public MainForm()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void kryptonRibbonGroupButtonCliente_Click(object sender, EventArgs e)
    {
      //if (this.frmClientes == null || this.frmClientes.IsDisposed)
      //  this.frmClientes = new FormClientes();
      //this.frmClientes.MdiParent = (Form) this;
      //this.frmClientes.WindowState = FormWindowState.Normal;
      //this.frmClientes.StartPosition = FormStartPosition.CenterScreen;
      //this.frmClientes.Show();
      //this.frmClientes.StartPosition = FormStartPosition.CenterParent;
      //this.frmClientes.Focus();
    }

    private void kryptonRibbonGroupButtonCambiarClave_Click(object sender, EventArgs e)
    {
      //if (this.frmChangePass == null || this.frmChangePass.IsDisposed)
      //  this.frmChangePass = new FormChangePass(new UserWraper().GetUserById(new int?(Global.CurrentUserID)));
      //this.frmChangePass.MdiParent = (Form) this;
      //this.frmChangePass.WindowState = FormWindowState.Normal;
      //this.frmChangePass.StartPosition = FormStartPosition.CenterScreen;
      //this.frmChangePass.Show();
      //this.frmChangePass.StartPosition = FormStartPosition.CenterParent;
      //this.frmChangePass.Focus();
    }

    private void kryptonRibbonGroupButtonUsuarios_Click(object sender, EventArgs e)
    {
      //if (this.frmUser == null || this.frmUser.IsDisposed)
      //  this.frmUser = new FormUser();
      //this.frmUser.MdiParent = (Form) this;
      //this.frmUser.WindowState = FormWindowState.Normal;
      //this.frmUser.StartPosition = FormStartPosition.CenterScreen;
      //this.frmUser.Show();
      //this.frmUser.StartPosition = FormStartPosition.CenterParent;
      //this.frmUser.Focus();
    }

    private void kryptonRibbonGroupButtonPermisos_Click(object sender, EventArgs e)
    {
      //if (this.frmPermisos == null || this.frmPermisos.IsDisposed)
      //  this.frmPermisos = new FormPermisos();
      //this.frmPermisos.MdiParent = (Form) this;
      //this.frmPermisos.WindowState = FormWindowState.Normal;
      //this.frmPermisos.StartPosition = FormStartPosition.CenterScreen;
      //this.frmPermisos.Show();
      //this.frmPermisos.StartPosition = FormStartPosition.CenterParent;
      //this.frmPermisos.Focus();
    }

    private void kryptonRibbonGroupButtonComponent_Click(object sender, EventArgs e)
    {
      //if (this.frmComponentes == null || this.frmComponentes.IsDisposed)
      //  this.frmComponentes = new FormComponentes();
      //this.frmComponentes.MdiParent = (Form) this;
      //this.frmComponentes.WindowState = FormWindowState.Normal;
      //this.frmComponentes.StartPosition = FormStartPosition.CenterScreen;
      //this.frmComponentes.Show();
      //this.frmComponentes.StartPosition = FormStartPosition.CenterParent;
      //this.frmComponentes.Focus();
    }

    private void kryptonRibbonGroupButtonSellos_Click(object sender, EventArgs e)
    {
      //if (this.frmSellos == null || this.frmSellos.IsDisposed)
      //  this.frmSellos = new FormSellos();
      //this.frmSellos.MdiParent = (Form) this;
      //this.frmSellos.WindowState = FormWindowState.Normal;
      //this.frmSellos.StartPosition = FormStartPosition.CenterScreen;
      //this.frmSellos.Show();
      //this.frmSellos.StartPosition = FormStartPosition.CenterParent;
      //this.frmSellos.Focus();
    }

    private void kryptonRibbonGroupButtonNetStatus_Click(object sender, EventArgs e)
    {
      //if (this.frmStatus == null || this.frmStatus.IsDisposed)
      //  this.frmStatus = new FormStatus();
      //this.frmStatus.MdiParent = (Form) this;
      //this.frmStatus.WindowState = FormWindowState.Normal;
      //this.frmStatus.StartPosition = FormStartPosition.CenterScreen;
      //this.frmStatus.Show();
      //this.frmStatus.StartPosition = FormStartPosition.CenterParent;
      //this.frmStatus.Focus();
    }

    private void kryptonRibbonGroupSync_Click(object sender, EventArgs e)
    {
      //if (this.frmSync == null || this.frmSync.IsDisposed)
      //  this.frmSync = new FormSync();
      //this.frmSync.MdiParent = (Form) this;
      //this.frmSync.WindowState = FormWindowState.Normal;
      //this.frmSync.StartPosition = FormStartPosition.CenterScreen;
      //this.frmSync.Show();
      //this.frmSync.StartPosition = FormStartPosition.CenterParent;
      //this.frmSync.Focus();
    }

    private void kryptonRibbonGroupButtonSalir_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void kryptonRibbonGroupButtonCamion_Click(object sender, EventArgs e)
    {
      //if (this.frmCamion == null || this.frmCamion.IsDisposed)
      //  this.frmCamion = new FormCamion();
      //this.frmCamion.MdiParent = (Form) this;
      //this.frmCamion.WindowState = FormWindowState.Normal;
      //this.frmCamion.StartPosition = FormStartPosition.CenterScreen;
      //this.frmCamion.Show();
      //this.frmCamion.StartPosition = FormStartPosition.CenterParent;
      //this.frmCamion.Focus();
    }

    private void kryptonRibbonGroupProducto_Click(object sender, EventArgs e)
    {
      //if (this.frmProducts == null || this.frmProducts.IsDisposed)
      //  this.frmProducts = new FormProducts();
      //this.frmProducts.MdiParent = (Form) this;
      //this.frmProducts.WindowState = FormWindowState.Normal;
      //this.frmProducts.StartPosition = FormStartPosition.CenterScreen;
      //this.frmProducts.Show();
      //this.frmProducts.StartPosition = FormStartPosition.CenterParent;
      //this.frmProducts.Focus();
    }

    private void kryptonRibbonGroupButtonChofer_Click(object sender, EventArgs e)
    {
      //if (this.frmChofer == null || this.frmChofer.IsDisposed)
      //  this.frmChofer = new FormChofer();
      //this.frmChofer.MdiParent = (Form) this;
      //this.frmChofer.WindowState = FormWindowState.Normal;
      //this.frmChofer.StartPosition = FormStartPosition.CenterScreen;
      //this.frmChofer.Show();
      //this.frmChofer.StartPosition = FormStartPosition.CenterParent;
      //this.frmChofer.Focus();
    }

    private void kryptonRibbonGroupButtonDespacho_Click(object sender, EventArgs e)
    {
      //if (this.frmDespacho == null || this.frmDespacho.IsDisposed)
      //  this.frmDespacho = new FormDespacho();
      //this.frmDespacho.MdiParent = (Form) this;
      //this.frmDespacho.WindowState = FormWindowState.Normal;
      //this.frmDespacho.StartPosition = FormStartPosition.CenterScreen;
      //this.frmDespacho.Show();
      //this.frmDespacho.StartPosition = FormStartPosition.CenterParent;
      //this.frmDespacho.Focus();
    }

    private void kryptonRibbonGroupRecepcion_Click(object sender, EventArgs e)
    {
      //if (this.frmRecepcion == null || this.frmRecepcion.IsDisposed)
      //  this.frmRecepcion = new FormRecepcion();
      //this.frmRecepcion.MdiParent = (Form) this;
      //this.frmRecepcion.WindowState = FormWindowState.Normal;
      //this.frmRecepcion.StartPosition = FormStartPosition.CenterScreen;
      //this.frmRecepcion.Show();
      //this.frmRecepcion.StartPosition = FormStartPosition.CenterParent;
      //this.frmRecepcion.Focus();
    }

    private void kryptonRibbonGroupButtonTerminales_Click(object sender, EventArgs e)
    {
      //if (this.frmTerminales == null || this.frmTerminales.IsDisposed)
      //  this.frmTerminales = new FormTerminales();
      //this.frmTerminales.MdiParent = (Form) this;
      //this.frmTerminales.WindowState = FormWindowState.Normal;
      //this.frmTerminales.StartPosition = FormStartPosition.CenterScreen;
      //this.frmTerminales.Show();
      //this.frmTerminales.StartPosition = FormStartPosition.CenterParent;
      //this.frmTerminales.Focus();
    }

    private void kryptonRibbonGroupButtonItemsVerificacion_Click(object sender, EventArgs e)
    {
      //if (this.frmItemsVerificacion == null || this.frmItemsVerificacion.IsDisposed)
      //  this.frmItemsVerificacion = new FormItemsVerificacion();
      //this.frmItemsVerificacion.MdiParent = (Form) this;
      //this.frmItemsVerificacion.WindowState = FormWindowState.Normal;
      //this.frmItemsVerificacion.StartPosition = FormStartPosition.CenterScreen;
      //this.frmItemsVerificacion.Show();
      //this.frmItemsVerificacion.StartPosition = FormStartPosition.CenterParent;
      //this.frmItemsVerificacion.Focus();
    }

    private void grandMenu(int idUser)
    {
      //List<UserPermisos> userPermisos1 = new PermisoWraper().GetUserPermisos(idUser);
      using (IEnumerator<KryptonRibbonGroupButton> enumerator = this.kryptonRibbon.RibbonTabs.SelectMany((Func<KryptonRibbonTab, IEnumerable<KryptonRibbonGroup>>)(tabs => (IEnumerable<KryptonRibbonGroup>)tabs.Groups), (tabs, groups) => new
      {
        tabs = tabs,
        groups = groups
      }).SelectMany(param0 => (IEnumerable<KryptonRibbonGroupContainer>)param0.groups.Items, (param0, gitems) => gitems).Cast<KryptonRibbonGroupTriple>().SelectMany<KryptonRibbonGroupTriple, KryptonRibbonGroupItem, KryptonRibbonGroupItem>((Func<KryptonRibbonGroupTriple, IEnumerable<KryptonRibbonGroupItem>>)(t => (IEnumerable<KryptonRibbonGroupItem>)t.Items), (Func<KryptonRibbonGroupTriple, KryptonRibbonGroupItem, KryptonRibbonGroupItem>)((t, b) => b)).Cast<KryptonRibbonGroupButton>().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KryptonRibbonGroupButton ctr = enumerator.Current;
          if (ctr.Tag != null)
          {
            //UserPermisos userPermisos2 = userPermisos1.Where<UserPermisos>((Func<UserPermisos, bool>) (s => s.IdComponente.ToString().Equals(ctr.Tag))).Select<UserPermisos, UserPermisos>((Func<UserPermisos, UserPermisos>) (s => s)).FirstOrDefault<UserPermisos>();
            ctr.Enabled = true; // userPermisos2 != null && userPermisos2.Consultar;
          }
        }
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void kryptonRibbonGroupButton2_Click(object sender, EventArgs e)
    {
      // int num = (int) new SellosReportForm().ShowDialog();
    }

    private void kryptonRibbonGroupButton2_Click_1(object sender, EventArgs e)
    {
      //if (this.frmSystem == null || this.frmSystem.IsDisposed)
      //    this.frmSystem = new FormSystem();

      //this.frmSystem.MdiParent = (Form)this;
      //this.frmSystem.WindowState = FormWindowState.Normal;
      //this.frmSystem.StartPosition = FormStartPosition.CenterScreen;
      //this.frmSystem.Show();
      //this.frmSystem.StartPosition = FormStartPosition.CenterParent;
      //this.frmSystem.Focus();
    }
  }
}
