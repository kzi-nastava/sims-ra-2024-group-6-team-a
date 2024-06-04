using BookingApp.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels.OwnerViewModel
{
    public class ReviewGuestVM : INotifyPropertyChanged
    {
        private int reservationId;
        List<int> combo;
        public List<int> Combo
        {
            get 
            {
            return combo;
            }
            set
            {
                combo = value;
                OnPropertyChanged(nameof(combo));
            }
        }


        int? cleanSelected;

        public int? CleanSelected
        {
            get { return cleanSelected; }
            set 
            { 
            cleanSelected = value;
            OnPropertyChanged(nameof(CleanSelected));
            }
        }

        int? respectSelected;

        public int? RespectSelected
        {
            get { return respectSelected; }
            set
            {
                respectSelected = value;
                OnPropertyChanged(nameof(respectSelected));
            }
        }

        public string commentBox;

        public string CommentBox
        {
            get => commentBox;
            set
            {
                commentBox = value;
                OnPropertyChanged(nameof(CommentBox));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand ConfirmCommand { get; }
        


        public ReviewGuestVM(int reservationId)
        {
            this.reservationId = reservationId;
            Combo = new List<int>();
            for(int i = 1; i <= 5; i++) 
            {
                Combo.Add(i);
            }
            CloseCommand = new RelayCommand(CloseWindow);
            ConfirmCommand = new RelayCommand(SaveGrade);
            
        }



        private void SaveGrade(object parameter)
        {
            if (CleanSelected != null && RespectSelected != null)
            {
                if (MessageBox.Show("Confirm review?", "Grade the guest", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    GuestReviewService.GetInstance().Save(new Model.GuestReview(reservationId, (int)CleanSelected, (int)RespectSelected, CommentBox));

                    CloseWindow(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));

                }

            }
            else
            {
                MessageBox.Show("Must fill all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }

    }
}
