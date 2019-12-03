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
            Antecedentes_clinicos ac = new Antecedentes_clinicos();
            Servicios.Paciente paciente = new Servicios.Paciente();
            bool insertarPaciente = paciente.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, descripcion, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
            if (insertarPaciente)
            {
                MessageBox.Show("Exito");
            }
            else
            {
                MessageBox.Show("No se inserto");
            }
        }

    }
}
