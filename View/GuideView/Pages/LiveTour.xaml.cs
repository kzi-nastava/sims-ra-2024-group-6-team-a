using BookingApp.Model;
using BookingApp.Observer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.Observer;
using BookingApp.Resources;
using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using System.ComponentModel;

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for LiveTour.xaml
    /// </summary>
    public partial class LiveTour : Page, INotifyPropertyChanged
    {


        public static ObservableCollection<Checkpoint> Checkpoints { get; set; }

        public static ObservableCollection<TourGuests> TourGuests { get; set; }

       


        public Tour SelectedTour { get; set; }
        public TourSchedule SelectedTourSchedule { get; set; }
        public Location SelectedLocation { get; set; }
        public Language SelectedLanguage {  get; set; }

        private Checkpoint _selectedCheckpoint;
        public Checkpoint SelectedCheckpoint
        {
            get { return _selectedCheckpoint; }
            set
            {
                if (_selectedCheckpoint != value)
                {
                    _selectedCheckpoint = value;
                    OnPropertyChanged(nameof(SelectedCheckpoint));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int TouristNumber {  get; set; }

        public LiveTour(int tourScheduleId)
        {
            InitializeComponent();
            DataContext = this;

           

            Checkpoints = new ObservableCollection<Checkpoint>();
            TourGuests = new ObservableCollection<TourGuests>();

            SelectedTourSchedule = TourScheduleService.GetInstance().GetById(tourScheduleId);
            SelectedTour = TourService.GetInstance().GetById(SelectedTourSchedule.TourId);
            SelectedLocation = LocationService.GetInstance().GetById(SelectedTour.LocationId);
            SelectedLanguage = LanguageService.GetInstance().GetById(SelectedTour.LocationId);
            UpdateCheckpoints();
            SelectedCheckpoint  = Checkpoints.FirstOrDefault(checkpoint => !checkpoint.IsReached);;
            UpdateGuests();
            TouristNumber = TourGuests.Count;


        }

        public void UpdateCheckpoints()
        {
            Checkpoints.Clear();
            foreach (Checkpoint checkpoint in CheckpointService.GetInstance().GetAllByTourScheduleId(SelectedTourSchedule.Id))
            {
                Checkpoints.Add(checkpoint);
            }
        }

        public void UpdateGuests()
        {
            TourGuests.Clear();
            foreach (TourGuests tourGuest in TourGuestService.GetInstance().GetAllByTourId(SelectedTourSchedule.Id))
            {
                if(tourGuest.IsPresent == false)
                TourGuests.Add(tourGuest);
            }
        }


        private void NextCheckpointClick(object sender, RoutedEventArgs e)
        {
            
            UpdateTourGuests();
            MarkCheckpointReached();
        }

        private void MarkCheckpointReached()
        {
            int currentIndex = Checkpoints.IndexOf(SelectedCheckpoint);


            SelectedCheckpoint.IsReached = true;
            CheckpointService.GetInstance().Update(SelectedCheckpoint);
            UpdateCheckpoints();

            if (currentIndex + 1 >= Checkpoints.Count)
            {
                TourEndedNotificationClick();
                return;
            }

            SelectedCheckpoint = Checkpoints[currentIndex + 1];
            
        }

        private void TourGuestsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (TourGuests tourist in e.RemovedItems)
            {
                tourist.IsPresent = false;
            }

            foreach (TourGuests tourist in e.AddedItems)
            {
                tourist.IsPresent = true;

                if (tourist.UserTypeId != -1)
                {                    
                    VisitedTourService.GetInstance().Save(tourist.UserTypeId, SelectedTourSchedule.Id);
                }
            }
        }

        private void UpdateTourGuests()
        {
            foreach (var tourist in TourGuests)
            {
                if (tourist.IsPresent)
                {
                    tourist.CheckpointId = SelectedCheckpoint.Id;
                    TourGuestService.GetInstance().Update(tourist);
                }
            }
                        UpdateGuests();

        }

        private void TourEndedClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to end the tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
               
                TourEndedNotificationClick();
               
            }
        }


        private void TourEndedNotificationClick()
        {
            MessageBox.Show("Congratulations, the \" "+ SelectedTour.Name + "\" tour has been successfully completed.", "Tour Status", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedTourSchedule.TourActivity = Enums.TourActivity.Finished;
            TourScheduleService.GetInstance().Update(SelectedTourSchedule);           
            //RADI OVO, GORE PUCA NULL             
            GoBackIfPossible();
            TouristNotificationService.GetInstance().SendNotification(SelectedTourSchedule);
        }

       
        private void GoBackIfPossible()
        {

            LiveToursPage liveToursPage = new LiveToursPage(UserService.GetInstance().GetById(SelectedTour.GuideId));
                NavigationService.Navigate(liveToursPage);
            
        }

        public void GoBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
