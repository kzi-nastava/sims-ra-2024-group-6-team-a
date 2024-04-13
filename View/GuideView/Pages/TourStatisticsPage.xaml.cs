using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.Observer;
using System.ComponentModel;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page
    {
        public User LoggedUser { get; set; }
        private LocationRepository _locationRepository;
        private ImageRepository _imageRepository;
        private TourScheduleRepository _tourScheduleRepository;
        private TourRepository _tourRepository;
        private TourGuestRepository _tourGuestRepository;


        public static ObservableCollection<TourStatisticsDTO> FinishedTours { get; set; }
        public static ObservableCollection<TourStatisticsDTO> MostVisitedTour { get; set; }

        private string _selectedYear;
        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    if (_selectedYear.Equals("Default"))
                        FindMostVisitedTour(0);
                    else
                    {
                        FindMostVisitedTour(int.Parse(_selectedYear));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        public TourStatisticsPage(User user, LocationRepository locationRepository, ImageRepository imageRepository, TourScheduleRepository tourScheduleRepository, TourRepository tourRepository, TourGuestRepository tourGuestRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourRepository = tourRepository;
            _tourScheduleRepository = tourScheduleRepository;
            _tourGuestRepository = tourGuestRepository;
            FinishedTours = new ObservableCollection<TourStatisticsDTO>();
            MostVisitedTour = new ObservableCollection<TourStatisticsDTO>();
            Update();
            FindMostVisitedTour(0);
        }


        private void FindMostVisitedTour(int selectedYear)
        {
            if (FinishedTours.Count == 0) return;

            TourStatisticsDTO mostVisitedTour = GetMostVisitedTour(selectedYear);

            MostVisitedTour.Clear();
            MostVisitedTour.Add(mostVisitedTour);
        }

        private TourStatisticsDTO GetMostVisitedTour(int selectedYear)
        {
            TourStatisticsDTO mostVisitedTour = FinishedTours.First();

            if (datesBox.SelectedIndex != 0 && datesBox.SelectedIndex != -1)
            {
                mostVisitedTour = FindMostVisitedTourByYear(selectedYear);
            }
            else
            {
                mostVisitedTour = FindMostVisitedTourOverall();
            }

            return mostVisitedTour;
        }

        private TourStatisticsDTO FindMostVisitedTourByYear(int selectedYear)
        {
            TourStatisticsDTO? mostVisitedTour = FinishedTours
                .Where(tour => tour.Year == selectedYear)
                .OrderByDescending(tour => tour.TouristNumber)
                .FirstOrDefault();

            return mostVisitedTour ?? FinishedTours.First();
        }
        private TourStatisticsDTO FindMostVisitedTourOverall()
        {
            TourStatisticsDTO? mostVisitedTour = FinishedTours
                .OrderByDescending(tour => tour.TouristNumber)
                .FirstOrDefault();

            return mostVisitedTour ?? FinishedTours.First();
        }


     

        //It is assumed that the statistics are done for the sum of all TourSchedules, if so, we go through the list of Tour
        //and take the tour schedule, for each tour we count the number of children/adults/elderly, we also need to make a function for dates Combobox because of distinct value
        public void Update()
        {
            FinishedTours.Clear();
            List<int> dates = new List<int>();

            foreach (Tour tour in _tourRepository.GetAll())
            {
                Location location = _locationRepository.GetById(tour.LocationId);
                Model.Image image = GetFirstTourImage(tour.Id);
                int touristCount = 0;
                int childrenCount = 0;
                int adultCount = 0;
                int elderlyCount = 0;
                foreach (TourSchedule schedule in _tourScheduleRepository.GetAllByTourId(tour.Id))
                {
                    if (schedule.TourActivity != Enums.TourActivity.Finished) continue;

                    CountGuests(schedule, ref touristCount, ref childrenCount, ref adultCount, ref elderlyCount);

                    FinishedTours.Add(new TourStatisticsDTO(tour.Name, schedule.Start, tour.Language, image.Path, location, touristCount, childrenCount, adultCount, elderlyCount));
                    dates.Add(schedule.Start.Year);
                }
            }

            AddDatesToComboBox(dates);
        }


        private void CountGuests(TourSchedule schedule, ref int touristCount, ref int childrenCount, ref int adultCount, ref int elderlyCount)
        {
            foreach (TourGuests guest in _tourGuestRepository.GetAllByTourId(schedule.Id))
            {
                touristCount++;

                if (guest.Age < 18)
                {
                    childrenCount++;
                }
                else if (guest.Age >= 18 && guest.Age < 50)
                {
                    adultCount++;
                }
                else
                {
                    elderlyCount++;
                }
            }
        }

        private void AddDatesToComboBox(List <int> dates)
        {
            var distinctDates = dates.Distinct().ToList();
            foreach (var date in distinctDates)
            {
                datesBox.Items.Add(date);
            }

            datesBox.Items.Insert(0, "Default");

        }

        private Model.Image GetFirstTourImage(int tourId)
        {
            return _imageRepository.GetByEntity(tourId, Enums.ImageType.Tour).First();
        }
    }
}
