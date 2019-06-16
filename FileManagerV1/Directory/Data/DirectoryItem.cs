/// <summary>
/// Information about a directory item such as drive, a file or a folder
/// </summary>
namespace FileManagerV1
{
    public class DirectoryItem
    {

        /// <summary>
        /// the type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

    }
}
