using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Resources;
using BookingApp.Serializer;



namespace BookingApp.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
 
        public String TouristName { get; set; }
        public String TouristSurname { get; set; }
        public int TouristBirth {  get; set; }

        public DateTime IssuingDate { get; set; }

        public Voucher() { }


      
        public Voucher(int id, string touristName, string touristSurname, int touristBirth, DateTime issuingDate)
        {
            Id = id;
            TouristName = touristName;
            TouristSurname = touristSurname;
            TouristBirth = touristBirth;
            IssuingDate = issuingDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristName, TouristSurname, TouristBirth.ToString(),  IssuingDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            Id = Convert.ToInt32(values[0]);
            TouristName = values[1];
            TouristSurname = values[2];
            TouristBirth = int.Parse(values[3]);
            IssuingDate = DateTime.Parse(values[4]);


        }

    
    }

}
