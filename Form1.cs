using Accord.Video.FFMPEG;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord;

namespace VideoGluer
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private string FolderName { get; set; }

        private static int Framerate;
        private static VideoCodec Videocodec = VideoCodec.MPEG4;
        private static int Bitrate = 2400000;
        public static int VideoWidth;
        public static int VideoHeight;

        public static List<List<VideoFileReader>> VideosArray;
        public static VideoFileReader[][] FinallyVideosArray;
        private List<List<string>> CompleteDirectory = new List<List<string>>();

        //private string[] directoryfolder;
        
        private string InstallVideoDirectory = "C:/Users/User/Desktop/FramesGluerTest/Video4.mp4";
        //private List<string> CompleteDirectory = new List<string>();
        private List<string> Names = new List<string>()
        {
            "01",
            "01",
            "02",
            "03",
            "03",
            "04",
            "05",
            "05",
            "06",
            "07",
            "07",
            "08"
        };

        private string FilePath { get; set; } = "C:/Users/User/Desktop/Videopapka/01_2022_05_18_09_39_00__09_45_50.mp4";
        private string[] Files = new string[6] 
        {
            "C:/Users/User/Desktop/Videopapka/01_2022_05_18_09_39_00__09_45_50.mp4",
            "C:/Users/User/Desktop/Videopapka/01_2022_05_18_09_39_00__09_59_59.mp4",
            "C:/Users/User/Desktop/Videopapka/02_2022_05_18_09_39_00__09_45_50.mp4",
            "C:/Users/User/Desktop/Videopapka/04_2022_05_18_09_39_00__09_59_06.mp4",
            "C:/Users/User/Desktop/Videopapka/06_2022_05_18_09_39_00__09_45_50.mp4",
            "C:/Users/User/Desktop/Videopapka/07_2022_05_18_09_39_00__09_45_50.mp4",

        };

        private string[] Filenames = new string[6]
        {
            "01_2022_05_18_09_39_00__09_45_50.mp4",
            "01_2022_05_18_09_39_00__09_59_59.mp4",
            "02_2022_05_18_09_39_00__09_45_50.mp4",
            "04_2022_05_18_09_39_00__09_59_06.mp4",
            "06_2022_05_18_09_39_00__09_45_50.mp4",
            "07_2022_05_18_09_39_00__09_45_50.mp4",

        };

        private void button1_Click(object sender, EventArgs e)
        {
            

            var openFileDialog1 = new FolderBrowserDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FolderName = openFileDialog1.SelectedPath;

                PathVideo pathVideo = new PathVideo();

                pathVideo.FullName = Directory.GetFiles(FolderName);//полное имя файла 
                pathVideo.Name = GetPathes.GetFileNames(pathVideo.FullName);//имя файла

                CompleteDirectory = GetPathes.GetPathVideos(ref pathVideo);
                VideosArray = GetPathes.Opener(ref CompleteDirectory);//работает, можно обрабатывать
                VideosArray = GetPathes.DeleteNullLists(ref VideosArray);//удаляет пустые элементы(несуществующие камеры)
                FinallyVideosArray = GetPathes.ConvertToArray(ref VideosArray);//конвертирует List в массив (для передачи
                                                                               //параметров с ref). Но на производительность
                                                                               //это не повлияло
            }
            GetVideoInf();

            int CountFrames = (int)Frames.MaxFrameCount(VideosArray);

            VideoFileWriter vfw = new VideoFileWriter();
            vfw.Open($"{InstallVideoDirectory}", VideoWidth * 3, VideoHeight * 2, Framerate, Videocodec, Bitrate);

            for (int i = 0; i < CountFrames; i++)
            {
                vfw.WriteVideoFrame(GlueImage.GlueFrames(Frames.GetFrames(ref FinallyVideosArray[0], FinallyVideosArray[0].Length),
                                                         Frames.GetFrames(ref FinallyVideosArray[1], FinallyVideosArray[1].Length),
                                                         Frames.GetFrames(ref FinallyVideosArray[2], FinallyVideosArray[2].Length),
                                                         Frames.GetFrames(ref FinallyVideosArray[3], FinallyVideosArray[3].Length),
                                                         Frames.GetFrames(ref FinallyVideosArray[4], FinallyVideosArray[4].Length),
                                                         Frames.GetFrames(ref FinallyVideosArray[5], FinallyVideosArray[5].Length)));
            }
            vfw.Close();

            /*for (int i = 0; i < Files.Length; i++)
            {
                videos[i] = new VideoFileReader();
                videos[i].Open(Files[i]);
            }*/






            /*var vivod = GetPathes.GetPathVideos(Names);
            var ssvvswv = vivod;*/
            /*GetVideoInf();

            VideoFileReader[] videos = new VideoFileReader[Files.Length];
            for (int i = 0; i < Files.Length; i++)
            {
                videos[i] = new VideoFileReader();
                videos[i].Open(Files[i]);
            }


            



            VideoFileWriter vfw = new VideoFileWriter();
            vfw.Open($"{InstallVideoDirectory}.mp4", VideoWidth*3, VideoHeight*2, Framerate, Videocodec, Bitrate);

            for (int i = 0; i < 1000; i++)
            {
                vfw.WriteVideoFrame(GlueImage.GlueFrames(Frames.GetFrames(ref videos[0], i),
                                                         Frames.GetFrames(ref videos[1], i),
                                                         Frames.GetFrames(ref videos[2], i),
                                                         Frames.GetFrames(ref videos[3], i),
                                                         Frames.GetFrames(ref videos[4], i),
                                                         Frames.GetFrames(ref videos[5], i)));
            }
            vfw.Close();

            for (int i = 0; i < Files.Length; i++)
            {
                videos[i].Close();
            }*/

            /*
             * Пока в папке есть видео, которое должно быть склеено сразу за следующим, алгоритм должен вытаскивать кадры из следующего 
             * видео, продолжая запись в тот же видеопоток, не требуя записи на диск.
             * Можно отсортировать массив путей по которым находятся видео, затем извлечь первые уникальные пути, потом
             * когда кадры закончатся, перейти к следующему пути, который должен быть склеен с первым и извлекать кадры из него
             * в тот же видеопоток.
             * Например: 011 => обработка(извлекаем кадры) => кадры закончились => 012 => обработка(извлекаем кадры)
             * 
             * Отсортируем массив:
             *  "01",
                "01",
                "02",
                "03",
                "03",
                "04",
                "05",
                "05",
                "06",
                "07",
                "07",
                "08"
            Проходим по элементам массива и сравниваем элементы. Если текущий элемент равен предыдущему, помечаем его номером как вторичный, 
            третичный и т.д. Если текущий элемент не равен предыдущему - он уникальный, тоесть должен обрабатываться первым.

             */

            /* for (int i = 0; i < vivod.Count; i++)
             {
                 for (int j = 0; j < vivod.Count; j++)
                 {
                     richTextBox1.Text = vivod[i][j] + "\n"; 
                 }
             }*/
        }

        
        private void GetVideoInf()
        {
            var video = new VideoFileReader();
            video.Open(FilePath);
            Framerate = (int)video.FrameRate;
            VideoWidth = video.Width;
            VideoHeight = video.Height;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}