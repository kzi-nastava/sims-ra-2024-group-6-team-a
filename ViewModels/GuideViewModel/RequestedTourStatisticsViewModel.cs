using BookingApp.ApplicationServices;
using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.View.GuideView.Pages;
using BookingApp.View.TouristView;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public RequestedTourStatisticsViewModel(RequestedTourStatistics window, User user)
        {
            this.Window = window;
            LoggedUser = user; 
            InitializeLanguages();
            InitializeLocations();
            TourData = new Dictionary<int, int> { };
            SeriesCollection = new SeriesCollection(new LineSeries());
            Window.overallCheckBox.IsChecked = true;
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
          
        }

        public void OverallStatistics(int languageId, int locationId, List<TourRequest> requests)
        {
            foreach (TourRequest request in requests)
            {
                if (request.LanguageId != languageId && languageId != 0)
                {
                    continue;
                }
                if (request.LocationId != locationId && locationId != 0)
                {
                    continue;
                }
                int year = request.StartDate.Year;
                if (!TourData.ContainsKey(year))
                {
                    TourData[year] = 1;
                }
                else
                {
                    TourData[year]++;
                }
            }
            TourData = TourData.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var years = TourData.Keys.ToList();
            var values = TourData.Values.ToList();

            // Add data to SeriesCollection
            SeriesCollection.Add(new LineSeries
            {
                Title = "Requested Tours",
                Values = new ChartValues<int>(values),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 15
            });

            // Set X axis labels
            Window.RequestedTourChart.AxisX.Clear();
            Window.RequestedTourChart.AxisX.Add(new Axis
            {
                Title = "Year",
                Labels = years.Select(y => y.ToString()).ToList()
            });

            // Set Y axis labels to display rounded numbers
            Window.RequestedTourChart.AxisY.Clear();
            Window.RequestedTourChart.AxisY.Add(new Axis
            {
                Title = "Number of Tours",
                LabelFormatter = value => Math.Round(value).ToString()
            });
        }

    }
}
