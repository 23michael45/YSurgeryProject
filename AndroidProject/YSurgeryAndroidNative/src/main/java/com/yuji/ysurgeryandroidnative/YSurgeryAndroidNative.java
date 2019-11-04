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
    void onMessage(String sValue, int iValue);
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


    public boolean CallFromUnity(String sValue, int iValue) {
        unityListener.onMessage(sValue, 1234);
        return true;
    }


    public void TestCallUnityFunction(String sValue, int iValue) {

        unityListener.onMessage(sValue, 1234);
    }
}

