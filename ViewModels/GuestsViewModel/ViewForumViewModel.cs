using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Resources;
using BookingApp.View.GuestViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using System.Windows.Controls;

namespace BookingApp.ViewModels.GuestsViewModel
{
    public class ViewForumViewModel : INotifyPropertyChanged
    {
        public ForumsDTO Forum { get; set; }
        public ObservableCollection<ForumViewDTO> ForumsComment { get; set; }
        public RelayCommand AddCommentCommand { get; set; }
        public RelayCommand CreateCommentCommand { get; set; }

        public Guest Guest { get; set; }
        public NavigationService NavService { get; set; }
        public ViewForumView ForumView { get; set; }
        public ViewForumViewModel(Guest guest, ForumsDTO SelectedForum, ViewForumView forumView, NavigationService navigation)
        {

            Guest = guest;
            Forum = SelectedForum;
            ForumView = forumView;
            NavService = navigation;
            ForumsComment = new ObservableCollection<ForumViewDTO>();
            AddCommentCommand = new RelayCommand(Execute_AddCommentCommand);
            CreateCommentCommand = new RelayCommand(Execute_CreateCommentCommand);
            Update();
        }
        public void Update()
        {
            foreach (var forumComment in ForumsCommentService.GetInstance().GetCommentByForumId(Forum.Id))
            {
                string username = UserService.GetInstance().GetUsername(forumComment.UserId);
                Enums.ReservationChangeStatus status = Enums.ReservationChangeStatus.Rejected;
                if (forumComment.Status==1)
                status = Enums.ReservationChangeStatus.Pending;
                ForumsComment.Add(new ForumViewDTO(username, forumComment, status));
            }
        }
        public void Execute_AddCommentCommand(object obj)
        {
            if (obj is Button button)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }
        public void Execute_CreateCommentCommand(object obj)
        {
            if (Commentt != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Are you sure to create the comment?", "Create comment", MessageBoxButton.YesNo);
                switch (odgovor)
                {
                    case MessageBoxResult.Yes:
                        int status = 0;
                        if (AccommodationReservationService.GetInstance().CheckGuest(Forum.UserId, Forum.locationId)==Enums.ReservationChangeStatus.Pending)
                         status = 1;
                        Domain.Model.ForumsComment comment = new Domain.Model.ForumsComment(Forum.Id, Guest.UserId, DateOnly.FromDateTime(DateTime.Today), Commentt, Enums.UserType.Guest,status);
                        ForumsCommentService.GetInstance().Save(comment);
                        NavService.Navigate(new ViewForumView(Forum,Guest, NavService));
                        break;
                    default:
                        break;
                }
            }
            else MessageBox.Show("The fields are not filled in correctly!", "WRONG FORMAT ", MessageBoxButton.OK, MessageBoxImage.Exclamation);

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
