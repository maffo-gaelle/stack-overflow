using PRBD_Framework;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace PRBD_2S_Aurélie
{
    public partial class TagsView : UserControlBase
    {
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

        private string name;
        public string Nom
        {
            get => name;
            set => SetProperty<string>(ref name, value, () => ValidateTag());
        }

        private Tag selectedTag;
        public Tag SelectedTag
        {
            get => selectedTag;
            set
            {
                selectedTag = value;
                RaisePropertyChanged(nameof(SelectedTag));
                Nom = selectedTag.TagName;
            }
        }

        public ICommand NewTag { get; set; }
        public ICommand DeleteTag { get; set; }
        public ICommand UpdateTag { get; set; }
        public bool ValidateTag()
        {
            ClearErrors();

            var tagName = (from t in App.Model.Tags
                        where Nom.Equals(t.TagName)
                        select t).FirstOrDefault();

            if (string.IsNullOrEmpty(Nom))
            {
                AddError("TagName", Properties.Resources.Error_Required);
            }
            else
            {
                if (tagName != null)
                {
                    AddError("UserName", Properties.Resources.Error_Exist);
                }
            }
            RaiseErrors();

            return !HasErrors;
        }
        public void AddTagAction()
        {
            if(ValidateTag())
            {
                var tag = App.Model.CreateTag(Nom);
                
                App.Model.SaveChanges();
                Console.WriteLine("liste de tags avant: \n");
                foreach (var t in Tags)
                {
                    Console.WriteLine(t + "\n");
                }
                Nom = "";
                //vue que le app.register doit se faire sur la mm page, c'est pas necessaire il faut juste observer le dbset du model pour enregistrer les changements
                Tags = new ObservableCollection<Tag>(App.Model.Tags.OrderBy(t => t.TagName)); 
                Console.WriteLine("liste de tags apès: \n");
                foreach (var t in Tags)
                {
                    Console.WriteLine(t + "\n");
                }
            }
        }

        private void DeleteTagAction()
        {
            Console.WriteLine("supprimer un tag");
            App.Model.Tags.Remove(SelectedTag);
            App.Model.SaveChanges();
            Nom = "";
            Tags = new ObservableCollection<Tag>(App.Model.Tags.OrderBy(t => t.TagName));
        }

        private void UpdateTagAction()
        {
            Console.WriteLine("Modifier un tag");
            SelectedTag.TagName = Nom;
            Nom = "";
            App.Model.SaveChanges();
            Tags = new ObservableCollection<Tag>(App.Model.Tags.OrderBy(t => t.TagName));
        }

        public TagsView()
        {
            DataContext = this;
            Tags = new ObservableCollection<Tag>(App.Model.Tags.OrderBy(tag => tag.TagName));

            DeleteTag = new RelayCommand(DeleteTagAction, () => {
                return SelectedTag != null && Nom == SelectedTag.TagName;
            });

            NewTag = new RelayCommand(AddTagAction, () =>
            {

                return Nom != null && ValidateTag();
            });

            UpdateTag = new RelayCommand(UpdateTagAction, () =>
            {
                return SelectedTag != null && Nom != null;
            });

            InitializeComponent();
        }
    }
}
