using BookingApp.ApplicationServices;
using BookingApp.DTOs;
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
        public static ObservableCollection<TourGuestDTO> Guests { get; set; }

        public Tour SelectedTour { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public KeypointsTrackingViewModel( TourScheduleDTO tourSchedule)
        {
            SelectedTourSchedule = tourSchedule;
            SelectedTour = TourService.GetInstance().GetById(SelectedTourSchedule.TourId);

            Checkpoints = new ObservableCollection<Checkpoint>();
            Guests = new ObservableCollection<TourGuestDTO>();

            Update();
        }

        public void Update()
        {
            Checkpoints.Clear();
            foreach (Checkpoint checkpoint in CheckpointService.GetInstance().GetFinishedCheckpoints(SelectedTourSchedule))
            {
                Checkpoints.Add(checkpoint);
            }

            Guests.Clear();
            foreach(TourGuests guest in TourGuestService.GetInstance().GetAllByTourId(SelectedTourSchedule.Id))
            {
                Guests.Add(new TourGuestDTO(guest));
            }

        }
    }
}
