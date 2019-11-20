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

namespace bonita_smile_v1.Interfaz.Administrador.Antecedentes
{
    /// <summary>
    /// Lógica de interacción para Ingresar_Antecedentes_Clinicos.xaml
    /// </summary>
    public partial class Ingresar_Antecedentes_Clinicos : MetroWindow
    {
        public Ingresar_Antecedentes_Clinicos()
        {
            InitializeComponent();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = txtAntecedentes.Text;
            Antecedentes_clinicos ac = new Antecedentes_clinicos();
            bool inserto = ac.insertarAntecedentes_clinicos(descripcion);
            if(inserto)
            {
                MessageBox.Show("si");
            }
            else
            {
                MessageBox.Show("no");
            }
        }

       
    }
}
