using ClientMonitor.Application.Abstractions;

using System;
using System.IO;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// /Проверка размера файлов
    /// </summary>
    public class CheckFileHandler : ICheckFileHandler
    {
        /// <summary>
        /// Подключение библиотек
        /// </summary>
        public CheckFileHandler(){}

        public void CheckFileHandle()
        {
            string[] allFoundFiles = Directory.GetFiles("C:\\Users\\Big Lolipop\\Desktop\\ЗаписиКамер", "", SearchOption.AllDirectories);

            foreach (string file in allFoundFiles)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Length < 3072)
                {
                    //TimeSpan dt = DateTime.Now-fi.LastWriteTime;
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
            }
        }
    }
}
