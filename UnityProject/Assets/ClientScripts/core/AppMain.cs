using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class AppMain : MonoSingleton<AppMain>
{

    string version = "0";
	long Lversion = 0;

    void Awake()
    {

#if UNITY_EDITOR
        //PlayerPrefs.DeleteAll();
#endif
        //限帧
        Application.targetFrameRate = 60;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    // Use this for initialization
    IEnumerator Start()
    {

        //StartCoroutine(LoadVersion());
        
    
        yield return 0;
        //PlayerPrefs.DeleteAll();
        
        
 
    }

    
    

#region pause app
    void OnApplicationPause(bool isPause)
    {

#if UNITY_IPHONE || UNITY_ANDROID

        Debug.Log("OnApplicationPause:  " + isPause);

        if (isPause)
        {
            // 前台到后台的操作
        }
        else
        {
        }
        
#endif

    }

    void OnApplicationFocus(bool isFocus)
    {

#if UNITY_IPHONE || UNITY_ANDROID

        Debug.Log("OnApplicationFocus  " + isFocus);

        if (isFocus)
        {
            // “启动”手机时，事件
        }
        else
        {

        }

#endif

    }
    #endregion

#region Version
     
    

    public string GetVersion()
    {
        return version;
    }

	public long GetVersionTicks()
	{
		return Lversion;
	}

#endregion
}
