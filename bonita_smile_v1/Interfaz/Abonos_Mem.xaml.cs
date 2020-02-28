using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace bonita_smile_v1.Interfaz
{
    /// <summary>
    /// Lógica de interacción para Page2_notas.xaml
    /// </summary>
    public partial class Abonos_Mem : Page
    {
        ObservableCollection<abonos_membresiaModel> GAbono;
        PacienteModel paciente;
        Motivo_citaModel motivo;
        double restante = 0.0;
        double abonado = 0.0;
        double total = 0.0;
        bool bandera_online_offline = false;
        ObservableCollection<abonos_membresiaModel> notas;
        CultureInfo culture = new CultureInfo("en-US");
        NumberFormatInfo nfi = new CultureInfo("en-US", true).NumberFormat;
        string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        string alias;
        string id_membresia;
        public Abonos_Mem(PacienteModel paciente,string id_membresia,double totales,string alias)
        {
            //System.Windows.MessageBox.Show("imprimo el abono :" + totales);
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);

            InitializeComponent();
            rt_imagen.Fill = new Page2().Imagen(@configuracion.carpetas.ruta_imagenes_carpeta + "\\" + paciente.foto);
           
            //lblNombre.Content = paciente.nombre + " " + paciente.apellidos;
            //lblmotivo.Content = motivo.descripcion;
            //lblTotal.Content = motivo.costo.ToString();
            Abonos_Membresia abono = new Abonos_Membresia(bandera_online_offline);
            //lblAbonado.Content = abono.Abonados(motivo.id_motivo).ToString();
            //lblRestante.Content = abono.Restante(motivo.id_motivo).ToString();
            //System.Windows.MessageBox.Show(motivo.id_motivo.ToString() + "  " + paciente.id_paciente.ToString());
            txtNombre.Text = paciente.nombre + " " + paciente.apellidos;
            txtNombre.IsEnabled = false;
            txtMotivo.Text = "Pago de membresia";
            txtMotivo.IsEnabled = false;
            txtTotal.IsEnabled = false;
            txtAbonado.IsEnabled = false;
            txtRestante.IsEnabled = false;
            txtTotal.Text = "$" + Convert.ToDouble(totales, culture).ToString("n", nfi);//Convert.ToDouble(totales, culture).ToString("n", nfi);
            total = Convert.ToDouble(totales.ToString(), culture);
            txtAbonado.Text = "$" + Convert.ToDouble(abono.Abonados(id_membresia,paciente.id_paciente,paciente.clinica.id_clinica), culture).ToString("n", nfi);
            txtRestante.Text = "$" + Convert.ToDouble(abono.Restante(id_membresia), culture).ToString("n", nfi);
            restante = Convert.ToDouble(abono.Restante(id_membresia).ToString(), culture);
            abonado = Convert.ToDouble(abono.Abonados(id_membresia,paciente.id_paciente,paciente.clinica.id_clinica).ToString(), culture);
            llenar_list_view(id_membresia,paciente.id_paciente,paciente.clinica.id_clinica);
            this.alias = alias;
            this.paciente = paciente;
            this.id_membresia = id_membresia;


        }
        void llenar_list_view(string id_membresia,string id_paciente,string id_clinica)
        {
            this.notas = new ObservableCollection<abonos_membresiaModel>(new Servicios.Abonos_Membresia(bandera_online_offline).MostrarAbonosMembresia(id_membresia,id_paciente,id_clinica));

            lvNotas.ItemsSource = notas;
            GAbono = notas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Abonos_Membresia abonos = new Abonos_Membresia(bandera_online_offline);
            double restanten = Convert.ToDouble(abonos.Restante(id_membresia).ToString(), culture);
            DialogResult resultado = new DialogResult();
            Form mensaje = new Ingresar_Abono_Membresia(paciente,id_membresia,total,restanten,abonado,alias);
            resultado = mensaje.ShowDialog();
            this.notas = new ObservableCollection<abonos_membresiaModel>(new Servicios.Abonos_Membresia(bandera_online_offline).MostrarAbonosMembresia(id_membresia, paciente.id_paciente, paciente.clinica.id_clinica));
            lvNotas.ItemsSource = this.notas;
            txtAbonado.Text = "$" + Convert.ToDouble(abonos.Abonados(id_membresia,paciente.id_paciente,paciente.clinica.id_clinica), culture).ToString("n", nfi);
            txtRestante.Text = "$" + Convert.ToDouble(abonos.Restante(id_membresia), culture).ToString("n", nfi);
            //Abonos abonos = new Abonos(bandera_online_offline);
            //DialogResult resultado = new DialogResult();
            //Form mensaje = new MessageBoxAbono(motivo.id_motivo, paciente.id_paciente, txtNombre.Text, txtMotivo.Text, restanten, abonado, total, paciente,alias);
            //resultado = mensaje.ShowDialog();
            //this.notas = new ObservableCollection<AbonosModel>(new Servicios.Abonos(bandera_online_offline).MostrarAbonos(motivo.id_motivo, paciente.id_paciente));
            //lvNotas.ItemsSource = this.notas;

            //txtAbonado.Text = "$" + Convert.ToDouble(abonos.Abonados(motivo.id_motivo), culture).ToString("n", nfi);
            //txtRestante.Text = "$" + Convert.ToDouble(abonos.Restante(motivo.id_motivo), culture).ToString("n", nfi);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            abonos_membresiaModel abono = (abonos_membresiaModel)lvNotas.SelectedItem;
            if (lvNotas.SelectedItems.Count > 0)
            {
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  abono :" + abono.comentario + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Abonos_Membresia abo = new Abonos_Membresia(bandera_online_offline);

                    bool elimino = abo.EliminarAbonoMembresia(abono.id_abono_membresia,abono.membresia.id_membresia,paciente.id_paciente,paciente.clinica.id_clinica, alias);
                    if (elimino)
                    {

                        //abo = new Abonos(!bandera_online_offline);
                        //abo.eliminarAbono(abono.id_abono);


                       // System.Windows.Forms.MessageBox.Show("Se elimino el abono correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.notas.Remove((abonos_membresiaModel)lvNotas.SelectedItem);

                        Abonos_Membresia abonos = new Abonos_Membresia(bandera_online_offline);
                        txtAbonado.Text = "$" + Convert.ToDouble(abonos.Abonados(id_membresia,paciente.id_paciente,paciente.clinica.id_clinica), culture).ToString("n", nfi);
                        txtRestante.Text = "$" + Convert.ToDouble(abonos.Restante(id_membresia), culture).ToString("n", nfi);
                    }
                    else
                    {
                       // System.Windows.Forms.MessageBox.Show("No se pudo eliminar el abono", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No seleccionó ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            abonos_membresiaModel abono = (abonos_membresiaModel)lvNotas.SelectedItem;
            if(lvNotas.SelectedItems.Count>0)
            {
                Abonos_Membresia abonos = new Abonos_Membresia(bandera_online_offline);
                double restanten = Convert.ToDouble(abonos.Restante(id_membresia).ToString(), culture);
                DialogResult resultado = new DialogResult();
                Form mensaje = new Actualizar_Abono_Membresia(abono,paciente,id_membresia,total,restanten,abonado,alias);
                resultado = mensaje.ShowDialog();
                this.notas = new ObservableCollection<abonos_membresiaModel>(new Servicios.Abonos_Membresia(bandera_online_offline).MostrarAbonosMembresia(id_membresia, paciente.id_paciente, paciente.clinica.id_clinica));
                lvNotas.ItemsSource = this.notas;
                txtAbonado.Text = "$" + Convert.ToDouble(abonos.Abonados(id_membresia, paciente.id_paciente, paciente.clinica.id_clinica), culture).ToString("n", nfi);
                txtRestante.Text = "$" + Convert.ToDouble(abonos.Restante(id_membresia), culture).ToString("n", nfi);

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No seleccionó ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            //AbonosModel abono = (AbonosModel)lvNotas.SelectedItem;
            //if (lvNotas.SelectedItems.Count > 0)
            //{
            //    Abonos abonos = new Abonos(bandera_online_offline);
            //    double restanten = Convert.ToDouble(abonos.Restante(motivo.id_motivo).ToString(), culture);
            //    DialogResult resultado = new DialogResult();
            //    Form mensaje = new Actualizar_Abono(motivo.id_motivo, paciente.id_paciente, txtNombre.Text, txtMotivo.Text, restanten, abonado, total, abono, paciente,alias);
            //    resultado = mensaje.ShowDialog();

            //    txtAbonado.Text = "$" + Convert.ToDouble(abonos.Abonados(motivo.id_motivo), culture).ToString("n", nfi);
            //    txtRestante.Text = "$" + Convert.ToDouble(abonos.Restante(motivo.id_motivo), culture).ToString("n", nfi);
            //    this.notas = new ObservableCollection<AbonosModel>(new Servicios.Abonos(bandera_online_offline).MostrarAbonos(motivo.id_motivo, paciente.id_paciente));
            //    lvNotas.ItemsSource = this.notas;
            //}
            //else
            //{
            //    System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar la membresia  de :" + paciente.nombre+" "+paciente.apellidos + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (confirmation == System.Windows.Forms.DialogResult.Yes)
            {
                Membresia abo = new Membresia(bandera_online_offline);

                bool elimino = abo.EliminarMembresia(id_membresia,paciente.id_paciente,paciente.clinica.id_clinica, alias);
                if (elimino)
                {
                    //abo = new Abonos(!bandera_online_offline);
                    //abo.eliminarAbono(abono.id_abono);

                   // System.Windows.Forms.MessageBox.Show("Se elimino la membresia correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("No se pudo eliminar el abono", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

        }
    }
}
