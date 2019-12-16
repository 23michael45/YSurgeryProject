package com.yuji.face;

import android.app.Activity;
import android.util.Log;
import android.view.WindowManager;


import org.json.JSONException;
import org.json.JSONObject;

import java.util.Arrays;
import java.util.List;

interface IYSurgeryUnityListener {

    String onCalculateLowPolyFace(BytesWrapper ojbDataWrapper,int gender,float height,float weight);
    boolean onLoadLowPolyFace(String roleJson,BytesWrapper textureDataWrapper);
    String onSaveDeform();
    boolean onLoadDeform(String deformJson);
    String onSaveAvatar();
    boolean onLoadAvatar(String avatarJson);

    void onEnterEditMode();
    void onExitEditMode();
    void onFullScreen(boolean b);
}


public class YSurgeryUnityInterface {

    public  static YSurgeryUnityInterface instance;
    private Activity parentActivity;
    private IYSurgeryUnityListener unityListener;

    public YSurgeryUnityInterface(Activity activity) {
        instance = this;
        this.parentActivity = activity;
    }

    public void SetUnityListener(IYSurgeryUnityListener listener) {
        Log.e("YSurgeryUnityInterface","SetUnityListener Success");
        unityListener = listener;
    }

    public String CallFromUnity(String funcName, String Value)
    {
        return "";
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Java端使用的接口函数

    //description : 服务器计算的高模转成低模，生成roleJson数据结构
    //param1 ：hdObjData  服务机计算生成的obj文件二进制bytes数据
    //param2 ：gender  性别  0 男  1 女
    //param3 ：height  身高 cm
    //param4 ：weight  体重 kg
    //return :  json字符串，roleJson数据结构
    public String CalculateLowPolyFace(byte[] hdObjData,int gender,float height,float weight)
    {
        return unityListener.onCalculateLowPolyFace(new BytesWrapper(hdObjData),gender,height,weight);
    }

    //description : 读取roleJson数据结构及贴图，把生成的低模模型显示在屏幕上
    //param1 ： 计算生成的低模json数据字符串
    //param2 ： 服务机计算生成的贴图jpg二进制bytes
    //return :  读取成功或失败
    public boolean LoadLowPolyFace(String roleJson,byte[] textureData)
    {
        return unityListener.onLoadLowPolyFace(roleJson,new BytesWrapper(textureData));
    }


    //description : 保存当前模型变型数据到deformJson数据结构
    //return :  json字符串，deformJson数据
    public String SaveDeform()
    {
        return unityListener.onSaveDeform();
    }


    //description : 读取deformJson数据结构,读取到当前模型
    //param1 ： 低模变形deformJson数据字符串
    //return :  读取成功或失败
    public boolean LoadDeform(String deformJson)
    {
        return unityListener.onLoadDeform(deformJson);

    }

    //description : 保存当前模型换状数据到avatarJson数据结构
    //return :  json字符串，avatarJson数据
    public String SaveAvatar()
    {
        return unityListener.onSaveAvatar();

    }

    //description : 读取avatarJson数据结构,读取到当前模型
    //param1 ： 低模换装avatarJson数据字符串
    //return :  读取成功或失败
    public boolean LoadAvatar(String avatarJson)
    {

        return unityListener.onLoadAvatar(avatarJson);
    }

    //description : 进入编辑模式
    public void EnterEditMode()
    {
        unityListener.onEnterEditMode();
    }

    //description : 退出编辑模式
    public void ExitEditMode()
    {
        unityListener.onExitEditMode();
    }

    //description : 退出编辑模式
    public void FullScreen(Activity activity,boolean b)
    {
//        unityListener.onFullScreen(b);
        full(activity,b);
    }



    private void full(Activity activity,boolean enable) {
        if (enable) {
            WindowManager.LayoutParams lp =  activity.getWindow().getAttributes();
            lp.flags |= WindowManager.LayoutParams.FLAG_FULLSCREEN;
            activity.getWindow().setAttributes(lp);
            activity.getWindow().addFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
        } else {
            WindowManager.LayoutParams attr = activity.getWindow().getAttributes();
            attr.flags &= (~WindowManager.LayoutParams.FLAG_FULLSCREEN);
            activity.getWindow().setAttributes(attr);
            activity.getWindow().clearFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
        }
    }
}
