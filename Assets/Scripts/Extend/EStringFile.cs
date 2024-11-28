/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： EStringFile
* 创建日期：2024-11-26 16:56:42
* 作者名称：cy
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

namespace Extend
{
    /// <summary>
    /// 
    /// </summary>
	public static class EStringFile
	{

        public static void Del(this string path)
        {
            if (!File.Exists(path))
                return;
            File.Delete(path);
        }

        public static bool Exists(this string path)
        {
            return File.Exists(path);
        }

        public static void Save(this string path, byte[] bytes, FileMode mode = FileMode.CreateNew)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fileStream = File.Open(path, mode))
                fileStream.Write(bytes, 0, bytes.Length);
        }

        public static void Save(this string path, string str, FileMode mode = FileMode.CreateNew)
        {
            path.Save(Encoding.UTF8.GetBytes(str), mode);
        }

        public static byte[] Load(this string path)
        {
            if (!new FileInfo(path).Exists)
                return (byte[])null;
            using (FileStream fileStream = File.OpenRead(path))
            {
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static void FilterFiles(
          this string path,
          Action<FileInfo> fileFilter,
          string searchPattern = null,
          Func<DirectoryInfo, bool> dirFilter = null)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                return;
            foreach (FileInfo file in directoryInfo.GetFiles(searchPattern))
                fileFilter(file);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                if (dirFilter == null || dirFilter.Invoke(directory))
                    directory.FullName.FilterFiles(fileFilter, searchPattern, dirFilter);
            }
        }
    }
}

