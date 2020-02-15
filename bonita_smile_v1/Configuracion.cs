using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bonita_smile_v1
{
    public partial class Configuracion : Form
    {
        Configuracion_Model configuracion;
        string ruta;
        public Configuracion(Configuracion_Model configuracion,string ruta)
        {
            InitializeComponent();
            this.configuracion = configuracion;
            this.ruta = ruta;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Servidor_Interno s_i= new Servidor_Interno(this.configuracion,this.ruta);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
