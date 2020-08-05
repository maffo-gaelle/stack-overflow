using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    
    public partial class DeletePostView : UserControlBase
    {
        public Post Post { get; set; }
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
        public ICommand DeleteQuestion { get; set; }
        public ICommand CancelDelete { get; set; }

        public void DeleteQuestionAction()
        {
            if (Post.Parent == null)
            {
                if(Post.PostTags.Count() != 0)
                {
                    foreach (var posttag in PostTags)
                    {
                        App.Model.PostTags.Remove(posttag);
                        App.Model.SaveChanges();
                    }
                }
                App.Model.Posts.Remove(Post);
                App.Model.SaveChanges();
                App.NotifyColleagues(AppMessages.MSG_QUESTION_DELETED, Post);
                App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
            }
            else
            {
                App.Model.Posts.Remove(Post);
                App.Model.SaveChanges();
                App.NotifyColleagues(AppMessages.MSG_ANSWER_DELETED, Post);
                App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
            }
        }
        public DeletePostView(Post post)
        {
            InitializeComponent();
            DataContext = this;

            Post = post;
            PostTags = new ObservableCollection<PostTag>(Post.PostTags);
            
            DeleteQuestion = new RelayCommand(DeleteQuestionAction, () =>
            {
                return true;
            });
        }
    }
}
