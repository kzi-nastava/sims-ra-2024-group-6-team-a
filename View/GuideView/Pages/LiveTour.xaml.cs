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

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for LiveTour.xaml
    /// </summary>
    public partial class LiveTour : Page
    {


        public static ObservableCollection<Checkpoint> Checkpoints { get; set; }

        public static ObservableCollection<TourGuests> TourGuests { get; set; }

       


        public Tour SelectedTour { get; set; }
        public TourSchedule SelectedTourSchedule { get; set; }
        public Location SelectedLocation { get; set; }

        public event EventHandler TourEnded;
        public event EventHandler TourEndedMainWindow;

        public LiveTour(int tourScheduleId)
        {
            InitializeComponent();
            DataContext = this;

           

            Checkpoints = new ObservableCollection<Checkpoint>();
            TourGuests = new ObservableCollection<TourGuests>();

            SelectedTourSchedule = TourScheduleService.GetInstance().GetById(tourScheduleId);
            SelectedTour = TourService.GetInstance().GetById(SelectedTourSchedule.TourId);
            SelectedLocation = LocationService.GetInstance().GetById(SelectedTour.LocationId);

            UpdateCheckpoints();
            UpdateGuests();

        }

        public void UpdateCheckpoints()
        {
            Checkpoints.Clear();
            foreach (Checkpoint checkpoint in CheckpointService.GetInstance().GetAllByTourScheduleId(SelectedTourSchedule.Id))
            {

                Checkpoints.Add(checkpoint);
            }
            Checkpoints.ElementAt(0).IsReached = true;
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


        private void CheckpointCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Checkpoint selectedCheckpoint = (sender as CheckBox).DataContext as Checkpoint;
            CheckBox checkbox = sender as CheckBox;


            DisableCheckbox(checkbox, e);

            MarkCheckpointReached(selectedCheckpoint);

            

            UpdateTourGuests(selectedCheckpoint);
            HasTourEnded(selectedCheckpoint, sender, e);
        }

        private void MarkCheckpointReached(Checkpoint checkpoint)
        {
            checkpoint.IsReached = true;
            CheckpointService.GetInstance().Update(checkpoint);
        }



        private void UpdateTourGuests(Checkpoint selectedCheckpoint)
        {
            foreach (var tourist in TourGuests)
            {
                if (tourist.IsPresent)
                {
                    tourist.CheckpointId = selectedCheckpoint.Id;
                    TourGuestService.GetInstance().Update(tourist);
                }
            }
                        UpdateGuests();

        }


        private void DisableCheckbox(CheckBox checkbox, RoutedEventArgs e)
        {
            if (checkbox != null && checkbox.IsChecked == true)
            {
                checkbox.IsEnabled = false;
                e.Handled = true;
            }
        }

        private void HasTourEnded(Checkpoint selectedCheckpoint,object sender,RoutedEventArgs e)
        {
            if (Checkpoints.Last() == selectedCheckpoint)
            {
                TourEndedNotificationClick(sender, e);
                //TouristNotificationService.GetInstance().SendNotification(SelectedTourSchedule);
            }
        }

      

        private void TourEndedNotificationClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tour ended.", "Tour Status", MessageBoxButton.OK, MessageBoxImage.Information);
            SelectedTourSchedule.TourActivity = Enums.TourActivity.Finished;
            TourScheduleService.GetInstance().Update(SelectedTourSchedule);
            
            

            RaiseTourEndedEvents();
            
            GoBackIfPossible();
            TouristNotificationService.GetInstance().SendNotification(SelectedTourSchedule);
        }

        private void RaiseTourEndedEvents()
        {
            RaiseTourEndedEvent();
            RaiseTourEndedEventMain();
        }


        
        private void RaiseTourEndedEventMain()
        {
            TourEndedMainWindow?.Invoke(this, EventArgs.Empty);
        }


        private void RaiseTourEndedEvent()
        {

            TourEnded?.Invoke(this, EventArgs.Empty);

        }

        private void GoBackIfPossible()
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

    }
}
