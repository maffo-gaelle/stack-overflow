﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour AskQuestionView.xaml
    /// </summary>
    public partial class AskQuestionView : UserControlBase
    {
        private Post Post { get; set; }

        private bool isNew;
        public bool IsNew
        {
            get => isNew;
            set
            {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew));
            }
        }

        private bool isQuestion;
        public bool IsQuestion
        {
            get => isQuestion;
            set
            {
                isQuestion = value;
                RaisePropertyChanged(nameof(IsQuestion));
            }
        }
        
        public string Title
        {
            get => Post.Title;
            set
            {
                Post.Title = value;
                RaisePropertyChanged(nameof(Title));
                ValidateTitle();

            }
        }

        public string Body
        {
            get => Post.Body;
            set
            {
                Post.Body = value;
                RaisePropertyChanged(nameof(Body));
                ValidateBody();

            }
        }

        public ICommand Annuler { get; set; }
        public ICommand Valider { get; set; }

        public AskQuestionView(Post post, bool isNew, bool isQuestion)
        {
            InitializeComponent();
            DataContext = this;
            Post = post;
            IsNew = isNew;
            IsQuestion = isQuestion;

            Valider = new RelayCommand(
                SaveAction, CanSaveOrCancelAction
            );
        }


        public bool ValidateTitle()
        {
            ClearErrors();

            if (string.IsNullOrEmpty(Title))
            {
                AddError("Title", Properties.Resources.Error_Required);
            }
            RaiseErrors();

            return !HasErrors;
        }

        public bool ValidateBody()
        {
            ClearErrors();
            if (string.IsNullOrEmpty(Body))
            {
                AddError("Body", Properties.Resources.Error_Required);
            }
            RaiseErrors();

            return !HasErrors;
        }

        private void SaveAction()
        {
           
            var user = App.CurrentUser;
            
            Post.Author = user;
            //Post.Body = Body;
            //Post.Title = Title;
            App.Model.Posts.Add(Post);

            App.Model.SaveChanges();
            //App.NotifyColleagues(AppMessages.MSG_NEW_QUESTION);
            App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
        }

        private bool CanSaveOrCancelAction()
        {
            if(IsNew)
            {
                //doit renvoyer true
                return ValidateTitle() && ValidateBody();
            }
            var change = (from c in App.Model.ChangeTracker.Entries<Post>()
                          where c.Entity == Post
                          select c).FirstOrDefault();
            return change != null && change.State != EntityState.Unchanged;
        }
    }
}

