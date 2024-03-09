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

        public int guestId;

        public int GuestId
        {
            get
            { return guestId; }

            set
            {
                if(value !=  guestId) 
                {
                    guestId = value;
                    OnPropertyChanged("GuestId");
                }
            }
        }

        public int reservationId;

        public int ReservationId
        {
            get { return reservationId; }

            set
            {
                if(value != reservationId) 
                {
                    reservationId = value;
                    OnPropertyChanged("ReservationId");
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
                    OnPropertyChanged("image");
                }
            }
        }

        public GuestReviewDTO(int reservationId,int userId,int cleanlinessGrade,int respectGrade,string comment)
        {
            reservationId = reservationId;
            guestId = userId;
            this.cleanlinessGrade = cleanlinessGrade;
            this.respectGrade = respectGrade;
            this.comment = comment;
            image = "\\Resources\\Images\\blank-profile.jpg";
        }

    }
}
