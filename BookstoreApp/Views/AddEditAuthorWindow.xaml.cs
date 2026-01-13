using BookstoreApp.ViewModel;
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

namespace BookstoreApp.Views
{
    /// <summary>
    /// Interaction logic for AddEditAuthorWindow.xaml
    /// </summary>
    public partial class AddEditAuthorWindow : Window
    {
        public AddEditAuthorWindow()
        {
            InitializeComponent();
            Loaded += AddEditAuthorWindow_Loaded;
        }

        private void AddEditAuthorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthorDetailViewModel vm)
            {
                vm.RequestClose += result =>
                {
                    DialogResult = result;
                    Close();
                };
            }
        }
    }
}
