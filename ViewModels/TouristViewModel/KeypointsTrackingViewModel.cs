using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class KeypointsTrackingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private ObservableCollection<CheckpointDTO> checkpoints;
        public ObservableCollection<CheckpointDTO> Checkpoints
        {
            get => checkpoints;
            set
            {
                checkpoints = value;
                OnPropertyChanged("Checkpoints");
            }
        }
        public static ObservableCollection<TourGuestDTO> Guests { get; set; }

        public Tour SelectedTour { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }

        private ScrollViewer _scrollViewer;
        public RelayCommand ScrollLeftCommand { get; }
        public RelayCommand ScrollRightCommand { get; }

        public KeypointsTrackingViewModel( TourScheduleDTO tourSchedule)
        {
            SelectedTourSchedule = tourSchedule;
            SelectedTour = TourService.GetInstance().GetById(SelectedTourSchedule.TourId);

            Checkpoints = new ObservableCollection<CheckpointDTO>();
            Guests = new ObservableCollection<TourGuestDTO>();

            ScrollLeftCommand = new RelayCommand(ScrollLeft);
            ScrollRightCommand = new RelayCommand(ScrollRight);

            Update();
        }

        public void Update()
        {
            Checkpoints.Clear();
            foreach (Checkpoint checkpoint in CheckpointService.GetInstance().GetAllByTourScheduleId(SelectedTourSchedule.Id))
            {
                Checkpoints.Add(new CheckpointDTO(checkpoint));
            }
            MarkLastReachedCheckpoint();


            Guests.Clear();
            foreach(TourGuests guest in TourGuestService.GetInstance().GetAllByTourId(SelectedTourSchedule.Id))
            {
                Guests.Add(new TourGuestDTO(guest));
            }

        }

        private void MarkLastReachedCheckpoint()
        {
            for (int i = 0; i < Checkpoints.Count; i++)
            {
                Checkpoints[i].IsLastReached = false;
            }

            for (int i = Checkpoints.Count - 1; i >= 0; i--)
            {
                if (Checkpoints[i].IsReached)
                {
                    Checkpoints[i].IsLastReached = true;
                    break;
                }
            }
        }

        public void SetScrollViewer(ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
        }
        private void ScrollLeft(object obj)
        {
            _scrollViewer?.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - 165);
        }

        private void ScrollRight(object obj)
        {
            _scrollViewer?.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset + 165);
        }
    }
}
