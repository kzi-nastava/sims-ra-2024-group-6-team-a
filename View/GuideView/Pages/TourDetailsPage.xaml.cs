using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Interaction logic for TourDetailsPage.xaml
    /// </summary>
    public partial class TourDetailsPage : Page, INotifyPropertyChanged
    {

        public TourDetailsDTO SelectedTour { get; set; }

        private int tourScheduleId;

        public static ObservableCollection<ImageItemDTO> ImagesCollection { get; set; }
        public  ImageItemDTO SelectedImage { get; set; }
        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }

        public string BigImage { get; set; }


        public TourDetailsPage(int scheduleId)
        {
            InitializeComponent();
            DataContext = this;
            tourScheduleId = scheduleId;
            ImagesCollection = new ObservableCollection<ImageItemDTO>();
            UpdateDetails();

            imageListBox.ItemsSource = ImagesCollection;
        }

        public void UpdateDetails()
        {
            List<String> images = new List<String>();

            TourSchedule tourSchedule = TourScheduleService.GetInstance().GetById(tourScheduleId);

            Tour tour = TourService.GetInstance().GetById(tourSchedule.TourId);
            List<String> checkpoints = CheckpointService.GetInstance().GetAllNamesByTourId(tourScheduleId);
            images = ImageService.GetInstance().GetAllImages(tour.Id);
            UpdateImages(images);
            List<DateTime> realisationDates = TourScheduleService.GetInstance().GetAllRealisationDates(tour.Id);
            Language language = LanguageService.GetInstance().GetById(tour.LanguageId);
            Location location = LocationService.GetInstance().GetById(tour.LocationId);

            SelectedTour = new TourDetailsDTO(tour, language, location, checkpoints, realisationDates);
            NextImageCommand = new RelayCommand(NextImageClick);
            PreviousImageCommand = new RelayCommand(PreviousImageClick);

        }
        public void NextImageClick(object obj)
        {
            if (ImageIndex < ImagesCollection.Count - 1) ImageIndex++;
            else ImageIndex = 0;
        }
        public void PreviousImageClick(object obj)
        {
            if (ImageIndex > 0) ImageIndex--;
            else ImageIndex = ImagesCollection.Count - 1;
        }

        private int _imageIndex = 0;
        public int ImageIndex
        {
            get => _imageIndex;
            set
            {
                _imageIndex = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }

        public ImageItemDTO CurrentImage => ImagesCollection[ImageIndex];


        private void ChangeBigImage(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedImage != null)
            ImageIndex = imageListBox.SelectedIndex;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateImages(List <String> images)
        {
            ImagesCollection.Clear();
            foreach(string image in images)
            {
                BitmapImage imageSource = new BitmapImage(new Uri(image, UriKind.Relative));
                ImagesCollection.Add(new ImageItemDTO (image, imageSource));  
            }
        }



    }
}
