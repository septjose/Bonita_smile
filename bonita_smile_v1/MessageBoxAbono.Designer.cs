using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using SystemColors = System.Drawing.SystemColors;
namespace bonita_smile_v1
{
    partial class MessageBoxAbono
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        int id_motivo = 0;
        int id_paciente = 0;
        public MessageBoxAbono(int id_motivo,int id_paciente)
        {
            this.id_motivo = id_motivo;
            this.id_paciente = id_paciente;
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
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.Location = new System.Drawing.Point(208, 45);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(49, 17);
            this.lblAbono.TabIndex = 0;
            this.lblAbono.Text = "Abono";
            //this.lblAbono.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtAbono
            // 
            this.txtAbono.Location = new System.Drawing.Point(272, 45);
            this.txtAbono.Name = "txtAbono";
            this.txtAbono.Size = new System.Drawing.Size(100, 22);
            this.txtAbono.TabIndex = 1;
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(66, 107);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(443, 205);
            this.txtComentario.TabIndex = 2;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(182, 337);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelat
            // 
            this.btnCancelat.Location = new System.Drawing.Point(320, 337);
            this.btnCancelat.Name = "btnCancelat";
            this.btnCancelat.Size = new System.Drawing.Size(75, 23);
            this.btnCancelat.TabIndex = 4;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = true;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // MessageBoxAbono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(584, 423);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.txtAbono);
            this.Controls.Add(this.lblAbono);
            this.Name = "MessageBoxAbono";
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
            
            string comentario = txtComentario.Text;
           
            DateTime fecha = DateTime.Now;
            double abono = double.Parse(txtAbono.Text);
            bool insertarAbono = new Servicios.Abonos().insertarAbono(id_paciente, id_motivo, fecha.ToString("yyyy/MM/dd"), abono,comentario);
            if (insertarAbono)
            {
                System.Windows.MessageBox.Show("Exito");
            }
            else
            {
                System.Windows.MessageBox.Show("No se inserto");
            }

            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private System.Windows.Forms.Label lblAbono;
        private System.Windows.Forms.TextBox txtAbono;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelat;
    }
}