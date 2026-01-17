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
    /// Interaction logic for StockLevelView.xaml
    /// </summary>
    public partial class StockLevelView : UserControl
    {
        public StockLevelView()
        {
            InitializeComponent();
        }

        private async void StockLevelView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is not StockLevelViewModel vm)
                return;

            // Vänta tills databasen är redo
            while (!App.DatabaseReady)
            {
                await Task.Delay(50);
            }

            await vm.InitializeAsync();
        }

    }
}
