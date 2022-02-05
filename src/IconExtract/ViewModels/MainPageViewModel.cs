using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using IconExtract.Models;
using IconExtract.Services.Interfaces;
using Microsoft.Toolkit.Mvvm.Input;

namespace IconExtract.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IExtractService _extractService;
        private readonly IDialogService _dialogService;

        private ObservableCollection<IconObject> _icons;

        public ObservableCollection<IconObject> Icons
        {
            get => _icons;
            set => SetProperty(ref _icons, value);
        }

        private IList<IconObject> _selectedItems;

        public IList<IconObject> SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private IconObject _selectedItem;

        public IconObject SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private double _imageSize;

        public double ImageSize
        {
            get => _imageSize;
            set => SetProperty(ref _imageSize, value);
        }

        private string _displayName;

        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public bool CanExport => SelectedItems is not null && SelectedItems.Any() && SelectedItems.Count > 0;

        public bool ShowEmptyText => !(SelectedItems is not null && Icons.Count > 0);

        public ICommand OpenFileCommand { get; }

        public ICommand ExportCommand { get; }

        public MainPageViewModel(IExtractService extractService, IDialogService dialogService)
        {
            _extractService = extractService;
            _dialogService = dialogService;

            Icons = new ObservableCollection<IconObject>();
            SelectedItems = new List<IconObject>();
            ImageSize = 32;

            // Commands
            OpenFileCommand = new AsyncRelayCommand(OpenFile);
        }

        private async Task OpenFile()
        {
            _dialogService.FileExtensions = new[] { ".exe", ".dll" };

            var file = await _dialogService.ShowDialog();

            if (file == null)
            {
                return;
            }

            var imageList = await _extractService.ExtractIconsFromDll(file);

            Icons = new ObservableCollection<IconObject>(imageList);
            DisplayName = file.Name;
            OnPropertyChanged(nameof(ShowEmptyText));
        }

        public void SelectItems(IEnumerable<IconObject> addedItems, IEnumerable<IconObject> removedItems)
        {
            foreach (IconObject addedItem in addedItems)
            {
                SelectedItems.Add(addedItem);
            }

            foreach (IconObject removedItem in removedItems)
            {
                SelectedItems.Remove(removedItem);
            }

            OnPropertyChanged(nameof(CanExport));
        }
    }
}