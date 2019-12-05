using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public interface IYSurgeryUnityListener
{
    string onMessage(string sValue, string  iValue);
}
public class ListenerAdapter : AndroidJavaProxy
{
    private readonly IYSurgeryUnityListener listener;
    public ListenerAdapter(IYSurgeryUnityListener listener) : base("com.yuji.ysurgeryandroidnative.IYSurgeryUnityListener")
    {
        this.listener = listener;
    }
    string onMessage(string sValue, string iValue)
    {
        return listener.onMessage(sValue, iValue);
    }
}



public class AndroidNative : IYSurgeryUnityListener
{
    static AndroidJavaClass javaUnityPlayer;
    static AndroidJavaObject currentActivity;
    static AndroidJavaObject androidPlugin;

    static ListenerAdapter listenerAdapter;

    public delegate string NativeCallFunc(string value);
    Dictionary<string,NativeCallFunc> functionDict = new Dictionary<string, NativeCallFunc>();

    void InitThread()
    {

    }

    public AndroidNative()
    {
        //Debug.Log("AndroidNative Construction");
        //Thread t1 = new Thread(new ThreadStart(InitThread));
        //t1.IsBackground = false;
        //t1.Start();


        //AndroidJNI.AttachCurrentThread();
        javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        androidPlugin = new AndroidJavaObject("com.yuji.ysurgeryandroidnative.YSurgeryAndroidNative", currentActivity);



        listenerAdapter = new ListenerAdapter(this);
        CallJavaFunc("SetUnityListener", listenerAdapter);

        //AndroidJNI.DetachCurrentThread();
    }

    private void CallJavaFunc(string funcName, params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            androidPlugin.Call(funcName, args);
        }
    }
    private T CallJavaFunc<T>(string funcName,params object[] args)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return androidPlugin.Call<T>(funcName, args);
        }
        return default(T);
    }

    //call Android Function
    public string CallFromUnity(string funcName,string value)
    {
        return CallJavaFunc<string>("CallFromUnity",funcName,value);

    }

    //Listen call From Android Java
    public string onMessage(string funcName,string value)
    {
        Debug.Log(string.Format("Call From Android funcName : {0}  {1}" ,funcName, value));
        if(functionDict.ContainsKey(funcName))
        {
            return functionDict[funcName](value);
        }

        return "";
    }


    public bool RegisterFunction(string funcName,NativeCallFunc func)
    {
        if(functionDict.ContainsKey(funcName))
        {
            return false;
        }
        else
        {
            functionDict.Add(funcName,func);
            return true;
        }
    }
    public bool UnregisterFunction(string funcName)
    {
        if(functionDict.ContainsKey(funcName))
        {
            functionDict.Remove(funcName);
            return true;
        }
        else
        {
            return false;
        }
    }
}


