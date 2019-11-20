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
using MySql.Data.MySqlClient;

namespace bonita_smile_v1.Interfaz.Administrador.Usuario
{
    /// <summary>
    /// Lógica de interacción para Actualizar_Usuario.xaml
    /// </summary>
    public partial class Actualizar_Usuario : MetroWindow
    {
        string alias = "";
        string nombre = "";
        string apellidos = "";
        public Actualizar_Usuario(int id_usuario)
        {
            llenar_campos(id_usuario);

            InitializeComponent();

        }

        void llenar_campos(int id_usuario)
        {
            
            Usuarios user = new Usuarios();
            List<UsuarioModel> usu = new List<UsuarioModel>();
            usu=user.Mostrar_unico_usuario(id_usuario);
            foreach(UsuarioModel usuario in usu)
            {
                alias = usuario.alias;

            }
            txtAlias.Text = "jajaja";
           
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
