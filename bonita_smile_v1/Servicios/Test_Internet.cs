using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1.Servicios
{
    class Test_Internet
    {
        string result = "";
        public bool Test()
        {
            bool saber = false;
            System.Uri Url = new System.Uri("http://www.google.com/");
            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objResp;
            try
            {
                objResp = WebRequest.GetResponse();
                //result = "Su dispositivo está correctamente conectado a internet";
                saber = true;
                objResp.Close();
                WebRequest = null;
            }
            catch (Exception ex)
            {
                saber = false;
                //result = "Error al intentar conectarse a internet " + ex.Message;
                WebRequest = null;
            }
            return saber;
        }
    }
}
