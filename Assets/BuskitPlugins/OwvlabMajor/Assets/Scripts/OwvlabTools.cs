/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： OwvlabTools
* 创建日期：2021-11-18 11:28:07
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{
    /// <summary>
    /// 提供数据格式转义等功能
    /// </summary>
	public class OwvlabTools 
	{

		/// <summary>
		/// 音频转字节 
		/// </summary>
		/// <param name="clip">音频文件</param>
		/// <returns></returns>
		public static byte[] ToBytes(AudioClip clip)
		{
			float[] samples = new float[clip.samples];

			clip.GetData(samples, 0);

			short[] intData = new short[samples.Length];

			byte[] bytesData = new byte[samples.Length * 2];

			int rescaleFactor = 32767;

			for (int i = 0; i < samples.Length; i++)
			{
				intData[i] = (short)(samples[i] * rescaleFactor);
				byte[] byteArr = new byte[2];
				byteArr = BitConverter.GetBytes(intData[i]);
				byteArr.CopyTo(bytesData, i * 2);
			}
			return bytesData;
		}

		/// <summary>
		/// 2d纹理砖字节
		/// </summary>
		/// <param name="texture2D">2d纹理</param>
		/// <returns></returns>
		public static byte[] ToPNGBytes(Texture2D texture2D)
		{
			return texture2D.EncodeToPNG();
		}
	}
}

