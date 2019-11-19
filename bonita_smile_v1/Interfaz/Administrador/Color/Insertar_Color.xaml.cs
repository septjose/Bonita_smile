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
using MahApps.Metro.Controls;
using bonita_smile_v1.Servicios;

namespace bonita_smile_v1.Interfaz.Administrador.Color
{
    /// <summary>
    /// Lógica de interacción para Insertar_Color.xaml
    /// </summary>
    public partial class Insertar_Color : MetroWindow
    {
        public Insertar_Color()
        {
            InitializeComponent();
            //color();
            cmbColor.ItemsSource = typeof(Colors).GetProperties();

        }

        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
             string color = cmbColor.SelectedItem.ToString().Replace("System.Windows.Media.Color", "");
           
            MessageBox.Show(color);

            Colores c = new Colores();
            bool inserto = c.insertarColor(color);
            if(inserto)
            {
                MessageBox.Show("Si se pudo");
            }
            else
            {
                MessageBox.Show("No se pudo");
            }

        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
