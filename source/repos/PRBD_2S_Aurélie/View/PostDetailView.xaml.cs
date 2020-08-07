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
        private bool editMode = false;
        private int editId;
        private Post post;
        public Post Post
        {
            get => post;
            set
            {
                post = value;
                RaisePropertyChanged(nameof(Post));
            }
        }
        private int score;
        public int Score
        {
            get => Post.Score; 
            set
            {
                score = value;
                RaisePropertyChanged(nameof(Score));
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

        private string btnDeletePost;
        public string BtnDeletePost
        {
            get => btnDeletePost;
            set
            {
                btnDeletePost = value;
                RaisePropertyChanged(nameof(BtnDeletePost));
            }
        }

        private string btnUpdatePost;
        public string BtnUpdatePost
        {
            get => btnUpdatePost;
            set
            {
                btnUpdatePost = value;
                RaisePropertyChanged(nameof(BtnUpdatePost));
            }
        }

        private string btnAnswerActive;
        public string BtnAnswerActive
        {
            get => btnAnswerActive;
            set
            {
                btnAnswerActive = value;
                RaisePropertyChanged(nameof(BtnAnswerActive));
            }
        }

        private string btnAcceptAnswer;
        public string BtnAcceptAnswer
        {
            get => btnAcceptAnswer;
            set
            {
                btnAcceptAnswer = value;
                RaisePropertyChanged(nameof(BtnAcceptAnswer));
            }
        }

        private string accept;
        public string Accept
        {
            get => accept;
            set
            {
                accept = value;
                RaisePropertyChanged(nameof(Accept));
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

        private ObservableCollection<Comment> comments;
        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                RaisePropertyChanged(nameof(Comments));
            }
        }

        private ObservableCollection<PostTag> postTags;
        public ObservableCollection<PostTag> PostTags
        {
            get { return postTags; }
            set
            {
                postTags = value;
                RaisePropertyChanged(nameof(PostTags));
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

        private ObservableCollection<Tag> tags;
        public ObservableCollection<Tag> Tags
        {
            get { return tags; }
            set
            {
                tags = value;
                RaisePropertyChanged(nameof(Tags));
            }
        }
        private string bodyResponse; 
        public string BodyResponse
        {
            get => bodyResponse;
            set
            {
                bodyResponse = value;
                RaisePropertyChanged(nameof(BodyResponse));
            }
        }

        private string bodyComment; 
        public string BodyComment
        {
            get => bodyComment;
            set
            {
                bodyComment = value;
                RaisePropertyChanged(nameof(BodyComment));
            }
        }

        private int countAnwsers;
        public int CountAnswers
        {
            get => countAnwsers;
            set
            {
                countAnwsers = value;
                RaiseErrorsChanged(nameof(CountAnswers));
                
            }
        }

        private Tag selectedTag;
        public Tag SelectedTag
        {
            get { return selectedTag; }
            set
            {
                selectedTag = value;
                RaisePropertyChanged(nameof(SelectedTag));
            }
        }


        public ICommand Valider { get; set; }
        public ICommand ValiderComment { get; set; }
        public ICommand UpdatePost { get; set; }
        public ICommand DeletePost { get; set; }
        public ICommand UpdateResponse { get; set; }
        public ICommand DeleteResponse { get; set; }
        public ICommand AcceptResponse { get; set; }
        public ICommand VoteUpPost { get; set; }
        public ICommand VoteDownPost { get; set; }
        public ICommand AffichePostsOfTag { get; set; }
        public ICommand DeletePostTag { get; set; }
        public ICommand UpdateComment { get; set; }
        public ICommand DeleteComment { get; set; }

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

        private void BtnDeleteActive()
        {
            Console.WriteLine($"Nobre de réponses: {CountAnswers}");
            if ((App.CurrentUser != null && App.CurrentUser.Equals(Post.Author) && CountAnswers == 0) ||
                         (App.CurrentUser != null && App.CurrentUser.Role == Role.Admin && CountAnswers == 0))
            {
                BtnDeletePost = "Visible";
            }
            else
            {
                BtnDeletePost = "Collapsed";
            }
        }

        private void BtnUpdateActive()
        {
            if((App.CurrentUser != null && App.CurrentUser.Equals(post.Author)) || 
                (App.CurrentUser != null && App.CurrentUser.Role == Role.Admin))
            {
                BtnUpdatePost = "Visible";
            }
            else
            {
                BtnUpdatePost = "Collapsed";
            }
        }

        private void BtnResponseActive(Post a)
        {
            if ((App.CurrentUser != null && a.Author.Equals(App.CurrentUser)) ||
                (App.CurrentUser != null && App.CurrentUser.Role == Role.Admin))
            {
                BtnAnswerActive = "Visible";
            }
            else
            {
                BtnAnswerActive = "Collapsed";
            }
        }

        private void BtnAcceptActive()
        {
            foreach (var a in Answers)
            {
                if(Post.AcceptedAnswer != null)
                {
                    if ((App.CurrentUser != null && Post.Author.Equals(App.CurrentUser) && !Post.AcceptedAnswer.Equals(a)) ||
                    App.CurrentUser != null && App.CurrentUser.Role == Role.Admin && !Post.AcceptedAnswer.Equals(a))
                    {
                        BtnAcceptAnswer = "Visible";
                    }
                    else
                    {
                        BtnAcceptAnswer = "Collapsed";
                    }
                }  
                else
                {
                    if(App.CurrentUser != null && Post.Author.Equals(App.CurrentUser) || App.CurrentUser != null && App.CurrentUser.Role == Role.Admin)
                    {
                        BtnAcceptAnswer = "Visible";
                    }
                    else
                    {
                        BtnAcceptAnswer = "Collapsed";
                    }
                }

            }
        }

        private void AcceptDisplay()
        {
            foreach (var a in Answers)
            {
                if (Post.AcceptedAnswer != null && Post.AcceptedAnswer.Equals(a))
                {
                    Accept = "Visible";
                }
                else
                {
                    Accept = "Collapsed";
                }
            }
        }

        public void SaveAction()
        {
            var user = App.CurrentUser;
            if(!editMode)
            {
                var post = App.Model.CreateAnswer(user, Post, BodyResponse);
                Post.Answers.Add(post);
            } 
            else
            {
                var post = (from p in App.Model.Posts
                            where p.PostId == editId
                            select p).FirstOrDefault();
                post.Body = BodyResponse;
            }
            App.Model.SaveChanges();
            BodyResponse = "";
            Answers = new ObservableCollection<Post>(Post.Answers);
            App.NotifyColleagues(AppMessages.MSG_ANSWER_ADDED);
            CountAnswers = Answers.Count();
        }

        private void SaveActionComment()
        {
            var user = App.CurrentUser;
            if (!editMode)
            {
                var comment = App.Model.CreateComment(user, Post, BodyComment);
                Post.Comments.Add(comment);
            } else
            {
                var comment = (from c in App.Model.Comments
                               where c.CommentId == editId
                               select c).FirstOrDefault();
                comment.Body = BodyComment;
                App.Model.SaveChanges();
            }          
            BodyComment = "";
            Comments = new ObservableCollection<Comment>(Post.Comments);
        }

        public void DeleteCommentAction(Comment comment)
        {
            App.Model.Comments.Remove(comment);
            App.Model.SaveChanges();
            Comments = new ObservableCollection<Comment>(Post.Comments);
        }
        private void AcceptAnswerAction(Post p)
        {
            if (post.AcceptedAnswer == null)
            {
                post.AcceptedAnswer = p;
            }
            else
            {
                post.AcceptedAnswer = null;
                post.AcceptedAnswer = p;
            }
            App.Model.SaveChanges();
            Post.AcceptedAnswer = p;
            Console.WriteLine("Réponse acceptée ok");
        }
        public void VoteUpAction()
        {
            Console.WriteLine($"Vote Up Score : {Post.Score} ");
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
            
            Score = Post.Score;
            Console.WriteLine($"Vote Up Score : {Score} ");
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
            Score = Post.Score;
        }

        private void DeletePostTagAction(PostTag posttag)
        {
            Console.WriteLine($"On va supprimer ce tag associé au post: {posttag.Tag.TagName} ");
            App.Model.PostTags.Remove(posttag);
            App.Model.SaveChanges();

            PostTags = new ObservableCollection<PostTag>(Post.PostTags);
            App.NotifyColleagues(AppMessages.MSG_POSTTAG_DELETED, Post);
            Console.WriteLine("Suppression du posttag ok");
        }

        public PostDetailView(Post post)
        {
            InitializeComponent();
            DataContext = this;
           
            Console.WriteLine(BtnDeletePost);

            Post = post;
            Answers = new ObservableCollection<Post>(Post.Answers);
            PostTags = new ObservableCollection<PostTag>(Post.PostTags);
            Tags = new ObservableCollection<Tag>(App.Model.Tags);
            Comments = new ObservableCollection<Comment>(Post.Comments);

            CountAnswers = Answers.Count();
            GetConnectUser();
            GetDeConnectUser();
            BtnDeleteActive();
            BtnUpdateActive();
            BtnResponseActive(post);
            BtnAcceptActive();
            AcceptDisplay();
            Console.WriteLine(SelectedTag);
          
            Valider = new RelayCommand(SaveAction, () =>
            {
                return BodyResponse != null && BodyResponse.Length != 0;              
            });

            ValiderComment = new RelayCommand(SaveActionComment, () => {
                return BodyComment != null && BodyComment.Length != 0;
            });

            VoteUpPost = new RelayCommand(VoteUpAction, () =>
            {
                return true;
            });

            VoteDownPost = new RelayCommand(VoteDownAction, () =>
            {
                return true;
            });

            DeletePostTag = new RelayCommand<PostTag>(DeletePostTagAction, posttag =>
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

            UpdateResponse = new RelayCommand<Post>(Post =>
            {
                BodyResponse = Post.Body;
                editId = Post.PostId;
                editMode = true;
                Console.WriteLine(BodyResponse);
            });

            UpdateComment = new RelayCommand<Comment>(comment =>
            {
                BodyComment = comment.Body;
                editId = comment.CommentId;
                editMode = true;

            });

            DeleteResponse = new RelayCommand<Post>(Post => 
            {
                App.NotifyColleagues(AppMessages.MSG_ANSWER_DELETE, Post);
            });

            DeleteComment = new RelayCommand<Comment>(DeleteCommentAction, comment =>
            {
                return true;
            }); 

            AcceptResponse = new RelayCommand<Post>(AcceptAnswerAction, p =>
            {
                return true;
            });

            AffichePostsOfTag = new RelayCommand<PostTag>(PostTag =>
            {
                App.NotifyColleagues(AppMessages.MSG_DISPLAY_POSTOFTAG, PostTag.Tag);
            });

            //AcceptResponse = new RelayCommand<Post>(p => {
            //    if(post.AcceptedAnswer == null)
            //    {
            //        post.AcceptedAnswer = p;
            //    }
            //    else
            //    {
            //        post.AcceptedAnswer = null;
            //        post.AcceptedAnswer = p;
            //    }
            //    App.Model.SaveChanges();
            //    Console.WriteLine("Réponse acceptée ok");
            //});

            App.Register<Post>(this, AppMessages.MSG_QUESTION_CHANGED, p => {
                Post = post;
            });

            App.Register<Post>(this, AppMessages.MSG_QUESTION_DELETED, p =>
            {
                post = null;
                App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
            });

            App.Register<Post>(this, AppMessages.MSG_ANSWER_DELETED, p =>
            {
                Answers = new ObservableCollection<Post>(Post.Answers);
                CountAnswers = Answers.Count();
            });

            
        }

       
    }
}
