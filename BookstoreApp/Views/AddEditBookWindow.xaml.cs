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
    /// Interaction logic for AddEditBookWindow.xaml
    /// </summary>
    public partial class AddEditBookWindow : Window
    {
        public AddEditBookWindow()
        {
            InitializeComponent();
            Loaded += AddEditBookWindow_Loaded;
        }

        private void AddEditBookWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is BookDetailViewModel vm)
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
