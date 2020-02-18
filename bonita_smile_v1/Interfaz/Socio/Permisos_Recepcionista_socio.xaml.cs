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
    public partial class Permisos_Recepcionista_socio : Page
    {
        ObservableCollection<PermisosModel> GuPermisos;
        System.Windows.Controls.ListView lv_aux;
        bool bandera_online_offline = false;
        List<string> lista = new List<string>();
        string alias = "";
        int id_rol = 0;
        public Permisos_Recepcionista_socio (List<string>lista,string alias,int id_rol)
        {
            InitializeComponent();
            this.lista = lista;
            this.alias = alias;
            this.id_rol = id_rol;
            
                this.btn_ingresar.IsEnabled = false;
                this.btn_ingresar.Visibility = System.Windows.Visibility.Collapsed;
           
            llenar_list_view(alias);
        }

        void llenar_list_view(string lista)
        {
            //Usuarios user = new Usuarios();
            //List<UsuarioModel> items = new List<UsuarioModel>();
            //items=user.MostrarUsuario();

            /*foreach(UsuarioModel usu in items)
            {
                MessageBox.Show(usu.alias + "  ");
            }*/

            //ObservableCollection<UsuarioModel> Gusuario;
            var permisos = new ObservableCollection<PermisosModel>((new Clinicas(bandera_online_offline).Mostrar_Permisos_socio(id_rol,alias)));

            lv_Users.ItemsSource = permisos;
            lv_aux = lv_Users;
            GuPermisos = permisos;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PermisosModel permiso = (PermisosModel)lv_Users.SelectedItem;
            if (lv_Users.SelectedItems.Count > 0)
            {

                Test_Internet ti = new Test_Internet();
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  permiso :" + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Clinicas c = new Clinicas(bandera_online_offline);
                    bool existe = c.Verificar_Tabla_Permisos(permiso.id_usuario);
                    if (!existe)
                    {
                        System.Windows.Forms.MessageBox.Show("Este usuario no tiene permisos, si lo desea eliminar vaya al apartado de usuarios ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Clinicas cli = new Clinicas(bandera_online_offline);

                        bool elimino = cli.eliminar_Permiso(permiso.id_usuario, permiso.id_clinica);
                        if (elimino)
                        {
                            //cli = new Clinicas(!bandera_online_offline);
                            //cli.eliminar_Permiso(permiso.id_usuario, permiso.id_clinica);
                            permiso.nombre_sucursal = "";
                            System.Windows.Forms.MessageBox.Show("Se elimino el permiso correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GuPermisos.Remove((PermisosModel)lv_Users.SelectedItem);
                            GuPermisos.Add(permiso);
                        }
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

            PermisosModel permiso = (PermisosModel)lv_Users.SelectedItem;
            if (lv_Users.SelectedItems.Count > 0)
            {

                Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                if (socio != null)
                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                    socio.Main4.Content = new Actualizar_Permiso_recepcionista_socio(this.id_rol,permiso.alias, permiso.nombre_sucursal,this.alias,this.lista,permiso.id_clinica);
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
                socio.Main4.Content = new Ingresar_Permisos_Recepcionista_socio(this.alias,this.lista,this.id_rol);
        }
    }
}
