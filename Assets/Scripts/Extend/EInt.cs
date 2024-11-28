/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： EInt
* 创建日期：2024-11-26 17:00:37
* 作者名称：cy
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Extend
{
    /// <summary>
    /// 
    /// </summary>
	public static class EInt
	{
        public static string SToHMS(this int s)
        {
            string temp = "";
            int hour = 0;
            int min = 0;
            int sce = 0;
            hour = s / 3600;
            min = (s - (hour * 3600)) / 60;
            sce = s % 60;
            temp = hour.ToString("00") + ":" + min.ToString("00") + ":" + sce.ToString("00");
            return temp;
        }
    }
}

