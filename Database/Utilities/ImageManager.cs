﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace Database.Utilities
{
    public static class ImageManager
    {
        public static BitmapImage SelectImage(int width, int height)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Files|*.jpg;*.jpeg;*.png;*.gif;";
            if (dlg.ShowDialog() != true) return null;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(dlg.FileName);
            image.DecodePixelWidth = width;
            image.DecodePixelHeight = height;
            image.EndInit();
            return image;
        }

        public static byte[] ImageToBytes(ImageSource image)
        {
            BitmapSource bitmapSource = image as BitmapSource;
            if (bitmapSource == null) return null;
            byte[] bytes = null;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                bytes = stream.ToArray();
            }
            return bytes;
        }

        public static ImageSource BytesToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public static byte[] BlobToBytes(SQLiteDataReader reader, int columnNumber)
        {
            byte[] buffer = new byte[2048];
            long bytesRead;
            long fieldOffset = 0;
            try { bytesRead = reader.GetBytes(columnNumber, fieldOffset, buffer, 0, buffer.Length); }
            catch (InvalidCastException) { return null; }
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(columnNumber, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }
    }
}
