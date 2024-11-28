/* 
*����:  MToggle
*����:  NPCsama
*ʱ��:  2024-05-14 15:35:54
*����:  �ļ�����
*�汾:  1.0
*unity�汾:  2022.3.16f1
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

    [ContextMenu("��ԭToggle", false)]
    public void BandingToggle()
    {
        targetGraphic = transform.Find("Background").GetComponent<Image>();
        graphic = transform.Find("Background").Find("Checkmark").GetComponent<Image>();
    }


    [ContextMenu("��TMP�滻Text", false)]
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

    [ContextMenu("��Toogle�������滻Label�ı�����")]
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

        // ����ʵ�ʶ������л�����
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
        // ��ȡTransition����
        SerializedProperty transitionProperty = serializedObject.FindProperty("m_Transition");

        // ��ʾTransition����
        EditorGUILayout.PropertyField(transitionProperty);

        // ����Transition��ֵ��ʾ��Ӧ��UIԪ��
        switch ((Selectable.Transition)transitionProperty.enumValueIndex)
        {
            case Selectable.Transition.None:
                // Noneѡ�����Ҫ��ʾ����UIԪ��
                break;
            case Selectable.Transition.ColorTint:
                // ColorTintѡ���ʾColorTint��ص�UIԪ��
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Colors"));
                break;
            case Selectable.Transition.SpriteSwap:
                // SpriteSwapѡ���ʾSpriteSwap��ص�UIԪ��
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SpriteState"));
                break;
            case Selectable.Transition.Animation:
                // Animationѡ���ʾAnimation��ص�UIԪ��
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_AnimationTriggers"));
                break;
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_TargetGraphic"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("graphic"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Group"));

        // �����ı����ʵ�ʶ���
        serializedObject.ApplyModifiedProperties();

    }
}
#endif