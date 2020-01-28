﻿using System;
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
    partial class MessageBoxAbono
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
        bool bandera_online_offline = false;

        public MessageBoxAbono(string id_motivo, string id_paciente, string nombre, string motivo, double restante, double abonado, double total)
        {
            this.id_motivo = id_motivo;
            this.id_paciente = id_paciente;
            this.nombre = nombre;
            this.motivo = motivo;
            this.restante = restante;
            this.abonado = abonado;
            this.total = total;
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
            this.lblAbono.Location = new System.Drawing.Point(69, 57);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(83, 29);
            this.lblAbono.TabIndex = 0;
            this.lblAbono.Text = "Abono";
            // 
            // txtAbono
            // 
            this.txtAbono.Location = new System.Drawing.Point(183, 57);
            this.txtAbono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAbono.Name = "txtAbono";
            this.txtAbono.Size = new System.Drawing.Size(235, 26);
            this.txtAbono.TabIndex = 1;
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(74, 112);
            this.txtComentario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(500, 183);
            this.txtComentario.TabIndex = 2;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAceptar.Location = new System.Drawing.Point(90, 421);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(199, 61);
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
            this.btnCancelat.Size = new System.Drawing.Size(190, 61);
            this.btnCancelat.TabIndex = 4;
            this.btnCancelat.Text = "Cancelar";
            this.btnCancelat.UseVisualStyleBackColor = false;
            this.btnCancelat.Click += new System.EventHandler(this.btnCancelat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Efectivo";
            // 
            // txt_efectivo
            // 
            this.txt_efectivo.Location = new System.Drawing.Point(183, 346);
            this.txt_efectivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_efectivo.Name = "txt_efectivo";
            this.txt_efectivo.Size = new System.Drawing.Size(235, 26);
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
            // MessageBoxAbono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(657, 529);
            this.Controls.Add(this.txt_efectivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelat);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.txtAbono);
            this.Controls.Add(this.lblAbono);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            double efectivo = double.Parse(txt_efectivo.Text);
            //System.Windows.MessageBox.Show("el restante es " + restante);
            //System.Windows.MessageBox.Show("el abono es de " + abono);
            double cambio = efectivo - abono;
            if (abono <= restante || abonado == 0.0)
            {
                Abonos ab = new Abonos(bandera_online_offline);
                bool insertarAbono = ab.insertarAbono(id_paciente, id_motivo, fecha.ToString("yyyy/MM/dd"), abono, comentario);
                if (insertarAbono)
                {
                    ab = new Abonos(!bandera_online_offline);
                    ab.insertarAbono(id_paciente, id_motivo, fecha.ToString("yyyy/MM/dd"), abono, comentario);
                    //System.Windows.Forms.MessageBox.Show("Se registro Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //System.Windows.Forms.MessageBox.Show("El cambio es de " + cambio, "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //System.Windows.Forms.MessageBox.Show("Se esta imprimiendo el recibo", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //imprimir_recibo(fecha.ToString("yyyy/MM/dd"), nombre, abono, motivo, restante, cambio);

                    imprimir_recibo();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo realizar el pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Excedio el restante", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //AGREGAR SI DESEA CONTINUAR
            }
        }

        public void imprimir_recibo()
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
            // }*/


            // doc.Add(new Paragraph("---------------------------------------------------------"));
            // doc.Close();

            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += new PrintPageEventHandler(Imprimir);

            //printDocument1.PrinterSettings.PrinterName = "HPFEF3CF (HP Officejet Pro 6830) (Red)";
            printDocument1.PrinterSettings.PrinterName = "58 Printer";
            //printDocument1.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
            //poner try catch
            if (printDocument1.PrinterSettings.IsValid)
            {
                printDocument1.Print();
            }
            else
            {
                System.Windows.MessageBox.Show("Impresora no válida.");
            }
        }
        float centimetroAPixel(double Centimeter)
        {
            double pixel = -1;
            using (Graphics g = this.CreateGraphics())
            {
                pixel = Centimeter * g.DpiY / 2.54d;
            }
            return (int)pixel;
        }

        /*MÉTODO PARA IMPRIMIR*/

        /*MÉTODO PARA IMPRIMIR*/

        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            System.Drawing.Font font = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font titulo = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font cuerpo = new System.Drawing.Font("Courier New", 10);

            /*MODIFICADORES DE FORMATO DE HOJA*/
            float margen_izquierdo = 0;
            float margen_superior = e.MarginBounds.Top;
            double margen_cuerpo = 0; //DESPUES DEL TITULO
            double tamanio_hoja_horizontal = 5.8;
            /*----------------------------------*/

            double cambio = (Int32.Parse(this.txt_efectivo.Text)) - (Int32.Parse(this.txtAbono.Text));
            //double restante = this.restante - (Int32.Parse(this.txtAbono.Text));

            string fecha = DateTime.Now.ToString("d/M/yyyy");
            string hora = DateTime.Now.ToString("HH:mm:ss") + " hrs";
            Abonos a = new Abonos(bandera_online_offline);
            double restante_pagado = a.Restante(id_motivo);
            double abonado_pagado = a.Abonados(id_motivo);


            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(margen_izquierdo, margen_superior, centimetroAPixel(4), 50);//tamanio_hoja_horizontal en vez de 4


            //e.Graphics.FillRectangle(Brushes.Red, rect); usarlo para cada recyangulo
            e.Graphics.DrawString("BONITA SMILE", titulo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.X = margen_izquierdo + Convert.ToSingle(centimetroAPixel(margen_cuerpo));
            rect.Width = centimetroAPixel(tamanio_hoja_horizontal); //nuevo

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 3) + margen_superior;
            stringFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("FECHA: " + fecha, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 4) + margen_superior;
            e.Graphics.DrawString("HORA: " + hora, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 5) + margen_superior;
            e.Graphics.DrawString("MOTIVO: ", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 6) + margen_superior;
            e.Graphics.DrawString(this.motivo, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 7) + margen_superior;
            e.Graphics.DrawString("PRECIO: $" + this.total, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 8) + margen_superior + 10;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);
            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 8) + margen_superior + 10) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 10) + margen_superior - 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 10) + margen_superior - 5) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 12) + margen_superior);
            e.Graphics.DrawString("TOTAL: $" + this.total, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 13) + margen_superior;
            e.Graphics.DrawString("ABONO: $" + txtAbono.Text, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 14) + margen_superior;
            e.Graphics.DrawString("RECIBIDO: $" + txt_efectivo.Text, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 15) + margen_superior;
            e.Graphics.DrawString("CAMBIO: $" + cambio, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 17) + margen_superior - 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);
            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 17) + margen_superior - 5) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 19) + margen_superior;
            e.Graphics.DrawString("ABONADO: $" + abonado_pagado, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 20) + margen_superior;
            e.Graphics.DrawString("RESTANTE: $" + restante_pagado, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            e.HasMorePages = false;
        }
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