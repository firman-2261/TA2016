using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace View
{
    public static class Utility
    {
        public static BitmapImage LoadImage(string NamaGambar)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(Environment.CurrentDirectory + "\\Gambar\\" + NamaGambar + ".png");
            b.EndInit();
            return b;
        }

        public static Image SetImage(string NamaGambar)
        {
            Image Temp = new Image();
            Temp.Source = LoadImage(NamaGambar);
            Temp.Stretch = Stretch.Fill;
            return Temp;
        }
    }
}
