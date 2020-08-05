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

        private void getPostByTag()
        {
            foreach (var postag in PosttTags)
            {
                //if (postag.Tag.TagName == Ttag.TagName)
                //{
                //    Console.WriteLine($"Le tag dontenu dans le posttag: {postag.Tag.TagName}");
                //    Console.WriteLine($"Le post contenu dans le posttag est :{postag.Post.Title}");
                //    Posts.Add(postag.Post);
                //}
                //posts.Add(postag.Post);
                var post = (from p in App.Model.Posts
                            where p.PostId == postag.PostId
                            select p).FirstOrDefault(); 
                if(post != null)
                {
                    Posts.Add(post);
                }
                
            }
        }
        public PostOfTagView(Tag tag)
        {
            DataContext = this;
            InitializeComponent();
            
            //Console.WriteLine(Tag.ToString());
            // Ttag = tag;
            Console.WriteLine(tag.TagName);
            Ttag = tag;
            Console.WriteLine(Ttag.ToString());
            PosttTags = new ObservableCollection<PostTag>(Ttag.PostTags);
            getPostByTag();
        }
    }
}
