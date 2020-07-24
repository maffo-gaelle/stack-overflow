using PRBD_Framework;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using System.Windows.Controls;


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //ici on reçoit la notification avec 3 parametre le this, le message envoyé, et une fonction lambda
            //par ex, pour ajouter un post, je cree une notification dans le add post, puis je cree mon message dans app et je par voir celui
            //qui doit recevoir la notification et je fait un register pour recevoir la notification
            App.Register<Post>(this, AppMessages.MSG_DETAILS_POST, post =>
            {
                if (post != null)
                {
                    var tab = (from TabItem t in tabControl.Items where (string)t.Header == post.Title select t).FirstOrDefault();
                    if (tab == null)
                        AddTab(post, false);
                    else
                        Dispatcher.InvokeAsync(() => tab.Focus());
                }
            });
            //App.Register<Post>(this, AppMessages.MSG_DETAILS_POST, post =>
            //{
            //    if (post != null)
            //    {
            //        var tab = (from TabItem t in tabControl.Items where (string)t.Header == post.Title select t).FirstOrDefault();
            //        if (tab == null)
            //            AddTab(post, false);
            //        else
            //            Dispatcher.InvokeAsync(() => tab.Focus());
            //    }
            //});

            App.Register<UserControlBase>(this, AppMessages.MSG_CLOSE_TAB, ctl =>
            {
                var tab = (from TabItem t in tabControl.Items where t.Content == ctl select t).SingleOrDefault();
                ctl.Dispose();
                tabControl.Items.Remove(tab);
            });
        }

        private void AddTab(Post post, bool isNew)
        {
            var ctl = new PostDetailView(post, isNew);
            var tab = new TabItem()
            {
                Header = isNew ? "<new Post>" : post.Title,
                Content = ctl
            };

            tab.MouseDown += (o, e) =>
            {
                if (e.ChangedButton == MouseButton.Middle &&
                    e.ButtonState == MouseButtonState.Pressed)
                {
                    tabControl.Items.Remove(o);
                    (tab.Content as UserControlBase).Dispose();
                }
            };

            tab.PreviewKeyDown += (o, e) =>
            {
                if (e.Key == Key.W && Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    tabControl.Items.Remove(o);
                    (tab.Content as UserControlBase).Dispose();
                }
            };

            tabControl.Items.Add(tab);
            Dispatcher.InvokeAsync(() => tab.Focus());
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
