using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ApplicationServices
{
    public class GuideService
    {
        private IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        public static GuideService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<GuideService>();
        }

        public List<Guide> GetAll()
        {
            return _guideRepository.GetAll();
        }

        public Guide Save(Guide guide)
        {
            return _guideRepository.Save(guide);
        }

        public int NextId()
        {
           return _guideRepository.NextId();    
        }

        public void Delete(Guide guide)
        {
            _guideRepository.Delete(guide);
        }

        public Guide Update(Guide guide)
        {
           return _guideRepository.Update(guide);   
        }

        public Guide GetById(int id)
        {
           return _guideRepository.GetById(id);
        }

        public Guide GetByUserId(int id)
        {
            foreach(Guide guide in GetAll())
            {
                if(guide.UserId == id)
                {
                    return guide;
                }
            }
            return null;
        }

        public void UpdateGuideDetails(User user)
        {
            Language primaryLanguage = FindPrimaryLanguage(user);
            double avgGrade = FindAvgGrade(primaryLanguage,user);

            int numberOfTours = CalculateEndedTours(user);


            int numberOfToursForRank = CalculateToursForLanguage(user,primaryLanguage);

            Guide guide = GetByUserId(user.Id);
            guide.Language = primaryLanguage.Name;
            guide.Grade = avgGrade;
            guide.NumberOfTours = numberOfTours;
            if (numberOfToursForRank > 20 && avgGrade>=4.0)
            {
                guide.Rank = Enums.GuideRank.SuperGuide;
            }
            else
            {
                guide.Rank = Enums.GuideRank.Guide;
            }

            Update(guide);

        }
        public int CalculateEndedTours(User user)
        {
            List<TourSchedule> schedules = TourScheduleService.GetInstance().GetAllByUser(user);

            int numberOfTours = 0;

            foreach (TourSchedule schedule in schedules)
            {
                if (schedule.TourActivity == Enums.TourActivity.Finished)
                {
                    numberOfTours++;
                }
            }

            return numberOfTours;
        }


        public int CalculateToursForLanguage(User user, Language language)
        {
            List<TourSchedule> schedules = TourScheduleService.GetInstance().GetAllByUser(user);

            int numberOfTours = 0;


            foreach (TourSchedule schedule in schedules)
            {
                if (TourService.GetInstance().GetById(schedule.TourId).LanguageId == language.Id && schedule.TourActivity == Enums.TourActivity.Finished)
                {
                    numberOfTours++;
                }
            }

            return numberOfTours;
        }

        public double FindAvgGrade(Language language, User user)
        {
            List<TourReview> reviews = TourReviewService.GetInstance().GetAllReviewsByPrimaryLanguage(user,language);
            double avgGrade = 0;
            int numberOfReviews = 0;
            double gradeSum = 0;

            foreach(TourReview review in reviews)
            {
                gradeSum +=(double)(review.GuideKnowledgeGrade + review.GuideLanguageGrade + review.TourAttractionsGrade)/3;
                numberOfReviews++;
            }

            if (numberOfReviews != 0)
            {
                avgGrade = gradeSum / numberOfReviews;
            }
            return avgGrade;

        }



        public Language FindPrimaryLanguage(User user)
        {
            List<Language> languages = LanguageService.GetInstance().GetAll();
            List<TourSchedule> schedules = TourScheduleService.GetInstance().GetAllByUser(user);

            
            Language language = languages[0];

            int numberOfTours = 0;
            int numberOfToursTemp = 0;
            foreach(Language lang in languages)
            {
                foreach(TourSchedule schedule in schedules)
                {
                    if(TourService.GetInstance().GetById(schedule.TourId).LanguageId == lang.Id && schedule.TourActivity == Enums.TourActivity.Finished )
                    {
                        numberOfToursTemp++;
                    }
                }

                if (numberOfToursTemp > numberOfTours)
                {
                    numberOfTours = numberOfToursTemp;
                    language = lang;
                }
                numberOfToursTemp = 0;

            }
            return language;    
        }

        public bool DeleteGuide(Guide guide)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete your account?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                User user = UserService.GetInstance().GetById(guide.UserId);
                List<TourSchedule> schedule = TourScheduleService.GetInstance().GetAllByUser(user);
                foreach (TourSchedule sch in schedule)
                {
                    List<TourGuests> guests = TourGuestService.GetInstance().GetAllByTourId(sch.Id);
                    if (guests.Count != 0)
                    {
                        VoucherService.GetInstance().SaveAllGuests(guests);
                        foreach (TourGuests guest in guests)
                        {
                            TourGuestService.GetInstance().Delete(guest);
                        }

                    }

                    sch.TourActivity = Enums.TourActivity.Canceled;
                    TourScheduleService.GetInstance().Update(sch);  
                }
                GuideService.GetInstance().Delete(guide);
                UserService.GetInstance().Delete(user);

                return true;
            }
            return false;    
        }




    }
}
