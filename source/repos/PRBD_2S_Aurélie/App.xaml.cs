using PRBD_2S_Aurélie.Properties;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PRBD_2S_Aurélie.Properties;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static User CurrentUser { get; set; }
        public static Model Model { get; private set; }
        public App()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
        }

        
    }
}
