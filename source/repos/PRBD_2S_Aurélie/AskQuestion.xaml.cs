using System;
using System.Windows;
using System.Windows.Input;
using PRBD_Framework;

namespace PRBD_2S_Aurélie
{
    
    public partial class AskQuestion : WindowBase
    {
        public ICommand Annuler { get; set; }
        public ICommand Valider { get; set; }
        private string title;


        public string Titre
        { 
            get =>title;
            set => SetProperty<string>(ref title, value, () => ValidatePost()); 
        }

        private string body;
        public string Body { 
            get => body;
            set => SetProperty<string>(ref body, value, () => ValidatePost()); }
         public AskQuestion()
         {
            DataContext = this;
            Valider = new RelayCommand(Ask(), () => {
                return title != null && body != null && !HasErrors;
            });

            Annuler = new RelayCommand(() =>
            {
                var listePost = new ListePost();
                listePost.Show();
                Application.Current.MainWindow = listePost;
                Close();
            });
            InitializeComponent();
         }

        
        public bool ValidatePost()
        {
            ClearErrors();
            
            if (string.IsNullOrEmpty(Titre))
            {
                AddError("Title", Properties.Resources.Error_Required);
            }
             if (string.IsNullOrEmpty(Body))
            {
                AddError("Body", Properties.Resources.Error_Required);
            }
            RaiseErrors();

            return !HasErrors;
        }

        private Action Ask()
        {
            throw new NotImplementedException();
        }

    }
}
