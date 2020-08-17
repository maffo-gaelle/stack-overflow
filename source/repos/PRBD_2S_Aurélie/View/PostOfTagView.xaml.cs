using System.Windows.Controls;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PRBD_2S_Aurélie.View
{
    public partial class PostOfTagView : UserControlBase
    {
        private Tag tag;
        public Tag Ttag
        {
            get => tag;
            set
            {
                tag = value;
                RaisePropertyChanged(nameof(Ttag));
            }
        }

        private ObservableCollection<PostTag> posttTags;
        public ObservableCollection<PostTag> PosttTags
        {
            get { return posttTags; }
            set
            {
                posttTags = value;
                RaisePropertyChanged(nameof(PosttTags));
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


        public ICommand ShowPost { get; set; }
        public ICommand AffichePostsTag { get; set; }
        public PostOfTagView(Tag tag)
        {
           //MEttre initialize component tjrs en premier
            InitializeComponent();
            DataContext = this;

            Ttag = tag;
            Posts = new ObservableCollection<Post>(Ttag.Posts);
            Console.WriteLine($"Le tag ici est {Ttag.TagName}");

            ShowPost = new RelayCommand<Post>(Post =>
            {
                Console.WriteLine("Post sélectionné");
                App.NotifyColleagues(AppMessages.MSG_DETAILS_POST, Post);
            });

            AffichePostsTag = new RelayCommand<Tag>(t =>
            {
                Console.WriteLine("tag sélectionné");
                App.NotifyColleagues(AppMessages.MSG_DISPLAY_POSTOFTAG, t);
            });

            App.Register<Post>(this, AppMessages.MSG_POSTTAG_DELETED, post => {
                Posts = new ObservableCollection<Post>(Ttag.Posts);
            });

            App.Register<Post>(this, AppMessages.MSG_POSTTAG_ADDED, post => {
                Posts = new ObservableCollection<Post>(Ttag.Posts);
            });
        }
    }
}
