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
        public User user = null;

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

        private int scorePost;
        public int ScorePost
        {
            get => scorePost;
            set
            {
                scorePost = value;
                RaisePropertyChanged(nameof(ScorePost));
            }
        }

        private Post acceptedAnswer;
        public Post AcceptedAnswer
        {
            get => Post.AcceptedAnswer;
            set
            {
                acceptedAnswer = value;
                RaisePropertyChanged(nameof(AcceptedAnswer));
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

        private ICollection<Comment> commentsQuestion;
        public ICollection<Comment> CommentsQuestion
        {
            get => commentsQuestion;
            set
            {
                commentsQuestion = value;
                RaisePropertyChanged(nameof(CommentsQuestion));
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

        private string postTagExist;
        public string PostTagExist
        {
            get { return postTagExist; }
            set
            {
                postTagExist = value;
                RaisePropertyChanged(nameof(PostTagExist));
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
        public ICommand UpdateCommentAnswer { get; set; }
        public ICommand DeleteComment { get; set; }
        public ICommand DeleteCommentAnswer { get; set; }
        public ICommand ValiderCommentAnswer { get; set; }
        public ICommand VoteUpAnswer { get; set; }
        public ICommand VoteDownAnswer { get; set; }

        /// //////////////////////////////////////FONCTIONS POUR LA VISIBILIT2 DES BOUTONS////////////////////////////////

        private void GetUser()
        {
            if (App.CurrentUser != null)
            {
                user = App.CurrentUser;
            }
        }

        private void GetConnectUser()
        {
            Console.WriteLine("hello");
            if (user != null)
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
            if (user != null)
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
            if ((user != null && App.CurrentUser.Equals(Post.Author) && CountAnswers == 0 && Post.Comments.Count() == 0) ||
                         (user != null && App.CurrentUser.Role == Role.Admin && CountAnswers == 0 && Post.Comments.Count() == 0))
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
            if ((user != null && App.CurrentUser.Equals(post.Author)) ||
                (user != null && App.CurrentUser.Role == Role.Admin))
            {
                BtnUpdatePost = "Visible";
            }
            else
            {
                BtnUpdatePost = "Collapsed";
            }
        }


        private void BtnCancelacceptAnswerActive()
        {
            if (user != null)
            {
                if (Post.AcceptedAnswer != null && (Post.Author.Equals(user) || user.Role == Role.Admin))
                {
                    BtnCancelacceptAnswer = "Visible";
                }
                else
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
            if (user != null && Post.Author.Equals(App.CurrentUser) || user != null && user.Role == Role.Admin)
            {
                BtnDeletePostTag = "Visible";
            } else
            {
                BtnDeletePostTag = "Collapsed";
            }
        }

        private void BtnAddPostTagActive()
        {
            if ((user != null && Post.Author.Equals(user) && PostTags.Count < 3) || (user != null && user.Role == Role.Admin && PostTags.Count < 3))
            {
                BtnAddPostTag = "Visible";
            }
            else
            {
                BtnAddPostTag = "Hidden";
            }
        }

        private void PostTagExistActive()
        {
            if (SelectedTag != null)
            {
                if (Post.Tags.Contains(SelectedTag))
                {
                    PostTagExist = "Visible";
                } else
                {
                    PostTagExist = "Collapsed";
                }
            } else
            {
                PostTagExist = "Collapsed";
            }
        }
        /// /////////////////////////////////////////FONCTION DE TYPE ACTION//////////////////////////////////

        private void CountAnswersAction()
        {
            CountAnswers = Answers.Count();
        }
        public void SaveAction()
        {
            if (!editMode)
            {
                var post = App.Model.CreateAnswer(user, Post, BodyResponse);
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
            CountAnswersAction();
            App.NotifyColleagues(AppMessages.MSG_ANSWER_ADDED);
            CountAnswers = Answers.Count();
        }

        private void SaveActionComment()
        {
            if (!editMode)
            {
                var comment = App.Model.CreateComment(user, Post, BodyComment);

            } else
            {
                var comment = (from c in App.Model.Comments
                               where c.CommentId == editId
                               select c).FirstOrDefault();
                comment.Body = BodyComment;
                editMode = false;
                App.Model.SaveChanges();
            }

            var thisPost = (from p in App.Model.Posts where p.PostId.Equals(Post.PostId) select p).FirstOrDefault();
            Post = thisPost;
            CommentsQuestion = new ObservableCollection<Comment>(thisPost.Comments);
            BodyComment = "";
            BtnDeletePostActive();
            //Post.Comments;
            //CommentsQuestion = new ObservableCollection<Comment>(Post.Comments);
        }

        private void SaveActionCommentAnswer(Post post)
        {
            if (!editMode)
            {
                var comment = App.Model.CreateComment(user, post, BodyCommentAnswer);
                var thisPost = (from p in App.Model.Posts where p.PostId.Equals(Post.PostId) select p).FirstOrDefault();
                Post = thisPost;
                Answers = new ObservableCollection<Post>(Post.Answers);
                BodyCommentAnswer = "";
            }
            else
            {
                var comment = (from c in App.Model.Comments
                               where c.CommentId == editId
                               select c).FirstOrDefault();
                comment.Body = BodyCommentAnswer;
                editMode = false;
                App.Model.SaveChanges();
                BodyCommentAnswer = "";
                Answers = new ObservableCollection<Post>(Post.Answers);
            }

        }

        public void DeleteCommentAction(Comment comment)
        {
            App.Model.Comments.Remove(comment);
            App.Model.SaveChanges();
            //Comments = Post.Comments;
            CommentsQuestion = new ObservableCollection<Comment>(Post.Comments);
            BtnDeletePostActive();
        }

        private void AcceptAnswerAction(Post p)
        {

            Post.AcceptedAnswer = p;

            App.Model.SaveChanges();
            AcceptedAnswer = p;
            Answers = new ObservableCollection<Post>(Post.Answers);
            Console.WriteLine("Réponse acceptée ok");
        }

        public void VoteUpAction()
        {
            Console.WriteLine($"Vote Up Score : {Post.Score} ");
            Vote vote = (from v in App.Model.Votes
                         where v.PostId == Post.PostId && v.UserId == user.UserId
                         select v).FirstOrDefault();
            if (vote == null)
            {
                App.Model.CreateVote(user, Post, 1);
            } else
            {
                if (vote.UpDown == -1)
                {
                    App.Model.Votes.Remove(vote);
                    App.Model.SaveChanges();
                }
            }

            ScorePost = Post.Score;
            App.NotifyColleagues(AppMessages.MSG_VOTE_CHANGED, Post);
            Console.WriteLine($"Vote Up Score : {ScorePost} ");
        }

        public void VoteDownAction()
        {
            Console.WriteLine("Vote Down");
            Vote vote = (from v in App.Model.Votes
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
            ScorePost = Post.Score;
            App.NotifyColleagues(AppMessages.MSG_VOTE_CHANGED, Post);
            Console.WriteLine($"Vote Down Score : {ScorePost} ");
        }

        private void DeletePostTagAction(PostTag posttag)
        {
            Console.WriteLine($"On va supprimer ce tag associé au post: {posttag.Tag.TagName} ");
            App.Model.PostTags.Remove(posttag);
            Post.PostTags.Remove(posttag);
            App.Model.SaveChanges();

            PostTags = new ObservableCollection<PostTag>(Post.PostTags);
            BtnAddPostTagActive();
            App.NotifyColleagues(AppMessages.MSG_POSTTAG_DELETED, Post);
            Console.WriteLine("Suppression du posttag ok");
        }

        private void AddPostTag()
        {
            Console.WriteLine(SelectedTag);

            if (!Post.Tags.Contains(SelectedTag) && PostTags.Count() < 3)
            {
                var posttag = App.Model.CreatePostTag(SelectedTag, Post);
                App.Model.PostTags.Add(posttag);
                App.Model.SaveChanges();

                PostTags = new ObservableCollection<PostTag>(Post.PostTags);
                BtnAddPostTagActive();
                App.NotifyColleagues(AppMessages.MSG_POSTTAG_ADDED, Post);
            }

        }

        private void UpdateResponseAction(Post post)
        {
            BodyResponse = post.Body;
            editId = post.PostId;
            editMode = true;
            Console.WriteLine(BodyResponse);
        }

        private void DeleteResponseAction(Post post)
        {
            App.NotifyColleagues(AppMessages.MSG_ANSWER_DELETE, post);
        }

        private void UpdateCommentAction(Comment comment) 
        {
            BodyComment = comment.Body;
            editId = comment.CommentId;
            editMode = true;
        }

        private void UpdateCommentAnswerAction(Comment comment)
        {
            BodyCommentAnswer = comment.Body;
            editId = comment.CommentId;
            editMode = true;
        }

        private void DeleteCommentAnswerAction(Comment comment)
        {
            App.Model.Comments.Remove(comment);
            App.Model.SaveChanges();
            Console.WriteLine("commentaire supprimé");
            Answers = new ObservableCollection<Post>(Post.Answers);
        }

        private void VoteUpAnswerAction(Post post)
        {
            Vote vote = (from v in App.Model.Votes
                         where v.PostId == post.PostId && v.UserId == user.UserId
                         select v).FirstOrDefault();
            if (vote == null)
            {
                App.Model.CreateVote(user, post, 1);
            }
            else
            {
                if (vote.UpDown == -1)
                {
                    App.Model.Votes.Remove(vote);
                    App.Model.SaveChanges();
                }
            }
            Answers = new ObservableCollection<Post>(Post.Answers);
            Console.WriteLine($"Vote Up Answer :  ok");
        }

        private void VoteDownAnswerAction(Post post)
        {
            Vote vote = (from v in App.Model.Votes
                         where v.PostId == post.PostId && v.UserId == user.UserId
                         select v).FirstOrDefault();
            if (vote == null)
            {
                App.Model.CreateVote(user, post, -1);
            }
            else
            {
                if (vote.UpDown == 1)
                {
                    App.Model.Votes.Remove(vote);
                    App.Model.SaveChanges();
                }
            }
            Answers = new ObservableCollection<Post>(Post.Answers);
            Console.WriteLine($"Vote Down Answer : ok");
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
            CommentsQuestion = new ObservableCollection<Comment>(Post.Comments);
            if(Post.Votes != null)
            {
                foreach(var v in Post.Votes)
                {
                    Console.WriteLine(v);
                }
            }

            GetUser();
            GetConnectUser();
            GetDeConnectUser();
            BtnDeletePostActive();
            BtnUpdatePostActive();
            PostTagExistActive();
            BtnDeletePostTagActive();
            BtnAddPostTagActive();
            BtnCancelacceptAnswerActive();
            CountAnswersAction();

            ScorePost = Post.Score;

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

            UpdateResponse = new RelayCommand<Post>(UpdateResponseAction, P =>
            {
                if (P != null && P.Author != null)
                {
                    //j'ai une exception ici quand je supprime une question
                    return user != null && (P.Author.UserId.Equals(user.UserId) || user.Role == Role.Admin);
                }

                return false;
            });

            UpdateComment = new RelayCommand<Comment>(UpdateCommentAction, comment =>
            {
                if (user != null && comment != null && (user.Role == Role.Admin || user.Equals(comment.User)))
                {
                    return true;
                }
                return false;
            });

            UpdateCommentAnswer = new RelayCommand<Comment>(UpdateCommentAnswerAction, comment =>
            {
                if(user != null && comment != null && (user.Role == Role.Admin || user.Equals(comment.User))) {
                    return true;
                }
                return false;
            });

            DeleteResponse = new RelayCommand<Post>(DeleteResponseAction, P => 
            {
                if (P != null && P.Author != null && user != null && P.Comments.Count() == 0 && (P.Author.UserId.Equals(user.UserId) || user.Role == Role.Admin))
                {
                    return true;
                }

                return false;
            });

            DeleteComment = new RelayCommand<Comment>(DeleteCommentAction, comment =>
            {
                if (user != null && comment != null && (user.Role == Role.Admin || user.Equals(comment.User)))
                {
                    return true;
                }
                return false;
            });

            DeleteCommentAnswer = new RelayCommand<Comment>(DeleteCommentAnswerAction, comment =>
            {
                if (user != null && comment != null && (user.Role == Role.Admin || user.Equals(comment.User)))
                {
                    return true;
                }
                return false;
            });

            AcceptResponse = new RelayCommand<Post>(AcceptAnswerAction, p =>
            {
                if (p != null && user != null)
                {
                    if (Post.AcceptedAnswer != null)
                    {
                        return Post.AcceptedAnswer.PostId != p.PostId
                            && (user.Role == Role.Admin || user.Equals(Post.Author));
                    }
                    else
                    {
                        return (user.Role == Role.Admin || user.Equals(Post.Author));
                    }

                }

                return false;
            });

            CancelAcceptResponse = new RelayCommand(() =>
            {
                Post.AcceptedAnswer = null;
                App.Model.SaveChanges();
                
                AcceptedAnswer = Post.AcceptedAnswer;
                Answers = new ObservableCollection<Post>(Post.Answers);
            });

            ValiderCommentAnswer = new RelayCommand<Post>(SaveActionCommentAnswer, p =>
            {
                //return BodyCommentAnswer != null && BodyCommentAnswer.Length != 0;
                return true;
            });

            VoteUpAnswer = new RelayCommand<Post>(VoteUpAnswerAction, p =>
            {
                return true;
            });

            VoteDownAnswer = new RelayCommand<Post>(VoteDownAnswerAction, p =>
            {
                return true;
            });

            AffichePostsOfTag = new RelayCommand<PostTag>(PostTag =>
            {
                App.NotifyColleagues(AppMessages.MSG_DISPLAY_POSTOFTAG, PostTag.Tag);
            });

            App.Register<Post>(this, AppMessages.MSG_QUESTION_CHANGED, p => {
                var thisPost = (from pp in App.Model.Posts where pp.PostId.Equals(p.PostId) select pp).FirstOrDefault();
                Post = thisPost;
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

            App.Register(this, AppMessages.MSG_NOT_CURRENT, () =>
            {
                App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
            });

        }

    }
}
