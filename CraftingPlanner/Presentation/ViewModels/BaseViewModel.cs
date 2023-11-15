using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CraftingPlanner.Presentation.ViewModels
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected virtual bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        public static bool SetProperty<T>(BaseViewModel instance, ref T field, T newValue, [CallerMemberName] string? propertyName = null)
            => instance.SetProperty<T>(ref field, newValue, propertyName);
    }
}
