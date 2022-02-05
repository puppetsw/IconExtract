using System;
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
        public static AppWindow AppWindow { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();
            AppWindow = this.GetAppWindow();
            AppWindow.Title = "IconExtract";
            AppWindow.SetIcon("Assets\\app.ico");
        }
    }
}
