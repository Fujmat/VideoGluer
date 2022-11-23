using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGluer
{
    /// <summary>
    /// Хранит название папки и имена файлов в этой папке.
    /// </summary>
    class PathVideo
    {
        /// <summary>
        /// Полное имя файла с указанием пути и расширения.
        /// </summary>
        public string[] FullName { get; set; }
        /// <summary>
        /// Название файла с расширением.
        /// </summary>
        public string[] Name { get; set; }
        /// <summary>
        /// Имя папки.
        /// </summary>
        public string FolderName { get; set; }
    }
}
