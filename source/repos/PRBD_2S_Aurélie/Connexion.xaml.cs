using PRBD_Framework;
using PRBD_2S_Aurélie.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace PRBD_2S_Aurélie
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : WindowBase
    {
        public ICommand Login { get; set; }
        public ICommand Annuler { get; set; }
        public ICommand Inscription { get; set; }
        public string password;
        public string Password
        {
            get => password;
            set => SetProperty<string>(ref password, value, () => ValidatePassword());
        }
        public string username;
        public string UserName
        {
            get => username;
            set => SetProperty<string>(ref username, value, () => ValidateUserName());
        }
        public Connexion()
        {
            InitializeComponent();
            //ceci pour ne plus mettre le ElementName = window; c'est pour dire que on travaille ds ce contexte-ci
            DataContext = this;

            Login = new RelayCommand(Login_Action, () => {
                return username != null && password != null && !HasErrors;
            });

            Annuler = new RelayCommand(() =>
            {
                var listePost = new ListePost();
                listePost.Show();
                Application.Current.MainWindow = listePost;
                Close();
            });
        }

        public void Login_Action()
        {
            if(ValidateUserName() && ValidatePassword())
            {
                var user = (from u in App.Model.Users
                            where UserName.Equals(u.UserName)
                            select u).FirstOrDefault();
                App.CurrentUser = user;

                var listePost = new ListePost();
                listePost.Show();
                Application.Current.MainWindow = listePost;
                Close();
            }
        }

        public bool ValidateUserName()
        {
            ClearErrors();
            var user = (from u in App.Model.Users
                        where UserName.Equals(u.UserName)
                        select u).FirstOrDefault();

            if(string.IsNullOrEmpty(UserName))
            {
                AddError("UserName", Properties.Resources.Error_Required);
            } else
            {
                if(UserName.Length < 4)
                {
                    AddError("UserName", Properties.Resources.Error_MinLength);
                }
                else if(user == null)
                {
                    AddError("UserName", Properties.Resources.Error_NotExist);
                }
            }
            RaiseErrors();

            return !HasErrors;
        }

        public bool ValidatePassword()
        {
            ClearErrors();
            var password = (from u in App.Model.Users
                        where UserName.Equals(u.UserName)
                        select u.Password).FirstOrDefault();

            if (string.IsNullOrEmpty(Password))
            {
                AddError("Password", Properties.Resources.Error_Required);
            }
            else
            { 
                if (Password.Length < 8)
                {
                    AddError("Password", Properties.Resources.Error_MinLengthPassword);
                }
                else if (password != Password )
                {
                    AddError("Password", Properties.Resources.Error_BadPassword);
                }
            }
            RaiseErrors();

            return !HasErrors;
        }
    }
}
