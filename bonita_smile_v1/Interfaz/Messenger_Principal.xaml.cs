using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bonita_smile_v1
{
    /// <summary>
    /// Lógica de interacción para Pagina_Messenger.xaml
    /// </summary>
    public partial class Pagina_Messenger : Page
    {
        public Pagina_Messenger()
        {
            InitializeComponent();
            wbMessenger.Navigate("https://www.Facebook.com/");
        }
    }
}
