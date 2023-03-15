using System.ComponentModel;

namespace Domain
{
    public class PlacementAlongTheRoad : INotifyPropertyChanged
    {
        private long id;
        private string name;

        public long Id 
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        { 
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public override string ToString()
        {
            return Name;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
