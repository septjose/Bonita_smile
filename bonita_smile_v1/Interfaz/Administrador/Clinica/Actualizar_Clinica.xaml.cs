﻿using System;
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
using MySql.Data.MySqlClient;
using bonita_smile_v1.Servicios;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using System.Windows.Forms;

namespace bonita_smile_v1.Interfaz.Administrador.Clinica
{
    /// <summary>
    /// Lógica de interacción para Actualizar_Clinica.xaml
    /// </summary>
    public partial class Actualizar_Clinica : MetroWindow
    {
       
        //string valor = "";
        public int id_clin = 0;
        
        public Actualizar_Clinica(ClinicaModel clinica)
        {
            InitializeComponent();
            txtNombre.Text = clinica.nombre_sucursal;
            lblColor.Content = clinica.color;
            id_clin = clinica.id_clinica;
            System.Windows.Forms.MessageBox.Show(clinica.color);
            
            llenar_Combo();
        }

        public void llenar_Combo()
        {
            cmbColor.ItemsSource = typeof(Colors).GetProperties();
        }
            private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            Clinicas cl = new Clinicas();
            string nombre_clinica = txtNombre.Text;
            int id_clinica = id_clin;
            int combo = cmbColor.SelectedIndex;
            string color = "";
                if(combo>-1)
            {
                color = cmbColor.SelectedItem.ToString().Replace("System.Windows.Media.Color", "");
                System.Windows.Forms.MessageBox.Show("se eligio un color     "+color);
                bool actualizo = cl.actualizarClinica(id_clinica, nombre_clinica, color);
                if (actualizo)
                {
                    System.Windows.Forms.MessageBox.Show("Se actualizo la Clinica", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                  
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                color = lblColor.Content.ToString();
                System.Windows.Forms.MessageBox.Show("No se eligio ningun color      "+color);
                bool actualizo = cl.actualizarClinica(id_clinica, nombre_clinica, color);
                if (actualizo)
                {
                    System.Windows.Forms.MessageBox.Show("Se actualizo la Clinica", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}