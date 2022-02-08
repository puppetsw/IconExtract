using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using IconExtract.Helpers;
using IconExtract.Models;
using IconExtract.Services.Interfaces;
using Windows.Storage;

namespace IconExtract.Services.Implementation
{
    public class ExtractService : IExtractService
    {
        public async Task<IList<IconObject>> ExtractIconsFromDll(IStorageFile file)
        {
            var iconsList = new List<IconObject>();
            var currentProc = Process.GetCurrentProcess();

            IntPtr hIcon = IntPtr.Zero;

            try
            {
                int iconCount = (int)Win32Helpers.ExtractIcon(currentProc.Handle, file.Path, -1);
                for (int index = 0; index < iconCount; index++)
                {
                    hIcon = Win32Helpers.ExtractIcon(currentProc.Handle, file.Path, index);

                    using var icon = Icon.FromHandle(hIcon);

                    var bitmap = icon.ToBitmap();
                    var bitmapData = (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));
                    var bitmapImage = await BitmapHelpers.ConvertImage(bitmapData);

                    var iconObject = new IconObject(Path.GetFileNameWithoutExtension(file.Name), index, bitmapImage, bitmap, bitmapData);
                    iconsList.Add(iconObject);
                    Win32Helpers.DestroyIcon(hIcon);
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
                    Win32Helpers.DestroyIcon(hIcon);
                }
            }

            return iconsList;
        }
    }
}
