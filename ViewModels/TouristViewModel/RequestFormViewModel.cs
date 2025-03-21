﻿using BookingApp.Model;
using BookingApp.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class RequestFormViewModel
    {
        public User LoggedUser { get; set; }
        public RelayCommand SimpleRequestCommand { get; set; }
        public Action CloseAction { get; set; }
        public RequestFormViewModel(User user)
        {
            LoggedUser = user;
            SimpleRequestCommand = new RelayCommand(Execute_SimpleRequestCommand);
        }

        private void Execute_SimpleRequestCommand(object obj)
        {
            SimpleRequest simple = new SimpleRequest(LoggedUser);
            simple.Owner = Application.Current.MainWindow;
            simple.ShowDialog();
            CloseAction();
        }
    }
}
