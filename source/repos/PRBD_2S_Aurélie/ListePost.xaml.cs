using PRBD_Framework;
using System;
using System.Windows;
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
        public ICommand AskQuestion { get; set; }
        public ICommand Deconnexion { get; set; }

        public ListePost()
        {
            InitializeComponent();
            DataContext = this;

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

            AskQuestion = new RelayCommand(AskQuestionAction, () =>
            {
                return true;
            });
        }

                //public void InscriptionAction()
        //{
        //    Console.WriteLine("Le binding de l'inscription fonctionne");
        //    var inscription = new Insciption();
        //    inscription.Show();
        //}
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

        private void AskQuestionAction()
        {
            Console.WriteLine("Poser une question");
            var askQuestion = new AskQuestion();
            askQuestion.Show();
            var user = App.CurrentUser;
            Application.Current.MainWindow = askQuestion;
            Close();
        }

    }
}
