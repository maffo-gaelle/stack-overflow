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
        public ICommand AffichePostsTag { get; set; }

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

        private int countAnswers;
        public int CountAnswers
        {
            get => countAnswers;
            set
            {
                countAnswers = value;
                RaisePropertyChanged(nameof(CountAnswers));
            }
        }

        private int scoreQuestion;
        public int ScoreQuestion
        {
            get => scoreQuestion;
            set
            {
                scoreQuestion = value;
                RaisePropertyChanged(nameof(ScoreQuestion));
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
            ObservableCollection<Post> PostsFilter = new ObservableCollection<Post>();

            IEnumerable<Post> query = App.Model.Posts.Where(p => p.Parent == null);
            if(!string.IsNullOrEmpty(Filter))
                query = (from p in App.Model.Posts
                        where p.Body.Contains(Filter) || p.Title.Contains(Filter)
                        select p);

            foreach (var p in query)
            {
                if (p.Parent == null)
                {
                    PostsFilter.Add(p);
                } else
                {
                    var a = (from s in App.Model.Posts
                             where s.PostId.Equals(p.Parent.PostId)
                             select s).FirstOrDefault();

                    if(!PostsFilter.Contains(a))
                    {
                        PostsFilter.Add(a);
                    }
                }
            }

            Posts = PostsFilter;
            Console.WriteLine($"{PostsFilter.Count()} Posts trouvés");
        }

        public ListPosts()
        {

            InitializeComponent();
            DataContext = this;
            GetConnectUser();
            GetDeConnectUser();

            Posts = new ObservableCollection<Post>(App.Model.Posts.Where(p => p.Parent == null && p.Title != null));
            Console.WriteLine("Nombre des réponses");
            
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
            App.Register(this, AppMessages.MSG_ANSWER_ADDED, () =>
            {
                Posts = new ObservableCollection<Post>(App.Model.Posts.Where(p => p.Parent == null && p.Title != null));
            });

            App.Register<Post>(this, AppMessages.MSG_POSTTAG_DELETED,
                                post => { ApplyFilterAction(); });
            //Ouvre un nouveau onglet , cree une notification de type post qui doit etre mis à jour
            ShowPost = new RelayCommand<Post>(Post =>
            {
                App.NotifyColleagues(AppMessages.MSG_DETAILS_POST, Post);
            });

            AffichePostsTag = new RelayCommand<Tag>(tag =>
            {
                Console.WriteLine("Affiche post tags clické");
                App.NotifyColleagues(AppMessages.MSG_DISPLAY_POSTOFTAG, tag);
            });
        }


    }
}
