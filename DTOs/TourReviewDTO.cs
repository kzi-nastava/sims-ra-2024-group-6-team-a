using BookingApp.Domain.Model;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.DTOs
{
    public  class TourReviewDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
       

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

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged("Age");

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
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
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

        private string _firstname;

        public string FirstName
        {
            get
            {
                return _firstname;
            }

            set
            {
                if (value != _firstname)
                {
                    _firstname = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        private string _lastname;

        public string LastName
        {
            get
            {
                return _lastname;
            }

            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private List<Model.Image> _listImages;

        public List<Model.Image> ListImages
        {
            get
            {
                return _listImages;
            }

            set
            {
                if (value != _listImages)
                {
                    _listImages = value;
                    OnPropertyChanged("ListImages");
                }
            }

        }

        private int _imageIndex = 0;
        public int ImageIndex
        {
            get => _imageIndex;
            set
            {
                _imageIndex = value;
                OnPropertyChanged(nameof(ImageIndex));
            }
        }


        public Model.Image CurrentImage => ListImages[ImageIndex];
       

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void NextImage()
        {
            if (ImageIndex < ListImages.Count - 1) ImageIndex++;
            else ImageIndex = 0;
            
            OnPropertyChanged(nameof(CurrentImage));
        }

        internal void PreviousImage()
        {
            if (ImageIndex > 0) ImageIndex--;
            else ImageIndex = ListImages.Count - 1;
            
            OnPropertyChanged(nameof(CurrentImage));

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

        public TourReviewDTO(int reviewId,int scheduleId, int languageGrade, int knowledgeGrade, int attractionsGrade, string impression, List<Model.Image> images, int touristId, Tourist tourist, string checkpoint,bool isValid)
        {
            ReviewId = reviewId;
            ScheduleId = scheduleId;
            GuideKnowledgeGrade = knowledgeGrade;
            GuideLanguageGrade = languageGrade;
            TourAttractionsGrade = attractionsGrade;
            Impression = impression;
            ListImages = images;
            TouristId = touristId;
            FirstName = tourist.Name;
            LastName = tourist.Surname;
            Age = tourist.Age;
            Checkpoint = checkpoint;
            IsValid = isValid;

        }


    }
}
