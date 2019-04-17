using SevenDB;
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        RepositoryMember rm;

        public Login()
        {
            rm = new RepositoryMember();

            InitializeComponent();

            TbMail.Focus();
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.currentMember = rm.GetMemberByIdentifiers(TbMail.Text, SevenLib.Helpers.HashHelper.HashString(TbPassword.Password));

            if (MainWindow.currentMember == null)
            {
                TbResponse.Text = "The Login or Password is invalid !";
                TbPassword.Password = String.Empty;
            }
            else
                this.Close();
        }

        private void TbRegister_Clicked(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();

            registerWindow.ShowDialog();

            this.DialogResult = registerWindow.DialogResult;
        }

        private void TbForgotPassword_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
