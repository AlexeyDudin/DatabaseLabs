using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DomainLab3
{
    public class Cource: INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private ObservableCollection<Page> pages = new ObservableCollection<Page>(); 

        public int Id { get; set; }
        public string Name 
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description 
        { 
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public ObservableCollection<Page> Pages 
        { 
            get => pages;
            set
            {
                pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}