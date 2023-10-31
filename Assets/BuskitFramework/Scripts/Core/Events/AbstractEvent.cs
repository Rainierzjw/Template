/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：AbstractEvent
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 抽象事件，作为MethodEvent和PropertyEvent的基类
    /// </summary>
    public class AbstractEvent : IEvent, ISerializable
    {
        
        public object EventSource
        {
            set
            {
                this.eventSource = value;
            }
            get
            {
                return this.eventSource;
            }
        }
        /// <summary>
        /// 事件源对象，发出事件的对象既是实现IEventSourceSupportable的对象
        /// </summary>
        protected object eventSource = null;

        /// <summary>
        /// 事件名称，指方法或属性名称
        /// </summary>
        public string EventName
        {
            set
            {
                this.eventName = value;
            }
            get
            {
                return this.eventName;
            }
        }
        /// <summary>
        /// 事件名称，指方法或属性名称
        /// </summary>
        protected string eventName = "";

        /// <summary>
        /// 回调事件源属性或方法
        /// </summary>
        public virtual void InokeEventSource()
        {

        }

        /// <summary>
        /// 从另外一个对象中复制，执行浅复制
        /// </summary>
        /// <param name="seri">实现了ISerializable的对象</param>
        public virtual void Copy(ISerializable other)
        {
            if (this.GetType() != other.GetType())
            {
                throw new Buskit3DException("AbstractEvent#Copy[type inconsistency]");
            }

            var allFields = other
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            var allProperties = other
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            foreach (FieldInfo info in allFields)
            {
                var value = info.GetValue(other);
                FieldInfo fieldInfo = this.GetType().GetField(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(this, value);
            }

            foreach (PropertyInfo info in allProperties)
            {
                var value = info.GetValue(other);
                PropertyInfo propertyInfo = this.GetType().GetProperty(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(this, value);
            }
        }

        /// <summary>
        /// 将源对象属性值拷贝到目标对象，执行浅复制
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        public virtual void Copy(ISerializable source, ISerializable target)
        {
            if (source.GetType() != target.GetType())
            {
                throw new Buskit3DException("AbstractEvent#Copy[type inconsistency]");
            }

            var allFields = source
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            var allProperties = source
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            foreach (FieldInfo info in allFields)
            {
                var value = info.GetValue(source);
                FieldInfo fieldInfo = target.GetType().GetField(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(target, value);
            }

            foreach (PropertyInfo info in allProperties)
            {
                var value = info.GetValue(source);
                PropertyInfo propertyInfo = target.GetType().GetProperty(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(target, value);
            }
        }

        /// <summary>
        /// 拷贝自身对象并返回对象，执行浅复制
        /// </summary>
        /// <returns></returns>
        public virtual ISerializable Clone()
        {
            return null;
        }

        /// <summary>
        /// 序列化为JSON对象，执行浅复制
        /// </summary>
        /// <returns>JObject</returns>
        public virtual JObject ToJson()
        {
            //填充事件基本信息
            JObject jo = new JObject();
            jo["EventType"] = this.GetType().ToString();    //事件类型
            jo["EventName"] = this.eventName;               //事件名称
            jo["ClassType"] = this.GetType().AssemblyQualifiedName;

            //序列化事件源
            //如果事件源是Entity则序列化参数和类型
            if (this.eventSource is Entity)
            {
                jo["EventSource"] = new JObject();
                jo["EventSource"]["ClassType"] = this.eventSource.GetType().AssemblyQualifiedName;
                jo["EventSource"]["Type"] = "Entity";
                jo["EventSource"]["Id"] = (this.eventSource as Entity).Id;
            }
            //如果事件源是DataModelBehaviour则序列化参数和类型
            else if (this.eventSource is DataModelBehaviour)
            {
                jo["EventSource"] = new JObject();
                jo["EventSource"]["ClassType"] = this.eventSource.GetType().AssemblyQualifiedName;
                jo["EventSource"]["Type"] = "DataModelBehaviour";
                jo["EventSource"]["Id"] = (this.eventSource as DataModelBehaviour).Id;
            }
            //如果事件源是Behaviour类型则保持其GameObjectID及Behaviour类型
            else if (this.eventSource is Behaviour)
            {
                Behaviour behaviour = (Behaviour)this.eventSource;
                GameObject go = behaviour.gameObject;
                int uniqueId = go.GetComponent<UniqueID>().UniqueId;

                jo["EventSource"] = new JObject();
                jo["EventSource"]["ClassType"] = this.eventSource.GetType().AssemblyQualifiedName;
                jo["EventSource"]["Type"] = "Behaviour";
                jo["EventSource"]["Id"] = uniqueId;
            }
            //如果事件源实现ISerializable则事件源对象自己完成序列化
            else if (this.eventSource is ISerializable)
            {
                ISerializable serializable = (ISerializable)this.eventSource;
                jo["EventSource"]["ClassType"] = this.eventSource.GetType().AssemblyQualifiedName;
                jo["EventSource"]["Type"] = "Serializable";
                jo["EventSource"]["Content"] = serializable.ToJson();
            }
            //如果事件源不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string json = JsonConvert.SerializeObject(this.eventSource, Formatting.Indented, settings);
                jo["EventSource"]["ClassType"] = this.eventSource.GetType().AssemblyQualifiedName;
                jo["EventSource"]["Type"] = "Other";
                try
                {
                    jo["EventSource"]["Content"] = JObject.Parse(json);
                }
                catch (Exception e)
                {
                    jo["EventSource"]["Content"] = json;
                }
            }

            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public virtual void FromJson(JObject obj)
        {
            string evtType = obj["EventType"].ToString();   //事件类型
            string evtName = obj["EventName"].ToString();   //事件名称

            //设置反序列化事件名称
            this.eventName = evtName;

            //如果当前Type和给定Type不一致，则抛出异常
            if (Type.GetType(evtType) != this.GetType())
            {
                throw new Buskit3DException("MethodEvent#FromJson[type inconsistency]");
            }

            //事件源
            JObject joEventSource = obj["EventSource"] as JObject;
            //事件源类型
            string eventSourceType = obj["EventSource"]["Type"].ToString();
            //事件源类类型
            string eventSourceClassType = obj["EventSource"]["ClassType"].ToString();

            //DataModel对象池
            DataModelPool pool = InjectService.Get<DataModelPool>();
            //GameObject对象池
            GameObjectPool goPool = InjectService.Get<GameObjectPool>();

            //判断事件源类型根据事件源Entity/DataModelBehaviour/ISerializable
            //当事件源是Entity时，则根据Entity.Id来搜索Entity实例
            if (eventSourceType.Equals("Entity"))
            {
                int id = int.Parse(obj["EventSource"]["Id"].ToString());

                Entity entity = pool.FindEntity(id, Type.GetType(eventSourceClassType));
                if (entity == null)
                {
                  throw new Buskit3DException("MethodEvent#FromJson[No entity object was found]");
                }
                this.eventSource = entity;
            }
            //当事件源是DataModelBehaviour时，则根据DataModelBehaviour.Id搜索DataModelBehaviour实例
            else if (eventSourceType.Equals("DataModelBehaviour"))
            {
                int id = int.Parse(obj["EventSource"]["Id"].ToString());
                DataModelBehaviour dataModel = pool.FindDataModel(id);

                if (dataModel == null)
                {
                    throw new Buskit3DException("MethodEvent#FromJson[No DataModel object was found]");
                }
                this.eventSource = dataModel;
            }
            //当事件源是Behaviour类型时，根据GameObject.UniqueID搜索指定类型Behaviour
            else if (eventSourceType.Equals("Behaviour"))
            {
                int id = int.Parse(obj["EventSource"]["Id"].ToString());
                Type type = Type.GetType(eventSourceClassType);
                GameObject go = goPool.FindGameObject(id);
                if (go == null)
                {
                  throw new Buskit3DException("MethodEvent#FromJson[No gameobject object was found]");
                }
                Behaviour behaviour = go.GetComponent(type) as Behaviour;
                this.eventSource = behaviour;
            }
            //当事件源是Serializable时，则直接反序列化对象
            else if (eventSourceType.Equals("Serializable"))
            {
                Type type = Type.GetType(eventSourceClassType);
                ISerializable deserialObj = Activator.CreateInstance(type) as ISerializable;
                deserialObj.FromJson(obj["EventSource"]["Content"] as JObject);
                this.EventSource = deserialObj;
            }
            //如果不是Entity/DataModelBehaviour/ISerializable
            else if (eventSourceType.Equals("Other"))
            {
                Type type = Type.GetType(eventSourceClassType);
                string json = "";
                try
                {
                    json = (joEventSource["Content"] as JObject).ToString();
                }
                catch (Exception e)
                {
                    json = joEventSource["Content"].ToString();
                }
                if (json.Equals(""))
                {
                    throw new Buskit3DException("AbstractEvent#FromJson:[Json Error]");
                }
                object desrialObj = JsonConvert.DeserializeObject(json, type);
                this.EventSource = desrialObj;
            }
        }
    }
}
