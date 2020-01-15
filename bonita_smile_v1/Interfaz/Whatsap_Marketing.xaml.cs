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
using RestSharp;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Pagina_Whatssap.xaml
    /// </summary>
    public partial class Pagina_Whatssap : Page
    {
       
        public Pagina_Whatssap(PacienteModel paciente)
        {
            InitializeComponent();
             string telefono = "+521" + paciente.telefono;
            txtTelefono.Text = telefono;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string telefono = txtTelefono.Text;
            string mensaje = txtMensaje.Text;
            var sms = SendMessage(telefono, mensaje);
            MessageBox.Show(sms);
        }

        public string SendMessage(string telefono, string mensaje)
        {
            var client = new RestClient("https://whatsmsapi.com/api/send_sms");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("x-api-key", "5df051025363d");
            //request.AddParameter("undefined", "phone=+5214641124092&text=Hello%20World!", ParameterType.RequestBody);
            request.AddParameter("phone", telefono, ParameterType.GetOrPost);
            request.AddParameter("text", mensaje, ParameterType.GetOrPost);
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}
