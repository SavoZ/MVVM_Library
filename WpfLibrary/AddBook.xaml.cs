using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using WpfLibrary.ViewModels;


namespace WpfLibrary
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook :MetroWindow
    {
        private AddBook addBook;

        public AddBook()
        {
            InitializeComponent();
            this.DataContext = new AddBookViewModel(this);
        }

    }
}
