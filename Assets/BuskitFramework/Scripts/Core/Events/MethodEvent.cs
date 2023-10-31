/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：MethodEvent
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：方法事件
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 方法事件
    /// </summary>
    public class MethodEvent : AbstractEvent
    {
        /// <summary>
        /// 方法调用参数列表
        /// </summary>
        public object[] Argments{ set; get;}

        /// <summary>
        /// 拷贝自身对象并返回对象，执行浅复制
        /// </summary>
        /// <returns>ISerializable对象</returns>
        public override ISerializable Clone()
        {
            MethodEvent evt = new MethodEvent();
            evt.Argments = this.Argments;
            evt.EventName = this.EventName;
            evt.eventSource = this.eventSource;
            return evt;
        }

        /// <summary>
        /// 回调源对象函数
        /// </summary>
        public override void InokeEventSource()
        {

            MethodInfo mi = this.eventSource.GetType().GetMethod(this.eventName, 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if(mi != null)
            {
                mi.Invoke(this.eventSource, this.Argments);
            }
        }

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>JObject</returns>
        public override JObject ToJson()
        {
            //序列化事件源和时间名称
            JObject jo = base.ToJson();

            //序列化事件参数
            jo["Argments"] = new JArray();
            //便利参数集合
            foreach (object obj in Argments)
            {
                JObject joArg = new JObject();
                if (obj == null)
                {
                    (jo["Argments"] as JArray).Add("null");
                    continue;
                }

                //如果事件参数是Entity则序列化参数和类型
                if (obj is Entity)
                {
                    joArg["ClassType"] = obj.GetType().AssemblyQualifiedName;
                    joArg["Type"] = "Entity";
                    joArg["Id"] = (obj as Entity).Id;

                    //场景中并没有哪个DataModel与此Entity关联，表示Entity当做一般对象使用
                    if ((obj as Entity).Id == -1)
                    {
                        ISerializable serializable = (ISerializable)obj;
                        joArg["Content"] = serializable.ToJson();
                    }
                }
                //如果事件参数是DataModelBehaviour则序列化参数和类型
                else if (obj is DataModelBehaviour)
                {
                    joArg["ClassType"] = obj.GetType().AssemblyQualifiedName;
                    joArg["Type"] = "DataModelBehaviour";
                    joArg["Id"] = (obj as DataModelBehaviour).Id;
                }
                //如果事件参数是GameObject则记录GameObject的静态ID
                else if (obj is GameObject)
                {
                    GameObject  go = (GameObject)obj;
                    UniqueID    ID = go.GetComponent<UniqueID>();

                    joArg["ClassType"] = go.GetType().AssemblyQualifiedName;
                    joArg["Type"] = "GameObject";
                    joArg["Id"] = ID.UniqueId;
                }
                //如果事件参数是MonoBehaviour则记录GameObject的静态ID及MonoBehavior类型
                else if (obj is Behaviour)
                {
                    Behaviour   behaviour = (Behaviour)obj;
                    UniqueID    ID = behaviour.gameObject.GetComponent<UniqueID>();
                    GameObject  go = behaviour.gameObject;

                    joArg["ClassType"] = behaviour.GetType().AssemblyQualifiedName;
                    joArg["Type"] = "Behaviour";
                    joArg["Id"] = ID.UniqueId;
                }
                //如果事件参数实现ISerializable则交给参数对象完成序列化
                else if (obj is ISerializable)
                {
                    joArg["ClassType"] = obj.GetType().AssemblyQualifiedName;
                    joArg["Type"] = "Serializable";
                    ISerializable serializable = (ISerializable)obj;
                    joArg["Content"] = serializable.ToJson();
                }
                //如果事件参数不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
                else
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    string json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
                    joArg["ClassType"] = obj.GetType().AssemblyQualifiedName;
                    joArg["Type"] = "Other";
                    try
                    {
                        joArg["Content"] = JObject.Parse(json);
                    }
                    catch (Exception e)
                    {
                        joArg["Content"] = json;
                    }
                }

                //保存参数对象
                (jo["Argments"] as JArray).Add(joArg);
            }
            
            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public override void FromJson(JObject obj)
        {
            //反序列化事件源和事件名称
            base.FromJson(obj);

            //DataModel对象池
            DataModelPool pool = InjectService.Get<DataModelPool>();

            //GameObject对象池
            GameObjectPool goPool = InjectService.Get<GameObjectPool>();

            JArray joArgs = obj["Argments"] as JArray;
            object[] args = new object[joArgs.Count];
            this.Argments = args;
            for(int i=0; i< joArgs.Count; i++ )
            {
                if (joArgs[i].ToString().Equals("null"))
                {
                    args[i] = null;
                    continue;
                }

                JObject joArg = joArgs[i] as JObject;

                //如果事件源是Entity则序列化参数和类型
                string strJoArgClassType = joArg["ClassType"].ToString();
                string strJoArgType = joArg["Type"].ToString();
                //如果事件源是Entity则序列化参数和类型
                if (strJoArgType.Equals("Entity"))
                {
                    int id = int.Parse(joArg["Id"].ToString());
                    Type type = Type.GetType(strJoArgClassType);
                    Entity entity = pool.FindEntity(id, type);
                    if(entity != null)
                    {
                        args[i] = entity;
                    }
                    else
                    {
                        string strContent = joArg["Content"].ToString();
                        /*
                        object desrialObj = JsonConvert.DeserializeObject(
                             strContent, type);
                        (desrialObj as ISerializable).FromJson(joArg["Content"] as JObject);
                        */
                        object desrialObj = Activator.CreateInstance(type);
                        (desrialObj as ISerializable).FromJson(joArg["Content"] as JObject);
                        args[i] = desrialObj;
                    }
                }
                //如果事件源是DataModelBehaviour则序列化参数和类型
                else if (strJoArgType.Equals("DataModelBehaviour"))
                {
                    int id = int.Parse(joArg["Id"].ToString());
                    Type type = Type.GetType(strJoArgClassType);
                    DataModelBehaviour dm = pool.FindDataModel(id);
                    args[i] = dm;
                }
                //如果参数类型是GameObject则在场景中查找相应ID的GameObject
                else if (strJoArgType.Equals("GameObject"))
                {
                    string uniqueId = joArg["Id"].ToString();
                    GameObjectPool gp = InjectService.Get<GameObjectPool>();
                    GameObject go = gp.FindGameObject(int.Parse(uniqueId));
                    args[i] = go;
                }
                //如果参数类型是Behaviour则在场景中查找相应ID的GameObject和相应类型的Behaviour
                else if (strJoArgType.Equals("Behaviour"))
                {
                    string uniqueId = joArg["Id"].ToString();
                    GameObjectPool gp = InjectService.Get<GameObjectPool>();
                    GameObject go = gp.FindGameObject(int.Parse(uniqueId));
                    Type behaviourType = Type.GetType(strJoArgClassType);
                    Behaviour behaviour = go.GetComponent(behaviourType) as Behaviour;
                    args[i] = behaviour;
                }
                //如果事件源实现ISerializable则交给参数对象完成序列化
                else if (strJoArgType.Equals("Serializable"))
                {
                    Type type = Type.GetType(strJoArgClassType);
                    string strContent = joArg["Content"].ToString();
                    /*
                    object desrialObj = JsonConvert.DeserializeObject(
                         strContent, Type.GetType(strJoArgClassType));
                    (desrialObj as ISerializable).FromJson(joArg["Content"] as JObject);
                    */
                    object desrialObj = Activator.CreateInstance(Type.GetType(strJoArgClassType));
                    (desrialObj as ISerializable).FromJson(joArg["Content"] as JObject);
                    args[i] = desrialObj;
                }
                //如果事件源不是Entity、DataModelBehaviour、ISerializable则使用JsonConvert自动序列化
                else
                {
                    string strContent = joArg["Content"].ToString();
                    object desrialObj = JsonConvert.DeserializeObject(
                        strContent, Type.GetType(strJoArgClassType));
                    if(desrialObj != null)
                    {
                        args[i] = desrialObj;
                    }
                }
            }
        }
    }
}

