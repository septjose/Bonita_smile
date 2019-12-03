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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bonita_smile_v1.Interfaz.Administrador.Antecedentes
{
    /// <summary>
    /// Lógica de interacción para Actualizar_Antecedentes.xaml
    /// </summary>
    public partial class Actualizar_Antecedentes : MetroWindow
    {
        PacienteModel paciente;
        public Actualizar_Antecedentes(PacienteModel paciente)
        {
           
            InitializeComponent();
            txtAntecedentes.Text = paciente.antecedente;
            //MessageBox.Show(paciente.antecedente);
            this.paciente = paciente;
            
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = txtAntecedentes.Text;
            Antecedentes_clinicos ac = new Antecedentes_clinicos();
            Servicios.Paciente paciente = new Servicios.Paciente();
            bool insertarPaciente = paciente.actualizarPaciente(this.paciente.id_paciente,this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, descripcion, this.paciente.email, this.paciente.marketing, this.paciente.clinica.id_clinica);
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

