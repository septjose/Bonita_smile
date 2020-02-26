using bonita_smile_v1.Interfaz.Administrador;
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

namespace bonita_smile_v1.Interfaz.Socio
{
    /// <summary>
    /// Lógica de interacción para Page4.xaml
    /// </summary>
    public partial class Socio_usuarios : Page
    {
        ObservableCollection<UsuarioModel> Gusuario;
        System.Windows.Controls.ListView lv_aux;
        bool bandera_online_offline = false;
        List<string> lista = new List<string>();
        string alias = "";
        public Socio_usuarios(List<string> lista, string alias)
        {
            this.lista = lista;
            this.alias = alias;
            InitializeComponent();
            llenar_list_view(alias);
        }

        void llenar_list_view(string alias )
        {
            //Usuarios user = new Usuarios();
            //List<UsuarioModel> items = new List<UsuarioModel>();
            //items=user.MostrarUsuario();

            /*foreach(UsuarioModel usu in items)
            {
                MessageBox.Show(usu.alias + "  ");
            }*/

            //ObservableCollection<UsuarioModel> Gusuario;
            var usuarios = new ObservableCollection<UsuarioModel>((new Usuarios(bandera_online_offline).MostrarUsuario_Socio(alias)));

            lv_Users.ItemsSource = usuarios;
            lv_aux = lv_Users;
            Gusuario = usuarios;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsuarioModel usuario = (UsuarioModel)lv_Users.SelectedItem;
            if (lv_Users.SelectedItems.Count > 0)
            {
                string id_usuario = usuario.id_usuario;
                string alias = usuario.alias;
                Test_Internet ti = new Test_Internet();
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar al usuario :" + alias + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Usuarios user = new Usuarios(bandera_online_offline);

                    bool elimino = user.eliminarUsuario(id_usuario,alias);
                    if (elimino)
                    {
                        //user = new Usuarios(!bandera_online_offline);
                        //user.eliminarUsuario(id_usuario);
                        Gusuario.Remove((UsuarioModel)lv_Users.SelectedItem);
                        System.Windows.Forms.MessageBox.Show("Se elimino el usuario correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            UsuarioModel usuario = (UsuarioModel)lv_Users.SelectedItem;
            if (lv_Users.SelectedItems.Count > 0)
            {
                //int id_usuario = usuario.id_usuario;
                string alias = usuario.alias;
                //Actualizar_Usuario au = new Actualizar_Usuario(usuario, lv_aux);
                //au.ShowDialog();
                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                if (socio != null)
                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                    socio.Main4.Content = new Actualizar_usuario_socio(usuario,this.lista,this.alias);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
            if (socio != null)
                socio.Main4.Content = new Ingresar_usuario_socio(this.lista,this.alias);
        }
    }
}
