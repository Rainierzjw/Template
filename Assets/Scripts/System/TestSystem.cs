/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： TestSystem
* 创建日期：2023-10-31 14:32:47
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using App;
using Misc;
using System.IO;
using Extend;
using UnityEngine.UI;

namespace SystemManager
{
    /// <summary>
    /// 
    /// </summary>
	public class TestSystem : ISystem
    {
        private AppFacade app;
        private GameObject mask;
        private string data;

        public TestSystem(AppFacade appFacade)
        {
            app = appFacade;
            Init();
        }

        public void Register()
        {
            app.RegisterSystem(SystemType.TestSystem, this);
        }

        public void UnRegister()
        {
            app.UnRegisterSystem(SystemType.TestSystem);
        }

        private void Init()
        {
            mask = AppFacade.Instance.transform.FindComp<Image>("RainierTestMask").gameObject;
            try
            {
                string path = Application.streamingAssetsPath + "/timelimit.tl";
                data = File.ReadAllText(path);
            }
            catch
            {
                mask.SetActive(true);
                return;
            }
            var endTime = System.DateTime.Parse(CryptDES.DecryptDES(data));
            //endTime = DateTime.Parse("2022-12-01 00:00:00");
            Debug.Log($"{endTime}+++++++{System.DateTime.Now > endTime}");
            if (System.DateTime.Now > endTime)
            {
                mask.SetActive(true);
            }
        }
    }
}

