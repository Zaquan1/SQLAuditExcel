using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// A helper class to query information about directories
/// </summary>
namespace FileManagerV1
{
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get all logical drive in computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath">the full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents (string fullPath)
        {
            var items = new List<DirectoryItem>();

            //get folder
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            //get files
            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            return items;
        }

        #region Helpers
        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">the full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //return empty if no path
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            
            //replace slashes to back slashes
            var normalizedPath = path.Replace('/', '\\');

            //find the last backslash of the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //return the path itself if there are no backslash
            if (lastIndex <= 0)
                return path;

            //return the name of the last back slash
            return path.Substring(lastIndex + 1);

        }
        #endregion
    }
}
