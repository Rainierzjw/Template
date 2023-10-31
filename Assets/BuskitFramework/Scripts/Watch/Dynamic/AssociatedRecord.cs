/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：AssociatedRecord
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：保存的记录使用[左值  关联对象  右值]格式定义，表示
*           的含义为：左值通过监听关联对象事件与右值建立联系；
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;
using Newtonsoft.Json.Linq;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 保存的记录使用[左值  关联对象  右值]格式定义，表示
    /// 的含义为：左值通过监听关联对象事件与右值建立联系；
    /// </summary>
    public class AssociatedRecord : ISerializable
    {
        /// <summary>
        /// 左值
        /// </summary>
        public DataModelBehaviour Left;

        /// <summary>
        /// 关联对象
        /// </summary>
        public Entity AEntity;

        /// <summary>
        /// 右值
        /// </summary>
        public DataModelBehaviour Right;

        /// <summary>
        /// 复写Equals函数
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            AssociatedRecord ar = (AssociatedRecord)obj;
            if ((ar.Left == this.Left) && (ar.AEntity == this.AEntity) && (ar.Right ==this.Right))
            {
                return true;
            }
            bool equals = base.Equals(obj);
            return equals;
        }


        /// <summary>
        /// 从另外一个对象深拷贝内容并保存到本对象中
        /// </summary>
        /// <param name="seri">可序列化对象</param>
        public virtual void Copy(ISerializable other)
        {
            if (!(other is AssociatedRecord))
            {
                throw new Buskit3DException("AssociatedRecord#Copy[Type inconsistency]");
            }

            AssociatedRecord or = (AssociatedRecord)other;
            this.AEntity = or.AEntity;
            this.Left    = or.Left;
            this.Right   = or.Right;
        }

        /// <summary>
        /// 将源对象属性值拷贝到目标对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        public virtual void Copy(ISerializable source, ISerializable target)
        {
            if (!((target is AssociatedRecord) || (source is AssociatedRecord)))
            {
                throw new Buskit3DException("AssociatedRecord#Copy[Type inconsistency]");
            }

            AssociatedRecord s = (AssociatedRecord)source;
            AssociatedRecord t = (AssociatedRecord)target;
            t.AEntity = s.AEntity;
            t.Left    = s.Left;
            t.Right   = s.Right;
        }

        /// <summary>
        /// 执行浅复制，对于关联表记录而言深复制没有意义
        /// 原因在于记录中的对象与记录本身都是引用关系而不是组合或聚合关系
        /// </summary>
        /// <returns>ISerializable对象</returns>
        public virtual ISerializable Clone()
        {
            AssociatedRecord ar = new AssociatedRecord();
            ar.AEntity = this.AEntity;
            ar.Left = this.Left;
            ar.Right = this.Right;
            return ar;
        }

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>JObject</returns>
        public virtual JObject ToJson()
        {
            JObject jo = new JObject();
            jo["Left"] = Left.Id;
            jo["Right"] = Right.Id;
            jo["AssociatedEntity"] = new JObject();
            jo["AssociatedEntity"]["Id"] = AEntity.Id;
            jo["AssociatedEntity"]["Type"] = AEntity.GetType().AssemblyQualifiedName;
            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public virtual void FromJson(JObject obj)
        {
            string strLid = obj["Left"].ToString();
            string strRid = obj["Right"].ToString();
            string strEid = obj["AssociatedEntity"]["Id"].ToString();
            string strEType = obj["AssociatedEntity"]["Type"].ToString();

            int idL = int.Parse(strLid);
            int idR = int.Parse(strRid);
            int idE = int.Parse(strEid);
            Type typeE = Type.GetType(strEType);

            DataModelBehaviour dmL = null;
            DataModelBehaviour dmR = null;
            Entity ea = null;
        
            DataModelPool pool = InjectService.Get<DataModelPool>();

            dmL = pool.FindDataModel(idL);
            dmR = pool.FindDataModel(idR);
            ea  = pool.FindEntity(idE, typeE);

            if((dmL == null) || (dmR == null) || (ea == null))
            {
                throw new Buskit3DException("AssociatedRecord#FromJson[No specified ID number object was found]");
            }

            this.Left = dmL;
            this.Right = dmR;
            this.AEntity = ea;
        }
    }
}
