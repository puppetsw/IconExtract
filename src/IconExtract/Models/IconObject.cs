using System.Drawing;
using Microsoft.UI.Xaml.Media.Imaging;

namespace IconExtract.Models
{
    public class IconObject
    {
        public int Index { get; }

        public BitmapImage Image { get; }

        public Bitmap Bitmap { get; }

        public string FileName { get; }

        public byte[] BitmapData { get; }

        public IconObject(string fileName, int index, BitmapImage image, Bitmap bitmap, byte[] bitmapData)
        {
            Index = index;
            Image = image;
            FileName = fileName;
            Bitmap = bitmap;
            BitmapData = bitmapData;
        }
    }
}