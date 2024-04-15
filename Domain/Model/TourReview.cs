using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Resources;
using BookingApp.Serializer;
using BookingApp.Model;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class TourReview : ISerializable
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int GuideKnowledgeGrade { get; set; }
        public int GuideLanguageGrade { get; set; }
        public int TourAttractionsGrade { get; set; }
        public string Impression { get; set; }
        public int TouristId { get; set; }
        public bool IsValid { get; set; }
        public List<Image> Images { get; set; }

        public TourReview(int scheduleId, int guideKnowledgeGrade, int guideLanguageGrade, int tourAttractionsGrade, string impression, int touristId, bool isValid)
        {
            ScheduleId = scheduleId;
            GuideKnowledgeGrade = guideKnowledgeGrade;
            GuideLanguageGrade = guideLanguageGrade;
            TourAttractionsGrade = tourAttractionsGrade;
            Impression = impression;
            TouristId = touristId;
            IsValid = isValid;
        }

        public TourReview()
        {
            Images = new List<Image>();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ScheduleId.ToString(), GuideKnowledgeGrade.ToString(), GuideLanguageGrade.ToString(), TourAttractionsGrade.ToString(), Impression, TouristId.ToString(), IsValid.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ScheduleId = Convert.ToInt32(values[1]);
            GuideKnowledgeGrade = Convert.ToInt32(values[2]);
            GuideLanguageGrade = Convert.ToInt32(values[3]);
            TourAttractionsGrade = Convert.ToInt32(values[4]);
            Impression = values[5];
            TouristId = Convert.ToInt32(values[6]);
            IsValid = bool.Parse(values[7]);
        }

    }
}
