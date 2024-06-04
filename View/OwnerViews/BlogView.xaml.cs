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


    }
}
