using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bonita_smile_v1
{
    public partial class Configurar_Carpetas : Form
    {
        Configuracion_Model configuracion;
        string ruta;
        public Configurar_Carpetas(Configuracion_Model configuracion,string ruta)
        {
            InitializeComponent();
            txt_imagen.Text=configuracion.carpetas.ruta_imagenes_carpeta;
            txt_subir_servidor.Text = configuracion.carpetas.ruta_subir_servidor_carpeta;
            txt_fotografias.Text = configuracion.carpetas.ruta_fotografias_carpeta;
            txt_temporal.Text = configuracion.carpetas.ruta_respaldo_carpeta;
            txt_script.Text = configuracion.carpetas.ruta_script_carpeta;
            txt_eliminar.Text = configuracion.carpetas.ruta_eliminar_carpeta;
            txt_acceso_directo.Text = configuracion.carpetas.ruta_acceso_deirecto;
            this.configuracion = configuracion;
            this.ruta = ruta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txt_imagen.Text.Equals(txt_subir_servidor.Text)|| txt_imagen.Text.Equals(txt_fotografias.Text)||txt_imagen.Text.Equals(txt_temporal.Text)||txt_imagen.Text.Equals(txt_script.Text)||txt_imagen.Text.Equals(txt_eliminar.Text)|| txt_imagen.Text.Equals(txt_acceso_directo.Text) ||
                txt_subir_servidor.Text.Equals(txt_fotografias.Text)||txt_subir_servidor.Text.Equals(txt_temporal.Text)||txt_subir_servidor.Text.Equals(txt_script.Text)||txt_subir_servidor.Text.Equals(txt_eliminar.Text)|| txt_subir_servidor.Text.Equals(txt_acceso_directo.Text) ||
                txt_fotografias.Text.Equals(txt_temporal.Text)||txt_fotografias.Text.Equals(txt_script.Text)||txt_fotografias.Text.Equals(txt_eliminar.Text)|| txt_fotografias.Text.Equals(txt_acceso_directo.Text) ||
                txt_temporal.Text.Equals(txt_script.Text)||txt_temporal.Text.Equals(txt_eliminar.Text)|| txt_temporal.Text.Equals(txt_acceso_directo.Text) ||
                txt_script.Text.Equals(txt_eliminar.Text) || txt_script.Text.Equals(txt_acceso_directo.Text) ||
                txt_eliminar.Text.Equals(txt_acceso_directo.Text))
            {
                System.Windows.Forms.MessageBox.Show("No se deben de repetir las rutas ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else
            {
               
                Actualizo_rutas_carpetas(this.configuracion);
            }
            
        }

        public void Actualizo_rutas_carpetas(Configuracion_Model configuracion)
        {
            Archivo_Binario ab = new Archivo_Binario();

            configuracion.carpetas.ruta_imagenes_carpeta = txt_imagen.Text;
            configuracion.carpetas.ruta_subir_servidor_carpeta = txt_subir_servidor.Text;
            configuracion.carpetas.ruta_fotografias_carpeta = txt_fotografias.Text;
            configuracion.carpetas.ruta_respaldo_carpeta = txt_temporal.Text;
            configuracion.carpetas.ruta_script_carpeta = txt_script.Text;
            configuracion.carpetas.ruta_eliminar_carpeta = txt_eliminar.Text;
            configuracion.carpetas.ruta_acceso_deirecto = txt_acceso_directo.Text;
            ab.SetFileReadAccess(ruta, false);
            File.Delete(ruta);

            ab.Guardar(configuracion, this.ruta);
            System.Windows.Forms.MessageBox.Show("Se actualizo Correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

       
        private void btn_imagenes_Click(object sender, EventArgs e)
        {          
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_imagen.Text = tempPath;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        }

        private void btn_temporal_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_temporal.Text = tempPath;
            }
        }

        private void btn_fotografias_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_fotografias.Text = tempPath;
            }
        }

        private void btn_subir_servidor_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_subir_servidor.Text = tempPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_script.Text = tempPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_eliminar.Text = tempPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tempPath = "";

            if (folder_imagenes.ShowDialog() == DialogResult.OK)
            {
                tempPath = folder_imagenes.SelectedPath; // prints path
                txt_acceso_directo.Text = tempPath;
            }
        }
    }
}
