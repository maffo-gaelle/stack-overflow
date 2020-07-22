using System;
using System.Linq;
using System.Collections.ObjectModel;
using PRBD_Framework;
using System.Windows;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour ListPosts.xaml
    /// </summary>
    public partial class ListPosts : UserControlBase
    {
        public ICommand PostShow { get; set; }
        public string AuthorPost { get; set; }
       
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
        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                ApplyFilterAction();
                RaisePropertyChanged(nameof(Filter));
            }
        }

        
        public ListPosts()
        {
           
            InitializeComponent();
            DataContext = this;
            Posts = new ObservableCollection<Post>(App.Model.Posts);
             Console.WriteLine("hello");
         
        }

       
        private void ApplyFilterAction()
        {
            Console.WriteLine("Search clicked! " + Filter);
            var model = new Model();
            var query = from p in model.Posts
                        where p.Body.Contains(Filter) || p.Title.Contains(Filter)
                        select p;
            Posts = new ObservableCollection<Post>(query);
            Console.WriteLine($"{query.Count()} Posts trouvés");
        }

    }
}
