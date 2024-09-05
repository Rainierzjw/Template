/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： OwvlabRequest
* 创建日期：2021-11-18 11:06:19
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{
    /// <summary>
    /// http 请求包装
    /// </summary>
	public class OwvlabRequest 
	{
        //执行携程的Mono
        public MonoBehaviour _mono;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mono">behaviour</param>
        public OwvlabRequest(MonoBehaviour mono)
        {
            _mono = mono;
        }

        /// <summary>
        /// wwwform 格式请求
        /// </summary>
        /// <param name="form"></param>
        public void Post(string url, WWWForm form, System.Action<string> callback = null)
        {
            IEnumerator Send()
            {
                UnityWebRequest req = UnityWebRequest.Post(url, form);
                req.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                var op = req.SendWebRequest();
                
                yield return op;
              
                if (req.isNetworkError || req.isHttpError)
                {
                    Debug.Log(req.error);
                }
                else
                {
                    Debug.Log(op.webRequest.downloadHandler.text);
                }
                if (callback != null)
                {
                    callback.Invoke(op.webRequest.downloadHandler.text);
                }
            }

            _mono.StartCoroutine(Send());
        }

        public void Post(string url, JObject param,System.Action<string> callback=null)
        {
            IEnumerator Send()
            {
                Debug.Log(url);
                Debug.Log(param.ToString(Newtonsoft.Json.Formatting.None));
                UnityWebRequest req = UnityWebRequest.PostWwwForm(url, param.ToString(Newtonsoft.Json.Formatting.None));
                req.SetRequestHeader("Content-Type", "Application/json;charset=UTF-8");
                var op = req.SendWebRequest();
                yield return op;
                if (req.isNetworkError || req.isHttpError)
                {
                    Debug.Log(req.error);
                }
                else
                {
                    Debug.Log(op.webRequest.downloadHandler.text);
                }
                if (callback != null)
                {
                    callback.Invoke(op.webRequest.downloadHandler.text);
                }
            }
            _mono.StartCoroutine(Send());
        }
        public void Get(string url,System.Action<string> callback)
        {

            IEnumerator Get()
            {
                UnityWebRequest req = UnityWebRequest.Get(url);
                var op = req.SendWebRequest();
                yield return op;
                if (req.isNetworkError || req.isHttpError)
                {
                    Debug.Log(req.error);
                }
                else
                {
                    Debug.Log(op.webRequest.downloadHandler.text);
                }
                if (callback != null)
                {
                    callback(op.webRequest.downloadHandler.text);
                }
            }
            _mono.StartCoroutine(Get());
        }       
	}
}

