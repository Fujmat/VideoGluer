using Accord;
using Accord.Video.FFMPEG;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VideoGluer
{
    static class GetPathes
    {
        private static string[] fileEntries;
        private static int CountGetPathVideos;

        private static string[] Files = new string[6]
        {
            "C:/Users/User/Desktop/Videopapka/01_2022_05_18_09_39_00__09_45_50.mp4",
            "C:/Users/User/Desktop/Videopapka/01_2022_05_18_09_39_00__09_59_59.mp4",
            "C:/Users/User/Desktop/Videopapka/02_2022_05_18_09_39_00__09_45_50.mp4",
            "C:/Users/User/Desktop/Videopapka/04_2022_05_18_09_39_00__09_59_06.mp4",
            "C:/Users/User/Desktop/Videopapka/06_2022_05_18_09_39_00__09_45_50.mp4",
            "C:/Users/User/Desktop/Videopapka/07_2022_05_18_09_39_00__09_45_50.mp4",

        };

        public static void GetPath(string[] Files)
        {
            VideoFileReader[] videos = new VideoFileReader[Files.Length];
            for (int i = 0; i < Files.Length; i++)
            {
                videos[i] = new VideoFileReader();
                videos[i].Open(Files[i]);
            }

            for (int i = 0; i < 100; i++)//количество кадров
            {
                for (int j = 0; j < Files.Length; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        if (Files[j][0] == '0' && Files[j][1] == (char)k+1 && videos[j].FrameCount < i)
                        {
                            //Вызываем метод GetFrames(videos[j], i)
                            //Frames.GetFrames(ref videos[0], i);
                        }
                    }
                }
            }
        }

        public static string[][] GetPath(ref VideoFileReader[] videos, ref string[] filenames)
        {
            int iterator = 0;
            string[][] videoFiles = new string[6][];

            List<string> filenamesList = new List<string>();

            for (int i = 0; i < filenames.Length; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (filenames[i][0] == '0' && filenames[i][1] == '1')//&& (filenames[i + 1][0] == '0' && filenames[i + 1][1] != (char)j)
                    {
                        videoFiles[iterator][iterator] = filenames[iterator];

                        filenamesList.Add(filenames[iterator]);

                        iterator++;
                    }
                }
                iterator = 0;
            }

            

            return videoFiles;
        }


        public static List<List<string>> GetPathVideos(ref List<string> fileNames)//поиск видео с одной камеры. Должен вызываться в первую очередь 
        {
            /*List<string> pathVideos1 = new List<string>();
            List<string> pathVideos2 = new List<string>();
            List<string> pathVideos3 = new List<string>();
            List<string> pathVideos4 = new List<string>();
            List<string> pathVideos5 = new List<string>();
            List<string> pathVideos6 = new List<string>();
            List<string> pathVideos7 = new List<string>();
            List<string> pathVideos8 = new List<string>();
            List<List<string>> pathVideos9 = new List<List<string>>();
            List<string> pathVideos10 = new List<string>();*/

            /*string[,] pathVideos = new string[fileNames.Count, 8];

            List<List<string>> wrwr = new List<List<string>>();*/

            List<List<string>> list = new List<List<string>>(); //инициализация
            for (int i = 0; i < 8; i++)
            {
                list.Add(new List<string>());
            }
            //добавление новой строки
            //list[0].Add("asd");//добавление столбца в новую строку
            //list[0][0];//обращение к первому столбцу первой строки

            //string[][] weew = new string[6][];
            //string[][] pathVideos10;

            for (int i = 0; i < fileNames.Count; i++)
            {
                if (fileNames[i][0] == '0' && fileNames[i][1] == '1')//проверяет первый и второй символ строки
                {
                    //pathVideos1.Add(fileEntries[i]);
                    //pathVideos9[0].Add(fileNames[i]);
                    //pathVideos[i,0] = fileNames[i];
                    //weew[i][0] = fileNames[i];
                    list[0].Add(fileNames[i]);
                    
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '2')
                {
                    //pathVideos2.Add(fileEntries[i]);
                    //pathVideos9[1].Add(fileNames[i]);
                    //pathVideos[i,1] = fileNames[i];
                    list[1].Add(fileNames[i]);
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '3')
                {
                    //pathVideos3.Add(fileEntries[i]);
                    //pathVideos9[2].Add(fileNames[i]);
                    //pathVideos[i,2] = fileNames[i];
                    list[2].Add(fileNames[i]);
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '4')
                {
                    //pathVideos4.Add(fileEntries[i]);
                    //pathVideos9[3].Add(fileNames[i]);
                    //pathVideos[i,3] = fileNames[i];
                    list[3].Add(fileNames[i]);
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '5')
                {
                    //pathVideos5.Add(fileEntries[i]);
                    //pathVideos9[4].Add(fileNames[i]);
                    //pathVideos[i,4] = fileNames[i];
                    list[4].Add(fileNames[i]);
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '6')
                {
                    //pathVideos6.Add(fileEntries[i]);
                    //pathVideos9[5].Add(fileNames[i]);
                    //pathVideos[i,5] = fileNames[i];
                    list[5].Add(fileNames[i]);
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '7')
                {
                    //pathVideos7.Add(fileEntries[i]);
                    //pathVideos9[6].Add(fileNames[i]);
                    //pathVideos[i,6] = fileNames[i];
                    list[6].Add(fileNames[i]);
                }
                else if (fileNames[i][0] == '0' && fileNames[i][1] == '8')
                {
                    //pathVideos8.Add(fileEntries[i]);
                    //pathVideos9[7].Add(fileNames[i]);
                    //pathVideos[i,7] = fileNames[i];
                    list[7].Add(fileNames[i]);
                }

            }
            return list;




            /*if (pathVideos1.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos2.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos3.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos4.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos5.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos6.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos7.Count > 1)
            {
                CountGetPathVideos++;
            }

            if (pathVideos8.Count > 1)
            {
                CountGetPathVideos++;
            }


            if (pathVideos1.Count > 1)
            {
                GlueVideos(pathVideos1, 1);
            }
            if (pathVideos1.Count == 1)
            {
                filepathes.Add(pathVideos1[0]);
            }


            if (pathVideos2.Count > 1)
            {
                GlueVideos(pathVideos2, 2);
            }
            if (pathVideos2.Count == 1)
            {
                filepathes.Add(pathVideos2[0]);
            }


            if (pathVideos3.Count > 1)
            {
                GlueVideos(pathVideos3, 3);
            }
            if (pathVideos3.Count == 1)
            {
                filepathes.Add(pathVideos3[0]);
            }


            if (pathVideos4.Count > 1)
            {
                GlueVideos(pathVideos4, 4);
            }
            if (pathVideos4.Count == 1)
            {
                filepathes.Add(pathVideos4[0]);
            }


            if (pathVideos5.Count > 1)
            {
                GlueVideos(pathVideos5, 5);
            }
            if (pathVideos5.Count == 1)
            {
                filepathes.Add(pathVideos5[0]);
            }


            if (pathVideos6.Count > 1)
            {
                GlueVideos(pathVideos6, 6);
            }
            if (pathVideos6.Count == 1)
            {
                filepathes.Add(pathVideos6[0]);
            }


            if (pathVideos7.Count > 1)
            {
                GlueVideos(pathVideos7, 7);
            }
            if (pathVideos7.Count == 1)
            {
                filepathes.Add(pathVideos7[0]);
            }


            if (pathVideos8.Count > 1)
            {
                GlueVideos(pathVideos8, 8);
            }
            if (pathVideos8.Count == 1)
            {
                filepathes.Add(pathVideos8[0]);
            }*/
        }

        public static List<List<string>> GetPathVideos(ref PathVideo pathVideo)//поиск видео с одной камеры.
        {
            List<List<string>> list = new List<List<string>>(); 
            for (int i = 0; i < 8; i++)
            {
                list.Add(new List<string>());
            }

            for (int i = 0; i < pathVideo.FullName.Length; i++)
            {
                if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '1')//проверяет первый и второй символ строки
                {
                    list[0].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '2')
                {
                    list[1].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '3')
                {
                    list[2].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '4')
                {
                    list[3].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '5')
                {
                    list[4].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '6')
                {
                    list[5].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '7')
                {
                    list[6].Add(pathVideo.FullName[i]);
                }
                else if (pathVideo.Name[i][0] == '0' && pathVideo.Name[i][1] == '8')
                {
                    list[7].Add(pathVideo.FullName[i]);
                }
            }

            /*VideoFileReader[] videos = new VideoFileReader[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (list[i][j] != null)
                    {
                        videos[i].Open(list[i][j]);
                    }
                }
            }*/

            return list;
        }

        public static List<List<VideoFileReader>> Opener(ref List<List<string>> path)
        {
            List<List<VideoFileReader>> list = new List<List<VideoFileReader>>();

            for (int i = 0; i < 8; i++)
            {
                list.Add(new List<VideoFileReader>());
            }

            int iterator = 0;
            foreach (var file in path)
            {
                foreach (var video in file)
                {
                    VideoFileReader v = new VideoFileReader();
                    v.Open(video);
                    list[iterator].Add(v);
                }
                iterator++;
            }
            return list;
        }

        public static List<List<VideoFileReader>> DeleteNullLists(ref List<List<VideoFileReader>> videos)
        {
            List<List<VideoFileReader>> list = new List<List<VideoFileReader>>(); 
            for (int i = 0; i < videos.Count; i++)
            {
                if (videos[i].Count != 0)
                {
                    list.Add(new List<VideoFileReader>(videos[i]));
                }
            }
            return list;
        }

        public static VideoFileReader[][] ConvertToArray(ref List<List<VideoFileReader>> videos)
        {
            VideoFileReader[][] VideosArray = new VideoFileReader[videos.Count][];

            for (int i = 0; i < videos.Count; i++)
            {
                VideosArray[i] = videos[i].ToArray();
            }

            return VideosArray;
        }

        public static string[] GetFileNames(string[] filepath)
        {
            string[] filenames = new string[filepath.Length];

            for (int i = 0; i < filepath.Length; i++)
            {
                filenames[i] = Path.GetFileName(filepath[i]);
            }
            return filenames;
        }
    }
}
