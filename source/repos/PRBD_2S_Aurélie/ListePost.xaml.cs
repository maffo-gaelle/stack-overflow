using PRBD_Framework;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour ListePost.xaml
    /// </summary>
    public partial class ListePost : WindowBase
    {
        public ICommand Connexion { get; set; }
        public ICommand Inscription { get; set; }
        public ICommand Ask { get; set; }
        public ICommand Deconnexion { get; set; }

        private string connectUser;
        public string ConnectUser
        {
            get => connectUser;
            set
            {
                connectUser = value;
                RaisePropertyChanged(nameof(ConnectUser));
            }
        }

        private string deconnectUser;
        public string DeConnectUser
        {
            get => deconnectUser;
            set
            {
                deconnectUser = value;
                RaisePropertyChanged(nameof(DeConnectUser));
            }
        }

        public ListePost()
        {
            InitializeComponent();
            DataContext = this;

            GetConnectUser();
            GetDeConnectUser();


            Connexion = new RelayCommand(ConnexionAction, () => {
                return true;
            });

            Inscription = new RelayCommand(InscriptionAction, () => {
                return true;
            });

            Deconnexion = new RelayCommand(() =>
            {
                Application.Current.MainWindow = this;
                App.CurrentUser = null;
            });

            Ask = new RelayCommand(AskAction, () =>
            {
                return true;
            });
        }

        private void GetConnectUser()
        {
            Console.WriteLine("hello");
            if (App.CurrentUser != null)
            {
                ConnectUser = "Visible";
            }
            else
            {
                ConnectUser = "Collapsed";
            }
        }

        private void GetDeConnectUser()
        {
            if (App.CurrentUser != null)
            {
                DeConnectUser = "Collapsed";
            }
            else
            {
                DeConnectUser = "Visible";
            }
        }

        public void ConnexionAction()
        {
            Console.WriteLine("Maffo");
            var connexion = new Connexion();
            connexion.Show();
            Application.Current.MainWindow = connexion;
            Close();
        }

        public void InscriptionAction()
        {
            Console.WriteLine("page Inscription");
            var inscription = new Signup();
            inscription.Show();
            Application.Current.MainWindow = inscription;
            Close();
        }

        private void AskAction()
        {
            Console.WriteLine("Poser une question");
            var ask = new AskQuestion();
            ask.Show();
            //var user = App.CurrentUser;
            Application.Current.MainWindow = ask;
            Close();
        }

        
    }
}
