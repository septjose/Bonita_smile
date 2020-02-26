using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Modelos
{
    public class ConfiguracionFTPModel
    {
        public string nombre_impresora { get; set; }
        public string ftp_path { get; set; }
        public string ftp_server { get; set; }
        public string ftp_user { get; set; }
        public string ftp_password { get; set; }

    }
}
