using System;
using IconExtract.Services.Implementation;
using IconExtract.Services.Interfaces;
using IconExtract.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IconExtract
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window _window;

        private IServiceProvider Services { get; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            Services = ConfigureServices();
            Ioc.Default.ConfigureServices(Services);
        }
        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new();

            services
                // TODO: Loggers:

                // TODO Settings:

                // Dialogs:
                .AddSingleton<IDialogService, DialogService>()

                // Helpers:
                .AddSingleton<IExtractService, ExtractService>()
                .AddScoped<IExportService, ExportService>()

                // ViewModels:
                .AddScoped<MainPageViewModel>()

                // TODO: Views:

                ; // End of service configuration


            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
