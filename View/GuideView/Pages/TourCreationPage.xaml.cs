using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View.GuideView.Pages
{
    /// <summary>
    /// Interaction logic for TourCreationPage.xaml
    /// </summary>
    public partial class TourCreationPage : Page, INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }

        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ImageRepository _imageRepository;
        private readonly CheckpointRepository _checkRepository;
        private readonly TourScheduleRepository _tourScheduleRepository;

        public Tour SelectedTour { get; set; }
        public Location SelectedLocation { get; set; }
        public String SelectedImageUrl { get; set; }
        public String SelectedCheckpoint { get; set; }

        public DateTime SelectedTourDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        List<String> _imageRelativePath = new List<String>();

        public static ObservableCollection<String> ImagesCollection { get; set; }

        public static ObservableCollection<String> CheckpointsCollection { get; set; }

        public static ObservableCollection<DateTime> TourDatesCollection { get; set; }


        public event EventHandler SomethingHappened;


        protected virtual void OnSomethingHappened(EventArgs e)
        {
            SomethingHappened?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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




        public TourCreationPage(User user, TourRepository tourRepository, LocationRepository locationRepository, ImageRepository imageRepository, CheckpointRepository checkpointRepository, TourScheduleRepository tourScheduleRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;

            _tourRepository = tourRepository;
            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            _tourScheduleRepository = tourScheduleRepository;
            _checkRepository = checkpointRepository;

            SelectedLocation = new Location();
            SelectedTour = new Tour();
            SelectedTourDate = new DateTime();
            CheckpointsCollection = new ObservableCollection<string>();
            ImagesCollection = new ObservableCollection<string>();
            TourDatesCollection = new ObservableCollection<DateTime>();
        }



        private void AddCheckPointClick(object sender, EventArgs e)
        {

            CheckpointsCollection.Add(checkpoint);
            txtTourCheckpoints.Clear();
        }



        private void SelectImagesClick(object sender, RoutedEventArgs e)
        {
            List<String> _imagePath = new List<String>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Multiselect = true;


            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string imagePath in openFileDialog.FileNames)
                {
                    _imagePath.Add(imagePath);
                }
            }
            TransformToRelativePath(_imagePath);
        }

       
        private void TransformToRelativePath(List<String> imagePath)
        {
            foreach (String imgPath in imagePath)
            {
                int relativePathStartIndex = imgPath.IndexOf("\\Resources");
                String relativePath = imgPath.Substring(relativePathStartIndex);
                ImagesCollection.Add(relativePath);
            }
        }



        private void SelectDatesClick(object sender, RoutedEventArgs e)
        {
            DateTime time;

            DateTime.TryParse(datePicker.SelectedDate.Value.Date.ToShortDateString() + " " + txtTourScheduleTime.Text, out time);
            TourDatesCollection.Add(time);
            datePicker.SelectedDate = null;
            txtTourScheduleTime.Clear();
        }

        private void RemoveDateClick(object sender, RoutedEventArgs e)
        {
            DateTime SelectedDate = SelectedTourDate;
            TourDatesCollection.Remove(SelectedDate);
        }



        private void SaveTourClick(object sender, RoutedEventArgs e)
        {

            SelectedLocation = _locationRepository.Save(SelectedLocation);
            SelectedTour.LocationId = SelectedLocation.Id;
            SelectedTour.GuideId = LoggedUser.Id;
            SelectedTour = _tourRepository.Save(SelectedTour);


            SaveCheckpoints(CheckpointsCollection.ToList());
            SaveImages(ImagesCollection.ToList());
            SaveTourDates(TourDatesCollection.ToList());
            OnSomethingHappened(EventArgs.Empty);


        }

        private void SaveCheckpoints(List<String> checkpoints)
        {
            foreach (string checkpoint in checkpoints)
            {
                _checkRepository.Save(new Checkpoint(checkpoint, SelectedTour.Id,false));
            }
        }

        private void SaveImages(List<String> images)
        {
            foreach (string relativePath in images)
            {
                _imageRepository.Save(new Model.Image(relativePath, SelectedTour.Id, Enums.ImageType.Tour));
            }
        }

        private void SaveTourDates(List<DateTime> dates)
        {
            foreach (DateTime date in dates)
            {
                _tourScheduleRepository.Save(new TourSchedule(date, SelectedTour.Id, SelectedTour.Capacity,Enums.TourActivity.Ready));
            }
        }

        private void CheckpointRemoveClick(object sender, EventArgs e)
        {
            string checkpoint = SelectedCheckpoint;
            CheckpointsCollection.Remove(checkpoint);
        }

        private void ImageRemoveClick(object sender, EventArgs e)
        {
            string imageUrl = SelectedImageUrl;
            ImagesCollection.Remove(imageUrl);
        }

    }
}

