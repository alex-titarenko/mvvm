using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace TAlex.Mvvm.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="System.Windows.Application"/> class.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Gets the currently active window of the application.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>
        /// The active window of the application or null in case of none window is opened.
        /// </returns>
        public static Window GetActiveWindow(this Application application)
        {
            System.Windows.Window activeWindow = null;

            if (application != null)
            {
                application.Dispatcher.Invoke(() =>
                {
                    if (application.Windows.Count > 0)
                    {
                        var windowList = new List<Window>(application.Windows.Cast<Window>());
                        activeWindow = windowList.FirstOrDefault(cur => cur.IsActive);
                        if (activeWindow == null && windowList.Count == 1 && windowList[0].Topmost)
                        {
                            activeWindow = windowList[0];
                        }
                    }
                });
            }

            return activeWindow;
        }
    }
}
