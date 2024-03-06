using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml.Schema;

namespace BookingApp.Model
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int Capacity {  get; set; }
        public List<Checkpoint> Checkpoints { get; set; }
        public double Duration { get; set; }
        public List<TourSchedule> TourSchedules { get; set; }
        public List<Image> Images { get; set; }


        public Tour() { Images = new List<Image>(); }

        public Tour(int id, string name, int location, string description, string language, int capacity, List<Checkpoint> checkpoints, double duration,List <TourSchedule> tourSchedules, List <Image> images)
        {
            Id = id;
            Name = name;
            LocationId = location;
            Description = description;
            Language = language;
            Capacity = capacity;
            Checkpoints = checkpoints;
            Duration = duration;
            TourSchedules = tourSchedules;
            Images = images;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Description , Language , Capacity.ToString() , Duration.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description =  values[2];
            Language = values[3];
            Capacity = Convert.ToInt32(values[4]);
            Duration = Convert.ToDouble(values[5]);
        }

    }

}
