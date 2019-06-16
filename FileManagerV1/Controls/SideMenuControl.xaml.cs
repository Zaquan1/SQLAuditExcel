using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;

namespace FileManagerV1
{
    /// <summary>
    /// Interaction logic for SideMenuControl.xaml
    /// </summary>
    public partial class SideMenuControl : UserControl
    {

        #region Constructor

        /// <summary>
        /// Defalut Constructor
        /// </summary>
        public SideMenuControl()
        {
            InitializeComponent();
            
        }
        #endregion

        #region On Loaded
        /// <summary>
        /// When the application first open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive
                };
                
                item.Items.Add(null);

                item.Expanded += Folder_Expended;

                FolderView.Items.Add(item);
            }
        }

        private void Folder_Expended(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();

            var fullPath = (string)item.Tag;

            var directories = new List<string>();
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
                
            }
            catch { }
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expended;

                item.Items.Add(subItem);
            });
        }
        #endregion

        /// <summary>
        /// Find the file or foler name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);

        }
    }
}
