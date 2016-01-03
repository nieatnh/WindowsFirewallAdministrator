using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using D = System.Drawing;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

namespace WindowsFirewallAdministrator.Tools
{
    public static class Extensors
    {
        public static string ShortPath(this FileInfo fileInfo)
        {
            StringBuilder shortPath = new StringBuilder(255);
            int length = WinAPI.GetShortPathName(fileInfo.FullName, shortPath, shortPath.Capacity);
            return shortPath.ToString().TrimEnd();
        }
        public static ImageSource ToImageSource(this D.Icon icon)
        {
            D.Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!WinAPI.DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }

            return wpfBitmap;
        }
    }
}
