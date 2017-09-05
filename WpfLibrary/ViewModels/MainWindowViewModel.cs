using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfLibrary.Commands;

namespace WpfLibrary.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private MainWindow mainWindow;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            using (LibraryEntities context = new LibraryEntities())
            {
                Books = context.vwBooks.ToList();
            }
        }

        private List<vwBook> books;
        public List<vwBook> Books
        {
            get { return books; }
            set
            {
                books = value;
                OnProperyChanged("Books");
            }
        }

        private ICommand addBook;
        public ICommand AddBook
        {
            get
            {
                if (addBook == null)
                {
                    addBook = new RelayCommand(parm => AddNewBookExecute(), parm => CanAddNewBookExecute());
                }
                return addBook;

            }
        }

        private bool CanAddNewBookExecute()
        {
            return true;
        }

        private void AddNewBookExecute()
        {
            try
            {
                AddBook addBook = new AddBook();
                addBook.ShowDialog();
                if ((addBook.DataContext as AddBookViewModel).IsUpdate == true)
                {
                    using (LibraryEntities context = new LibraryEntities())
                    {
                        Books = context.vwBooks.ToList();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
        }


        private ICommand allCustomer;
        public ICommand AllCustomer
        {
            get
            {
                if (allCustomer == null)
                {
                    allCustomer = new RelayCommand(parm => AllCustomerExecute());
                }
                return allCustomer;
            }
        }

        private void AllCustomerExecute()
        {
            try
            {
                AllCustomer customer = new AllCustomer();
                customer.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }



        }


        private ICommand addCustomer;
        public ICommand AddCustomer
        {
            get
            {
                if (addCustomer == null)
                {
                    addCustomer = new RelayCommand(parm => AddCustomerExecute());
                }
                return addCustomer;
            }
        }

        private void AddCustomerExecute()
        {
            AddCustomer newCustomer = new AddCustomer();
            newCustomer.ShowDialog();
        }

    }
}
