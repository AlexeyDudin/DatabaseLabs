using System.ComponentModel;

namespace Domain
{
    public class Road: INotifyPropertyChanged
    {
        private long id;
        private LocalityName startPoint;
        private LocalityName endPoint;

        public long Id 
        { 
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public LocalityName StartPoint 
        {
            get => startPoint;
            set
            {
                startPoint = value;
                OnPropertyChanged(nameof(StartPoint));
            }
        }
        public LocalityName EndPoint
        { 
            get => endPoint;
            set
            {
                endPoint = value;
                OnPropertyChanged(nameof(EndPoint));
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
