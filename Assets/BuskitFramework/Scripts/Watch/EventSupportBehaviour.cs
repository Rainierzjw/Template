/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：EventSupportBehaviour
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：事件支持Behaviour
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System.Linq;
using System.Reflection;
using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 事件支持Behaviour
    /// </summary>
    public class EventSupportBehaviour : MonoBehaviour,IEventSourceSupportable, ISerializable
    {
        /// <summary>
        /// 初始化IOC
        /// </summary>
        static EventSupportBehaviour()
        {
            //tods:检查IOC初始化代码是否有问题
            DefaultIoCInitializer ioc = DefaultIoCInitializer.NewInstance();
            ioc.InitializeIoC();
        }

        /// <summary>
        /// 事件源用于记录实体对象的侦听器、广播属性和方法事件
        /// </summary>
        protected EventSource eventSource = null;

        /// <summary>
        /// 初始化EventSource、执行依赖注入、绑定Entity
        /// </summary>
        protected virtual void Awake()
        {
            //实例化EventSource
            if (this.eventSource == null)
                this.eventSource = new EventSource(this);
            //注入依赖项
            InjectService.InjectInto(this);

            //获取DataModel如果DataModel不为空则添加DataModel为自己的侦听器
            //此处代码主要为GameObject动态添加EventSupportBehaviour服务。
            //当运行态添加EventSupportBehaviour时，自己添加DataModel为侦听器。
            //静态添加EventSupportBehaviour时，在DataModel#Start中注册侦听器。
            DataModelBehaviour dm = GetComponent<DataModelBehaviour>();
            if (dm == null)
            {
                throw new Buskit3DException("EventSupportBehaviour#Awake[ DataModelBehaviour on this gameobject is null, or Initialize  order wrong:  DataModelBehaviour（Awake） is behind of    EventSupportBehaviour(Awake) ] ");
            }
            if (!this.Contains(dm))
            {
                this.AddEventListener(dm);
            }
        }

        /// <summary>
        /// 向关联EventDirvenBehaviour发送OnDisable事件
        /// </summary>
        protected virtual void OnDisable()
        {
            this.FireEvent("OnDisable", null);
            this.eventSource.OffFire();
        }

        /// <summary>
        /// 向关联EventDirvenBehaviour发送OnEnable事件
        /// </summary>
        protected virtual void OnEnable()
        {
            this.eventSource.OnFire();
            this.FireEvent("OnEnable", null);
        }

        /// <summary>
        /// 向关联EventDirvenBehaviour发送OnDestroy事件
        /// </summary>
        protected virtual void OnDestroy()
        {
            this.eventSource.ClearListeners();
            this.FireEvent("OnDestroy", null);
        }

        /// <summary>
        /// 判断是否已经拥有事件源
        /// </summary>
        /// <param name="listener">侦听器</param>
        /// <returns>bool</returns>
        public bool Contains(IEventListener listener)
        {
            if (this.eventSource == null)
                this.eventSource = new EventSource(this);
            return this.eventSource.Contains(listener);
        }

        /// <summary>
        /// 添加事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        public void AddEventListener(IEventListener listener)
        {
            this.eventSource.AddEventListener(listener);
        }

        /// <summary>
        /// 清除事件侦听器
        /// </summary>
        public void ClearListeners()
        {
            this.eventSource.ClearListeners();
        }

        /// <summary>
        /// 广播属性事件
        /// </summary>
        /// <param name="evt">属性变化消息体</param>
        public void FireEvent(PropertyEvent evt)
        {
            this.eventSource.FireEvent(evt);
        }

        /// <summary>
        /// 广播属性事件
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public void FireEvent(string name, object oldValue, object newValue)
        {
            PropertyEvent pe = new PropertyEvent();
            pe.EventName = name;
            pe.OldValue = oldValue;
            pe.NewValue = newValue;
            this.eventSource.FireEvent(pe);
        }

        /// <summary>
        /// 广播方法事件
        /// </summary>
        /// <param name="evt">方法调用消息体</param>
        public void FireEvent(MethodEvent evt)
        {
            this.eventSource.FireEvent(evt);
        }

        /// <summary>
        /// 广播方法事件
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="args">参数列表</param>
        public void FireEvent(string name, object[] args)
        {
            MethodEvent me = new MethodEvent();
            me.EventName = name;
            if (args != null)
            {
                me.Argments = new object[args.Length];
                args.CopyTo(me.Argments, 0);
            }
            else
            {
                me.Argments = new object[0];
            }
            this.eventSource.FireEvent(me);
        }

        /// <summary>
        /// 关闭广播事件功能
        /// </summary>
        public void OffFire()
        {
            this.eventSource.OffFire();
        }

        /// <summary>
        /// 打开广播事件功能
        /// </summary>
        public void OnFire()
        {
            this.eventSource.OnFire();
        }

        /// <summary>
        /// 删除事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        public void RemoveEventListener(IEventListener listener)
        {
            this.eventSource.RemoveEventListener(listener);
        }

        /// <summary>
        /// 强制发送所有属性事件
        /// </summary>
        public void ForceFireAll()
        {
            //广播初始化事件，在_eventSource中搜索所有带有
            //“FireInitEvent”注解的属性和字段
            //并广播需要广播初始化消息的属性或字段事件
            var allFields = this
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(FireInitEventAttribute), true).Length > 0)
                .ToArray();
            var allProperties = this
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(FireInitEventAttribute), true).Length > 0)
                .ToArray();

            //向Listener广播字段初始化消息
            foreach (FieldInfo info in allFields)
            {
                var value = info.GetValue(this);
                var attr = info.GetCustomAttribute<FireInitEventAttribute>();
                this.FireEvent(info.Name, value, value);
            }

            //向Listener广播初属性始化消息
            foreach (PropertyInfo info in allProperties)
            {
                var value = info.GetValue(this);
                var attr = info.GetCustomAttribute<FireInitEventAttribute>();
                this.FireEvent(info.Name, value, value);
            }
        }

        /// <summary>
        /// 从另外一个对象深拷贝内容并保存到本对象中
        /// </summary>
        /// <param name="seri">可序列化对象</param>
        public virtual void Copy(ISerializable other)
        {
            if (this.GetType() != other.GetType())
            {
                throw new Buskit3DException("EventSupportBehaviour#Copy[type inconsistency]");
            }
            object temp = this;

            var allFields = temp
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            var allProperties = temp
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            foreach (FieldInfo info in allFields)
            {
                var value = info.GetValue(temp);
                FieldInfo fieldInfo = this.GetType().GetField(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(this, value);
            }

            foreach (PropertyInfo info in allProperties)
            {
                var value = info.GetValue(temp);
                PropertyInfo propertyInfo = this.GetType().GetProperty(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(this, value);
            }
        }

        /// <summary>
        /// 将源对象属性值拷贝到目标对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        public virtual void Copy(ISerializable source, ISerializable target)
        {
            if (source.GetType() != target.GetType())
            {
                throw new Buskit3DException("EventSupportBehaviour#Copy[type inconsistency]");
            }

            object temp = this;

            var allFields = temp
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            var allProperties = temp
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            foreach (FieldInfo info in allFields)
            {
                var value = info.GetValue(temp);
                FieldInfo fieldInfo = target.GetType().GetField(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(target, value);
            }

            foreach (PropertyInfo info in allProperties)
            {
                var value = info.GetValue(temp);
                PropertyInfo propertyInfo = target.GetType().GetProperty(info.Name,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(target, value);
            }
        }

        /// <summary>
        /// 拷贝自身对象并返回对象
        /// </summary>
        /// <returns>ISerializable对象</returns>
        public virtual ISerializable Clone()
        {
            return null;
        }

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>JObject</returns>
        public virtual JObject ToJson()
        {
            object temp = this;
            JObject jo = new JObject();

            UniqueID uid = GetComponent<UniqueID>();

            if(uid == null)
            {
                throw new Buskit3DException("EventSupportBehaviour:ToJson[Failed to find the specified field]");
            }

            //序列化ID号
            jo["Id"] = uid.UniqueId;

            //序列化类型
            jo["ClassType"] = this.GetType().AssemblyQualifiedName;

            //获取所有字段
            var allFields = temp
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            //获取所有属性
            var allProperties = temp
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToArray();

            //序列化所有字段
            JArray joFieldArray = new JArray();
            foreach (FieldInfo info in allFields)
            {
                /*
                //仅仅序列化带有JsonPropertyAttribute的字段
                if (info.GetCustomAttribute<JsonPropertyAttribute>() == null)
                {
                    continue;
                }
                */

                string fieldName = info.Name;
                var fieldValue = info.GetValue(temp);
                Type filedType = info.FieldType;

                JObject joField = new JObject();
                joField["Name"] = fieldName;
                joField["ClassType"] = filedType.AssemblyQualifiedName;

                if (fieldValue == null)
                {
                    joField["Value"] = "null";
                }
                else
                {
                    try
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        joField["Value"] = JsonConvert.SerializeObject(fieldValue, Formatting.Indented, settings);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }

                joFieldArray.Add(joField);
            }
            jo["Fields"] = joFieldArray;

            //序列化所有属性
            JArray joPropertyArray = new JArray();
            foreach (PropertyInfo info in allProperties)
            {
                /*
                //仅仅序列化带有JsonPropertyAttribute的属性
                if (info.GetCustomAttribute<JsonPropertyAttribute>() == null)
                {
                    continue;
                }
                */

                var value = info.GetValue(temp);
                string propertyName = info.Name;
                var propertyValue = info.GetValue(temp);
                Type propertyType = info.PropertyType;

                JObject joProperty = new JObject();
                joProperty["Name"] = propertyName;
                joProperty["ClassType"] = propertyType.AssemblyQualifiedName;

                if (propertyValue == null)
                {
                    joProperty["Value"] = "null";
                }
                else
                {
                    try
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        joProperty["Value"] = JsonConvert.SerializeObject(propertyValue, Formatting.Indented, settings);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
                joPropertyArray.Add(joProperty);
            }
            jo["Properties"] = joPropertyArray;

            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public virtual void FromJson(JObject jo)
        {
            //获取字段序列化表
            JArray fieldArray = jo["Fields"] as JArray;
            //获取属性序列化表
            JArray propertyArray = jo["Properties"] as JArray;

            //反序列化字段
            foreach (JObject joField in fieldArray)
            {
                string strFieldName = joField["Name"].ToString();
                string strFieldType = joField["ClassType"].ToString();
                string strFieldValue = joField["Value"].ToString();

                FieldInfo fi = this.GetType().GetField(strFieldName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (fi == null)
                {
                    throw new Buskit3DException("EventSupportBehaviour#FromJson[Failed to find the specified field]");
                }
                Type fieldType = Type.GetType(strFieldType);
                if (fieldType == null)
                {
                    throw new Buskit3DException("EventSupportBehaviour#FromJson[Failed to find the specified field]");
                }

                //反序列化字段并设置值
                if (strFieldValue.Equals("null"))
                {
                    fi.SetValue(this, null);
                }
                else
                {
                    try
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        object deserialValue = JsonConvert.DeserializeObject(strFieldValue, fieldType,settings);
                        fi.SetValue(this, deserialValue);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            //反序列化属性
            foreach (JObject joProperty in propertyArray)
            {
                string strPropertyName = joProperty["Name"].ToString();
                string strPropertyType = joProperty["ClassType"].ToString();
                string strPropertyValue = joProperty["Value"].ToString();

                PropertyInfo pi = this.GetType().GetProperty(strPropertyName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (pi == null)
                {
                    throw new Buskit3DException("EventSupportBehaviour#FromJson[Failed to find the specified field]");
                }
                Type propertyType = Type.GetType(strPropertyType);
                if (propertyType == null)
                {
                    throw new Buskit3DException("EventSupportBehaviour#FromJson[Failed to find the specified field]");
                }

                //反序列化属性并设置值
                if (strPropertyValue.Equals("null"))
                {
                    pi.SetValue(this, null);
                }
                else
                {
                    try
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        object deserialValue = JsonConvert.DeserializeObject(strPropertyValue, propertyType,settings);
                        pi.SetValue(this, deserialValue);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
}
