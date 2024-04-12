using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TouristView
{
    /// <summary>
    /// Interaction logic for ActiveTours.xaml
    /// </summary>
    public partial class ActiveTours : Window
    {
        public static ObservableCollection<TourScheduleDTO> Tours { get; set; }
        public TourScheduleDTO SelectedTourSchedule { get; set; }
        public User LoggedUser { get; set; }

        private LocationRepository _locationRepository;
        private TourRepository _tourRepository;
        private TourScheduleRepository _scheduleRepository;
        private TourReservationRepository _tourReservationRepository;
        public ActiveTours(TourScheduleDTO tourSchedule, User user)
        {
            InitializeComponent();
            DataContext = this;

            _tourRepository = new TourRepository();
            _scheduleRepository = new TourScheduleRepository();
            _locationRepository = new LocationRepository();
            _tourReservationRepository = new TourReservationRepository();
            LoggedUser = user;

            Tours = new ObservableCollection<TourScheduleDTO>();
            SelectedTourSchedule = tourSchedule;

            Update();
        }

        private void Update() //prodji kroz sve aktivne ture mog usera i dodaj ih u kolekciju
        {
            Tours.Clear();
            foreach(TourSchedule tour in _tourRepository.GetOngoingToursByUser(LoggedUser))
            {
                Tours.Add(new TourScheduleDTO(tour));
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (SelectedTourSchedule != null)
            {
                KeypointsTracking keypointsTracking = new KeypointsTracking(SelectedTourSchedule.Id);
                keypointsTracking.ShowDialog();

            }
           
        }

        //zelim da prikazem ture koje je rezervisao ulogovani korisnik a da su one ONGOING 
    }
}
