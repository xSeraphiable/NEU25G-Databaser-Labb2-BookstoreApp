using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookstoreApp.Commands
{
    internal class AsyncDelegateCommand : ICommand
    {
        private readonly Func<object?, Task> executeAsync;
        private readonly Func<object?, bool>? canExecute;
        private bool isExecuting;

        public event EventHandler? CanExecuteChanged;

        public AsyncDelegateCommand(
            Func<object?, Task> executeAsync,
            Func<object?, bool>? canExecute = null)
        {
            this.executeAsync = executeAsync
                ?? throw new ArgumentNullException(nameof(executeAsync));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
            => !isExecuting && (canExecute?.Invoke(parameter) ?? true);

        public async void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
                return;

            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();

                await executeAsync(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fel");
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
