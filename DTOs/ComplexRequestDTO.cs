using BookingApp.Domain.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ComplexRequestDTO : INotifyPropertyChanged
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
        private int _touristId;
        public int TouristId
        {
            get
            {
                return _touristId;
            }
            set
            {
                if (value != _touristId)
                {
                    _touristId = value;
                    OnPropertyChanged("TouristId");
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
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }

            set

            {
                if (value != _number)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }
        public ComplexRequestDTO() { }
        public ComplexRequestDTO(ComplexTourRequest request, int number)
        {
            Id = request.Id;
            Status = request.Status;
            TouristId = request.TouristId;
            Number = number;
        }
    }
}
