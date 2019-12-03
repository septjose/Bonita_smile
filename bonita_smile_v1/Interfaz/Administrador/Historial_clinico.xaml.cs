﻿using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace bonita_smile_v1.Interfaz.Administrador
{
    /// <summary>
    /// Lógica de interacción para Historial_clinico.xaml
    /// </summary>
    public partial class Historial_clinico : Window
    {
        ObservableCollection<Motivo_citaModel> GMotivo;
        int id = 0;
        public Historial_clinico(PacienteModel paciente)
        {
            
            InitializeComponent();
            lblNombre.Content = paciente.nombre;
            lblApellido.Content = paciente.apellidos;
            lblClinica.Content = paciente.clinica.nombre_sucursal;
            lblDireccion.Content = paciente.direccion;
            lblTelefono.Content = paciente.telefono;
            lblEmail.Content = paciente.email;
            tbAntecedentes.Text = paciente.antecedente;
            llenar_list_view(paciente.id_paciente);
            id = paciente.id_paciente;
        }

        void llenar_list_view(int id_paciente)
        {
            var motivos = new ObservableCollection<Motivo_citaModel>(new Servicios.Motivo_cita().Mostrar_MotivoCita(id_paciente));

            lvMotivo.ItemsSource = motivos;
            GMotivo = motivos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Motivo_citaModel motivo = (Motivo_citaModel)lvMotivo.SelectedItem;
            if (lvMotivo.SelectedItems.Count > 0)
            {
                System.Windows.MessageBox.Show("id_paciente :" + motivo.paciente.id_paciente.ToString() + "   " + "id_motivo   " + motivo.id_motivo.ToString());
               
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Windows[0].Close();

            Ingresar_Motivo im = new Ingresar_Motivo(id);
            im.ShowDialog();
        }
    }
}