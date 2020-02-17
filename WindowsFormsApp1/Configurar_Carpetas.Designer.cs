namespace WindowsFormsApp1
{
    partial class Configurar_Carpetas
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
            this.btn_subir_servidor = new System.Windows.Forms.Button();
            this.btn_fotografias = new System.Windows.Forms.Button();
            this.btn_temporal = new System.Windows.Forms.Button();
            this.btn_imagenes = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_subir_servidor = new System.Windows.Forms.TextBox();
            this.txt_fotografias = new System.Windows.Forms.TextBox();
            this.txt_temporal = new System.Windows.Forms.TextBox();
            this.txt_imagen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.folder_imagenes = new System.Windows.Forms.FolderBrowserDialog();
            this.folder_temporal = new System.Windows.Forms.FolderBrowserDialog();
            this.folder_fotografias = new System.Windows.Forms.FolderBrowserDialog();
            this.folder_subir_servidor = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_subir_servidor
            // 
            this.btn_subir_servidor.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_subir_servidor.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_subir_servidor.Location = new System.Drawing.Point(783, 248);
            this.btn_subir_servidor.Name = "btn_subir_servidor";
            this.btn_subir_servidor.Size = new System.Drawing.Size(221, 29);
            this.btn_subir_servidor.TabIndex = 25;
            this.btn_subir_servidor.Text = "Elegir carpeta subir servidor";
            this.btn_subir_servidor.UseVisualStyleBackColor = false;
            this.btn_subir_servidor.Click += new System.EventHandler(this.btn_subir_servidor_Click_1);
            // 
            // btn_fotografias
            // 
            this.btn_fotografias.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_fotografias.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_fotografias.Location = new System.Drawing.Point(783, 193);
            this.btn_fotografias.Name = "btn_fotografias";
            this.btn_fotografias.Size = new System.Drawing.Size(208, 26);
            this.btn_fotografias.TabIndex = 24;
            this.btn_fotografias.Text = "Elegir carpeta fotografias";
            this.btn_fotografias.UseVisualStyleBackColor = false;
            this.btn_fotografias.Click += new System.EventHandler(this.btn_fotografias_Click_1);
            // 
            // btn_temporal
            // 
            this.btn_temporal.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_temporal.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_temporal.Location = new System.Drawing.Point(783, 123);
            this.btn_temporal.Name = "btn_temporal";
            this.btn_temporal.Size = new System.Drawing.Size(208, 26);
            this.btn_temporal.TabIndex = 23;
            this.btn_temporal.Text = "Elegir carpeta Temporal";
            this.btn_temporal.UseVisualStyleBackColor = false;
            this.btn_temporal.Click += new System.EventHandler(this.btn_temporal_Click_1);
            // 
            // btn_imagenes
            // 
            this.btn_imagenes.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_imagenes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_imagenes.Location = new System.Drawing.Point(783, 57);
            this.btn_imagenes.Name = "btn_imagenes";
            this.btn_imagenes.Size = new System.Drawing.Size(208, 30);
            this.btn_imagenes.TabIndex = 22;
            this.btn_imagenes.Text = "Elegir Carpeta imagenes";
            this.btn_imagenes.UseVisualStyleBackColor = false;
            this.btn_imagenes.Click += new System.EventHandler(this.btn_imagenes_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(358, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(315, 69);
            this.button1.TabIndex = 21;
            this.button1.Text = "Actualizar ruta de carpetas";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_subir_servidor
            // 
            this.txt_subir_servidor.Location = new System.Drawing.Point(472, 248);
            this.txt_subir_servidor.Name = "txt_subir_servidor";
            this.txt_subir_servidor.Size = new System.Drawing.Size(276, 26);
            this.txt_subir_servidor.TabIndex = 20;
            // 
            // txt_fotografias
            // 
            this.txt_fotografias.Location = new System.Drawing.Point(465, 193);
            this.txt_fotografias.Name = "txt_fotografias";
            this.txt_fotografias.Size = new System.Drawing.Size(283, 26);
            this.txt_fotografias.TabIndex = 19;
            // 
            // txt_temporal
            // 
            this.txt_temporal.Location = new System.Drawing.Point(465, 123);
            this.txt_temporal.Name = "txt_temporal";
            this.txt_temporal.Size = new System.Drawing.Size(283, 26);
            this.txt_temporal.TabIndex = 18;
            // 
            // txt_imagen
            // 
            this.txt_imagen.Location = new System.Drawing.Point(465, 61);
            this.txt_imagen.Name = "txt_imagen";
            this.txt_imagen.Size = new System.Drawing.Size(283, 26);
            this.txt_imagen.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(429, 29);
            this.label4.TabIndex = 16;
            this.label4.Text = "Nombre de la carpeta subir al servidor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(370, 29);
            this.label3.TabIndex = 15;
            this.label3.Text = "Nombre de la carpeta fotografias:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "Nombre de la carpeta Temporal :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nombre de la carpeta Imagenes:";
            // 
            // Configurar_Carpetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 460);
            this.Controls.Add(this.btn_subir_servidor);
            this.Controls.Add(this.btn_fotografias);
            this.Controls.Add(this.btn_temporal);
            this.Controls.Add(this.btn_imagenes);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_subir_servidor);
            this.Controls.Add(this.txt_fotografias);
            this.Controls.Add(this.txt_temporal);
            this.Controls.Add(this.txt_imagen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Configurar_Carpetas";
            this.Text = "Configurar_Carpetas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_subir_servidor;
        private System.Windows.Forms.Button btn_fotografias;
        private System.Windows.Forms.Button btn_temporal;
        private System.Windows.Forms.Button btn_imagenes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_subir_servidor;
        private System.Windows.Forms.TextBox txt_fotografias;
        private System.Windows.Forms.TextBox txt_temporal;
        private System.Windows.Forms.TextBox txt_imagen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folder_imagenes;
        private System.Windows.Forms.FolderBrowserDialog folder_temporal;
        private System.Windows.Forms.FolderBrowserDialog folder_fotografias;
        private System.Windows.Forms.FolderBrowserDialog folder_subir_servidor;
    }
}