using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using bonita_smile_v1.Modelos;

namespace bonita_smile_v1.Servicios
{
    class Escribir_Archivo
    {
        string ruta = @"C:\backup_bs\script_temporal.txt";
        string ruta_borrar = @"C:\backup_bs\eliminar_imagen_temporal.txt";
        List<string> abonos = new List<string>();
        List<string> carpeta_archivos = new List<string>();
        List<string> clinica = new List<string>();
        List<string> foto_estudio_carpeta = new List<string>();
        List<string> marketing = new List<string>();
        List<string> motivo_cita = new List<string>();
        List<string> nota_de_digi_evolucion = new List<string>();
        List<string> paciente = new List<string>();
        List<string> permisos = new List<string>();
        List<string> usuario = new List<string>();

        public Escribir_Archivo()
        {
            abonos.Add("id_paciente");
            abonos.Add("id_motivo");
            abonos.Add("fecha");
            abonos.Add("monto");
            abonos.Add("comentario");
            abonos.Add("auxiliar_identificador");

            carpeta_archivos.Add("nombre_carpeta");
            carpeta_archivos.Add("id_paciente");
            carpeta_archivos.Add("auxiliar_identificador");

            clinica.Add("nombre_sucursal");
            clinica.Add("color");
            clinica.Add("auxiliar_identificador");

            foto_estudio_carpeta.Add("id_carpeta");
            foto_estudio_carpeta.Add("id_paciente");
            foto_estudio_carpeta.Add("foto");
            foto_estudio_carpeta.Add("auxiliar_identificador");

            marketing.Add("descripcion");
            marketing.Add("fecha_de_envio");
            marketing.Add("id_paciente");
            marketing.Add("auxiliar_identificador");

            motivo_cita.Add("descripcion");
            motivo_cita.Add("costo");
            motivo_cita.Add("id_paiente");
            motivo_cita.Add("auxuliar_identificador");

            nota_de_digi_evolucion.Add("id_paciente");
            nota_de_digi_evolucion.Add("id_motivo");
            nota_de_digi_evolucion.Add("descripcion");
            nota_de_digi_evolucion.Add("fecha");
            nota_de_digi_evolucion.Add("auxuliar_identificador");

            paciente.Add("nombre");
            paciente.Add("apellidos");
            paciente.Add("direccion");
            paciente.Add("telefono");
            paciente.Add("foto");
            paciente.Add("email");
            paciente.Add("marketing");
            paciente.Add("id_clinica");
            paciente.Add("antecedente");
            paciente.Add("auxiliar_identificador");

            permisos.Add("id_usuario");
            permisos.Add("id_clinica");
            permisos.Add("auxiliar_identificador");

            usuario.Add("alias");
            usuario.Add("nombre");
            usuario.Add("apellidos");
            usuario.Add("password");
            usuario.Add("id_rol");
            usuario.Add("auxiliar_identificador");
        }


