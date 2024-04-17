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
        public KeypointsTrackingViewModel( int tourScheduleId)
        {
            SelectedTourSchedule = TourScheduleService.GetInstance().GetById(tourScheduleId);
            SelectedTour = TourService.GetInstance().GetById(SelectedTourSchedule.TourId);

            Checkpoints = new ObservableCollection<Checkpoint>();

            Update();
        }

        public void Update()
        {
            Checkpoints.Clear();

            foreach (Checkpoint checkpoint in CheckpointService.GetInstance().GetFinishedCheckpoints(SelectedTourSchedule))
            {

                Checkpoints.Add(checkpoint);
            }

        }
    }
}
