using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class OwnerService
    {
        public IOwnerRepository OwnerRepository { get; set; }

        public OwnerService()
        {
            OwnerRepository = new OwnerRepository();
        }

        public void UpdateOwnerStatus(Owner owner)
        {
             owner.AverageGrade = owner.AverageGrade / owner.GradeCount;

            owner.AverageGrade = Math.Round(owner.AverageGrade, 3);
            
            if (!owner.IsSuper && owner.GradeCount >= 50 &&  owner.AverageGrade > 4.5)
            {
                owner.IsSuper = true;
                
            }
            else if (owner.IsSuper && owner.AverageGrade < 4.5)
            {
                owner.IsSuper = false;
                
            }

            OwnerRepository.Update(owner);
        }


    }
}
