namespace bonita_smile_v1
{
    partial class Configuracion_FTP
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
            this.txt_ruta_ftp = new System.Windows.Forms.TextBox();
            this.txt_passworf_ftp = new System.Windows.Forms.TextBox();
            this.txt_usuario_ftp = new System.Windows.Forms.TextBox();
            this.txt_servidor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbImpresora = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_ruta_ftp
            // 
            this.txt_ruta_ftp.Location = new System.Drawing.Point(306, 174);
            this.txt_ruta_ftp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ruta_ftp.Name = "txt_ruta_ftp";
            this.txt_ruta_ftp.Size = new System.Drawing.Size(287, 22);
            this.txt_ruta_ftp.TabIndex = 45;
            // 
            // txt_passworf_ftp
            // 
            this.txt_passworf_ftp.Location = new System.Drawing.Point(306, 126);
            this.txt_passworf_ftp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_passworf_ftp.Name = "txt_passworf_ftp";
            this.txt_passworf_ftp.PasswordChar = '*';
            this.txt_passworf_ftp.Size = new System.Drawing.Size(287, 22);
            this.txt_passworf_ftp.TabIndex = 44;
            // 
            // txt_usuario_ftp
            // 
            this.txt_usuario_ftp.Location = new System.Drawing.Point(306, 77);
            this.txt_usuario_ftp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_usuario_ftp.Name = "txt_usuario_ftp";
            this.txt_usuario_ftp.Size = new System.Drawing.Size(287, 22);
            this.txt_usuario_ftp.TabIndex = 43;
            // 
            // txt_servidor
            // 
            this.txt_servidor.Location = new System.Drawing.Point(306, 26);
            this.txt_servidor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_servidor.Name = "txt_servidor";
            this.txt_servidor.Size = new System.Drawing.Size(287, 22);
            this.txt_servidor.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 25);
            this.label5.TabIndex = 41;
            this.label5.Text = "Nombre de la impresora:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 40;
            this.label4.Text = "Ruta FTP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 25);
            this.label3.TabIndex = 39;
            this.label3.Text = "Contraseña FTP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 25);
            this.label2.TabIndex = 38;
            this.label2.Text = "Nombre de usuario FTP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 25);
            this.label1.TabIndex = 37;
            this.label1.Text = "Servidor FTP:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(219, 290);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(274, 48);
            this.button1.TabIndex = 36;
            this.button1.Text = "Actualizar Configuracion";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbImpresora
            // 
            this.cmbImpresora.FormattingEnabled = true;
            this.cmbImpresora.Location = new System.Drawing.Point(306, 229);
            this.cmbImpresora.Name = "cmbImpresora";
            this.cmbImpresora.Size = new System.Drawing.Size(287, 24);
            this.cmbImpresora.TabIndex = 47;
            this.cmbImpresora.SelectedIndexChanged += new System.EventHandler(this.cmbImpresora_SelectedIndexChanged);
            // 
            // Configuracion_FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 360);
            this.Controls.Add(this.cmbImpresora);
            this.Controls.Add(this.txt_ruta_ftp);
            this.Controls.Add(this.txt_passworf_ftp);
            this.Controls.Add(this.txt_usuario_ftp);
            this.Controls.Add(this.txt_servidor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Configuracion Servidor FTP";
            this.Text = "Configuracion Servidor FTP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_ruta_ftp;
        private System.Windows.Forms.TextBox txt_passworf_ftp;
        private System.Windows.Forms.TextBox txt_usuario_ftp;
        private System.Windows.Forms.TextBox txt_servidor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbImpresora;
    }
}