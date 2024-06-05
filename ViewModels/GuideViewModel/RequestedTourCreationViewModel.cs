using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using BookingApp.View.TouristView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Resources;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class RequestedTourCreationViewModel
    {
        public RequestedTourCreation Window { get; set; }
        public Tour Tour { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public TourRequest Request { get; set; }
        public int RequestId { get; set; }

        public ImageItemDTO SelectedImageUrl { get; set; }
        public String SelectedCheckpoint { get; set; }
        public DateTime SelectedTourDate { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> locations { get; set; }
        public List<string> languages { get; set; }

        public static ObservableCollection<ImageItemDTO> ImagesCollection { get; set; }
        public static ObservableCollection<String> CheckpointsCollection { get; set; }
        public static ObservableCollection<DateTime> TourDatesCollection { get; set; }
        
        public static ObservableCollection<TourGuests> Tourists { get; set; }        

        private DateTime _tourDate;
        public DateTime TourDate
        {
            get
            {
                return _tourDate;
            }
            set
            {
                if (value != _tourDate)
                {
                    _tourDate = value;
                    OnPropertyChanged("TourDate");

                }

            }
        }

        private string checkpoint;
        public string Checkpoint
        {
            get
            {
                return checkpoint;
            }
            set
            {
                if (value != checkpoint)
                {
                    checkpoint = value;
                    OnPropertyChanged("Checkpoint");
                }
            }
        }
        public int UserId { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RequestedTourCreationViewModel(RequestedTourCreation window, int requestId, int userId)
        {
            this.Window = window;
            RequestId = requestId;
            UserId = userId;

            Request = new TourRequest();
            Tour = new Tour();
            SelectedTourDate = new DateTime();
            CheckpointsCollection = new ObservableCollection<string>();
            Tourists = new ObservableCollection<TourGuests>();
            ImagesCollection = new ObservableCollection<ImageItemDTO>();
            TourDatesCollection = new ObservableCollection<DateTime>();
            Window.imageListBox.ItemsSource = ImagesCollection;
            LoadRequestDetails();
            Window.datePicker.FormatString = "dd.MM.yyyy HH:mm";
            LoadTourists();

        }

        public void LoadTourists() 
        {
            List<TourGuests> guests = TourGuestService.GetInstance().GetAllByRequestId(RequestId);
            foreach (var guest in guests)
            {
                Tourists.Add(guest);
            }
        }



        public void LoadRequestDetails()
        {
            Request = TourRequestService.GetInstance().GetById(RequestId);


           Window.datePicker.Kind = DateTimeKind.Local;
            

            Window.datePicker.Minimum = Request.StartDate.ToDateTime(new TimeOnly(0, 0));
            Window.datePicker.Maximum = Request.EndDate.ToDateTime(new TimeOnly(23, 59));

            Location location = LocationService.GetInstance().GetById(Request.LocationId);
            Location = location.City + ", " + location.State;
            Tour.LocationId = Request.LocationId;
            Tour.LanguageId = Request.LanguageId;
            Tour.Description = Request.Description;
            Tour.Capacity = TourGuestService.GetInstance().GetGuestsCountByRequest(RequestId);
            Language = LanguageService.GetInstance().GetById(Request.LanguageId).Name;
        }

        public void AddCheckPointClick()
        {
            if (String.IsNullOrWhiteSpace(Window.txtTourCheckpoints.Text))
            {
                Window.txtCheckpointAddValidation.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                Window.txtCheckpointAddValidation.Visibility = Visibility.Collapsed;
            }

            CheckpointsCollection.Add(checkpoint);
            Window.txtTourCheckpoints.Clear();
        }

        public void SelectImagesClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string imagePath in openFileDialog.FileNames)
                {
                    int relativePathStartIndex = imagePath.IndexOf("\\Resources");
                    String relativePath = imagePath.Substring(relativePathStartIndex);
                    BitmapImage imageSource = new BitmapImage(new Uri(imagePath));
                    ImagesCollection.Add(new ImageItemDTO(relativePath, imageSource));
                }
            }
        }

        public void SelectDatesClick()
        {
            DateTime time;

            if (String.IsNullOrWhiteSpace(Window.datePicker.Text))
            {
                Window.txtDatePickerValidation.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                Window.txtDatePickerValidation.Visibility = Visibility.Collapsed;
            }


            DateTime.TryParse(Window.datePicker.Text, out time);
            if (CheckDateConditions(time))
            {
                TourDatesCollection.Add(time);
                Window.addTourButton.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("The Guide is busy at that time!");
            }
        }
                
          

        private bool CheckDateConditions(DateTime time)
        {
            User user = UserService.GetInstance().GetById(UserId);
            var tours = TourService.GetInstance().GetAllByUser(user);
            foreach (var tour in tours)
            {
                foreach(var tourSchedule in TourScheduleService.GetInstance().GetAllByTourId(tour.Id))
                {
                    if (time < tourSchedule.Start.AddHours(tour.Duration) && time > tourSchedule.Start)
                    {
                        return false;
                    }
                }  
            }
            return true;
        }


        public void RemoveDateClick()
        {
            DateTime SelectedDate = SelectedTourDate;
            TourDatesCollection.Remove(SelectedDate);
            Window.addTourButton.IsEnabled = true;

        }

        bool IsDataValid()
        {
       
            if (String.IsNullOrEmpty(Window.txtTourDuration.Text))
            {
                Window.txtDurationValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtDurationValidation.Visibility = Visibility.Collapsed;
            }
            if (String.IsNullOrEmpty(Window.txtTourCapacity.Text))
            {
                Window.txtTourCapacityValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtTourCapacityValidation.Visibility = Visibility.Collapsed;
            }

            if (CheckpointsCollection.Count() < 2)
            {
                Window.txtCheckpointTableValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtCheckpointTableValidation.Visibility = Visibility.Collapsed;
            }
            if (TourDatesCollection.Count() != 1)
            {
                Window.txtDateTableValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtDateTableValidation.Visibility = Visibility.Collapsed;
            }
            if (ImagesCollection.Count() < 1)
            {
                Window.txtImageTable.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtImageTable.Visibility = Visibility.Collapsed;
            }



            return      !string.IsNullOrEmpty(Window.txtTourCapacity.Text)
                       && !string.IsNullOrEmpty(Window.txtTourDuration.Text)
                       && CheckpointsCollection.Count >= 2
                       && ImagesCollection.Count >= 1
                       && TourDatesCollection.Count == 1;
        }


        private bool IsTextAllowed(string text)
        {
            // Only allow numeric input
            Regex regex = new Regex("^[1-9][0-9]+$");
            return regex.IsMatch(text);
        }

        internal void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        public void CreateTourClick()
        {
            if (!IsDataValid())
            {
                return;
            }


            Tour.GuideId = UserId;
            Tour.Type = Enums.TourType.Requested; //OVDJE SAM DODALA POSTO SE PRAVI OBICNA TURA########
            Tour.RequestId = RequestId;//###############################################
            Tour = TourService.GetInstance().Save(Tour);
            ChangeRequestStatus();
            SaveImages();
            SaveTourDatesAndCheckpoints(TourDatesCollection.ToList(), CheckpointsCollection.ToList());

            MessageBox.Show("Tour Added");
        }
        private void ChangeRequestStatus()
        {
            Request.Status = Enums.RequestStatus.Accepted;
            Request.StartDate = DateOnly.FromDateTime(TourDatesCollection.First());
            Request = TourRequestService.GetInstance().Update(Request);
        }
        private TourReservation CreateReservation(TourSchedule tourSchedule)
        {
            int touristsNumber = TourGuestService.GetInstance().GetGuestsCountByRequest(RequestId);
            TourReservation reservation = new TourReservation(touristsNumber, tourSchedule.Id, tourSchedule.TourId, Request.TouristId);
            reservation = TourReservationService.GetInstance().Save(reservation);

            return reservation;
        }

        private void SaveImages()
        {
            ImageService.GetInstance().SaveAllImages(ImagesCollection, Tour.Id);
        }

        public void SaveTourDatesAndCheckpoints(List<DateTime> dates, List<String> checkpoints)
        {
            
            TourSchedule tourSchedule = TourScheduleService.GetInstance().Save(new TourSchedule(dates.First(), Tour.Id, Tour.Capacity, Enums.TourActivity.Ready));
            CheckpointService.GetInstance().SaveAll(checkpoints, Tour.Id, tourSchedule.Id);
            TourReservation reservation = CreateReservation(tourSchedule);
            UpdateGuestReservation(reservation);
            TouristNotificationService.GetInstance().SendRequestNotification(tourSchedule, Request, UserId);
            
        }

        public void UpdateGuestReservation(TourReservation reservation)
        {
            TourGuestService.GetInstance().UpdateGuestReservation(reservation, RequestId);
        }

        public void CheckpointRemoveClick()
        {
            string checkpoint = SelectedCheckpoint;
            CheckpointsCollection.Remove(checkpoint);
        }

        public void ImageRemoveClick()
        {
            ImagesCollection.Remove(SelectedImageUrl);
        }

        public void CancelCreationClick()
        {
            Window.NavigationService.GoBack();
        }


    }
}
