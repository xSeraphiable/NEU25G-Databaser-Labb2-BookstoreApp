using BookstoreApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.ViewModel
{
    internal class SelectStoreViewModel : ViewModelBase
    {
        public ObservableCollection<Store> Stores { get; }

        private Store? _selectedStore;
        public Store? SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasSelectedStore));

            }
        }
        public bool HasSelectedStore => SelectedStore != null;
       
        public SelectStoreViewModel(IEnumerable<Store> stores, Store? currentStore)
        {
            Stores = new ObservableCollection<Store>(stores);
            SelectedStore = currentStore; //TODO: behöver jag detta?
        }
    }
}
