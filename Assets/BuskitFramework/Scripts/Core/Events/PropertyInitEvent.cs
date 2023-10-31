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

using Newtonsoft.Json.Linq;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 属性初始化事件
    /// </summary>
    public class PropertyInitEvent : PropertyEvent
    {

        /// <summary>
        /// 拷贝自身对象并返回对象，执行浅复制
        /// </summary>
        /// <returns>实现了ISerializable的对象</returns>
        public override ISerializable Clone()
        {
            PropertyInitEvent evt = new PropertyInitEvent();
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
            JObject jo = base.ToJson();
            jo["EventType"] = this.GetType().ToString();    //事件类型
            return jo;
        }

        /// <summary>
        /// 从JSON对象中读取数据内容
        /// </summary>
        /// <param name="obj">JObject</param>
        public override void FromJson(JObject obj)
        {
            obj["EventType"] = this.GetType().ToString();    //事件类型
            base.FromJson(obj);
        }
    }
}
