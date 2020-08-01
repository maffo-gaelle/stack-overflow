using System;
using System.Linq;
using System.Collections.ObjectModel;
using PRBD_Framework;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour ListPosts.xaml
    /// </summary>
    public partial class ListPosts : UserControlBase
    {
        public ICommand ShowPost { get; set; }
        public ICommand Newest { get; set; }
        public ICommand Vote { get; set; }
        public ICommand Unanswered { get; set; }
        public ICommand Active { get; set; }
        public ICommand DetailPost { get; set; }
        public ICommand NewQuestion { get; set; }
        
        private ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get => posts;
            set
            {
                posts = value;
                //GetParentPost();
                RaisePropertyChanged(nameof(Posts));
            }
        }

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
        public void GetParentPost()
        {//Pourquoi IIEnumerable fonctionne et pas ICollection, Quelle est la difference et il y'a un overstackflow ??
            Console.WriteLine("Posts parents séléctionnés");
            IEnumerable<Post> parentsPosts = App.Model.Posts;
            parentsPosts = from p in App.Model.Posts
                           where p.Parent == null
                           select p;
            Console.WriteLine(parentsPosts);
            Posts = new ObservableCollection<Post>(parentsPosts);
        }

        public void NewestAction()
        {
            Console.WriteLine("Newest");
            IEnumerable<Post> newest = App.Model.Posts;
            newest = from p in App.Model.Posts
                     where p.Parent == null
                     orderby p.Timestamp descending
                     select p;
            Posts = new ObservableCollection<Post>(newest);
        }

        //ScorePost ne marche pas de la meme façon que neest et autres parce que le tri se fait sur une proprieté qui n'a pas été mappée donc non supportée par Linq
        public void VoteAction()
        {
            Console.WriteLine("Vote");
            IEnumerable<Post> vote = App.Model.Posts;
            vote = from p in App.Model.Posts
                   where p.Parent == null
                   //orderby p.ScorePost descending
                   select p;
            //Ici on transforme IEnumerable en AsEnumerable pour pouvoir utiliser  OrderByDescending sur le ScorePost non mappé
            var postVote = vote.AsEnumerable().OrderByDescending(p => p.ScorePost).ToList();
            Posts = new ObservableCollection<Post>(vote);
        }

        public void UnansweredAction()
        {
            Console.WriteLine("Unanswered");
            IEnumerable<Post> unanswered = App.Model.Posts;
            unanswered = from p in App.Model.Posts
                         where p.AcceptedAnswer == null && p.Parent == null
                         orderby p.Timestamp descending
                   select p;
            Posts = new ObservableCollection<Post>(unanswered);
        }

        public void ActiveAction()
        {
            Console.WriteLine("ActiveAction");

        }
        
        private void ApplyFilterAction()
        {
            Console.WriteLine("Search clicked! " + Filter);
            IEnumerable<Post> query = App.Model.Posts;
            if(!string.IsNullOrEmpty(Filter))
               query = from p in App.Model.Posts
                        where p.Body.Contains(Filter) || p.Title.Contains(Filter)
                        select p;
            Posts = new ObservableCollection<Post>(query);
            Console.WriteLine($"{query.Count()} Posts trouvés");
        }
        public ListPosts()
        {

            InitializeComponent();
            DataContext = this;
            GetConnectUser();
            GetDeConnectUser();

            Posts = new ObservableCollection<Post>(App.Model.Posts.Where(p => p.Parent == null));

            //this.nameStudent.BackColor = System.Drawing.Color.Aqua;

            Newest = new RelayCommand(NewestAction, () => {
                return true;
            });

            Vote = new RelayCommand(VoteAction, () => {
                return true;
            });

            Unanswered = new RelayCommand(UnansweredAction, () => {
                return true;
            });

            Active = new RelayCommand(ActiveAction, () => {
                return true;
            });

            NewQuestion = new RelayCommand(() =>
            {
                App.NotifyColleagues(AppMessages.MSG_NEW_QUESTION);
            });

            App.Register<Post>(this, AppMessages.MSG_QUESTION_CHANGED,
                                post => { ApplyFilterAction(); });
            App.Register<Post>(this, AppMessages.MSG_QUESTION_DELETED,
                                post => { ApplyFilterAction(); });

            //Ouvre un nouveau onglet , cree une notification de type post qui doit etre mis à jour
            ShowPost = new RelayCommand<Post>(Post =>
            {
                App.NotifyColleagues(AppMessages.MSG_DETAILS_POST, Post);
            });

        }


    }
}
