/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Owvlab1
* 创建日期：2021-11-18 11:04:56
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{

    /// <summary>
    /// Owvlab 实验数据类型
    /// </summary>
    public enum OwvlabExpType
    {
        Null,
        Score,//得分
        Report,//实验报告
        File,//附件
        ScriptContent,//实验脚本
        Custom //其他

    }

    /// <summary>
    /// 上传至 Owvlab 的本次实验中产生的实验数据对象，实验过程中，保持独一份
    /// 通过 OwvlabContext 填充相应的实验数据段
    /// </summary>
	public class OwvlabData
    {

        //描述项
        private HeaderData _headerData;

        //实验步骤详细列表
        private ExpData<ScoreDetailItem> _scoreDetails;

        //多媒体文件
        private ExpData<FileItem> _fileLists;

        //实验报告内容
        private ExpData<JObject> _reportData;

        //实验脚本信息
        private ExpData<JObject> _expScriptContent;


        public OwvlabData()
        {
            _headerData = new HeaderData("北京润尼尔网络科技公司",
                                         "1.0.0",
                                         System.DateTime.Now.ToString());
            _scoreDetails = new ExpData<ScoreDetailItem>(OwvlabExpType.Score);
            _fileLists = new ExpData<FileItem>(OwvlabExpType.File);
            _reportData = new ExpData<JObject>(OwvlabExpType.Report);
            _expScriptContent = new ExpData<JObject>(OwvlabExpType.ScriptContent);
          
        }

        /// <summary>
        /// 追加数据项
        /// </summary>
        /// <param name="type">数据类型</param>
        public void AppendData(OwvlabExpType type, object data)
        {
            switch (type)
            {
                case OwvlabExpType.Score:
                    _scoreDetails.AppendItem((ScoreDetailItem)data);
                    break;
                case OwvlabExpType.Report:
                    _reportData.AppendItem(data as JObject);
                    break;
                case OwvlabExpType.File:
                    _fileLists.AppendItem((FileItem)data);
                    break;
                case OwvlabExpType.ScriptContent:
                    _expScriptContent.AppendItem(data as JObject);
                    break;

                default:
                    throw new System.Exception(string.Format("[{0}] Type doesn't exist! ]", type));
            }
        }

        /// <summary>
        /// 追加自定义数据项
        /// </summary>
        public virtual void AppendCustomItem()
        { }

        //public JObject GetExpData()
        //{
        //    JObject result = new JObject();

        //}

        /// <summary>
        /// 获取实验结果数据，数据板式为当前版本
        /// </summary>
        /// <returns></returns>
        public JObject GetExpResultData()
        {
            //TODO：版本判断 

            JObject result = new JObject();
            result["appId"] = Owvlab.appId;
            result["expId"] = Owvlab.expId;
            //添加版本标识 by OnlyIU
            result["version"] = "1.0";
            if (_reportData.Count() != 0)
            {

                //实验报告段
                JArray reportArryay = new JArray();
                foreach (var item in _reportData)
                {
                    reportArryay.Add(item);
                }
                result["reportData"] = reportArryay;
            }
            if (_scoreDetails.Count() != 0)
            {
                //详细得分段
                JArray scoreArray = new JArray();
                foreach (var item in _scoreDetails)
                {
                    scoreArray.Add(item.ToJson());
                }
                result["expScoreDetails"] = scoreArray;
            }
            if (_expScriptContent.Count() != 0)
            {
                //实验脚本数据段
                JArray scriptArray = new JArray();
                foreach (var item in _expScriptContent)
                {
                    scriptArray.Add(item);
                }
                result["expScriptContent"] = scriptArray;
            }

            return result;
        }

        public ExpData<FileItem> GetFileData()
        {
            return _fileLists;
        }
    }

    /// <summary>
    /// OwvlabData  数据头，标志信息和版本
    /// </summary>
    public struct HeaderData
    {
        //公司
        public string Company { get; set; }

        //当前使用版本号
        public string Version { get; set; }

        //时间戳
        public string TimeStamp { get; set; }

        public HeaderData(string company, string version, string timeStamp)
        {
            Company = company;
            Version = version;
            TimeStamp = timeStamp;
        }

    }

    /// <summary>
    /// Owblab 实验数据
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ExpData<TItem> : IEnumerable<TItem>
    {
        //实验数据类型
        private OwvlabExpType _type;

        //实验数据项列表
        private List<TItem> content = new List<TItem>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">实验数据类型</param>
        public ExpData(OwvlabExpType type)
        {
            _type = type;
        }

        /// <summary>
        /// 根据索引获取数据项
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public TItem this[int index]
        {
            get { return content[index]; }
        }

        /// <summary>
        /// 获取实验数据类型
        /// </summary>
        /// <returns></returns>
        public string GetExpType()
        {
            return _type.ToString();
        }

        /// <summary>
        /// 追加当前实验数据类型数据项
        /// </summary>
        /// <param name="type">子段名称</param>
        /// <param name="data">子段内容</param>
        public void AppendItem(TItem data)
        {
            content.Add(data);
        }

        /// <summary>
        /// 删除当前实验数据类型数据项
        /// </summary>
        /// <param name="type">子段名称</param>
        public void RemoveItem(TItem data)
        {
            content.Remove(data);
        }

        /// <summary>
        /// 元素个数
        /// </summary>
        /// <returns>个数</returns>
        public int Count()
        {
            return content.Count;
        }

        /// <summary>
        /// 遍历数据项列表
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            for (int i = 0; i < content.Count; i++)
            {
                yield return content[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// 实验得分详细步骤数据项
    /// </summary>
    public struct ScoreDetailItem
    {
        public string          moduleFlag      ;// ="实验成绩";
        public int             questionNumber  ;// =1;
        public string          questionStem    ;// ="学生操作成绩";
        public int             score           ;// =80;
        public string          trueOrFalse     ;// = "True";
        public System.DateTime startTime       ;// =System.DateTime.Now;
        public System.DateTime endTime         ;// =System.DateTime.Now;
        public int             expectTime      ;// =200;
        public int             maxScore        ;// =60;
        public int             repeatCount     ;// =1;
        public string          evaluation      ;// ="其他";
        public string          scoringModel    ;// ="其他";
        public string          remarks         ;// ="备注";

        public ScoreDetailItem(
            string          moduleFlag      ,
            int             questionNumber  ,
            string          questionStem    ,
            int             score           ,
            string          trueOrFalse     ,
            System.DateTime startTime       ,
            System.DateTime endTime         ,
            int             expectTime      ,
            int             maxScore        ,
            int             repeatCount     ,
            string          evaluation      ,
            string          scoringModel    ,
            string          remarks         
            )
        { 
            this.moduleFlag      =moduleFlag     ;
            this.questionNumber  =questionNumber ;
            this.questionStem    =questionStem   ;
            this.score           =score          ;
            this.trueOrFalse     =trueOrFalse    ;
            this.startTime       =startTime      ;
            this.endTime         =endTime        ;
            this.expectTime      =expectTime     ;
            this.maxScore        =maxScore       ;
            this.repeatCount     =repeatCount    ;
            this.evaluation      =evaluation     ;
            this.scoringModel    =scoringModel   ;
            this.remarks         =remarks        ;
        }

        public ScoreDetailItem Clone(ScoreDetailItem source)
        {
            return new ScoreDetailItem(
                source.moduleFlag,
                source.questionNumber,
                source.questionStem,
                source.score,
                source.trueOrFalse,
                source.startTime,
                source.endTime,
                source.expectTime,
                source.maxScore,
                source.repeatCount,
                source.evaluation,
                source.scoringModel,
                source.remarks
                );
        }

        public JObject ToJson()
        {
            JObject result = new JObject();
            result["moduleFlag"]=       moduleFlag;
            result["questionNumber"] =  questionNumber;
            result["questionStem"]=     questionStem;
            result["score"]=            score;
            result["trueOrFalse"] =     trueOrFalse;
            result["startTime"]=        startTime.ToString("yyyy-MM-dd HH:mm:ss");
            result["endTime"]=          endTime.ToString("yyyy-MM-dd HH:mm:ss");
            result["expectTime"]=       expectTime;
            result["maxScore"]=         maxScore;
            result["repeatCount"]=      repeatCount;
            result["evaluation"]=       evaluation;
            result["scoringModel"]=     scoringModel;
            result["remarks"] =         remarks;
           
            return result;

        }
    }

    /// <summary>
    /// 实验附件数据项
    /// </summary>
    public struct FileItem
    {
        public string filename;
        public byte[] data;
    }
}

