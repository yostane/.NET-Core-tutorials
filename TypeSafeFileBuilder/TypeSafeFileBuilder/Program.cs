using System;
using System.IO;

namespace TypeSafeFileBuilder
{
    /// <summary>
    /// This program demonstrated the use of a FileBuilder that is TypeSafe. 
    /// See <see cref="FileBuilder"/>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //create a folder
            FileBuilder.CreateFolder("test", mainFileBuilder =>
            {
                mainFileBuilder.CreateSubFolder("1");
                mainFileBuilder.CreateSubFolder("2", fileBuilder =>
                {
                    fileBuilder.CreateFile("hello.txt", (sw) =>
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
                    fb.CreateFile("hello.txt", sw =>
                    {
                        sw.WriteLine("hello");
                    });
                    fb.CreateSubFolder("21");
                });
                mainFileBuilder.CreateSubFolder("3");
            });
        }
    }
}
