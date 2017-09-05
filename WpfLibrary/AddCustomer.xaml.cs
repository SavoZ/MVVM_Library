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
using WpfLibrary.ViewModels;
using MahApps.Metro.Controls;


namespace WpfLibrary
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : MetroWindow
    {
        public AddCustomer()
        {
            InitializeComponent();
            this.DataContext = new AddCustomerViewModel(this);
        }
    }
}
