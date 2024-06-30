using BookingApp.ApplicationServices;
using BookingApp.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.ViewModels
{
    public class BlogViewVM : IObserver
    { 
        public AccommodationBlog Blog { get; set; }
        public ObservableCollection<Comment> Comments { get; set; }
        public Comment SelectedComment {  get; set; }

        public String OwnerName { get; set; }

        public String MainImage { get; set; }

        public ICommand ReportCommand { get; }
        public ICommand ConfirmCommentCommand { get; }
        public ICommand CommentDetailedCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public BlogViewVM(AccommodationBlog blog,String image,String owner) 
        {
            Blog = blog;
            MainImage = image;
            OwnerName = owner;
            Comments = new ObservableCollection<Comment>();
            ReportCommand = new RelayCommand(Report);
            ConfirmCommentCommand = new RelayCommand(ConfirmComment);
            CommentDetailedCommand = new RelayCommand(CommentDetailed);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            Update();
        }

        public void Update()
        {
            Comments.Clear();
            foreach(Comment comment in CommentService.GetInstance().GetByBlog(Blog))
            {
                Comments.Add(comment);
            }

        }

        internal void Report(object parameter)
        {
            if (Blog.Reported == false)
            { 

                if (MessageBox.Show("Are you sure?", "Report the user", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    Blog.Reported = true;
                    AccommodationBlogService.GetInstance().Update(Blog);
                }
            }
            else
            {
                MessageBox.Show("Already reported","This user is already reported", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        internal void ConfirmComment(object parameter)
        {
            if (MessageBox.Show("Are you sure?", "Post the comment?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string text = parameter as string;
                Comment comment = new Comment(Blog.Id, text, OwnerName, 0, "Owner");
                CommentService.GetInstance().Save(comment);
                Update();
            }
        }

        internal void CommentDetailed(object parameter)
        {
            if(SelectedComment != null)
            {
                if (MessageBox.Show("Are you sure?", "Report the comment", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SelectedComment.Ratio += 1;
                    CommentService.GetInstance().Update(SelectedComment);
                    Update();
                }

            }
        }

        private void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
