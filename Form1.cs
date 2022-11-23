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
using System.Drawing.Text;

namespace VideoGluer
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.listBox1.DragDrop += new DragEventHandler(this.listBox1_DragDrop);
            this.listBox1.DragEnter += new DragEventHandler(this.listBox1_DragEnter);
        }

        private static int Framerate;
        private static VideoCodec Videocodec = VideoCodec.MPEG4;
        private static int Bitrate = 2400000;
        public static int VideoWidth;
        public static int VideoHeight;

        public static List<List<VideoFileReader>> VideosArray;//массив с открытыми видео
        public static VideoFileReader[][] FinallyVideosArray;//массив с открытыми видео
        private List<List<string>> CompleteDirectory = new List<List<string>>();//полные пути видео
        private string[] folderpath;

        private string InstallVideoDirectory = "C:/Users/User/Desktop/FramesGluerTest/";

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                button1.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }

        }



        /// <summary>
        /// Обробатывает список видеофайлов, указанных по заданному пути.
        /// </summary>
        /// <param name="path">Путь к папке с видеофайлами.</param>
        private void VideoHandler(string path)
        {
            string namefolder = new DirectoryInfo(path).Name;//path
            PathVideo pathVideo = new PathVideo();

            pathVideo.FullName = Directory.GetFiles(path);//полное имя файла//path 
            pathVideo.Name = GetPathes.GetFileNames(pathVideo.FullName);//название файла с расширением

            CompleteDirectory = GetPathes.GetPathVideos(ref pathVideo);
            VideosArray = GetPathes.Opener(ref CompleteDirectory);//работает, можно обрабатывать
            VideosArray = GetPathes.DeleteNullLists(ref VideosArray);//удаляет пустые элементы(несуществующие камеры)
            FinallyVideosArray = GetPathes.ConvertToArray(ref VideosArray);//конвертирует List в массив (для передачи
                                                                           //параметров с ref). Но на производительность
                                                                           //это не повлияло
                                                                           //}
            GetVideoInf(FinallyVideosArray);

            int CountFrames = (int)Frames.MaxFrameCount(VideosArray);

            VideoFileWriter vfw = new VideoFileWriter();
            vfw.Open($"{InstallVideoDirectory}/{namefolder}.mp4", VideoWidth * 3, VideoHeight * 2, Framerate, Videocodec, Bitrate);

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

            /*for (int i = 0; i < FinallyVideosArray.Length; i++)
            {
                for (int j = 0; j < FinallyVideosArray[i].Length; j++)
                {
                    FinallyVideosArray[i][j].Close();
                    FinallyVideosArray[i][j].Dispose();
                }
            }*/
            GC.Collect();
        }
        /// <summary>
        /// Открывает первое видео в списке и получает из него данные.
        /// </summary>
        /// <param name="videos"></param>
        private void GetVideoInf(VideoFileReader[][] videos)
        {
            Framerate = (int)videos[0][0].FrameRate;
            VideoWidth = videos[0][0].Width;
            VideoHeight = videos[0][0].Height;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            for (int i = 0; i < folderpath.Length; i++)
            {
                VideoHandler(folderpath[i]);
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            folderpath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            AddElementsInListBox(folderpath);
        }
        /// <summary>
        /// выводит название папки и все файлы в ней в ListBox
        /// </summary>
        /// <param name="data"></param>
        private void AddElementsInListBox(string[] data)
        {
            List<string[]> allpathes = new List<string[]>();
            string[] namefolder = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                namefolder[i] = new DirectoryInfo(data[i]).Name;
                allpathes.Add(Directory.GetFiles(data[i]));
            }

            for (int i = 0; i < allpathes.Count; i++)
            {
                listBox1.Items.Add(namefolder[i]);
                for (int j = 0; j < allpathes[i].Length; j++)
                {
                    listBox1.Items.Add($". . . .{allpathes[i][j]}");
                }
            }
        }
    }
}