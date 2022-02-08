using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using IconExtract.Helpers;
using IconExtract.Services.Interfaces;

namespace IconExtract.Services.Implementation
{
    public class DialogService : IDialogService
    {
        public string[] FileExtensions { get; set; }

        public async Task<IStorageFile> ShowDialog()
        {
            var filePicker = new FileOpenPicker();
            filePicker.SetOwnerWindow(MainWindow.Default);

            foreach (string fileExtension in FileExtensions)
            {
                filePicker.FileTypeFilter.Add(fileExtension);
            }

            var folder = await filePicker.PickSingleFileAsync();

            if (folder == null || string.IsNullOrEmpty(folder.Path))
            {
                return null;
            }

            return folder;
        }
    }
}
