package com.yuji.ysurgeryandroidnative;

import android.Manifest;
import android.app.Activity;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageManager;
import android.os.BatteryManager;
import android.os.Build;
import android.os.Environment;
import android.support.annotation.RequiresApi;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.util.Log;

import java.io.File;

interface IYSurgeryUnityListener {
    String onMessage(String funcName, String value);
}


public class YSurgeryAndroidNative {

    private Activity parentActivity;
    private IYSurgeryUnityListener unityListener;

    public YSurgeryAndroidNative(Activity activity) {
        this.parentActivity = activity;
    }

    public void SetUnityListener(IYSurgeryUnityListener listener) {
        unityListener = listener;
    }


    public String CallFromUnity(String funcName, String Value) {
        return unityListener.onMessage(funcName, Value);
    }


    public String CallUnityFunction(String funcName, String value) {

        return unityListener.onMessage(funcName, value);
    }


}

