/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SendDataToWebManager
* 创建日期：2022-07-8 11:24:36
* 作者名称：武延乐
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using Com.Rainier.Buskit3D.OwvlabMajor;
using System;
using System.Web;
using Misc;

namespace Com.HIEU.ZHTCC
{
    /// <summary>
    /// 
    /// </summary>
	public class SendDataToNewPlatform : MonoSingleton<SendDataToNewPlatform>
    {
        /// <summary>
        /// 云渲染用，webgl不管
        /// </summary>
        public string URL = "http://open-set.owvlab.com";
        #region 数据参数
        private string appId;
        private string expId;
        private string host;
        private string userCode;
        private string sequenceCode;
        private string userName;
        private string expName;
        private string expScore;
        #endregion
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime startTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime endTime;
        /// <summary>
        /// 实验步骤内容
        /// </summary>
        private JArray joReportStepList = new JArray();
        /// <summary>
        /// 实验报告文字
        /// </summary>
        private JArray joReportTextList = new JArray();
        /// <summary>
        /// 实验报告图片
        /// </summary>
        private JArray joReportImageList = new JArray();
        /// <summary>
        /// 序号
        /// </summary>
        private int questionNumber = 1;
        /// <summary>
        /// Owvlab
        /// </summary>
        private OwvlabContext context;

        private string useraccout = "13716121151";
        private string password = "wu13716121151";

