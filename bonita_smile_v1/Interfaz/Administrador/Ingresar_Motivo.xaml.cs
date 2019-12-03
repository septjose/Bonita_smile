using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using bonita_smile_v1.Servicios;

namespace bonita_smile_v1.Interfaz.Administrador
{
    /// <summary>
    /// Lógica de interacción para Ingresar_Motivo.xaml
    /// </summary>
    public partial class Ingresar_Motivo : Window
    {
        int id = 0;
        public Ingresar_Motivo(int id_paciente)
        {
            InitializeComponent();
            id = id_paciente;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txt_nombre.Text;
            double costo= Convert.ToDouble(txt_costo.Text);

            Motivo_cita mc = new Motivo_cita();
            bool inserto = mc.insertarMotivo_cita(nombre, costo, id);
            if(inserto)
            {
                MessageBox.Show("se ingreso");
            }
            else
            {
                MessageBox.Show("no se ingreso");
            }
            
        }
    }
}
