using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using IconExtract.Services.Implementation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IconExtract.Dialogs
{
    public sealed partial class ExportDialog : ContentDialog
    {

        public ObservableCollection<ExportFormat> ExportFormats { get; set; } = new();

        public ExportFormat SelectedItem { get; set; }

        public ExportDialog()
        {
            InitializeComponent();
            ExportFormats.Add(ExportFormat.Png);
            ExportFormats.Add(ExportFormat.Bmp);
            ExportFormats.Add(ExportFormat.Jpeg);
            ExportFormats.Add(ExportFormat.Ico);
            SelectedItem = ExportFormats[0];
        }
    }
}
