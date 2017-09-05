using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfLibrary.Commands;

namespace WpfLibrary.ViewModels
{
    class AllCustomerViewModel : ViewModelBase
    {
        private AllCustomer allCustomer;

        public AllCustomerViewModel(AllCustomer allCustomer)
        {
            this.allCustomer = allCustomer;
            using (LibraryEntities context = new LibraryEntities())
            {
                names = context.vwCustomers.ToList();
            }
            this.filter = "";
        }

        private vwCustomer customer;
        public vwCustomer Customer
        {
            get { return customer; }
            set { customer = value; OnProperyChanged("Customer"); }
        }

        private List<vwCustomer> names;
        private String filter;
        public List<vwCustomer> FilteredNames
        {
            get
            {
                return (from name in names where name.customerName.Contains(filter) select name).ToList<vwCustomer>();
            }


        }

        public String Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                this.filter = value;
                OnProperyChanged("FilteredNames");

            }
        }

        private ICommand bookCustomer;
        public ICommand BookCustomer
        {
            get
            {
                if (bookCustomer == null)
                {
                    bookCustomer = new RelayCommand(parm => BookCustomerExecute(), parm => CanBookCustomerExecute());
                }
                return bookCustomer;
            }

        }

        private bool CanBookCustomerExecute()
        {
            if (customer != null)
                return true;
            else
                return false;
        }

        private void BookCustomerExecute()
        {
            Customer bookCustomer = new Customer(Customer);
            bookCustomer.ShowDialog();

            if ((bookCustomer.DataContext as CustomerViewModel).IsUpdate == true)
            {
                using (LibraryEntities context = new LibraryEntities())
                {
                    names = context.vwCustomers.ToList();
                    OnProperyChanged("FilteredNames");
                }
            }
        }



    }
}
