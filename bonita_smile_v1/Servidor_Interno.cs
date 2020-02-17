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
    public partial class Servidor_Interno : Form
    {
        Configuracion_Model configuracion;
        string ruta;
        public Servidor_Interno(Configuracion_Model configuracion, string ruta)
        {
            InitializeComponent();
            txt_ip.Text = configuracion.servidor_interno.servidor_local;
            txt_puerto.Text = configuracion.servidor_interno.puerto_local;
            txt_nombre.Text = configuracion.servidor_interno.usuario_local;
            txt_password.Text = configuracion.servidor_interno.password_local;
            txt_bd.Text = configuracion.servidor_interno.database_local;
            txt_bd_aux.Text = configuracion.servidor_interno.database_local_aux;
            this.configuracion = configuracion;
            this.ruta = ruta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Actualizo_servidor_interno(this.configuracion);
        }

        public void Actualizo_servidor_interno(Configuracion_Model configuracion)
        {

            Archivo_Binario ab = new Archivo_Binario();

            configuracion.servidor_interno.servidor_local = txt_ip.Text;
            configuracion.servidor_interno.puerto_local = txt_puerto.Text;
            configuracion.servidor_interno.usuario_local = txt_nombre.Text;
            configuracion.servidor_interno.password_local = txt_password.Text;
            configuracion.servidor_interno.database_local = txt_bd.Text;
            configuracion.servidor_interno.database_local_aux = txt_bd_aux.Text;

            ab.SetFileReadAccess(ruta, false);
            File.Delete(ruta);
            ab.Guardar(configuracion, this.ruta);
            System.Windows.Forms.MessageBox.Show("Se actualizo Correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void txt_bd_aux_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_bd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_puerto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ip_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
