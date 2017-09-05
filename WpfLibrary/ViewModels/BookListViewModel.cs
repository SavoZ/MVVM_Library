using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfLibrary.Commands;

namespace WpfLibrary.ViewModels
{
    class BookListViewModel : ViewModelBase
    {
        private BookList bookList;
        private vwCustomer customer;

        public BookListViewModel(BookList bookList, vwCustomer customer)
        {
            this.bookList = bookList;
            this.customer = customer;
            using (LibraryEntities context = new LibraryEntities())
            {
                Books = context.vwBooks.ToList();
            }
            this.filter = "";
        }

        private vwBook book;
        public vwBook Book
        {
            get { return book; }
            set { book = value; OnProperyChanged("Book"); }
        }
        private List<vwBook> books;
        public List<vwBook> Books
        {
            get { return books; }
            set { books = value; OnProperyChanged("Books"); }
        }

        private String filter;
        public String Filter
        {
            get { return filter; }
            set { filter = value; OnProperyChanged("FilterBooks"); }
        }

        public List<vwBook> FilterBooks
        {
            get
            {
                return (from book in books where book.name.Contains(filter) select book).ToList();
            }
        }


        private ICommand rent;
        public ICommand Rent
        {
            get
            {
                if (rent == null)
                {
                    rent = new RelayCommand(parm => RentExecute(), parm => CanRentExecute());
                }
                return rent;
            }
        }

        private bool CanRentExecute()
        {
            if (book != null)
                return true;
            else
                return false;
        }

        private void RentExecute()
        {
            using (LibraryEntities context = new LibraryEntities())
            {
                int id = customer.CustomerID;
                var findCustomer = context.tblCustomers.SingleOrDefault(x => x.CustomerID == id);
                findCustomer.BookID = book.BookID;
                context.SaveChanges();
                IsValid = true;
                bookList.Close();
            }
        }

        public bool IsValid { get; set; }
    }
}
