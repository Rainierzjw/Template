
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ObjectIdentityEditor
* 创建日期：2019-03-20 13:40:55
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 自定义属性绘制
    /// </summary>
    [CustomPropertyDrawer(typeof(ObjectIdentity))]
	public class ObjectIdentityEditor : PropertyDrawer
	{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);
            {
               
                GUI.Box(position, string.Empty, EditorStyles.helpBox);

                Rect idRect = new Rect(position.x, position.y, position.width, position.height / 2);
                GUI.enabled = false;
                idRect = EditorGUI.PrefixLabel(idRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("UniqueID"));
              
                EditorGUI.PropertyField(idRect, property.FindPropertyRelative("id"), GUIContent.none);

                Rect prefabIdRect = new Rect(position.x, position.y+18, position.width, position.height / 2);
                prefabIdRect = EditorGUI.PrefixLabel(prefabIdRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("PrefabID"));

                EditorGUI.PropertyField(prefabIdRect, property.FindPropertyRelative("prefabId"), GUIContent.none);
                GUI.enabled = true;
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label)*2;
        }
    }
}

