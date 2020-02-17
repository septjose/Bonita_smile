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
    public partial class Servidor_Externo : Form
    {
        Configuracion_Model configuracion;
        string ruta;
        public Servidor_Externo(Configuracion_Model configuracion,string ruta)
        {
            InitializeComponent();
            txt_ip.Text = configuracion.servidor_externo.servidor_local;
            txt_puerto.Text = configuracion.servidor_externo.puerto_local;
            txt_nombre.Text = configuracion.servidor_externo.usuario_local;
            txt_password.Text = configuracion.servidor_externo.password_local;
            txt_bd.Text = configuracion.servidor_externo.database_local;
           
            this.configuracion = configuracion;
            this.ruta = ruta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Actualizo_servidor_Externo(this.configuracion);
        }

        public void Actualizo_servidor_Externo(Configuracion_Model configuracion)
        {
            Archivo_Binario ab = new Archivo_Binario();
            configuracion.servidor_externo.servidor_local = txt_ip.Text;
            configuracion.servidor_externo.puerto_local = txt_puerto.Text;
            configuracion.servidor_externo.usuario_local = txt_nombre.Text;
            configuracion.servidor_externo.password_local = txt_password.Text;
            configuracion.servidor_externo.database_local = txt_bd.Text;
           

            
            ab.SetFileReadAccess(ruta, false);
            File.Delete(ruta);
            ab.Guardar(configuracion, this.ruta);
            System.Windows.Forms.MessageBox.Show("Se actualizo Correctamente", "Se actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }
    }
}
