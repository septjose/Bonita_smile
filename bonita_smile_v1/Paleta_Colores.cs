using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace bonita_smile_v1
{
    public partial class Paleta_Colores : Form
    {
        public Paleta_Colores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {

            }
            System.Drawing.Color color =MyDialog.Color;

            System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);

            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
            if (admin != null)
            {
                admin.paleta_colores.Background = new SolidColorBrush(mediaColor);
            }
            else
                if (clin != null)
            {
                clin.paleta_colores.Background = new SolidColorBrush(mediaColor);
                this.Hide();
            }else
                if (recep != null)
            {
                recep.paleta_colores.Background = new SolidColorBrush(mediaColor);
                this.Hide();
            }
            else
                if (socio != null)
            {
                socio.paleta_colores.Background = new SolidColorBrush(mediaColor);
                this.Hide();
            }
        }
    }
}
