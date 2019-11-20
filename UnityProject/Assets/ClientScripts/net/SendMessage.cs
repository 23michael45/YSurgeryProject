using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;

public class SendMessage : MonoBehaviour
{
    AndroidNative mAndroidNativeUtils;

    public string ClothPartjson;
    public string Scencejson;
    public string Makeupjson;


    public string ModelPath;
    public string TexturePath;


    public User_Deform DeformNewst;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="loadjson"></param>

    //加载场景。。。。。。。。必须
    public void LoadScencejson(string Scenejson)
    {

        Debug.Log(Scenejson);
    }




    public void LoadModel(string modelstring)
    {

        Debug.Log(modelstring);

    }







    //进入编辑。。。。。。。。。

    //加载初始模型信息

    public void LoadRolejson(string Rolejson)
    {


        Debug.Log(Rolejson);
    }


    //加载最新一次编辑信息
    public void LoadDeformJson(string Deformjson)
    {


        Debug.Log(Deformjson);
    }


    //加载初始配饰信息

    //加载当前配饰信息   包含发型、服装、化妆
    public void LoadOrnamentjson(string Ornamentjson)
    {


        Debug.Log(Ornamentjson);
    }






    //加载服装列表
    public void LoadClothList(string ClothListjson)
    {
        Debug.Log(ClothListjson);

    }


    //加载化妆信息
    public void LoadMakeupList(string MakeupListjson)
    {


        Debug.Log(MakeupListjson);
    }


    //加载发型信息
    public void LoadHairListjson(string HairListjson)
    {
        Debug.Log(HairListjson);


    }


    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="savejson"></param>




    //保存当前场景。。。。。。。
    public void SaveScencejson(string Scenejson)
    {

        mAndroidNativeUtils = new AndroidNative();
        mAndroidNativeUtils.CallFromUnity("SaveScencejson", Scenejson);

    }



    //保存模型信息。。。。。。。。。   
    public void SaveRolejson(string Rolejson)
    {

        mAndroidNativeUtils = new AndroidNative();
        mAndroidNativeUtils.CallFromUnity("SaveRolejson", Rolejson);
    }



    //保存当前编辑信息
    public void SaveDeformJson(string Deformjson)
    {

        mAndroidNativeUtils = new AndroidNative();
        mAndroidNativeUtils.CallFromUnity("SaveDeformJson", Deformjson);

    }



    //编辑另存为。。。。。。。
    public void SaveDeformAs(string Deformjson)

    {

        mAndroidNativeUtils = new AndroidNative();
        mAndroidNativeUtils.CallFromUnity("SaveDeformAs", Deformjson);
    }




    //保存当前配饰信息  包含发型、服装、化妆
    public void SaveOrnamentjson(string Ornamentjson)
    {
        mAndroidNativeUtils = new AndroidNative();
        mAndroidNativeUtils.CallFromUnity("SaveOrnamentjson", Ornamentjson);


    }


    /// <summary>
    /// 更新信息 /////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name=" Updatajson"></param>





    //更新服装列表
    public void UpdataClothList(string ClothListjson)
    {


    }



    //加载化妆信息
    public void UpdataMakeupList(string MakeupListjson)
    {



    }


    //加载发型信息
    public void UpdataHairListjson(string HairListjson)
    {



    }
















}







