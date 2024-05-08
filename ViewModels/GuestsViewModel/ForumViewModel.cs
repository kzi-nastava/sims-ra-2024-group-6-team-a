using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.ApplicationServices;
using System.Windows.Controls;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class ForumViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ForumsDTO> Forums { get; set; }
        public ForumsDTO Forum { get; set; }
        public Guest Guest { get; set; }
        public List<String> locations { get; set; }

        public NavigationService NavService { get; set; }
        public ForumView forumView { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand CreateForumCommand { get; set; }

        public ForumViewModel(Guest guest, NavigationService navigation)
        {
            Guest = guest;
            NavService = navigation;
            Forums = new ObservableCollection<ForumsDTO>();
            this.locations = new List<String>();

            CreateCommand = new RelayCommand(Execute_CreateCommand);
            CreateForumCommand = new RelayCommand(Execute_CreateForumCommand);
            AddLocations();
            Update();
        }
        private void AddLocations()
        {
            foreach (Location location in LocationService.GetInstance().GetAll())
            {
                locations.Add(location.State + ", " + location.City);
            }
        }
        public void Update() { 
            foreach (var forum in ForumService.GetInstance().GetAll()) {
                string username = UserService.GetInstance().GetUsername(forum.UserId);
                Location location = LocationService.GetInstance().GetById(forum.LocationId);
                Forums.Add(new ForumsDTO(username,location,forum));
            }
        }
        public void Execute_CreateCommand(object obj)
        {
            if (obj is Button button)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }    
        public void Execute_CreateForumCommand(object obj)
        {

            if (SelectedLocation!= null && Commentt != null) {
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to create the forum?", "Create forum", MessageBoxButton.YesNo);
                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        Location location = LocationService.GetInstance().GetByCityAndState(SelectedLocation);
                        Domain.Model.Forums forum = new Domain.Model.Forums(location.Id, Guest.UserId, DateOnly.FromDateTime(DateTime.Today), Commentt, Enums.UserType.Guest);
                        ForumService.GetInstance().Save(forum);
                        NavService.Navigate(new ForumView(Guest, NavService));
                        break;
                    default:
                        break;
                }
            } else MessageBox.Show("The fields are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private string _selectedLocation;
        public string SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation)); 
            }
        }  
        private string _commnett;
        public string Commentt
        {
            get { return _commnett; }
            set
            {
                _commnett = value;
                OnPropertyChanged(nameof(Commentt)); 
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
