/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： EGameObject
* 创建日期：2024-11-26 16:51:43
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
	public static class EGameObject
	{
        /// <summary>
        /// 复制组件脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objStart">复制的目标</param>
        /// <param name="objEnd">复制到的物体</param>
        public static void CopyCompTo<T>(this GameObject objStart, GameObject objEnd) where T : Component
        {
            T comp = objStart.GetComponent<T>();
            System.Type type = comp.GetType();
            Component copy = objEnd.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(comp));
            }
        }
    }
}

