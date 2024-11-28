/* 
*类名:  MTMP_Dropdown
*作者:  NPCsama
*时间:  2024-05-27 14:29:18
*描述:  文件描述
*版本:  1.0
*unity版本:  2022.3.16f1
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class ETMP_Dropdown : TMP_Dropdown
{
    [Tooltip("显示的背景")]
    public Transform Background;
    protected override GameObject CreateDropdownList(GameObject template)
    {
        Background.gameObject.SetActive(true);
        return base.CreateDropdownList(template);
    }

    protected override void DestroyDropdownList(GameObject dropdownList)
    {
        base.DestroyDropdownList(dropdownList);
        Background.gameObject.SetActive(false);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ETMP_Dropdown))]
public class ETMP_DropdownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Interactable"));
        EditorGUILayout.Space();
        // 获取Transition属性
        SerializedProperty transitionProperty = serializedObject.FindProperty("m_Transition");

        // 显示Transition属性
        EditorGUILayout.PropertyField(transitionProperty);
        // 根据Transition的值显示对应的UI元素
        switch ((Selectable.Transition)transitionProperty.enumValueIndex)
        {
            case Selectable.Transition.None:
                // None选项，不需要显示额外UI元素
                break;
            case Selectable.Transition.ColorTint:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_TargetGraphic"));
                // ColorTint选项，显示ColorTint相关的UI元素
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Colors"));
                break;
            case Selectable.Transition.SpriteSwap:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_TargetGraphic"));
                // SpriteSwap选项，显示SpriteSwap相关的UI元素
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SpriteState"));
                break;
            case Selectable.Transition.Animation:
                // Animation选项，显示Animation相关的UI元素
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_AnimationTriggers"));
                break;
        }
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Navigation"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Template"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_CaptionText"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_CaptionImage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Placeholder"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ItemText"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ItemImage"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Value"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_AlphaFadeSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Options"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_OnValueChanged"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Background"));


        if (GUILayout.Button("绑定原Dropdown"))
        {
            BandingDropdown();
        }
        serializedObject.ApplyModifiedProperties();
    }


    private void BandingDropdown()
    {
        ETMP_Dropdown dd = (ETMP_Dropdown)target;
        dd.targetGraphic = dd.transform.GetComponent<Image>();
        dd.template = dd.transform.Find("Template").GetComponent<RectTransform>();
        dd.captionText = dd.transform.Find("Label").GetComponent<TextMeshProUGUI>();
        dd.itemText = dd.template.GetComponentInChildren<TextMeshProUGUI>(true);
    }
}
#endif