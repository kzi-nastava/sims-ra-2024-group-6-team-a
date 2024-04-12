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
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for KeypointsTracking.xaml
    /// </summary>
    public partial class KeypointsTracking : Window
    {
        public static ObservableCollection<Checkpoint> Checkpoints { get; set; }

        public Tour SelectedTour { get; set; }
        public TourSchedule SelectedTourSchedule { get; set; }

        private CheckpointRepository _checkpointRepository;
        private TourRepository _tourRepository;
        private TourScheduleRepository _tourScheduleRepository;
        public KeypointsTracking(int tourScheduleId)
        {
            InitializeComponent();
            DataContext = this;

            _checkpointRepository = new CheckpointRepository();
            _tourRepository = new TourRepository();
            _tourScheduleRepository = new TourScheduleRepository();

            SelectedTourSchedule = _tourScheduleRepository.GetById(tourScheduleId);
            SelectedTour = _tourRepository.GetById(SelectedTourSchedule.TourId);
            
            Checkpoints = new ObservableCollection<Checkpoint>();

            Update();
        }

        private void Update()
        {
            Checkpoints.Clear();

            foreach (Checkpoint checkpoint in _checkpointRepository.GetFinishedCheckpoints(SelectedTour))
            {

                Checkpoints.Add(checkpoint);
            }

        }
    }
}
