using PRBD_Framework;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        public TagsView()
        {
            DataContext = this;
            Tags = new ObservableCollection<Tag>(App.Model.Tags);

            NewTag = new RelayCommand(() =>
            {
                Console.WriteLine("page addTag ok");
            });
            InitializeComponent();
        }


    }
}
