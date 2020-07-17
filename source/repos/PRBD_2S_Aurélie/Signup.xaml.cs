using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using PRBD_Framework;

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
        public string Password
        {
            get => Password;
            set => SetProperty<string>(ref username, value, () => ValidatePassword());
        }

        public Signup()
        {
            
            InitializeComponent();
        }

        private bool ValidateUserName()
        {
            ClearErrors();
            var user = (from u in App.Model.Users
                        where UserName.Equals(u.UserName)
                        select u).FirstOrDefault();
            if(string.IsNullOrEmpty(UserName))
            {
                AddError("UserName", Properties.Resources.Error_Required);
            }
            else
            {
                if(UserName.Length < 4)
                {
                    AddError("UserName", Properties.Resources.Error_MinLength);
                }
                else if(user != null)
                {
                    AddError("UserName", Properties.Resources.Error_Exist);
                }   
            }
            RaiseErrors();
            return !HasErrors;
        }

        private object ValidatePassword()
        {
            ClearErrors();
            if(string.IsNullOrEmpty(Password))
            {
                AddError("Password", Properties.Resources.Error_Required);
            }
            else
            {
                if(Password.Length < 8)
                {
                    AddError("Password", Properties.Resources.Error_MinLengthPassword);
                }
                else if()
            }

            RaiseErrors();
            return !HasErrors;
        }

    }
'