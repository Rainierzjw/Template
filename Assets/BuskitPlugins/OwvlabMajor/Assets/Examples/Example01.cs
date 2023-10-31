/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Test
* 创建日期：2021-11-22 16:28:39
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.Web;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{
    /// <summary>
    /// 测试代码，测试实验结果数据上传
    /// </summary>
	public class Example01 : MonoBehaviour 
	{
		public OwvlabContext owvlabContext;

		public Texture2D texture;
		public Button expDataBtn;

		public Button AttachmentBtn;
		/// <summary>
		/// Unity Method
		/// </summary>
		void Start () 
		{
			Owvlab owvlab = gameObject.AddComponent<Owvlab>();
#if UNITY_STANDALONE
			
			//客户端
            //owvlabContext = owvlab.Init("http://vr.owvlab.com", "cbb9a21f-5cf3-4250-8098-094ee460b3d3", "202111292","rainier");
            //云渲染
            //owvlabContext = owvlab.Init("http://vr.owvlab.com");
#endif

#if UNITY_WEBGL
			owvlabContext = owvlab.Init();
#endif
		
			

			//Owvlab.appId = "ccf00c76-a0a9-4671-b4cc-03060dfe7fc7";
			//Owvlab.expId = "2058";
			//Owvlab.host = "http://vr.owvlab.com";

			expDataBtn.onClick.AddListener(()=> {

				AddReportData("text1", "ceshi1");
				AddReportData("text2", "ceshi2", "red");
				AddScoreDetail();
				AddScriptContent();
				owvlabContext.SendData();
			});
			AttachmentBtn.onClick.AddListener(() => {

				AddFiles();
				owvlabContext.SendAttachment();
			});
		}
		/// <summary>
		/// 修改Json格式 by OnlyIU
		/// </summary>
		/// <param name="Key"></param>
		/// <param name="Value"></param>
		/// <param name="Color"></param>
		public void AddReportData(string Key, string Value, string Color = "black")
		{
			JObject report = new JObject();
			JObject item = new JObject();
			item["text"] = Value;
			item["color"] = Color;
			report[Key] = item;
			owvlabContext.AppendReportItem(report);
		}


		public void AddScoreDetail()
		{
			System.DateTime time = System.DateTime.Now;
            for (int i = 1; i < 11; i++)
            {
				ScoreDetailItem item = ExpStep("实验成绩"+i, "学生操作成绩"+i,i,Random.Range(1,10), time,"测试评价");
				owvlabContext.AppendScoreDetailItem(item);
				time = time.AddSeconds(Random.Range(15, 30));
            }
		}
		 
		public ScoreDetailItem ExpStep(string moduleFlag,string questionStem, int questionNumber , int score, System.DateTime time, string evaluation)
		{
			ScoreDetailItem item1 = new ScoreDetailItem();
			item1.moduleFlag = moduleFlag;
			item1.questionNumber = questionNumber;
			item1.questionStem = questionStem;
			item1.score = score;
			item1.trueOrFalse = "True";
			item1.startTime = time;
			item1.endTime = time.AddMinutes(Random.Range(1, 5)).AddSeconds(Random.Range(1, 30));
			item1.expectTime = 200;
			item1.maxScore = 10;
			item1.repeatCount = 1;
			item1.evaluation = evaluation;
			item1.scoringModel = "其他";
			item1.remarks = "备注";

			return item1;
		}

		public void AddFiles()
		{

			FileItem f1 = new FileItem();
			f1.filename = "image1.jpg";
			f1.data = texture.EncodeToJPG();
			FileItem f2 = new FileItem();
			f2.filename = "image2.jpg";
			f2.data = texture.EncodeToJPG();

			owvlabContext.AppendFileItem(f1);
			owvlabContext.AppendFileItem(f2);
		}


		public void AddScriptContent()
		{
			JObject script = new JObject();
			script["dataType"] = "MyScripts";
			script["测试1"] = "测试代码1";
			script["测试2"] = "测试代码2";

			owvlabContext.AppendExpScriptContent(script);
		}

		public void DownloadData()
		{
			owvlabContext.GetData("MyScripts", p => {
				Debug.Log("DownloadData: " + p);
			});
		}

	     
	}
}

