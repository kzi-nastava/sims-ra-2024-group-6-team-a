using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        public List<Voucher> GetAll();
        public Voucher Save(Voucher voucher);

        public void Delete(Voucher voucher);


        public int NextId();
    }
}
