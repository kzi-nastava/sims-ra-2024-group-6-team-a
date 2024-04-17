using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class GuestReviewDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int reservationId;

        public int ReservationId
        {
            get { return reservationId; }

            set
            {
                if (reservationId != value)
                {
                    reservationId = value;
                    OnPropertyChanged("reservationId");

                }
            }
        }


        public string guestName;

        public string GuestName
        {
            get
            { return guestName; }

            set
            {
                if(value !=  guestName) 
                {
                    guestName = value;
                    OnPropertyChanged("GuestName");
                }
            }
        }

        public string accommodationName;

        public string AccommodationName
        {
            get { return accommodationName; }

            set
            {
                if(value != accommodationName) 
                {
                    accommodationName = value;
                    OnPropertyChanged("accommodationName");
                }
            }
        }

        public int cleanlinessGrade;

        public int CleanlinessGrade
        {
            get { return cleanlinessGrade; }

            set
            {
                if(cleanlinessGrade != value)
                {
                    cleanlinessGrade = value;
                    OnPropertyChanged("CleanlinessGrade");

                }
            }
        }

        public int respectGrade;

        public int RespectGrade
        {
            get { return respectGrade; }

            set
            {
                if (respectGrade != value)
                {
                    respectGrade = value;
                    OnPropertyChanged("RespectGrade");

                }
            }
        }

        public string date;

        public string Date
        {
            get { return date; }

            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public string comment;

        public string Comment
        {
            get { return comment; }

            set
            {
                if(comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public String image;

        public String Image
        {
            get
            {
                return image;
            }

            set
            {
                if (value != image)
                {
                    image = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        public String info;

        public String Info
        {
            get { return info; }

            set
            {
                if(info != value)
                {
                    info = value;
                }
            }
        }

        public GuestReviewDTO(string accommodationName, string userName,int cleanlinessGrade,int respectGrade,string comment,string date,int reservationId)
        {
            this.accommodationName = accommodationName;
            guestName = userName;
            this.cleanlinessGrade = cleanlinessGrade;
            this.respectGrade = respectGrade;
            this.comment = comment;
            image = "\\Resources\\Images\\blank-profile.jpg";
            this.date = date;
            this.reservationId = reservationId;
            if (this.cleanlinessGrade == 0 && this.respectGrade == 0)
            {
                info = "Click \"Enter\" to review guest.";
            }
            else
                info = "Click \"Enter\" to see guest's feedback.";
        }

    }
}
