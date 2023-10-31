/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ISystem
* 创建日期：2022-02-24 09:15:19
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：系统接口
******************************************************************************/

using UnityEngine;
using System.Collections;
using App;

namespace SystemManager
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystem
    {
        void Register();

        void UnRegister();
    }
}

