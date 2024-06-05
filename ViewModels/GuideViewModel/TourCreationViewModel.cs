using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows;
using System.ComponentModel;
using BookingApp.View.GuideView.Pages;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class TourCreationViewModel: INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }

        public Tour SelectedTour { get; set; }
        public Location SelectedLocation { get; set; }

        public Location SuggestedLocation { get; set; }
        public Language SuggestedLanguage { get; set; }

        public ImageItemDTO SelectedImageUrl { get; set; }
        public String SelectedCheckpoint { get; set; }

        public DateTime SelectedTourDate { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Locations { get; set; }

        public List<string> Languages { get; set; }

        public static ObservableCollection<ImageItemDTO> ImagesCollection { get; set; }
        public static ObservableCollection<String> CheckpointsCollection { get; set; }
        public static ObservableCollection<DateTime> TourDatesCollection { get; set; }

        public event EventHandler SomethingHappened;

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
        public TourCreationPage Window {  get; set; }

        public TourCreationViewModel(TourCreationPage window, User user)
        {
            this.Window = window;
            LoggedUser = user;
            InitializeLocations();
            InitializeLanguages();


            SelectedLocation = new Location();
            SelectedTour = new Tour();
            SelectedTourDate = new DateTime();
            CheckpointsCollection = new ObservableCollection<string>();
            ImagesCollection = new ObservableCollection<ImageItemDTO>();
            TourDatesCollection = new ObservableCollection<DateTime>();
            Window.datePicker.FormatString = "dd.MM.yyyy HH:mm";
            Window.datePicker.Minimum = DateTime.Now;

            Window.imageListBox.ItemsSource = ImagesCollection;

        }
        private void InitializeLanguages()
        {
            Languages = new List<String>();
            SuggestedLanguage = TourRequestService.GetInstance().CalculateMostRequestedLanguage();

            foreach (Language language in LanguageService.GetInstance().GetAll())
            {
                Languages.Add(language.Name);
            }
        }

        private void InitializeLocations()
        {
            SuggestedLocation = TourRequestService.GetInstance().CalculateMostRequestedLocation();
            Locations = new List<String>();
            foreach (Location location in LocationService.GetInstance().GetAll())
            {
                Locations.Add(location.City + " , " + location.State);
            }
        }

        protected virtual void OnSomethingHappened(EventArgs e)
        {
            SomethingHappened?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

            if(String.IsNullOrWhiteSpace(Window.datePicker.Text))
            {
                Window.txtDatePickerValidation.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                Window.txtDatePickerValidation.Visibility = Visibility.Collapsed;
            }

            DateTime.TryParse(Window.datePicker.Text, out time);
            TourDatesCollection.Add(time);
            Window.datePicker.Text = "";
        }

        public void RemoveDateClick()
        {
            DateTime SelectedDate = SelectedTourDate;
            TourDatesCollection.Remove(SelectedDate);
        }

        public bool IsDataValid()
        {
            if(String.IsNullOrWhiteSpace(Window.txtTourName.Text))
            {
                Window.txtTourNameValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtTourNameValidation.Visibility = Visibility.Collapsed;
            }
            if(Window.locationComboBox.SelectedIndex == -1 && Window.locationRadioButton.IsChecked==false)
            {
                Window.txtLocationValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtLocationValidation.Visibility = Visibility.Collapsed;
            }
            if(Window.languageComboBox.SelectedIndex == -1 && Window.languageRadioButton.IsChecked == false)
            {
                Window.txtLanguageValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtLanguageValidation.Visibility = Visibility.Collapsed;
            }
            if(String.IsNullOrEmpty(Window.txtTourDuration.Text))
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
            if (String.IsNullOrEmpty(Window.txtTourDescription.Text))
            {
                Window.txtTourDecsriptionValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtTourDecsriptionValidation.Visibility = Visibility.Collapsed;
            }

            if (CheckpointsCollection.Count() < 2)
            {
                Window.txtCheckpointTableValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtCheckpointTableValidation.Visibility = Visibility.Collapsed;
            }
            if(TourDatesCollection.Count() < 1)
            {
                Window.txtDateTableValidation.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtDateTableValidation.Visibility = Visibility.Collapsed;
            }
            if(ImagesCollection.Count() < 1 )
            {
                Window.txtImageTable.Visibility = Visibility.Visible;
            }
            else
            {
                Window.txtImageTable.Visibility = Visibility.Collapsed;
            }




            return !string.IsNullOrEmpty(Window.txtTourName.Text)
                && !string.IsNullOrEmpty(Window.txtTourCapacity.Text)
                && !string.IsNullOrEmpty(Window.txtTourDescription.Text)
                && !string.IsNullOrEmpty(Window.txtTourDuration.Text)
                && CheckpointsCollection.Count >= 2
                && ImagesCollection.Count >= 1
                && TourDatesCollection.Count >= 1;


        }

        private bool IsTextAllowed(string text)
        {
            // Only allow numeric input
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        internal void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        internal void DurationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public void CreateTourClick()
        {
            if (!IsDataValid())
            {
                return;
            }
            SelectedTour.LocationId = Window.locationComboBox.SelectedIndex + 1;
            SelectedTour.LanguageId = Window.languageComboBox.SelectedIndex + 1;
            SelectedTour.GuideId = LoggedUser.Id;
            SelectedTour.Type = Enums.TourType.Ordinary;
            SelectedTour.RequestId = -1;


            CheckRadioButtonsStatus();

            SelectedTour = TourService.GetInstance().Save(SelectedTour);

            if(SelectedTour.Type == Enums.TourType.Statistics)
            {
                foreach (Tourist tourist in TourRequestService.GetInstance().GetTouristsForNotification(SelectedTour))
                {
                    TouristNotificationService.GetInstance().SendStatisticTourNotification(tourist.UserId, SelectedTour.Id);

                }
            }  


            SaveImages();
            SaveTourDatesAndCheckpoints(TourDatesCollection.ToList(), CheckpointsCollection.ToList());
            OnSomethingHappened(EventArgs.Empty);

            MessageBox.Show("Tour Added");
        }

        public void CheckRadioButtonsStatus()
        {
            if(Window.locationRadioButton.IsChecked == true)
            {
                SelectedTour.LocationId = SuggestedLocation.Id;
                SelectedTour.Type = Enums.TourType.Statistics;
            }
            if (Window.languageRadioButton.IsChecked == true)
            {
                SelectedTour.LanguageId = SuggestedLanguage.Id;
                SelectedTour.Type = Enums.TourType.Statistics;
            }
        }


        public void SaveImages()
        {
            foreach (ImageItemDTO imageItem in ImagesCollection)
            {
                ImageService.GetInstance().Save(new Model.Image(imageItem.ImagePath, SelectedTour.Id, Enums.ImageType.Tour));
            }
        }

        public void SaveTourDatesAndCheckpoints(List<DateTime> dates, List<String> checkpoints)
        {
            foreach (DateTime date in dates)
            {
                TourSchedule tourSchedule = TourScheduleService.GetInstance().Save(new TourSchedule(date, SelectedTour.Id, SelectedTour.Capacity, Enums.TourActivity.Ready));
                foreach (string checkpoint in checkpoints)
                {
                    CheckpointService.GetInstance().Save(new Checkpoint(checkpoint, SelectedTour.Id, false, tourSchedule.Id));
                }
            }
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
