/*******************************************************************************
* ��Ȩ�������������������Ƽ����޹�˾���������а�Ȩ
* �汾������v1.0.0
* �� �� �ƣ�UnityExtensionToolsEditor
* �������ڣ�2023-08-21 08:58:47
* �������ƣ�Τΰ
* CLR �汾��4.0.30319.42000
* �޸ļ�¼��
* ������unity��չ����
******************************************************************************/

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EditorTools
{
    #region -----------------------------------------------------1.ģ�͵���-----------------------------------------------------

    #region 1.��ѡ��ģ�������޸Ĳ���
    public class Model_ModifyMaterial : EditorWindow
    {
        Material material;
        Model_ModifyMaterial()
        {
            this.titleContent = new GUIContent("�޸Ĳ���");
        }
        [MenuItem("Unity����/1.ģ�͵���/1.��ѡ��ģ�������޸Ĳ���")]
        static void ShowWindow()
        {
            GetWindow(typeof(Model_ModifyMaterial));
        }
        private void OnGUI()
        {
            material = (Material)EditorGUILayout.ObjectField("��ģ��Ҫ�滻�ɵĲ���", material, typeof(Material), true);
            if (GUILayout.Button("�滻����"))
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
        #region 2.����ģ�ʹ�С���Box��ײ��
        [MenuItem("Unity����/1.ģ�͵���/2.����ģ����״���Box��ײ��")]
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

        #region 3.�������Ŀո�����
        [MenuItem("Unity����/1.ģ�͵���/3.�������Ŀո�����")]
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

        #region 4.������ѡ�Ķ����������˳������
        [MenuItem("Unity����/1.ģ�͵���/4.������ѡ�Ķ����������˳������")]
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

        #region 5.���������Material����
        [MenuItem("Unity����/1.ģ�͵���/5.���������Material����")]
        private static void AutoCreatMaterialParent()
        {
            //���δѡ���κ����� ����
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

        #region 6.�Զ�ɾ������MeshRenderer�Ŀ�����
        [MenuItem("Unity����/1.ģ�͵���/6.�Զ�ɾ������MeshRenderer�Ŀ�����")]
        private static void AutoDeleteNoneObj()
        {
            //���δѡ���κ����� ����
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

        #region 7.�Զ�ɾ����ѡģ����������ײ��
        [MenuItem("Unity����/1.ģ�͵���/7.�Զ�ɾ����ѡģ����������ײ��")]
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
    /// ��չ��������ȡ����renderer��Bounds
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


    #region -----------------------------------------------------2.UI����-----------------------------------------------------

    #region 1.�滻Hierarchy��������е�Text������
    public class UI_TextFontEditor : EditorWindow
    {
        private Font newFont;
        private FontStyle newFontStyle;

        [MenuItem("Unity����/2.UI����/1.�滻Hierarchy��������е�Text������")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(UI_TextFontEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("��Ҫ�滻�ɵ�����:", EditorStyles.boldLabel);
            newFont = (Font)EditorGUILayout.ObjectField(newFont, typeof(Font), true);

            GUILayout.Space(10f);

            GUILayout.Label("�������FontStyle:", EditorStyles.boldLabel);
            newFontStyle = (FontStyle)EditorGUILayout.EnumPopup(newFontStyle);

            GUILayout.Space(20f);

            if (GUILayout.Button("�滻����"))
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

    #region 2.�滻��ѡ�ĸ�����������������Image��ɫ
    public class UI_ModifyImageColor : EditorWindow
    {
        Color newColor;
        UI_ModifyImageColor()
        {
            this.titleContent = new GUIContent("�޸���ɫ");
        }
        [MenuItem("Unity����/2.UI����/2.�滻��ѡ�ĸ�����������������Image��ɫ")]
        static void ShowWindow()
        {
            GetWindow(typeof(UI_ModifyImageColor));
        }
        private void OnGUI()
        {
            newColor = EditorGUILayout.ColorField("�滻�ɵ���ɫ", newColor);
            if (GUILayout.Button("�滻��ɫ"))
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

    #region 3.�滻��ѡ�ĸ�����������������Text��ɫ
    public class UI_ModifyTextColor : EditorWindow
    {
        Color newColor;
        UI_ModifyTextColor()
        {
            this.titleContent = new GUIContent("�޸���ɫ");
        }
        [MenuItem("Unity����/2.UI����/3.�滻��ѡ�ĸ�����������������Text��ɫ")]
        static void ShowWindow()
        {
            GetWindow(typeof(UI_ModifyTextColor));
        }
        private void OnGUI()
        {
            newColor = EditorGUILayout.ColorField("�滻�ɵ���ɫ", newColor);
            if (GUILayout.Button("�滻��ɫ"))
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
    /// �滻ѡ�����������в��ʡ�������չ
    /// </summary>
    public class UI_ModifyMaterial : EditorWindow
    {
        private Material newUIMaterial;

        UI_ModifyMaterial()
        {
            this.titleContent = new GUIContent("�滻UI����");
        }

        [MenuItem("Unity����/2.UI����/4.�滻��ѡ�ĸ�����������������UI����")]
        private static void ShowWindow()
        {
            GetWindow<UI_ModifyMaterial>("�滻UI����");
        }

        private void OnGUI()
        {
            GUILayout.Label("����UI����", EditorStyles.boldLabel);
            newUIMaterial = (Material)EditorGUILayout.ObjectField(newUIMaterial, typeof(Material), false);
            if (GUILayout.Button("�滻UI����"))
            {
                ReplaceUIMaterials();
            }
        }

        /// <summary>
        /// �滻ui����
        /// </summary>
        private void ReplaceUIMaterials()
        {
            
            GameObject[] selectedObjects = Selection.gameObjects;

            foreach (var obj in selectedObjects)
            {
                ReplaceUIMaterialRecursive(obj.transform);
            }

            Debug.Log("UI�����滻���");
        }

        private void ReplaceUIMaterialRecursive(Transform parent)
        {
            // ����UI������滻�����
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