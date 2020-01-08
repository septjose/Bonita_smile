using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Pagina_Agregar_Estudios.xaml
    /// </summary>
    public partial class Pagina_Agregar_Estudios : Page
    {
        string ruta = "";
        public Pagina_Agregar_Estudios()
        {
            InitializeComponent();

        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            lista.Items.Clear();
        }*/

        //Tiene lugar cuando se completa una operación de arrastrar y colocar en el destino. 
        private void lista_Drop(object sender, DragEventArgs e)
        {
            //Recuperamos la lista de los elementos arrastrados y y los añadimos a la lista
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                lista.Items.Add(s[i]);
        }

        private void lista_DragEnter(object sender, DragEventArgs e)
        {
            //Comprobamos el tipo de dato que se arrastra
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;
        }

        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lista.SelectedItem != null)
            {
                ruta = lista.SelectedItem.ToString();
                Imagen(ruta);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //int div = 100 / lista.Items.Count;
            //int res = 0;
            // MessageBox.Show(div.ToString());
            string ruta_foto = @"";
            lblCantidad.Content = "/ " + lista.Items.Count;
            for (int i = 0; i < lista.Items.Count; i++)
            {
                ruta_foto = lista.Items[i].ToString();

                bool inserto = false;
                for (int j = 0; j <= 100; j++)
                {
                    lblNombre.Content = "Subiendo al servidor : " + ruta_foto;
                    //lblNombre.Content = "Subiendo al servidor : " + lista.Items[i].ToString();

                    //pb_imagen.Value = j;
                    lblcontador.Content = i;
                }
                inserto = SubirFicheroStockFTP("imagen_" + i + ".jpg", ruta_foto);
                if (inserto)
                {
                    lblcontador.Content = i;
                    lblNombre.Content = "Subiendo al servidor : " + ruta_foto;
                    MessageBox.Show("SE VA AINSERTAR EN LA BASE DE DATOS " + ruta_foto);



                }

            }
            lblcontador.Content = lista.Items.Count;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*if (lista.SelectedItem != null)
            {

                lista.Items.RemoveAt(lista.Items.IndexOf(ruta));
                rt_imagen.Fill = null;
            }
            else
            {
                MessageBox.Show("no selecciono ningun registro ");
            }*/


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
        
    }
}
