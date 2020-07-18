using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PRBD_Framework;
using System.Text.RegularExpressions;
using System.Windows;
using System;

namespace PRBD_2S_Aurélie
{
    public partial class Signup : WindowBase
    {
        public ICommand Inscription { get; set; }
        public ICommand Annuler { get; set; }
        public ICommand Login { get; set; }
        private Role role = 0;
        private string username;
        public string UserName
        {
            get => username;
            set => SetProperty<string>(ref username, value, () => ValidateUserName());
        }
        private string password;
        public string Password
        {
            get => password;
            set => SetProperty<string>(ref password, value, () => ValidatePassword());
        }
        private string fullname;
        public string FullName
        {
            get => fullname;
            set => SetProperty<string>(ref fullname, value, () => ValidateFullName());
        }
        private string passwordConfirm;
        public string PasswordConfirm
        {
            get => passwordConfirm;
            set => SetProperty<string>(ref passwordConfirm, value, () => ValidatepasswordConfirm());
        }
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty<string>(ref email, value, () => ValidateEmail());
        }

        public Signup()
        {

            InitializeComponent();
            Login = new RelayCommand(ConnexionAction, () => {
                return true;
            });
            Inscription = new RelayCommand(InscriptionAction, () => {
                return true;
            });

            Annuler = new RelayCommand(() =>
            {
                var listePost = new ListePost();
                listePost.Show();
                Application.Current.MainWindow = listePost;
                Close();
            });
        }

        private bool ValidateUserName()
        {
            ClearErrors();
            var user = (from u in App.Model.Users
                        where UserName.Equals(u.UserName)
                        select u).FirstOrDefault();
            if (string.IsNullOrEmpty(UserName))
            {
                AddError("UserName", Properties.Resources.Error_Required);
            }
            else
            {
                if (UserName.Length < 4)
                {
                    AddError("UserName", Properties.Resources.Error_MinLength);
                }
                else if (user != null)
                {
                    AddError("UserName", Properties.Resources.Error_Exist);
                }
            }
            RaiseErrors();
            return !HasErrors;
        }

        private bool ValidatePassword()
        {
            ClearErrors();
            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
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
                else if (regex.IsMatch(Password))
                {
                    AddError("Password", Properties.Resources.Error_BadFormat);
                }
            }
            RaiseErrors();

            return !HasErrors;
        }

        private bool ValidateFullName()
        {
            ClearErrors();
            if (string.IsNullOrEmpty(FullName))
            {
                AddError("FullName", Properties.Resources.Error_Required);
            }
            RaiseErrors();

            return !HasErrors;
        }


        private object ValidatepasswordConfirm()
        {
            ClearErrors();
            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                AddError("PasswordConfirm", Properties.Resources.Error_Required);
            }
            else
            {
                if(PasswordConfirm != Password)
                {
                    AddError("PasswordConfirm", Properties.Resources.Error_PasswordConfirm);
                }
            }
            RaiseErrors();

            return !HasErrors;
        }

        private bool ValidateEmail()
        {
            ClearErrors();
            var regex = new Regex("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$");
            var email = (from u in App.Model.Users
                            where UserName.Equals(u.UserName)
                            select u.Email).FirstOrDefault();
            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                AddError("Email", Properties.Resources.Error_Required);
            }
            else
            {
                if (regex.IsMatch(Email))
                {
                    AddError("Email", Properties.Resources.Error_Email);
                }
                else if (email == Email)
                {
                    AddError("Email", Properties.Resources.Error_ExistEmail);
                }
            }
            RaiseErrors();

            return !HasErrors;
        }

        public void ConnexionAction()
        {
            Console.WriteLine("Maffo");
            var connexion = new Connexion();
            connexion.Show();
            Application.Current.MainWindow = connexion;
            Close();
        }

        public void InscriptionAction()
        {
            Console.WriteLine("Inscription");
            if(ValidateUserName() && ValidatePassword() && ValidateFullName() && ValidateEmail())
            {
                var user = App.Model.CreateUser(UserName, Password, FullName, Email, role);
                App.CurrentUser = user;
                var listePost = new ListePost();
                listePost.Show();
                Application.Current.MainWindow = listePost;
                Close();
            }
        }
    }

}