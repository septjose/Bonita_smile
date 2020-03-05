using bonita_smile_v1.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace bonita_smile_v1.Servicios
{
    class Paciente
    {
        private MySqlDataReader reader = null;
        private string query;
        private MySqlConnection conexionBD;
        Conexion obj = new Conexion();
        // string ruta = @"\\DESKTOP-ED8E774\bs\";
        string ruta;
        //string ruta2= @"\\DESKTOP-ED8E774\bs_auxiliar\";
        string ruta2;
        Test_Internet ti = new Test_Internet();
        private bool online;
        string ruta_archivo = System.IO.Path.Combine(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"dentista\setup\conf\configuracion.txt");
        Configuracion_Model configuracion;
        //
        public Paciente(bool online)
        {
            Archivo_Binario ab = new Archivo_Binario();
            Configuracion_Model configuracion = ab.Cargar(ruta_archivo);
            this.conexionBD = obj.conexion(online);
            this.online = online;
            this.ruta = @configuracion.carpetas.ruta_imagenes_carpeta + "\\";
            //MessageBox.Show("jdjdjd" + ruta);
            this.configuracion = configuracion;
        }

        public List<PacienteModel> MostrarPaciente()
        {
            List<PacienteModel> listaPaciente = new List<PacienteModel>();

            query = "SELECT paciente.id_paciente,paciente.nombre,paciente.apellidos,paciente.direccion,paciente.telefono,paciente.foto,paciente.email,paciente.marketing,paciente.id_clinica,paciente.antecedentes, if ((STRCMP(date_format(membresia.membresia, '%d/%m/%Y'), '00/00/0000') = 0) or(DATEDIFF(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), now()) < 0),'',CONCAT('Adquirida: ', date_format(membresia.membresia, '%d/%m/%Y'), '\n', 'Caduca: ', date_format(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), '%d/%m/%Y'), '\n', 'Quedan: ', DATEDIFF(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), now()),' días')) as membresia,paciente.factura,clinica.id_clinica,clinica.nombre_sucursal,clinica.color FROM paciente inner join clinica on clinica.id_clinica = paciente.id_clinica left join membresia on membresia.id_paciente=paciente.id_paciente";
            //query = "SELECT paciente.id_paciente,paciente.nombre,paciente.apellidos,paciente.direccion,paciente.telefono,paciente.foto,paciente.email,paciente.marketing,paciente.id_clinica,paciente.antecedente,paciente.auxiliar_identificador, date_format(paciente.membresia, '%d/%m/%Y') as membresia,clinica.id_clinica,clinica.nombre_sucursal,clinica.color,clinica.auxiliar_identificador FROM paciente inner join clinica on clinica.id_clinica=paciente.id_clinica";
            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PacienteModel pacienteModel = new PacienteModel();
                    ClinicaModel clinicaModel = new ClinicaModel();

                    pacienteModel.id_paciente = reader[0].ToString();
                    pacienteModel.nombre = reader[1].ToString();
                    pacienteModel.apellidos = reader[2].ToString();
                    pacienteModel.direccion = reader[3].ToString();
                    pacienteModel.telefono = reader[4].ToString();
                    pacienteModel.foto = reader[5].ToString();
                    pacienteModel.imagen = LoadImage(reader[5].ToString());
                    pacienteModel.email = reader[6].ToString();
                    if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
                    //pacienteModel.marketing = reader[6].ToString();
                    pacienteModel.antecedente = reader[9].ToString();
                    if (reader[10].ToString().Equals(""))
                    {
                        pacienteModel.imagen_membresia = null;
                    }
                    else
                    {
                        pacienteModel.imagen_membresia = LoadImage_Membresia(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\trofeo.jpg"));

                    }
                    pacienteModel.membresia = reader[10].ToString();
                    if (reader[11].ToString() == "False") { pacienteModel.factura = false; } else { pacienteModel.factura = true; }
                    clinicaModel.id_clinica = reader[12].ToString();
                    clinicaModel.nombre_sucursal = reader[13].ToString();
                    clinicaModel.color = reader[14].ToString();
                    pacienteModel.clinica = clinicaModel;
                    /*saber si la membresi que devuelve es vacio o no 
                    si regresa  vacio entonces no poner la imagen del trofeo
                    de lo contrario mandar la foto en una variable para que la muestre*/
                    listaPaciente.Add(pacienteModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return listaPaciente;
        }


        public List<PacienteModel> MostrarPaciente_socio(List<string> lista)
        {
            List<PacienteModel> listaPaciente = new List<PacienteModel>();
            foreach (var id in lista)
            {
                // MessageBox.Show("el id de la clinica es " + id);
                query = "SELECT paciente.id_paciente,paciente.nombre,paciente.apellidos,paciente.direccion,paciente.telefono,paciente.foto,paciente.email,paciente.marketing,paciente.id_clinica,paciente.antecedentes, if ((STRCMP(date_format(membresia.membresia, '%d/%m/%Y'), '00/00/0000') = 0) or(DATEDIFF(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), now()) < 0),'',CONCAT('Adquirida: ', date_format(membresia.membresia, '%d/%m/%Y'), '\n', 'Caduca: ', date_format(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), '%d/%m/%Y'), '\n', 'Quedan: ', DATEDIFF(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), now()),' días')) as membresia,paciente.factura,clinica.id_clinica,clinica.nombre_sucursal,clinica.color FROM paciente inner join clinica on clinica.id_clinica = paciente.id_clinica left join membresia on membresia.id_paciente=paciente.id_paciente where clinica.id_clinica='" + id + "'";

                try
                {
                    conexionBD.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conexionBD);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PacienteModel pacienteModel = new PacienteModel();
                        ClinicaModel clinicaModel = new ClinicaModel();

                        pacienteModel.id_paciente = reader[0].ToString();
                        pacienteModel.nombre = reader[1].ToString();
                        pacienteModel.apellidos = reader[2].ToString();
                        pacienteModel.direccion = reader[3].ToString();
                        pacienteModel.telefono = reader[4].ToString();
                        pacienteModel.foto = reader[5].ToString();
                        pacienteModel.imagen = LoadImage(reader[5].ToString());
                        pacienteModel.email = reader[6].ToString();
                        if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
                        //pacienteModel.marketing = reader[6].ToString();
                        pacienteModel.antecedente = reader[9].ToString();
                        if (reader[10].ToString().Equals(""))
                        {
                            pacienteModel.imagen_membresia = null;
                        }
                        else
                        {
                            pacienteModel.imagen_membresia = LoadImage_Membresia(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\trofeo.jpg"));

                        }
                        pacienteModel.membresia = reader[10].ToString();
                        if (reader[11].ToString() == "False") { pacienteModel.factura = false; } else { pacienteModel.factura = true; }
                        clinicaModel.id_clinica = reader[12].ToString();
                        clinicaModel.nombre_sucursal = reader[13].ToString();
                        clinicaModel.color = reader[14].ToString();
                        pacienteModel.clinica = clinicaModel;
                        /*saber si la membresi que devuelve es vacio o no 
                        si regresa  vacio entonces no poner la imagen del trofeo
                        de lo contrario mandar la foto en una variable para que la muestre*/
                        listaPaciente.Add(pacienteModel);
                    }
                }
                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conexionBD.Close();
            }


            return listaPaciente;
        }

        public List<PacienteModel> MostrarPaciente_Clinica(string id)
        {
            List<PacienteModel> listaPaciente = new List<PacienteModel>();
            query = "SELECT paciente.id_paciente,paciente.nombre,paciente.apellidos,paciente.direccion,paciente.telefono,paciente.foto,paciente.email,paciente.marketing,paciente.id_clinica,paciente.antecedentes, if ((STRCMP(date_format(membresia.membresia, '%d/%m/%Y'), '00/00/0000') = 0) or(DATEDIFF(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), now()) < 0),'',CONCAT('Adquirida: ', date_format(membresia.membresia, '%d/%m/%Y'), '\n', 'Caduca: ', date_format(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), '%d/%m/%Y'), '\n', 'Quedan: ', DATEDIFF(ADDDATE(membresia.membresia, INTERVAL 1 YEAR), now()),' días')) as membresia,paciente.factura,clinica.id_clinica,clinica.nombre_sucursal,clinica.color FROM paciente inner join clinica on clinica.id_clinica = paciente.id_clinica left join membresia on membresia.id_paciente=paciente.id_paciente where clinica.id_clinica='"+id+"'";

            try
            {
                conexionBD.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexionBD);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PacienteModel pacienteModel = new PacienteModel();
                    ClinicaModel clinicaModel = new ClinicaModel();

                    pacienteModel.id_paciente = reader[0].ToString();
                    pacienteModel.nombre = reader[1].ToString();
                    pacienteModel.apellidos = reader[2].ToString();
                    pacienteModel.direccion = reader[3].ToString();
                    pacienteModel.telefono = reader[4].ToString();
                    pacienteModel.foto = reader[5].ToString();
                    pacienteModel.imagen = LoadImage(reader[5].ToString());
                    pacienteModel.email = reader[6].ToString();
                    if (reader[7].ToString() == "False") { pacienteModel.marketing = 0; } else { pacienteModel.marketing = 1; }
                    //pacienteModel.marketing = reader[6].ToString();
                    pacienteModel.antecedente = reader[9].ToString();
                    if (reader[10].ToString().Equals(""))
                    {
                        pacienteModel.imagen_membresia = null;
                    }
                    else
                    {
                        pacienteModel.imagen_membresia = LoadImage_Membresia(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\trofeo.jpg"));

                    }
                    pacienteModel.membresia = reader[10].ToString();
                    if (reader[11].ToString() == "False") { pacienteModel.factura = false; } else { pacienteModel.factura = true; }
                    clinicaModel.id_clinica = reader[12].ToString();
                    clinicaModel.nombre_sucursal = reader[13].ToString();
                    clinicaModel.color = reader[14].ToString();
                    pacienteModel.clinica = clinicaModel;
                    /*saber si la membresi que devuelve es vacio o no 
                    si regresa  vacio entonces no poner la imagen del trofeo
                    de lo contrario mandar la foto en una variable para que la muestre*/
                    listaPaciente.Add(pacienteModel);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexionBD.Close();
            return listaPaciente;
        }

        public bool eliminarPaciente(string id_paciente,string id_clinica, string alias)
        {
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ELIMINAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "DELETE FROM paciente where id_paciente='" + id_paciente + "' and id_clinica='"+id_clinica+"'";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se eliminó correctamente el paciente: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar eliminar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        public bool insertarPaciente(string nombre, string apellidos, string direccion, string telefono, string foto, string antecedente, string email, int marketing, string id_clinica, string alias)
        {
            Seguridad s = new Seguridad();
            string foto_paciente = "";
            string id_paciente = "";
            Seguridad seguridad = new Seguridad();
            if (foto.Equals(""))
            {
                foto_paciente = foto;
            }
            else
            {
                foto_paciente = s.quitar_acentos(foto);
            }
            id_paciente = seguridad.SHA1(nombre + apellidos + direccion + telefono + foto_paciente + antecedente + email + marketing + id_clinica + DateTime.Now);
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSERTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "INSERT INTO usuario (id_usuario,alias,nombre,apellidos,password,id_rol) VALUES('" + auxiliar_identificador + "','" + alias + "','" + nombre + "','" + apellidos + "','" + password + "'," + id_rol + ")";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    query = "INSERT INTO paciente (id_paciente,nombre,apellidos,direccion,telefono,foto,email,marketing,id_clinica,antecedentes,factura) VALUES('" + id_paciente + "','" + nombre + "','" + apellidos + "','" + direccion + "','" + telefono + "','" + foto_paciente + "','" + email + "'," + marketing + ",'" + id_clinica + "','" + antecedente + "'," + 0 + ")";

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se insertó correctamente el Paciente: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar insertar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarPaciente(string id_paciente, string nombre, string apellidos, string direccion, string telefono, string foto, string antecedente, string email, int marketing, string id_clinica, string alias)
        {
            Seguridad s = new Seguridad();
            string foto_paciente = "";
            if (foto.Equals(""))
            {
                foto_paciente = foto;
            }
            else
            {
                foto_paciente = s.quitar_acentos(foto);
            }

            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE paciente set nombre = '" + nombre + "',apellidos = '" + apellidos + "',direccion = '" + direccion + "',telefono = '" + telefono + "',foto = '" + foto_paciente + "',email = '" + email + "',marketing = " + marketing + ",antecedentes='" + antecedente  + "', id_clinica='"+id_clinica+"' where id_paciente ='" + id_paciente + "'";
                    Console.WriteLine(query);

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se actualizó correctamente el Paciente: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar actualizar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizar_Factura(string id_paciente ,string id_clinica , int factura,string alias)
        {
            Seguridad s = new Seguridad();

            try
            {
                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE paciente set factura = " + factura + " where id_paciente ='" + id_paciente + "' and id_clinica='"+id_clinica+"'";
                    Console.WriteLine(query);

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Este usuario solicita factura de sus pagos: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar hacer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        private BitmapImage LoadImage(string filename)
        {
            BitmapImage bi;

            if (File.Exists(this.ruta + filename))
            {
                //MessageBox.Show("se encontro la foto en " + filename);
                var bitmap = new BitmapImage();
                //MessageBox.Show("A");
                var stream = File.OpenRead(this.ruta + filename);
                //MessageBox.Show("B");
                bitmap.BeginInit();
                //MessageBox.Show("C");
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                //MessageBox.Show("D");
                bitmap.StreamSource = stream;
                //MessageBox.Show("E");
                bitmap.EndInit();
                //MessageBox.Show("F");
                stream.Close();
                //MessageBox.Show("G");
                stream.Dispose();
                //MessageBox.Show("H");

                return bitmap;
            }
            else
            {
                //MessageBox.Show("no se encontro la foto en " + filename);
                var bitmap = new BitmapImage();
                //MessageBox.Show("A");
                var stream = File.OpenRead(System.IO.Path.Combine(@System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, @"..\..\..\Assets\img1.jpg"));
                //MessageBox.Show("B");
                bitmap.BeginInit();
                //MessageBox.Show("C");
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                //MessageBox.Show("D");
                bitmap.StreamSource = stream;
                //MessageBox.Show("E");
                bitmap.EndInit();
                //MessageBox.Show("F");
                stream.Close();
                //MessageBox.Show("G");
                stream.Dispose();
                //MessageBox.Show("H");

                return bitmap;
            }
        }

        private BitmapImage LoadImage_Membresia(string filename)
        {
            BitmapImage bi;


            //MessageBox.Show("se encontro la foto en " + filename);
            var bitmap = new BitmapImage();
            //MessageBox.Show("A");
            var stream = File.OpenRead(filename);
            //MessageBox.Show("B");
            bitmap.BeginInit();
            //MessageBox.Show("C");
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //MessageBox.Show("D");
            bitmap.StreamSource = stream;
            //MessageBox.Show("E");
            bitmap.EndInit();
            //MessageBox.Show("F");
            stream.Close();
            //MessageBox.Show("G");
            stream.Dispose();
            //MessageBox.Show("H");

            return bitmap;

        }
        public bool eliminarMembresia(PacienteModel paciente, string alias)
        {
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE paciente set membresia = ' ' where id_paciente ='" + paciente.id_paciente + "'";
                    Console.WriteLine(query);

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");
                    System.Windows.Forms.MessageBox.Show("Se eliminó correctamente la Membresía: ", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error al intentar eliminar ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexionBD.Close();
                return false;
            }
        }

        public bool actualizarMembresia(PacienteModel paciente, string alias)
        {
            try
            {

                MySqlCommand cmd; ;
                if (online)
                {
                    bool internet = ti.Test();

                    if (!internet)
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA ACTUALIZAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI NO LO HAY, ENTONCES NO HACER NADA Y SEGUIR MANTENIENDO QUERIES EN EL ARCHIVO 
                        return false;
                    }
                    else
                    {
                        //EN CASO DE REALIZAR UNA PETICION PARA INSEACTUALIZARRTAR EN SERVIDOR VERIFICAR SI HAY INTERNET, SI LO HAY, ENTONCES INSERTAR TODOS LOS QUERIES DEL ARCHIVO

                        //query = "UPDATE usuario set alias = '" + alias + "',nombre = '" + nombre + "',apellidos = '" + apellidos + "',password = '" + password + "',id_rol = " + id_rol + " where id_usuario = '" + id_usuario + "'";
                        Sincronizar sincronizar = new Sincronizar();
                        sincronizar.insertarArchivoEnServidor(conexionBD);
                        return true;
                    }
                }
                else
                {
                    //MessageBox.Show(paciente.id_paciente);
                    //string auxiliar_identificador = MostrarUsuario_Update(id_usuario);
                    query = "UPDATE paciente set membresia = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' where id_paciente ='" + paciente.id_paciente + "'";
                    //MessageBox.Show(query);
                    Console.WriteLine(query);

                    conexionBD.Open();
                    cmd = new MySqlCommand(query, conexionBD);
                    cmd.ExecuteReader();
                    conexionBD.Close();

                    Escribir_Archivo ea = new Escribir_Archivo();
                    ea.escribir_imagen_eliminar(query + ";", @configuracion.carpetas.ruta_script_carpeta + "\\script_temporal_" + alias + ".txt");

                    return true;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                conexionBD.Close();
                return false;
            }
        }

    }
}