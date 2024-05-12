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
            Window.imageListBox.ItemsSource = ImagesCollection;

        }
        private void InitializeLanguages()
        {
            Languages = new List<String>();
            SuggestedLanguage = SimpleRequestService.GetInstance().CalculateMostRequestedLanguage();

            foreach (Language language in LanguageService.GetInstance().GetAll())
            {
                Languages.Add(language.Name);
            }
        }

        private void InitializeLocations()
        {
            SuggestedLocation = SimpleRequestService.GetInstance().CalculateMostRequestedLocation();
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
            DateTime.TryParse(Window.datePicker.Text, out time);
            TourDatesCollection.Add(time);
            Window.datePicker.Text = "";
        }

        public void RemoveDateClick()
        {
            DateTime SelectedDate = SelectedTourDate;
            TourDatesCollection.Remove(SelectedDate);
        }

        bool IsDataValid()
        {
            return !string.IsNullOrEmpty(Window.txtTourName.Text)
                && !string.IsNullOrEmpty(Window.txtTourCapacity.Text)
                && !string.IsNullOrEmpty(Window.txtTourDescription.Text)
                && CheckpointsCollection.Count >= 2
                && ImagesCollection.Count >= 1
                && TourDatesCollection.Count >= 1;


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
            SelectedTour.Type = Enums.TourType.Ordinary; //OVDJE SAM DODALA POSTO SE PRAVI OBICNA TURA########
            SelectedTour.RequestId = -1;//###############################################


            CheckRadioButtonsStatus();

            SelectedTour = TourService.GetInstance().Save(SelectedTour);


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
