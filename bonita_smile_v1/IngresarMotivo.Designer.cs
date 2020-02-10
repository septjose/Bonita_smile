using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;

using iTextSharp.text.pdf;

using System.IO;
using System.Windows;
using SystemColors = System.Drawing.SystemColors;
using System.Drawing.Printing;
using bonita_smile_v1.Servicios;

namespace bonita_smile_v1
{
    partial class IngresarMotivo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        string id = "";

        public IngresarMotivo(string id_paciente)
        {
            id = id_paciente;
            InitializeComponent();
        }

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
            this.lblAbono = new System.Windows.Forms.Label();
            this.txtAbono = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_efectivo = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.SuspendLayout();
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbono.Location = new System.Drawing.Point(22, 121);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(276, 29);
            this.lblAbono.TabIndex = 0;
            this.lblAbono.Text = "Nombre del Tratamiento";
            // 
            // txtAbono
            // 
            this.txtAbono.Location = new System.Drawing.Point(311, 115);
            this.txtAbono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAbono.Name = "txtAbono";
            this.txtAbono.Size = new System.Drawing.Size(214, 26);
            this.txtAbono.TabIndex = 1;
            this.txtAbono.TextChanged += new System.EventHandler(this.txtAbono_TextChanged);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAceptar.Location = new System.Drawing.Point(70, 401);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(221, 65);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelat
            // 
            this.btnCancelat.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCancelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelat.Location = new System.Drawing.Point(358, 401);
            this.btnCancelat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelat.Name = "btnCancelat";
            this.btnCancelat.Size = new System.Drawing.Size(202, 65);
            this.btnCancelat.TabIndex = 4;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = false;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Costo del Tratamiento";
            // 
            // txt_efectivo
            // 
            this.txt_efectivo.Location = new System.Drawing.Point(322, 227);
            this.txt_efectivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_efectivo.Name = "txt_efectivo";
            this.txt_efectivo.Size = new System.Drawing.Size(214, 26);
            this.txt_efectivo.TabIndex = 6;
            this.txt_efectivo.TextChanged += new System.EventHandler(this.txt_efectivo_TextChanged);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // IngresarMotivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(657, 529);
            this.Controls.Add(this.txt_efectivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtAbono);
            this.Controls.Add(this.lblAbono);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "IngresarMotivo";
            this.Text = "MessageBoxAbono";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void btnCancelat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if(!txtAbono.Text.Equals("")&& !txt_efectivo.Text.Equals(""))
            {
                if(new Seguridad().validar_numero(txt_efectivo.Text))
                {
                    string nombre = txtAbono.Text;
                    double costo = Convert.ToDouble(txt_efectivo.Text);

                    Motivo_cita mc = new Motivo_cita(false);
                    bool inserto = mc.insertarMotivo_cita(nombre, costo, id);
                    if (inserto)
                    {
                        mc = new Motivo_cita(true);
                        mc.insertarMotivo_cita(nombre, costo, id);
                        System.Windows.Forms.MessageBox.Show("Se registro Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No se ingreso ningun motivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Solo ingresar valores numericos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Favor de llenar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        

      
        #endregion

        private System.Windows.Forms.Label lblAbono;
        private System.Windows.Forms.TextBox txtAbono;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelat;
        private Label label1;
        private TextBox txt_efectivo;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintDialog printDialog1;
    }
}