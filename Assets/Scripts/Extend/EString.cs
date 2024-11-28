using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Extend
{
    public static class EString
    {
        public static string FillStr(this string str, string fill = "0", int len = 2, bool pre = true)
        {
            while (str.Length < len)
                str = !pre ? str + fill : fill + str;
            return str;
        }

        public static T DeserializeTo<T>(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static JObject DeserializeToJObj(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<JObject>(str);
            }
            catch (Exception ex)
            {
                return (JObject)null;
            }
        }

        public static byte[] ToJsonBytes(this object obj)
        {
            return JsonConvert.SerializeObject(obj).ToBytes();
        }

        public static byte[] HexToBytes(this string hex)
        {
            hex = hex.Replace(" ", "");
            if ((uint)(hex.Length % 2) > 0U)
                hex += " ";
            int length = hex.Length / 2;
            byte[] numArray = new byte[length];
            for (int index = 0; index < length; ++index)
                numArray[index] = Convert.ToByte(hex.Substring(index * 2, 2), 16);
            return numArray;
        }

        public static byte[] ToBytes(this string utf8)
        {
            return Encoding.UTF8.GetBytes(utf8);
        }

        public static string GetMD5(this string path)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream fileStream = File.OpenRead(path))
                {
                    byte[] hash = md5.ComputeHash((Stream)fileStream);
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (byte num in hash)
                        stringBuilder.Append(num.ToString("X2"));
                    return stringBuilder.ToString();
                }
            }
        }

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
    }
}

