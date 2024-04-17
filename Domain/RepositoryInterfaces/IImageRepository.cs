using BookingApp.Model;
using BookingApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryInterfaces
{
    public interface IImageRepository
    {
        public List<Image> GetAll();
        public Image Save(Image Image);
        public int NextId();
        public Image Update(Image image);
        public void Delete(Image Image);
        public List<Image> GetByEntity(int id, Enums.ImageType type);
    }
}
