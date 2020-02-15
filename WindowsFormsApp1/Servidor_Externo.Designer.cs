namespace WindowsFormsApp1
{
    partial class Servidor_Externo
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
            this.txt_bd = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_puerto = new System.Windows.Forms.TextBox();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_bd
            // 
            this.txt_bd.Location = new System.Drawing.Point(444, 281);
            this.txt_bd.Name = "txt_bd";
            this.txt_bd.Size = new System.Drawing.Size(322, 26);
            this.txt_bd.TabIndex = 46;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(444, 217);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(322, 26);
            this.txt_password.TabIndex = 45;
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(444, 158);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(322, 26);
            this.txt_nombre.TabIndex = 44;
            // 
            // txt_puerto
            // 
            this.txt_puerto.Location = new System.Drawing.Point(444, 96);
            this.txt_puerto.Name = "txt_puerto";
            this.txt_puerto.Size = new System.Drawing.Size(322, 26);
            this.txt_puerto.TabIndex = 43;
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(444, 32);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(322, 26);
            this.txt_ip.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(412, 29);
            this.label5.TabIndex = 41;
            this.label5.Text = "Nombre de la Base de datos externo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 29);
            this.label4.TabIndex = 40;
            this.label4.Text = "Contraseña del usuario externo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(318, 29);
            this.label3.TabIndex = 39;
            this.label3.Text = "Nombre del usuario externo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 29);
            this.label2.TabIndex = 38;
            this.label2.Text = "Puerto del servidor externo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 29);
            this.label1.TabIndex = 37;
            this.label1.Text = "Ip del servidor externo :";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(243, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(308, 60);
            this.button1.TabIndex = 36;
            this.button1.Text = "Actualizar Configuracion";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Servidor_Externo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_bd);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.txt_puerto);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Servidor_Externo";
            this.Text = "Servidor_Externo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_bd;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_puerto;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}