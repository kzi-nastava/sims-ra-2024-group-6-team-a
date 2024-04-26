using BookingApp.ApplicationServices;
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

       

        public TourReviewsPage Window {  get; set; }
        public TourReviewsViewModel(TourReviewsPage window,Frame mainFrame, User user)
        {
            LoggedUser = user;
            Window = window;
            
            Tours = new ObservableCollection<GuideReviewDTO>();

            Update();
        }


        public void Update()
        {
            Tours.Clear();
            foreach (Tour tour in TourService.GetInstance().GetAllByUser(LoggedUser))
            {
                Model.Image image = GetFirstTourImage(tour.Id);
                Double avgGrade = 0;
                Location location = LocationService.GetInstance().GetById(tour.LocationId);
                Language language = LanguageService.GetInstance().GetById(tour.LanguageId);

                foreach (TourSchedule tourSchedule in TourScheduleService.GetInstance().GetAllByTourId(tour.Id))
                {
                    if (tourSchedule.TourActivity != Enums.TourActivity.Finished) continue;

                    avgGrade = CalculateAvgGrade(tourSchedule.Id);
                    DateTime dateTime = tourSchedule.Start;
                    Tours.Add(new GuideReviewDTO(tour,language, location, image.Path, dateTime, tourSchedule.Id, avgGrade));
                }


            }

        }

        public double CalculateAvgGrade(int tourScheduleId)
        {
            double gradeSum = 0;
            double reviewsCount = 0;

            foreach (TourReview tourReview in TourReviewService.GetInstance().GetAllReviewsByScheduleId(tourScheduleId))
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
            return ImageService.GetInstance().GetByEntity(tourId, Enums.ImageType.Tour).First();
        }
    }
}
