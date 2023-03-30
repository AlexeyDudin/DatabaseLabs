using System.ComponentModel;

namespace DomainLab3
{
    public class Page: INotifyPropertyChanged
    {
        private string text;
        public int Id { get; set; }
        public string Text 
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
