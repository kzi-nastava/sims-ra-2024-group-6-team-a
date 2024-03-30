using BookingApp.DTOs;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.ViewModels
{
    public class AccommodationDetailedVM : IObserver
    {
        public static ObservableCollection<ImageDTO> Images { get; set; }
        public ObservableCollection<ReservationOwnerDTO> Reservations { get; set; }
        public static List<Model.Image> imageModels { get; set; }

        public ReservationOwnerDTO SelectedReservation { get; set; }

        public AccommodationDetailedVM(List<Model.Image> images, ObservableCollection<ReservationOwnerDTO> Reservations) 
        {
            Images = new ObservableCollection<ImageDTO>();

            imageModels = images;
            this.Reservations = Reservations;
            

            Update();
        }

        public void Update()
        {
            Images.Clear();

            foreach (Model.Image i in imageModels)
            {
                Images.Add(new ImageDTO(i));
            }


        }

        public void SelectFirstReservation(ListBox ReservationsList)
        {
            if (SelectedReservation == null)
            {
                SelectedReservation = Reservations.First();
                ReservationsList.SelectedIndex = 0;
                ReservationsList.UpdateLayout();
                ReservationsList.Focus();

            }
        }
    }
}
