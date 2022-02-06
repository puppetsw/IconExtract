using IconExtract.Helpers;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IconExtract
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static AppWindow AppWindow { get; set; }

        public static MainWindow Default { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();
            AppWindow = this.GetAppWindow();
            AppWindow.Title = "IconExtract";
            Default = this;
        }
    }
}
