/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： OwvlabContext
* 创建日期：2021-11-18 11:01:39
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{
    /// <summary>
    /// Owvlab 上下文,负责执行与 Owvlav 相关的操作
    /// </summary>
	public class OwvlabContext 
	{
        //实验数据对象
        private OwvlabData _owvlabData;

        //数据交互对象
        private OwvlabRequest _owvlabRequest;
        public OwvlabContext(MonoBehaviour mono)
        {
            _owvlabData = new OwvlabData();
            _owvlabRequest = new OwvlabRequest(mono);
        }

        /// <summary>
        /// Pc 登录认证
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="passward">密码</param>
        /// <param name="callback">回调</param>
        public void LoginIn(string account, string passward, System.Action<string> callback)
        {
            string url = Owvlab.host + "/openapi/outer/login";// + Owvlab.appId
            /*
            JObject param = new JObject();
            param["account"] = account;
            param["password"] = passward;
            */
            WWWForm form = new WWWForm();
            form.AddField("account", account);
            form.AddField("password", passward);

            JObject jo = new JObject();
            jo.Add("account", account);
            jo.Add("password", passward);
            _owvlabRequest.Post(url, jo, callback);
        }

        /// <summary>
        /// 云渲染登录认证
        /// </summary>
        /// <param name="sequenceCode">sequenceCode</param>
        /// <param name="callback">回调</param>
        public void LoginIn(string sequenceCode, System.Action<string> callback)
        {
            string url = Owvlab.host + "/openapi/" + Owvlab.appId;
            /*
            JObject param = new JObject();
            param["sequenceCode"] = sequenceCode;
            */
            WWWForm form = new WWWForm();
            form.AddField("sequenceCode", sequenceCode);
            _owvlabRequest.Post(url, form, callback);
        }
        
        /// <summary>
        /// 追加详细得分步骤数据项
        /// </summary>
        /// <param name="item">数据项</param>
        public void AppendScoreDetailItem(ScoreDetailItem item)
        {
            _owvlabData.AppendData(OwvlabExpType.Score, item);
        }

        /// <summary>
        /// 追加文件列表
        /// </summary>
        /// <param name="item"></param>
        public void AppendFileItem(FileItem item)
        {
            _owvlabData.AppendData(OwvlabExpType.File, item);
        }

        /// <summary>
        /// 追加实验报告数据项
        /// </summary>
        /// <param name="item">数据项</param>
        public void AppendReportItem(JObject item)
        {
            _owvlabData.AppendData(OwvlabExpType.Report, item);
        }

        /// <summary>
        /// 追加实验脚本数据内容
        /// </summary>
        /// <param name="content">数据内容</param>
        public void AppendExpScriptContent(JObject content)
        {
            _owvlabData.AppendData(OwvlabExpType.ScriptContent, content);
        }

        /// <summary>
        /// 上传数据
        /// </summary>
        public void SendData()
        {
            string url=Owvlab.host+"/openapi/data_upload";
            _owvlabRequest.Post(url,_owvlabData.GetExpResultData());
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        public void SendAttachment()
        {
            string url = Owvlab.host + "/openapi/upload_file";
            WWWForm form = new WWWForm();
            form.AddField("appId", Owvlab.appId);
            form.AddField("expId", Owvlab.expId);
            foreach (var item in _owvlabData.GetFileData())
            {
                form.AddBinaryData("fileList", item.data, item.filename);
            }
            _owvlabRequest.Post(url, form);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void GetData(string dataType, System.Action<string> callback)
        {
            string url = string.Format("{0}/openapi/data_get?appId={1}&expId={2}&dataType={3}",
                Owvlab.host, Owvlab.appId, Owvlab.expId, dataType);
            _owvlabRequest.Get(url, callback);
        }
    }
}

