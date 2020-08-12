using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    
    public partial class PostDetailView : UserControlBase
    {
       
        /// /////////////////////PROPRIT2S SIMPLES/////////////////////////////////////////////////
      
        private bool editMode = false;
        private int editId;
        //public User user = null;

        /// ////////////////////////////PROPRIET2S AVEC ACCESSEURS////////////////////////////////////////////

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

        //private Post acceptedAnswer;
        //public Post AcceptedAnswer
        //{
        //    get => Post.AcceptedAnswer;
        //    set
        //    {
        //        acceptedAnswer = value;
        //        RaisePropertyChanged(nameof(AcceptedAnswer));
        //    }
        //}

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

        private string bodyCommentAnswer;
        public string BodyCommentAnswer
        {
            get => bodyCommentAnswer;
            set
            {
                bodyCommentAnswer = value;
                RaisePropertyChanged(nameof(BodyCommentAnswer));
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



        private Tag selectedTag;
        public Tag SelectedTag
        {
            get { return selectedTag; }
            set
            {
                selectedTag = value;
                AddPostTag();
                RaisePropertyChanged(nameof(SelectedTag));
            }
        }

        ////////////////////COLLECTIONS OBSERVABLES///////////////////////////////////////////////////////////

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

        /// /////////////////////////////////////Visibilité des boutons/////////////////////////////////////////

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

        //private string btnUpdateAnswer;
        //public string BtnUpdateAnswer
        //{
        //    get { return btnUpdateAnswer; }
        //    set
        //    {
        //        btnUpdateAnswer = value;
        //        RaisePropertyChanged(nameof(BtnUpdateAnswer));
        //    }
        //}

        //private string btnDeleteAnswer;
        //public string BtnDeleteAnswer
        //{
        //    get => btnDeleteAnswer;
        //    set
        //    {
        //        btnDeleteAnswer = value;
        //        RaisePropertyChanged(nameof(BtnDeleteAnswer));
        //    }
        //}

        //private string btnAcceptAnswer;
        //public string BtnAcceptAnswer
        //{
        //    get => btnAcceptAnswer;
        //    set
        //    {
        //        btnAcceptAnswer = value;
        //        RaisePropertyChanged(nameof(BtnAcceptAnswer));
        //    }
        //}

        //private string acceptAnswerVisible;
        //public string AcceptAnswerVisible
        //{
        //    get => acceptAnswerVisible;
        //    set
        //    {
        //        acceptAnswerVisible = value; 
        //        RaisePropertyChanged(nameof(AcceptAnswerVisible));
        //    }
        //}

        private string btnCancelacceptAnswer;
        public string BtnCancelacceptAnswer
        {
            get => btnCancelacceptAnswer;
            set
            {
                btnCancelacceptAnswer = value; 
                RaisePropertyChanged(nameof(BtnCancelacceptAnswer));
            }
        }

        private string btnDeletePostTag; 
        public string BtnDeletePostTag
        {
            get { return btnDeletePostTag; }
            set
            {
                btnDeletePostTag = value;
                RaisePropertyChanged(nameof(BtnDeletePostTag));
            }
        }

        private string btnAddPostTag; 
        public string BtnAddPostTag
        {
            get { return btnAddPostTag; }
            set
            {
                btnAddPostTag = value;
                RaisePropertyChanged(nameof(BtnAddPostTag));
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

        public ICommand Valider { get; set; }
        public ICommand ValiderComment { get; set; }
        public ICommand UpdatePost { get; set; }
        public ICommand DeletePost { get; set; }
        public ICommand UpdateResponse { get; set; }
        public ICommand DeleteResponse { get; set; }
        public ICommand AcceptResponse { get; set; }
        public ICommand CancelAcceptResponse { get; set; }
        public ICommand VoteUpPost { get; set; }
        public ICommand VoteDownPost { get; set; }
        public ICommand AffichePostsOfTag { get; set; }
        public ICommand DeletePostTag { get; set; }
        public ICommand UpdateComment { get; set; }
        public ICommand DeleteComment { get; set; }
        public ICommand ValiderCommentAnswer { get; set; }

       /// <summary>
       /// //////////////////////////////////////FONCTIONS POUR LA VISIBILIT2 DES BOUTONS////////////////////////////////
       /// </summary>
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

        private void BtnDeletePostActive()
        {
            Console.WriteLine($"Nombre de réponses: {CountAnswers}");
            Console.WriteLine($"Nombre de Commentaires: {Post.Comments.Count()}");
            if ((App.CurrentUser != null && App.CurrentUser.Equals(Post.Author) && CountAnswers == 0 && Post.Comments.Count() == 0) ||
                         (App.CurrentUser != null && App.CurrentUser.Role == Role.Admin && CountAnswers == 0 && Post.Comments.Count() == 0))
            {
                BtnDeletePost = "Visible";
            }
            else
            {
                BtnDeletePost = "Collapsed";
            }
        }

        private void BtnUpdatePostActive()
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


        //private void BtnDeleteAnswerActive()
        //{
        //    //je veux recupérer la réponse sur laquelle j'ai cliqué, pas juste
        //    if ((user != null && user.Role == Role.Admin || user != null && IfCurrentUser))
        //    {
        //        BtnDeleteAnswer = "Visible";
        //    }
        //    else
        //    {
        //        BtnDeleteAnswer = "Collapsed";
        //    }
        //}

        //private void BtnAcceptAnswerActive()
        //{
        //    if (Post.AcceptedAnswer != null)
        //    {
        //        if ((user != null &&  IfCurrentUser) || (user != null && user.Role == Role.Admin))

        //        {
        //            BtnAcceptAnswer = "Visible";
        //        }
        //        else
        //        {
        //            BtnAcceptAnswer = "Collapsed";
        //        }
        //    }
        //    else
        //    {
        //        if ((user != null && Post.Author.Equals(user)) || (user != null && user.Role == Role.Admin))
        //        {
        //            BtnAcceptAnswer = "Visible";
        //        }
        //        else
        //        {
        //            BtnAcceptAnswer = "Collapsed";
        //        }
        //    //}
        //}

        //private void AcceptDisplayActive()
        //{
        //    if(Post.AcceptedAnswer != null)
        //    {

        //    }
        //        if (Post.AcceptedAnswer != null)
        //        {
        //            if(Post.AcceptedAnswer.PostId.Equals(a.PostId))
        //            {
        //                AcceptAnswerVisible = "Visible";
        //            } else
        //            {
        //                AcceptAnswerVisible = "Collapsed";
        //            }
        //        }
        //        else
        //        {
        //            AcceptAnswerVisible = "Collapsed";
        //        }
        //}

        private void BtnCancelacceptAnswerActive()
        {
            if(App.CurrentUser != null)
            {
                if((Post.Author.Equals(App.CurrentUser) && Post.AcceptedAnswer != null) || 
                    (App.CurrentUser.Role == Role.Admin && Post.AcceptedAnswer != null))
                {
                    foreach (var a in Post.Answers)
                    {
                        if (a.Equals(Post.AcceptedAnswer))
                        {
                            BtnCancelacceptAnswer = "visible";
                        }
                        else
                        {
                            BtnCancelacceptAnswer = "Collapsed";
                        }
                    }
                } else
                {
                    BtnCancelacceptAnswer = "Collapsed";
                }
            } 
            else
            {
                BtnCancelacceptAnswer = "Collapsed";
            }
                
        }
        private void BtnDeletePostTagActive()
        {
            //var user = App.CurrentUser;
            if(App.CurrentUser != null && Post.Author.Equals(App.CurrentUser) || App.CurrentUser != null && App.CurrentUser.Role == Role.Admin)
            {
                BtnDeletePostTag = "Visible";
            } else
            {
                BtnDeletePostTag = "Collapsed";
            }
        }

        private void BtnAddPostTagActive()
        {
            //var user = App.CurrentUser;
            if (App.CurrentUser != null && Post.Author.Equals(App.CurrentUser) && Post.PostTags.Count < 3 || App.CurrentUser != null && App.CurrentUser.Role == Role.Admin && Post.PostTags.Count < 3)
            {
                BtnAddPostTag = "Visible";
            }
            else
            {
                BtnAddPostTag = "Collapsed";
            }
        }

        /// /////////////////////////////////////////FONCTION DE TYPE ACTION//////////////////////////////////

        public void SaveAction()
        {
            //var user = App.CurrentUser;
            if(!editMode)
            {
                var post = App.Model.CreateAnswer(App.CurrentUser, Post, BodyResponse);
                post.AcceptedAnswer = null;
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
            //Comments = new ObservableCollection<Comment>(Post.Comments);
        }

        public void DeleteCommentAction(Comment comment)
        {
            App.Model.Comments.Remove(comment);
            App.Model.SaveChanges();
            //Comments = new ObservableCollection<Comment>(Post.Comments);
        }
        private void AcceptAnswerAction(Post p)
        {
            
            Post.AcceptedAnswer = p;
            
            App.Model.SaveChanges();
            //AcceptedAnswer = p;
            //Answers = new ObservableCollection<Post>(Post.Answers);
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
            Console.WriteLine($"Vote Down Score : {Score} ");
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

        private void AddPostTag()
        {
            Console.WriteLine(SelectedTag);
        }

        private void UpdateResponseAction(Post post)
        {
            BodyResponse = Post.Body;
            editId = Post.PostId;
            editMode = true;
            Console.WriteLine(BodyResponse);
        }
        /// /////////////////////////////////CONSTRUCTEUR/////////////////////////////////////////////////////

        public PostDetailView(Post post)
        {
            InitializeComponent();
            DataContext = this;

            Post = post;
            Answers = new ObservableCollection<Post>(Post.Answers);
            PostTags = new ObservableCollection<PostTag>(Post.PostTags);
            Tags = new ObservableCollection<Tag>(App.Model.Tags);
            //Comments = new ObservableCollection<Comment>(Post.Comments);

            CountAnswers = Answers.Count();
            //GetUser();
            GetConnectUser();
            GetDeConnectUser();
            BtnDeletePostActive();
            BtnUpdatePostActive();
            //ça doit bindé directement sur la reponse sur laquelle j'ai cliqué et je ne sais pas comment recuperer le bouton sur leql je clique
            //BtnDeleteAnswerActive();
            //BtnUpdateAnswerActive();
            //BtnAcceptAnswerActive();
            //AcceptDisplayActive();
            BtnDeletePostTagActive();
            BtnAddPostTagActive();
            BtnCancelacceptAnswerActive();

            Valider = new RelayCommand(SaveAction, () =>
            {
                return BodyResponse != null && BodyResponse.Length != 0;              
            });

            ValiderComment = new RelayCommand(SaveActionComment, () => {
                return BodyComment != null && BodyComment.Length != 0;
            });

            //ValiderCommentAnswer = new RelayCommand<Comment>(SaveActionComment, comment =>
            //{
            //    return BodyCommentAnswer != null && BodyCommentAnswer.Length != 0;
            //});

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

            UpdateResponse = new RelayCommand<Post>(UpdateResponseAction, Post =>
            {
                //if(Post != null)
                //{

                //    return user != null && (Post.Author.UserId.Equals(user.UserId) || user.Role == Role.Admin);
                //}

                //return false;
                return true;
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
                //if (p != null && user != null)
                //{
                //    if (Post.AcceptedAnswer != null)
                //    {
                //        return Post.AcceptedAnswer.PostId != p.PostId
                //            && (user.Role == Role.Admin || user.UserId == p.Author.UserId);
                //    }
                //    else
                //    {
                //        return (user.Role == Role.Admin || user.UserId == p.Author.UserId);
                //    }

                //}

                //return false;
                return true;
            });

            CancelAcceptResponse = new RelayCommand(() =>
            {
                Post.AcceptedAnswer = null;
                App.Model.SaveChanges();
                
                //AcceptedAnswer = Post.AcceptedAnswer;
                //Answers = new ObservableCollection<Post>(Post.Answers);
            });

            AffichePostsOfTag = new RelayCommand<PostTag>(PostTag =>
            {
                App.NotifyColleagues(AppMessages.MSG_DISPLAY_POSTOFTAG, PostTag.Tag);
            });

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
