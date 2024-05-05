using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class ImageDTO : INotifyPropertyChanged
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

        public string leftPath;

        public string LeftPath
        {
            get { return leftPath; }
            set
            {
                if (value != leftPath)
                {
                    leftPath = value;
                    OnPropertyChanged("LeftPath");
                }
            }
        }

        public string rightPath;

        public string RightPath
        {
            get { return rightPath; }
            set
            {
                if (value != rightPath)
                {
                    rightPath = value;
                    OnPropertyChanged("RightPath");
                }
            }
        }

        public ImageDTO(Model.Image image)
        {
            Id = image.Id;
            
        }

        public ImageDTO(string path)
        {
            LeftPath = path;

        }
    }

}
