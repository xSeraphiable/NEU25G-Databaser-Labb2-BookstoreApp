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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookstoreApp.Views
{
    /// <summary>
    /// Interaction logic for BooksView.xaml
    /// </summary>
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
        }

        private async void BooksView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is not AuthorsViewModel vm)
                return;

            while (!App.DatabaseReady)
            {
                await Task.Delay(50);
            }

            await vm.LoadAsync();
        }
    }
}
