using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
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
            DataContext = this;

            Login = new RelayCommand(ConnexionAction, () => {
                return true;
            });

            Inscription = new RelayCommand(InscriptionAction, () => {
                return true;
            });

            Annuler = new RelayCommand(AnnulerAction, () =>
            {
                return true;
            });
        }

        private void AnnulerAction()
        {
            Console.WriteLine("on est là");
            var listePost = new MainView();
            listePost.Show();
            Application.Current.MainWindow = listePost;
            Close();
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
                if (UserName.Length < 3)
                {
                    AddError("UserName", "Properties.Resources.Error_MinLength");
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

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool ValidateEmail()
        {
            ClearErrors();
            var email = (from u in App.Model.Users
                            where UserName.Equals(u.UserName)
                            select u.Email).FirstOrDefault();

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                AddError("Email", Properties.Resources.Error_Required);
            }
            else
            {
                if (!IsValidEmail(Email))
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
            if(ValidateUserName() && ValidatePassword() && ValidateFullName() && ValidateEmail())
            {
                var user = App.Model.CreateUser(UserName, Password, FullName, Email);
                Console.WriteLine(user);
                App.Model.SaveChanges();
                App.CurrentUser = user;
                var listePost = new MainView();
                listePost.Show();
                Application.Current.MainWindow = listePost;
                Close();
            }
        }
    }

}