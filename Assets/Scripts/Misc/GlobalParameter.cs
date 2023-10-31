/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： GlobalParameter
* 创建日期：2022-04-07 14:04:11
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
	public class GlobalParameter : MonoBehaviour
    {
        public static bool openUI;

        public static SceneName EnterScene = SceneName.App;



        public static string GetUIPrefabRoot()
        {
            return "UIPrefab";
        }

        public static string GetUIPrefabPath(string name)
        {
            return $"{GetUIPrefabRoot()}/{name}";
        }

        public static string GetEntityPrefabRoot()
        {
            return "EntityPrefab";
        }

        public static string GetEntityPrefabPath(string name)
        {
            return $"{GetEntityPrefabRoot()}/{name}";
        }

        public static string GetImageRoot()
        {
            return "Image";
        }

        public static string GetImagePath(string name)
        {
            return $"{GetImageRoot()}/{name}";
        }
    }

    public enum SystemType
    {
        StepSystem,
        SceneSystem,
        UISystem,
        CameraPathSystem,
    }

    public enum SceneName
    {
        App,
        ShiYanShi,
        GongChang
    }

    public enum StepIndex
    {
        s0_0 = 0000,
        s0_1 = 0001,
        s0_2 = 0002,
        s0_3 = 0003,
        s0_4 = 0004,
        s1_1 = 1001,
        s1_2 = 1002,
        s1_3 = 1003,
        s1_4 = 1004,
        s1_5 = 1005,
        s1_6 = 1006,
        s1_7 = 1007,
        s1_8 = 1008,
        s1_9 = 1009,
        s1_10 = 1010,
        s1_11 = 1011,
        s1_12 = 1012,
        s1_13 = 1013,
        s1_14 = 1014,
        s1_15 = 1015,
        s1_16 = 1016,
        s1_17 = 1017,
        s1_18 = 1018,
        s1_19 = 1019,
        s1_20 = 1020,
        s1_21 = 1021,
        //s1_22 = 1022,
        s2_1 = 2001,
        s2_2 = 2002,
        s2_3 = 2003,
        s2_4 = 2004,
        s2_5 = 2005,
        s2_6 = 2006,
        s2_7 = 2007,
        s2_8 = 2008,
        s2_9 = 2009,
        s2_10 = 2010,
        s2_11 = 2011,
        s2_12 = 2012,
        s2_13 = 2013,
        s2_14 = 2014,
        s2_15 = 2015,
        s2_16 = 2016,
        s2_17 = 2017,
        s2_18 = 2018,
        s2_19 = 2019,
        s2_20 = 2020,
        s2_21 = 2021,
        s2_22 = 2022,
        s3_0 = 3000,
        s3_1 = 3001,
        s3_2 = 3002,
        s3_3 = 3003,
        s3_4 = 3004,
        s3_5 = 3005,
        s3_6 = 3006,
        s3_7 = 3007,
        s3_8 = 3008,
        s3_9 = 3009,
        s3_10 = 3010,
        s3_11 = 3011,
        s3_12 = 3012,
        s3_13 = 3013,
        s3_14 = 3014,
        s3_15 = 3015,
        s3_16 = 3016,
        s3_17 = 3017,
        s3_18 = 3018,
        s4_0 = 4000,
        s4_1 = 4001,
        s4_2 = 4002,
        s4_3 = 4003,
    }
}

