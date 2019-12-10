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
     
    
    IEnumerator LoadVersion()
    {
        //不写这句IOS可能CRASH
        yield return new WaitForEndOfFrame();
#if UNITY_EDITOR

        string versionfilepath = "file://" + Application.streamingAssetsPath + "/version.bytes";
#else
        string versionfilepath = AssetBundlePlatformPathManager.GetStreamingAssetsPathForWWW("/version.bytes");
#endif

        WWW w = new WWW(versionfilepath);
        yield return w;

        byte[] arr = w.bytes;

        System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
        string str = converter.GetString(arr);
        long ticks = Convert.ToInt64(str);
		Lversion = ticks;
        DateTime dt = new DateTime(ticks);
        version = dt.ToString("yyyy-MM-dd-HH-mm-ss");


        //if (File.Exists(versionfilepath))
        //{
        //    FileStream fs = new FileStream(versionfilepath, FileMode.Open);
        //    byte[] farr = new byte[fs.Length];
        //    fs.Read(farr, 0, farr.Length);
        //    fs.Close();

        //    version = converter.GetString(farr);
        //}
    }
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
