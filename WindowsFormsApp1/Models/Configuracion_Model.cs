using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.Models
{
    [Serializable]
    public class Configuracion_Model
    {
        public ServidorModelo servidor_externo { get; set; }
        public ServidorModelo servidor_interno { get; set; }
        public RutasCarpetasModelo carpetas { get; set; }
    }
}
