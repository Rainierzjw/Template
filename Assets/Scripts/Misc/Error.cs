/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Error
* 创建日期：2022-03-29 15:09:45
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
	public class Error : IError 
	{
		public Error(int code, string msg)
		{
			this.msg = msg;
			this.code = code;
		}

		public Error(string msg) => this.msg = msg;

		public Error(string msg, params object[] args) => this.msg = string.Format(msg, args);

		public Error(int code, string msg, params object[] args)
		{
			this.code = code;
			this.msg = string.Format(msg, args);
		}

		public int code { get; }

		public string msg { get; }

		public override string ToString() => this.code > 0 ? string.Format("{0}:{1}", (object) this.code, (object) this.msg) : this.msg ?? "";
	}
}

