using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using BookingApp.Model;
using BookingApp.Resources;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window
    {
        public User LoggedUser { get; set; }

        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ImageRepository _imageRepository;
        private readonly CheckpointRepository _checkRepository;
        private readonly TourScheduleRepository _tourScheduleRepository;


        public Tour SelectedTour {  get; set; }
        public Location SelectedLocation {  get; set; }
        public Model.Image SelectedImage {  get; set; }

        public List <String> CheckPoints { get; set; }

        public List <DateTime> TourSchedules { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       

        private List<string> _imagePath;
        public List<string> ImagePath
        {
            get => _imagePath;
            set
            {
                if (value != _imagePath)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = user;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
            SelectedLocation = new Location();
            SelectedTour = new Tour();
            SelectedImage = new Model.Image();
            CheckPoints = new List<String>();
            _checkRepository = new CheckpointRepository();
            TourSchedules = new List<DateTime>();
            _tourScheduleRepository = new TourScheduleRepository();
        }

       
        private void AddCheckPointClick(object sender, EventArgs e)
        {

            CheckPoints.Add(txtTourCheckpoints.Text);
            txtTourCheckpoints.Clear();
        }

        
        
        private void SelectImagesClick(object sender, RoutedEventArgs e)
        {
            _imagePath = new List<string>();    
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Slike|*.jpg;*.jpeg;*.png;*.gif|Svi fajlovi|*.*";
            openFileDialog.Multiselect = true; 
            
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string imagePath in openFileDialog.FileNames)
                {
                    _imagePath.Add(imagePath);
                }
            }

            if(_imagePath.Count == 1)
                 txtImageNumber.Text = "Attached " + _imagePath.Count + " image";
            else
            {
                txtImageNumber.Text = "Attached " + _imagePath.Count + " images";
            }
        }

        private void SelectDatesClick(object sender, RoutedEventArgs e)
        {
            DateTime time;
            DateTime.TryParse(txtTourScheduleDate.Text + " " + txtTourScheduleTime.Text,out time);
            TourSchedules.Add(time);
            txtTourScheduleDate.Clear();
            txtTourScheduleTime.Clear();
        }


        private void SaveTourClick(object sender, RoutedEventArgs e)
        {
            SelectedLocation.State = txtTourState.Text;
            SelectedLocation.City = txtTourCity.Text;
            SelectedLocation = _locationRepository.Save(SelectedLocation);

            SelectedTour.Name = txtTourName.Text;
            SelectedTour.Description = txtTourDescription.Text;
            SelectedTour.LocationId = SelectedLocation.Id;
            SelectedTour.Language = txtTourLanguage.Text;
            SelectedTour.Capacity = Convert.ToInt32(txtTourCapacity.Text);
            SelectedTour.Duration = Convert.ToInt32(txtTourDuration.Text);
            SelectedTour.GuideId = LoggedUser.Id;

            SelectedTour = _tourRepository.Save(SelectedTour);


            foreach(string checkPoint in CheckPoints)
            {
                _checkRepository.Save(new Checkpoint(checkPoint, SelectedTour.Id)); 
            }
            foreach (string imagePath in _imagePath)
            {
                _imageRepository.Save(new Model.Image(imagePath, SelectedTour.Id, Enums.ImageType.Tour));
            }
            
            foreach(DateTime date in TourSchedules)
            {
                _tourScheduleRepository.Save(new TourSchedule(date, SelectedTour.Id,SelectedTour.Capacity));
            }

            Close();
        }
    }
}
