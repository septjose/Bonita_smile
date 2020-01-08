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
using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Modelos;
namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Pagina_Estudios.xaml
    /// </summary>
    public partial class Pagina_Estudios : Page
    {
        public Pagina_Estudios(PacienteModel paciente)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

            if (admin != null)
                admin.Main.Content = new Pagina_Agregar_Estudios();
            else
            {
                clin.Main2.Content = new Pagina_Agregar_Estudios();
            }
        }
            
    }
}
