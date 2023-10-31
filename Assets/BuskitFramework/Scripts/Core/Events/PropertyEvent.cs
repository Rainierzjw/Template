/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：PropertyEvent
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;
using System.Reflection;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 属性事件
    /// </summary>
    public class PropertyEvent : AbstractEvent
    {
        /// <summary>
        /// 属性老值
        /// </summary>
        public object OldValue { set; get; }

        /// <summary>
        /// 属性新值
        /// </summary>
        public object NewValue { set; get; }

        /// <summary>
        /// 回调事件源属性
        /// </summary>
        public override void InokeEventSource()
        {

            PropertyInfo pi = this.eventSource.GetType().GetProperty(this.eventName, 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if(pi != null)
            {
                pi.SetValue(this.eventSource, this.NewValue);
                return;
            }

            FieldInfo fi = this.eventSource.GetType().GetField(this.eventName, 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fi != null)
            {
                fi.SetValue(this.eventSource, this.NewValue);
                return;
            }
        }

        /// <summary>
        /// 拷贝自身对象并返回对象，执行浅复制
        /// </summary>
        /// <returns>实现了ISerializable的对象</returns>
        public override ISerializable Clone()
        {
            PropertyEvent evt = new PropertyEvent();
            evt.OldValue = this.OldValue;
            evt.EventName = this.EventName;
            evt.eventSource = this.eventSource;
            evt.NewValue = this.NewValue;
            return evt;
        }

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>JObject</returns>
        public override JObject ToJson()
        {
            //序列化事件源和时间名称
            JObject jo = base.ToJson();
            jo["OldValue"] = new JObject();
            jo["NewValue"] = new JObject();

            if(OldValue == null)
            {
                jo["OldValue"] = "null";
            }
            //如果事件老值是Entity则序列化参数和类型
            else if ((OldValue is Entity))
            {
                jo["OldValue"]["ClassType"] = OldValue.GetType().AssemblyQualifiedName;
                jo["OldValue"]["Type"] = "Entity";
                jo["OldValue"]["Id"] = (OldValue as Entity).Id;

                //场景中并没有哪个DataModel与此Entity关联，表示Entity当做一般对象使用
                if ((OldValue as Entity).Id == -1)
                {
                    ISerializable serializable = (ISerializable)OldValue;
                    jo["OldValue"]["Content"] = serializable.ToJson();
                }
            }
            //如果事件老值是DataModelBehaviour则序列化参数和类型
            else if (OldValue is DataModelBehaviour)
            {
                jo["OldValue"]["ClassType"] = OldValue.GetType().AssemblyQualifiedName;
                jo["OldValue"]["Type"] = "DataModelBehaviour";
                jo["OldValue"]["Id"] = (OldValue as DataModelBehaviour).Id;
            }
            //如果事件老值是GameObject则记录GameObject的静态ID
            else if (OldValue is GameObject)
            {
                GameObject go = (GameObject)OldValue;
                UniqueID ID = go.GetComponent<UniqueID>();

                jo["OldValue"]["ClassType"] = go.GetType().AssemblyQualifiedName;
                jo["OldValue"]["Type"] = "GameObject";
                jo["OldValue"]["Id"] = ID.UniqueId;
            }
            //如果事件老值是MonoBehaviour则记录GameObject的静态ID及MonoBehavior类型
            else if (OldValue is Behaviour)
            {
                Behaviour behaviour = (Behaviour)OldValue;
                UniqueID ID = behaviour.gameObject.GetComponent<UniqueID>();
                GameObject go = behaviour.gameObject;

                jo["OldValue"]["ClassType"] = behaviour.GetType().AssemblyQualifiedName;
                jo["OldValue"]["Type"] = "Behaviour";
                jo["OldValue"]["Id"] = ID.UniqueId;
            }
            //如果事件老值实现ISerializable则交给参数对象完成序列化
            else if (OldValue is ISerializable)
            {
                jo["OldValue"]["ClassType"] = OldValue.GetType().AssemblyQualifiedName;
                jo["OldValue"]["Type"] = "Serializable";
                ISerializable serializable = (ISerializable)OldValue;
                jo["OldValue"]["Content"] = serializable.ToJson();
            }
            //如果事件老值不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string json = JsonConvert.SerializeObject(OldValue, Formatting.Indented, settings);
                jo["OldValue"]["ClassType"] = OldValue.GetType().AssemblyQualifiedName;
                jo["OldValue"]["Type"] = "Other";
                try
                {
                    jo["OldValue"]["Content"] = JObject.Parse(json);
                }
                catch (Exception e)
                {
                    jo["OldValue"]["Content"] = json;
                }
            }

            if (NewValue == null)
            {
                jo["NewValue"] = "null";
            }
            //如果事件新值是Entity则序列化参数和类型
            else if ((NewValue is Entity))
            {
                jo["NewValue"]["ClassType"] = NewValue.GetType().AssemblyQualifiedName;
                jo["NewValue"]["Type"] = "Entity";
                jo["NewValue"]["Id"] = (NewValue as Entity).Id;

                //场景中并没有哪个DataModel与此Entity关联，表示Entity当做一般对象使用
                if ((NewValue as Entity).Id == -1)
                {
                    ISerializable serializable = (ISerializable)NewValue;
                    jo["NewValue"]["Content"] = serializable.ToJson();
                }
            }
            //如果事件新值是DataModelBehaviour则序列化参数和类型
            else if (NewValue is DataModelBehaviour)
            {
                jo["NewValue"]["ClassType"] = NewValue.GetType().AssemblyQualifiedName;
                jo["NewValue"]["Type"] = "DataModelBehaviour";
                jo["NewValue"]["Id"] = (NewValue as DataModelBehaviour).Id;
            }
            //如果事件新值是GameObject则记录GameObject的静态ID
            else if (NewValue is GameObject)
            {
                GameObject go = (GameObject)NewValue;
                UniqueID ID = go.GetComponent<UniqueID>();

                jo["NewValue"]["ClassType"] = go.GetType().AssemblyQualifiedName;
                jo["NewValue"]["Type"] = "GameObject";
                jo["NewValue"]["Id"] = ID.UniqueId;
            }
            //如果事件新值是MonoBehaviour则记录GameObject的静态ID及MonoBehavior类型
            else if (NewValue is Behaviour)
            {
                Behaviour behaviour = (Behaviour)NewValue;
                UniqueID ID = behaviour.gameObject.GetComponent<UniqueID>();
                GameObject go = behaviour.gameObject;

                jo["NewValue"]["ClassType"] = behaviour.GetType().AssemblyQualifiedName;
                jo["NewValue"]["Type"] = "Behaviour";
                jo["NewValue"]["Id"] = ID.UniqueId;
            }
            //如果事件新值实现ISerializable则交给参数对象完成序列化
            else if (NewValue is ISerializable)
            {
                jo["NewValue"]["ClassType"] = NewValue.GetType().AssemblyQualifiedName;
                jo["NewValue"]["Type"] = "Serializable";
                ISerializable serializable = (ISerializable)NewValue;
                jo["NewValue"]["Content"] = serializable.ToJson();
            }
            //如果事件新值不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                string json = JsonConvert.SerializeObject(NewValue, Formatting.Indented, settings);
                jo["NewValue"]["ClassType"] = NewValue.GetType().AssemblyQualifiedName;
                jo["NewValue"]["Type"] = "Other";
                try
                {
                    jo["NewValue"]["Content"] = JObject.Parse(json);
                }
                catch (Exception e)
                {
                    jo["NewValue"]["Content"] = json;
                }
            }

            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public override void FromJson(JObject obj)
        {
            //基类还原EventSource和EventName
            base.FromJson(obj);

            //DataModel对象池
            DataModelPool pool = InjectService.Get<DataModelPool>();
            //GameObject对象池
            GameObjectPool goPool = InjectService.Get<GameObjectPool>();

            //反序列化老值
            JObject joOldValue = obj["OldValue"] as JObject;

            //如果序列化对象为空，则对OldValue设置为null
            if (joOldValue == null)
            {
                this.OldValue = null;
            }
            //如果序列化对象内容为空，则对OldValue设置为null
            else if (joOldValue.ToString().Equals("null")){
                this.OldValue = null;
            }
            //如果序列化对象不为空且内容不为空，则对OldValue执行正常的序列化过程
            else 
            {
                string strJoOldValueClassType = joOldValue["ClassType"].ToString();
                string strJoOldValueType = joOldValue["Type"].ToString();
                //如果事件老值是Entity则序列化参数和类型
                if (strJoOldValueType.Equals("Entity"))
                {
                    int id = int.Parse(joOldValue["Id"].ToString());
                    Type type = Type.GetType(strJoOldValueClassType);
                    Entity entity = pool.FindEntity(id, type);
                    this.OldValue = entity;
                }
                //如果事件老值是DataModelBehaviour则序列化参数和类型
                else if (strJoOldValueType.Equals("DataModelBehaviour"))
                {
                    int id = int.Parse(joOldValue["Id"].ToString());
                    Type type = Type.GetType(strJoOldValueClassType);
                    DataModelBehaviour dm = pool.FindDataModel(id);
                    this.OldValue = dm;
                }
                //如果参数类型是GameObject则在场景中查找相应ID的GameObject
                else if (strJoOldValueType.Equals("GameObject"))
                {
                    string uniqueId = joOldValue["Id"].ToString();
                    GameObjectPool gp = InjectService.Get<GameObjectPool>();
                    GameObject go = gp.FindGameObject(int.Parse(uniqueId));
                    this.OldValue = go;
                }
                //如果参数类型是Behaviour则在场景中查找相应ID的GameObject和相应类型的Behaviour
                else if (strJoOldValueType.Equals("Behaviour"))
                {
                    string uniqueId = joOldValue["Id"].ToString();
                    GameObjectPool gp = InjectService.Get<GameObjectPool>();
                    GameObject go = gp.FindGameObject(int.Parse(uniqueId));
                    Type behaviourType = Type.GetType(strJoOldValueClassType);
                    Behaviour behaviour = go.GetComponent(behaviourType) as Behaviour;
                    this.OldValue = behaviour;
                }
                //如果事件老值实现ISerializable则交给参数对象完成序列化
                else if (strJoOldValueType.Equals("Serializable"))
                {
                    Type type = Type.GetType(strJoOldValueClassType);
                    ISerializable deserialObj = Activator.CreateInstance(type) as ISerializable;
                    deserialObj.FromJson(joOldValue["Content"] as JObject);
                    this.OldValue = deserialObj;
                }
                //如果事件老值不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
                else
                {
                    Type type = Type.GetType(strJoOldValueClassType);
                    string json = "";
                    try
                    {
                        json = (joOldValue["Content"] as JObject).ToString();
                    }
                    catch (Exception e)
                    {
                        json = joOldValue["Content"].ToString();
                    }
                    if (json.Equals(""))
                    {
                        throw new Buskit3DException("PropertyEvent#FromJson:[Json Error]");
                    }
                    object desrialObj = JsonConvert.DeserializeObject(json, type);
                    this.OldValue = desrialObj;
                }

            }

            //反序列化新值
            JObject joNewValue = obj["NewValue"] as JObject;
            //如果序列化对象为空，则对NewValue设置为null
            if (joNewValue == null)
            {
                this.NewValue = null;
            }
            //如果序列化对象内容为空，则对NewValue设置为null
            else if (joNewValue.ToString().Equals("null"))
            {
                this.NewValue = null;
            }
            //如果序列化对象不为空且内容不为空，则对NewValue执行正常的序列化过程
            else
            {
                if (joNewValue["ClassType"] == null)
                {
                    return;
                }
                string strJoNewValueClassType = joNewValue["ClassType"].ToString();
                string strJoNewValueType = joNewValue["Type"].ToString();
                //如果事件新值是Entity则序列化参数和类型
                if (strJoNewValueType.Equals("Entity"))
                {
                    int id = int.Parse(joNewValue["Id"].ToString());
                    Type type = Type.GetType(strJoNewValueClassType);
                    Entity entity = pool.FindEntity(id, type);
                    this.NewValue = entity;
                }
                //如果事件新值是DataModelBehaviour则序列化参数和类型
                else if (strJoNewValueType.Equals("DataModelBehaviour"))
                {
                    int id = int.Parse(joNewValue["Id"].ToString());
                    Type type = Type.GetType(strJoNewValueClassType);
                    DataModelBehaviour dm = pool.FindDataModel(id);
                    this.NewValue = dm;
                }
                //如果参数类型是GameObject则在场景中查找相应ID的GameObject
                else if (strJoNewValueType.Equals("GameObject"))
                {
                    string uniqueId = joNewValue["Id"].ToString();
                    GameObjectPool gp = InjectService.Get<GameObjectPool>();
                    GameObject go = gp.FindGameObject(int.Parse(uniqueId));
                    this.NewValue = go;
                }
                //如果参数类型是Behaviour则在场景中查找相应ID的GameObject和相应类型的Behaviour
                else if (strJoNewValueType.Equals("Behaviour"))
                {
                    string uniqueId = joNewValue["Id"].ToString();
                    GameObjectPool gp = InjectService.Get<GameObjectPool>();
                    GameObject go = gp.FindGameObject(int.Parse(uniqueId));
                    Type behaviourType = Type.GetType(strJoNewValueClassType);
                    Behaviour behaviour = go.GetComponent(behaviourType) as Behaviour;
                    this.NewValue = behaviour;
                }
                //如果事件新值实现ISerializable则交给参数对象完成序列化
                else if (strJoNewValueType.Equals("Serializable"))
                {
                    Type type = Type.GetType(strJoNewValueClassType);
                    ISerializable deserialObj = Activator.CreateInstance(type) as ISerializable;
                    deserialObj.FromJson(joNewValue["Content"] as JObject);
                    this.NewValue = deserialObj;
                }
                //如果事件新值不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
                else
                {
                    Type type = Type.GetType(strJoNewValueClassType);
                    string json = "";
                    try
                    {
                        json = (joNewValue["Content"] as JObject).ToString();
                    }
                    catch (Exception e)
                    {
                        json = joNewValue["Content"].ToString();
                    }
                    if (json.Equals(""))
                    {
                        throw new Buskit3DException("PropertyEvent#FromJson:[Json Error]");
                    }
                    object desrialObj = JsonConvert.DeserializeObject(json, type);
                    this.NewValue = desrialObj;
                }
            }
        }
    }
}

