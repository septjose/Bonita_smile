using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using bonita_smile_v1.Servicios;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using bonita_smile_v1.Interfaz;
using bonita_smile_v1.Interfaz.Socio;
using System.Globalization;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        // ObservableCollection<PacienteModel> GPaciente;
        string alias;
        string nombre_doctor;
        public Page1(string alias,string nombre_doctor)
        {
            InitializeComponent();
            llenar_list_view();
            Conexion con = new Conexion();
            this.alias = alias;
            this.nombre_doctor = nombre_doctor;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource);
            view.Filter = UserFilter;

        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
                return true;
            else
                return ((item as PacienteModel).nombre.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0 || (item as PacienteModel).apellidos.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        void llenar_list_view()
        {
            //var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente().MostrarPaciente());
            List<PacienteModel> pacientes=new Servicios.Paciente(false).MostrarPaciente();
            lv_Paciente.ItemsSource = pacientes;
            //GPaciente = pacientes;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource).Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // File.Delete(@"\\DESKTOP-ED8E774\bs\"+paci)
           
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                
                //System.Windows.MessageBox.Show("hi");
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
               // Market market = System.Windows.Application.Current.Windows.OfType<Market>().FirstOrDefault();

                if (admin != null)
                {
                    admin.Main.Content = new Page2(paciente, nombre_doctor, alias);
                }
                    
                //else
                //if( clin != null)
                //{
                //    clin.Main2.Content = new Page2(paciente, alias);
                //}
                //else
                //    if(market != null)
                //{
                //    market.Main3.Content = new Page2(paciente);
                //}
                    

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

           Test_Internet ti = new Test_Internet();
            Sincronizar sinc = new Sincronizar();
            bool verificar = ti.Test();
            if (verificar)
            {
               // System.Windows.MessageBox.Show("hi");
                try
                {
                     // System.Windows.MessageBox.Show("hii x2");
                    bool subir_scripts = sinc.SincronizarLocalServidor();
                    if (subir_scripts) { /*System.Windows.MessageBox.Show("se subieron los scripts");*/ }
                   // System.Windows.MessageBox.Show("hii x3");
                    if (verificar)
                    {
                        //System.Windows.MessageBox.Show("se hace el backup");
                        sinc.Backup();
                        //System.Windows.MessageBox.Show("despues backup");
                        bool borrar = sinc.borrar_bd();
                        if (borrar)
                        {
                          //  System.Windows.MessageBox.Show("Se borro la bd");
                            bool si_creo = sinc.crear_bd();
                            if (si_creo)
                            {
                            //    System.Windows.MessageBox.Show("se creo la bd");
                                sinc.Restore();

                                bool subio_fotos = sinc.subir_fotos();
                                if (subio_fotos)
                                {
                              //      System.Windows.MessageBox.Show("se subieron las fotos correctamente");
                                //    System.Windows.MessageBox.Show("toca eliminar fotos");
                                    bool eliminar_fotos = sinc.eliminar_fotos();
                                    if (eliminar_fotos)
                                    {
                                       // System.Windows.MessageBox.Show("se eliminaron las fotos");
                                    }
                                   // System.Windows.MessageBox.Show("toca descargar");
                                    bool descargar_fotos = sinc.descargar_fotos();
                                    if (descargar_fotos)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Se realizó correctamente la Sincronización ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }


                                    else
                                    {
                                        System.Windows.Forms.MessageBox.Show("Se ha producido un error al Sincronizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Se ha producido un error al Sincronizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Se ha producido un error al Sincronizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Se ha producido un error al Sincronizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Se ha producido un error al Sincronizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                List<PacienteModel> pacientes = new Servicios.Paciente(false).MostrarPaciente();
                lv_Paciente.ItemsSource = pacientes;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No hay conexión a internet intente más tarde.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");
            string id_membresia = "" ;
            double abono = 0.0;
            Membresia me = new Membresia(false);
            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                List<MembresiaModel> list_membresia = me.MostrarMembresias(paciente.id_paciente, paciente.clinica.id_clinica);
                    foreach(var membresia in list_membresia)
                {
                    id_membresia = membresia.id_membresia;
                    abono=Convert.ToDouble(membresia.costo);
                }
                if (id_membresia.Equals(""))
                {
                    DialogResult resultado = new DialogResult();
                    Form mensaje = new InsertarMembresia(paciente,alias);
                    resultado = mensaje.ShowDialog();
                    //List<PacienteModel> pacientes = new Servicios.Paciente(false).MostrarPaciente();
                    //lv_Paciente.ItemsSource = pacientes;
                }
                else
                {
                    //DialogResult resultado = new DialogResult();
                    //Form mensaje = new EliminarMembresia(paciente,alias);
                    //resultado = mensaje.ShowDialog();
                    //List<PacienteModel> pacientes = new Servicios.Paciente(false).MostrarPaciente();
                    //lv_Paciente.ItemsSource = pacientes;
                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                    Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

                    if (admin != null)
                    {
                        admin.Main.Content = new Abonos_Mem(paciente, id_membresia, abono, alias);
                    }
                    //else
                    //if (clin != null)
                    //{
                    //    clin.Main2.Content = new Abonos_Mem(paciente, id_membresia, abono, alias);
                    //}
                    //else
                    //if (socio != null)
                    //{
                    //    socio.Main4.Content = new Abonos_Mem(paciente, id_membresia, abono, alias);
                    //}

                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se seleccionó ningún registro ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
