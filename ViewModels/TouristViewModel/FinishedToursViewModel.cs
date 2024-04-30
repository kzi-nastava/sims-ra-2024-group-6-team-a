using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class FinishedToursViewModel
    {
        public static ObservableCollection<TourScheduleDTO> Tours { get; set; }
        public User LoggedUser { get; set; }

        public RelayCommand RateTourCommand { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public FinishedToursViewModel( User user)
        {
            LoggedUser = user;

            Tours = new ObservableCollection<TourScheduleDTO>();
            RateTourCommand = new RelayCommand(Execute_RateTourCommand);

            Update();
        }

        public void Update()
        {
            Tours.Clear();
            foreach (TourSchedule tour in TourScheduleService.GetInstance().GetAllFinishedTours(LoggedUser))
            {
                Model.Image image = GetFirstTourImage(tour.TourId);
                Tours.Add(new TourScheduleDTO(tour, LocationService.GetInstance().GetById(TourService.GetInstance().GetById(tour.TourId).LocationId), LanguageService.GetInstance().GetById(TourService.GetInstance().GetById(tour.TourId).LanguageId), image.Path));
            }

        }

        public Model.Image GetFirstTourImage(int tourId)
        {
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }

        public void Execute_RateTourCommand(object obj)
        {
            TourScheduleDTO tourSchedule = (TourScheduleDTO)obj;
            TourRating rating = new TourRating(tourSchedule, LoggedUser);
            rating.Owner = Application.Current.MainWindow;
            rating.ShowDialog();
        }
    }
} 
