using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class CryptDES : MonoBehaviour
{
    #region  �ַ������ܽ���

    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    /// <summary>
    /// DES�����ַ���
    /// </summary>
    /// <param name="encryptString">�����ܵ��ַ���</param>
    /// <param name="key">������Կ,Ҫ��Ϊ8λ</param>
    /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
    public static string EncryptDES(string encryptString, string key = "45131929")
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch
        {
            Debug.LogError("StringEncrypt/EncryptDES()/ Encrypt error!");
            return encryptString;
        }
    }

    /// <summary>
    /// DES�����ַ���
    /// </summary>
    /// <param name="decryptString">�����ܵ��ַ���</param>
    /// <param name="key">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
    /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
    public static string DecryptDES(string decryptString, string key = "45131929")
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch
        {
            Debug.LogError("StringEncrypt/DecryptDES()/ Decrypt error!");
            return decryptString;
        }
    }

    #endregion
}
