using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for LiveTour.xaml
    /// </summary>
    public partial class LiveTour : Page
    {


        public static ObservableCollection<Checkpoint> Checkpoints {  get; set; }

        public static ObservableCollection<TourGuests> TourGuests { get; set; }

        private TourScheduleRepository _tourScheduleRepository;
        private TourGuestRepository _tourGuestRepository;
        private TourRepository _tourRepository;
        private LocationRepository _locationRepository;
        private CheckpointRepository _checkpointRepository;


        public Tour SelectedTour {  get; set; }
        public TourSchedule SelectedTourSchedule { get; set; }
        public Location SelectedLocation {  get; set; }


        public LiveTour(int tourScheduleId)
        {
            InitializeComponent();
            DataContext = this;

            _tourScheduleRepository = new TourScheduleRepository();
            _tourGuestRepository = new TourGuestRepository();
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _checkpointRepository = new CheckpointRepository();

            Checkpoints = new ObservableCollection<Checkpoint>();
            TourGuests = new ObservableCollection<TourGuests>();
              
            SelectedTourSchedule = _tourScheduleRepository.GetById(tourScheduleId);
            SelectedTour = _tourRepository.GetById(SelectedTourSchedule.TourId);
            SelectedLocation = _locationRepository.GetById(SelectedTour.LocationId);
            Update();
        }

        public void Update()
        {
            Checkpoints.Clear();
            foreach(Checkpoint checkPoint in _checkpointRepository.GetAllByTourId(SelectedTour.Id))
            {
               Checkpoints.Add(checkPoint);
            }
            TourGuests.Clear();
            foreach (TourGuests tourGuest in _tourGuestRepository.GetAllByTourId(SelectedTourSchedule.Id))
            {
                TourGuests.Add(tourGuest);
            }

        }

       

    }
}
