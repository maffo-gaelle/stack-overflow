﻿using System;
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
        //ma listeview binde avec les tags de type CheckedTag et je donne la valeur de la liste Tags dans la fonction getTags
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

        private ObservableCollection<PostTag> postTags;
        public ObservableCollection<PostTag> PostTags
        {
            get => postTags;
            set
            {
                postTags = value;
                RaisePropertyChanged(nameof(PostTags));
            }
        }

        private ObservableCollection<Tag> selectTags ;
        public ObservableCollection<Tag> SelectTags
        {
            get => selectTags;
            set
            {
                selectTags = value;
                RaisePropertyChanged(nameof(SelectTags));
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
                Console.WriteLine($"Nombre de tags dans selectTags: {SelectTags.Count()}");
                foreach (var tag in SelectTags)
                {
                    var posttag = App.Model.CreatePostTag(tag, Post);
                    Post.PostTags.Add(posttag);
                }
                IsNew = false;
            } else
            {
                foreach (var tag in SelectTags)
                {
                    var posttag = (from p in App.Model.PostTags
                                   where Post == Post && Tag == tag
                                   select p);
                    if(posttag != null)
                    {
                        var Newposttag = App.Model.CreatePostTag(tag, Post);
                        Post.PostTags.Add(Newposttag);
                    }
                    
                }
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
        //dans cette fonction getTags,je recopie tout mon dbset de tags dans la collection observable Tags de type CheckedTag
        public void GetTags()
        {
            Tags = new ObservableCollection<CheckedTag>();

            foreach(var tag in App.Model.Tags)
            {
                //je crée un nouveau checktag qui va contenir le name du tag qui fait partir du App.Model.Tags et la proprieté ISChecked
                var checkedTag = new CheckedTag()
                {
                    Name = tag.TagName,
                    //le Ischecked si me dit si le post est associé à ce tag; IsChecked me renvoie bien entendu un booléen
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
            PostTags = new ObservableCollection<PostTag>(Post.PostTags);
            //SelectTags = new ObservableCollection<Tag>(Post.Tags);
            GetTags();
            Valider = new RelayCommand(
                SaveAction, CanSaveOrCancelAction
            );
            Annuler = new RelayCommand(
                CancelAction
           );
             
            CheckTagCommand = new RelayCommand<CheckedTag>(t =>
            {
                Console.WriteLine("j'entre dans le checktagCommand");
                Console.WriteLine($"La valeur du t {t}");
                SelectTags = new ObservableCollection<Tag>(Post.Tags);
                var tt = (from c in App.Model.Tags
                          where t.Name.Equals(c.TagName)
                          select c).FirstOrDefault();
                Console.WriteLine(tt);
                Console.WriteLine($"Le nombre de tag dans selectTags: {SelectTags.Count()}");
                SelectTags.Add(tt);
            });
            
        }

    }
}

