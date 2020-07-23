using PRBD_Framework;

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
            get { return Post.Title; }
            set
            {
                Post.Title = value;
                RaiseErrorsChanged(nameof(Title));
                App.NotifyColleagues(AppMessages.MSG_TITLE_POST);
            }
        }
        public string Body
        {
            get { return Post.Body; }
            set
            {
                Post.Body = value;
                RaiseErrorsChanged(nameof(Body));
                App.NotifyColleagues(AppMessages.MSG_BODY_POST);
            }
        }
        public PostDetailView(Post post)
        {
            DataContext = this;
            Post = post;
            InitializeComponent();
        }
    }
}
