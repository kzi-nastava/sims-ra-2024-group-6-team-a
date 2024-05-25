using BookingApp.Model;
using BookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for BlogView.xaml
    /// </summary>
    public partial class BlogView : Window
    {
        BlogViewVM ViewModel { get; set; }
        public BlogView(AccommodationBlog blog,String image,String owner)
        {
            ViewModel = new BlogViewVM(blog,image,owner);
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Report();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Post the comment?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ViewModel.ConfirmComment(NewComment.Text);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                Close();
            }
            else if(e.Key == Key.Enter)
            {
                ViewModel.CommentDetailed();

            }
        }
    }
}
