using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace 拼图游戏
{
    class CutPicture
    {
        public static string picturePath = "";
        public static List<Bitmap> BitMapList = null;
        public static Image Resize(string path, int iwidth, int iheignt)
        {
            Image thumbnail = null;
            try
            {
                var img = Image.FromFile(path);
                thumbnail = img.GetThumbnailImage(iwidth, iheignt, null, IntPtr.Zero);
                thumbnail.Save(Application.StartupPath.ToString() + "//Picture//img.jpeg");
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return thumbnail;
        }
        public static Bitmap Cut(Image b, int startX, int startY, int iwidth, int iheight)
        {
            if (b == null)
            { return null; }
            int w = b.Width;
            int h = b.Height;
            if (startX >= w || startY >= h)
            { return null; }
            if (startX + iwidth > w)
            { iwidth = w - startX; }
            if (startY + iheight > h)
            { iheight = h - startY; }
            try
            {
                Bitmap bmpout = new Bitmap(iwidth, iheight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpout);
                g.DrawImage(b, new Rectangle(0, 0, iwidth, iheight), new Rectangle(startX, startY, iwidth, iheight),
                    GraphicsUnit.Pixel);
                g.Dispose();
                return bmpout;
            }
            catch
            {
                return null;
            }
        }
    }
}
