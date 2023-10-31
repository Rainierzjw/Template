/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：DataModelPool
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：DataModel对象池，存储当前场景下所有DataModelBehaviour
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using System;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// DataModel对象池
    /// </summary>
    public class DataModelPool : ObjectPool<DataModelBehaviour>,IEventListener
    {
        /// <summary>
        /// DataModelBehaviour关联表
        /// </summary>
        [Inject]
        private AssociatedRecordPool _associatedTable;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataModelPool()
        {
            if(InjectService.Get<AssociatedRecordPool>() == null)
            {
                InjectService.RegisterSingleton(new AssociatedRecordPool());
            }
            InjectService.InjectInto(this);
        }

        /// <summary>
        /// 侦听DataModelBehaiviour事件
        /// </summary>
        /// <param name="evt">消息体</param>
        public void OnEvent(IEvent evt)
        {
            //处理DataModelBehaviour关联事件
            if (evt.EventName.Equals("AttachEntity"))
            {
                HandleAttachEvent(evt);
            }
            //处理DataModelBehaviour解除关联事件
            else if (evt.EventName.Equals("DetachEntity"))
            {
                HandleDetachEvent(evt);
            }
            //处理DataModelBehaviour的OnDisable事件
            else if (evt.EventName.Equals("OnDisable"))
            {
                HandleDisableEvent(evt);
            }
            //处理DataModelBehaviour的OnEnable事件
            else if (evt.EventName.Equals("OnEnable"))
            {
                HandleEnableEvent(evt);
            }
            //处理DataModelBehaviour的OnDestroy事件
            else if (evt.EventName.Equals("OnDestroy"))
            {
                HandleDestroyEvent(evt);
            }
        }

        /// <summary>
        /// 处理DataModelBehaviour关联事件
        /// </summary>
        /// <param name="evt">消息体</param>
        protected void HandleAttachEvent(IEvent evt)
        {
            MethodEvent me = evt as MethodEvent;
            DataModelBehaviour dmLeft = me.EventSource as DataModelBehaviour;
            DataModelBehaviour dmRight = me.Argments[0] as DataModelBehaviour;
            Entity ea = me.Argments[1] as Entity;

            //1、dmLeft监听dmRight上的ea实体对象
            ea.AddEventListener(dmLeft);

            //2、通过dmLeft ea dmRight 三个参数确定关联记录，创建关联记录并保存
            AssociatedRecord record = AssociatedRecordPool.CreateRecord(dmLeft, ea, dmRight);
            this._associatedTable.AddRecord(record);
        }

        /// <summary>
        /// 处理DataModelBehaviour解除关联事件
        /// </summary>
        /// <param name="evt">消息体</param>
        protected void HandleDetachEvent(IEvent evt)
        {
            MethodEvent me = evt as MethodEvent;
            DataModelBehaviour dmLeft = me.EventSource as DataModelBehaviour;
            DataModelBehaviour dmRight = me.Argments[0] as DataModelBehaviour;
            Entity ea = me.Argments[1] as Entity;

            //解除dm自身拥有的entity，此时需要完成两件事
            //1、entity删除所有Listener
            //2、_associatedTable中所有相关联的记录都应删除(相当于Entity销毁但dm没有销毁的情况)
            if (dmLeft == dmRight)
            {
                ea.ClearListeners();
                AssociatedRecord[] records = this._associatedTable.FindByEntity(ea);
                foreach (AssociatedRecord record in records)
                {
                    this._associatedTable.RemoveRecord(record);
                }
            }
            //解除dmLeft监听dmRight上的ea实体对象的情况
            else
            {
                //1、dmLeft解除监听dmRight上的ea实体对象
                ea.RemoveEventListener(dmLeft);

                //2、通过dmLeft ea dmRight 三个参数确定关联记录，创建关联记录并删除
                AssociatedRecord record = AssociatedRecordPool.CreateRecord(dmLeft, ea, dmRight);
                this._associatedTable.RemoveRecord(record);
            }
        }

        /// <summary>
        /// 处理DataModelBehaviour销毁事件
        /// </summary>
        /// <param name="evt">消息体</param>
        protected void HandleDestroyEvent(IEvent evt)
        {
            MethodEvent me = evt as MethodEvent;
            DataModelBehaviour dm = evt.EventSource as DataModelBehaviour;

            AssociatedRecord[] leftRecords = this._associatedTable.FindByLeft(dm);
            foreach(AssociatedRecord record in leftRecords)
            {
                record.AEntity.RemoveEventListener(dm);
                this._associatedTable.RemoveRecord(record);
            }

            AssociatedRecord[] rightRecords = this._associatedTable.FindByRight(dm);
            foreach (AssociatedRecord record in rightRecords)
            {
                record.AEntity.RemoveEventListener(record.Left);
                this._associatedTable.RemoveRecord(record);
            }
        }

        /// <summary>
        /// 处理DataModelBehaviour使能事件
        /// </summary>
        /// <param name="evt">消息体</param>
        protected void HandleEnableEvent(IEvent evt)
        {
            MethodEvent me = evt as MethodEvent;
            DataModelBehaviour dm = evt.EventSource as DataModelBehaviour;
            foreach(Entity en in dm.Entities)
            {
                en.Enable = true;
            }
        }

        /// <summary>
        /// 处理DataModelBehaviour失能事件
        /// </summary>
        /// <param name="evt">消息体</param>
        protected void HandleDisableEvent(IEvent evt)
        {
            MethodEvent me = evt as MethodEvent;
            DataModelBehaviour dm = evt.EventSource as DataModelBehaviour;
            foreach (Entity en in dm.Entities)
            {
                en.Enable = false;
            }
        }

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <param name="obj">模型对象</param>
        public override void RegisterObject(DataModelBehaviour obj)
        {
            obj.AddEventListener(this);
            base.RegisterObject(obj);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj">模型对象</param>
        public override void RemoveObject(DataModelBehaviour obj)
        {
            obj.RemoveEventListener(this);
            base.RemoveObject(obj);
        }

        /// <summary>
        /// 给定一个DataModelBehaviour返回其拥有的指定类型Entity
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dm">模型</param>
        /// <returns>实体</returns>
        public T FindEntity<T>(DataModelBehaviour dm) where T : Entity
        {
            T entity = dm.FindEntity<T>();
            return entity;
        }

        /// <summary>
        /// 给定GameObject返回其上拥有的指定类型Entity
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="go">GameObject</param>
        /// <returns>实体</returns>
        public T FindEntity<T>(GameObject go) where T: Entity
        {
            DataModelBehaviour dm = go.GetComponent<DataModelBehaviour>();
            if(dm == null)
            {
                return null;
            }
            return dm.FindEntity<T>();
        }

        /// <summary>
        /// 搜索所有同样类型的Entity
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>实体集合</returns>
        public T[] FindEntity<T>() where T : Entity
        {
            List<T> entities = new List<T>();
            this.Foreach((i,t) => {
                T e = t.FindEntity<T>();
                if(e != null)
                {
                    entities.Add(e);
                }
            });
            return entities.ToArray();
        }

        /// <summary>
        /// 根据指定ID和指定类型实体对象
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="id">与模型对象一致的唯一id</param>
        /// <returns>实体</returns>
        public T FindEntity<T>(int id) where T: Entity
        {
            DataModelBehaviour dm = FindDataModel(id);

            if (dm == null)
            {
                return null;
            }

            return dm.FindEntity<T>();
        }

        /// <summary>
        /// 根据指定ID和指定类型实体对象
        /// </summary>
        /// <param name="id">唯一id</param>
        /// <param name="type">类型</param>
        /// <returns>实体</returns>
        public Entity FindEntity(int id,Type type)
        {
            DataModelBehaviour dm = FindDataModel(id);

            if (dm == null)
            {
                return null;
            }

            return dm.FindEntity(type);
        }

        /// <summary>
        /// 搜索具备同样Entity类型的所有DataModelBehaviour列表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>模型对象集合</returns>
        public DataModelBehaviour[] FindDataModel<T>() where T : Entity
        {
            List<DataModelBehaviour> dms = new List<DataModelBehaviour>();
            this.Foreach((i, t) => {
                Entity e = t.FindEntity<T>();
                if (e != null)
                {
                    dms.Add(t);
                }
            });
            return dms.ToArray();
        }

        /// <summary>
        /// 根据ID号搜索DataModelBehaviour
        /// </summary>
        /// <param name="id">模型对象的唯一id</param>
        /// <returns>模型对象</returns>
        public DataModelBehaviour FindDataModel(int id)
        {
            DataModelBehaviour dm = null;
            this.Foreach((i, t) => {
                if(t.Id == id)
                {
                    dm = t;
                }
            });
            return dm;
        }
    }
}
