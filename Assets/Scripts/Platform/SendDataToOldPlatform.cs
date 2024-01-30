/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ScoreManager
* 创建日期：2023-12-08 15:21:32
* 作者名称：NPC
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Misc;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace UI
{
    /// <summary>
    /// 
    /// </summary>
	public class SendDataToOldPlatform : MonoSingleton<SendDataToOldPlatform>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime;

        [HideInInspector] public JObject ReslutJson = new JObject();

        private string appId = "";
        private string expId = "";
        private string host = "";

        private JArray steps = new JArray();

        private void Awake()
        {
            startTime = DateTime.Now;
            this.gameObject.name = "Owvlab";
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Unity Method
        /// </summary>
        void Start()
        {
            GetUserInfo();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log(GetGrossScore());
                SendScoreToWeb();
            }
        }

        #region 公开函数

        /// <summary>
        /// WebGL获取用户信息
        /// </summary>
        private void GetUserInfo()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalCall("getExpId", this.gameObject.name, "Saas_JsCallBack");
#endif
        }

        /// <summary>
        /// webgl提交总分
        /// </summary>
        public void SendScoreToWeb()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string[] moduleFlag = { "实验成绩" };
            string[] questionNumber = { "1" };
            string[] questionStem = { "学生实验操作成绩" };
            string[] scores = { GetGrossScore().ToString() };
            string[] isTrue = { "True" };
            Application.ExternalCall("ReciveData", moduleFlag, questionNumber, questionStem, scores, isTrue);
#endif
        }

        /// <summary>
        /// webgl提交实验步骤
        /// </summary>
        /// <param name="jsonReslut">json格式报告字符串</param>
        public void SendStepToWeb()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PatchStep();
            ChangeTime();
            Application.ExternalCall("UploadSteps", GetStepJson());
#endif
        }

        /// <summary>
        /// webgl提交实验报告
        /// </summary>
        /// <param name="jsonReslut">json格式报告字符串</param>
        public void SendReportToWeb(string jsonReslut)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalCall("ReportEdit", jsonReslut);
            StartCoroutine(SendScore());
