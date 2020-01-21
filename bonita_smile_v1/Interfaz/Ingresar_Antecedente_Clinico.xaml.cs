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
using System.Windows.Forms;

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

        private void btnOmitir_Click(object sender, RoutedEventArgs e)
        {
            Paciente pa = new Paciente();
            bool inserto = pa.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
            if (inserto)
            {
                System.Windows.Forms.MessageBox.Show("Se Ingreso  el Paciente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se pudo  Ingresar el Paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
