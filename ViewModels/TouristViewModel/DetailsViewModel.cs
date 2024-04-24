using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class DetailsViewModel
    {
        public TourTouristDTO TourTouristDTO { get; set; }
        public ObservableCollection<Model.Image> SelectedTourImages { get; set; }
        public ObservableCollection<Checkpoint> SelectedTourCheckpoints { get; set; }   

        public DetailsViewModel(TourTouristDTO tour)
        {
            TourTouristDTO = tour;

            ObservableCollection<Model.Image> lista = new ObservableCollection<Model.Image>();
            foreach (Model.Image image in ImageService.GetInstance().GetByEntity(tour.Id, Enums.ImageType.Tour))
                lista.Add(image);

            SelectedTourImages = lista;

            ObservableCollection<Checkpoint> points = new ObservableCollection<Checkpoint>();
            foreach(Checkpoint check in CheckpointService.GetInstance().GetAllByTourId(TourTouristDTO.Id))
                points.Add(check);

            SelectedTourCheckpoints = points;
        }
    }
}
