using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs
{
    public class VouchersDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public VouchersDTO() { }
        public VouchersDTO(Voucher voucher)
        {
            Id = voucher.Id;
            ExpirationDate = voucher.IssuingDate;
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
                if(value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private DateTime _expirationDate;
        public DateTime ExpirationDate
        {
            get
            {
                return _expirationDate;
            }
            set
            {
                if (value != _expirationDate)
                {
                    _expirationDate = value;
                    OnPropertyChanged("ExpirationDate");

                }
            }

        }
    }
}
