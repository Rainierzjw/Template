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
        TestSystem,
    }

    public enum SceneName
    {
        App,
        ShiYanShi,
        GongChang
    }
}

