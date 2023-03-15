using System.ComponentModel;

namespace Domain
{
    public class StopOnTheRoad: INotifyPropertyChanged
    {
        private long id;
        private Road road;
        private string isHavePavilion;
        private PlacementAlongTheRoad placementAlongTheRoad;
        private float rangeFromStart;
        private string busStopName;

        public long Id 
        { 
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public Road Road 
        { 
            get => road;
            set
            {
                road = value;
                OnPropertyChanged(nameof(Road));
            }
        }
        public string IsHavePavilion
        {
            get => isHavePavilion;
            set
            {
                isHavePavilion = value;
                OnPropertyChanged(nameof(IsHavePavilion));
            }
        }
        public PlacementAlongTheRoad PlacementAlongTheRoad 
        { 
            get => placementAlongTheRoad;
            set
            {
                placementAlongTheRoad = value;
                OnPropertyChanged(nameof(PlacementAlongTheRoad));
            }
        }
        public float RangeFromStart 
        {
            get => rangeFromStart;
            set
            {
                rangeFromStart = value;
                OnPropertyChanged(nameof(RangeFromStart));
            }
        }
        public string BusStopName 
        { 
            get => busStopName;
            set
            {
                busStopName = value;
                OnPropertyChanged(nameof(BusStopName));
            }
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
