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
using System.Security.Cryptography;
using System.Net;
using System.Windows.Forms;

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
            if(txtUsuario.Text.Equals("")||pbPassword.Password.Equals(""))
            {
                System.Windows.Forms.MessageBox.Show("Le faltan campos por llenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                user.redireccionarLogin(txtUsuario.Text, pbPassword.Password);
            }

            

            //new Sincronizar().SincronizarLocalServidor() ;

            /*Uri siteUri = new Uri("ftp://jjdeveloperswdm.com/imagen_a_.jpg");
            bool verdad= DeleteFileOnServer(siteUri, "bonita_smile@jjdeveloperswdm.com", "bonita_smile");

             if (verdad)
            {
                MessageBox.Show("si");
            }
            else
            {
                MessageBox.Show("nno");
            }
            //user.redireccionarLogin();
            //Rol r = new Rol();
            //r.eliminarRol(5);

            //string r1 = SHA1("n,vnak.nv.al.v.vnl.SML.VJ,CMA-klnmdxlk,gnvIKLneMDLK,JFMOPLÑj,endjgknvMKLE,NDIGKLVNoioekldjkgmoibqkfjhoibrkfjiksjffmigkjviskdjdgivsnmhkakjzlfjanjznjndkajnvikdhdofilhankjvhnajznvkn<dj");
            //string r2 = SHA1("n,vnak.nv.al.v.vnl.SML.VJ,CMA-klnmdxlk,gnvIKLneMDLK,JFMOPLÑj,endjgknvMKLE,NDIGKLVNoioekldjkgmoibqkfjhoibrkfjiksjffmigkjviskdjdgivsn");
            //MessageBox.Show("Muestro r1 su longitud es de "+r1.Length+"    su incriptacion es "+r1);
            //MessageBox.Show("Muestro r2 su longitud es de " + r2.Length + "    su incriptacion es " + r2);
            /*Escribir_Archivo ea = new Escribir_Archivo();
            ea.corregirArchivo();*/

            /*Sincronizar sincronizar = new Sincronizar();
            sincronizar.SincronizarLocalServidor();*/

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

