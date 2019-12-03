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
        public Page2_Abonos(PacienteModel paciente, Motivo_citaModel motivo)
        {


            InitializeComponent();
            this.paciente = paciente;
            this.motivo = motivo;
            lblNombre.Content = paciente.nombre + " " + paciente.apellidos;
            lblmotivo.Content = motivo.descripcion;
            lblTotal.Content = motivo.costo.ToString();
            Abonos abono = new Abonos();
            lblAbonado.Content = abono.Abonados(motivo.id_motivo).ToString();
            lblRestante.Content = abono.Restante(motivo.id_motivo).ToString();
            System.Windows.MessageBox.Show(motivo.id_motivo.ToString() + "  " + paciente.id_paciente.ToString());
            llenar_list_view(motivo.id_motivo, paciente.id_paciente);

        }
        void llenar_list_view(int id_motivo, int id_paciente)
        {
            var notas = new ObservableCollection<AbonosModel>(new Servicios.Abonos().MostrarAbonos(id_motivo, id_paciente));

            lvNotas.ItemsSource = notas;
            GAbono = notas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new MessageBoxAbono(motivo.id_motivo, paciente.id_paciente);
            resultado = mensaje.ShowDialog();
        }
    }
}
