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
using bonita_smile_v1.Servicios;
using bonita_smile_v1.Modelos;
namespace bonita_smile_v1
{
    partial class Actualizar_Nota_Evolucion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        Nota_de_digi_evolucionModel nota;
        bool bandera_online_offline = false;
        string alias;
        string nombre_doctor;
        public Actualizar_Nota_Evolucion(Nota_de_digi_evolucionModel nota,string nombre_doctor,string alias)
        {
           
            this.nota = nota;
            System.Windows.MessageBox.Show(nota.fecha);
            this.alias = alias;
            this.nombre_doctor = nota.nombre_doctor;
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
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbono.Location = new System.Drawing.Point(210, 54);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(207, 29);
            this.lblAbono.TabIndex = 0;
            this.lblAbono.Text = "Nota de evolucion";
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(74, 134);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(498, 255);
            this.txtComentario.TabIndex = 2;
            this.txtComentario.Text = this.nota.descripcion;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAceptar.Location = new System.Drawing.Point(96, 421);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(174, 60);
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
            this.btnCancelat.Location = new System.Drawing.Point(360, 421);
            this.btnCancelat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelat.Name = "btnCancelat";
            this.btnCancelat.Size = new System.Drawing.Size(182, 60);
            this.btnCancelat.TabIndex = 4;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = false;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // Actualizar_Nota_Evolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(657, 529);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.lblAbono);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Actualizar Nota Evolucion";
            this.Text = "Actualizar Nota Evolucion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void btnCancelat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!txtComentario.Text.Equals(""))
            {
                string comentario = txtComentario.Text;

                //DateTime fecha = DateTime.Now;
                // DateTime parsedDate = DateTime.Parse(nota.fecha);
                DateTime parsedDate = DateTime.Parse(nota.fecha);
                //System.Windows.MessageBox.Show(" imprimo conversion  " + parsedDate.ToString("yyyy/MM/dd"));
                string fecha_actual = parsedDate.ToString("yyyy/MM/dd");
                Nota_de_digi_evolucion ne = new Nota_de_digi_evolucion(bandera_online_offline);
                bool insertarAbono = ne.actualizarNota_de_digi_evolucion(nota.id_nota, nota.id_paciente, nota.id_motivo, comentario, fecha_actual,nombre_doctor , alias);
                if (insertarAbono)
                {
                  //  System.Windows.Forms.MessageBox.Show("Se Actualizo Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ne = new Nota_de_digi_evolucion(!bandera_online_offline);
                    //ne.actualizarNota_de_digi_evolucion(nota.id_nota, nota.id_paciente, nota.id_motivo, comentario, fecha_actual);
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("No se Actualizo ningun motivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Favor de llenar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        #endregion

        private System.Windows.Forms.Label lblAbono;

        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelat;
    }
}