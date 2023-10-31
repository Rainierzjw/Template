/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：DataModelBehaviour
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：事件驱动MonoBehaviour
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;
using UnityEngine;
using System.Collections.Generic;
using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 事件驱动MonoBehaviour
    /// </summary>
    [DisallowMultipleComponent]
    public class DataModelBehaviour : EventSupportBehaviour, IEventListener
    {
        /// <summary>
        /// 获取ID号
        /// </summary>
        [FireInitEvent]
        public int Id
        {
            set
            {
                UniqueID uid = GetComponent<UniqueID>();
                if (uid != null)
                {
                    int oldValue = uid.UniqueId;
                    _id = value;
                    foreach (Entity en in Entities)
                    {
                        en.Id = value;
                    }
                    this.FireEvent("Id", oldValue, uid.UniqueId);
                }
                else
                {
                    int oldValue = _id;
                    _id = value;
                    foreach (Entity en in Entities)
                    {
                        en.Id = value;
                    }
                    this.FireEvent("Id", oldValue, _id);
                }
            }
            get
            {
                UniqueID uid = GetComponent<UniqueID>();
                if (uid != null)
                {
                    _id = uid.UniqueId;
                }
                return _id;
            }
        }

        /// <summary>
        /// 获取ID号
        /// </summary>
        protected int _id = -1;


        /// <summary>
        /// 自身拥有的实体对象
        /// </summary>
        public List<Entity> Entities
        {
            get
            {
                return _entities;
            }
        }

        /// <summary>
        /// 自身拥有的实体对象
        /// </summary>
        private List<Entity> _entities = new List<Entity>();

        /// <summary>
        /// 事件驱动Behaviour对象池
        /// </summary>
        [Inject]
        protected DataModelPool dataModels;

        /// <summary>
        /// 注入关联表
        /// </summary>
        [Inject]
        protected AssociatedRecordPool associatedTable;

        /// <summary>
        /// 注入GameObject对象池
        /// </summary>
        [Inject]
        protected GameObjectPool gameObjects;

        /// <summary>
        /// 初始化EventSource、执行依赖注入、绑定Entity
        /// </summary>
        protected override void Awake()
        {
           
            //实例化EventSource
            this.eventSource = new EventSource(this);

            //2、注入依赖项
            InjectService.InjectInto(this);

            //3、将自己注册到对象池中
            this.dataModels.RegisterObject(this);
            gameObjects.RegisterObject(this.gameObject);
        }

        /// <summary>
        /// 子类复写此函数
        /// </summary>
        protected virtual void Start()
        {

            //1、绑定自己拥有的实体对象(Entity)
            foreach (Entity en in this.Entities)
            {
                AttachEntity(en);
            }

            //2、获取GameObject上的所有EventSupportBehaviour
            //   绑定所有EventSupportBehaviour
            EventSupportBehaviour[] esbs = 
                GetComponents<EventSupportBehaviour>();
            foreach(EventSupportBehaviour esb in esbs)
            {
                if(esb != this)
                {
                    if (esb.Contains(this))
                    {
                        esb.AddEventListener(this);
                    }
                }
            }
        }

        /// <summary>
        /// 关联自身实体对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected void AttachEntity(Entity entity)
        {
            //Entity赋值唯一ID号
            entity.Id = this.Id;

            //如果_entities中不存在entity实例则添加到_entities列表中
            if (!_entities.Contains(entity))
            {
                this._entities.Add(entity);
            }

            //广播绑定实体类消息
            this.FireEvent("AttachEntity", new object[] { this, entity });
        }

        /// <summary>
        /// 分离对另外一个实体对象的监听
        /// </summary>
        /// <param name="entity">实体</param>
        protected void DetachEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new Buskit3DException("DataModelBehaviour#DetachEntity[Trying to bind empty entity]");
            }

            //广播取消绑定实体类消息
            this.FireEvent("DetachEntity", new object[] { this, entity });
        }

        /// <summary>
        /// 关联自身实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        public void AttachEntity<T>() where T : Entity,new()
        {
            //判断是否有相同类型的实体对象，如果有则抛出异常
            Entity entity = this.FindEntity<T>();

            if (entity != null)
            {
                throw new Buskit3DException("DataModelBehaviour#AttachEntity[Repeated addition of entitiy of the same type]");
            }
            
            //实例化实体对象
            entity = new T();

            //Entity赋值唯一ID号
            entity.Id = this.Id;

            //添加到Entity列表
            this._entities.Add(entity);

            //广播关联事件
            this.FireEvent("AttachEntity", new object[] { this, entity });
        }

        /// <summary>
        /// 分离对另外一个实体对象的监听
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        public void DetachEntity<T>() where T : Entity
        {
            Entity entity = this.FindEntity<T>();

            if (entity == null)
            {
                throw new Buskit3DException("DataModelBehaviour#DetachEntity[Trying to bind empty entity]");
            }

            //广播取消绑定实体类消息
            this.FireEvent("DetachEntity", new object[] { this, entity });

            //从实体对象列表中删除对象
            this._entities.Remove(entity);
        }

        /// <summary>
        /// 关联实体对象
        /// </summary>
        /// <param name="type">指定继承自Entity的type</param>
        public void AttachEntity(Type type)
        {
            //如果有同类型Entity则抛出重复添加异常
            if (FindEntity(type) != null)
            {
                throw new Buskit3DException("DataModelBehaviour#AttachEntity[Repeated addition of entitiy of the same type]");
            }

            //如果给定类型不能正常创建Entity或不是Entity类型则抛出异常
            object entityObj = Activator.CreateInstance(type);
            if (!(entityObj is Entity))
            {
                throw new Buskit3DException("DataModelBehaviour#AttachEntity[The specified type should inherit from Entity]");
            }

            //Entity赋值唯一ID号
            Entity entity = entityObj as Entity;
            entity.Id = this.Id;

            //添加到Entity列表
            this._entities.Add(entity);

            //广播关联事件
            this.FireEvent("AttachEntity", new object[] { this, entityObj });
        }

        /// <summary>
        /// 分离对另外一个实体对象的监听
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        public void DetachEntity(Type type)
        {
            Entity entity = this.FindEntity(type);

            if (entity == null)
            {
                throw new Buskit3DException("DataModelBehaviour#DetachEntity[Trying to bind empty entity]");
            }

            //广播取消绑定实体类消息
            this.FireEvent("DetachEntity", new object[] { this, entity });

            //从实体对象列表中删除对象
            this._entities.Remove(entity);
        }

        /// <summary>
        /// 关联另外一个GameObject上的Entity
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="edb">模型对象</param>
        public void AttachEntity<T>(DataModelBehaviour edb) where T:Entity
        {
            Entity entity = edb.FindEntity<T>();

            if (entity == null)
            {
                throw new Buskit3DException("DataModelBehaviour#AttachEntity[Trying to bind empty entity]");
            }

            //广播绑定实体类消息
            this.FireEvent("AttachEntity", new object[] {edb, entity });
        }

        /// <summary>
        /// 取消关联另外一个GameObject上的Entity
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="edb">模型对象</param>
        public void DetachEntity<T>(DataModelBehaviour edb) where T : Entity
        {
            Entity entity = edb.FindEntity<T>();

            if (entity == null)
            {
                throw new Buskit3DException("DataModelBehaviour#DetachEntity[Trying to bind empty entity]");
            }
            
            //广播取消绑定实体类消息
            this.FireEvent("DetachEntity", new object[] { edb, entity });
        }

        /// <summary>
        /// 向关联EventDirvenBehaviour发送OnDisable事件
        /// </summary>
        protected override void OnDisable()
        {
            this.FireEvent("OnDisable", null);
        }

        /// <summary>
        /// 向关联EventDirvenBehaviour发送OnEnable事件
        /// </summary>
        protected override void OnEnable()
        {
            this.FireEvent("OnEnable", null);
        }

        /// <summary>
        /// 向关联EventDirvenBehaviour发送OnDestroy事件
        /// </summary>
        protected override void OnDestroy()
        {
            //解除绑定自己拥有的实体对象(Entity)
            for (int i = 0; i < Entities.Count; i++)
            {
                DetachEntity(Entities[i]);
            }
            this.FireEvent("OnDestroy", null);
            this.dataModels.RemoveObject(this);
            this.gameObjects.RemoveObject(this.gameObject);
        }

        /// <summary>
        /// 响应Entity事件
        /// </summary>
        /// <param name="evt">消息体</param>
        public void OnEvent(IEvent evt)
        {
            //发送由Entity和EventSupportBehaviour发出的事件
            if ((evt.EventSource is Entity) || ((evt.EventSource is EventSupportBehaviour)))
            {
                //如果是自身发出的事件则不向Logic广播
                if (evt.EventSource == this.eventSource)
                {
                    return;
                }

                LogicBehaviour[] handlers = this.GetComponents<LogicBehaviour>();
                foreach (LogicBehaviour eh in handlers)
                {
                    //搜索事件处理的[Interested]注解
                    Type type = eh.GetType();
                    object[] attrs = type.GetCustomAttributes(true);
                    InterestedAttribute iattr = null;
                    foreach (Attribute attr in attrs)
                    {
                        if (attr.GetType() == typeof(InterestedAttribute))
                        {
                            iattr = (InterestedAttribute)attr;
                            break;
                        }
                    }

                    //当未明确指定感兴趣的实体对象时，
                    //认为处理器对所有实体数据都敏感。
                    //如下写法:
                    // XXHandler : LogicBehaviour{
                    //   ....
                    // }
                    if (iattr == null)
                    {
                        eh.ProcessLogic(evt);
                        continue;
                    }

                    //当明确给出敏感实体对象标签却没有指定敏感类型时,
                    //认为处理器对所有实体数据都敏感。
                    //类似这种写法:
                    // [Interested]
                    // XXHandler : LogicBehaviour{
                    //   ....
                    // }
                    else if (iattr.Types == null)
                    {
                        eh.ProcessLogic(evt);
                        continue;
                    }
                    //当明确给出敏感实体对象标签却没有指定敏感类型时,
                    //认为处理器对所有实体数据都敏感。
                    //类似这种写法:
                    // [Interested(Types=new Type[]{})]
                    // XXHandler : LogicBehaviour{
                    //   ....
                    // }
                    else if (iattr.Types.Length == 0)
                    {
                        eh.ProcessLogic(evt);
                        continue;
                    }
                    //当明确给出敏感实体对象标签并明确指定敏感类型时,
                    //认为处理器对指定实体类型事件敏感。
                    //类似这种写法:
                    // [Interested(Types=new Type[]{typeof(XXEntity)})]
                    // XXHandler : LogicBehaviour{
                    //   ....
                    // }
                    else if ((iattr.Types != null) && (iattr.Types.Length > 0))
                    {
                        //当给定标签却没有填充内容时
                        Type[] iatypes = iattr.Types;
                        Type typeOfSource = evt.EventSource.GetType();
                        foreach (Type targetType in iatypes)
                        {
                            if (targetType == typeOfSource)
                            {
                                eh.ProcessLogic(evt);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查找指定类型的实体对象
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns>实体对象</returns>
        public Entity FindEntity(Type type)
        {
            Entity entity = null;
            foreach (Entity en in this.Entities)
            {
                if (en.GetType() == type)
                {
                    entity = en;
                    break;
                }
            }
            return entity;
        }

        /// <summary>
        /// 查找特定类型的实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>实体对象</returns>
        public T FindEntity<T>() where T : Entity
        {
            T entity = null;
            foreach (Entity en in this.Entities)
            {
                if (en.GetType() == typeof(T))
                {
                    entity = en as T;
                    break;
                }
            }
            return entity;
        }
    }
}
