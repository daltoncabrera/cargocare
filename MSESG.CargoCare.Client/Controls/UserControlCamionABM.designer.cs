using ComponentFactory.Krypton.Toolkit;
using DCM.Core;
using DCM.Core.Data;
using DCM.Core.UI;
using Marhex.CargoCare.DAC;
using Marhex.CargoCare.DAC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MSESG.CargoCare.Client
{
    public partial class UserControlCamionABM
    {
        private IContainer components;
        private KryptonHeader kryptonHeader1;
        private KryptonButton kryptonButtonCancel;
        private KryptonButton kryptonButtonAccept;
        private KryptonPanel kryptonPanelInner;
        private KryptonLabel Direccion;
        private KryptonLabel kryptonLabel6;
        private KryptonLabel kryptonLabel5;
        private KryptonLabel kryptonLabel4;
        private KryptonTextBox kryptonTextBoxFicha;
        private KryptonLabel kryptonLabel3;
        private KryptonTextBox kryptonTextBoxMarca;
        private KryptonLabel kryptonLabel2;
        private KryptonTextBox kryptonTextBoxModelo;
        private KryptonLabel kryptonLabel1;
        private KryptonTextBox kryptonTextBoxPlaca;
        private KryptonLabel kryptonLabel7;
        private KryptonTextBox kryptonTextBoxComentario;
        private KryptonNumericUpDown kryptonNumericUpDownSalidas;
        private KryptonNumericUpDown kryptonNumericUpDownEntradas;
        private KryptonNumericUpDown kryptonNumericUpDownCompartimentos;
        private KryptonLabel kryptonLabel8;
        private KryptonNumericUpDown kryptonNumericUpDownCapacidad;
        private KryptonLabel kryptonLabel9;
        private UserControlSingleSearch userControlSingleSearchCliente;


        private void InitializeComponent()
        {
            this.kryptonHeader1 = new KryptonHeader();
            this.kryptonButtonAccept = new KryptonButton();
            this.kryptonButtonCancel = new KryptonButton();
            this.kryptonPanelInner = new KryptonPanel();
            this.kryptonNumericUpDownCapacidad = new KryptonNumericUpDown();
            this.kryptonTextBoxComentario = new KryptonTextBox();
            this.kryptonNumericUpDownSalidas = new KryptonNumericUpDown();
            this.kryptonNumericUpDownEntradas = new KryptonNumericUpDown();
            this.kryptonNumericUpDownCompartimentos = new KryptonNumericUpDown();
            this.kryptonLabel8 = new KryptonLabel();
            this.kryptonLabel7 = new KryptonLabel();
            this.Direccion = new KryptonLabel();
            this.kryptonLabel6 = new KryptonLabel();
            this.kryptonLabel5 = new KryptonLabel();
            this.kryptonLabel4 = new KryptonLabel();
            this.kryptonTextBoxFicha = new KryptonTextBox();
            this.kryptonLabel3 = new KryptonLabel();
            this.kryptonTextBoxMarca = new KryptonTextBox();
            this.kryptonLabel2 = new KryptonLabel();
            this.kryptonTextBoxModelo = new KryptonTextBox();
            this.kryptonLabel1 = new KryptonLabel();
            this.kryptonTextBoxPlaca = new KryptonTextBox();
            this.kryptonLabel9 = new KryptonLabel();
            this.userControlSingleSearchCliente = new UserControlSingleSearch();
            this.kryptonPanel1.BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.kryptonPanelInner.BeginInit();
            this.kryptonPanelInner.SuspendLayout();
            this.SuspendLayout();
            this.kryptonPanel1.Controls.Add((Control)this.kryptonLabel9);
            this.kryptonPanel1.Controls.Add((Control)this.userControlSingleSearchCliente);
            this.kryptonPanel1.Controls.Add((Control)this.kryptonPanelInner);
            this.kryptonPanel1.Controls.Add((Control)this.kryptonButtonCancel);
            this.kryptonPanel1.Controls.Add((Control)this.kryptonButtonAccept);
            this.kryptonPanel1.Controls.Add((Control)this.kryptonHeader1);
            this.kryptonPanel1.Size = new Size(423, 454);
            this.kryptonHeader1.Dock = DockStyle.Top;
            this.kryptonHeader1.Location = new Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new Size(423, 4);
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "";
            this.kryptonHeader1.Values.Image = (Image)null;
            this.kryptonButtonAccept.Location = new Point(210, 391);
            this.kryptonButtonAccept.Name = "kryptonButtonAccept";
            this.kryptonButtonAccept.Size = new Size(90, 25);
            this.kryptonButtonAccept.TabIndex = 8;
            this.kryptonButtonAccept.Values.Text = "Aceptar";
            this.kryptonButtonAccept.Click += new EventHandler(this.kryptonButtonAccept_Click);
            this.kryptonButtonCancel.Location = new Point(319, 391);
            this.kryptonButtonCancel.Name = "kryptonButtonCancel";
            this.kryptonButtonCancel.Size = new Size(90, 25);
            this.kryptonButtonCancel.TabIndex = 9;
            this.kryptonButtonCancel.Values.Text = "Cancelar";
            this.kryptonButtonCancel.Click += new EventHandler(this.kryptonButtonCancel_Click);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonNumericUpDownCapacidad);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonTextBoxComentario);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonNumericUpDownSalidas);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonNumericUpDownEntradas);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonNumericUpDownCompartimentos);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel8);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel7);
            this.kryptonPanelInner.Controls.Add((Control)this.Direccion);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel6);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel5);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel4);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonTextBoxFicha);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel3);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonTextBoxMarca);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel2);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonTextBoxModelo);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonLabel1);
            this.kryptonPanelInner.Controls.Add((Control)this.kryptonTextBoxPlaca);
            this.kryptonPanelInner.Enabled = false;
            this.kryptonPanelInner.Location = new Point(13, 77);
            this.kryptonPanelInner.Name = "kryptonPanelInner";
            this.kryptonPanelInner.PanelBackStyle = PaletteBackStyle.PanelAlternate;
            this.kryptonPanelInner.Size = new Size(396, 308);
            this.kryptonPanelInner.TabIndex = 10;
            this.kryptonNumericUpDownCapacidad.Location = new Point(122, 150);
            this.kryptonNumericUpDownCapacidad.Maximum = new Decimal(new int[4]
            {
        1000000000,
        0,
        0,
        0
            });
            this.kryptonNumericUpDownCapacidad.Name = "kryptonNumericUpDownCapacidad";
            this.kryptonNumericUpDownCapacidad.Size = new Size(120, 22);
            this.kryptonNumericUpDownCapacidad.TabIndex = 22;
            this.kryptonTextBoxComentario.Location = new Point(122, 230);
            this.kryptonTextBoxComentario.Multiline = true;
            this.kryptonTextBoxComentario.Name = "kryptonTextBoxComentario";
            this.kryptonTextBoxComentario.Size = new Size(234, 57);
            this.kryptonTextBoxComentario.TabIndex = 21;
            this.kryptonNumericUpDownSalidas.Location = new Point(122, 203);
            this.kryptonNumericUpDownSalidas.Maximum = new Decimal(new int[4]
            {
        1000000000,
        0,
        0,
        0
            });
            this.kryptonNumericUpDownSalidas.Name = "kryptonNumericUpDownSalidas";
            this.kryptonNumericUpDownSalidas.Size = new Size(120, 22);
            this.kryptonNumericUpDownSalidas.TabIndex = 20;
            this.kryptonNumericUpDownEntradas.Location = new Point(122, 176);
            this.kryptonNumericUpDownEntradas.Maximum = new Decimal(new int[4]
            {
        1000000000,
        0,
        0,
        0
            });
            this.kryptonNumericUpDownEntradas.Name = "kryptonNumericUpDownEntradas";
            this.kryptonNumericUpDownEntradas.Size = new Size(120, 22);
            this.kryptonNumericUpDownEntradas.TabIndex = 19;
            this.kryptonNumericUpDownCompartimentos.Location = new Point(122, 122);
            this.kryptonNumericUpDownCompartimentos.Maximum = new Decimal(new int[4]
            {
        1000000000,
        0,
        0,
        0
            });
            this.kryptonNumericUpDownCompartimentos.Name = "kryptonNumericUpDownCompartimentos";
            this.kryptonNumericUpDownCompartimentos.Size = new Size(120, 22);
            this.kryptonNumericUpDownCompartimentos.TabIndex = 17;
            this.kryptonLabel8.Location = new Point(43, 230);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new Size(75, 20);
            this.kryptonLabel8.TabIndex = 16;
            this.kryptonLabel8.Values.Text = "Comentario";
            this.kryptonLabel7.Location = new Point(70, 204);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new Size(48, 20);
            this.kryptonLabel7.TabIndex = 15;
            this.kryptonLabel7.Values.Text = "Salidas";
            this.Direccion.Location = new Point(61, 178);
            this.Direccion.Name = "Direccion";
            this.Direccion.Size = new Size(57, 20);
            this.Direccion.TabIndex = 14;
            this.Direccion.Values.Text = "Entradas";
            this.kryptonLabel6.Location = new Point(50, 152);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new Size(68, 20);
            this.kryptonLabel6.TabIndex = 12;
            this.kryptonLabel6.Values.Text = "Capacidad";
            this.kryptonLabel5.Location = new Point(16, 126);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new Size(105, 20);
            this.kryptonLabel5.TabIndex = 10;
            this.kryptonLabel5.Values.Text = "Compartimientos";
            this.kryptonLabel4.Location = new Point(79, 100);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new Size(39, 20);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Ficha";
            this.kryptonTextBoxFicha.Location = new Point(122, 97);
            this.kryptonTextBoxFicha.Name = "kryptonTextBoxFicha";
            this.kryptonTextBoxFicha.Size = new Size(234, 20);
            this.kryptonTextBoxFicha.TabIndex = 7;
            this.kryptonLabel3.Location = new Point(74, 22);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new Size(44, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Marca";
            this.kryptonTextBoxMarca.Location = new Point(122, 22);
            this.kryptonTextBoxMarca.Name = "kryptonTextBoxMarca";
            this.kryptonTextBoxMarca.Size = new Size(234, 20);
            this.kryptonTextBoxMarca.TabIndex = 1;
            this.kryptonLabel2.Location = new Point(79, 74);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new Size(39, 20);
            this.kryptonLabel2.TabIndex = 5;
            this.kryptonLabel2.Values.Text = "Placa";
            this.kryptonTextBoxModelo.Location = new Point(122, 47);
            this.kryptonTextBoxModelo.Name = "kryptonTextBoxModelo";
            this.kryptonTextBoxModelo.Size = new Size(234, 20);
            this.kryptonTextBoxModelo.TabIndex = 2;
            this.kryptonLabel1.Location = new Point(65, 48);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new Size(53, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Modelo";
            this.kryptonTextBoxPlaca.Location = new Point(122, 72);
            this.kryptonTextBoxPlaca.Name = "kryptonTextBoxPlaca";
            this.kryptonTextBoxPlaca.Size = new Size(234, 20);
            this.kryptonTextBoxPlaca.TabIndex = 3;
            this.kryptonLabel9.Location = new Point(13, 10);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new Size(48, 20);
            this.kryptonLabel9.TabIndex = 20;
            this.kryptonLabel9.Values.Text = "Cliente";
            this.userControlSingleSearchCliente.BorderStyle = BorderStyle.FixedSingle;
            this.userControlSingleSearchCliente.Location = new Point(15, 33);
            this.userControlSingleSearchCliente.Name = "userControlSingleSearchCliente";
            this.userControlSingleSearchCliente.NameEntity = "";
            this.userControlSingleSearchCliente.Size = new Size(394, 30);
            this.userControlSingleSearchCliente.TabIndex = 19;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "UserControlCamionABM";
            this.Size = new Size(423, 454);
            this.Load += new EventHandler(this.UserControlCamionABM_Load);
            this.kryptonPanel1.EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonPanelInner.EndInit();
            this.kryptonPanelInner.ResumeLayout(false);
            this.kryptonPanelInner.PerformLayout();
            this.ResumeLayout(false);
        }

    }
}
