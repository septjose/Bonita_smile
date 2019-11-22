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
using MySql.Data.MySqlClient;
using bonita_smile_v1.Modelos;

namespace bonita_smile_v1.Interfaz.Administrador.Clinica
{
    /// <summary>
    /// Lógica de interacción para Ingresar_Clinica.xaml
    /// </summary>
    public partial class Ingresar_Clinica : MetroWindow
    {
        
        
        public Ingresar_Clinica()
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
            if(correcto)
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