        private void Awake()
        {
            startTime = DateTime.Now;
            this.gameObject.name = "Owvlab";
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            //web获取userinfo
#if UNITY_WEBGL && !UNITY_EDITOR
			Application.ExternalCall("getBaseInfo", this.gameObject.name, "New_JsCallBack");
#endif
#if UNITY_STANDALONE || UNITY_EDITOR
            //云渲染对接
            context = new OwvlabContext(this);
            Owvlab.host = URL;
            host = URL;
            string sequenceCode = GetCommandArgs();
            context.LoginIn(sequenceCode, PopUpExpWindow);

            //PC对接
            /*appId = "53b9e595f7674db3a10c9440bb55179f";
			context = new OwvlabContext(this);
			Owvlab.host = URL;
			Owvlab.appId = appId;
			host = URL;
			context.LoginIn(useraccout, password, PopUpExpWindow);*/
#endif
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P))
            {
                SendDataToWeb();
            }
        }

        /// <summary>
        /// Js回调函数，获取登录用户信息，新平台
        /// </summary>
        /// <param name="jsonStr"></param>
        public void New_JsCallBack(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
            {
                Debug.LogError("网页返回用户信息为空");
            }
            else
            {
                JObject jstr = JObject.Parse(jsonStr);
                appId = jstr["appId"].ToString();
                expId = jstr["expId"].ToString();
                sequenceCode = jstr["sequenceCode"].ToString();
                userCode = jstr["userCode"].ToString();
                userName = jstr["userName"].ToString();
                host = jstr["host"].ToString();
                Debug.Log("获取到的用户信息：" + jstr);
            }
        }

        #region 公开函数

        /// <summary>
        /// 上传数据到web
        /// </summary>
        public void SendDataToWeb()
        {
            PatchStep();
            ChangeTime();
            RecordsReport();
            JObject jo = new JObject();
            jo["appId"] = appId;
            jo["expId"] = expId;
            jo["version"] = "1.0";
            jo["reportData"] = joReportTextList;
            jo["imageDetails"] = joReportImageList;
            jo["expScoreDetails"] = joReportStepList;
            Debug.Log("jo:" + jo.ToString());
#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(SendData(jo.ToString()));
#endif
        }

        /// <summary>
        /// 添加步骤
        /// </summary>
        /// <param name="moduleFlag">模块名</param>
        /// <param name="questionStemg">题干</param>
        /// <param name="score">得分</param>
        /// <param name="maxScore">步骤满分</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="scoringModel">赋分模型</param>
        public void RecordsStep(string moduleFlag, string questionStemg, double maxScore, double score, string scoringModel)
        {
            //已上传的步骤，只更新得分
            for (int i = 0; i < joReportStepList.Count; i++)
            {
                if (joReportStepList[i]["moduleFlag"].ToString() == moduleFlag && joReportStepList[i]["questionStem"].ToString() == questionStemg)
                {
                    Debug.Log(questionStemg + "：已上传过得分" + joReportStepList[i]["score"]);

                    //StepList[i]["endTime"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (score > 0)
                    {
                        joReportStepList[i]["score"] = score;
                        Debug.Log(questionStemg + "：更新得分" + score);
                    }

                    if (score < maxScore * 0.3)
                    {
                        joReportStepList[i]["evaluation"] = "差";
                    }
                    if ((maxScore * 0.3 <= score) && (score < maxScore * 0.7))
                    {
                        joReportStepList[i]["evaluation"] = "良";
                    }
                    if (maxScore * 0.7 <= score)
                    {
                        joReportStepList[i]["evaluation"] = "优秀";
                    }
                    return;
                }
            }
            Debug.Log(questionStemg + "-未上传过得分");
            JObject jo = new JObject();
            jo["moduleFlag"] = moduleFlag;
            jo["questionNumber"] = questionNumber;
            jo["questionStem"] = questionStemg;
            jo["score"] = score;
            jo["trueOrFalse"] = "True";
            jo["startTime"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            jo["expectTime"] = 200;
            jo["maxScore"] = maxScore;
            jo["repeatCount"] = 1;
            if (score <= maxScore * 0.3)
            {
                jo["evaluation"] = "差";
            }
            if ((maxScore * 0.3 < score) && (score <= maxScore * 0.7))
            {
                jo["evaluation"] = "良";
            }
            if (maxScore * 0.7 < score)
            {
                jo["evaluation"] = "优秀";
            }

            jo["scoringModel"] = scoringModel;
            jo["remarks"] = "备注";
            jo["endTime"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //Debug.Log("endTime" + jo["endTime"]);
            questionNumber++;
            Debug.Log(jo.ToString());
            joReportStepList.Add(jo);
        }

        /// <summary>
        /// 记录实验报告文本
        /// </summary>
        public void RecordsReport(string key, string value)
        {
            JObject jo = new JObject();
            jo[key] = value;
            joReportTextList.Add(jo);
        }

        /// <summary>
        /// 记录图片
        /// </summary>
        /// <param name="renderTexture">图片</param>
        /// <param name="imageName">图片对应报告的名字（image1_N）</param>
        /// <param name="extend">图片格式（jpg，png等等）</param>
        public void RecordsImage(RenderTexture renderTexture, string imageName, string extend)
        {
            ///将RenderTexturex先转成Texture2D再转换成base64编码格式
            int width = renderTexture.width;
            int height = renderTexture.height;
            Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture2D.Apply();
            texture2D.Apply();
            byte[] bytes = texture2D.EncodeToJPG();
            ///把base64编码保存到streamingAssets文件夹下.txt文件中，去网页测试是否正确
            //string path = Path.Combine(Application.streamingAssetsPath, imageName + ".txt");
            //File.WriteAllText(path, Convert.ToBase64String(bytes));
            string src = "data:image/" + extend + ";base64," + Convert.ToBase64String(bytes);

            //已上传的步骤，只更新得分和结束时间
            for (int i = 0; i < joReportImageList.Count; i++)
            {
                if (joReportImageList[i]["name"].ToString() == imageName)
                {
                    //Debug.Log("--------------------已上传过该图片，重新覆盖替换");
                    joReportImageList[i]["name"] = imageName;
                    joReportImageList[i]["src"] = src;
                    joReportImageList[i]["extend"] = extend;
                    return;
                }
            }
            //Debug.Log(--------------------未上传过该图片);
            JObject jo = new JObject();
            jo["name"] = imageName;
            jo["src"] = src;
            jo["extend"] = extend;
            joReportImageList.Add(jo);
            Debug.Log("--------------------成功写入图片数据");
        }

        #endregion

        #region 私有函数

        #region 接口API
        private IEnumerator SendData(string str)
        {
            var req = UnityWebRequest.PostWwwForm(host + "/openapi/data_upload", str);
            req.SetRequestHeader("Content-Type", "application/json;charset=UTF-8");
            var op = req.SendWebRequest();
            yield return op;
            if (op.webRequest.isNetworkError)
            {
                Debug.LogError($"Network Error: {op.webRequest.error}");
                yield break;
            }
            var obj = op.webRequest.downloadHandler.text;
            Debug.Log(obj.ToString());
        }

        #endregion

        /// <summary>
        /// 填充步骤
        /// </summary>
        private void PatchStep()
        {
            if (joReportStepList.Count < 10)
            {
                int count = joReportStepList.Count;
                for (int i = 0; i < 11 - count; i++)
                {
                    RecordsStep("填充步骤", $"填充步骤{i}", 0, 0, "赋分模型");
                }
            }
        }

        /// <summary>
        /// 修改上传时间
        /// </summary>
        private void ChangeTime()
        {
            System.DateTime time = startTime;
            for (int i = 0; i < joReportStepList.Count; i++)
            {
                time = time.AddSeconds(UnityEngine.Random.Range(1, 3));
                joReportStepList[i]["startTime"] = time.ToString("yyyy-MM-dd HH:mm:ss");
                time = time.AddSeconds(UnityEngine.Random.Range(1, 3));
                joReportStepList[i]["endTime"] = time.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 云渲染版获取应用程序启动参数
        /// </summary>
        /// <returns>sequenceCode</returns>
        private string GetCommandArgs2()
        {
            string[] args = Environment.GetCommandLineArgs();
            string sequenceCode = "";

            foreach (var item in args)
            {
                //GameObject.Find("UIRoot").transform.Find("args").GetComponent<Text>().text += item;
                if (item.StartsWith("serialNumber"))
                {
                    string[] str = HttpUtility.UrlDecode(item.Split('=')[1]).Split('|');
                    appId = str[0];
                    Owvlab.appId = appId;
                    //GameObject.Find("UIRoot").transform.Find("appid").GetComponent<Text>().text = appId;
                    sequenceCode = str[1];
                }
            }
            Debug.Log("sequenceCode:" + sequenceCode);
            //GameObject.Find("UIRoot").transform.Find("sequenceCode").GetComponent<Text>().text = sequenceCode;
            return sequenceCode;
        }

        /// <summary>
        /// PC获取启动项
        /// </summary>
        /// <returns></returns>
        private string GetCommandArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            string sequenceCode = "";

            string newArgs = "";
            foreach (var item in args)
            {
                //GameObject.Find("UIRoot").transform.Find("args").GetComponent<Text>().text += ":" + item;
                newArgs += item;
            }
            if ((newArgs.Contains("token")) && (newArgs.Contains("sequenceCode")))
            {
                int start1 = newArgs.IndexOf("token=");

                string newLine = newArgs.Substring(start1, newArgs.Length - start1);
                int end1 = newLine.IndexOf("_");
                int start2 = newLine.IndexOf("sequenceCode=");
                Debug.Log("newline:" + newLine);
                appId = newLine.Substring(6, end1 - 6);
                Owvlab.appId = appId;
                //GameObject.Find("UIRoot").transform.Find("appid").GetComponent<Text>().text = appId;
                sequenceCode = newLine.Substring(start2 + 13, newLine.Length - start2 - 13);
                Debug.Log("appId:" + appId);
                Debug.Log("sequenceCode:" + sequenceCode);
                //GameObject.Find("UIRoot").transform.Find("sequenceCode").GetComponent<Text>().text = sequenceCode;
            }
            return sequenceCode;
        }

        /// <summary>
        /// Pc 云渲染认证成功，显示实验选项列表
        /// </summary>
        /// <param name="str">认证响应数据</param>
        private void PopUpExpWindow(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }
            JObject jo = JObject.Parse(str);

            JArray exps = jo["expList"].ToObject<JArray>();

            foreach (var item in exps)
            {

                expId = item["expId"].ToString();
                expName = item["expName"].ToString();
                expScore = item["expScore"].ToString();
                Debug.Log("expId:" + expId);
                //GameObject.Find("UIRoot").transform.Find("expId").GetComponent<Text>().text = expId;
            }
        }

        #endregion
    }
}

