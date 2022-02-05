using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;

namespace IconExtract.Models
{
    public class IconObject : ObservableObject
    {
        private int _index;

        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }

        private BitmapImage _image;

        public BitmapImage Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public IconObject(int index, BitmapImage image)
        {
            _index = index;
            _image = image;
        }
    }
}