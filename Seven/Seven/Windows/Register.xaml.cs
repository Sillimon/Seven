using SevenDB;
using SevenLib;
using System;
using System.Collections.Generic;
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

namespace Seven
{
    /// <summary>
    /// Logique d'interaction pour Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        #region METHODS

        public bool CheckEmptyFields()
        {
            if (String.IsNullOrEmpty(TbFirstName.Text))
                return false;

            if (String.IsNullOrEmpty(TbLastName.Text))
                return false;

            if (String.IsNullOrEmpty(TbMail.Text))
                return false;

            if (String.IsNullOrEmpty(TbPassword.Password))
                return false;

            if (String.IsNullOrEmpty(TbConfirmPassword.Password))
                return false;

            return true;
        }
        
        public bool CheckPasswordMatch()
        {
            return String.Compare(TbPassword.Password, TbConfirmPassword.Password) == 0;
        }

        #endregion

        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEmptyFields())
            {
                TbResponse.Text = "Please fill up all the non-optional fields.";
                TbPassword.Password = String.Empty;
                TbConfirmPassword.Password = String.Empty;
                return;
            }

            if(!CheckPasswordMatch())
            {
                TbResponse.Text = "The passwords do not match";
                TbPassword.Password = String.Empty;
                TbConfirmPassword.Password = String.Empty;
                return;
            }

            Member registerMember = new Member(TbLastName.Text,
                TbFirstName.Text,
                TbMail.Text,
                TbPassword.Password,
                false,
                String.IsNullOrEmpty(TbTel.Text) ? String.Empty : TbTel.Text);

            if(new RepositoryMember().AddMember(registerMember))
            {
                MainWindow.currentMember = registerMember;
                this.DialogResult = true;
            }

            this.Close();
        }
    }
}
