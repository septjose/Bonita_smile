using bonita_smile_v1.Interfaz.Administrador;
using bonita_smile_v1.Interfaz.Clinica;
using bonita_smile_v1.Interfaz.Socio;
using bonita_smile_v1.Modelos;
using bonita_smile_v1.Servicios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonita_smile_v1.Interfaz;
using System.Windows.Forms;
using bonita_smile_v1.Interfaz.Recepcionista;

namespace bonita_smile_v1
{
    public partial class InsertarMembresia : Form
    {
        PacienteModel paciente = new PacienteModel();
        bool bandera_online_offline = false;
        private MySqlDataReader reader = null, reader2 = null;
        private string query, query2;
        private MySqlConnection conexionBD, conexionBD2;
        Conexion obj = new Conexion();
        CultureInfo culture = new CultureInfo("en-US");
        Configuracion_Model configuracion;
        string ruta = Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        string alias;
        public InsertarMembresia(PacienteModel paciente,string alias)
        {
            this.conexionBD = obj.conexion(bandera_online_offline);
            this.paciente = paciente;
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta);
            this.configuracion = configuracion;
            this.alias = alias;
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string id_membresia="";
            if (!txtPrecio.Text.Equals("") && !txt_efectivo.Text.Equals(""))
            {
                if (new Seguridad().validar_numero(txtPrecio.Text) && new Seguridad().validar_numero(txt_efectivo.Text))
                {
                    double abono = Convert.ToDouble(txtPrecio.Text, culture);
                    double efectivo = Convert.ToDouble(txt_efectivo.Text, culture);
                    if ( efectivo > 0)
                    {
                        try
                        {

                            //MessageBox.Show(paciente.nombre);
                            bool inserto = new Membresia(bandera_online_offline).InsertarMembresia(paciente.id_paciente, DateTime.Now.ToString("yyyy/MM/dd"),paciente.clinica.id_clinica,abono.ToString(culture),alias);
                            if (inserto)
                            {
                                List<MembresiaModel> lista = new Membresia(bandera_online_offline).MostrarMembresias(paciente.id_paciente, paciente.clinica.id_clinica);
                                foreach(var membresia in lista)
                                {
                                    id_membresia=membresia.id_membresia;
                                }

                                bool inserto2 = new Abonos_Membresia(bandera_online_offline).InsertarAbonoMembresia(DateTime.Now.ToString("yyyy/MM/dd"), efectivo.ToString(culture), "Primer pago para tener la membresia", id_membresia, paciente.id_paciente, paciente.clinica.id_clinica, alias);
                                if(inserto2)
                                {
                                    System.Windows.Forms.MessageBox.Show("El paciente " + paciente.nombre + " " + paciente.apellidos + " es ahora miembro", "Se ingreso correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    imprimir_recibo();

                                    Soc socio = System.Windows.Application.Current.Windows.OfType<Soc>().FirstOrDefault();
                                    Admin admin = System.Windows.Application.Current.Windows.OfType<Admin>().FirstOrDefault();
                                    Recep recep = System.Windows.Application.Current.Windows.OfType<Recep>().FirstOrDefault();

                                    if (admin != null)
                                    {
                                        MessageBox.Show(abono.ToString());
                                        admin.Main.Content = new Abonos_Mem(paciente,id_membresia, abono, alias);
                                    }
                                    else
                                    if (recep != null)
                                    {
                                        recep.Main3.Content = new Abonos_Mem(paciente, id_membresia, abono, alias);
                                    }
                                    else
                                    if (socio != null)
                                    {
                                        socio.Main4.Content = new Abonos_Mem(paciente, id_membresia, abono, alias);
                                    }
                                }
                                

                                // -----------------------------------------------/
                                //inserto = new Paciente(!bandera_online_offline).actualizarMembresia(paciente);

                                
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("No se pudo  Ingresar la membresia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex + "");
                            this.DialogResult = DialogResult.OK;
                        }
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Abono mayor que efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Cantidad no valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("LLene todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void imprimir_recibo()
        {
            
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += new PrintPageEventHandler(Imprimir);

            //printDocument1.PrinterSettings.PrinterName = "HPFEF3CF (HP Officejet Pro 6830) (Red)";
            printDocument1.PrinterSettings.PrinterName = configuracion.ftp.nombre_impresora;

            //printDocument1.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("210 x 297 mm", 196, 822);
            //printDocument1.DefaultPageSettings.PaperSize = new PaperSize("210 x 297 mm", 228, 822);
            //poner try catch
            if (printDocument1.PrinterSettings.IsValid)
            {
                printDocument1.Print();
            }
            else
            {
                System.Windows.MessageBox.Show("Impresora no válida.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            MessageBox.Show(paciente.clinica.id_clinica);
            string sucursal = obtener_nombre_sucursal(paciente.clinica.id_clinica);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            System.Drawing.Font font = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font titulo = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font cuerpo = new System.Drawing.Font("Courier New", 9);

            //MODIFICADORES DE FORMATO DE HOJA/
             float margen_izquierdo = 0;
            float margen_superior = 10;
            double margen_cuerpo = 0.42; //DESPUES DEL TITULO
            double tamanio_hoja_horizontal = 3.8;
            // ----------------------------------/
            double abono = Convert.ToDouble(txtPrecio.Text, culture);
            double efectivo = Convert.ToDouble(txt_efectivo.Text, culture);
            double cambio = efectivo- abono;
            //double restante = this.restante - (Int32.Parse(this.txtAbono.Text));

            string fecha_inicio = DateTime.Now.ToString("d/M/yyyy");
            string fecha_finalizacion = DateTime.Now.AddYears(1).ToString("d/M/yyyy");
            string hora = DateTime.Now.ToString("HH:mm:ss") + " hrs";
            Abonos a = new Abonos(bandera_online_offline);


            System.Drawing.Image imagen = System.Drawing.Image.FromFile(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\bs_ticket_imagen.bmp"));
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(margen_izquierdo, margen_superior, centimetroAPixel(4), 30);//tamanio_hoja_horizontal en vez de 4
            RectangleF rImage = new RectangleF(38, margen_superior, 110, 110);

            e.Graphics.DrawImage(imagen, rImage);

            //e.Graphics.FillRectangle(Brushes.Red, rect); usarlo para cada recyangulo
            rect.Y = (cuerpo.GetHeight(e.Graphics) * 7) + margen_superior;
            e.Graphics.DrawString("BONITA SMILE", titulo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.X = Convert.ToSingle(centimetroAPixel(margen_cuerpo));
            rect.Width = centimetroAPixel(tamanio_hoja_horizontal); //nuevo

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 9) + margen_superior;
            stringFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("SUCURSAL: " + sucursal, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 10) + margen_superior;
            e.Graphics.DrawString("CLIENTE: " + paciente.nombre + " " + paciente.apellidos, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 12) + margen_superior;
            e.Graphics.DrawString("FECHA: " + fecha_inicio, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 13) + margen_superior;
            e.Graphics.DrawString("HORA: " + hora, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 15) + margen_superior;
            e.Graphics.DrawString("MOTIVO: ", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 17) + margen_superior;
            e.Graphics.DrawString("Membresía", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 18) + margen_superior;
            e.Graphics.DrawString("PRECIO: $" + txtPrecio.Text.ToString(culture), cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 19) + margen_superior + 10;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);
            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 19) + margen_superior + 10) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 21) + margen_superior - 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 21) + margen_superior - 5) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 23) + margen_superior);
            e.Graphics.DrawString("TOTAL: $" + txtPrecio.Text.ToString(culture), cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            //rect.Y = (cuerpo.GetHeight(e.Graphics) * 15) + margen_superior;
            //e.Graphics.DrawString("ABONO: $" + txtAbono.Text, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 24) + margen_superior;
            e.Graphics.DrawString("RECIBIDO: $" + txt_efectivo.Text.ToString(culture), cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 25) + margen_superior;
            e.Graphics.DrawString("CAMBIO: $" + cambio, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 27) + margen_superior - 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);
            rect.Y = ((cuerpo.GetHeight(e.Graphics) * 27) + margen_superior - 5) + 5;
            e.Graphics.DrawString("-------------------", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 29) + margen_superior;
            e.Graphics.DrawString("PERIODO MEMBRESÍA:", cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 31) + margen_superior;
            e.Graphics.DrawString("INICIO: " + fecha_inicio, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            rect.Y = (cuerpo.GetHeight(e.Graphics) * 32) + margen_superior;
            e.Graphics.DrawString("TERMINO: " + fecha_finalizacion, cuerpo, new SolidBrush(Color.Black), rect, stringFormat);

            e.HasMorePages = false;
        }

        float centimetroAPixel(double Centimeter)
        {
            double pixel = -1;
            using (Graphics g = this.CreateGraphics())
            {
                pixel = Centimeter * g.DpiY / 2.54d;
            }
            return (int)pixel;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public string obtener_nombre_sucursal(string descripcion)
        {
            string id = "";
            query = "SELECT nombre_sucursal FROM clinica where id_clinica='" + descripcion + "'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // ColoresModel coloresModel = new ColoresModel();

                    //coloresModel.id_color = int.Parse(reader[0].ToString());
                    //coloresModel.descripcion = reader[1].ToString();

                    id = reader[0].ToString();
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return "";
            }
            conexionBD.Close();

            return id;
        }
    }
}