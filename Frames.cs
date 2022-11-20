using Accord.Video.FFMPEG;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VideoGluer
{
    class Frames
    {
        private static readonly int width = Form1.VideoWidth;
        private static readonly int height = Form1.VideoHeight;
        private static readonly Bitmap Black = new Bitmap(width, height);
        private static Bitmap IsEnd;//используется для определения коныа файла. Если null - файл закончился
        public static Bitmap GetFrames(ref VideoFileReader video, in int num)//num передаёт порядковый номер кадра
        {
            var countFrames = video.FrameCount;//получает количество кадров в видео
            if (num >= countFrames)//если общее количество обработанных кадров больше или равно кадров в видео, вместо недостающего кадра вставляется чёрная картинка
            {
                return Black;
            }
            else
            {
                return video.ReadVideoFrame(num);//получает определённый кадр из видео
            }
            //return videoFrame;
        }

        public static Bitmap GetFrames(VideoFileReader video)
        {
            if (video != null)
            {
                return video.ReadVideoFrame();
            }
            else 
            { 
                return Black; 
            }
        }

        public static Bitmap GetFrames(ref VideoFileReader video)
        {
            if (video != null)
            {
                return video.ReadVideoFrame();
            }
            else
            {
                return Black;
            }
        }

        public static Bitmap GetFrames(ref List<VideoFileReader> video, in int num)
        {
            for (int i = 0; i < video.Count; i++)
            {
                if (num <= video[i].FrameCount)
                {
                    return video[i].ReadVideoFrame();
                }
            }
            return Black;
        }

        public static Bitmap GetFrames(List<VideoFileReader> video, in int num)//num количество видеофайлов в списке
        {
            for (int i = 0; i < num; i++)
            {
                IsEnd = video[i].ReadVideoFrame();
                if (IsEnd != null)
                {
                    return IsEnd;
                }
            }
            return Black;
        }

        public static Bitmap GetFrames(ref VideoFileReader[] video, in int num)//num количество видеофайлов в списке
        {
            for (int i = 0; i < num; i++)
            {
                IsEnd = video[i].ReadVideoFrame();
                if (IsEnd != null)
                {
                    return IsEnd;
                }
            }
            return Black;
        }

        public static long SumFrameCount(List<VideoFileReader> videos)
        {
            return videos.Sum(video => video.FrameCount);
        }
        public static long MaxFrameCount(List<List<VideoFileReader>> videos)
        {
            long max = 0;
            foreach (var v in videos)
            {
                if (v.Count != 0)
                {
                    if (SumFrameCount(v) > max)
                    {
                        max = SumFrameCount(v);
                    }
                }
            }
            return max;
        }
    }
  }
