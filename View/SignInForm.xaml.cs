using BookingApp.Model;
using BookingApp.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Resources;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;
        private readonly LocationRepository _locationRepository;
        private readonly ImageRepository _imageRepository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            _locationRepository = new LocationRepository();
            _imageRepository = new ImageRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    switch (user.Type)
                    {


                        case Enums.UserType.Owner:
                            AccommodationViewMenu accommodationViewMenu = new AccommodationViewMenu(user,_locationRepository,_imageRepository);
                            accommodationViewMenu.Show();
                            Close();
                            break;
                        case Enums.UserType.Guest:
                            AccommodationReservationViewMenu accommodationReservationViewMenu = new AccommodationReservationViewMenu(_locationRepository);
                            accommodationReservationViewMenu.Show();
                            Close(); 
                            break;
                        case Enums.UserType.Tourist:
                            //replace with tourist
                            break;
                        case Enums.UserType.Guide:
                            //replace with guide 
                            break;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            
        }
    }
}
