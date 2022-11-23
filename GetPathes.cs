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
        /// <summary>
        /// Выволняет поиск видео с одной камеры и записывает пути видеофацйлов в соответствующий камере список.
        /// </summary>
        /// <param name="pathVideo"></param>
        /// <returns>Возвращает список списков путей видео, соответствующей камере.</returns>
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
            return list;
        }
        /// <summary>
        /// Открывает все видеофайлы в скисках.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Возвращает список списков видеофайлов, готовых к обработке</returns>
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
        /// <summary>
        /// Удаляет из списка камер элементы, не содержащие видео.
        /// </summary>
        /// <param name="videos"></param>
        /// <returns>Возвращает список списков не содержищих пустых списков.</returns>
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
        /// <summary>
        /// Конвертирует список в двумерный массив (для тестов скорости).
        /// </summary>
        /// <param name="videos"></param>
        /// <returns>Возвращает двумерный массив видеофайлов.</returns>
        public static VideoFileReader[][] ConvertToArray(ref List<List<VideoFileReader>> videos)
        {
            VideoFileReader[][] VideosArray = new VideoFileReader[videos.Count][];

            for (int i = 0; i < videos.Count; i++)
            {
                VideosArray[i] = videos[i].ToArray();
            }

            return VideosArray;
        }
        /// <summary>
        /// Извлекает название и расширение файла.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>Возвращает массив названий и расширений файлов.</returns>
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
