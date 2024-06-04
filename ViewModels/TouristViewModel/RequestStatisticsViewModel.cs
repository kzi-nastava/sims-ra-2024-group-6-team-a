using BookingApp.ApplicationServices;
using BookingApp.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveCharts.Defaults;
using BookingApp.Validation;
using System.Windows.Input;

namespace BookingApp.ViewModels.TouristViewModel
{
    public class RequestStatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<int> Years { get; set; } = new ObservableCollection<int>();

        private SeriesCollection _pieChartCollection;
        public SeriesCollection PieChartCollection
        {
            get
            {
                return _pieChartCollection;
            }
            set
            {
                if (_pieChartCollection != value)
                {
                    _pieChartCollection = value;
                    OnPropertyChanged(nameof(PieChartCollection));
                }
            }
        }
        private SeriesCollection _cartesianCollection;
        public SeriesCollection CartesianCollection
        {
            get
            {
                return _cartesianCollection;
            }
            set
            {
                if (_cartesianCollection != value)
                {
                    _cartesianCollection = value;
                    OnPropertyChanged(nameof(CartesianCollection));
                }
            }
        }

        private SeriesCollection _cartesianLocationCollection;
        public SeriesCollection CartesianLocationCollection
        {
            get
            {
                return _cartesianLocationCollection;
            }
            set
            {
                if (_cartesianLocationCollection != value)
                {
                    _cartesianLocationCollection = value;
                    OnPropertyChanged(nameof(CartesianLocationCollection));
                }
            }
        }
        private int _selectedYear;
        public int SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if(_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }
        private int _selectedYearAvg;
        public int SelectedYearAvg
        {
            get
            {
                return _selectedYearAvg;
            }
            set
            {
                if (_selectedYearAvg != value)
                {
                    _selectedYearAvg = value;
                    OnPropertyChanged(nameof(SelectedYearAvg));
                }
            }
        }
        private double _averageNumber;
        public double AverageNumber
        {
            get
            {
                return _averageNumber;
            }
            set
            {
                if (_averageNumber != value)
                {
                    _averageNumber = value;
                    OnPropertyChanged(nameof(AverageNumber));
                }
            }
        }
        public int UserId { get; set; }

        public RelayCommand LoadGeneralStatisticsCommand { get; set; }
        public RelayCommand LoadYearStatisticsCommand { get; set; }
        public RelayCommand LoadGeneralPeopleNumberCommand { get; set; }
        public RelayCommand LoadYearPeopleNumberCommand { get; set; }
        public RequestStatisticsViewModel(int userId)
        {
            UserId = userId;
            LoadGeneralStatisticsCommand = new RelayCommand(Execute_LoadGeneralStatisticsCommand);
            LoadYearStatisticsCommand = new RelayCommand(Execute_LoadYearsStatisticsCommand);
            LoadGeneralPeopleNumberCommand = new RelayCommand(Execute_LoadGeneralPeopleNumberCommand);
            LoadYearPeopleNumberCommand = new RelayCommand(Execute_LoadYearPeopleNumberCommand);
            CartesianCollection = new SeriesCollection();
            CartesianLocationCollection = new SeriesCollection();
            LoadYears();
            LoadAllTimePieChart();
            LoadLangugaeChartData();
            LoadLocationChartData();
        }

        private void LoadLangugaeChartData()
        {
            var requestsByLanguages = TourRequestService.GetInstance().GetRequestsByLanguages(UserId);

            foreach (var pair in requestsByLanguages)
            {
                CartesianCollection.Add(new ColumnSeries
                {
                    Title = pair.Key,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(pair.Value) },
                });
            }
        }
        private void LoadLocationChartData()
        {
            var requestsByLocations = TourRequestService.GetInstance().GetRequestsByLocations(UserId);

            foreach (var pair in requestsByLocations)
            {
                CartesianLocationCollection.Add(new ColumnSeries
                {
                    Title = pair.Key.City + " - " + pair.Key.State,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(pair.Value) },
                });
            }
        }

        public void LoadYears()
        {
            Years.Clear();
            foreach (var year in TourRequestService.GetInstance().GetRequestYears(UserId))
                Years.Add(year);
        }

        public void LoadAllTimePieChart()
        {
            PieChartCollection = new SeriesCollection();
            double acceptedPercentage = TourRequestService.GetInstance().GetAllTimeAcceptedPercentage(UserId);

            PieChartCollection.Clear();
            PieChartCollection.Add(new PieSeries
            {
                Title = "Accepted",
                Values = new ChartValues<double> { acceptedPercentage },
            });
            PieChartCollection.Add(new PieSeries
            {
                Title = "Not accepted",
                Values = new ChartValues<double> { 100 - acceptedPercentage },
            });
        }

        public void LoadSelectedYearPieChart()
        {
            PieChartCollection = new SeriesCollection();
            double acceptedPercentage = TourRequestService.GetInstance().GetAcceptedPercentageByYear(UserId, SelectedYear);

            PieChartCollection.Clear();
            PieChartCollection.Add(new PieSeries
            {
                Title = "Accepted",
                Values = new ChartValues<double> { acceptedPercentage },
            });
            PieChartCollection.Add(new PieSeries
            {
                Title = "Not accepted",
                Values = new ChartValues<double> { 100 - acceptedPercentage },
            });
        }

        private void Execute_LoadGeneralStatisticsCommand(object obj)
        {
            
                LoadAllTimePieChart();
        }
        private void Execute_LoadYearsStatisticsCommand(object obj)
        {
                LoadSelectedYearPieChart();
        }
        private void Execute_LoadGeneralPeopleNumberCommand(object obj)
        {
            
                AverageNumber = TourRequestService.GetInstance().GetAverageAccepetedPeople(UserId);
        }
        private void Execute_LoadYearPeopleNumberCommand(object obj)
        {
                AverageNumber = TourRequestService.GetInstance().GetAverageAccepetedPeopleByYear(UserId, SelectedYearAvg);
        }
        
    }
}
