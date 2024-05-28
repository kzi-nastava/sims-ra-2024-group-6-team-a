using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ApplicationServices
{
    public class CommentService
    {
        public ICommentRepository CommentRepository { get; set; }

        public CommentService(ICommentRepository commentRepository)
        {
            CommentRepository = commentRepository;
        }

        public static CommentService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<CommentService>();
        }

        public List<Comment> GetAll()
        {
            return CommentRepository.GetAll();
        }

        public Comment Save(Comment comment)
        {
            return CommentRepository.Save(comment);
        }

        public void Delete(Comment comment)
        {
            CommentRepository.Delete(comment);
        }

        public Comment Update(Comment comment)
        {
            return CommentRepository.Update(comment); 
        }

        public List<Comment> GetByBlog(AccommodationBlog blog)
        {
            return CommentRepository.GetByBlog(blog);
        }
    }
}
