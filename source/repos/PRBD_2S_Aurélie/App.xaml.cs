using PRBD_2S_Aurélie.Properties;
using PRBD_Framework;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : ApplicationBase 
    { 
        public static User CurrentUser { get; set; }
        public static Model Model { get; private set; }
        public App()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
            Model = new Model();
        }

        
    }
}
