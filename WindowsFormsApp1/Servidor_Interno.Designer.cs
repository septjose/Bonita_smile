namespace WindowsFormsApp1
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
            this.button1 = new System.Windows.Forms.Button();
            this.txt_bd_aux = new System.Windows.Forms.TextBox();
            this.txt_bd = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_puerto = new System.Windows.Forms.TextBox();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(230, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(387, 70);
            this.button1.TabIndex = 25;
            this.button1.Text = "Actualizar Configuracion";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_bd_aux
            // 
            this.txt_bd_aux.Location = new System.Drawing.Point(462, 364);
            this.txt_bd_aux.Name = "txt_bd_aux";
            this.txt_bd_aux.Size = new System.Drawing.Size(322, 26);
            this.txt_bd_aux.TabIndex = 24;
            // 
            // txt_bd
            // 
            this.txt_bd.Location = new System.Drawing.Point(462, 296);
            this.txt_bd.Name = "txt_bd";
            this.txt_bd.Size = new System.Drawing.Size(322, 26);
            this.txt_bd.TabIndex = 23;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(462, 232);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(322, 26);
            this.txt_password.TabIndex = 22;
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(462, 173);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(322, 26);
            this.txt_nombre.TabIndex = 21;
            // 
            // txt_puerto
            // 
            this.txt_puerto.Location = new System.Drawing.Point(462, 111);
            this.txt_puerto.Name = "txt_puerto";
            this.txt_puerto.Size = new System.Drawing.Size(322, 26);
            this.txt_puerto.TabIndex = 20;
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(462, 47);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(322, 26);
            this.txt_ip.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(52, 361);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 29);
            this.label6.TabIndex = 18;
            this.label6.Text = "Base de datos auxiliar local:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(52, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 29);
            this.label5.TabIndex = 17;
            this.label5.Text = "Nombre de la Base de datos local:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(324, 29);
            this.label4.TabIndex = 16;
            this.label4.Text = "Contraseña del usuario local:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 29);
            this.label3.TabIndex = 15;
            this.label3.Text = "Nombre del usuario local:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "Puerto del servidor local:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ip del servidor local :";
            // 
            // Servidor_Interno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 579);
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

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_bd_aux;
        private System.Windows.Forms.TextBox txt_bd;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_puerto;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}