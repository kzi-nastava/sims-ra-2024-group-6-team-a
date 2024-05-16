using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Resources;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Language : ISerializable
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public Language() {}

        public Language(int id, string name)
        {
            Id = id;
            Name = name;

        }

        public string[] ToCSV()
        {
            string[] cssValues = { Id.ToString(), Name };
            return cssValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
        }

    }
}
