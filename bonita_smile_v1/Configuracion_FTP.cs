using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bonita_smile_v1
{
    public partial class Configuracion_FTP : Form
    {
        Configuracion_Model configuracion;
        string ruta;
        public Configuracion_FTP(Configuracion_Model configuracion, string ruta)
        {
            InitializeComponent();
            cmbImpresorasInstaladas();
            //txt_impresora.Text = configuracion.ftp.nombre_impresora;
            txt_passworf_ftp.Text = configuracion.ftp.ftp_password;
            txt_ruta_ftp.Text = configuracion.ftp.ftp_path;
            txt_servidor.Text = configuracion.ftp.ftp_server;
            txt_usuario_ftp.Text = configuracion.ftp.ftp_user;
            cmbImpresora.Text = configuracion.ftp.nombre_impresora;
            this.configuracion = configuracion;
            this.ruta = ruta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Actulizo_ftp(this.configuracion);
        }

        public void Actulizo_ftp(Configuracion_Model configuracion)
        {
            Archivo_Binario ab = new Archivo_Binario();

            if (cmbImpresora.SelectedIndex != -1)
            {
                configuracion.ftp.nombre_impresora = cmbImpresora.Text;
            }
            configuracion.ftp.ftp_password = txt_passworf_ftp.Text;
            configuracion.ftp.ftp_path = txt_ruta_ftp.Text;
            configuracion.ftp.ftp_server = txt_servidor.Text;
            configuracion.ftp.ftp_user = txt_usuario_ftp.Text;



            ab.SetFileReadAccess(ruta, false);
            File.Delete(ruta);
            ab.Guardar(configuracion, this.ruta);
            System.Windows.Forms.MessageBox.Show("Se actualizo Correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void cmbImpresorasInstaladas()
        {
            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String impresorasInstaladas;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                impresorasInstaladas = PrinterSettings.InstalledPrinters[i];
                cmbImpresora.Items.Add(impresorasInstaladas);
            }
        }

        private void cmbImpresora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbImpresora.SelectedIndex != -1)
            {

                string impresora = cmbImpresora.Text;
            }
        }
    }
}