        public void escribir(string script)
        {
            //FileAttributes attributes = File.GetAttributes(ruta);
            FileStream fs = null;
            try
            {

                if (File.Exists(ruta))
                {
                    SetFileReadAccess(ruta, false);
                    // Create the file, or overwrite if the file exists.
                    StreamWriter sw = new StreamWriter(ruta, true);
                    sw.WriteLine(script);
                    sw.Close();
                    File.SetAttributes(ruta, File.GetAttributes(ruta) | FileAttributes.Hidden);
                    //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                    SetFileReadAccess(ruta, true);
                }
                else
                {
                    fs = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                    fs.Close();
                    StreamWriter sw = new StreamWriter(ruta, true);
                    sw.WriteLine(script);
                    sw.Close();
                    File.SetAttributes(ruta, File.GetAttributes(ruta) | FileAttributes.Hidden);
                    //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                    SetFileReadAccess(ruta, true);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Archivo no encontrado");
                Console.WriteLine(ex.ToString());
            }
        }

        public void escribir_imagen_eliminar(string script, string ruta)
        {
            //FileAttributes attributes = File.GetAttributes(ruta);
            FileStream fs = null;
            try
            {

                if (File.Exists(ruta))
                {
                    SetFileReadAccess(ruta, false);
                    // Create the file, or overwrite if the file exists.
                    StreamWriter sw = new StreamWriter(ruta, true);
                    sw.WriteLine(script);
                    sw.Close();
                    File.SetAttributes(ruta, File.GetAttributes(ruta) | FileAttributes.Hidden);
                    //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                    SetFileReadAccess(ruta, true);
                }
                else
                {
                    fs = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                    fs.Close();
                    StreamWriter sw = new StreamWriter(ruta, true);
                    sw.WriteLine(script);
                    sw.Close();
                    File.SetAttributes(ruta, File.GetAttributes(ruta) | FileAttributes.Hidden);
                    //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                    SetFileReadAccess(ruta, true);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Archivo no encontrado");
                Console.WriteLine(ex.ToString());
            }
        }

        private  FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        public List<String> leer(string ruta)
        {
            List<string> datosArchivo = new List<string>();
            using (StreamReader sr = File.OpenText(ruta))
            {
                string linea = "";
                while ((linea = sr.ReadLine()) != null)
                {
                    if(linea.Equals(""))
                    {
                        MessageBox.Show("esta vacio");
                    }else
                    {
                        datosArchivo.Add(linea);
                    }


                    //Console.WriteLine(s);
                    //MessageBox.Show(s);
                    
                }
            }
            return datosArchivo;
        }

        public Categoria categorizar(string ruta)
        {
            List<String> abonos = new List<string>();
            List<String> carpeta_archivos = new List<string>();
            List<String> clinica = new List<string>();
            List<String> fotos_estudio_carpeta = new List<string>();
            List<String> marketing = new List<string>();
            List<String> motivo_cita = new List<string>();
            List<String> nota_de_digi_evolucion = new List<string>();
            List<String> paciente = new List<string>();
            List<String> permisos = new List<string>();
            List<String> rol = new List<string>();
            List<String> usuario = new List<string>();

            Categoria categoria = new Categoria();

            using (StreamReader sr = File.OpenText(ruta))
            {

                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(s);

                    if (s.Contains("INSERT INTO abonos") || s.Contains("UPDATE abonos"))
                        abonos.Add(s);

                    if (s.Contains("INSERT INTO carpeta_archivos") || s.Contains("UPDATE carpeta_archivos"))
                        carpeta_archivos.Add(s);

                    if (s.Contains("INSERT INTO clinica") || s.Contains("UPDATE clinica"))
                        clinica.Add(s);

                    if (s.Contains("INSERT INTO fotos_estudio_carpeta") || s.Contains("UPDATE fotos_Estudio_carpeta"))
                        fotos_estudio_carpeta.Add(s);

                    if (s.Contains("INSERT INTO marketing") || s.Contains("UPDATE marketing"))
                        marketing.Add(s);

                    if (s.Contains("INSERT INTO motivo_cita") || s.Contains("UPDATE motivo_cita"))
                        motivo_cita.Add(s);

                    if (s.Contains("INSERT INTO nota_de_digi_evolucion") || s.Contains("UPDATE nota_de_digi_evolucion"))
                        nota_de_digi_evolucion.Add(s);

                    if (s.Contains("INSERT INTO paciente") || s.Contains("UPDATE paciente"))
                        paciente.Add(s);

                    if (s.Contains("INSERT INTO permisos") || s.Contains("UPDATE permisos"))
                        permisos.Add(s);

                    if (s.Contains("INSERT INTO rol") || s.Contains("UPDATE rol"))
                        rol.Add(s);

                    if (s.Contains("INSERT INTO usuario") || s.Contains("UPDATE usuario"))
                        usuario.Add(s);
                }

                categoria.abonos = abonos;
                categoria.carpeta_archivos = carpeta_archivos;
                categoria.clinica = clinica;
                categoria.fotos_estudio_carpeta = fotos_estudio_carpeta;
                categoria.marketing = marketing;
                categoria.motivo_cita = motivo_cita;
                categoria.nota_de_digi_evolucion = nota_de_digi_evolucion;
                categoria.paciente = paciente;
                categoria.permisos = permisos;
                categoria.rol = rol;
                categoria.usuario = usuario;

                /*foreach(string query in abonos)
                {
                    escribir(@"c:\offline\script.txt",query);
                }

                foreach (string query in carpeta_archivos)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in clinica)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in fotos_estudio_carpeta)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in marketing)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in motivo_cita)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in nota_de_digi_evolucion)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in paciente)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in permisos)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in rol)
                {
                    escribir(@"c:\offline\script.txt", query);
                }

                foreach (string query in usuario)
                {
                    escribir(@"c:\offline\script.txt", query);
                }*/

                return categoria;
            }
        }

        public List<String> verificarActualizaciones(Categoria categorias)
        {
            //generalizar_("abonos",abonos,categorias.abonos);
            //generalizar_("carpeta_archivos", carpeta_archivos, categorias.carpeta_archivos);
            Categoria cate = new Categoria();
            cate.clinica = generalizar_("clinica", clinica, categorias.clinica);
            foreach (var l in cate.clinica)
            {
                MessageBox.Show("VALORES NUEVOS DE LA LISTA");
                MessageBox.Show(l);
            }
            return cate.clinica;

            /* generalizar_("fotos_estudio_carpeta", foto_estudio_carpeta, categorias.fotos_estudio_carpeta);
             generalizar_("marketing", marketing, categorias.marketing);
             generalizar_("motivo_cita", motivo_cita, categorias.motivo_cita);
             generalizar_("nota_de_digi_evolucion", nota_de_digi_evolucion, categorias.nota_de_digi_evolucion);
             generalizar_("paciente", paciente, categorias.paciente);
             generalizar_("permisos", permisos, categorias.permisos);
             generalizar_("usuario", usuario, categorias.usuario);*/
        }
        public List<string> corregirArchivo()
        {
            Categoria categorias = new Categoria();
            categorias = categorizar(@"c:\offline\script_temporal.txt");
            foreach (var list in categorias.clinica)
            {
                MessageBox.Show(list);
            }
            return verificarActualizaciones(categorias);
        }
        public string extraer_aux_identificador(string query)
        {
            string aux_identi = "";
            bool bandera = false;
            char[] charArr = query.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                if (!bandera)
                {
                    if (charArr[i].Equals('<'))
                    {
                        if (charArr[i + 1].Equals('!'))
                        {
                            if (charArr[i + 2].Equals('-'))
                            {
                                if (charArr[i + 3].Equals('-'))
                                {
                                    i = i + 3;
                                    bandera = true;

                                }
                            }
                        }
                    }
                }
                else
                {
                    if (charArr[i].Equals('-'))
                    {
                        if (charArr[i + 1].Equals('-'))
                        {
                            if (charArr[i + 2].Equals('>'))
                            {
                                i = charArr.Length;
                                break;

                            }
                            else
                            {
                                aux_identi = aux_identi + charArr[i];
                            }
                        }
                        else
                        {
                            aux_identi = aux_identi + charArr[i];
                        }
                    }
                    else
                    {
                        aux_identi = aux_identi + charArr[i];
                    }

                }

            }
            return aux_identi;
        }

