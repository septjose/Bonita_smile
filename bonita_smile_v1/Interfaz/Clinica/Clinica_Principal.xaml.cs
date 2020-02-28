using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;


using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page1.xaml
    /// </summary>
    public partial class Pagina_Clinica : Page
    {
        ObservableCollection<PacienteModel> GPaciente;
        string id_clinica = "";
        string alias;
        string nombre_doctor;
        public Pagina_Clinica(string id,string nombre_doctor,string alias)
        {

            InitializeComponent();
            llenar_list_view(id);
            id_clinica = id;
           // System.Windows.MessageBox.Show("imprimo el id de la clinica");

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource);
            view.Filter = UserFilter;
            this.alias = alias;
            this.nombre_doctor = nombre_doctor;

        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtNombre.Text))
                return true;
            else
                return ((item as PacienteModel).nombre.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0 || (item as PacienteModel).apellidos.IndexOf(txtNombre.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        void llenar_list_view(string id)
        {
            var pacientes = new ObservableCollection<PacienteModel>(new Servicios.Paciente(false).MostrarPaciente_Clinica(id));

            lv_Paciente.ItemsSource = pacientes;
            GPaciente = pacientes;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Paciente.ItemsSource).Refresh();
        }

        /*private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //verificar si hay internet   -listo
            //subir los scripts del archivo -listo
            //hacer respaldo y restaurar - listo
            //subir fotos a la nube - listo
            //descargar las fotos   

            Test_Internet ti = new Test_Internet();
            Sincronizar sinc = new Sincronizar();
            bool verificar = ti.Test();
            if (verificar)
            {
                System.Windows.MessageBox.Show("hi");
                try
                {
                    System.Windows.MessageBox.Show("hii x2");
                    bool subir_scripts = sinc.SincronizarLocalServidor();
                    if (subir_scripts) { System.Windows.MessageBox.Show("se subieron los scripts"); }
                    System.Windows.MessageBox.Show("hii x3");
                    if (verificar)
                    {
                        System.Windows.MessageBox.Show("se hace el backup");
                        sinc.Backup();
                        System.Windows.MessageBox.Show("despues backup");
                        bool borrar = sinc.borrar_bd();
                        if (borrar)
                        {
                            System.Windows.MessageBox.Show("Se borro la bd");
                            bool si_creo = sinc.crear_bd();
                            if (si_creo)
                            {
                                System.Windows.MessageBox.Show("se creo la bd");
                                sinc.Restore();

                                bool subio_fotos = sinc.subir_fotos();
                                if (subio_fotos)
                                {
                                    System.Windows.MessageBox.Show("se subieron las fotos correctamente");
                                    bool descargar_fotos = sinc.descargar_fotos_clinica(id_clinica);
                                    if (descargar_fotos)
                                    {
                                        System.Windows.MessageBox.Show("se descargaron las fotos correctamente");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("hubo problemas al subir las fotos");
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("hubo problemas al subir las fotos");
                                }


                            }
                            else
                            {
                                System.Windows.MessageBox.Show("No se pudo crear bd ");
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("No se pudo borrar");
                        }
                    }



                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No hay conexión a internet intente más tarde.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            PacienteModel paciente = (PacienteModel)lv_Paciente.SelectedItem;
            if (lv_Paciente.SelectedItems.Count > 0)
            {
                //System.Windows.MessageBox.Show("hi");
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();
                if(clin!=null)
                {
                   
                        clin.Main2.Content = new Datos_Personales_Doctor(paciente,nombre_doctor,alias);
                    
                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No seleccionó ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
