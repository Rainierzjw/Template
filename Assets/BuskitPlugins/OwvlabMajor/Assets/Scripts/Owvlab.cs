/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： OwvlabMajor
* 创建日期：2021-11-18 10:31:02
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.UI;
using System.Web;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{
    /// <summary>
    /// 长期维护版本，在此插件基础进行更新迭代
    /// Owvlab 初始化上下文（需调用Init），进行Js交互，执行携程请求
    /// 需要以组件形式挂载至 GameObject
    /// </summary>
    public class Owvlab : MonoBehaviour
    {
        public static string appId;
        public static string expId;
        public static string userCode;
        public static string userName;
        public static string host;

        /// <summary>
        /// 初始化数据交互 ,支持 WebGL 和云渲染
        /// </summary>
        /// <param name="mono">monobehaviour,生命周期从实验开始到实验结束</param>
        /// <returns>owvlab 上下文</returns>
        public OwvlabContext Init(
#if UNITY_STANDALONE
            string host
#endif
        )
        {
            OwvlabContext context = new OwvlabContext(this);

#if UNITY_STANDALONE
            Owvlab.host = host;
            string sequenceCode = GetCommandArgs();
            context.LoginIn(sequenceCode, PopUpExpWindow);
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalCall("getBaseInfo", this.gameObject.name, "WebGL_JsCallback");
#endif
            return context;
        }

        /// <summary>
        /// 初始化数据交互 ,PC 专用
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="appId">平台获取填写</param>
        /// <returns></returns>
        public OwvlabContext Init(string host, string appId, string account, string passward, Action<string> action)
        {
            Owvlab.appId = appId;
            Owvlab.host = host;
            OwvlabContext context = new OwvlabContext(this);
            //context.LoginIn(account, passward, PopUpExpWindow);
            context.LoginIn(account, passward, action);
            return context;
        }

        /// <summary>
        /// Owvlab Js 回调，获取当前实验相关信息
        /// </summary>
        /// <param name="info"></param>
        public void WebGL_JsCallback(string info)
        {
            JObject userInfo = JObject.Parse(info);
            appId = userInfo["appId"].ToString();
            expId = userInfo["expId"].ToString();
            userCode = userInfo["userCode"].ToString();
            userName = userInfo["userName"].ToString();
            host = userInfo["host"].ToString();
            Debug.Log(string.Format("WebGL_JsCallback success: appId={0},expId={1},userCode={2},userName={3},host={4}",
                appId, expId, userCode, userName, host));
        }

        /// <summary>
        /// 云渲染版获取应用程序启动参数
        /// </summary>
        /// <returns>sequenceCode</returns>
        private string GetCommandArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            string sequenceCode = "";
            foreach (var item in args)
            {
                if (item.StartsWith("serialNumber"))
                {
                    // string[] str = HttpUtility.UrlDecode(item.Split('=')[1]).Split('|');
                    // Owvlab.appId = str[0];
                    // sequenceCode = str[1];
                }
            }

            return sequenceCode;
        }

        /// <summary>
        /// Pc 云渲染认证成功，显示实验选项列表
        /// </summary>
        /// <param name="str">认证响应数据</param>
        private void PopUpExpWindow(string str)
        {
            JObject jo = JObject.Parse(str);

            var error = jo["error"].ToObject<string>();
            if (error == "" || error == null)
            {
                JArray exps = jo["expList"].ToObject<JArray>();

                GameObject canvas = Instantiate(Resources.Load<GameObject>("OwvlabMajorWindow"));
                Transform content = canvas.transform.Find("ScrollView/Viewport/Content");

                foreach (var item in exps)
                {
                    GameObject _item = Instantiate(Resources.Load<GameObject>("OwvlabMajorExpItem"));
                    _item.transform.SetParent(content);
                    _item.transform.SetAsLastSibling();
                    _item.transform.Find("expId").GetComponent<Text>().text = item["expId"].ToString();
                    _item.transform.Find("expName").GetComponent<Text>().text = item["expName"].ToString();
                    _item.transform.Find("expScore").GetComponent<Text>().text = item["expScore"].ToString();
                    _item.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        string expId = item["expId"].ToString();
                        Owvlab.expId = expId;
                        Destroy(canvas);
                    });
                }
            }
        }
    }
}