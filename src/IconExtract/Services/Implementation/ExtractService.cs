using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using IconExtract.Models;
using IconExtract.Services.Interfaces;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace IconExtract.Services.Implementation
{
    public class ExtractService : IExtractService
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr ExtractIcon(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName, int iconIndex);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        private static extern int DestroyIcon(IntPtr icon);

        public async Task<IList<IconObject>> ExtractIconsFromDll(IStorageFile file)
        {
            var iconsList = new List<IconObject>();
            var currentProc = Process.GetCurrentProcess();

            IntPtr hIcon = IntPtr.Zero;

            try
            {
                int iconCount = (int)ExtractIcon(currentProc.Handle, file.Path, -1);
                for (int i = 0; i < iconCount; i++)
                {
                    hIcon = ExtractIcon(currentProc.Handle, file.Path, i);

                    using var icon = Icon.FromHandle(hIcon);

                    var image = icon.ToBitmap();
                    var bitmapData = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
                    var bitmapImage = await ConvertImage(bitmapData);

                    var iconObject = new IconObject(i, bitmapImage);
                    iconsList.Add(iconObject);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if(hIcon != IntPtr.Zero)
                {
#pragma warning disable CA1806 // Do not ignore method results
                    DestroyIcon(hIcon);
#pragma warning restore CA1806 // Do not ignore method results
                }
            }

            return iconsList;
        }

        private static async Task<BitmapImage> ConvertImage(byte[] image)
        {
            using InMemoryRandomAccessStream ms = new();

            using (DataWriter writer = new(ms.GetOutputStreamAt(0)))
            {
                writer.WriteBytes(image);
                await writer.StoreAsync();
            }

            var output = new BitmapImage();
            await output.SetSourceAsync(ms);
            return output;
        }
    }
}