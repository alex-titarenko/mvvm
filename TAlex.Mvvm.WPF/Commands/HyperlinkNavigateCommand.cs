using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;


namespace TAlex.Mvvm.Commands
{
    public class HyperlinkNavigateCommand : ICommand
    {
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Uri uri = parameter is Uri ? (Uri)parameter : new Uri(parameter.ToString());

            if (uri != null && string.IsNullOrEmpty(uri.OriginalString) == false)
            {
                string uriString = uri.AbsoluteUri;
                Process.Start(new ProcessStartInfo(uriString));
            }
        }


        public virtual void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        #endregion
    }
}
