using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using IconExtract.Services.Interfaces;
using WinRT;

namespace IconExtract.Services.Implementation
{
    public class DialogService : IDialogService
    {
        public string[] FileExtensions { get; set; }

        public async Task<IStorageFile> ShowDialog()
        {
            var filePicker = new FileOpenPicker();

            //Get the Window's HWND
            var hwnd = App.MainWindowHandle;
            //https://github.com/microsoft/microsoft-ui-xaml/issues/4100
            //Make folder Picker work in Win32
            var initializeWithWindow = filePicker.As<IInitializeWithWindow>();
            initializeWithWindow.Initialize(hwnd);

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


        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }
    }
}