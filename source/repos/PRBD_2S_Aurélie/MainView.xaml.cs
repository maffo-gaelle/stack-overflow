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
    public partial class MainView : WindowBase
    {
        public ICommand Connexion { get; set; }
        public ICommand Inscription { get; set; }
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

       
        private void AddTabPost(Post post, bool isNew, bool isQuestion)
        {
            var ctl = new AskQuestionView(post, isNew, isQuestion);

            var tab = new TabItem()
            {
                Header = isNew ?  (isQuestion ? "<new Question>" : "<new Answer>") : (isQuestion ? "<Update Question>" : "<Update Answer>"),
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

        private void AddTabDetailsQuestion(Post post)
        {
            var ctl = new PostDetailView(post);

            var tab = new TabItem()
            {
                Header = $"<Details Question {post.PostId}>",
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

        private void LogOutAction()
        {
            Console.WriteLine("Logout action");
            App.CurrentUser = null;
            GetDeConnectUser();
        }

        public MainView()
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

            Deconnexion = new RelayCommand(LogOutAction);

            App.Register(this, AppMessages.MSG_NEW_QUESTION, () =>
            {   //Crée dynamiquement une nouvelle instance pour le post
                var post = App.Model.Posts.Create();
                //App.Model.Posts.Add(post);
                AddTabPost(post, true, true);
            });

            //ici on reçoit la notification avec 3 parametre le this, le message envoyé, et une fonction lambda
            //par ex, pour ajouter un post, je cree une notification dans le add post, puis je cree mon message dans app et je par voir celui
            //qui doit recevoir la notification et je fait un register pour recevoir la notification
            App.Register<Post>(this, AppMessages.MSG_DETAILS_POST, post =>
            {
                if (post != null)
                {
                    var tab = (from TabItem t in tabControl.Items where (string)t.Header == $"<Details Question {post.PostId}>" select t).FirstOrDefault();
                    if (tab == null)
                        AddTabDetailsQuestion(post);
                    else
                        Dispatcher.InvokeAsync(() => tab.Focus());
                }
            });
            //?????
            App.Register<UserControlBase>(this, AppMessages.MSG_CLOSE_TAB, ctl =>
            {
                var tab = (from TabItem t in tabControl.Items where t.Content == ctl select t).SingleOrDefault();
                ctl.Dispose();
                tabControl.Items.Remove(tab);
            });
        }


    }
}
