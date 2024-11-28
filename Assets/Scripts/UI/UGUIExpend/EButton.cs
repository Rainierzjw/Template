/* 
*类名:  MButton
*作者:  NPCsama
*时间:  2024-05-17 10:51:06
*描述:  文件描述
*版本:  1.0
*unity版本:  2022.3.16f1
*/

using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EButton : Button
{
    public bool canChangeTextColor = false;
    [SerializeField, Tooltip("按下按钮时的颜色")]
    public Color pressedColor = Color.white;
    /// <summary>
    /// 按钮原来的颜色
    /// </summary>
    private Color originalColor;
    private Text oldText;
    private TMPro.TextMeshProUGUI newText;

    protected override void Start()
    {
        base.Start();
        if (GetComponentInChildren<Text>())
        {
            oldText = GetComponentInChildren<Text>();
            originalColor = oldText.color;
        }
        else if (GetComponentInChildren<TMPro.TextMeshProUGUI>())
        {
            newText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
            originalColor = newText.color;
        }

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (canChangeTextColor)
        {
            if (oldText != null) oldText.color = pressedColor;
            if (newText != null) newText.color = pressedColor;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (canChangeTextColor)
        {
            if (oldText != null) oldText.color = originalColor;
            if (newText != null) newText.color = originalColor;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EButton))]
[CanEditMultipleObjects]
public class EButtonEditor : Editor
{

    bool openFuncButtons = false;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        // 加载实际对象到序列化对象
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Interactable"));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("canChangeTextColor"));
        EButton mButton = target as EButton;
        if (mButton.canChangeTextColor)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("pressedColor"));
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

        // 将更改保存回实际对象
        serializedObject.ApplyModifiedProperties();

        //// 添加一行标题头，文字使用粗体
        //EditorGUILayout.LabelField("功能按钮", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        // 添加一个可以收起和展开的列表
        openFuncButtons = EditorGUILayout.Foldout(openFuncButtons, "功能按钮列表");

        if (openFuncButtons)
        {
            if (GUILayout.Button("应用名称至文本"))
            {
                if (targets.Length == 1)
                {
                    Button button = (Button)targets[0];
                    ApplyNameToText(button.transform);
                }
                else
                {
                    foreach (var item in targets)
                    {
                        Button button = (Button)item;
                        ApplyNameToText(button.transform);
                    }
                }
            }
        }

    }


    #region Func
    private void ApplyNameToText(Transform trans)
    {
        if (trans.Find("Text (TMP)"))
        {
            trans.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = trans.name;
            return;
        }
        else if (trans.Find("Text (Legacy)"))
        {
            trans.Find("Text (Legacy)").GetComponent<Text>().text = trans.name;
            return;
        }
        else if (trans.Find("Text"))
        {
            trans.Find("Text").GetComponent<Text>().text = trans.name;
            return;
        }
        else if (trans.GetChild(0))
        {
            var text = trans.GetChild(0).GetComponent<Text>();
            var tmp = trans.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            if (text != null)
            {
                text.text = trans.name;
                return;
            }
            if (tmp != null)
            {
                tmp.text = trans.name;
                return;
            }
        }
    }

    #endregion
}
#endif
