using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfLibrary.Commands;

namespace WpfLibrary.ViewModels
{
    class CustomerViewModel : ViewModelBase
    {
        private Customer customerBook;

        public CustomerViewModel(Customer customerBook, vwCustomer customer)
        {
            this.customerBook = customerBook;
            this.customer = customer;

            if (customer.BookID == null)
            {
                ReturnB = Visibility.Hidden;
                RentB = Visibility.Visible;
            }
            else
            {
                ReturnB = Visibility.Visible;
                RentB = Visibility.Hidden;
            }
        }

        private vwCustomer customer;
        public vwCustomer Customer
        {
            get { return customer; }
            set { customer = value; OnProperyChanged("Customer"); }
        }

        private Visibility returnB = Visibility.Collapsed;
        public Visibility ReturnB
        {
            get { return returnB; }
            set { returnB = value; OnProperyChanged("ReturnBook"); }
        }

        private Visibility rentB = Visibility.Collapsed;
        public Visibility RentB
        {
            get { return rentB; }
            set { rentB = value; OnProperyChanged("RentBook"); }
        }

        private ICommand returnBook;
        public ICommand ReturnBook
        {
            get
            {
                if (returnBook == null)
                {
                    returnBook = new RelayCommand(parm => ReturnBookExecute());
                }
                return returnBook;

            }
        }

        public bool IsUpdate { get; set; }

        private void ReturnBookExecute()
        {
            using (LibraryEntities context = new LibraryEntities())
            {
                int? id = customer.CustomerID;

                var customerToReturnBook = context.tblCustomers.SingleOrDefault(x => x.CustomerID == id);
                customerToReturnBook.BookID = null;
                context.SaveChanges();
                IsUpdate = true;
                customer = context.vwCustomers.SingleOrDefault(x => x.CustomerID == id);
            }

            customerBook.Close();
            Customer customerRefresh = new Customer(customer);
            customerRefresh.ShowDialog();
            
        }

        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(parm => CancelExecute());
                }
                return cancel;
            }
        }

        private void CancelExecute()
        {
            customerBook.Close();
        }

        private ICommand rentBook;
        public ICommand RentBook
        {
            get
            {
                if (rentBook == null)
                {
                    rentBook = new RelayCommand(parm => RentBookExecute());
                }
                return rentBook;
            }
        }

        private void RentBookExecute()
        {
            BookList bookList = new BookList(customer);
            bookList.ShowDialog();
            if ((bookList.DataContext as BookListViewModel).IsValid == true)
            {
                int id = customer.CustomerID;
                IsUpdate = true;


                using (LibraryEntities context = new LibraryEntities())
                {
                    Customer = context.vwCustomers.SingleOrDefault(x => x.CustomerID == id);
                }
            }
        }

    }
    
}
