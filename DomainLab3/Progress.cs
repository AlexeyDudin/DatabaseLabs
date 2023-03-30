using System.ComponentModel;

namespace DomainLab3
{
    public class Progress : INotifyPropertyChanged
    {
        private Cource cource = new Cource();
        private Page page = new Page();
        public int Id { get; set; }
        public Cource Cource 
        { 
            get => cource;
            set
            {
                cource = value;
                OnPropertyChanged(nameof(Cource));
            }
        }
        public Page Page 
        { 
            get => page;
            set
            {
                page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
