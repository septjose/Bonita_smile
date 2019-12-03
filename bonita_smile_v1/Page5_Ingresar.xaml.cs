using bonita_smile_v1.Servicios;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page5_Ingresar.xaml
    /// </summary>
    public partial class Page5_Ingresar : Page
    {
        public Page5_Ingresar()
        {

            InitializeComponent();
            cmbColor.ItemsSource = typeof(Colors).GetProperties();
        }



        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {

            string color = cmbColor.SelectedItem.ToString().Replace("System.Windows.Media.Color", "");

            // MessageBox.Show(color);

            //MessageBox.Show(color);
            string nombre_sucursal = txtNombre.Text;
            MessageBox.Show(nombre_sucursal);
            Clinicas c = new Clinicas();
            bool correcto = c.insertarClinica(nombre_sucursal, color);
            if (correcto)
            {
                MessageBox.Show("SI");
            }
            else
            {
                MessageBox.Show("No");
            }


        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
