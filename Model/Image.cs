using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;
using BookingApp.Resources;

namespace BookingApp.Model
{
    
    public class Image : ISerializable
    {
        public int Id { get; set; } //id slike
        public string Path { get; set; } //relativni put (url) do slike
        public int EntityId { get; set; } // id smestaja ili ture
        public Enums.ImageType Type { get; set; } //


        public Image() { }

        public Image(string path,int entityId)
        {
            this.Path = path;
            this.EntityId = entityId;
        }

        public Image(string path, int entityId,Enums.ImageType type)
        {
            this.Path = path;
            this.EntityId = entityId;
            this.Type = type;

        }


        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Path.ToString(), EntityId.ToString(), Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Path = values[1];
            EntityId = Convert.ToInt32(values[2]);
            Type = (Enums.ImageType)Enum.Parse(typeof(Enums.ImageType), values[3]);
        }


    }
}
