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

        private int _reviewId;
        public int ReviewId
        {
            get
            {
                return _reviewId;
            }
            set
            {
                if (_reviewId != value)
                {
                    _reviewId = value;
                    OnPropertyChanged("ReviewId");

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
        private bool _isValid;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged("IsValid");

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

        private String _nickname;
        public String Nickname
        {
            get
            {
                return _nickname;
            }

            set
            {
                if (value != _nickname)
                {
                    _nickname = value;
                    OnPropertyChanged("Nickname");
                }
            }
        }

        private String _checkpoint;
        public String Checkpoint
        {

            get
            {
                return _checkpoint;
            }

            set
            {
                if (value != _checkpoint)
                {
                    _checkpoint = value;
                    OnPropertyChanged("Checkpoint");
                }
            }

        }

        public TourReviewDTO() { }
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

        public TourReviewDTO(int reviewId,int scheduleId, int languageGrade, int knowledgeGrade, int attractionsGrade, string impression, string imagePath, int touristId, string nickname, string checkpoint,bool isValid)
        {
            ReviewId = reviewId;
            ScheduleId = scheduleId;
            GuideKnowledgeGrade = knowledgeGrade;
            GuideLanguageGrade = languageGrade;
            TourAttractionsGrade = attractionsGrade;
            Impression = impression;
            Image = imagePath;
            TouristId = touristId;
            Nickname = nickname;
            Checkpoint = checkpoint;
            IsValid = isValid;

        }


    }
}
