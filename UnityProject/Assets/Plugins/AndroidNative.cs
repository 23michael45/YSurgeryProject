using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IYSurgeryUnityListener
{
    void onMessage(string sValue, int iValue);
}
public class ListenerAdapter : AndroidJavaProxy
{
    private readonly IYSurgeryUnityListener listener;
    public ListenerAdapter(IYSurgeryUnityListener listener) : base("com.yuji.ysurgeryandroidnative.IYSurgeryUnityListener")
    {
        this.listener = listener;
    }
    void onMessage(string sValue, int iValue)
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
    public bool CallFromUnity(string sValue,int iValue)
    {
        return CallJavaFunc<bool>("CallFromUnity",sValue,iValue);

    }

    //Listen call From Android Java
    public void onMessage(string sValue,int iValue)
    {
        Debug.Log("Call From Android:" + sValue);
    }
}


