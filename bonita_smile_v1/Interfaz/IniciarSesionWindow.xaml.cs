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
            Seguridad secure = new Seguridad();
            //user.redireccionarLogin();
            //Rol r = new Rol();
            //r.eliminarRol(5);
            user.redireccionarLogin(txtUsuario.Text, pbPassword.Password);
           /*Sincronizar s = new Sincronizar();
            s.Backup();
            bool verdad = s.borrar_bd();
            if (verdad)
            {
                MessageBox.Show("Se borro la bd");
                bool verdad2 = s.crear_bd();
                if(verdad2)
                {
                    MessageBox.Show("se creo la bd");
                    s.Restore();
                }
                else
                {
                    MessageBox.Show("No se pudo crear bd ");
                }
            }
            else
            {
                MessageBox.Show("No se pudo borrar");
            }*/

            
        }
    }
}
