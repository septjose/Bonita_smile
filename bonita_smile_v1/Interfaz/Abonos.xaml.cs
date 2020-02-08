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
    public partial class Page2_Abonos : Page
    {
        ObservableCollection<AbonosModel> GAbono;
        PacienteModel paciente;
        Motivo_citaModel motivo;
        double restante = 0.0;
        double abonado = 0.0;
        double total = 0.0;
        bool bandera_online_offline = false;
        ObservableCollection<AbonosModel> notas;
        public Page2_Abonos(PacienteModel paciente, Motivo_citaModel motivo)
        {

            InitializeComponent();
            rt_imagen.Fill = new Page2().Imagen( paciente.foto);
            this.paciente = paciente;
            this.motivo = motivo;
            //lblNombre.Content = paciente.nombre + " " + paciente.apellidos;
            //lblmotivo.Content = motivo.descripcion;
            //lblTotal.Content = motivo.costo.ToString();
            Abonos abono = new Abonos(bandera_online_offline);
            //lblAbonado.Content = abono.Abonados(motivo.id_motivo).ToString();
            //lblRestante.Content = abono.Restante(motivo.id_motivo).ToString();
            //System.Windows.MessageBox.Show(motivo.id_motivo.ToString() + "  " + paciente.id_paciente.ToString());
            txtNombre.Text = paciente.nombre + " " + paciente.apellidos;
            txtNombre.IsEnabled = false;
            txtMotivo.Text = motivo.descripcion;
            txtMotivo.IsEnabled = false;
            txtTotal.IsEnabled = false;
            txtAbonado.IsEnabled = false;
            txtRestante.IsEnabled = false;
            txtTotal.Text = "$" + motivo.costo.ToString();
            total = motivo.costo;
            txtAbonado.Text = "$" + abono.Abonados(motivo.id_motivo).ToString();
            txtRestante.Text = "$" + abono.Restante(motivo.id_motivo).ToString();
            restante = abono.Restante(motivo.id_motivo);
            abonado = abono.Abonados(motivo.id_motivo);
            llenar_list_view(motivo.id_motivo, paciente.id_paciente);
            

        }
        void llenar_list_view(string id_motivo, string id_paciente)
        {
             this.notas = new ObservableCollection<AbonosModel>(new Servicios.Abonos(bandera_online_offline).MostrarAbonos(id_motivo, id_paciente));

            lvNotas.ItemsSource = notas;
            GAbono = notas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Abonos abonos = new Abonos(bandera_online_offline);
            DialogResult resultado = new DialogResult();
            double restanten = abonos.Restante(motivo.id_motivo);
            Form mensaje = new MessageBoxAbono(motivo.id_motivo, paciente.id_paciente,txtNombre.Text,txtMotivo.Text, restanten, abonado, total);
            resultado = mensaje.ShowDialog();
            this.notas = new ObservableCollection<AbonosModel>(new Servicios.Abonos(bandera_online_offline).MostrarAbonos(motivo.id_motivo, paciente.id_paciente));
            lvNotas.ItemsSource = this.notas;
            
            txtAbonado.Text = "$" + abonos.Abonados(motivo.id_motivo).ToString();
            txtRestante.Text = "$" + abonos.Restante(motivo.id_motivo).ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AbonosModel abono = (AbonosModel)lvNotas.SelectedItem;
            if (lvNotas.SelectedItems.Count > 0)
            {
                    var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  abono :" + abono.comentario + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (confirmation == System.Windows.Forms.DialogResult.Yes)
                    {
                        Abonos abo = new Abonos(bandera_online_offline);

                        bool elimino = abo.eliminarAbono(abono.id_abono);
                        if (elimino)
                        {

                        abo = new Abonos(!bandera_online_offline);
                        abo.eliminarAbono(abono.id_abono);

                        
                            System.Windows.Forms.MessageBox.Show("Se elimino el abono correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        this.notas.Remove((AbonosModel)lvNotas.SelectedItem);

                        Abonos abonos = new Abonos(bandera_online_offline);
                        txtAbonado.Text = "$" + abonos.Abonados(motivo.id_motivo).ToString();
                        txtRestante.Text = "$" + abonos.Restante(motivo.id_motivo).ToString();
                    }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("No se pudo eliminar el abono", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                    }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

            private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AbonosModel abono = (AbonosModel)lvNotas.SelectedItem;
            if (lvNotas.SelectedItems.Count > 0)
            {
                Abonos abonos = new Abonos(bandera_online_offline);
                double restanten = abonos.Restante(motivo.id_motivo);
                DialogResult resultado = new DialogResult();
                Form mensaje = new Actualizar_Abono(motivo.id_motivo, paciente.id_paciente, txtNombre.Text, txtMotivo.Text, restanten, abonado, total, abono);
                resultado = mensaje.ShowDialog();
                
                txtAbonado.Text = "$" + abonos.Abonados(motivo.id_motivo).ToString();
                txtRestante.Text = "$" + abonos.Restante(motivo.id_motivo).ToString();
                this.notas = new ObservableCollection<AbonosModel>(new Servicios.Abonos(bandera_online_offline).MostrarAbonos(motivo.id_motivo, paciente.id_paciente));
                lvNotas.ItemsSource = this.notas;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
