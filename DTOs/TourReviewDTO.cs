using BookingApp.Model;
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
    public  class TourReviewDTO : INotifyPropertyChanged
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

        private int _scheduleId;
        public int ScheduleId
        {
            get
            {
                return _scheduleId;
            }
            set
            {
                if (_scheduleId != value)
                {
                    _scheduleId = value;
                    OnPropertyChanged("ScheduleId");

                }
            }
        }

        private int _guideKnowledgeGrade;
        public int GuideKnowledgeGrade
        {
            get
            {
                return _guideKnowledgeGrade;
            }
            set
            {
                if (_guideKnowledgeGrade != value)
                {
                    _guideKnowledgeGrade = value;
                    OnPropertyChanged("GuideKnowledgeGrade");

                }
            }
        }

        private int _guideLanguageGrade;
        public int GuideLanguageGrade
        {
            get
            {
                return _guideLanguageGrade;
            }
            set
            {
                if (_guideLanguageGrade != value)
                {
                    _guideLanguageGrade = value;
                    OnPropertyChanged("GuideLanguageGrade");

                }
            }
        }

        private int _tourAttractionsGrade;
        public int TourAttractionsGrade
        {
            get
            {
                return _tourAttractionsGrade;
            }
            set
            {
                if (_tourAttractionsGrade != value)
                {
                    _tourAttractionsGrade = value;
                    OnPropertyChanged("TourAttractionsGrade");

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
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged("TouristId");

                }
            }
        }

        private string _impression;
        public string Impression
        {
            get
            {
                return _impression;
            }
            set
            {
                if (value != _impression)
                {
                    _impression = value;
                    OnPropertyChanged("Impression");

                }
            }
        }

        private String _image;

        public String Image
        {
            get
            {
                return _image;
            }

            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        public TourReviewDTO()
        {

        }

        public TourReviewDTO(int scheduleId, int languageGrade, int knowledgeGrade, int attractionsGrade, string impression, string imagePath, int touristId)
        {
            ScheduleId = scheduleId;
            GuideKnowledgeGrade = knowledgeGrade;
            GuideLanguageGrade = languageGrade;
            TourAttractionsGrade = attractionsGrade;
            Impression = impression;
            Image = imagePath;
            TouristId = touristId;
        }


    }
}
