using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TAlex.Mvvm.Extensions;


namespace TAlex.Mvvm.Services
{
    public class MessageService : IMessageService
    {
        #region Methods

        /// <summary>
        /// Translates the message box result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>
        /// Corresponding <see cref="MessageResult"/>.
        /// </returns>
        protected static MessageResult TranslateMessageBoxResult(MessageBoxResult result)
        {
            return (MessageResult)Enum.Parse(typeof(MessageResult), result.ToString(), true);
        }

		/// <summary>
		/// Translates the message image.
		/// </summary>
		/// <param name="image">The image.</param>
		/// <returns>
		/// Corresponding <see cref="MessageBoxImage"/>.
		/// </returns>
		protected static MessageBoxImage TranslateMessageImage(MessageImage image)
		{
            return (MessageBoxImage)Enum.Parse(typeof(MessageBoxImage), image.ToString(), true);
		}

        /// <summary>
        /// Translates the message button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        /// Corresponding <see cref="MessageBoxButton"/>.
        /// </returns>
        protected static MessageBoxButton TranslateMessageButton(MessageButton button)
        {
            return (MessageBoxButton)Enum.Parse(typeof(MessageBoxButton), button.ToString(), true);

        }

        #endregion

        #region IMessageService Members

        /// <summary>
        /// Shows an error message to the user and allows a callback operation when the message is completed.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="completedCallback">The callback to invoke when the message is completed. Can be <c>null</c>.</param>
        /// <remarks>
        /// There is no garantuee that the method will be executed asynchronous, only that the <paramref name="completedCallback"/>
        /// will be invoked when the message is dismissed.
        /// </remarks>
        /// <exception cref="ArgumentNullException">The <paramref name="exception"/> is <c>null</c>.</exception>
        public virtual void ShowError(Exception exception, Action completedCallback)
        {
            ShowError(exception.Message, string.Empty, completedCallback);
        }

        /// <summary>
        /// Shows an error message to the user and allows a callback operation when the message is completed.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="completedCallback">The callback to invoke when the message is completed. Can be <c>null</c>.</param>
        /// <remarks>
        /// There is no garantuee that the method will be executed asynchronous, only that the <paramref name="completedCallback"/>
        /// will be invoked when the message is dismissed.
        /// </remarks>
        /// <exception cref="ArgumentException">The <paramref name="message"/> is <c>null</c> or whitespace.</exception>
        public virtual void ShowError(string message, string caption, Action completedCallback = null)
        {
            const MessageButton button = MessageButton.OK;
            const MessageImage icon = MessageImage.Error;

            if (completedCallback != null)
            {
                ShowAsync(message, caption, button, icon, result => completedCallback());
            }
            else
            {
                Show(message, caption, button, icon);
            }
        }

        /// <summary>
        /// Shows a warning message to the user and allows a callback operation when the message is completed.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="completedCallback">The callback to invoke when the message is completed. Can be <c>null</c>.</param>
        /// <remarks>
        /// There is no garantuee that the method will be executed asynchronous, only that the <paramref name="completedCallback"/>
        /// will be invoked when the message is dismissed.
        /// </remarks>
        /// <exception cref="ArgumentException">The <paramref name="message"/> is <c>null</c> or whitespace.</exception>
        public virtual void ShowWarning(string message, string caption, Action completedCallback = null)
        {
            const MessageButton button = MessageButton.OK;
            const MessageImage icon = MessageImage.Warning;

            if (completedCallback != null)
            {
                ShowAsync(message, caption, button, icon, result => completedCallback());
            }
            else
            {
                Show(message, caption, button, icon);
            }
        }

        /// <summary>
        /// Shows an information message to the user and allows a callback operation when the message is completed.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="completedCallback">The callback to invoke when the message is completed. Can be <c>null</c>.</param>
        /// <remarks>
        /// There is no garantuee that the method will be executed asynchronous, only that the <paramref name="completedCallback"/>
        /// will be invoked when the message is dismissed.
        /// </remarks>
        /// <exception cref="ArgumentException">The <paramref name="message"/> is <c>null</c> or whitespace.</exception>
        public virtual void ShowInformation(string message, string caption, Action completedCallback = null)
        {
            const MessageButton button = MessageButton.OK;
            const MessageImage icon = MessageImage.Information;

            if (completedCallback != null)
            {
                ShowAsync(message, caption, button, icon, result => completedCallback());
            }
            else
            {
                Show(message, caption, button, icon);
            }
        }

        /// <summary>
        /// Shows the specified message and returns the result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>The <see cref="MessageResult"/>.</returns>
        /// <exception cref="ArgumentException">The <paramref name="message"/> is <c>null</c> or whitespace.</exception>
        public virtual MessageResult Show(string message, string caption, MessageButton button = MessageButton.OK, MessageImage icon = MessageImage.None)
        {
            return ShowMessageBox(message, caption, button, icon);
        }

        /// <summary>
        /// Shows an information message to the user and allows a callback operation when the message is completed.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="completedCallback">The callback to invoke when the message is completed. Can be <c>null</c>.</param>
        /// <remarks>
        /// There is no garantuee that the method will be executed asynchronous, only that the <paramref name="completedCallback"/>
        /// will be invoked when the message is dismissed.
        /// </remarks>
        /// <exception cref="ArgumentException">The <paramref name="message"/> is <c>null</c> or whitespace.</exception>
        public virtual void ShowAsync(string message, string caption, MessageButton button = MessageButton.OK,
            MessageImage icon = MessageImage.None, Action<MessageResult> completedCallback = null)
        {
            var result = Show(message, caption, button, icon);
            if (completedCallback != null)
            {
                completedCallback(result);
            }
        }

        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>The message result.</returns>
        /// <exception cref="ArgumentException">The <paramref name="message"/> is <c>null</c> or whitespace.</exception>
        protected virtual MessageResult ShowMessageBox(string message, string caption, MessageButton button = MessageButton.OK, MessageImage icon = MessageImage.None)
        {
            var result = MessageBoxResult.None;
            var messageBoxButton = TranslateMessageButton(button);

            var messageBoxImage = TranslateMessageImage(icon);

            var activeWindow = Application.Current.GetActiveWindow();
            if (activeWindow != null)
            {
                result = MessageBox.Show(activeWindow, message, caption, messageBoxButton, messageBoxImage);
            }
            else
            {
                result = MessageBox.Show(message, caption, messageBoxButton, messageBoxImage);
            }

            return TranslateMessageBoxResult(result);
        }

        #endregion
    }
}
