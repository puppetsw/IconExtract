using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Media.Imaging;

namespace IconExtract.Helpers
{
    public static class BitmapHelpers
    {
        public static async Task<BitmapImage> ConvertImage(byte[] image)
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
