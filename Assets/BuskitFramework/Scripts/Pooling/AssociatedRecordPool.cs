/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：AssociatedRecordPool
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：实体关联关系表，用于描述一个GameObject上的实体与另一个GameObject上
*           实体的关联记录，保存的记录使用[左值  关联对象  右值]格式定义，表示
*           的含义为：左值通过监听关联对象事件与右值建立联系；
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 实体关联关系表，用于描述一个GameObject上的实体与另一个GameObject上
    /// 实体的关联记录，保存的记录使用[左值  关联对象  右值] 格式定义，表示
    /// 的含义为：左值通过监听关联对象事件与右值建立联系；
    /// </summary>
    public class AssociatedRecordPool : ObjectPool<AssociatedRecord>,ISerializable
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return objects.Count;
            }
        }

        /// <summary>
        /// 关联记录列表
        /// </summary>
        List<AssociatedRecord> RecordRows
        {
            get
            {
                return objects;
            }
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="record">关联表</param>
        public bool AddRecord(AssociatedRecord record)
        {
            if (!objects.Contains(record))
            {
                this.RegisterObject(record);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="record">关联表</param>
        /// <returns>bool</returns>
        public bool RemoveRecord(AssociatedRecord record)
        {
            if (objects.Contains(record))
            {
                objects.Remove(record);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 所搜与entity相关的记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>关联表集合</returns>
        public AssociatedRecord[] FindByEntity(Entity entity)
        {
            List<AssociatedRecord> dms = new List<AssociatedRecord>();
            foreach (AssociatedRecord r in this.objects)
            {
                if (r.AEntity == entity)
                {
                    dms.Add(r);
                }
            }
            return dms.ToArray();
        }

        /// <summary>
        /// 给定一个DataModelBehaviour获取所有左值记录（所有监听其他entity的记录）
        /// </summary>
        /// <param name="left">模型</param>
        /// <returns>关联表集合</returns>
        public AssociatedRecord[] FindByLeft(DataModelBehaviour left)
        {
            List<AssociatedRecord> dms = new List<AssociatedRecord>();
            foreach(AssociatedRecord r in this.objects)
            {
                if(r.Left == left)
                {
                    dms.Add(r);
                }
            }
            return dms.ToArray();
        }

        /// <summary>
        /// 给定一个DataModelBehaviour获取所有右值记录（所有被其他DataModelBehaviour监听的记录）
        /// </summary>
        /// <param name="right">模型</param>
        /// <returns>关联表集合</returns>
        public AssociatedRecord[] FindByRight(DataModelBehaviour right)
        {
            List<AssociatedRecord> dms = new List<AssociatedRecord>();
            foreach (AssociatedRecord l in this.objects)
            {
                if (l.Right == right)
                {
                    dms.Add(l);
                }
            }
            return dms.ToArray();
        }

        /// <summary>
        /// 给出左值、关联Entity、右值创建一条关联记录
        /// </summary>
        /// <param name="left">模型</param>
        /// <param name="ae">实体</param>
        /// <param name="right">模型</param>
        /// <returns>AssociatedRecord</returns>
        public static AssociatedRecord CreateRecord(
            DataModelBehaviour left,Entity ae,DataModelBehaviour right)
        {
            AssociatedRecord ar = new AssociatedRecord();
            ar.Left = left;
            ar.Right = right;
            ar.AEntity = ae;
            return ar;
        }

        /// <summary>
        /// 从另外一个对象深拷贝内容并保存到本对象中
        /// 复制过程执行浅复制
        /// </summary>
        /// <param name="seri">ISerializable对象</param>
        public virtual void Copy(ISerializable other)
        {
            if (!(other is AssociatedRecordPool))
            {
                throw new Buskit3DException("AssociatedRecordPool#Copy[Type inconsistency]");
            }
            AssociatedRecordPool ts = (AssociatedRecordPool)other;
            this.objects = ts.objects;
        }

        /// <summary>
        /// 将源对象属性值拷贝到目标对象
        /// 复制过程执行浅复制
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        public virtual void Copy(ISerializable source, ISerializable target)
        {
            if(!(source is AssociatedRecordPool) || !(target is AssociatedRecordPool))
            {
                throw new Buskit3DException("AssociatedRecordPool#Copy[Type inconsistency]");
            }
            AssociatedRecordPool ts = (AssociatedRecordPool)source;
            AssociatedRecordPool tt = (AssociatedRecordPool)target;
            tt.objects = ts.objects;
        }

        /// <summary>
        /// 拷贝自身对象并返回对象,复制过程中执行浅复制
        /// </summary>
        /// <returns>ISerializable对象</returns>
        public virtual ISerializable Clone()
        {
            AssociatedRecordPool table = new AssociatedRecordPool();
            table.objects = this.objects;
            return table;
        }

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>Jobject</returns>
        public virtual JObject ToJson()
        {
            JObject jo = new JObject();
            JArray array = new JArray();
            foreach (AssociatedRecord r in this.RecordRows)
            {
                JObject joRecord = r.ToJson();
                array.Add(joRecord);
            }
            jo["Records"] = array;
            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public virtual void FromJson(JObject obj)
        {
            JArray array = (JArray)obj["Records"];
            if (array != null)
            {
                foreach(JObject jo in array)
                {
                    AssociatedRecord ar = new AssociatedRecord();
                    ar.FromJson(jo);

                    //忽略自关联记录
                    if(ar.Left == ar.Right)
                    {
                        continue;
                    }
                    else
                    {
                        this.AddRecord(ar);
                    }
                }
            }
        }
    }
}
