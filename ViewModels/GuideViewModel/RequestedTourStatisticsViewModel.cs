using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using BookingApp.View.TouristView;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.Primitives;

namespace BookingApp.ViewModels.GuideViewModel
{
    public class RequestedTourStatisticsViewModel
    {
        public RequestedTourStatistics Window {  get; set; }
        public User LoggedUser { get; set; }

        public List<string> Locations { get; set; }

        public List<string> Languages { get; set; } 

        private Dictionary<int, int> TourData {  get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public List<int> Years {  get; set; }

        public RequestedTourStatisticsViewModel(RequestedTourStatistics window, User user)
        {
            this.Window = window;
            LoggedUser = user; 
            InitializeLanguages();
            InitializeLocations();
            InitializeYears();
            TourData = new Dictionary<int, int> { };
            SeriesCollection = new SeriesCollection(new LineSeries());
            Window.overallCheckBox.IsChecked = true;
        }

        private void InitializeYears()
        {
            Years = new List<int>();
            List<TourRequest> tourRequests = SimpleRequestService.GetInstance().GetAll();
            Years = tourRequests
                           .Select(request => request.StartDate.Year)
                           .Distinct()
                           .OrderByDescending(year => year)
                           .ToList();
        }

        private void InitializeLanguages()
        {
            Languages = new List<String>();

            foreach (Language language in LanguageService.GetInstance().GetAll())
            {
                Languages.Add(language.Name);
            }
        }

        private void InitializeLocations()
        {
            Locations = new List<String>();
            foreach (Location location in LocationService.GetInstance().GetAll())
            {
                Locations.Add(location.City + " , " + location.State);
            }
        }

        public void SearchButtonClick()
        {
            int languageId = Window.languageComboBox.SelectedIndex + 1;
            int locationId = Window.locationComboBox.SelectedIndex + 1;

            TourData.Clear();
            SeriesCollection.Clear();
            List<TourRequest> requests = SimpleRequestService.GetInstance().GetAll();
            if(Window.overallCheckBox.IsChecked  == true)
            {
                OverallStatistics(languageId, locationId, requests);
            }
            else if(Window.yearComboBox.SelectedItem != null)
            {
                StatisticsPerMonth(languageId, locationId, requests);
            }
          
        }

        public void StatisticsPerMonth(int languageId, int locationId, List<TourRequest> requests)
        {
            int currentYear;
            if (int.TryParse(Window.yearComboBox.SelectedItem.ToString(), out currentYear));
            var filteredRequests = FilterRequestsByYear(languageId, locationId, requests, currentYear);
            var tourData = CountToursByMonth(filteredRequests);
            var sortedRequestData = SortTourDataByMonth(tourData);
            var (months, values) = ExtractMonthsAndValues(sortedRequestData);
            AddSeriesToChart("Requested Tours", values);
            SetUpXAxisLabelsMonth(months);
            SetUpYAxisLabels();

        }

        private void SetUpXAxisLabelsMonth(List<string> months)
        {
            Window.RequestedTourChart.AxisX.Clear();
            Window.RequestedTourChart.AxisX.Add(new Axis
            {
                Title = "Month",
                Labels = months
            });
        }

        private IEnumerable<TourRequest> FilterRequestsByYear(int languageId, int locationId, List<TourRequest> requests, int year)
        {
            return requests.Where(request =>
                (languageId == 0 || request.LanguageId == languageId) &&
                (locationId == 0 || request.LocationId == locationId) &&
                request.StartDate.Year == year
            );
        }

        private (List<string> months, List<int> values) ExtractMonthsAndValues(Dictionary<int, int> tourData)
        {
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            months.RemoveAll(string.IsNullOrEmpty);

            var values = new List<int>(12);
            for (int i = 1; i <= 12; i++)
            {
                values.Add(tourData.ContainsKey(i) ? tourData[i] : 0);
            }

            return (months, values);
        }


        private Dictionary<int, int> SortTourDataByMonth(Dictionary<int, int> tourData)
        {
            return tourData.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }



        private Dictionary<int, int> CountToursByMonth(IEnumerable<TourRequest> requests)
        {
            return requests.GroupBy(request => request.StartDate.Month)
                         .ToDictionary(group => group.Key, group => group.Count());
        }

        public void OverallStatistics(int languageId, int locationId, List<TourRequest> requests)
        {
            var filteredRequests = FilterRequests(languageId, locationId, requests);
            var tourData = CountToursByYear(filteredRequests);
            var sortedTourData = SortTourDataByYear(tourData);
            var (years, values) = ExtractYearsAndValues(sortedTourData);

            AddSeriesToChart("Requested Tours", values);
            SetUpXAxisLabels(years);
            SetUpYAxisLabels();
        }

        private IEnumerable<TourRequest> FilterRequests(int languageId, int locationId, List<TourRequest> requests)
        {
            return requests.Where(request =>
                (languageId == 0 || request.LanguageId == languageId) &&
                (locationId == 0 || request.LocationId == locationId)
            );
        }

        private Dictionary<int, int> CountToursByYear(IEnumerable<TourRequest> requests)
        {
            return requests.GroupBy(request => request.StartDate.Year)
                          .ToDictionary(group => group.Key, group => group.Count());
        }
        private Dictionary<int, int> SortTourDataByYear(Dictionary<int, int> tourData)
        {
            return tourData.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        private (List<int> years, List<int> values) ExtractYearsAndValues(Dictionary<int, int> tourData)
        {
            return (tourData.Keys.ToList(), tourData.Values.ToList());
        }

        private void AddSeriesToChart(string title, List<int> values)
        {
            SeriesCollection.Add(new LineSeries
            {
                Title = title,
                Values = new ChartValues<int>(values),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 15
            });
        }

        private void SetUpXAxisLabels(List<int> years)
        {
            Window.RequestedTourChart.AxisX.Clear();
            Window.RequestedTourChart.AxisX.Add(new Axis
            {
                Title = "Year",
                Labels = years.Select(y => y.ToString()).ToList()
            });
        }

        private void SetUpYAxisLabels()
        {
            Window.RequestedTourChart.AxisY.Clear();
            Window.RequestedTourChart.AxisY.Add(new Axis
            {
                Title = "Number of Tours",
                LabelFormatter = value => Math.Round(value).ToString()
            });
        }
    }
}
