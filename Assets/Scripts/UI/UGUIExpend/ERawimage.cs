using Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ERawimage : RawImage, IPointerDownHandler
{

    [SerializeField]
    public Camera camPreview; // 预览映射相机

    public Transform clickObj;

    private void Update()
    {
        var pos = ((Vector2)Input.mousePosition - (Vector2)transform.GetComponent<RectTransform>().position) / transform.GetComponent<RectTransform>().lossyScale - transform.GetComponent<RectTransform>().rect.position;
        var rate = pos / transform.GetComponent<RectTransform>().rect.size;
        var ray = camPreview.ViewportPointToRay(rate);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform != clickObj)
            {
                if (clickObj != null)
                {
                    var compmouse = clickObj.GetComponent<CompMouse>();
                    if (compmouse)
                    {
                        compmouse.onExit?.Invoke(compmouse);
                    }
                }
                var compmouse1 = raycastHit.transform.GetComponent<CompMouse>();
                if (compmouse1)
                {
                    compmouse1.onEnter?.Invoke(compmouse1);
                }
            }
            clickObj = raycastHit.transform;
        }
        else if (raycastHit.transform == null && clickObj != null)
        {
            var compmouse = clickObj.GetComponent<CompMouse>();
            if (compmouse)
            {
                compmouse.onExit?.Invoke(compmouse);
            }
            clickObj = null;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (GetRawImageObj(eventData, rectTransform, camPreview) == null)
            return;
        clickObj = GetRawImageObj(eventData, rectTransform, camPreview)?.transform;
        var compmouse = clickObj.GetComponent<CompMouse>();
        if (compmouse)
        {
            compmouse.onDown?.Invoke(compmouse);
        }
    }

    #region UI绑定相机
    /// <summary>
    /// 通过点击RawImage中映射的RenderTexture画面，对应的相机发射射线，得到物体
    /// </summary>
    /// <param name="data">rawimage点击的数据</param>
    /// <param name="rawImgRectTransform">rawimage的recttransfotm</param>
    /// <param name="previewCamera">生成rendertexture中画面的相机</param>
    /// <returns>返回射线碰撞到的物体</returns>
    private GameObject GetRawImageObj(PointerEventData data, RectTransform rawImgRectTransform, Camera previewCamera)
    {
        GameObject obj = null;
        var pos = (data.position - (Vector2)rawImgRectTransform.position) / rawImgRectTransform.lossyScale - rawImgRectTransform.rect.position;
        var rate = pos / rawImgRectTransform.rect.size;
        var ray = previewCamera.ViewportPointToRay(rate);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            obj = raycastHit.transform.gameObject;
        }
        return obj;
    }
    #endregion

}

#if UNITY_EDITOR
[CustomEditor(typeof(ERawimage))]
[CanEditMultipleObjects]
public class ERawimageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("camPreview"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("clickObj"));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Texture"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Material"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastTarget"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_RaycastPadding"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Maskable"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UVRect"));
        if (GUILayout.Button("SetNatoveSize"))
        {
            RawImage raw = (RawImage)target;
            raw.SetNativeSize();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif