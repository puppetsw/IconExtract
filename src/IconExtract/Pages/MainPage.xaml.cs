using System.Linq;
using Microsoft.UI.Xaml.Controls;
using IconExtract.Models;
using IconExtract.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IconExtract.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<MainPageViewModel>();

        public static MainPage MainPageInstance { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            ImageGridView.SelectionChanged += GridViewOnSelectionChanged;
            MainPageInstance = this;
        }
        private void GridViewOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var addedItems = e.AddedItems.Cast<IconObject>().ToList();
            var removedItems = e.RemovedItems.Cast<IconObject>().ToList();
            ViewModel.SelectItems(addedItems, removedItems);
        }

    }
}
