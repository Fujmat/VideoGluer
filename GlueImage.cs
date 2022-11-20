using Accord.Video.FFMPEG;
using System.Drawing;

namespace VideoGluer
{
    static class GlueImage
    {
        public static Bitmap GlueFrames(params Bitmap[] bitmaps)
        {
            Bitmap bmp = new Bitmap(1056, 576);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(bitmaps[0], 0, 0);

                g.DrawImage(bitmaps[1], 0, bitmaps[1].Height);

                g.DrawImage(bitmaps[2], bitmaps[2].Width, 0);

                g.DrawImage(bitmaps[3], bitmaps[3].Width, bitmaps[3].Height);

                g.DrawImage(bitmaps[4], bitmaps[4].Width * 2, 0);

                g.DrawImage(bitmaps[5], bitmaps[5].Width * 2, bitmaps[5].Height);
            }
            return bmp;
            //GlFr = bmp;
        }

        public static Bitmap GlueFrames(ref Bitmap[] bitmaps)
        {
            Bitmap bmp = new Bitmap(1056, 576);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(bitmaps[0], 0, 0);

                g.DrawImage(bitmaps[1], 0, bitmaps[1].Height);

                g.DrawImage(bitmaps[2], bitmaps[2].Width, 0);

                g.DrawImage(bitmaps[3], bitmaps[3].Width, bitmaps[3].Height);

                g.DrawImage(bitmaps[4], bitmaps[4].Width * 2, 0);

                g.DrawImage(bitmaps[5], bitmaps[5].Width * 2, bitmaps[5].Height);
            }
            return bmp;
            //GlFr = bmp;
        }
    }
}

