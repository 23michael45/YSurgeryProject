using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IYSurgeryUnityListener
{
    void onMessage(string sValue, string  iValue);
}
public class ListenerAdapter : AndroidJavaProxy
{
    private readonly IYSurgeryUnityListener listener;
    public ListenerAdapter(IYSurgeryUnityListener listener) : base("com.yuji.ysurgeryandroidnative.IYSurgeryUnityListener")
    {
        this.listener = listener;
    }
    void onMessage(string sValue, string iValue)
    {
        listener.onMessage(sValue, iValue);
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
    public AndroidNative()
    {

        javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        androidPlugin = new AndroidJavaObject("com.yuji.ysurgeryandroidnative.YSurgeryAndroidNative", currentActivity);



        listenerAdapter = new ListenerAdapter(this);
        CallJavaFunc("SetUnityListener",listenerAdapter);
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
    public bool CallFromUnity(string funcName,string value)
    {
        return CallJavaFunc<bool>("CallFromUnity",funcName,value);

    }

    //Listen call From Android Java
    public void onMessage(string funcName,string value)
    {
        Debug.Log(string.Format("Call From Android funcName : {0}" ,funcName));
        if(functionDict.ContainsKey(funcName))
        {
            functionDict[funcName](value);
        }
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


