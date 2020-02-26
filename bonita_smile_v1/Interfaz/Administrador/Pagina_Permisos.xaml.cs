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

namespace bonita_smile_v1.Interfaz.Administrador
{
    /// <summary>
    /// Lógica de interacción para Page4.xaml
    /// </summary>
    public partial class Pagina_Permisos : Page
    {
        ObservableCollection<PermisosModel> GuPermisos;
        System.Windows.Controls.ListView lv_aux;
        bool bandera_online_offline = false;
        int id_rol = 0;
        string alias;
        public Pagina_Permisos(int id_rol,string alias)
        {
            System.Windows.MessageBox.Show(alias+"constructor permisos");
            this.id_rol = id_rol;
            this.alias = alias;
            InitializeComponent();
            llenar_list_view(id_rol);
            if(id_rol!=5)
            {
                this.btn_ingresar.IsEnabled = false;
                this.btn_ingresar.Visibility= System.Windows.Visibility.Collapsed;
               
            }

            
        }

        void llenar_list_view(int id_rol)
        {
            //Usuarios user = new Usuarios();
            //List<UsuarioModel> items = new List<UsuarioModel>();
            //items=user.MostrarUsuario();

            /*foreach(UsuarioModel usu in items)
            {
                MessageBox.Show(usu.alias + "  ");
            }*/

            //ObservableCollection<UsuarioModel> Gusuario;
            var permisos = new ObservableCollection<PermisosModel>((new Clinicas(bandera_online_offline).Mostrar_Permisos(id_rol)));

            lv_Users.ItemsSource = permisos;
            lv_aux = lv_Users;
            GuPermisos = permisos;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(this.alias + "presiono el boton de eliminar");
            PermisosModel permiso = (PermisosModel)lv_Users.SelectedItem;
            if (lv_Users.SelectedItems.Count > 0)
            {    
                var confirmation = System.Windows.Forms.MessageBox.Show("Esta seguro de borrar el  permiso :" + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (confirmation == System.Windows.Forms.DialogResult.Yes)
                {
                    Clinicas c = new Clinicas(bandera_online_offline);
                    bool existe = c.Verificar_Tabla_Permisos(permiso.id_usuario);
                   if(!existe)
                    {
                        System.Windows.Forms.MessageBox.Show("Este usuario no tiene permisos, si lo desea eliminar vaya al apartado de usuarios ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   else
                    {
                        Clinicas cli = new Clinicas(bandera_online_offline);

                        bool elimino = cli.eliminar_Permiso(permiso.id_usuario, permiso.id_clinica,alias);
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
            System.Windows.MessageBox.Show(this.alias + "presiono el boton actualizar");
            PermisosModel permiso = (PermisosModel)lv_Users.SelectedItem;
            if (lv_Users.SelectedItems.Count > 0)
            {
                
                Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                if (admin != null)
                    //System.Windows.MessageBox.Show("imprimo " + usuario.rol.descripcion);
                    admin.Main.Content = new Pagina_Actualizar_Permisos(this.id_rol, permiso.alias, permiso.nombre_sucursal,permiso.id_clinica,alias,permiso.id_usuario);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No selecciono ningun registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(this.alias+"presiono el boton ingresar");
            Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
            if (admin != null)
                admin.Main.Content = new Pagina_Ingresar_Permisos(this.id_rol, this.alias);
        }
    }
}
