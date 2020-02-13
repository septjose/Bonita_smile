using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    [Serializable]
    public class Configuracion_Model
    {
        /*ARCHIVO BINARIO CON 3 REGISTROS:
        RELACIONADO CON LA CONFIGUTRACION LOCAL DEL SERVIDOR----Registro 1 =======> Modelo Servidor (todos sus registros seran de tipo string)
        RELACIONADO CON LA CONFIGUTRACION EXTERNA DEL SERVIDOR (HOSTGATOR)----Registro 2 ====> Modelo Servidor
        RUTA DE CARPETA LOCALE DE ALMACENAMIENTO --Registro 3 ====> Modelo RutaCarpetaLocal (seran 4 registro de tipo string, 1 para cada carpeta)

            MODELO SERVIDOR:
                public string  servidor_local { get; set; }
                public string puerto_local { get; set; }
                public string usuario_local { get; set; }
                public string password_local { get; set; }
                public string database_local { get; set; }
                public string database_local_aux { get; set; }

            MODELO RUTAS DE CARPETAS
                public string ruta_imagenes_carpeta { get; set; }
                public string ruta_temporal_carpeta { get; set; }
                public string ruta_fotografias_carpeta { get; set; }
                public string ruta_subir_servidor_carpeta { get; set; }



            CONFIGURACION_MODEL
            public ServidorModelo servidor_externo
            public ServidorModelo servidor_interno
            public RutasCarpetasModelo carpetas

            */




        //el nombre de la base de datos local auxiliar par sincronizar
      
        public ServidorModelo servidor_externo { get; set; }
        public ServidorModelo servidor_interno { get; set; }
        public RutasCarpetasModelo carpetas { get; set; }
       
    }
}
