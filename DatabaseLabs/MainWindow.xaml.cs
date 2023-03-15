using DbWorker;
using Domain;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DatabaseLabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private DatabaseWorker dbWorker = new DatabaseWorker();
        private ObservableCollection<LocalityName> localityNames;
        private ObservableCollection<PlacementAlongTheRoad> placementAlongTheRoads;
        private LocalityName selectedLocalityName;
        private PlacementAlongTheRoad selectedPlacementAlongTheRoad;

        public MainWindow()
        {
            InitializeComponent();
            LocalityNames = dbWorker.GetAllLocalityNames();
            PlacementAlongTheRoads = dbWorker.GetAllPlacementAlongTheRoad();
        }

        public ObservableCollection<LocalityName> LocalityNames
        { 
            get => localityNames;
            set
            {
                localityNames = value;
                OnPropertyChanged(nameof(LocalityNames));
            }
        }

        public ObservableCollection<PlacementAlongTheRoad> PlacementAlongTheRoads
        {
            get => placementAlongTheRoads;
            set
            {
                placementAlongTheRoads = value;
                OnPropertyChanged(nameof(PlacementAlongTheRoads));
            }
        }

        public LocalityName SelectedLocalityName 
        { 
            get => selectedLocalityName;
            set
            {
                selectedLocalityName = value;
                OnPropertyChanged(nameof(SelectedLocalityName));
            }
        }

        public PlacementAlongTheRoad SelectedPlacementAlongTheRoad 
        { 
            get => selectedPlacementAlongTheRoad;
            set
            {
                selectedPlacementAlongTheRoad = value;
                OnPropertyChanged(nameof(SelectedPlacementAlongTheRoad));
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
