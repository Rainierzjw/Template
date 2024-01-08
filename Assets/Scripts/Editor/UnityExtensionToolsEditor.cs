/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：UnityExtensionToolsEditor
* 创建日期：2023-08-21 08:58:47
* 作者名称：韦伟
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：unity拓展工具
******************************************************************************/

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EditorTools
{
    #region -----------------------------------------------------1.模型调整-----------------------------------------------------

    #region 1.所选的模型整体修改材质
    public class Model_ModifyMaterial : EditorWindow
    {
        Material material;
        Model_ModifyMaterial()
        {
            this.titleContent = new GUIContent("修改材质");
        }
        [MenuItem("Unity工具/1.模型调整/1.所选的模型整体修改材质")]
        static void ShowWindow()
        {
            GetWindow(typeof(Model_ModifyMaterial));
        }
        private void OnGUI()
        {
            material = (Material)EditorGUILayout.ObjectField("该模型要替换成的材质", material, typeof(Material), true);
            if (GUILayout.Button("替换材质"))
            {
                Transform[] parts = Selection.transforms;
                for (int i = 0; i < parts.Length; i++)
                {
                    Renderer[] renderers = parts[i].GetComponentsInChildren<Renderer>();
                    for (int j = 0; j < renderers.Length; j++)
                    {
                        Material[] materials = renderers[j].sharedMaterials;
                        for (int k = 0; k < materials.Length; k++)
                        {
                            materials[k] = this.material;
                        }
                        renderers[j].sharedMaterials = materials;
                    }
                }
            }
        }
    }
    #endregion

    public class Model_Adjustment : Editor
    {
        #region 2.根据模型大小添加Box碰撞体
        [MenuItem("Unity工具/1.模型调整/2.根据模型形状添加Box碰撞体")]
        private static void AddBoxCollider()
        {
            foreach (var item in Selection.transforms)
            {
                Vector3 oriEuler = item.eulerAngles;
                item.eulerAngles = Vector3.zero;
                BoxCollider col = Undo.AddComponent<BoxCollider>(item.gameObject);
                Vector3 pos = item.GetSelectedRendererBounds().center - item.position;
                Vector3 scale = item.lossyScale;
                col.center = new Vector3(pos.x / scale.x, pos.y / scale.y, pos.z / scale.z);
                col.size = new Vector3(item.GetSelectedRendererBounds().size.x / item.lossyScale.x, item.GetSelectedRendererBounds().size.y / item.lossyScale.y, item.GetSelectedRendererBounds().size.z / item.lossyScale.z);
                item.eulerAngles = oriEuler;
            }
        }
        #endregion

        #region 3.创建中心空父物体
        [MenuItem("Unity工具/1.模型调整/3.创建中心空父物体")]
        private static void CreatParent()
        {
            Transform[] parts = Selection.transforms;
            GameObject empty = new GameObject();
            foreach (var item in parts)
            {
                Renderer rend = item.GetComponentInChildren<Renderer>();
                if (rend)
                {
                    GameObject r = Instantiate(empty, item.parent);
                    r.transform.position = rend.bounds.center;
                    item.SetParent(r.transform);
                    r.name = item.name;
                }
            }
            DestroyImmediate(empty);
        }
        #endregion

        #region 4.根据所选的多个物体名字顺序排列
        [MenuItem("Unity工具/1.模型调整/4.根据所选的多个物体名字顺序排列")]
        private static void SetOrder()
        {
            Transform[] parts = Selection.transforms;
            for (int i = 0; i < parts.Length; i++)
            {
                Transform min = parts[i];
                for (int j = i + 1; j < parts.Length; j++)
                {
                    //Debug.Log(min.name + "++++" + parts[j].name + "++++" + string.CompareOrdinal(min.name, parts[j].name).ToString());
                    if (string.Compare(min.name, parts[j].name, false) > 0)
                    {
                        min = parts[j];
                        Transform temp = parts[i];
                        parts[i] = min;
                        parts[j] = temp;
                    }
                }
                min.SetSiblingIndex(i);
            }
        }
        #endregion

        #region 5.子物体根据Material归类
        [MenuItem("Unity工具/1.模型调整/5.子物体根据Material归类")]
        private static void AutoCreatMaterialParent()
        {
            //如果未选中任何物体 返回
            GameObject gameObject = Selection.activeGameObject;
            if (gameObject == null) return;
            var renders = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                if (gameObject.transform.Find(renders[i].materials[0].name) == null)
                {
                    GameObject obj = Instantiate(new GameObject(), Vector3.zero, new Quaternion(0, 0, 0, 0), gameObject.transform);
                    obj.name = renders[i].materials[0].name;
                    renders[i].transform.SetParent(obj.transform);
                }
                else
                {
                    renders[i].transform.SetParent(gameObject.transform.Find(renders[i].materials[0].name));
                }
            }
        }
        #endregion

        #region 6.自动删除不带MeshRenderer的空物体
        [MenuItem("Unity工具/1.模型调整/6.自动删除不带MeshRenderer的空物体")]
        private static void AutoDeleteNoneObj()
        {
            //如果未选中任何物体 返回
            GameObject gameObject = Selection.activeGameObject;
            if (gameObject == null) return;
            var renders = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                renders[i].transform.SetParent(gameObject.transform);
            }
            for (int i = 0; i < gameObject.GetComponentsInChildren<Transform>().Length; i++)
            {
                if (gameObject.GetComponentsInChildren<Transform>()[i].GetComponent<MeshRenderer>() == null)
                {
                    DestroyImmediate(gameObject.GetComponentsInChildren<Transform>()[i].gameObject);
                }
            }
        }
        #endregion

        #region 7.自动删除所选模型下所有碰撞体
        [MenuItem("Unity工具/1.模型调整/7.自动删除所选模型下所有碰撞体")]
        private static void AutoDeleteCollider()
        {
            Transform[] parts = Selection.transforms;
            for (int i = 0; i < parts.Length; i++)
            {
                var colliders = parts[i].GetComponentsInChildren<Collider>();
                for (int j = 0; j < colliders.Length; j++)
                {
                    DestroyImmediate(colliders[j]);
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 扩展方法：获取所有renderer的Bounds
    /// </summary>
    public static class TranformExtend
    {
        public static Bounds GetSelectedRendererBounds(this Transform tran)
        {
            if (tran.GetComponentsInChildren<Renderer>().Length > 1)
            {
                float maxX = float.MinValue;
                float maxY = float.MinValue;
                float maxZ = float.MinValue;
                float minX = float.MaxValue;
                float minY = float.MaxValue;
                float minZ = float.MaxValue;
                foreach (var item in tran.GetComponentsInChildren<Renderer>(true))
                {
                    Bounds bounds = item.bounds;
                    Vector3 center = bounds.center;
                    float x = bounds.extents.x;
                    float y = bounds.extents.y;
                    float z = bounds.extents.z;

                    maxX = maxX > center.x + x ? maxX : center.x + x;
                    maxY = maxY > center.y + y ? maxY : center.y + y;
                    maxZ = maxZ > center.z + z ? maxZ : center.z + z;
                    minX = minX < center.x - x ? minX : center.x - x;
                    minY = minY < center.y - y ? minY : center.y - y;
                    minZ = minZ < center.z - z ? minZ : center.z - z;
                }
                Vector3 rescenter = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2);
                Bounds res = new Bounds(rescenter, new Vector3(maxX - minX, maxY - minY, maxZ - minZ));
                res.size = res.size.x < 0.01f ? new Vector3(0.01f, res.size.y, res.size.z) : res.size;
                res.size = res.size.y < 0.01f ? new Vector3(res.size.x, 0.01f, res.size.z) : res.size;
                res.size = res.size.z < 0.01f ? new Vector3(res.size.x, res.size.y, 0.01f) : res.size;
                return res;
            }
            else
            {
                Bounds bounds = tran.GetComponentInChildren<Renderer>() ? tran.GetComponentInChildren<Renderer>().bounds : default;
                bounds.size = bounds.size.x < 0.01f ? new Vector3(0.01f, bounds.size.y, bounds.size.z) : bounds.size;
                bounds.size = bounds.size.y < 0.01f ? new Vector3(bounds.size.x, 0.01f, bounds.size.z) : bounds.size;
                bounds.size = bounds.size.z < 0.01f ? new Vector3(bounds.size.x, bounds.size.y, 0.01f) : bounds.size;
                return bounds;
            }
        }
    }
    #endregion


    #region -----------------------------------------------------2.UI调整-----------------------------------------------------

    #region 1.替换Hierarchy面板下所有的Text的字体
    public class UI_TextFontEditor : EditorWindow
    {
        private Font newFont;
        private FontStyle newFontStyle;

        [MenuItem("Unity工具/2.UI调整/1.替换Hierarchy面板下所有的Text的字体")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(UI_TextFontEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("需要替换成的字体:", EditorStyles.boldLabel);
            newFont = (Font)EditorGUILayout.ObjectField(newFont, typeof(Font), true);

            GUILayout.Space(10f);

            GUILayout.Label("新字体的FontStyle:", EditorStyles.boldLabel);
            newFontStyle = (FontStyle)EditorGUILayout.EnumPopup(newFontStyle);

            GUILayout.Space(20f);

            if (GUILayout.Button("替换字体"))
            {
                ReplaceFonts();
            }
        }

        private void ReplaceFonts()
        {
            Text[] allTextComponents = FindObjectsOfType<Text>();

            foreach (Text textComponent in allTextComponents)
            {
                Undo.RecordObject(textComponent, "Change Font");
                textComponent.font = newFont;
                textComponent.fontStyle = newFontStyle;
                EditorUtility.SetDirty(textComponent);
            }
        }
    }
    #endregion

    #region 2.替换所选的父物体的所有子物体的Image颜色
    public class UI_ModifyImageColor : EditorWindow
    {
        Color newColor;
        UI_ModifyImageColor()
        {
            this.titleContent = new GUIContent("修改颜色");
        }
        [MenuItem("Unity工具/2.UI调整/2.替换所选的父物体的所有子物体的Image颜色")]
        static void ShowWindow()
        {
            GetWindow(typeof(UI_ModifyImageColor));
        }
        private void OnGUI()
        {
            newColor = EditorGUILayout.ColorField("替换成的颜色", newColor);
            if (GUILayout.Button("替换颜色"))
            {
                Transform[] parts = Selection.transforms;
                for (int i = 0; i < parts.Length; i++)
                {
                    Image[] images = parts[i].GetComponentsInChildren<Image>(true);
                    for (int j = 0; j < images.Length; j++)
                    {
                        images[j].color = newColor;
                    }
                }
            }
        }
    }
    #endregion

    #region 3.替换所选的父物体的所有子物体的Text颜色
    public class UI_ModifyTextColor : EditorWindow
    {
        Color newColor;
        UI_ModifyTextColor()
        {
            this.titleContent = new GUIContent("修改颜色");
        }
        [MenuItem("Unity工具/2.UI调整/3.替换所选的父物体的所有子物体的Text颜色")]
        static void ShowWindow()
        {
            GetWindow(typeof(UI_ModifyTextColor));
        }
        private void OnGUI()
        {
            newColor = EditorGUILayout.ColorField("替换成的颜色", newColor);
            if (GUILayout.Button("替换颜色"))
            {
                Transform[] parts = Selection.transforms;
                for (int i = 0; i < parts.Length; i++)
                {
                    Text[] texts = parts[i].GetComponentsInChildren<Text>(true);
                    for (int j = 0; j < texts.Length; j++)
                    {
                        texts[j].color = newColor;
                    }
                }
            }
        }
    }
    #endregion

    #region

    /// <summary>
    /// 替换选中物体下所有材质、字体拓展
    /// </summary>
    public class UI_ModifyMaterial : EditorWindow
    {
        private Material newUIMaterial;

        UI_ModifyMaterial()
        {
            this.titleContent = new GUIContent("替换UI材质");
        }

        [MenuItem("Unity工具/2.UI调整/4.替换所选的父物体的所有子物体的UI材质")]
        private static void ShowWindow()
        {
            GetWindow<UI_ModifyMaterial>("替换UI材质");
        }

        private void OnGUI()
        {
            GUILayout.Label("设置UI材质", EditorStyles.boldLabel);
            newUIMaterial = (Material)EditorGUILayout.ObjectField(newUIMaterial, typeof(Material), false);
            if (GUILayout.Button("替换UI材质"))
            {
                ReplaceUIMaterials();
            }
        }

        /// <summary>
        /// 替换ui材质
        /// </summary>
        private void ReplaceUIMaterials()
        {
            
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

        #endregion

        #endregion
    }
}