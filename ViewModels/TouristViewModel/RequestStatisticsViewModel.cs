using BookingApp.ApplicationServices;
using BookingApp.DTOs;
using BookingApp.Model;
using BookingApp.View.TouristView;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Defaults;
using BookingApp.Domain.Model;
using System.Windows.Media;

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
        public ObservableCollection<int> RequestCounts { get; set; } = new ObservableCollection<int>();
        public List<string> Languages { get; set; } = new List<string>();

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
        public Func<double, string> IntLabelFormatter => value => value.ToString("N0");

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
        public List<string> CartesianLabels { get; set; } = new List<string>();
        public ChartValues<int> CartesianValues { get; set; } = new ChartValues<int>();
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
            var requestsByLanguages = SimpleRequestService.GetInstance().GetRequestsByLanguages(UserId);

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
            var requestsByLocations = SimpleRequestService.GetInstance().GetRequestsByLocations(UserId);

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
            foreach (var year in SimpleRequestService.GetInstance().GetRequestYears(UserId))
                Years.Add(year);
        }

        public void LoadAllTimePieChart()
        {
            PieChartCollection = new SeriesCollection();
            double acceptedPercentage = SimpleRequestService.GetInstance().GetAllTimeAcceptedPercentage(UserId);

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
            double acceptedPercentage = SimpleRequestService.GetInstance().GetAcceptedPercentageByYear(UserId, SelectedYear);

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
            AverageNumber = SimpleRequestService.GetInstance().GetAverageAccepetedPeople(UserId);

        }
        private void Execute_LoadYearPeopleNumberCommand(object obj)
        {
            AverageNumber = SimpleRequestService.GetInstance().GetAverageAccepetedPeopleByYear(UserId, SelectedYear);

        }
    }
}
