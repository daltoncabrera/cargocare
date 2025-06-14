using ComponentFactory.Krypton.Toolkit;
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
    public partial class UserControlSystemVarsABM :  UserControlCareCargoBase
    {

        private IContainer components;
        private KryptonHeader kryptonHeader1;
        private KryptonButton kryptonButtonCancel;
        private KryptonButton kryptonButtonAccept;
        private KryptonPanel kryptonPanel2;
        private KryptonLabel kryptonLabel3;
        private KryptonTextBox kryptonTextBoxName;
        private KryptonTextBox kryptonTextBoxDesc;
        private KryptonLabel kryptonLabel1;
        private KryptonTextBox kryptonTextBoxValue;
        private KryptonLabel kryptonLabel2;

        private KryptonLabel kryptonLabel5;


        private void InitializeComponent()
        {
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonTextBoxName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBoxDesc = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonTextBoxValue = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButtonAccept = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonButtonCancel);
            this.kryptonPanel1.Controls.Add(this.kryptonButtonAccept);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel1.Controls.Add(this.kryptonHeader1);
            this.kryptonPanel1.Size = new System.Drawing.Size(450, 259);
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(450, 20);
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "";
            // 
            // kryptonTextBoxName
            // 
            this.kryptonTextBoxName.Location = new System.Drawing.Point(132, 24);
            this.kryptonTextBoxName.Name = "kryptonTextBoxName";
            this.kryptonTextBoxName.Size = new System.Drawing.Size(234, 23);
            this.kryptonTextBoxName.TabIndex = 1;
            // 
            // kryptonTextBoxDesc
            // 
            this.kryptonTextBoxDesc.Location = new System.Drawing.Point(132, 50);
            this.kryptonTextBoxDesc.Name = "kryptonTextBoxDesc";
            this.kryptonTextBoxDesc.Size = new System.Drawing.Size(234, 23);
            this.kryptonTextBoxDesc.TabIndex = 2;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(52, 53);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(74, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Descripcion";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(70, 24);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Nombre";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonTextBoxValue);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel2.Controls.Add(this.kryptonTextBoxName);
            this.kryptonPanel2.Controls.Add(this.kryptonTextBoxDesc);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Location = new System.Drawing.Point(19, 65);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel2.Size = new System.Drawing.Size(411, 128);
            this.kryptonPanel2.TabIndex = 7;
            // 
            // kryptonTextBoxValue
            // 
            this.kryptonTextBoxValue.Location = new System.Drawing.Point(132, 77);
            this.kryptonTextBoxValue.Name = "kryptonTextBoxValue";
            this.kryptonTextBoxValue.Size = new System.Drawing.Size(234, 23);
            this.kryptonTextBoxValue.TabIndex = 7;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(85, 80);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(41, 20);
            this.kryptonLabel2.TabIndex = 8;
            this.kryptonLabel2.Values.Text = "Value";
            // 
            // kryptonButtonAccept
            // 
            this.kryptonButtonAccept.Location = new System.Drawing.Point(231, 204);
            this.kryptonButtonAccept.Name = "kryptonButtonAccept";
            this.kryptonButtonAccept.Size = new System.Drawing.Size(90, 25);
            this.kryptonButtonAccept.TabIndex = 8;
            this.kryptonButtonAccept.Values.Text = "Aceptar";
            this.kryptonButtonAccept.Click += new System.EventHandler(this.kryptonButtonAccept_Click);
            // 
            // kryptonButtonCancel
            // 
            this.kryptonButtonCancel.Location = new System.Drawing.Point(340, 204);
            this.kryptonButtonCancel.Name = "kryptonButtonCancel";
            this.kryptonButtonCancel.Size = new System.Drawing.Size(90, 25);
            this.kryptonButtonCancel.TabIndex = 9;
            this.kryptonButtonCancel.Values.Text = "Cancelar";
            this.kryptonButtonCancel.Click += new System.EventHandler(this.kryptonButtonCancel_Click);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(19, 41);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(71, 20);
            this.kryptonLabel5.TabIndex = 22;
            this.kryptonLabel5.Values.Text = "System Var";
            // 
            // UserControlSystemVarsABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Name = "UserControlSystemVarsABM";
            this.Size = new System.Drawing.Size(450, 259);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

        }




        private cargocareEntities dbEnt;
        private systemvar curSystemVar;
        private SystemVarsWraper systemVarWraper;

        public UserControlSystemVarsABM(List<SearchResult> objSearch = null)
        {
            this.InitializeComponent();
            this.ObjSearch = objSearch;
            prepareControls();
        }

        private void kryptonButtonCancel_Click(object sender, EventArgs e)
        {
        }

        private void kryptonButtonAccept_Click(object sender, EventArgs e)
        {
            this.save();
            GUtils.ShowMessage("Registro guardado");
            this.ParentForm.Close();
        }

        private void save()
        {
            this.curSystemVar = this.systemVarWraper.Save(this.curSystemVar != null ? new int?(this.curSystemVar.IdSystemVar) : new int?(), this.kryptonTextBoxName.Text, this.kryptonTextBoxValue.Text, this.kryptonTextBoxDesc.Text);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void prepareControls()
        {
            dbEnt = new cargocareEntities();
            this.systemVarWraper = new SystemVarsWraper();
            this.curSystemVar = this.systemVarWraper.GetSystemVar(this.ObjSearch);
               
            this.fillForm();
        }

        private void userControlSingleSearchVars_OnValueChanged(List<SearchResult> resultList, string displayValue)
        {
           // this.kryptonPanelInner.Enabled = resultList.Count > 0;
            this.curSystemVar = this.systemVarWraper.GetSystemVar(resultList);
        }
        private void fillForm()
        {
            if (this.curSystemVar == null)
                return;
            this.kryptonTextBoxName.Text = this.curSystemVar.VarName;
            this.kryptonTextBoxDesc.Text = this.curSystemVar.Description;
            this.kryptonTextBoxValue.Text = this.curSystemVar.VarValue;
        }

    }
}
