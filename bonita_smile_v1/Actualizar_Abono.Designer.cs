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
using bonita_smile_v1.Modelos;
namespace bonita_smile_v1
{
    partial class Actualizar_Abono
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        string id_motivo = "";
        string id_paciente = "";
        string nombre = "";
        string motivo = "";
        double restante = 0.0;
        double abonado = 0.0;
        double total = 0.0;
        AbonosModel abono;

        public Actualizar_Abono(string id_motivo, string id_paciente, string nombre, string motivo, double restante, double abonado, double total,AbonosModel abono)
        {
            this.id_motivo = id_motivo;
            this.id_paciente = id_paciente;
            this.nombre = nombre;
            this.motivo = motivo;
            this.restante = restante;
            this.abonado = abonado;
            this.abono = abono;
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
            this.lblAbono.Location = new System.Drawing.Point(72, 74);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(83, 29);
            this.lblAbono.TabIndex = 0;
            this.lblAbono.Text = "Abono";
            this.lblAbono.Click += new System.EventHandler(this.lblAbono_Click);
            // 
            // txtAbono
            // 
            this.txtAbono.Location = new System.Drawing.Point(213, 78);
            this.txtAbono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAbono.Name = "txtAbono";
            this.txtAbono.Size = new System.Drawing.Size(254, 26);
            this.txtAbono.TabIndex = 1;
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(77, 140);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(500, 172);
            this.txtComentario.TabIndex = 2;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAceptar.Location = new System.Drawing.Point(67, 433);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(134, 51);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelat
            // 
            this.btnCancelat.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCancelat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelat.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnCancelat.Location = new System.Drawing.Point(391, 433);
            this.btnCancelat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancelat.Name = "btnCancelat";
            this.btnCancelat.Size = new System.Drawing.Size(141, 51);
            this.btnCancelat.TabIndex = 4;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = false;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Efectivo";
            // 
            // txt_efectivo
            // 
            this.txt_efectivo.Location = new System.Drawing.Point(222, 352);
            this.txt_efectivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_efectivo.Name = "txt_efectivo";
            this.txt_efectivo.Size = new System.Drawing.Size(245, 26);
            this.txt_efectivo.TabIndex = 6;
            this.txt_efectivo.TextChanged += new System.EventHandler(this.txt_efectivo_TextChanged);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Actualizar_Abono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(625, 529);
            this.Controls.Add(this.txt_efectivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.txtAbono);
            this.Controls.Add(this.lblAbono);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Actualizar_Abono";
            this.Text = "Actualizar Pago";
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
            double efectivo = double.Parse(txt_efectivo.Text);
            //System.Windows.MessageBox.Show("el restante es " + restante);
            //System.Windows.MessageBox.Show("el abono es de " + abono);
            double cambio = efectivo - abono;
            if (abono <= restante || abonado == 0.0)
            {
                bool insertarAbono = new Servicios.Abonos(false).actualizarAbono(this.abono.id_abono,this.abono.id_paciente, this.abono.id_motivo, fecha.ToString("yyyy/MM/dd"), abono, comentario);
                if (insertarAbono)
                {
                    new Servicios.Abonos(true).actualizarAbono(this.abono.id_abono, this.abono.id_paciente, this.abono.id_motivo, fecha.ToString("yyyy/MM/dd"), abono, comentario);
                    System.Windows.Forms.MessageBox.Show("Se registro Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Windows.Forms.MessageBox.Show("El cambio es de " + cambio, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Windows.Forms.MessageBox.Show("Se esta imprimiendo el recibo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //imprimir_recibo(fecha.ToString("yyyy/MM/dd"), nombre, abono, motivo, restante, cambio);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se ingreso ningun motivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Excedio el restante", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //imprimir_recibo();


        }

        /*public void imprimir_recibo()
        {
            //double rest = restante - abono;

            // Document doc = new Document();
            // PdfWriter.GetInstance(doc, new FileStream(@"C:\bs\prueba.pdf", FileMode.Create));
            // doc.Open();

            // Paragraph title = new Paragraph();
            // title.Font = FontFactory.GetFont(FontFactory.TIMES, 18f, BaseColor.BLUE);
            // title.Add("Recibo de Pago de Bonita Smile");
            // doc.Add(title);


            // doc.Add(new Paragraph("Bonita Smile "));
            // doc.Add(new Paragraph("Se realiza un pago el dia-------------  " +fecha));
            // doc.Add(new Paragraph("Nombre del Paciente -------------------- " +nombre));
            // doc.Add(new Paragraph("El pago es de -------------------------- " +motivo));
            // //doc.Add(new Paragraph("El restante es de ---------------------- $ "+restante));
            // doc.Add(new Paragraph("El abono a pagar es de ----------------- $ "+abono));
            // doc.Add(new Paragraph("Su cambio es de ---------------------$" + camb));
            ///* if(rest==0.0)
            // {
            //     doc.Add(new Paragraph("Su Cuenta ya fue liquidada $ " + rest));
            // }
            // else
            // {
            //     doc.Add(new Paragraph("Lo restante que le falta de pagar es de ---- $ " + rest));
            // }


            // doc.Add(new Paragraph("---------------------------------------------------------"));
            // doc.Close();

            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += new PrintPageEventHandler(Imprimir);
            printDocument1.PrintPage += new PrintPageEventHandler(Imprimir);


            printDocument1.PrinterSettings.PrinterName = "HPFEF3CF (HP Officejet Pro 6830) (Red)";
            //printDocument1.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";

            if (printDocument1.PrinterSettings.IsValid)
            {
                printDocument1.Print();
            }
            else
            {
                System.Windows.MessageBox.Show("Printer is invalid.");
            }
        }*/

       /* private void Imprimir(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Font font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Regular, GraphicsUnit.Point);
            int width = 200;
            int y = 20;
            e.Graphics.DrawString("Un Ticket Feliz", font, Brushes.Black, new RectangleF(20, 20, width, 25));
            e.Graphics.DrawString("Un Ticket Feliz 2", font, Brushes.Black, new RectangleF(20, 50, width, 45));

        }*/
        #endregion

        private System.Windows.Forms.Label lblAbono;
        private System.Windows.Forms.TextBox txtAbono;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelat;
        private Label label1;
        private TextBox txt_efectivo;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintDialog printDialog1;
    }
}