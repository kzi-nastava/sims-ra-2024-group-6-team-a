using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuideView.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class TourReviewsViewModel
    {
        public static ObservableCollection<GuideReviewDTO> Tours { get; set; }
        public User LoggedUser { get; set; }

        public Frame mainFrame;

        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;
        private TourReviewRepository _tourReviewRepository;

        public TourReviewsPage Window {  get; set; }
        public TourReviewsViewModel(TourReviewsPage window,Frame mainFrame, TourCreationPage tourCreationPage, User user, LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleRepository tourScheduleRepository, TourRepository tourRepository, TourReviewRepository tourReviewRepository)
        {
            LoggedUser = user;
            Window = window;
            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = tourRepository;
            _tourScheduleRepository = tourScheduleRepository;
            _tourReviewRepository = tourReviewRepository;
            Tours = new ObservableCollection<GuideReviewDTO>();

            Update();
        }


        public void Update()
        {
            Tours.Clear();
            foreach (Tour tour in _tourRepository.GetAllByUser(LoggedUser))
            {
                Model.Image image = GetFirstTourImage(tour.Id);
                Double avgGrade = 0;
                Location location = _locationRepository.GetById(tour.LocationId);
                foreach (TourSchedule tourSchedule in _tourScheduleRepository.GetAllByTourId(tour.Id))
                {
                    if (tourSchedule.TourActivity != Enums.TourActivity.Finished) continue;

                    avgGrade = CalculateAvgGrade(tourSchedule.Id);
                    DateTime dateTime = tourSchedule.Start;
                    Tours.Add(new GuideReviewDTO(tour, location, image.Path, dateTime, tourSchedule.Id, avgGrade));
                }


            }

        }

        public double CalculateAvgGrade(int tourScheduleId)
        {
            double gradeSum = 0;
            double reviewsCount = 0;

            foreach (TourReview tourReview in _tourReviewRepository.GetAllReviewsByScheduleId(tourScheduleId))
            {
                int KnowledgeGrade = tourReview.GuideKnowledgeGrade;
                int LanguageGrade = tourReview.GuideLanguageGrade;
                int AttractionsGrade = tourReview.TourAttractionsGrade;

                gradeSum += (KnowledgeGrade + LanguageGrade + AttractionsGrade) / 3.0;
                reviewsCount++;
            }
            double AvgGrade = gradeSum / reviewsCount;
            if (double.IsNaN(AvgGrade))
                AvgGrade = 0;

            return AvgGrade;
        }

        public Model.Image GetFirstTourImage(int tourId)
        {
            return _imageRepository.GetByEntity(tourId, Enums.ImageType.Tour).First();
        }
    }
}
