namespace bonita_smile_v1
{
    partial class Actualizar_Nombre_Carpeta
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
            this.btnCancelat = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtAbono = new System.Windows.Forms.TextBox();
            this.lblAbono = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancelat
            // 
            this.btnCancelat.Location = new System.Drawing.Point(430, 277);
            this.btnCancelat.Name = "btnCancelat";
            this.btnCancelat.Size = new System.Drawing.Size(163, 46);
            this.btnCancelat.TabIndex = 8;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = true;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(207, 277);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(160, 46);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtAbono
            // 
            this.txtAbono.Location = new System.Drawing.Point(346, 128);
            this.txtAbono.Name = "txtAbono";
            this.txtAbono.Size = new System.Drawing.Size(276, 22);
            this.txtAbono.TabIndex = 6;
            this.txtAbono.TextChanged += new System.EventHandler(this.txtAbono_TextChanged);
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.Location = new System.Drawing.Point(178, 128);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(152, 17);
            this.lblAbono.TabIndex = 5;
            this.lblAbono.Text = "Nombre de las carpeta";
            // 
            // Actualizar_Nombre_Carpeta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtAbono);
            this.Controls.Add(this.lblAbono);
            this.Name = "Actualizar_Nombre_Carpeta";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelat;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtAbono;
        private System.Windows.Forms.Label lblAbono;
    }
}
