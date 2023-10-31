/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ReplaceMaterialOrText
* 创建日期：2023-07-21 09:10:47
* 作者名称：cy
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 替换选中物体下所有材质、字体拓展
/// </summary>
public class ReplaceMaterialOrText : EditorWindow
{
    private Material newMaterial;
    private Material newUIMaterial;
    private Font newFont;


    [MenuItem("编辑器工具/替换材质、字体")]
    private static void ShowWindow()
    {
        GetWindow<ReplaceMaterialOrText>("替换材质或替换字体");
    }

    private void OnGUI()
    {
        //窗口设置
        GUILayout.Label("设置材质:", EditorStyles.boldLabel);
        newMaterial = EditorGUILayout.ObjectField(newMaterial, typeof(Material), true) as Material;
        if (GUILayout.Button("替换材质"))
        {
            ReplaceMaterials();
        }

        GUILayout.Label("设置UI材质", EditorStyles.boldLabel);
        newUIMaterial = (Material)EditorGUILayout.ObjectField(newUIMaterial, typeof(Material), false);
        if (GUILayout.Button("替换UI材质"))
        {
            ReplaceUIMaterials();
        }

        GUILayout.Label("设置字体", EditorStyles.boldLabel);
        newFont = (Font)EditorGUILayout.ObjectField(newFont, typeof(Font), false);
        if (GUILayout.Button("替换UI字体"))
        {
            ReplaceFont();
        }
    }

    /// <summary>
    /// 替换当前物体及子物体材质
    /// </summary>
    private void ReplaceMaterials()
    {
        if (newMaterial != null)
        {
            GameObject[] selection = Selection.gameObjects;

            foreach (GameObject selectedObject in selection)
            {
                ReplaceMaterialInChildren(selectedObject.transform);
            }

            Debug.Log("材质替换完成");
        }
        else Debug.LogError("请先填入替换材质");
    }
    
    
    private void ReplaceMaterialInChildren(Transform parent)
    {
        if (parent == null)
            return;

        //获取渲染器组件
        Renderer renderer = parent.GetComponent<Renderer>();

        if (renderer != null)
        {
            Undo.RecordObject(renderer, "Material Replacement");
            //将renderer中的所有材质mat全部替换成该材质
            Material[] sharedMaterials = renderer.sharedMaterials;
            for (int i = 0; i < sharedMaterials.Length; i++)
            {
                sharedMaterials[i] = newMaterial;
            }
            renderer.sharedMaterials = sharedMaterials;
        }
        //替换该物体下的子物体
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform childTransform = parent.GetChild(i);
            ReplaceMaterialInChildren(childTransform);
        }
    }

    /// <summary>
    /// 替换ui材质
    /// </summary>
    private void ReplaceUIMaterials()
    {
        //如果没有放置材质则把所有ui材质替换为null
        //if (newMaterial == null)
        //{
        //    Debug.LogError("请放置材质");
        //    return;
        //}
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var obj in selectedObjects)
        {
            ReplaceUIMaterialRecursive(obj.transform);
        }

        Debug.Log("UI材质替换完成");
    }

    private void ReplaceUIMaterialRecursive(Transform parent)
    {
        // 根据UI组件以替换其材质
        Image image = parent.GetComponent<Image>();
        if (image != null)
        {
            image.material = newUIMaterial;
        }

        RawImage rawImage = parent.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.material = newUIMaterial;
        }

        Text text = parent.GetComponent<Text>();
        if (text != null)
        {
            text.material = newUIMaterial;
        }


        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            ReplaceUIMaterialRecursive(child);
        }
    }

    /// <summary>
    /// 替换UI字体
    /// </summary>
    private void ReplaceFont()
    {
        if (newFont == null)
        {
            Debug.LogError("请放置字体");
            return;
        }
        GameObject[] selectedObjects = Selection.gameObjects;

        foreach (var obj in selectedObjects)
        {
            ReplaceFontRecursive(obj.transform);
        }

        Debug.Log("字体替换完成");
    }
    private void ReplaceFontRecursive(Transform parent)
    {
        Text text = parent.GetComponent<Text>();
        if (text != null)
        {
            text.font = newFont;
        }

        // UI组件以替换其材质

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            ReplaceFontRecursive(child);
        }
    }
}