#endif
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq">实验步骤序号</param>
        /// <param name="title">步骤名称</param>
        /// <param name="startTime">实验步骤开始时间</param>
        /// <param name="endTime">实验步骤结束时间</param>
        /// <param name="maxScore">实验步骤满分</param>
        /// <param name="score">实验步骤得分</param>
        /// <param name="repeatCount">实验步骤操作次数</param>
        /// <param name="evaluation">步骤评价</param>
        /// <param name="scoringModel">考察点</param>
        /// <param name="remarks">备注</param>
        public void AddStep(string title, /*DateTime startTime, DateTime endTime,*/ int maxScore, int score, int repeatCount = 0, string evaluation = "暂无", string scoringModel = "暂无", string remarks = "暂无")
        {
            JObject step = new JObject();
            step["seq"] = steps.Count + 1;
            step["title"] = title;
            step["startTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            step["endTime"] = DateTime.Now.AddSeconds(UnityEngine.Random.Range(1, 3)).ToString("yyyy-MM-dd HH:mm:ss");
            step["expectTime"] = "240";
            step["maxScore"] = maxScore.ToString();
            step["score"] = score.ToString();
            step["repeatCount"] = repeatCount.ToString();
            step["evaluation"] = evaluation;
            step["scoringModel"] = scoringModel;
            step["remarks"] = remarks;
            steps.Add(step);
            Debug.Log($"步骤{steps.Count + 1}:{title},满分{maxScore},得分{score}");
        }

        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="files"></param>
        public void SendMediaFile(Dictionary<string, byte[]> files)
        {
            string url = host + "/openapi/upload_file";
            WWWForm data = new WWWForm();
            data.AddField("appId", appId);
            data.AddField("expId", expId);
            foreach (var item in files)
            {
                data.AddBinaryData("fileList", item.Value, item.Key);
            }
            IEnumerator Send()
            {
                UnityWebRequest req = UnityWebRequest.Post(url, data);
                var op = req.SendWebRequest();
                yield return op;
                Debug.Log(op.webRequest.downloadHandler.text);
            }

            StartCoroutine(Send());
        }

        /// <summary>
        /// 上传文本文件
        /// </summary>
        /// <param name="result"></param>
        public void SendText(JObject result)
        {
            string url = host + "/openapi/data_upload";
            result["appId"] = appId;
            result["expId"] = expId;
            IEnumerator Send()
            {
                UnityWebRequest req = UnityWebRequest.Post(url, result.ToString());
                req.SetRequestHeader("Content-Type", "Application/json");
                var op = req.SendWebRequest();
                yield return op;
                Debug.Log(op.webRequest.downloadHandler.text);
            }
            StartCoroutine(Send());
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="dataType"></param>
        public void GetData(System.Action<string> callback, string dataType)
        {
            string url = string.Format("{0}/openapi/data_get?appId={1}&expId={2}&dataType={3}",
                host, appId, expId, dataType);

            IEnumerator Get()
            {
                UnityWebRequest req = UnityWebRequest.Get(url);
                var op = req.SendWebRequest();
                yield return op;
                Debug.Log(op.webRequest.downloadHandler.text);
                if (callback != null)
                {
                    JObject jo = JObject.Parse(op.webRequest.downloadHandler.text);
                    callback(jo["data"]["context"].ToString());
                }
            }

            StartCoroutine(Get());
        }

        /// <summary>
        /// 获取运行时间
        /// </summary>
        /// <returns></returns>
        public string GetTime()
        {
            TimeSpan time = endTime - startTime;
            string str = "";
            if (time.Hours > 0)
            {
                str = time.Hours.ToString("00") + ":" + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
            }
            if (time.Hours == 0 && time.Minutes > 0)
            {
                str = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
            }
            if (time.Hours == 0 && time.Minutes == 0)
            {
                str = "00:" + time.Seconds.ToString("00");
            }

            return str;
        }

        /// <summary>
        /// 获取总分
        /// </summary>
        /// <returns></returns>
        public int GetGrossScore()
        {
            var score = 0;
            for (int i = 0; i < steps.Count; i++)
            {
                score += int.Parse(steps[i]["score"].ToString());
            }
            return score;
        }

        /// <summary>
        /// 向报告Json传入数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void WriteScoreToJson(string name, int score)
        {
            JArray array = new JArray();
            JObject obj = new JObject();
            obj["text"] = score.ToString();
            obj["color"] = "black";
            array.Add(obj);
            ReslutJson[name] = array;
        }

        public void WriteStringToJson(string name, string str)
        {
            JArray array = new JArray();
            JObject obj = new JObject();
            obj["text"] = str;
            obj["color"] = "black";
            array.Add(obj);
            ReslutJson[name] = array;
        }

#endregion

        #region 私有函数

        /// <summary>
        /// Js 回调函数
        /// </summary>
        /// <param name="json"></param>
        private void Saas_JsCallBack(string json)
        {
            Debug.Log("平台回传消息：" + json);
            JObject jo = JObject.Parse(json);
            appId = jo["appId"].ToString();
            expId = jo["expId"].ToString();
            host = jo["host"].ToString();
            Debug.Log(string.Format("appId:{0}\n expId:{1} \n host:{2}", appId, expId, host));
        }

        /// <summary>
        /// 获取步骤
        /// </summary>
        /// <returns></returns>
        private string GetStepJson()
        {
            JObject stepJson = new JObject();
            stepJson["eid"] = expId;
            stepJson["steps"] = steps;
            Debug.Log("步骤得分Json：" + stepJson.ToString());
            return stepJson.ToString();
        }

        /// <summary>
        /// 填充步骤
        /// </summary>
        private void PatchStep()
        {
            if (steps.Count < 10)
            {
                int count = steps.Count;
                for (int i = 0; i < 11 - count; i++)
                {
                    AddStep("填充步骤{i}", 0, 0);
                }
            }
        }

        /// <summary>
        /// 修改上传时间
        /// </summary>
        private void ChangeTime()
        {
            System.DateTime time = startTime;
            for (int i = 0; i < steps.Count; i++)
            {
                time = time.AddSeconds(UnityEngine.Random.Range(1, 3));
                steps[i]["startTime"] = time.ToString("yyyy-MM-dd HH:mm:ss");
                time = time.AddSeconds(UnityEngine.Random.Range(1, 3));
                steps[i]["endTime"] = time.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 添加实验报告数据
        /// </summary>
        /// <returns></returns>
        private string GetJsonReslut()
        {
            //OwvlabPlatform owvlabPc = InjectService.Get<OwvlabPlatform>();

            //ReslutJson["eid"] = owvlabPc.EID;
            //WriteStringToJson("text1", GetTime());

            Debug.Log(ReslutJson.ToString());
            return ReslutJson.ToString();
        }

        #endregion
    }
}

