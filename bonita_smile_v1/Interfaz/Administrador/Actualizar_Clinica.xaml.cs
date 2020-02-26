using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Modelos;
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
    /// Lógica de interacción para Page5_Actualizar.xaml
    /// </summary>
    public partial class Page5_Actualizar : Page
    {
        //string valor = "";
        public string id_clin = "";
        bool bandera_online_offline = false;
        string alias;
        public Page5_Actualizar(ClinicaModel clinica,string alias)
        {
            InitializeComponent();
            txtNombre.Text = clinica.nombre_sucursal;
            lblColor.Content = clinica.color;
            id_clin = clinica.id_clinica;
            this.alias = alias;
            //System.Windows.Forms.MessageBox.Show(clinica.color);

            llenar_Combo();
        }

        public void llenar_Combo()
        {
            cmbColor.ItemsSource = typeof(Colors).GetProperties();
        }
        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Falta llenar Campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
              
                    Clinicas cl = new Clinicas(bandera_online_offline);
                    Usuarios usu = new Usuarios(bandera_online_offline);
                    string nombre_clinica = txtNombre.Text;
                    string id_clinica = id_clin;
                    int combo = cmbColor.SelectedIndex;
                    string color = "";
                    string id_permiso = "";
                    if (combo > -1)
                    {
                        color = cmbColor.SelectedItem.ToString().Replace("System.Windows.Media.Color", "");
                        // System.Windows.Forms.MessageBox.Show("se eligio un color     " + color);
                        bool actualizo = cl.actualizarClinica(id_clinica, nombre_clinica, color, alias);
                    if (actualizo)
                    {
                        

                        System.Windows.Forms.MessageBox.Show("Se actualizo la Clinica", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //cl = new Clinicas(!bandera_online_offline);
                        //cl.actualizarClinica(id_clinica, nombre_clinica, color);
                        //Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        //if (admin != null) 
                        //{
                        //    alias = usu.Buscar_Alias(id_clinica);
                        //    id_permiso = usu.Buscar_Permiso(id_clinica);
                        //    if (alias.Equals("") || id_permiso.Equals(""))
                        //    {
                        //        System.Windows.Forms.MessageBox.Show("No le asignaste el usuario a la clinica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        if (admin != null)
                        //            admin.Main.Content = new Pagina_Ingresar_Permisos();
                        //    }
                        //    else
                        //    {


                        //        System.Windows.MessageBox.Show("id_clinica " + id_clinica);

                        //        admin.Main.Content = new Pagina_Actualizar_Permisos(alias, nombre_clinica, id_permiso);
                        //    }
                        //}  
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                            admin.Main.Content = new Page5(alias);

                    }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        color = lblColor.Content.ToString();
                        //System.Windows.Forms.MessageBox.Show("No se eligio ningun color      " + color);
                        bool actualizo = cl.actualizarClinica(id_clinica, nombre_clinica, color, alias);
                        if (actualizo)
                        {
                        
                        System.Windows.Forms.MessageBox.Show("Se actualizo la Clinica", "Se Actualizo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //cl = new Clinicas(!bandera_online_offline);
                        //cl.actualizarClinica(id_clinica, nombre_clinica, color);
                        //Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        //    if (admin != null)
                        //    {
                        //    alias = usu.Buscar_Alias(id_clinica);
                        //    id_permiso = usu.Buscar_Permiso(id_clinica);
                        //    if (alias.Equals("") || id_permiso.Equals(""))
                        //    {


                        //        System.Windows.Forms.MessageBox.Show("No le asignaste el usuarioa la clinica ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        if (admin != null)
                        //            admin.Main.Content = new Pagina_Ingresar_Permisos();
                        //    }
                        //    else
                        //    {


                        //        System.Windows.MessageBox.Show("id_clinica " + id_clinica);

                        //        admin.Main.Content = new Pagina_Actualizar_Permisos(alias, nombre_clinica, id_permiso);
                        //    }
                        //}
                        Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                        if (admin != null)
                            admin.Main.Content = new Page5(alias);

                    }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("No se pudo Actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

               
            }
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
