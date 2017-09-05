using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfLibrary.Commands;

namespace WpfLibrary.ViewModels
{
    class AddCustomerViewModel : ViewModelBase
    {
        private AddCustomer addCustomer;

        public AddCustomerViewModel(AddCustomer addCustomer)
        {
            this.addCustomer = addCustomer;
            customer = new vwCustomer();
            using (LibraryEntities context = new LibraryEntities())
            {
                books = context.vwBooks.ToList();
            }
        }

        private List<vwBook> books;
        public List<vwBook> Books
        {
            get { return books; }
            set { books = value; OnProperyChanged("Books"); }
        }

        private vwCustomer customer;
        public vwCustomer Customer
        {
            get { return customer; }
            set { customer = value; OnProperyChanged("Customer"); }
        }



        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(parm => SaveExecute(), parm => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private void SaveExecute()
        {
            AddNewCutomer(customer);
            addCustomer.Close();

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
            addCustomer.Close();
        }

        private void AddNewCutomer(vwCustomer customer)
        {
            try
            {
                using (LibraryEntities context = new LibraryEntities())
                {
                    tblCustomer newCustomer = new tblCustomer();
                    newCustomer.customerName = customer.customerName;
                    newCustomer.date = customer.date;
                    newCustomer.address = customer.address;
                    newCustomer.BookID = customer.BookID;
                    context.tblCustomers.Add(newCustomer);
                    context.SaveChanges();
                    customer.CustomerID = newCustomer.CustomerID;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }

        }
    }
}
