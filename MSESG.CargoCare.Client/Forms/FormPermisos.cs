using ComponentFactory.Krypton.Toolkit;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client.Forms
{
  public class FormPermisos : KryptonForm
  {
    private cargocareEntities db;
    private PermisoWraper objPermisosWraper;
    private UserWraper objUserWraper;
    private List<UserPermisos> objUserPermisos;
    private IContainer components;
    private KryptonManager kryptonManager;
    private KryptonPanel kryptonPanel;
    private KryptonButton kryptonButtonCerrar;
    private KryptonButton kryptonButtonGuardar;
    private KryptonPanel kryptonPanelPermisos;
    private KryptonHeader kryptonHeader1;
    private KryptonPanel kryptonPanel1;
    private KryptonHeader kryptonHeader2;
    private KryptonDataGridView kryptonDataGridViewPermisos;
    private KryptonComboBox kryptonComboBoxUsers;

    public FormPermisos()
    {
      this.InitializeComponent();
    }

    private void prepareControls()
    {
      this.db = new cargocareEntities();
      this.objUserWraper = new UserWraper();
      this.objPermisosWraper = new PermisoWraper();
      this.fillComboUsers();
    }

    private void fillComboUsers()
    {
      this.kryptonComboBoxUsers.DataSource = (object) this.objUserWraper.GetUsersNameValue();
      this.kryptonComboBoxUsers.SelectedIndex = -1;
      this.kryptonComboBoxUsers.ValueMember = "Value";
      this.kryptonComboBoxUsers.DisplayMember = "Name";
    }

    private void userControlGridSearchPermiso_OnDeleteEntity(List<SearchResult> selectedResult)
    {
      throw new NotImplementedException();
    }

    private void FormPermisos_Load(object sender, EventArgs e)
    {
      this.prepareControls();
    }

    private void kryptonComboBoxUsers_SelectedValueChanged(object sender, EventArgs e)
    {
      if (this.kryptonComboBoxUsers.SelectedValue != null)
      {
        int result = 0;
        if (!int.TryParse(this.kryptonComboBoxUsers.SelectedValue.ToString(), out result) || result <= 0)
          return;
        this.fillDatagridPermisos(result);
      }
      else
        this.kryptonDataGridViewPermisos.DataSource = (object) null;
    }

    private void fillDatagridPermisos(int idUsuario)
    {
      this.objUserPermisos = this.objPermisosWraper.GetUserPermisos(idUsuario);
      this.kryptonDataGridViewPermisos.DataSource = (object) this.objUserPermisos;
      this.kryptonDataGridViewPermisos.Columns["IdPermiso"].Visible = false;
      this.kryptonDataGridViewPermisos.Columns["IdComponente"].Visible = false;
      this.kryptonDataGridViewPermisos.Columns["NombreComponente"].Width = 250;
      this.kryptonDataGridViewPermisos.Columns["NombreComponente"].ReadOnly = true;
    }

    private void kryptonButtonGuardar_Click(object sender, EventArgs e)
    {
      try
      {
        int result = 0;
        if (!int.TryParse(this.kryptonComboBoxUsers.SelectedValue.ToString(), out result) || result <= 0)
          return;
        this.objPermisosWraper.UpadtePermisos(result, this.objUserPermisos);
        GUtils.ShowMessage("Permisos modificados");
        this.fillDatagridPermisos(result);
      }
      catch (Exception ex)
      {
        GUtils.ShowError("Error guardando permisos", ex);
      }
    }

    private void kryptonButtonCerrar_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.kryptonManager = new KryptonManager(this.components);
      this.kryptonPanel = new KryptonPanel();
      this.kryptonButtonCerrar = new KryptonButton();
      this.kryptonButtonGuardar = new KryptonButton();
      this.kryptonPanelPermisos = new KryptonPanel();
      this.kryptonDataGridViewPermisos = new KryptonDataGridView();
      this.kryptonHeader1 = new KryptonHeader();
      this.kryptonPanel1 = new KryptonPanel();
      this.kryptonComboBoxUsers = new KryptonComboBox();
      this.kryptonHeader2 = new KryptonHeader();
      this.kryptonPanel.BeginInit();
      this.kryptonPanel.SuspendLayout();
      this.kryptonPanelPermisos.BeginInit();
      this.kryptonPanelPermisos.SuspendLayout();
      //this.kryptonDataGridViewPermisos.BeginInit();
      this.kryptonPanel1.BeginInit();
      this.kryptonPanel1.SuspendLayout();
      this.kryptonComboBoxUsers.BeginInit();
      this.SuspendLayout();
      this.kryptonPanel.Controls.Add((Control) this.kryptonButtonCerrar);
      this.kryptonPanel.Controls.Add((Control) this.kryptonButtonGuardar);
      this.kryptonPanel.Controls.Add((Control) this.kryptonPanelPermisos);
      this.kryptonPanel.Controls.Add((Control) this.kryptonPanel1);
      this.kryptonPanel.Dock = DockStyle.Fill;
      this.kryptonPanel.Location = new Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new Size(659, 416);
      this.kryptonPanel.TabIndex = 0;
      this.kryptonButtonCerrar.Location = new Point(528, 382);
      this.kryptonButtonCerrar.Name = "kryptonButtonCerrar";
      this.kryptonButtonCerrar.Size = new Size(119, 25);
      this.kryptonButtonCerrar.TabIndex = 3;
      this.kryptonButtonCerrar.Values.Text = "Cerrar";
      this.kryptonButtonCerrar.Click += new EventHandler(this.kryptonButtonCerrar_Click);
      this.kryptonButtonGuardar.Location = new Point(391, 384);
      this.kryptonButtonGuardar.Name = "kryptonButtonGuardar";
      this.kryptonButtonGuardar.Size = new Size(131, 25);
      this.kryptonButtonGuardar.TabIndex = 2;
      this.kryptonButtonGuardar.Values.Text = "Guardar Cambios";
      this.kryptonButtonGuardar.Click += new EventHandler(this.kryptonButtonGuardar_Click);
      this.kryptonPanelPermisos.Controls.Add((Control) this.kryptonDataGridViewPermisos);
      this.kryptonPanelPermisos.Controls.Add((Control) this.kryptonHeader1);
      this.kryptonPanelPermisos.Location = new Point(13, 114);
      this.kryptonPanelPermisos.Name = "kryptonPanelPermisos";
      this.kryptonPanelPermisos.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanelPermisos.Size = new Size(634, 262);
      this.kryptonPanelPermisos.TabIndex = 1;
      this.kryptonDataGridViewPermisos.AllowUserToAddRows = false;
      this.kryptonDataGridViewPermisos.AllowUserToDeleteRows = false;
      this.kryptonDataGridViewPermisos.Dock = DockStyle.Fill;
      this.kryptonDataGridViewPermisos.Location = new Point(0, 31);
      this.kryptonDataGridViewPermisos.Name = "kryptonDataGridViewPermisos";
      this.kryptonDataGridViewPermisos.RowHeadersVisible = false;
      this.kryptonDataGridViewPermisos.ScrollBars = ScrollBars.Vertical;
      this.kryptonDataGridViewPermisos.Size = new Size(634, 231);
      this.kryptonDataGridViewPermisos.TabIndex = 1;
      this.kryptonHeader1.Dock = DockStyle.Top;
      this.kryptonHeader1.Location = new Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.Size = new Size(634, 31);
      this.kryptonHeader1.TabIndex = 0;
      this.kryptonHeader1.Values.Description = "Permisos para el componente";
      this.kryptonHeader1.Values.Heading = "Componentes";
      this.kryptonPanel1.Controls.Add((Control) this.kryptonComboBoxUsers);
      this.kryptonPanel1.Controls.Add((Control) this.kryptonHeader2);
      this.kryptonPanel1.Location = new Point(12, 12);
      this.kryptonPanel1.Name = "kryptonPanel1";
      this.kryptonPanel1.PanelBackStyle = PaletteBackStyle.PanelAlternate;
      this.kryptonPanel1.Size = new Size(635, 96);
      this.kryptonPanel1.TabIndex = 0;
      this.kryptonComboBoxUsers.DropDownStyle = ComboBoxStyle.DropDownList;
      this.kryptonComboBoxUsers.DropDownWidth = 344;
      this.kryptonComboBoxUsers.Location = new Point(18, 47);
      this.kryptonComboBoxUsers.Name = "kryptonComboBoxUsers";
      this.kryptonComboBoxUsers.Size = new Size(344, 21);
      this.kryptonComboBoxUsers.TabIndex = 3;
      this.kryptonComboBoxUsers.SelectedValueChanged += new EventHandler(this.kryptonComboBoxUsers_SelectedValueChanged);
      this.kryptonHeader2.Dock = DockStyle.Top;
      this.kryptonHeader2.Location = new Point(0, 0);
      this.kryptonHeader2.Name = "kryptonHeader2";
      this.kryptonHeader2.Size = new Size(635, 31);
      this.kryptonHeader2.TabIndex = 2;
      this.kryptonHeader2.Values.Description = "Favor elegir usuario";
      this.kryptonHeader2.Values.Heading = "Usuario";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(659, 416);
      this.Controls.Add((Control) this.kryptonPanel);
      this.Name = "FormPermisos";
      this.Text = "FormPermisos";
      this.Load += new EventHandler(this.FormPermisos_Load);
      this.kryptonPanel.EndInit();
      this.kryptonPanel.ResumeLayout(false);
      this.kryptonPanelPermisos.EndInit();
      this.kryptonPanelPermisos.ResumeLayout(false);
      this.kryptonPanelPermisos.PerformLayout();
     // this.kryptonDataGridViewPermisos.EndInit();
      this.kryptonPanel1.EndInit();
      this.kryptonPanel1.ResumeLayout(false);
      this.kryptonPanel1.PerformLayout();
      this.kryptonComboBoxUsers.EndInit();
      this.ResumeLayout(false);
    }
  }
}
