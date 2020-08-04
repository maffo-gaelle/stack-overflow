using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    //je crée une classe qui va m'aider à récuperer les tags qui ont été checkés
    public class CheckedTag
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public override string ToString()
        {
            return Name + " " + IsChecked;
        }
    }
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

        public ObservableCollection<CheckedTag> tags;
        public ObservableCollection<CheckedTag> Tags
        {
            get => tags;
            set
            {
                tags = value;
                RaisePropertyChanged(nameof(Tags));
            }
        }
        

        public ICommand Annuler { get; set; }
        public ICommand Valider { get; set; }

        public ICommand CheckTagCommand { get; set; }
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
            if(IsNew)
            {
                Post.Author = user;
                App.Model.Posts.Add(Post);
                IsNew = false;
            }
            
            App.Model.SaveChanges();
            App.NotifyColleagues(AppMessages.MSG_QUESTION_CHANGED, Post);
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

        private void CancelAction()
        {
            Console.WriteLine("J'annule l'ajout d'une question");
            if (IsNew)
            {
                Post.Title = "";
                Post.Body = "";
                RaisePropertyChanged(nameof(Post));
                App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
            }
            else
            {
                App.NotifyColleagues(AppMessages.MSG_CLOSE_TAB, this);
            }
        }

        public void GetTags()
        {
            Tags = new ObservableCollection<CheckedTag>();

            foreach(var tag in App.Model.Tags)
            {
                var checkedTag = new CheckedTag()
                {
                    Name = tag.TagName,
                    IsChecked = Post.Tags.Contains(tag)
                };

                Tags.Add(checkedTag);
            }
        }

        public AskQuestionView(Post post, bool isNew, bool isQuestion)
        {
            InitializeComponent();
            DataContext = this;
            Post = post;
            IsNew = isNew;
            IsQuestion = isQuestion;

            GetTags();
            Valider = new RelayCommand(
                SaveAction, CanSaveOrCancelAction
            );
            Annuler = new RelayCommand(
                CancelAction
           );

            CheckTagCommand = new RelayCommand<CheckedTag>(t =>
            {
                
                Console.WriteLine("Liste avant:  \n");
                foreach(var tag in Tags)
                {
                    Console.WriteLine(tag);
                }
                Console.WriteLine($"Tag sélectionné: {t} \n");
                Console.WriteLine("Liste après:  \n");
                foreach (var tag in Tags)
                {
                    Console.WriteLine(tag);
                }
            });
        }

    }
}

