/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Base64Helper
* 创建日期：2022-04-01 10:38:28
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Text;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
    class Base64Helper
    {
	    /// <summary>
	    /// Base64加密，采用utf8编码方式加密
	    /// </summary>
	    /// <param name="source">待加密的明文</param>
	    /// <returns>加密后的字符串</returns>
	    public static string Base64Encode(string source)
	    {
		    return Base64Encode(Encoding.UTF8, source);
	    }

	    /// <summary>
	    /// Base64加密
	    /// </summary>
	    /// <param name="encodeType">加密采用的编码方式</param>
	    /// <param name="source">待加密的明文</param>
	    /// <returns></returns>
	    public static string Base64Encode(Encoding encodeType, string source)
	    {
		    string encode = string.Empty;
		    byte[] bytes = encodeType.GetBytes(source);
		    try
		    {
			    encode = Convert.ToBase64String(bytes);
		    }
		    catch
		    {
			    encode = source;
		    }
		    return encode;
	    }

	    /// <summary>
	    /// Base64解密，采用utf8编码方式解密
	    /// </summary>
	    /// <param name="result">待解密的密文</param>
	    /// <returns>解密后的字符串</returns>
	    public static string Base64Decode(string result)
	    {
		    return Base64Decode(Encoding.UTF8, result);
	    }

	    /// <summary>
	    /// Base64解密
	    /// </summary>
	    /// <param name="encodeType">解密采用的编码方式，注意和加密时采用的方式一致</param>
	    /// <param name="result">待解密的密文</param>
	    /// <returns>解密后的字符串</returns>
	    public static string Base64Decode(Encoding encodeType, string result)
	    {
		    string decode = string.Empty;
		    byte[] bytes = Convert.FromBase64String(result);
		    try
		    {
			    decode = encodeType.GetString(bytes);
		    }
		    catch
		    {
			    decode = result;
		    }
		    return decode;
	    }
    }
}

