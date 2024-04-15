using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class KeypointsTrackingViewModel
    {
        public static ObservableCollection<Checkpoint> Checkpoints { get; set; }

        public Tour SelectedTour { get; set; }
        public TourSchedule SelectedTourSchedule { get; set; }

        private CheckpointService _checkpointService;
        private TourService _tourService;
        private TourScheduleService _schdeuleService;
        public KeypointsTracking Window { get; set; }
        public KeypointsTrackingViewModel(KeypointsTracking window, int tourScheduleId)
        {
            Window = window;

            _checkpointService = new CheckpointService();
            _tourService = new TourService();
            _schdeuleService = new TourScheduleService();

            SelectedTourSchedule = _schdeuleService.GetById(tourScheduleId);
            SelectedTour = _tourService.GetById(SelectedTourSchedule.TourId);

            Checkpoints = new ObservableCollection<Checkpoint>();

            Update();
        }

        public void Update()
        {
            Checkpoints.Clear();

            foreach (Checkpoint checkpoint in _checkpointService.GetFinishedCheckpoints(SelectedTourSchedule))
            {

                Checkpoints.Add(checkpoint);
            }

        }
    }
}
