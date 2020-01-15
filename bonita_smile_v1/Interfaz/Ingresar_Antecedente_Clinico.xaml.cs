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
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Interfaz.Administrador;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page7_Ingresar.xaml
    /// </summary>
    public partial class Page7_Ingresar : Page
    {
        PacienteModel paciente;
        public Page7_Ingresar(PacienteModel paciente)
        {
            InitializeComponent();
            this.paciente = paciente;
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = txtAntecedentes.Text;           
            paciente.antecedente = descripcion;
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Page8_IngresarFoto(paciente); 
        }

    }
}
