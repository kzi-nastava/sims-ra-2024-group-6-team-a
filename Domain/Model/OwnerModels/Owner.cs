using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
using BookingApp.Resources;

namespace BookingApp.Model
{
    public class Owner : ISerializable
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Surname { get; set; }

        public bool IsSuper {  get; set; }
        public double AverageGrade { get; set; }
        public int Ranking { get; set; }

        public Owner()
        {

        }


        public string[] ToCSV()
        {
            String super = "Super";
            if (!IsSuper)
                super = "NotSuper";
            string[] csvValues = {Id.ToString(),Name,Surname,super,AverageGrade.ToString(),Ranking.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] csvValues)
        {
            Id = Convert.ToInt32(csvValues[0]);
            Name = csvValues[1];
            Surname = csvValues[2];

            if (csvValues[3] == "Super")
                IsSuper = true;
            else
                IsSuper = false;

            AverageGrade = Convert.ToDouble(csvValues[4]);
            Ranking = Convert.ToInt32(csvValues[5]);
        }
    }
}
