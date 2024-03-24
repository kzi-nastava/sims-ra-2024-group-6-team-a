using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class OwnerInfoDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }

            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        private int reservations;

        public int Reservations
        {
            get { return reservations; }

            set
            {
                if(value != reservations)
                {
                    reservations = value;
                    OnPropertyChanged("reservations");
                }    
            }
        }


        private int accommodations;

        public int Accommodations
        {
            get { return accommodations; }

            set
            {
                if (value != accommodations)
                {
                    accommodations = value;
                    OnPropertyChanged("accommodations");
                }
            }
        }

        private double grade;

        public double Grade
        {
            get { return grade; }

            set
            {
                if( value != grade)
                {
                    grade = value;
                    OnPropertyChanged($"Grade {grade}");
                }
            }
        }

        private int ranking;

        public int Ranking
        {
            get { return ranking; }

            set
            {
                if(value!= ranking)
                {
                    ranking = value;
                    OnPropertyChanged("ranking");
                }
            }
        }

        private string status;

        public string Status
        {
            get { return status; }

            set
            {
                if(value != status)
                {
                    status = value;
                    OnPropertyChanged("status");
                }
            }
        }

        public OwnerInfoDTO(Owner owner,int reservations,int accommodations)
        {
            Id = owner.Id;
            Name = owner.Name;
            Surname = owner.Surname;
            Reservations = reservations;
            Accommodations = accommodations;
            Grade = owner.AverageGrade;
            Ranking = owner.Ranking;
            if (owner.IsSuper)
                Status = "Super Owner";
            else
                Status = "Regular Owner";

        }
    }
}
