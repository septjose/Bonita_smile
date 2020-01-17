using bonita_smile_v1.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
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
using bonita_smile_v1.Servicios;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Fotos_de_Estudios.xaml
    /// </summary>
    public partial class Fotos_de_Estudios : Page
    {
        //ObservableCollection<Imagen> GPaciente;
        string id_carpeta = "";
        string id_paciente = "";
        string ruta = "";

        void llenar_list_view(string id_carpeta, string id_paciente)
        {
            Fotos_estudio_carpeta f_estudio = new Fotos_estudio_carpeta();
            List<Fotos_estudio_carpetaModel> lista = f_estudio.MostrarFoto_estudio_carpeta(id_carpeta, id_paciente);

            var fotografos = new ObservableCollection<Fotos_estudio_carpetaModel>(lista);
            for (int i = 0; i < lista.Count; i++)
            {
                //lb_imagen.Items.Add(lista[i].foto);
                // MessageBox.Show("Lista es foto"+lista[i].foto);
                lb_imagen.Items.Add(fotografos[i]);
            }

            //lb_imagen.ItemsSource = lista;

        }
        public Fotos_de_Estudios(Carpeta_archivosModel carpeta)
        {
            Fotos_estudio_carpeta f_e_c = new Fotos_estudio_carpeta();
            f_e_c.fotos(carpeta.id_carpeta, carpeta.id_paciente);

            InitializeComponent();

            llenar_list_view(carpeta.id_carpeta, carpeta.id_paciente);
            id_carpeta = carpeta.id_carpeta;
            id_paciente = carpeta.id_paciente;
            //llenar_list_view2();
        }

        /* void llenar_list_view2()
         {
             List<Imagen> lista = new List<Imagen>();
             Imagen img1 = new Imagen();
             img1.foto = @"C:\bs\Estudios_Brackets_imagen_0.jpg";
             img1.imagen = LoadImage(@"C:\bs\Estudios_Brackets_imagen_0.jpg");
             Imagen img2 = new Imagen();
             img2.foto = @"C:\bs\Estudios_Brackets_imagen_1.jpg";
             img2.imagen = LoadImage(@"C:\bs\Estudios_Brackets_imagen_1.jpg");
             Imagen img3 = new Imagen();
             img3.foto = @"C:\bs\Estudios_Brackets_imagen_2.jpg";
             img3.imagen = LoadImage(@"C:\bs\Estudios_Brackets_imagen_2.jpg");
             lista.Add(img1);
             lista.Add(img2);
             lista.Add(img3);
             var pacientes = new ObservableCollection<Imagen>(lista);
             for (int i = 0; i < lista.Count; i++)
             {
                 //lb_imagen.Items.Add(lista[i].foto);
                 lb_imagen.Items.Add(pacientes[i]);
             }
             //lb_imagen.ItemsSource = lista;
             //GPaciente = pacientes;
         }*/
        private void lb_imagen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_imagen.SelectedItem != null)
            {

                Fotos_estudio_carpetaModel selectedOffer = (lb_imagen.SelectedItem as Fotos_estudio_carpetaModel);
                if (selectedOffer != null)
                {
                    //MessageBox.Show(selectedOffer.foto);
                    Imagen(@"C:\bs\" + selectedOffer.foto);
                    //DialogResult resultado = new DialogResult();
                    this.ruta = @"C:\bs\" + selectedOffer.foto;
                   // Form mensaje = new Form1(@"C:\bs\" + selectedOffer.foto);
                    //resultado = mensaje.ShowDialog();
                }
                //ruta = lista.SelectedItem.ToString();
                //Imagen(ruta);


            }
        }

        public void Imagen(string ruta)
        {
            //string ruta2 = @"C:\bs\img1.jpg";
            //if (File.Exists(ruta))

            Image image = new Image();
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new System.Uri(ruta);
            bi.EndInit();
            image.Source = bi;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = bi;
            //return ib;
            rt_imagen.Fill = ib;

        }

        private BitmapImage LoadImage(string filename)
        {
            BitmapImage bi;

            if (File.Exists(filename))
            {
                bi = new BitmapImage(new Uri(filename));
            }
            else
            {
                bi = new BitmapImage(new Uri(@"C:\bs\img1.jpg"));
            }
            return bi;
        }

        private void lb_imagen_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            //Comprobamos el tipo de dato que se arrastra
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
                e.Effects = System.Windows.DragDropEffects.All;
            else
                e.Effects = System.Windows.DragDropEffects.None;
        }

        private void lb_imagen_Drop(object sender, System.Windows.DragEventArgs e)
        {
            bool inserto = false;
            string result = "";
            Test_Internet ti = new Test_Internet();

            string extension = "";
            Fotos_estudio_carpetaModel f = new Fotos_estudio_carpetaModel();
            //Recuperamos la lista de los elementos arrastrados y y los añadimos a la lista
            string[] s = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);
            int i;
            System.Windows.MessageBox.Show("valor de s es " + s.Length);
            
            for (i = 0; i < s.Length; i++)
            {
                System.Windows.MessageBox.Show("el valor de s[i] es " + s[i]);
                extension = System.IO.Path.GetExtension(s[i]);
                if(extension.Equals(".png")||extension.Equals(".jpg"))
                {
                    System.Windows.MessageBox.Show("Es imagen "+s[i]);
                    f.foto = s[i];
                    f.imagen = LoadImage(s[i]);


                    lb_imagen.Items.Add(f);
                    result = System.IO.Path.GetFileName(s[i]);
                    if(ti.Test())
                    {
                        System.Windows.MessageBox.Show("Hay Internet");
                        inserto = SubirFicheroStockFTP(id_carpeta + "_" + result, s[i]);
                        if (inserto)
                        {
                            System.Windows.MessageBox.Show("se subio al servidor");
                            Fotos_estudio_carpeta fotos = new Fotos_estudio_carpeta();
                            bool verdad = fotos.insertarFoto_estudio_carpeta(id_carpeta, id_paciente, id_carpeta + "_" + result);
                            if (verdad)
                            {
                                System.Windows.MessageBox.Show("se subio a la bd");
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("no se subio a la bd");
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("No se subio");
                        }
                    }
                    else
                    {

                    }
                    
                }
                else
                {
                    System.Windows.MessageBox.Show("No es imagen "+s[i]);
                }

            }

        }

        public bool SubirFicheroStockFTP(string foto, string ruta)
        {
            bool verdad;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                        "ftp://jjdeveloperswdm.com" +
                        "/" +
                       foto);

                // string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                //      ruta,
                //       foto);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(
                        "bonita_smile@jjdeveloperswdm.com",
                        "bonita_smile");

                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var fileStream = File.OpenRead(ruta))
                {
                    using (var requestStream = request.GetRequestStream())
                    {
                        fileStream.CopyTo(requestStream);
                        requestStream.Close();
                    }
                }

                var response = (FtpWebResponse)request.GetResponse();

                response.Close();
                verdad = true;
            }
            catch (Exception ex)
            {
                //logger.Error("Error " + ex.Message + " " + ex.StackTrace);
                verdad = false;
                System.Windows.MessageBox.Show("Error " + ex.Message + " " + ex.StackTrace);


            }
            return verdad;

        }

        private void lb_imagen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult resultado = new DialogResult();
            Form mensaje = new Visualizador_Imagenes(this.ruta);
            resultado = mensaje.ShowDialog();
        }
    }
}