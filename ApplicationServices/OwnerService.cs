using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.RepositoryInterfaces;
using BookingApp.Serializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class OwnerService
    {
        public IOwnerRepository OwnerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            OwnerRepository = ownerRepository;

        }

        public static OwnerService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<OwnerService>();
        }
        public Owner GetByOwnersId(int id)
        {
            return OwnerRepository.GetAll().Find(c => c.Id == id);
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


        public void UpdateOwner(AccommodationReservation reservation,Owner owner)
        {

            foreach (OwnerReview review in OwnerReviewService.GetInstance().GetAll())
            {
                if (reservation.Id == review.ReservationId)
                {
                    owner.GradeCount++;
                    owner.AverageGrade = owner.AverageGrade + ((review.Correctness + review.Cleanliness) / 2.0);
                }
            }

            OwnerRepository.Update(owner);
        }

        public List<Owner> GetAll()
        {
            return OwnerRepository.GetAll();
        }

        public String GetOwnerNameByAccommodation(int accommodationId)
        {
            Accommodation accommodation = AccommodationService.GetInstance().GetAll().Find(a => a.Id == accommodationId);
            int ownerId = accommodation.OwnerId;
            return OwnerRepository.GetAll().Find(o => o.Id == ownerId).Name;
        }


    }
}
