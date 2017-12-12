using System;
using System.IO;

namespace TypeSafeFileBuilder
{
    /// <summary>
    /// A C# implemetation of FileBuilder that is TypeSafe and uses lambdas. 
    /// It allows to create a folder/file tree on the file system by imbricating lambda expressions.
    /// </summary>
    public class FileBuilder
    {
        /// <summary>
        /// The main entry point for creating an instance. It creates a folder at a given <c>path</c> 
        /// and allows to add files/folders to it using the <c>action</c> parameter
        /// </summary>
        /// <param name="path">Path. The path of the folder to create.</param>
        /// <param name="action">Action. The <c>FileBuilder</c> instance associated with the folder</param>
        public static void CreateFolder(string path, Action<FileBuilder> action = null)
        {
            var fileBuilder = new FileBuilder(new DirectoryInfo(path));
            action?.Invoke(fileBuilder);
        }
        private DirectoryInfo directoryInfo;

        private FileBuilder(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
            directoryInfo.Create();
        }

        /// <summary>
        /// Creates a file inside the folder that is associated with the <c>FileBuilder</c> and allows to write content.
        /// </summary>
        /// <param name="fileName">FileName. The name of the file</param>
        /// <param name="action">Action. The <c>StreamWriter</c> instance that allows to write on the file. You do not need to dispose it.</param>
        public void CreateFile(string fileName, Action<StreamWriter> action)
        {
            var path = Path.Combine(directoryInfo.FullName, fileName);
            using (StreamWriter sw = File.CreateText(path))
            {
                action(sw);
            }
        }

        /// <summary>
        /// Creates the sub folder.
        /// </summary>
        /// <param name="folderName">Folder name.</param>
        /// <param name="action">Action.</param>
        public void CreateSubFolder(string folderName, Action<FileBuilder> action = null)
        {
            var path = Path.Combine(directoryInfo.FullName, folderName);
            var f = new FileBuilder(new DirectoryInfo(path));
            action?.Invoke(f);
        }
    }
}
