using System;
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
using bonita_smile_v1.Servicios;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Interfaz.Administrador;
using System.Windows.Forms;
using bonita_smile_v1.Interfaz.Recepcionista;
using bonita_smile_v1.Interfaz.Socio;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Page7_Ingresar.xaml
    /// </summary>
    public partial class Page7_Ingresar : Page
    {
        PacienteModel paciente;
        bool bandera_offline_online = false;
        List<string> lista = new List<string>();
        string alias = "";
        public Page7_Ingresar(PacienteModel paciente,List<string> lista,string alias)
        {
            InitializeComponent();
            this.paciente = paciente;
            this.lista = lista;
            this.alias = alias;
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            string descripcion = txtAntecedentes.Text;           
            paciente.antecedente = descripcion;
            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
            Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
            {
                admin.Main.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                admin.Main.Content = new Page8_IngresarFoto(paciente, this.lista, this.alias);
            }
            else 
                if(recep!=null)
            {
                recep.Main3.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                recep.Main3.Content = new Page8_IngresarFoto(paciente, this.lista, this.alias);
            }
            else
                if (socio != null)
            {
                socio.Main4.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                socio.Main4.Content = new Page8_IngresarFoto(paciente,this.lista,this.alias);
            }

        }

        private void btnOmitir_Click(object sender, RoutedEventArgs e)
        {
            Paciente pa = new Paciente(bandera_offline_online);
            bool inserto = pa.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
            if (inserto)
            {
                //pa = new Paciente(!bandera_offline_online);
                // pa.insertarPaciente(this.paciente.nombre, this.paciente.apellidos, this.paciente.direccion, this.paciente.telefono, this.paciente.foto, txtAntecedentes.Text, this.paciente.email, 0, this.paciente.clinica.id_clinica);
                Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                if (admin != null)
                {
                    admin.Main.Content = new Page6();
                    System.Windows.Forms.MessageBox.Show("Se Ingreso  el Paciente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    if(recep!=null)
                {
                    recep.Main3.Content = new Pacientes_Recepcionista(this.paciente.clinica.id_clinica);
                    System.Windows.Forms.MessageBox.Show("Se Ingreso  el Paciente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    if (socio != null)
                {
                    socio.Main4.Content = new Pacientes_socio(this.lista,this.alias);
                    System.Windows.Forms.MessageBox.Show("Se Ingreso  el Paciente", "Se Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No se pudo  Ingresar el Paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