        public List<string> mandarlo_lista(string[] valores)
        {
            List<string> lista = new List<string>();
            for (int i = 0; i < valores.Length; i++)
            {
                lista.Add(limpiar_iguales(valores[i]));
            }
            return lista;
        }

        public string limpiar_iguales(string cadena)
        {
            string valor_limpio = "";
            char[] charArr = cadena.ToCharArray();
            int i = 0;
            while (!charArr[i].Equals('='))
            {
                i++;
            }
            valor_limpio = cadena.Substring(i + 1);

            return valor_limpio;
        }
        public string extraer_valores_update(string query)
        {
            string aux_identi = "";
            bool bandera = false;
            char[] charArr = query.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                if (!bandera)
                {
                    if (charArr[i].Equals('s'))
                    {
                        if (charArr[i + 1].Equals('e'))
                        {
                            if (charArr[i + 2].Equals('t'))
                            {
                                i = i + 2;
                                bandera = true;


                            }
                        }
                    }
                }
                else
                {
                    if (charArr[i].Equals('w'))
                    {
                        if (charArr[i + 1].Equals('h'))
                        {
                            if (charArr[i + 2].Equals('e'))
                            {
                                if (charArr[i + 3].Equals('r'))
                                {

                                    if (charArr[i + 4].Equals('e'))
                                    {
                                        i = charArr.Length;
                                        break;

                                    }
                                    else
                                    {
                                        aux_identi = aux_identi + charArr[i];
                                    }
                                }
                                else
                                {
                                    aux_identi = aux_identi + charArr[i];
                                }
                            }
                            else
                            {
                                aux_identi = aux_identi + charArr[i];
                            }
                        }
                        else
                        {
                            aux_identi = aux_identi + charArr[i];
                        }
                    }
                    else
                    {
                        aux_identi = aux_identi + charArr[i];
                    }
                }
            }
            return aux_identi;
        }
        public List<string> generalizar_(string nombre_tabla, List<string> campos, List<string> categoria)
        {
            List<string> list = new List<string>();
            string valor_tabla = "";
            string campos_tabla = "";
            int i = 0;
            List<string> aux_categoria = new List<string>();
            foreach (var campito in categoria)
            {
                aux_categoria.Add(campito);
            }
            //var aux_categoria = categoria;
            foreach (var query in categoria)
            {

                if (query.Contains("UPDATE"))
                {
                    MessageBox.Show(i.ToString());
                    MessageBox.Show("imprimo query : " + query);
                    if (i != 0)
                    {
                        for (int j = i - 1; j != -1; j--)
                        {
                            MessageBox.Show("imprimo J " + j);
                            MessageBox.Show("imprimo lista en posicion j " + aux_categoria[j]);
                            if (aux_categoria[j].Contains("INSERT"))
                            {
                                MessageBox.Show("imprimo J en if" + j);
                                string aux_identificador_update = extraer_aux_identificador(query);
                                string aux_identificador_insert = extraer_aux_identificador(aux_categoria[j]);

                                MessageBox.Show("aux_update " + aux_identificador_update);
                                MessageBox.Show("aux_insert " + aux_identificador_insert);

                                if (aux_identificador_insert.Equals(aux_identificador_update, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    foreach (var campo in campos)
                                    {
                                        campos_tabla = campos_tabla + campo + ",";
                                    }
                                    campos_tabla = campos_tabla.Substring(0, campos_tabla.Length - 1);
                                    //campos_tabla.Remove(campos_tabla.Length);
                                    string valor_campos = extraer_valores_update(query);
                                    string[] valores = valor_campos.Split(',');
                                    List<string> valor = mandarlo_lista(valores);
                                    foreach (var val in valor)
                                    {
                                        valor_tabla = valor_tabla + val + ",";
                                    }
                                    valor_tabla = valor_tabla.Substring(0, valor_tabla.Length - 1);
                                    //valor_tabla.Remove(valor_tabla.Length);
                                    string query_insert = "INSERT INTO " + nombre_tabla + "(" + campos_tabla + ") VALUES(" + valor_tabla + ");";
                                    //aux_categoria.Insert(j, "");
                                    //aux_categoria.Add("");
                                    //aux_categoria.Add(query_insert);
                                    //aux_categoria.Insert(i, query_insert);
                                    MessageBox.Show("El valor de j es " + j);
                                    MessageBox.Show("El valor de i es " + i);
                                    aux_categoria[j] = "";
                                    aux_categoria[i] = query_insert;
                                    MessageBox.Show("imprimo query_insert :" + query_insert);
                                    campos_tabla = "";
                                    valor_tabla = "";
                                    //categoria.Add(query_insert);
                                    //escribir(, query_insert);
                                }
                            }
                        }
                    }

                }
                i++;
            }
            return aux_categoria;
        }

        public void SetFileReadAccess(string FileName, bool SetReadOnly)
        {
            // Create a new FileInfo object.
            FileInfo fInfo = new FileInfo(FileName);

            // Set the IsReadOnly property.
            fInfo.IsReadOnly = SetReadOnly;
        }

        public List<string> obtenerQueryArchivo()
        {
            List<string> lista = new List<string>();
            if (File.Exists(ruta))
            {
                using (StreamReader sr = File.OpenText(ruta))
                {

                    string query = "";
                    while ((query = sr.ReadLine()) != null)
                    {
                        lista.Add(query);
                    }
                    return lista;
                }
            }
            else
            {
                return lista=null;
            }
           
           
        }

        public void escribir_fotos_borrar(string nombre_foto)
        {
            //FileAttributes attributes = File.GetAttributes(ruta);
            FileStream fs = null;
            try
            {

                if (File.Exists(ruta_borrar))
                {
                    SetFileReadAccess(ruta_borrar, false);
                    // Create the file, or overwrite if the file exists.
                    StreamWriter sw = new StreamWriter(ruta_borrar, true);
                    sw.WriteLine(nombre_foto);
                    sw.Close();
                    File.SetAttributes(ruta, File.GetAttributes(ruta_borrar) | FileAttributes.Hidden);
                    //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                    SetFileReadAccess(ruta_borrar, true);
                }
                else
                {
                    fs = new FileStream(ruta_borrar, FileMode.Create, FileAccess.Write);
                    fs.Close();
                    StreamWriter sw = new StreamWriter(ruta_borrar, true);
                    sw.WriteLine(nombre_foto);
                    sw.Close();
                    File.SetAttributes(ruta, File.GetAttributes(ruta_borrar) | FileAttributes.Hidden);
                    //attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                    SetFileReadAccess(ruta_borrar, true);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Archivo no encontrado");
                Console.WriteLine(ex.ToString());
            }
        }

        public List<string> obtener_nombre_foto_eliminar()
        {
            List<string> lista = new List<string>();
            if (File.Exists(ruta_borrar))
            {
                using (StreamReader sr = File.OpenText(ruta_borrar))
                {

                    string nombre_foto = "";
                    while ((nombre_foto = sr.ReadLine()) != null)
                    {
                        lista.Add(nombre_foto);
                    }
                    return lista;
                }
            }
            else
            {
                return lista = null;
            }
        }



    }
}