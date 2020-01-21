using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page7_Actualizar.xaml
    /// </summary>
    public partial class Page7_Actualizar : Page
    {
        PacienteModel paciente;
        public Page7_Actualizar(PacienteModel paciente)
        {

            InitializeComponent();
            txtAntecedentes.Text = paciente.antecedente;
            //MessageBox.Show(paciente.antecedente);
            this.paciente = paciente;

        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("la foto es :" + paciente.foto);
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Page8_ActualizarFoto(paciente); ;

            /*string descripcion = txtAntecedentes.Text;
            Antecedentes_clinicos ac = new Antecedentes_clinicos();
            Servicios.Paciente paciente = new Servicios.Paciente();
            bool insertarPaciente = paciente.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, descripcion, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
            if (insertarPaciente)
            {
                MessageBox.Show("Exito");
            }
            else
            {
                MessageBox.Show("No se inserto");
            }*/
        }

        private void btnOmitir_Click(object sender, RoutedEventArgs e)
        {
            Paciente pa = new Paciente();
            bool inserto = pa.actualizarPaciente(this.paciente.id_paciente, this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, txtAntecedentes.Text, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
            if (inserto)
            {



                //vu.refrescar_listview(this.usu, usu, lv_aux);
                System.Windows.Forms.MessageBox.Show("Se actualizo el Usuario", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
