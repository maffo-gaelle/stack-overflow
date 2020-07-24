using PRBD_Framework;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour TagsView.xaml
    /// </summary>
    public partial class TagsView : UserControlBase
    {
        public ICommand NewTag { get; set; }
        private ObservableCollection<Tag> tags;
        public ObservableCollection<Tag> Tags
        {
            get { return tags; }
            set
            {
                tags = value;
                RaiseErrorsChanged(nameof(Tags));
            }
        }

        private string tagName;
        public string TagName
        {
            get => tagName;
            set => SetProperty<string>(ref tagName, value, () => ValidateTag());
        }
        public TagsView()
        {
            DataContext = this;
            Tags = new ObservableCollection<Tag>(App.Model.Tags);

            NewTag = new RelayCommand(AddTagAction, () =>
            {
                return true;
            });
            InitializeComponent();
        }

        public bool ValidateTag()
        {
            ClearErrors();

            var tagName = (from t in App.Model.Tags
                        where TagName.Equals(t.TagName)
                        select t).FirstOrDefault();
            if (string.IsNullOrEmpty(TagName))
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
                var tag = App.Model.CreateTag(TagName);
                Console.WriteLine(tag);
                App.Model.SaveChanges();
                //Application.Current.MainWindow = TagsView;
            }

        }

    }
}
