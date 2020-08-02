using System;
using System.Collections.Generic;
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
        public ICommand DeleteQuestion { get; set; }
        public ICommand CancelDelete { get; set; }

        public void DeleteQuestionAction()
        {
            if (Post.Parent == null)
            {
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
            DeleteQuestion = new RelayCommand(DeleteQuestionAction, () =>
            {
                return true;
            });
        }
    }
}
