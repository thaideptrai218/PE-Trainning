using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFBoilerplate.Models
{
    public class Status : INotifyPropertyChanged
    {
        private int _statusId;
        private string _statusName;

        public int StatusId
        {
            get => _statusId;
            set => SetProperty(ref _statusId, value);
        }

        public string StatusName
        {
            get => _statusName;
            set => SetProperty(ref _statusName, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}