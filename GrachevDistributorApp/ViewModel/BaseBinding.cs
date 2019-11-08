using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GrachevDistributorApp.ViewModel
{
    public abstract class BaseBinding : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            RaisePropertyChanged(propertyName);
        }

        public void SetValue<T>(ref T target, T value, [CallerMemberName] string propertyName = "")
        {
            target = value;
            RaisePropertyChanged(propertyName);
        }

        public void SetValue(Action setter, [CallerMemberName] string propertyName = "")
        {
            setter?.Invoke();
            RaisePropertyChanged(propertyName);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Создать комманду
        /// </summary>
        /// <param name="onExecuteMethod">Выполняемый метод</param>
        /// <param name="onCanExecuteMethod">Метод, проверяющий доступность коммандны</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Action<object> onExecuteMethod, Func<object, bool> onCanExecuteMethod)
        {
            return new DelegateCommand(onExecuteMethod, onCanExecuteMethod);
        }

        /// <summary>
        /// Создать комманду, которая всегда разрешена к выполнению
        /// </summary>
        /// <param name="onExecuteMethod">Выполняемый метод</param>
        /// <returns></returns>
        protected ICommand CreateCommand(Action<object> onExecuteMethod)
        {
            return new DelegateCommand(onExecuteMethod, _ => true);
        }

        #endregion

        #region Helpers

        private void RaisePropertyChanged(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
