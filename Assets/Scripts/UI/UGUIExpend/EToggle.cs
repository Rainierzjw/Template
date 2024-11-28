/* 
*类名:  MToggle
*作者:  NPCsama
*时间:  2024-05-14 15:35:54
*描述:  文件描述
*版本:  1.0
*unity版本:  2022.3.16f1
*/
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EToggle : Toggle
{
    public bool canChangeTextColor = false;
    public Color changedColor = Color.white;
    private Color originalColor;

    protected override void Awake()
    {
        base.Awake();
        Transform label = transform.Find("Label");
        originalColor = label.GetComponent<Text>() ? label.GetComponent<Text>().color : label.GetComponent<TMPro.TextMeshProUGUI>().color;
        onValueChanged.AddListener(OnValueChanged);
    }
    private void OnValueChanged(bool b)
    {
        if (!canChangeTextColor) return;
        Transform label = transform.Find("Label");
        if (label.GetComponent<Text>()) label.GetComponent<Text>().color = b ? changedColor : originalColor;
        if (label.GetComponent<TMPro.TextMeshProUGUI>()) label.GetComponent<TMPro.TextMeshProUGUI>().color = b ? changedColor : originalColor;
        targetGraphic.color = b ? new Color(1, 1, 1, 0) : Color.white;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (group != null && enabled == true)
        {
            group.RegisterToggle(this);
        }
    }
    protected override void OnDestroy()
    {
        if (group != null)
        {
            group.UnregisterToggle(this);
        }
        base.OnDestroy();
    }


    #region ContextMenu

    [ContextMenu("绑定原Toggle", false)]
    public void BandingToggle()
    {
        targetGraphic = transform.Find("Background").GetComponent<Image>();
        graphic = transform.Find("Background").Find("Checkmark").GetComponent<Image>();
    }


    [ContextMenu("用TMP替换Text", false)]
    public void ReplaceTextWithTMP()
    {
        var label = transform.Find("Label");
        if (label)
        {
            if (!label.GetComponent<Text>()) return;
            DestroyImmediate(label.GetComponent<Text>());
            label.gameObject.AddComponent<TMPro.TextMeshProUGUI>();
        }
    }

    [ContextMenu("用Toogle的名字替换Label文本内容")]
    public void ReplaceTextWithName()
    {
        if (transform.Find("Label").GetComponent<Text>())
        {
            transform.Find("Label").GetComponent<Text>().text = name;
            return;
        }
        else if (transform.Find("Label").GetComponent<TMPro.TextMeshProUGUI>())
        {
            transform.Find("Label").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        }
    }

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(EToggle))]
[CanEditMultipleObjects]
public class EToggleEditor : Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        // 加载实际对象到序列化对象
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_IsOn"));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Interactable"));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("canChangeTextColor"));
        EToggle mToggle = target as EToggle;
        if (mToggle.canChangeTextColor)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("changedColor"));
        }

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
                // ColorTint选项，显示ColorTint相关的UI元素
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Colors"));
                break;
            case Selectable.Transition.SpriteSwap:
                // SpriteSwap选项，显示SpriteSwap相关的UI元素
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SpriteState"));
                break;
            case Selectable.Transition.Animation:
                // Animation选项，显示Animation相关的UI元素
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_AnimationTriggers"));
                break;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_TargetGraphic"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("graphic"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Group"));

        // 将更改保存回实际对象
        serializedObject.ApplyModifiedProperties();

    }
}
#endif