namespace MSESG.CargoCare.Client
{
  partial class Main
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.kryptonRibbon1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
      this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
      this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
      this.SuspendLayout();
      // 
      // kryptonRibbon1
      // 
      this.kryptonRibbon1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
      this.kryptonRibbon1.AllowButtonSpecToolTips = true;
      this.kryptonRibbon1.InDesignHelperMode = true;
      this.kryptonRibbon1.MinimizedMode = true;
      this.kryptonRibbon1.Name = "kryptonRibbon1";
      this.kryptonRibbon1.QATLocation = ComponentFactory.Krypton.Ribbon.QATLocation.Below;
      this.kryptonRibbon1.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1});
      this.kryptonRibbon1.RibbonAppButton.AppButtonToolTipImage = global::MSESG.CargoCare.Client.Properties.Resources.icon;
      this.kryptonRibbon1.RibbonAppButton.AppButtonToolTipTitle = "MyMenu";
      this.kryptonRibbon1.RibbonStyles.QATButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.BreadCrumb;
      this.kryptonRibbon1.RibbonStyles.ScrollerStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Form;
      this.kryptonRibbon1.SelectedTab = null;
      this.kryptonRibbon1.ShowMinimizeButton = false;
      this.kryptonRibbon1.Size = new System.Drawing.Size(933, 142);
      this.kryptonRibbon1.TabIndex = 0;
      // 
      // kryptonContextMenuItem1
      // 
      this.kryptonContextMenuItem1.Text = "Menu Item";
      // 
      // kryptonManager1
      // 
      this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.SparklePurple;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(933, 434);
      this.Controls.Add(this.kryptonRibbon1);
      this.Name = "Main";
      this.Text = "Main";
      ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
    public ComponentFactory.Krypton.Ribbon.KryptonRibbon kryptonRibbon1;
    private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
  }
}