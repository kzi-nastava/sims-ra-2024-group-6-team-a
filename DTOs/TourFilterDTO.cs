using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class TourFilterDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Name { get; set; } = "";
        public LocationDTO Location { get; set; } =  new LocationDTO();
        public LanguageDTO Language { get; set; } = new LanguageDTO();
        public int TouristNumber { get; set; } = 0;
        public double Duration { get; set; } = 0;
        public DateTime Beggining { get; set; } = DateTime.MinValue;

        public TourFilterDTO() { }
        public TourFilterDTO(string name, double duration, int touristNumber, LocationDTO location, LanguageDTO language, DateTime beggining)
        {
            Name = name;
            Location = location;
            Duration = duration;
            Language = language;
            TouristNumber = touristNumber;
            Beggining = beggining;
        }

        public bool isEmpty()
        {
            return Location.Id == 0 && Language.Id == 0  && Duration == 0 && TouristNumber == 0;
        }
    }
}
