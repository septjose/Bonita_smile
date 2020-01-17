﻿using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Servicios;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page5_Ingresar.xaml
    /// </summary>
    public partial class Page5_Ingresar : Page
    {
        public Page5_Ingresar()
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
            //MessageBox.Show(nombre_sucursal);
            Clinicas c = new Clinicas();
            bool correcto = c.insertarClinica(nombre_sucursal, color);
            if (correcto)
            {
                System.Windows.Forms.MessageBox.Show("Se Ingreso la Clinica correctamente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                if (admin != null)
                    admin.Main.Content = new Pagina_Ingresar_Permisos();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se ingreso la Clinica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}