using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;



public class ListenerAdapter : AndroidJavaProxy
{
    public ListenerAdapter() : base("com.yuji.face.IYSurgeryUnityListener")
    {
        Debug.Log("ListenerAdapter Construction");
    }

    string onCalculateLowPolyFace(AndroidJavaObject hdObjDataWrapper, int gender, float height, float weight,string retJson)
    {
        bool finish = false;
        string ret = "";
        Loom.QueueOnMainThread((param) =>
        {
            AndroidJavaObject bufferObject = hdObjDataWrapper.Call<AndroidJavaObject>("getBytes");
            byte[] buffer = AndroidJNIHelper.ConvertFromJNIArray<byte[]>(bufferObject.GetRawObject());

            ret = ModelDataManager.Instance.CalculateLowPolyFace(buffer, gender, height, weight,retJson);
            finish = true;
        }, null);

        while (!finish)
        {
            Thread.Sleep(100);
        }

        return ret;
    }


    bool onLoadLowPolyFace(string roleJson, AndroidJavaObject textureDataWrapper)
    {
        bool finish = false;
        bool ret = false;

        Loom.QueueOnMainThread((param) =>
        {
            AndroidJavaObject bufferObject = textureDataWrapper.Call<AndroidJavaObject>("getBytes");
            byte[] buffer = AndroidJNIHelper.ConvertFromJNIArray<byte[]>(bufferObject.GetRawObject());
            Debug.Log("onLoadLowPolyFace data len:" + buffer.Length);



            Texture2D tex = null;
            if (buffer != null)
            {
                tex = new Texture2D(1, 1);
                tex.LoadImage(buffer);
            }


            ret = ModelDataManager.Instance.LoadLowPolyFace(roleJson, tex);
            finish = true;

        }, null);

        while (!finish)
        {
            Thread.Sleep(100);
        }

        return ret;

    }

    string onSaveDeform()
    {
        bool finish = false;
        string ret = "";

        Loom.QueueOnMainThread((param) =>
        {
            ret = ModelDataManager.Instance.SaveDeform();
            finish = true;

        }, null);

        while (!finish)
        {
            Thread.Sleep(100);
        }

        return ret;

    }
    bool onLoadDeform(string deformJson)
    {
        bool finish = false;
        bool ret = false;

        Loom.QueueOnMainThread((param) =>
        {
            ret = ModelDataManager.Instance.LoadDeform(deformJson);
            finish = true;

        }, null);

        while (!finish)
        {
            Thread.Sleep(100);
        }

        return ret;
    }
    string onSaveAvatar()
    {
        return ModelDataManager.Instance.SaveAvatar();

    }
    bool onLoadAvatar(string avatarJson)
    {
        return ModelDataManager.Instance.LoadAvatar(avatarJson);

    }


    void onEnterEditMode()
    {
        Loom.QueueOnMainThread((param) =>
        {
            ViewUI.Instance.EditButton_clk();

        }, null);
    }

    void onExitEditMode()
    {
        Loom.QueueOnMainThread((param) =>
                {
                    ActiveScene.Instance.BackFirstpage();


                }, null);
    }

    void onFullScreen(bool b)
    {
        Loom.QueueOnMainThread((param) =>
                {

                    Screen.fullScreen = b;
                    if (b)
                    {
                        BarHelper.Hide();

                    }
                    else
                    {
                        BarHelper.Show();

                    }

                }, null);
    }
}



public class AndroidNativeInterface : MonoBehaviour
{
    public static AndroidNativeInterface Instance;
    void Awake()
    {
        Instance = this;
        BetterStreamingAssets.Initialize();

        try
        {

            javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            androidPlugin = new AndroidJavaObject("com.yuji.face.YSurgeryUnityInterface", currentActivity);


            listenerAdapter = new ListenerAdapter();
            CallJavaFunc("SetUnityListener", listenerAdapter);

        }
        catch (Exception e)
        {
            Debug.Log("AndroidNativeInterface Init Failed");
        }

    }




    static AndroidJavaClass javaUnityPlayer;
    static AndroidJavaObject currentActivity;
    static AndroidJavaObject androidPlugin;

    static ListenerAdapter listenerAdapter;


    private void CallJavaFunc(string funcName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            androidPlugin.Call(funcName, args);
        }
    }
    private T CallJavaFunc<T>(string funcName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return androidPlugin.Call<T>(funcName, args);
        }
        return default(T);
    }

    //call Android Function
    public string CallFromUnity(string funcName, string value)
    {
        return CallJavaFunc<string>("CallFromUnity", funcName, value);

    }
}



public class BarHelper
{
    static int newStatusBarValue;
    /// <summary>
    ///  隐藏上方状态栏
    /// </summary>
    public static void Hide()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        setStatusBarValue(10024); // WindowManager.LayoutParams.FLAG_FULLSCREEN; change this to 0 if unsatisfied
#endif
    }

    /// <summary>
    ///  显示上方状态栏
    /// </summary>
    public static void Show()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        setStatusBarValue(2048); // WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN
#endif
    }

    private static void setStatusBarValue(int value)
    {
        newStatusBarValue = value;
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                activity.Call("runOnUiThread", new AndroidJavaRunnable(setStatusBarValueInThread));
            }
        }
    }

    private static void setStatusBarValueInThread()
    {
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var window = activity.Call<AndroidJavaObject>("getWindow"))
                {
                    window.Call("setFlags", newStatusBarValue, newStatusBarValue);
                }
            }
        }
    }
}