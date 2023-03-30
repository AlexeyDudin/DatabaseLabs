using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DomainLab3
{
    public class User: INotifyPropertyChanged
    {
        private string userName = "";
        private ObservableCollection<Progress> progresses = new ObservableCollection<Progress>();

        public int Id { get; set; }
        public string UserName 
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public ObservableCollection<Progress> Progresses
        { 
            get => progresses;
            set
            {
                progresses = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public ObservableCollection<Cource> UserCources
        { 
            get
            {
                HashSet<Cource> cources = new HashSet<Cource>();
                foreach (var progres in progresses)
                {
                    if (!cources.Contains(progres.Cource))
                        cources.Add(progres.Cource);
                }
                return new ObservableCollection<Cource>(cources);
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
