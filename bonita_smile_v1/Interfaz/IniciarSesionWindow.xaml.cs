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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using bonita_smile_v1.Offline;


namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("txtx  :" +txtUsuario.Text+"    "+ "pass :"+pbPassword.Password);
            Usuarios user = new Usuarios();
            Usuarios_Offline u_o = new Usuarios_Offline();
            Seguridad secure = new Seguridad();
            //user.redireccionarLogin();
            //Rol r = new Rol();
            //r.eliminarRol(5);
            Test_Internet ti = new Test_Internet();
            bool verdad = ti.Test();
            if(verdad)
            {
                user.redireccionarLogin(txtUsuario.Text, pbPassword.Password);
            }
            else
            {
                MessageBox.Show("Offline");
                u_o.redireccionarLogin(txtUsuario.Text, pbPassword.Password);
                

            }
            
            
        }
    }
}
