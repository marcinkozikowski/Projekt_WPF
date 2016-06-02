using MySql.Data.MySqlClient;
using Projekt_WPF_Solution.DataBaseClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projekt_WPF_Solution.Converters
{
    public class ImageConverter
    {
        public static BitmapImage GetImage(string commandText, int ID)
        {
            //IDBaccess db = new IDBaccess();
            //if(db.OpenConnection() == true)
            //{
            //    MySqlCommand cmd = db.CreateCommand();
            //    cmd.CommandText = commandText;
            //    cmd.Parameters.AddWithValue(@"ID", ID);
            //    byte[] buffer = (byte[])cmd.ExecuteScalar();

            //    var image = new BitmapImage();
            //    using (var mem = new MemoryStream(buffer))
            //    {
            //        mem.Position = 0;
            //        image.BeginInit();
            //        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            //        image.CacheOption = BitmapCacheOption.OnLoad;
            //        image.UriSource = null;
            //        image.StreamSource = mem;
            //        image.EndInit();
            //    }
            //    image.Freeze();
            //    return image;
            //}
            //else
            //{
            //    return GetNoPhoto();
            //}
            return GetNoPhoto();
        }

        public static byte[] ImageToBytes(BitmapImage image)
        {
            byte[] buffer;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                buffer = ms.ToArray();
            }
            return buffer;
        }

        public static BitmapImage GetNoPhoto()
        {
            string dir = null, imgdir;
            do
            {
                if (dir == null)
                    dir = Directory.GetCurrentDirectory();
                else
                    dir = Directory.GetParent(dir).ToString();
                imgdir = System.IO.Path.Combine(dir, "Images");
            } while (!Directory.Exists(imgdir));
            string imagePath = System.IO.Path.Combine(imgdir, "brakZdjecia.gif");
            return new BitmapImage(new Uri(imagePath));
        }
    }
}
