/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： MonoSingleton
* 创建日期：2022-02-23 14:50:59
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace App
{
    /// <summary>
    /// 
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

	    private static T instance = null;
	    public static T Instance
	    {
		    get
		    {
			    if (instance == null)
			    {
				    instance = FindObjectOfType(typeof(T)) as T;
				    if (instance == null)
				    {
					    instance = new GameObject("_" + typeof(T).Name).AddComponent<T>();
					    DontDestroyOnLoad(instance);
				    }
				    if (instance == null)
					    Debug.LogWarning("Failed to create instance of " + typeof(T).FullName + ".");
			    }
			    return instance;
		    }
	    }

	    void OnApplicationQuit() { if (instance != null) instance = null; }

	    public static T CreateInstance()
	    {
		    if (Instance != null) Instance.OnCreate();
		    return Instance;
	    }

	    protected virtual void OnCreate()
	    {

	    }
    }
}

