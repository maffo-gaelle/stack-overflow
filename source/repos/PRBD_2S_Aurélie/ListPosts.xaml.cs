using System;
using System.Linq;
using System.Collections.ObjectModel;
using PRBD_Framework;
using System.Windows;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour ListPosts.xaml
    /// </summary>
    public partial class ListPosts : UserControlBase
    {
        public ICommand Connexion { get; set; }
        public ICommand Inscription { get; set; }
        public ICommand AskQuestion { get; set; }
        public ICommand Deconnexion { get; set; }
        public ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get => posts;
            set
            {
                posts = value;
                RaisePropertyChanged(nameof(posts));
            }
        }
        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                ApplyFilterAction();
                RaisePropertyChanged(nameof(Filter));
            }
        }

        
        public ListPosts()
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
                //Application.Current.MainWindow = this;
                App.CurrentUser = null;
            });
        }

        private void ApplyFilterAction()
        {
            Console.WriteLine("Search clicked! " + Filter);
            var model = new Model();
            var query = from p in model.Posts
                        where p.Body.Contains(Filter) || p.Title.Contains(Filter)
                        select p;
            Posts = new ObservableCollection<Post>(query);
            Console.WriteLine($"{query.Count()} Posts trouvés");
        }

        public void ConnexionAction()
        {
            Console.WriteLine("Maffo");
            var connexion = new Connexion();
            connexion.Show();
            Application.Current.MainWindow = connexion;
           // Close();
        }

        public void InscriptionAction()
        {
            Console.WriteLine("page Inscription");
            var inscription = new Signup();
            inscription.Show();
            Application.Current.MainWindow = inscription;
            //Close();
        }
    }
}
