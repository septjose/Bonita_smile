using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using bonita_smile_v1.Modelos;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Configuration;
using System.Configuration;
using System.Net.Mail;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Pagina_Correo.xaml
    /// </summary>
    public partial class Pagina_Correo : Page
    {
        private string correoApp { get; set; }
        public Pagina_Correo(PacienteModel paciente)
        {
            InitializeComponent();
            SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            correoApp = smtp.From;
            txtcorreo_origen.Text = correoApp;
            txtcorreo_destino.Text = paciente.email;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var smtp = new SmtpClient();
            EmailSender emailSender = new EmailSender(smtp, correoApp);
            emailSender.SendEmailAsync(txtcorreo_destino.Text, txtAsunto.Text, txtMensaje.Text);
            MessageBox.Show("Correo Enviado correctamente");
            LimpiarFormulario();
        }
        private void LimpiarFormulario()
        {
            txtcorreo_origen.Text = string.Empty;
            txtcorreo_destino.Text = string.Empty;
            txtMensaje.Text = string.Empty;
            txtAsunto.Text = string.Empty;
        }

    }
}
