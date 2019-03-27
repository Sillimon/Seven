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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Book> repositoryBook;

        public Book selectedBook { get; set; }
        public MainWindow()
        {
            this.selectedBook = new Book("titre", DateTime.Now, SevenLib.Helpers.Genre.Comedy);
            InitializeComponent();
            
            //repositoryBook = GetDataInDB
        }
    }
}
