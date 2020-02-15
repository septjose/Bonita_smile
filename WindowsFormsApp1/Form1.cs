using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Servicios;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //string ruta = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.cfg");
        string ruta = @"\\DESKTOP-ED8E774\dentista2\setup\conf\configuracion.cfg";
        Configuracion_Model configuracion;
        public Form1()
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
            //Configuracion_Model configuracion;
            //ServidorModelo servidor_intern = new ServidorModelo()
            //{
            //    servidor_local = "192.168.02",
            //    puerto_local = "3306",
            //    usuario_local = "usuariochido",
            //    password_local = "12345",
            //    database_local = "dentista",
            //    database_local_aux = "bs",
            //};

            //ServidorModelo servidor_extern = new ServidorModelo()
            //{
            //    puerto_local = "3306",
            //    usuario_local = "jjdevelo_dentista",
            //    password_local = "jjpd1996",
            //    database_local = "jjdevelo_dentist",
            //};
            //RutasCarpetasModelo carpeta = new RutasCarpetasModelo()
            //{
            //    ruta_fotografias_carpeta = "fotografias",
            //    ruta_imagenes_carpeta = "imagenes",
            //    ruta_subir_servidor_carpeta = "subir servidor",
            //    ruta_temporal_carpeta = "temporal",
            //};



            //configuracion = new Configuracion_Model()
            //{
            //    carpetas = carpeta,
            //    servidor_externo = servidor_extern,
            //    servidor_interno = servidor_intern

            //};
            //ab.Guardar(configuracion, ruta);

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servidor_Interno s_i = new Servidor_Interno(this.configuracion, this.ruta);
            s_i.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Servidor_Externo s_e = new Servidor_Externo(this.configuracion, this.ruta);
            s_e.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Configurar_Carpetas c_c = new Configurar_Carpetas(this.configuracion, this.ruta);
            c_c.Show();
        }
    }
}
