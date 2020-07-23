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
        public ICommand ShowPost { get; set; }
        public ICommand Newest { get; set; }
        public ICommand Vote { get; set; }
        public ICommand Unanswered { get; set; }
        public ICommand Active { get; set; }

        private string authorPost;
        public string AuthorPost { 
            get => authorPost; 
            set
            {
                authorPost = value;
                GetAuthorPost();
                RaisePropertyChanged(nameof(AuthorPost));
            }
        }
       
        private ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get => posts;
            set
            {
                posts = value;
                RaisePropertyChanged(nameof(Posts));
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
            Posts = new ObservableCollection<Post>(App.Model.Posts);

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

            ShowPost = new RelayCommand<Post>((m) => {
                Console.WriteLine("details du post");
                App.NotifyColleagues(AppMessages.MSG_DETAILS_POST, m); 
            });

         
        }

        public void NewestAction()
        {
            Console.WriteLine("Newest");

        }

        public void VoteAction()
        {
            Console.WriteLine("VoteAction");

        }

        public void UnansweredAction()
        {
            Console.WriteLine("UnansweredAction");

        }

        public void ActiveAction()
        {
            Console.WriteLine("ActiveAction");

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

        private string GetAuthorPost()
        {
            return "L'auteur du post";
        }

    }
}
