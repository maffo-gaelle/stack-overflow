using PRBD_2S_Aurélie.Properties;
using PRBD_Framework;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace PRBD_2S_Aurélie
{
    public enum AppMessages
    {
        MSG_DETAILS_POST
    }
    public partial class App : ApplicationBase 
    { 
        public static User CurrentUser { get; set; }
        public static Model Model { get; set; }
        public App()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
            Model = new Model();
        }

        
    }
}
