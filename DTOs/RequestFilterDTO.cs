using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.DTOs
{
    public class RequestFilterDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public LocationDTO Location { get; set; } = new LocationDTO();
        public LanguageDTO Language { get; set; } = new LanguageDTO();
        public DateOnly Beggining { get; set; } = DateOnly.MinValue;
        public DateOnly Ending { get; set; } = DateOnly.MinValue;

        public int TouristNumber { get; set; } = 0;

        public RequestFilterDTO() { }
        public RequestFilterDTO(int touristNumber, LocationDTO location, LanguageDTO language, DateOnly beggining, DateOnly ending)
        {
            TouristNumber = touristNumber;
            Location = location; Language = language;
            Beggining = beggining;
            Ending = ending;
        }

        public bool isEmpty()
        {
            return Location.Id == 0 && Language.Id == 0 && TouristNumber == 0 && Beggining == DateOnly.MinValue && Ending == DateOnly.MinValue;
        }
    }
}
