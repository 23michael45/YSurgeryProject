using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NativeToUnityParam
{
    public List<string> paramlist;
}

public class AndroidNativeInterface : MonoBehaviour
{
    AndroidNative mAndroidNativeUtils = new AndroidNative();
    void Awake()
    {
        mAndroidNativeUtils.RegisterFunction("CalculateLowPolyFace", CalculateLowPolyFace);
        mAndroidNativeUtils.RegisterFunction("LoadLowPolyFace", LoadLowPolyFace);
        mAndroidNativeUtils.RegisterFunction("SaveDeform", SaveDeform);
        mAndroidNativeUtils.RegisterFunction("LoadDeform", LoadDeform);
        mAndroidNativeUtils.RegisterFunction("SaveAvatar", SaveAvatar);
        mAndroidNativeUtils.RegisterFunction("LoadAvatar", LoadAvatar);


    }
    void OnDestroy()
    {
        mAndroidNativeUtils.UnregisterFunction("CalculateLowPolyFace");
        mAndroidNativeUtils.UnregisterFunction("LoadLowPolyFace");
        mAndroidNativeUtils.UnregisterFunction("SaveDeform");
        mAndroidNativeUtils.UnregisterFunction("LoadDeform");
        mAndroidNativeUtils.UnregisterFunction("SaveAvatar");
        mAndroidNativeUtils.UnregisterFunction("LoadAvatar");
    }



    /////////////////////////////////////////////////////////////////////////////////////////
    //函数入参以json字串的方式传入，格式示例如下
    //{"paramlist":["xxx/xxx/x.obj", "xxx/xxx/tex.jpg",...]}   
    //paramlist里为多个string型参数，详细请见下函数注释 
    /////////////////////////////////////////////////////////////////////////////////////////




    //description : 服务器计算的高模转成低模，生成roleJson数据结构
    //param1 ： 服务机计算生成的obj在本地磁盘上的文件地址   xxx/xxx/hd.obj
    //return :  json字符串，roleJson数据结构
    string CalculateLowPolyFace(string value)
    {
        NativeToUnityParam param = JsonUtility.FromJson<NativeToUnityParam>(value);
        string hdObjPath = param.paramlist[0];

        return ModelDataManager.Instance.CalculateLowPolyFace(hdObjPath);

    }



    //description : 读取roleJson数据结构及贴图，把生成的低模模型显示在屏幕上
    //param1 ： 计算生成的低模json数据 在本地磁盘上的文件地址  xxx/xxx/lowpoly1.json
    //param2 ： 服务机计算生成的贴图jpg在本地磁盘上的文件地址  xxx/xxx/texture1.jpg
    //return :  "True" 或 "False" ,读取成功或失败
    string LoadLowPolyFace(string value)
    {
        NativeToUnityParam param = JsonUtility.FromJson<NativeToUnityParam>(value);
        string roleJson = param.paramlist[0];
        string texturePath = param.paramlist[1];

        bool ret = ModelDataManager.Instance.LoadLowPolyFace(roleJson, texturePath);

        if (ret)
        {
            return "True";
        }
        else
        {
            return "False";
        }
    }

    //description : 保存当前模型变型数据到deformJson数据结构
    //return :  json字符串，deformJson数据
    string SaveDeform(string value)
    {
        return "";
    }


    //description : 读取deformJson数据结构,读取到当前模型
    //param1 ： 低模变形deformJson数据 在本地磁盘上的文件地址  xxx/xxx/deform1.json
    //return :  "True" 或 "False" ,读取成功或失败
    string LoadDeform(string value)
    {
        NativeToUnityParam param = JsonUtility.FromJson<NativeToUnityParam>(value);
        string deformJson = param.paramlist[0];
        return "";
    }


    //description : 保存当前模型换状数据到avatarJson数据结构
    //return :  json字符串，avatarJson数据
    string SaveAvatar(string value)
    {
        return "";
    }

    //description : 读取avatarJson数据结构,读取到当前模型
    //param1 ： 低模换装avatarJson数据 在本地磁盘上的文件地址  xxx/xxx/avatar1.json
    //return :  "True" 或 "False" ,读取成功或失败
    string LoadAvatar(string value)
    {
        NativeToUnityParam param = JsonUtility.FromJson<NativeToUnityParam>(value);
        string avatarJson = param.paramlist[0];
        return "";

    }

}
