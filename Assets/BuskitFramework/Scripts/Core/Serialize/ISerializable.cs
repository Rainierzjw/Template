/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：ISerializable
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：可序列化对象描述接口
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using Newtonsoft.Json.Linq;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 可序列化对象描述接口
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// 从另外一个对象深拷贝内容并保存到本对象中
        /// </summary>
        /// <param name="other">可序列化对象</param>
        void Copy(ISerializable other);

        /// <summary>
        /// 将源对象属性值拷贝到目标对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        void Copy(ISerializable source, ISerializable target);

        /// <summary>
        /// 拷贝自身对象并返回对象
        /// </summary>
        /// <returns>实现了ISerializable的对象</returns>
        ISerializable Clone();

        /// <summary>
        /// 序列化为JSON对象
        /// </summary>
        /// <returns>JObject</returns>
        JObject ToJson();

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        void FromJson(JObject obj);
    }
}
