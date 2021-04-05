using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game_2048.ViewModels
{
    internal abstract class ViewModel<TViewModel> : INotifyPropertyChanged, IViewModel
        where TViewModel : ViewModel<TViewModel>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
