using BookstoreApp.ViewModel;
using BookstoreApp.Views;
using System.Windows;


namespace BookstoreApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel();
            DataContext = vm;

            var dialogVm = new SelectStoreViewModel(vm.Stores, vm.SelectedStore);

            var dialog = new SelectStoreWindow
            {
                DataContext = dialogVm
            };

            if (dialog.ShowDialog() == true)
            {
                vm.SelectedStore = dialogVm.SelectedStore;
            }
        }
    }
}