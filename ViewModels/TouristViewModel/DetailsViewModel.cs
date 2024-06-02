using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TourTouristDTO TourTouristDTO { get; set; }
        public ObservableCollection<Model.Image> SelectedTourImages { get; set; }
        public ObservableCollection<Checkpoint> SelectedTourCheckpoints { get; set; }
        public ObservableCollection<TourSchedule> SelectedTourSchedules { get; set; }

        private ScrollViewer _scrollViewer;
        public RelayCommand ScrollLeftCommand { get; }
        public RelayCommand ScrollRightCommand { get; }
        public DetailsViewModel(TourTouristDTO tour)
        {
            ScrollLeftCommand = new RelayCommand(ScrollLeft);
            ScrollRightCommand = new RelayCommand(ScrollRight);

            TourTouristDTO = tour;

            ObservableCollection<Model.Image> lista = new ObservableCollection<Model.Image>();
            foreach (Model.Image image in ImageService.GetInstance().GetByEntity(tour.Id, Enums.ImageType.Tour))
                lista.Add(image);

            SelectedTourImages = lista;

            ObservableCollection<Checkpoint> points = new ObservableCollection<Checkpoint>();
            foreach(Checkpoint check in CheckpointService.GetInstance().GetAllByTourId(TourTouristDTO.Id))
                points.Add(check);

            SelectedTourCheckpoints = points;

            ObservableCollection<TourSchedule> schedules = new ObservableCollection<TourSchedule>();
            foreach(TourSchedule schedule in TourScheduleService.GetInstance().GetAllByTourId(TourTouristDTO.Id))
                schedules.Add(schedule);
            SelectedTourSchedules = schedules;
        }

        public void SetScrollViewer(ScrollViewer scrollViewer)
        {
            _scrollViewer = scrollViewer;
        }
        private void ScrollLeft(object obj)
        {
            _scrollViewer?.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset - 150);
        }

        private void ScrollRight(object obj)
        {
            _scrollViewer?.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset + 150);
        }
    }
}
