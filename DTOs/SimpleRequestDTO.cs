using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class SimpleRequestDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");

                }

            }
        }
        private string _city;
        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }

            }
        }
        private string _state;

        public string State
        {
            get
            {
                return _state;
            }

            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged("State");
                }

            }
        }
        private DateOnly _start;
        public DateOnly Start
        {
            get
            {
                return _start;
            }

            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                }
            }
        }
        private DateOnly _end;
        public DateOnly End
        {
            get
            {
                return _end;
            }

            set
            {
                if (value != _end)
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");

                }
            }
        }
        public int touristId;
        public int TouristId
        {
            get
            {
                return touristId;
            }
            set
            {
                if (value != touristId)
                {
                    touristId = value;
                    OnPropertyChanged("TouristId");

                }
            }

        }
        public int languageId;
        public int LanguageId
        {
            get
            {
                return languageId;
            }
            set
            {
                if (value != languageId)
                {
                    languageId = value;
                    OnPropertyChanged("LanguageId");

                }
            }

        }
        public int locationId;
        public int LocationId
        {
            get
            {
                return locationId;
            }
            set
            {
                if (value != locationId)
                {
                    locationId = value;
                    OnPropertyChanged("LocationId");

                }
            }

        }

        private Enums.RequestStatus status;
        public Enums.RequestStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if(value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }


        public SimpleRequestDTO() { }
        public SimpleRequestDTO(TourRequest request, Location location, Language language)
        {
            Id = request.Id;
            City = location.City;
            State = location.State;
            Language = language.Name;
            Description = request.Description;
            Start = request.StartDate;
            End = request.EndDate;
            TouristId = request.TouristId;
            Status = request.Status;

        }
        public SimpleRequestDTO(TourRequest request, int locationId, int languageId)
        {
            Id = request.Id;
            LocationId = locationId;
            LanguageId = languageId;
            Description = request.Description;
            Start = request.StartDate;
            End = request.EndDate;
            TouristId = request.TouristId;
            Status = request.Status;

        }

    }
}
