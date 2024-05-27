using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class ForumsCommentService
    {
        public IForumsCommentRepository forumsRepository;
        public ForumsCommentService(IForumsCommentRepository _forumsRepository)
        {
            forumsRepository = _forumsRepository;
        }
        public static ForumsCommentService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<ForumsCommentService>();
        }
        public List<ForumsComment> GetAll()
        {
            return forumsRepository.GetAll();
        }
      
        public List<ForumsComment> GetCommentByForumId(int forumId)
        {
            List<ForumsComment> lista = new List<ForumsComment>();
            foreach (ForumsComment comment in GetAll()) {
                if (comment.ForumId == forumId)
                    lista.Add(comment);
            }
            return lista;
        }
        public Enums.ReservationChangeStatus CheckSuperForum(int forumId)
        {
            int numberOfSuperCommentG = GetNumberOfSuperCommentGuest(forumId);
            int numberOfSuperCommentO = GetNumberOfSuperCommentOwner(forumId);
            Enums.ReservationChangeStatus status = Enums.ReservationChangeStatus.RejectedSeen;
            if (numberOfSuperCommentG >= 2 || numberOfSuperCommentO >= 10  )
                    status = Enums.ReservationChangeStatus.Pending;
            return status;
        }
        public int GetNumberOfSuperCommentOwner(int userId)
        {
            int numberOfSuperComment = 0;
           
            return numberOfSuperComment;
        }
        public int GetNumberOfSuperCommentGuest( int forumId)
        {
            int numberOfSuperComment=0;
            foreach (ForumsComment comment in GetAll()) {
                if (comment.ForumId == forumId && comment.Status == 1 && comment.UserType == Enums.UserType.Guest)
                    numberOfSuperComment++;
            }
            return numberOfSuperComment;
            }
        public ForumsComment Save(ForumsComment forum)
        {
            return forumsRepository.Save(forum);
        }
        public void Delete(ForumsComment forum)
        {
            forumsRepository.Delete(forum);
        }
        public ForumsComment Update(ForumsComment forum)
        {
            return forumsRepository.Update(forum);
        }
    }
}
