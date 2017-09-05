using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfLibrary.Commands;

namespace WpfLibrary.ViewModels
{
    class AddBookViewModel : ViewModelBase
    {
        private AddBook addBook;

        public AddBookViewModel(AddBook addBook)
        {
            this.addBook = addBook;
            book = new vwBook();
        }

        private vwBook book;
        public vwBook Book
        {
            get { return book; }
            set
            {
                book = value;
                OnProperyChanged("Book");
            }
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

        private vwBook SaveExecute()
        {
            try
            {
                AddNewBook(book);
                isUpadate = true;
                addBook.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            return book;

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
            addBook.Close();
        }

        private bool isUpadate;
        public bool IsUpdate
        {
            get { return isUpadate; }
            set { isUpadate = value; }
        }

        

        private vwBook AddNewBook(vwBook book)
        {

            try
            {
                using (LibraryEntities context = new LibraryEntities())
                {
                    //if (book.BookID == 0)
                    //{
                        tblBook newBook = new tblBook();
                        newBook.name = book.name;
                        newBook.writer = book.writer;
                        newBook.quantity = book.quantity;
                        context.tblBooks.Add(newBook);
                        context.SaveChanges();
                        book.BookID = newBook.BookID;

                    //}
                    return book;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }

        }


    }
}
