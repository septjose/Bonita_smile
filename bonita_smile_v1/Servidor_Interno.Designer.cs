namespace bonita_smile_v1
{
    partial class Servidor_Interno
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_puerto = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_bd = new System.Windows.Forms.TextBox();
            this.txt_bd_aux = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ip del servidor local :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Puerto del servidor local:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre del usuario local:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(324, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Contraseña del usuario local:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nombre de la Base de datos local:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 29);
            this.label6.TabIndex = 5;
            this.label6.Text = "Base de datos auxiliar local:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(436, 31);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(322, 26);
            this.txt_ip.TabIndex = 6;
            this.txt_ip.TextChanged += new System.EventHandler(this.txt_ip_TextChanged);
            // 
            // txt_puerto
            // 
            this.txt_puerto.Location = new System.Drawing.Point(436, 95);
            this.txt_puerto.Name = "txt_puerto";
            this.txt_puerto.Size = new System.Drawing.Size(322, 26);
            this.txt_puerto.TabIndex = 7;
            this.txt_puerto.TextChanged += new System.EventHandler(this.txt_puerto_TextChanged);
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(436, 157);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(322, 26);
            this.txt_nombre.TabIndex = 8;
            this.txt_nombre.TextChanged += new System.EventHandler(this.txt_nombre_TextChanged);
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(436, 216);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(322, 26);
            this.txt_password.TabIndex = 9;
            this.txt_password.TextChanged += new System.EventHandler(this.txt_password_TextChanged);
            // 
            // txt_bd
            // 
            this.txt_bd.Location = new System.Drawing.Point(436, 280);
            this.txt_bd.Name = "txt_bd";
            this.txt_bd.Size = new System.Drawing.Size(322, 26);
            this.txt_bd.TabIndex = 10;
            this.txt_bd.TextChanged += new System.EventHandler(this.txt_bd_TextChanged);
            // 
            // txt_bd_aux
            // 
            this.txt_bd_aux.Location = new System.Drawing.Point(436, 348);
            this.txt_bd_aux.Name = "txt_bd_aux";
            this.txt_bd_aux.Size = new System.Drawing.Size(322, 26);
            this.txt_bd_aux.TabIndex = 11;
            this.txt_bd_aux.TextChanged += new System.EventHandler(this.txt_bd_aux_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(204, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(387, 70);
            this.button1.TabIndex = 12;
            this.button1.Text = "Actualizar Configuracion";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Servidor_Interno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 582);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_bd_aux);
            this.Controls.Add(this.txt_bd);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.txt_puerto);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Servidor_Interno";
            this.Text = "Servidor_Interno";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.TextBox txt_puerto;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_bd;
        private System.Windows.Forms.TextBox txt_bd_aux;
        private System.Windows.Forms.Button button1;
    }
}