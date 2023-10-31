using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AddCollider
{
    [MenuItem("编辑器工具/添加碰撞体")]
    static void AddBoxCollider()
    {
        //bool res = Selection.transforms[0].gameObject.active;



        foreach (var item in Selection.transforms)
        {
            Vector3 oriEuler = item.eulerAngles;
            item.eulerAngles = Vector3.zero;
            BoxCollider col = Undo.AddComponent<BoxCollider>(item.gameObject);
            Vector3 pos = item.GetRendererBounds().center - item.position;
            Vector3 scale = item.lossyScale;
            col.center = new Vector3(pos.x / scale.x, pos.y / scale.y, pos.z / scale.z);

            col.size = new Vector3(item.GetRendererBounds().size.x / item.lossyScale.x, item.GetRendererBounds().size.y / item.lossyScale.y, item.GetRendererBounds().size.z / item.lossyScale.z);
            //col.size = item.GetRendererBounds().size;
            item.eulerAngles = oriEuler;
        }
    }

}
/// <summary>
/// 扩展方法：获取所有renderer的Bounds
/// </summary>
public static class TranformExtend
{
    public static Transform FindByName(this Transform tran, string name)
    {
        foreach (var item in tran.GetComponentsInChildren<Transform>(true))
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }
    public static Bounds GetRendererBounds(this Transform tran)
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