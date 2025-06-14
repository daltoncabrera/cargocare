using ComponentFactory.Krypton.Ribbon;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
  public partial class MainForm
  {
    private IContainer components;
    private KryptonManager kryptonManager;
    private StatusStrip statusStrip;
    private KryptonRibbon kryptonRibbon;
    private KryptonRibbonTab kryptonRibbonTabSystem;
    private KryptonRibbonGroup kryptonRibbonGroup1;
    private KryptonRibbonGroup kryptonRibbonGroup2;
    private KryptonRibbonGroup kryptonRibbonGroup3;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple2;
    private KryptonRibbonGroupButton kryptonRibbonGroupSync;
    private KryptonRibbonTab kryptonRibbonTabMaintenance;
    private KryptonRibbonTab kryptonRibbonTabTransactions;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple3;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonNetStatus;
    private KryptonRibbonGroup kryptonRibbonGroup5;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple4;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonCliente;
    private KryptonRibbonGroup kryptonRibbonGroup6;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple5;
    private KryptonRibbonGroup kryptonRibbonGroup7;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple6;
    private KryptonRibbonGroup kryptonRibbonGroup8;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple7;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonChofer;
    private KryptonRibbonGroup kryptonRibbonGroup9;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple9;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonCambiarClave;
    private KryptonRibbonGroup kryptonRibbonGroup4;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple8;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonPermisos;
    private KryptonRibbonGroupButton kryptonRibbonGroupButton1;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonUsuarios;
    private KryptonRibbonGroup kryptonRibbonGroup10;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple10;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonSalir;
    private KryptonRibbonGroup kryptonRibbonGroup11;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple11;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonSellos;
    private KryptonRibbonGroup kryptonRibbonGroup12;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple12;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonComponent;
    private KryptonRibbonGroup kryptonRibbonGroup13;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple13;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonDespacho;
    private KryptonRibbonGroup kryptonRibbonGroup14;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple14;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonCarga;
    private KryptonRibbonTab kryptonRibbonTab1;
    private KryptonRibbonTab kryptonRibbonTab2;
    private KryptonRibbonGroup kryptonRibbonGroup15;
    private KryptonRibbonGroup kryptonRibbonGroup16;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple15;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonTerminales;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple16;
    private KryptonRibbonGroupButton kryptonRibbonGroupButtonItemsVerificacion;
    private KryptonRibbonGroupButton kryptonRibbonGroupProducto;
    private KryptonRibbonGroup kryptonRibbonGroup17;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple17;
    private KryptonRibbonGroupButton kryptonRibbonSellosReport;
    private KryptonRibbonGroupButton ribbonGroupButton;
    private KryptonRibbonGroup kryptonRibbonGroup18;
    private KryptonRibbonGroupTriple kryptonRibbonGroupTriple18;
    private KryptonRibbonGroupButton kryptonRibbonGroupButton2;
    private KryptonRibbonGroup kryptonRibbonGroup19;


    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.kryptonRibbon = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
      this.kryptonRibbonTabSystem = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
      this.kryptonRibbonGroup18 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple18 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButton2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup9 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple9 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonCambiarClave = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonUsuarios = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup4 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple8 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonPermisos = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup12 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple12 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonComponent = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup11 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple11 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonSellos = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup15 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple16 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonItemsVerificacion = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonNetStatus = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupSync = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup10 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple10 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonSalir = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonTabMaintenance = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
      this.kryptonRibbonGroup5 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple4 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonCliente = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup6 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple5 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.ribbonGroupButton = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup7 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple6 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupProducto = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup8 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple7 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonChofer = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup16 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple15 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonTerminales = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup19 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonTabTransactions = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
      this.kryptonRibbonGroup13 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple13 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonDespacho = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonGroup14 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple14 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonGroupButtonCarga = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonTab1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
      this.kryptonRibbonGroup17 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
      this.kryptonRibbonGroupTriple17 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
      this.kryptonRibbonSellosReport = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      this.kryptonRibbonTab2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
      this.kryptonRibbonGroupButton1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).BeginInit();
      this.SuspendLayout();
      // 
      // kryptonManager
      // 
      this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2010Silver;
      // 
      // statusStrip
      // 
      this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.statusStrip.Location = new System.Drawing.Point(0, 418);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
      this.statusStrip.Size = new System.Drawing.Size(870, 22);
      this.statusStrip.TabIndex = 0;
      this.statusStrip.Text = "statusStrip1";
      // 
      // kryptonRibbon
      // 
      this.kryptonRibbon.InDesignHelperMode = true;
      this.kryptonRibbon.Name = "kryptonRibbon";
      this.kryptonRibbon.RibbonAppButton.AppButtonVisible = false;
      this.kryptonRibbon.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTabSystem,
            this.kryptonRibbonTabMaintenance,
            this.kryptonRibbonTabTransactions,
            this.kryptonRibbonTab1,
            this.kryptonRibbonTab2});
      this.kryptonRibbon.SelectedTab = this.kryptonRibbonTabSystem;
      this.kryptonRibbon.Size = new System.Drawing.Size(870, 115);
      this.kryptonRibbon.TabIndex = 1;
      // 
      // kryptonRibbonTabSystem
      // 
      this.kryptonRibbonTabSystem.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup18,
            this.kryptonRibbonGroup9,
            this.kryptonRibbonGroup1,
            this.kryptonRibbonGroup4,
            this.kryptonRibbonGroup12,
            this.kryptonRibbonGroup11,
            this.kryptonRibbonGroup15,
            this.kryptonRibbonGroup3,
            this.kryptonRibbonGroup2,
            this.kryptonRibbonGroup10});
      this.kryptonRibbonTabSystem.Text = "Sistema";
      // 
      // kryptonRibbonGroup18
      // 
      this.kryptonRibbonGroup18.Image = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroup18.Image")));
      this.kryptonRibbonGroup18.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple18});
      this.kryptonRibbonGroup18.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple18
      // 
      this.kryptonRibbonGroupTriple18.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton2});
      // 
      // kryptonRibbonGroupButton2
      // 
      this.kryptonRibbonGroupButton2.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButton2.ImageLarge")));
      this.kryptonRibbonGroupButton2.TextLine1 = "Config";
      this.kryptonRibbonGroupButton2.Click += new System.EventHandler(this.kryptonRibbonGroupButton2_Click_1);
      // 
      // kryptonRibbonGroup9
      // 
      this.kryptonRibbonGroup9.DialogBoxLauncher = false;
      this.kryptonRibbonGroup9.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple9});
      this.kryptonRibbonGroup9.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple9
      // 
      this.kryptonRibbonGroupTriple9.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonCambiarClave});
      // 
      // kryptonRibbonGroupButtonCambiarClave
      // 
      this.kryptonRibbonGroupButtonCambiarClave.Enabled = false;
      this.kryptonRibbonGroupButtonCambiarClave.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonCambiarClave.ImageLarge")));
      this.kryptonRibbonGroupButtonCambiarClave.Tag = "1";
      this.kryptonRibbonGroupButtonCambiarClave.TextLine1 = "Cambiar ";
      this.kryptonRibbonGroupButtonCambiarClave.TextLine2 = "Clave";
      this.kryptonRibbonGroupButtonCambiarClave.Click += new System.EventHandler(this.kryptonRibbonGroupButtonCambiarClave_Click);
      // 
      // kryptonRibbonGroup1
      // 
      this.kryptonRibbonGroup1.DialogBoxLauncher = false;
      this.kryptonRibbonGroup1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple1});
      this.kryptonRibbonGroup1.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple1
      // 
      this.kryptonRibbonGroupTriple1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonUsuarios});
      // 
      // kryptonRibbonGroupButtonUsuarios
      // 
      this.kryptonRibbonGroupButtonUsuarios.Enabled = false;
      this.kryptonRibbonGroupButtonUsuarios.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonUsuarios.ImageLarge")));
      this.kryptonRibbonGroupButtonUsuarios.Tag = "2";
      this.kryptonRibbonGroupButtonUsuarios.TextLine1 = "Mantenimiento";
      this.kryptonRibbonGroupButtonUsuarios.TextLine2 = "Usuarios";
      this.kryptonRibbonGroupButtonUsuarios.Click += new System.EventHandler(this.kryptonRibbonGroupButtonUsuarios_Click);
      // 
      // kryptonRibbonGroup4
      // 
      this.kryptonRibbonGroup4.DialogBoxLauncher = false;
      this.kryptonRibbonGroup4.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple8});
      this.kryptonRibbonGroup4.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple8
      // 
      this.kryptonRibbonGroupTriple8.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonPermisos});
      // 
      // kryptonRibbonGroupButtonPermisos
      // 
      this.kryptonRibbonGroupButtonPermisos.Enabled = false;
      this.kryptonRibbonGroupButtonPermisos.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonPermisos.ImageLarge")));
      this.kryptonRibbonGroupButtonPermisos.Tag = "3";
      this.kryptonRibbonGroupButtonPermisos.TextLine1 = "Editar";
      this.kryptonRibbonGroupButtonPermisos.TextLine2 = "Permisos";
      this.kryptonRibbonGroupButtonPermisos.Click += new System.EventHandler(this.kryptonRibbonGroupButtonPermisos_Click);
      // 
      // kryptonRibbonGroup12
      // 
      this.kryptonRibbonGroup12.DialogBoxLauncher = false;
      this.kryptonRibbonGroup12.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple12});
      this.kryptonRibbonGroup12.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple12
      // 
      this.kryptonRibbonGroupTriple12.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonComponent});
      // 
      // kryptonRibbonGroupButtonComponent
      // 
      this.kryptonRibbonGroupButtonComponent.Enabled = false;
      this.kryptonRibbonGroupButtonComponent.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonComponent.ImageLarge")));
      this.kryptonRibbonGroupButtonComponent.Tag = "4";
      this.kryptonRibbonGroupButtonComponent.TextLine1 = "Registrar";
      this.kryptonRibbonGroupButtonComponent.TextLine2 = "Componente";
      this.kryptonRibbonGroupButtonComponent.Click += new System.EventHandler(this.kryptonRibbonGroupButtonComponent_Click);
      // 
      // kryptonRibbonGroup11
      // 
      this.kryptonRibbonGroup11.DialogBoxLauncher = false;
      this.kryptonRibbonGroup11.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple11});
      this.kryptonRibbonGroup11.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple11
      // 
      this.kryptonRibbonGroupTriple11.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonSellos});
      // 
      // kryptonRibbonGroupButtonSellos
      // 
      this.kryptonRibbonGroupButtonSellos.Enabled = false;
      this.kryptonRibbonGroupButtonSellos.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonSellos.ImageLarge")));
      this.kryptonRibbonGroupButtonSellos.Tag = "5";
      this.kryptonRibbonGroupButtonSellos.TextLine1 = "Configuracion de";
      this.kryptonRibbonGroupButtonSellos.TextLine2 = "Sellos";
      this.kryptonRibbonGroupButtonSellos.Click += new System.EventHandler(this.kryptonRibbonGroupButtonSellos_Click);
      // 
      // kryptonRibbonGroup15
      // 
      this.kryptonRibbonGroup15.DialogBoxLauncher = false;
      this.kryptonRibbonGroup15.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple16});
      this.kryptonRibbonGroup15.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple16
      // 
      this.kryptonRibbonGroupTriple16.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonItemsVerificacion});
      // 
      // kryptonRibbonGroupButtonItemsVerificacion
      // 
      this.kryptonRibbonGroupButtonItemsVerificacion.Enabled = false;
      this.kryptonRibbonGroupButtonItemsVerificacion.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonItemsVerificacion.ImageLarge")));
      this.kryptonRibbonGroupButtonItemsVerificacion.Tag = "6";
      this.kryptonRibbonGroupButtonItemsVerificacion.TextLine1 = "Items";
      this.kryptonRibbonGroupButtonItemsVerificacion.TextLine2 = "Verificacion";
      this.kryptonRibbonGroupButtonItemsVerificacion.Click += new System.EventHandler(this.kryptonRibbonGroupButtonItemsVerificacion_Click);
      // 
      // kryptonRibbonGroup3
      // 
      this.kryptonRibbonGroup3.DialogBoxLauncher = false;
      this.kryptonRibbonGroup3.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple3});
      this.kryptonRibbonGroup3.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple3
      // 
      this.kryptonRibbonGroupTriple3.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonNetStatus});
      // 
      // kryptonRibbonGroupButtonNetStatus
      // 
      this.kryptonRibbonGroupButtonNetStatus.Enabled = false;
      this.kryptonRibbonGroupButtonNetStatus.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonNetStatus.ImageLarge")));
      this.kryptonRibbonGroupButtonNetStatus.Tag = "7";
      this.kryptonRibbonGroupButtonNetStatus.TextLine1 = "Estatus";
      this.kryptonRibbonGroupButtonNetStatus.TextLine2 = "Conexion";
      this.kryptonRibbonGroupButtonNetStatus.Click += new System.EventHandler(this.kryptonRibbonGroupButtonNetStatus_Click);
      // 
      // kryptonRibbonGroup2
      // 
      this.kryptonRibbonGroup2.DialogBoxLauncher = false;
      this.kryptonRibbonGroup2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple2});
      this.kryptonRibbonGroup2.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple2
      // 
      this.kryptonRibbonGroupTriple2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupSync});
      // 
      // kryptonRibbonGroupSync
      // 
      this.kryptonRibbonGroupSync.Enabled = false;
      this.kryptonRibbonGroupSync.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupSync.ImageLarge")));
      this.kryptonRibbonGroupSync.Tag = "8";
      this.kryptonRibbonGroupSync.TextLine1 = "Sincronizar";
      this.kryptonRibbonGroupSync.TextLine2 = "Sistema";
      this.kryptonRibbonGroupSync.Click += new System.EventHandler(this.kryptonRibbonGroupSync_Click);
      // 
      // kryptonRibbonGroup10
      // 
      this.kryptonRibbonGroup10.DialogBoxLauncher = false;
      this.kryptonRibbonGroup10.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple10});
      this.kryptonRibbonGroup10.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple10
      // 
      this.kryptonRibbonGroupTriple10.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonSalir});
      // 
      // kryptonRibbonGroupButtonSalir
      // 
      this.kryptonRibbonGroupButtonSalir.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonSalir.ImageLarge")));
      this.kryptonRibbonGroupButtonSalir.TextLine1 = "Salir del";
      this.kryptonRibbonGroupButtonSalir.TextLine2 = "Sistema";
      this.kryptonRibbonGroupButtonSalir.Click += new System.EventHandler(this.kryptonRibbonGroupButtonSalir_Click);
      // 
      // kryptonRibbonTabMaintenance
      // 
      this.kryptonRibbonTabMaintenance.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup5,
            this.kryptonRibbonGroup6,
            this.kryptonRibbonGroup7,
            this.kryptonRibbonGroup8,
            this.kryptonRibbonGroup16,
            this.kryptonRibbonGroup19});
      this.kryptonRibbonTabMaintenance.Text = "Mantenimientos";
      // 
      // kryptonRibbonGroup5
      // 
      this.kryptonRibbonGroup5.DialogBoxLauncher = false;
      this.kryptonRibbonGroup5.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple4});
      this.kryptonRibbonGroup5.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple4
      // 
      this.kryptonRibbonGroupTriple4.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonCliente});
      // 
      // kryptonRibbonGroupButtonCliente
      // 
      this.kryptonRibbonGroupButtonCliente.Enabled = false;
      this.kryptonRibbonGroupButtonCliente.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonCliente.ImageLarge")));
      this.kryptonRibbonGroupButtonCliente.Tag = "13";
      this.kryptonRibbonGroupButtonCliente.TextLine1 = "Agregar / Modificar";
      this.kryptonRibbonGroupButtonCliente.TextLine2 = "Cliente";
      this.kryptonRibbonGroupButtonCliente.Click += new System.EventHandler(this.kryptonRibbonGroupButtonCliente_Click);
      // 
      // kryptonRibbonGroup6
      // 
      this.kryptonRibbonGroup6.DialogBoxLauncher = false;
      this.kryptonRibbonGroup6.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple5});
      this.kryptonRibbonGroup6.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple5
      // 
      this.kryptonRibbonGroupTriple5.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.ribbonGroupButton});
      // 
      // ribbonGroupButton
      // 
      this.ribbonGroupButton.Enabled = false;
      this.ribbonGroupButton.ImageLarge = ((System.Drawing.Image)(resources.GetObject("ribbonGroupButton.ImageLarge")));
      this.ribbonGroupButton.Tag = "10";
      this.ribbonGroupButton.TextLine1 = "Agregar / Modificar";
      this.ribbonGroupButton.TextLine2 = "Camion";
      this.ribbonGroupButton.Click += new System.EventHandler(this.kryptonRibbonGroupButtonCamion_Click);
      // 
      // kryptonRibbonGroup7
      // 
      this.kryptonRibbonGroup7.DialogBoxLauncher = false;
      this.kryptonRibbonGroup7.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple6});
      this.kryptonRibbonGroup7.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple6
      // 
      this.kryptonRibbonGroupTriple6.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupProducto});
      // 
      // kryptonRibbonGroupProducto
      // 
      this.kryptonRibbonGroupProducto.Enabled = false;
      this.kryptonRibbonGroupProducto.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupProducto.ImageLarge")));
      this.kryptonRibbonGroupProducto.Tag = "11";
      this.kryptonRibbonGroupProducto.TextLine1 = "Agregar / Modificar";
      this.kryptonRibbonGroupProducto.TextLine2 = "Producto";
      this.kryptonRibbonGroupProducto.Click += new System.EventHandler(this.kryptonRibbonGroupProducto_Click);
      // 
      // kryptonRibbonGroup8
      // 
      this.kryptonRibbonGroup8.DialogBoxLauncher = false;
      this.kryptonRibbonGroup8.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple7});
      this.kryptonRibbonGroup8.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple7
      // 
      this.kryptonRibbonGroupTriple7.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonChofer});
      // 
      // kryptonRibbonGroupButtonChofer
      // 
      this.kryptonRibbonGroupButtonChofer.Enabled = false;
      this.kryptonRibbonGroupButtonChofer.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonChofer.ImageLarge")));
      this.kryptonRibbonGroupButtonChofer.Tag = "9";
      this.kryptonRibbonGroupButtonChofer.TextLine1 = "Agregar / Modificar";
      this.kryptonRibbonGroupButtonChofer.TextLine2 = "Chofer";
      this.kryptonRibbonGroupButtonChofer.Click += new System.EventHandler(this.kryptonRibbonGroupButtonChofer_Click);
      // 
      // kryptonRibbonGroup16
      // 
      this.kryptonRibbonGroup16.DialogBoxLauncher = false;
      this.kryptonRibbonGroup16.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple15});
      this.kryptonRibbonGroup16.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple15
      // 
      this.kryptonRibbonGroupTriple15.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonTerminales});
      // 
      // kryptonRibbonGroupButtonTerminales
      // 
      this.kryptonRibbonGroupButtonTerminales.Enabled = false;
      this.kryptonRibbonGroupButtonTerminales.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonTerminales.ImageLarge")));
      this.kryptonRibbonGroupButtonTerminales.Tag = "12";
      this.kryptonRibbonGroupButtonTerminales.TextLine1 = "Agregar / Modificar";
      this.kryptonRibbonGroupButtonTerminales.TextLine2 = "Terminales";
      this.kryptonRibbonGroupButtonTerminales.Click += new System.EventHandler(this.kryptonRibbonGroupButtonTerminales_Click);
      // 
      // kryptonRibbonTabTransactions
      // 
      this.kryptonRibbonTabTransactions.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup13,
            this.kryptonRibbonGroup14});
      this.kryptonRibbonTabTransactions.Text = "Trasacciones";
      // 
      // kryptonRibbonGroup13
      // 
      this.kryptonRibbonGroup13.DialogBoxLauncher = false;
      this.kryptonRibbonGroup13.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple13});
      this.kryptonRibbonGroup13.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple13
      // 
      this.kryptonRibbonGroupTriple13.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonDespacho});
      // 
      // kryptonRibbonGroupButtonDespacho
      // 
      this.kryptonRibbonGroupButtonDespacho.Enabled = false;
      this.kryptonRibbonGroupButtonDespacho.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonDespacho.ImageLarge")));
      this.kryptonRibbonGroupButtonDespacho.Tag = "14";
      this.kryptonRibbonGroupButtonDespacho.TextLine1 = "Despacho de";
      this.kryptonRibbonGroupButtonDespacho.TextLine2 = "Carga";
      this.kryptonRibbonGroupButtonDespacho.Click += new System.EventHandler(this.kryptonRibbonGroupButtonDespacho_Click);
      // 
      // kryptonRibbonGroup14
      // 
      this.kryptonRibbonGroup14.DialogBoxLauncher = false;
      this.kryptonRibbonGroup14.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple14});
      this.kryptonRibbonGroup14.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple14
      // 
      this.kryptonRibbonGroupTriple14.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButtonCarga});
      // 
      // kryptonRibbonGroupButtonCarga
      // 
      this.kryptonRibbonGroupButtonCarga.Enabled = false;
      this.kryptonRibbonGroupButtonCarga.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButtonCarga.ImageLarge")));
      this.kryptonRibbonGroupButtonCarga.Tag = "15";
      this.kryptonRibbonGroupButtonCarga.TextLine1 = "Recepcion de";
      this.kryptonRibbonGroupButtonCarga.TextLine2 = "Carga";
      this.kryptonRibbonGroupButtonCarga.Click += new System.EventHandler(this.kryptonRibbonGroupRecepcion_Click);
      // 
      // kryptonRibbonTab1
      // 
      this.kryptonRibbonTab1.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup17});
      this.kryptonRibbonTab1.Text = "Reportes";
      // 
      // kryptonRibbonGroup17
      // 
      this.kryptonRibbonGroup17.DialogBoxLauncher = false;
      this.kryptonRibbonGroup17.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple17});
      this.kryptonRibbonGroup17.TextLine1 = " ";
      // 
      // kryptonRibbonGroupTriple17
      // 
      this.kryptonRibbonGroupTriple17.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonSellosReport});
      // 
      // kryptonRibbonSellosReport
      // 
      this.kryptonRibbonSellosReport.Enabled = false;
      this.kryptonRibbonSellosReport.Tag = "16";
      this.kryptonRibbonSellosReport.TextLine1 = "Reporte Sellos";
      this.kryptonRibbonSellosReport.Click += new System.EventHandler(this.kryptonRibbonGroupButton2_Click);
      // 
      // kryptonRibbonTab2
      // 
      this.kryptonRibbonTab2.Text = "Ayuda";
      // 
      // kryptonRibbonGroupButton1
      // 
      this.kryptonRibbonGroupButton1.TextLine1 = "Permisos";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(870, 440);
      this.Controls.Add(this.kryptonRibbon);
      this.Controls.Add(this.statusStrip);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.Name = "MainForm";
      this.Text = "CargoCare";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.MainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

  }
}
