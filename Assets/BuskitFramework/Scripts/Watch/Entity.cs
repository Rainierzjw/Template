/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：Entity
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 实体对象
    /// </summary>
    public class Entity : EObject, ISerializable
    {
        /// <summary>
        /// GameObject唯一ID号
        /// </summary>
        [FireInitEvent]
        public int Id
        {
            set
            {
                var oldValue = _id;
                this._id = value;
                this.FireEvent("Id", oldValue, _id);
            }
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// GameObject唯一ID号
        /// </summary>
        protected int _id = -1;


        /// <summary>
        /// 从另外一个对象深拷贝内容并保存到本对象中
        /// </summary>
        /// <param name="seri">可序列化对象</param>
        public virtual void Copy(ISerializable other)
        {
            if (this.GetType() != other.GetType())
            {
                throw new Buskit3DException("Entity#Copy[type inconsistency]");
            }
            JObject jo = other.ToJson();
            this.FromJson(jo);
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
                throw new Buskit3DException("Entity#Copy[type inconsistency]");
            }

            JObject jo = source.ToJson();
            target.FromJson(jo);
        }

        /// <summary>
        /// 拷贝自身对象并返回对象
        /// </summary>
        /// <returns>ISerializable对象</returns>
        public virtual ISerializable Clone()
        {
            Entity newEntity = Activator.CreateInstance(this.GetType()) as Entity;
            JObject jo = ToJson();
            newEntity.FromJson(jo);
            return newEntity;
        }

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>JObject</returns>
        public virtual JObject ToJson()
        {
            object temp = this;
            JObject jo = new JObject();

            //序列化ID号
            jo["Id"] = this.Id;

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
                    throw new Buskit3DException("Entity#FromJson[Failed to find the specified field]");
                }
                Type fieldType = Type.GetType(strFieldType);
                if (fieldType == null)
                {
                    throw new Buskit3DException("Entity#FromJson[Failed to find the specified field]");
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
                        object deserialValue = JsonConvert.DeserializeObject(strFieldValue, fieldType, settings);
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
                    throw new Buskit3DException("Entity#FromJson[Failed to find the specified field]");
                }
                Type propertyType = Type.GetType(strPropertyType);
                if (propertyType == null)
                {
                    throw new Buskit3DException("Entity#FromJson[Failed to find the specified field]");
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
                        object deserialValue = JsonConvert.DeserializeObject(strPropertyValue, propertyType, settings);
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
