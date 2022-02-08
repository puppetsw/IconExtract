using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.Pickers;
using IconExtract.Dialogs;
using IconExtract.Helpers;
using IconExtract.Models;
using IconExtract.Pages;
using IconExtract.Services.Interfaces;
using Microsoft.UI.Xaml.Controls;

namespace IconExtract.Services.Implementation
{
    public class ExportService : IExportService
    {
        public async Task ExportTo(IEnumerable<IconObject> icons, ExportFormat format, IStorageFolder destinationFolder)
        {
            foreach (IconObject icon in icons)
            {
                var file = await destinationFolder.CreateFileAsync($"{icon.FileName}_{icon.Index}.{format.ToString().ToLower()}");

                await Task.Run( () =>
                {
                    switch (format)
                    {
                        case ExportFormat.None:
                            break;
                        case ExportFormat.Png:
                            icon.Bitmap.Save(file.Path, ImageFormat.Png);
                            break;
                        case ExportFormat.Bmp:
                            icon.Bitmap.Save(file.Path, ImageFormat.Png);
                            break;
                        case ExportFormat.Jpeg:
                            icon.Bitmap.Save(file.Path, ImageFormat.Png);
                            break;
                        case ExportFormat.Ico:
                            var hIcon = icon.Bitmap.GetHicon();
                            var ico = Icon.FromHandle(hIcon);
                            using (var fs = new FileStream(file.Path, FileMode.OpenOrCreate))
                            {
                                ico.Save(fs);
                            }
                            Win32Helpers.DestroyIcon(hIcon);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(format), format, null);
                    }
                });
            }
        }

        public async Task<(ExportFormat, IStorageFolder)> ShowDialog()
        {
            var dialog = new ExportDialog();

            // https://docs.microsoft.com/en-us/windows/winui/api/microsoft.ui.xaml.controls.contentdialog?view=winui-3.0
            // By default, content dialogs display modally relative to the root ApplicationView. When you use ContentDialog
            // inside of either an AppWindow or a XAML Island, you need to manually set the XamlRoot on the dialog to the
            // root of the XAML host.
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
            {
                dialog.XamlRoot = MainPage.MainPageInstance.XamlRoot;
            }

            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var folderPicker = new FolderPicker();
                folderPicker.SetOwnerWindow(MainWindow.Default);

                var folder = await folderPicker.PickSingleFolderAsync();

                if (folder == null || string.IsNullOrEmpty(folder.Path))
                {
                    return (ExportFormat.None, null);
                }

                return (dialog.SelectedItem, folder);
            }

            return (ExportFormat.None, null);
        }
    }

    public enum ExportFormat
    {
        None = 0,
        Png,
        Bmp,
        Jpeg,
        Ico
    }
}
