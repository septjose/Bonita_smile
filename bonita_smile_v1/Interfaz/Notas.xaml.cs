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
    /// Lógica de interacción para Page2_notas.xaml
    /// </summary>
    public partial class Page2_notas : Page
    {
        ObservableCollection<Nota_de_digi_evolucionModel> GNotas;
        PacienteModel paciente;
        Motivo_citaModel motivo;
        string id_motivo = "";
        string id_paciente = "";
        bool bandera_online_offline = false;
        public Page2_notas(PacienteModel paciente,Motivo_citaModel motivo)
        {

            
            InitializeComponent();
           rt_imagen.Fill =new Page2().Imagen(paciente.foto);

           
            this.paciente = paciente;
            this.motivo = motivo;
            txtNombre.Text = paciente.nombre + " " + paciente.apellidos;
            txtNombre.IsEnabled = false;
            txtMotivo.Text= motivo.descripcion;
            txtMotivo.IsEnabled = false;
            txtTotal.IsEnabled = false;
            txtAbonado.IsEnabled = false;
            txtRestante.IsEnabled = false;
            //lblmotivo.Content = motivo.descripcion;
            //lblTotal.Content = motivo.costo.ToString();
            Abonos abono = new Abonos(bandera_online_offline);
            txtTotal.Text= "$"+motivo.costo.ToString();
            txtAbonado.Text= "$" + abono.Abonados(motivo.id_motivo).ToString();
            txtRestante.Text="$" + abono.Restante(motivo.id_motivo).ToString();
            //lblAbonado.Content = abono.Abonados(motivo.id_motivo).ToString();
            //lblRestante.Content = 
            //System.Windows.MessageBox.Show(motivo.id_motivo.ToString() + "  " + paciente.id_paciente.ToString());
            id_motivo = motivo.id_motivo;
            id_paciente = paciente.id_paciente;
            llenar_list_view(motivo.id_motivo, paciente.id_paciente);
                
        }
        
        void llenar_list_view(string id_motivo,string id_paciente)
        {
            var notas = new ObservableCollection<Nota_de_digi_evolucionModel>(new Servicios.Nota_de_digi_evolucion(false).MostrarNota_de_digi_evolucion(id_motivo,id_paciente));

            lvNotas.ItemsSource = notas;
            GNotas = notas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Agregar_Nota_Evolucion(motivo.id_motivo, paciente.id_paciente);
            resultado = mensaje.ShowDialog();
            lvNotas.ItemsSource = null;
            lvNotas.ItemsSource = new ObservableCollection<Nota_de_digi_evolucionModel>(new Servicios.Nota_de_digi_evolucion(false).MostrarNota_de_digi_evolucion(id_motivo, id_paciente));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

            if (admin != null)
                admin.Main.Content = new Pagina_Estudios(paciente);
            else
            {
                clin.Main2.Content = new Pagina_Estudios(paciente);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            

            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            Clin clin = System.Windows.Application.Current.Windows.OfType<Clin>().FirstOrDefault();

            if (admin != null)
                admin.Main.Content = new Page2_Abonos(paciente, motivo);
            else
            {
                clin.Main2.Content = new Page2_Abonos(paciente, motivo);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Nota_de_digi_evolucionModel nota = (Nota_de_digi_evolucionModel)lvNotas.SelectedItem;

            Test_Internet ti = new Test_Internet();
            if (ti.Test())
            {
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el motivo :" + nota.descripcion + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Motivo_cita mot = new Motivo_cita(false);

                    bool elimino = mot.eliminarMotivo_cita(motivo.id_motivo);
                    if (elimino)
                    {
                        mot = new Motivo_cita(false);

                        mot.eliminarMotivo_cita(motivo.id_motivo);
                        GNotas.Remove((Nota_de_digi_evolucionModel)lvNotas.SelectedItem);
                        System.Windows.Forms.MessageBox.Show("Se elimino el motivo correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("No se puede eliminar el registro hasta que tengas internet", "Error Falta de Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Nota_de_digi_evolucionModel nota = (Nota_de_digi_evolucionModel)lvNotas.SelectedItem;
            if (lvNotas.SelectedItems.Count > 0)
            {
                DialogResult resultado = new DialogResult();
                Form mensaje = new Actualizar_Nota_Evolucion(nota);
                resultado = mensaje.ShowDialog();
                lvNotas.ItemsSource = null;
                lvNotas.ItemsSource = new ObservableCollection<Nota_de_digi_evolucionModel>(new Servicios.Nota_de_digi_evolucion(false).MostrarNota_de_digi_evolucion(id_motivo, id_paciente));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
