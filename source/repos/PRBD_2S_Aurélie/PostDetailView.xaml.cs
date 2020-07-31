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

        private string btnDeleteActive;
        public string BtnDeleteActive
        {
            get => btnDeleteActive;
            set
            {
                btnDeleteActive = value;
                RaisePropertyChanged(nameof(btnDeleteActive));
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

        private ObservableCollection<Vote> votes;
        public ObservableCollection<Vote> Votes
        {
            get { return votes; }
            set
            {
                votes = value;
                RaisePropertyChanged(nameof(Votes));
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
                Console.WriteLine("repose selectionnée");
            }
        }

        

        public ICommand Valider { get; set; }
        public ICommand UpdatePost { get; set; }
        public ICommand DeletePost { get; set; }
        public ICommand UpdateResponse { get; set; }
        public ICommand DeleteResponse { get; set; }
        public ICommand AcceptResponse { get; set; }
        public ICommand VoteUpPost { get; set; }
        public ICommand VoteDownPost { get; set; }

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

        //private void BtnDeletePost()
        //{
        //    if( App.CurrentUser != null && App.CurrentUser == Post.Author && Post.NbAnswers == 0 ||
        //                 App.CurrentUser != null && App.CurrentUser.Role is Role.Admin && Post.NbAnswers == 0)
        //    {
        //        BtnDeleteActive = "Visible";
        //    } else
        //    {
        //        BtnDeleteActive = "collapse";
        //    }
        //}

        public void SaveAction()
        {
            var user = App.CurrentUser;
            var post = App.Model.CreateAnswer(user, Post, BodyResponse);
            Post.Answers.Add(post);
           
            App.Model.SaveChanges();
            BodyResponse = "";

            Answers = new ObservableCollection<Post>(Post.Answers);
        }

        public void VoteUpAction()
        {
            Console.WriteLine("Vote Up");
            var user = App.CurrentUser;
            var vote = (from v in App.Model.Votes
                        where v.PostId == Post.PostId && v.UserId == user.UserId
                        select v).FirstOrDefault();
            if(vote == null)
            {
                App.Model.CreateVote(user, Post, 1);
            } else
            {
                if(vote.UpDown == -1)
                {
                    App.Model.Votes.Remove(vote);
                    App.Model.SaveChanges();
                }
            }
            Votes = new ObservableCollection<Vote>(Post.Votes);
        }

        public void VoteDownAction()
        {
            Console.WriteLine("Vote Down");
            var user = App.CurrentUser;
            var vote = (from v in App.Model.Votes
                        where v.PostId == Post.PostId && v.UserId == user.UserId
                        select v).FirstOrDefault();
            if (vote == null)
            {
                App.Model.CreateVote(user, Post, -1);
            }
            else
            {
                if (vote.UpDown == 1)
                {
                    App.Model.Votes.Remove(vote);
                    App.Model.SaveChanges();
                } 
            }
            Votes = new ObservableCollection<Vote>(Post.Votes);
        }

        //le SelectedPost ne prend pas
        private void UpdateResponseAction()
        {
            Console.WriteLine("Modifier une repnse");
            SelectedPost.Body = bodyResponse;
            bodyResponse = "";
            App.Model.SaveChanges();
        }
        
        public PostDetailView(Post post)
        {
            InitializeComponent();
            DataContext = this;
            GetConnectUser();
            GetDeConnectUser();
            //BtnDeletePost();

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

            VoteUpPost = new RelayCommand(VoteUpAction, () =>
            {
                return true;
            });

            VoteDownPost = new RelayCommand(VoteDownAction, () =>
            {
                return true;
            });
            UpdatePost = new RelayCommand<Post>(p =>
            {
                App.NotifyColleagues(AppMessages.MSG_UPDATE_QUESTION, post);
            });

            DeletePost = new RelayCommand<Post>(p =>
            {
                App.NotifyColleagues(AppMessages.MSG_DELETE_QUESTION, post);
            });

            UpdateResponse = new RelayCommand(UpdateResponseAction, () =>
            {
                Console.WriteLine("boutton modifier reponse ok");
                return BodyResponse != null && BodyResponse.Length != 0;
            });
            //App.Register(this, AppMessages.MSG_QUESTION_CHANGED,
            //        () => { UpdateQuestion(); });

        }
    }
}
