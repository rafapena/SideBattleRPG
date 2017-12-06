using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.Graphics.Imaging;

namespace GameUtilities
{
    public static class ImageManager
    {
        private static async Task<WriteableBitmap> StockImage(string path)
        {
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var writeableBitmap = BitmapFactory.New(1, 1);
            //await writeableBitmap.SetSourceAsync(await storageFile.OpenReadAsync());
            return writeableBitmap;
        }

        public static async Task<WriteableBitmap> SelectImageFile()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");
            StorageFile chosen = await openPicker.PickSingleFileAsync();
            if (chosen == null) return null;
            using (IRandomAccessStream stream = await chosen.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                WriteableBitmap bmp = BitmapFactory.New((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                bmp.SetSource(stream);
                return bmp;
            }
        }

        public static async Task<byte[]> ImageToBytes(WriteableBitmap sourceImage)
        {
            if (sourceImage == null) return null;
            byte[] pixels;
            using (Stream stream = sourceImage.PixelBuffer.AsStream())
            {
                pixels = new byte[(uint)stream.Length];
                await stream.ReadAsync(pixels, 0, pixels.Length);
            }
            return pixels;
        }

        public static async Task<WriteableBitmap> BytesToImage(byte[] data, int recordedWidth, int recordedHeight)
        {
            if (data == null) return null;
            var image = BitmapFactory.New(recordedWidth, recordedHeight);
            using (Stream stream = image.PixelBuffer.AsStream()) { await stream.WriteAsync(data, 0, data.Length); }
            return image;
        }

        public class SubImagePageHelper
        {
            private WriteableBitmap image;
            public WriteableBitmap Image { get { return image; } }
            public string InputMessage { get; set; }

            public byte[] ImageData { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public int GridWidth { get; set; }
            public int GridHeight { get; set; }

            private int subImageWidth;
            private int subImageHeight;
            public int SubWidth { get { return subImageWidth; } }
            public int SubHeight { get { return subImageHeight; } }

            public async void Load()
            {
                image = await BytesToImage(ImageData, Width, Height);
            }
            public void Divide()
            {
                subImageWidth = Width / GridWidth;
                subImageHeight = Height / GridHeight;
            }
        }

    }
}
