using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfLibrary.Commands
{
    class RelayCommand: ICommand
    {

        #region Fields readonly
        Action<object> _execute;
        readonly Predicate<object> _canExecuet;
        #endregion

        #region Constructor
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {

            if (execute == null) throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecuet = canExecute;
        }
        #endregion
        #region ICommand Members [DebuggerStepThrough]
        public bool CanExecute(object parameter) { return _canExecuet == null ? true : _canExecuet(parameter); }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) { _execute(parameter); }
        #endregion
    }
}
