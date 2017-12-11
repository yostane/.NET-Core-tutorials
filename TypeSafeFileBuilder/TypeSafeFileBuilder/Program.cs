using System;
using System.IO;

namespace TypeSafeFileBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            FileBuilder.CreateFolder("test", mainFileBuilder =>
            {
                mainFileBuilder.CreateSubFolder("1");
                mainFileBuilder.CreateSubFolder("2", fb =>
                {
                    fb.CreateFile("hello.txt", (sw) =>
                    {
                        sw.WriteLine("hello");
                    });
                });
            });

            FileBuilder.CreateFolder("test2", mainFileBuilder =>
            {
                mainFileBuilder.CreateSubFolder("1");
                mainFileBuilder.CreateSubFolder("2", fb =>
                {
                    fb.CreateFile("hello.txt", (sw) =>
                    {
                        sw.WriteLine("hello");
                    });
                    fb.CreateSubFolder("21");
                });
                mainFileBuilder.CreateSubFolder("3");
            });
        }
    }

        public class FileBuilder
        {

            public static void CreateFolder(string path, Action<FileBuilder> action = null)
            {
                var fileBuilder = new FileBuilder(new DirectoryInfo(path));
                if (action != null)
                {
                    action(fileBuilder);
                }
            }
            private DirectoryInfo directoryInfo;

            public FileBuilder(DirectoryInfo directoryInfo)
            {
                this.directoryInfo = directoryInfo;
                directoryInfo.Create();
            }

            public void CreateFile(string fileName, Action<StreamWriter> action)
            {
                var path = Path.Combine(directoryInfo.FullName, fileName);
                using (StreamWriter sw = File.CreateText(path))
                {
                    action(sw);
                }
            }

            public void CreateSubFolder(string folderName, Action<FileBuilder> action = null)
            {
                var path = Path.Combine(directoryInfo.FullName, folderName);
                var f = new FileBuilder(new DirectoryInfo(path));
                if (action != null)
                {
                    action(f);
                }

            }
        }


}
