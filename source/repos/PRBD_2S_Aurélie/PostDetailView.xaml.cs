using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour PostDetailView.xaml
    /// </summary>
    public partial class PostDetailView : UserControlBase
    {
        public Post Post { get; set; }

        public string Title
        {
            get { return Post.Title.ToUpper(); }
            set
            {
                Post.Title = value;
                RaiseErrorsChanged(nameof(Title));
               // App.NotifyColleagues(AppMessages.MSG_TITLE_POST);
            }
        }
        public string Body 
        {
            get { return Post.Body; }
            set
            {
                Post.Body = value;
                RaiseErrorsChanged(nameof(Body));
                //App.NotifyColleagues(AppMessages.MSG_BODY_POST);
            }
        }

        public DateTime Timestamp
        {
            get => Post.Timestamp;
            set
            {
                Post.Timestamp = value;
                RaisePropertyChanged(nameof(Timestamp));
                App.NotifyColleagues(AppMessages.MSG_TITLE_POST);
            }
        }

        public User Author
        {
            get => Post.Author;
            set
            {
                Post.Author = value;
                RaiseErrorsChanged(nameof(Author));
                App.NotifyColleagues(AppMessages.MSG_AUTHOR_POST);
            }
        }

        public ICollection<Post> Answers
        {
            get => Post.Answers;
            set
            {
                Post.Answers = value;
                RaiseErrorsChanged(nameof(Answers));
                App.NotifyColleagues(AppMessages.MSG_ANSWERS_POST);
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

        public ICommand Valider { get; set; }

        public void SaveAction()
        {
            var user = App.CurrentUser;
            var post = App.Model.CreateAnswer(user, Post, BodyResponse);
            Post.Answers.Add(post);
            Console.WriteLine(post);
            App.Model.SaveChanges();

            Answers = new ObservableCollection<Post>(Post.Answers);
        }

        public PostDetailView(Post post)
        {
            InitializeComponent();
            DataContext = this;
            Post = post;
            Answers = new ObservableCollection<Post>(Post.Answers);
            Valider = new RelayCommand(SaveAction, () =>
            {
                return true;
            });
        }
    }
}
