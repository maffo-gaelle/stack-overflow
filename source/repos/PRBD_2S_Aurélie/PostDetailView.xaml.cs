using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour PostDetailView.xaml
    /// </summary>
    public partial class PostDetailView : UserControlBase
    {
        public Post Post { get; set; }

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

        private ObservableCollection<Post> answers;
        public ObservableCollection<Post> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                RaisePropertyChanged(nameof(Answers));
            }
        }

        private string bodyResponse;
        public string BodyResponse
        {
            get => bodyResponse;
            set
            {
                bodyResponse = value;
                RaiseErrorsChanged(nameof(BodyResponse));
            }
        }

        private Post selectedPost;
        public Post SelectedPost
        {
            get => selectedPost;
            set
            {
                selectedPost = value;
                RaisePropertyChanged(nameof(SelectedPost));
                BodyResponse = selectedPost.Body;
            }
        }

        

        public ICommand Valider { get; set; }
        public ICommand UpdatePost { get; set; }
        public ICommand DeletePost { get; set; }
        public ICommand UpdateResponse { get; set; }
        public ICommand DeleteResponse { get; set; }
        public ICommand AcceptResponse { get; set; }

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
        public void SaveAction()
        {
            var user = App.CurrentUser;
            var post = App.Model.CreateAnswer(user, Post, BodyResponse);
            Post.Answers.Add(post);
           
            App.Model.SaveChanges();

            Answers = new ObservableCollection<Post>(Post.Answers);
        }

        //public void UpdateQuestion()
        //{
        //     var post = from p in App.Model.Posts
        //               where p.PostId == Post.PostId
        //               select p;
        //}
        public PostDetailView(Post post)
        {
            InitializeComponent();
            DataContext = this;
            GetConnectUser();
            GetDeConnectUser();

            Post = post;
            Answers = new ObservableCollection<Post>(Post.Answers);
            foreach(var answer in Answers)
            {
                Console.WriteLine(answer.Body);
            }
            

            Valider = new RelayCommand(SaveAction, () =>
            {
                return BodyResponse != null && BodyResponse.Length != 0;
            });

            UpdatePost = new RelayCommand<Post>(p =>
            {
                App.NotifyColleagues(AppMessages.MSG_UPDATE_QUESTION, post);
            });
            //App.Register(this, AppMessages.MSG_QUESTION_CHANGED,
            //        () => { UpdateQuestion(); });
       
        }
    }
}
