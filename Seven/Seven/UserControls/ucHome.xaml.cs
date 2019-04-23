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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seven
{
    /// <summary>
    /// Logique d'interaction pour ucHome.xaml
    /// </summary>
    public partial class ucHome : UserControl
    {
        RepositoryParameters rp;

        public ucHome()
        {
            InitializeComponent();

            rp = new RepositoryParameters();

            RefreshRepository();
        }

        private void BtBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();

            // Get the selected file name and display in a TextBox.
            if (result == true)
            {
                SevenLib.Helpers.Const.DBPath = @"data source=" + openFileDlg.FileName;

                RefreshRepository();
            }
        }

        #region METHODS

        private void RefreshRepository()
        {
            TbDBPath.Text = SevenLib.Helpers.Const.DBPath;
            rp.ConnectionString = SevenLib.Helpers.Const.DBPath;
        }

        #endregion
    }
}
