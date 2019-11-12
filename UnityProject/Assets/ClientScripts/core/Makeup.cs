using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makeup : MonoBehaviour
{




    //    public void SetMakeup(MakeupPart makeup, int id)
    //    {
    //        MakeupDef def = GetMakeupDef(makeup, id);

    //     //   Debug.Log(def.assetbundle);

    //        if (def != null)
    //        {

    //            List<object> paramlist = new List<object>();
    //            paramlist.Add(def);
    //            paramlist.Add(id);
    //            paramlist.Add(makeup);
    //            BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoadedMakeup, paramlist, AssetBundleLoadManager.m_FromHttp);

    //        }

    //    }
    //    void OnLoadedMakeup(object obj,object param)
    //    {
    //        Texture makeuptexture = obj as Texture;

    //        List<object> paramlist = param as List<object>;

    //        MakeupDef def = paramlist[0] as MakeupDef;
    //        int id = (int)paramlist[1];
    //        MakeupPart makeup = (MakeupPart)paramlist[2];
    //        if (makeuptexture == null)
    //        {
    //            Debug.LogError("MakeupPart load file failed! : " + def.assetbundle);
    //            return;
    //        }

    //        if (Makeupparts[(int)makeup] != null)
    //            GameObject.DestroyImmediate(Makeupparts[(int)makeup]);

    //        // Debug.Log(Makeupparts[(int)makeup].width);


    //        if (makeup == MakeupPart.Eye)
    //        {
    //            _eyes.GetComponent<Renderer>().material.SetTexture("_Diffusemap", makeuptexture);// mainTexture = makeuptexture as Texture2D;

    //        }
    //        else if (makeup == MakeupPart.BaseColor)
    //        {

    //            _head.GetComponent<Renderer>().material.SetTexture("_Skincolor", makeuptexture as Texture2D);
    //        }

    //        else if (makeup == MakeupPart.EyeBrow)
    //        {


    //            // Debug.Log(makeuptexture.width );

    //            if (makeuptexture.width == 256)
    //            {
    //                _head.GetComponent<Renderer>().material.SetFloat("_EyebrowValue", 0);

    //             //   Debug.Log(makeuptexture.width);
    //            }
    //            else
    //            {
    //                _head.GetComponent<Renderer>().material.SetFloat("_EyebrowValue", 1);
    //             //   Debug.Log(makeuptexture.width);
    //            }
    //            _head.GetComponent<Renderer>().material.SetTexture("_Eyebrow", makeuptexture as Texture2D);

    //        }

    //        else if (makeup == MakeupPart.EyeLash)
    //        {

    //            _eyelash.GetComponent<Renderer>().material.mainTexture = makeuptexture as Texture2D;
    //        }

    //        else if (makeup == MakeupPart.Eyeshadow)
    //        {

    //            _head.GetComponent<Renderer>().material.SetTexture("_Eyeshadow", makeuptexture as Texture2D);
    //        }
    //        else if (makeup == MakeupPart.FaceRed)
    //        {
    //            _head.GetComponent<Renderer>().material.SetTexture("_Facered", makeuptexture as Texture2D);

    //        }
    //        else if (makeup == MakeupPart.Mouth)
    //        {
    //            _head.GetComponent<Renderer>().material.SetTexture("_Lip", makeuptexture as Texture2D);

    //        }
    //        else if (makeup == MakeupPart.FaceTatoo)
    //        {
    //            _head.GetComponent<Renderer>().material.SetTexture("_FaceTatoo", makeuptexture as Texture2D);

    //        }
    //        else if (makeup == MakeupPart.BodyTatoo)
    //        {
    //            _body.GetComponent<Renderer>().material.SetTexture("_SkinTatoo1", makeuptexture as Texture2D);

    //        }
    //        else if (makeup == MakeupPart.Finger)
    //        {

    //            _finger.GetComponent<Renderer>().material.mainTexture = makeuptexture as Texture2D;
    //        }

    //    }














    //肤色调节//。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    //    public void SetPhotoBright(float xx) {
    //        _head.GetComponent<Renderer>().material.SetFloat("_FaceBright", xx);
    //    }

    //    public void SetSkincolorBase(float xx)   //基本色
    //    {

    //        int colornum = (int)xx;

    //        int _Hue = (int)(_skincolors[colornum].x - 24f);
    //        float _Sat = _skincolors[colornum].y + 0.88f;
    //        float _Val = _skincolors[colornum].z + 0.34f;

    //        _body.GetComponent<Renderer>().material.SetFloat("_Hue", _Hue);
    //        _body.GetComponent<Renderer>().material.SetFloat("_Saturation", _Sat);
    //        _body.GetComponent<Renderer>().material.SetFloat("_Value", _Val);


    //        _arm .GetComponent<Renderer>().material.SetFloat("_Hue", _Hue);
    //        _arm.GetComponent<Renderer>().material.SetFloat("_Saturation", _Sat);
    //        _arm.GetComponent<Renderer>().material.SetFloat("_Value", _Val);


    //        _head.GetComponent<Renderer>().material.SetFloat("_Hue", _Hue);
    //        _head.GetComponent<Renderer>().material.SetFloat("_Saturation", _Sat);
    //        _head.GetComponent<Renderer>().material.SetFloat("_Value", _Val);

    //       //Debug.Log(_Hue);
    //        // Debug.Log(_skincolors[66].x );
    //    }
    //    public void SetSkincolorSecai(float xx)   //色彩
    //    {
    //        float _hh;

    //        if (xx < 0)
    //        {

    //            _hh = 360 + xx;
    //        }

    //        else {
    //            _hh = xx;
    //        }

    //        _body.GetComponent<Renderer>().material.SetFloat("_Hue", _hh);
    //        _arm.GetComponent<Renderer>().material.SetFloat("_Hue", _hh);
    //        _head.GetComponent<Renderer>().material.SetFloat("_Hue", _hh);
    //    }
    //    public void SetSkincolorXianyan(float xx)   //鲜艳
    //    {

    //        _body.GetComponent<Renderer>().material.SetFloat("_Saturation", xx);
    //        _arm.GetComponent<Renderer>().material.SetFloat("_Saturation", xx);
    //        _head.GetComponent<Renderer>().material.SetFloat("_Saturation", xx);

    //    }                               
    //    public void SetSkincolorMingan(float xx)   //明暗
    //    {
    //        _body.GetComponent<Renderer>().material.SetFloat("_Value", xx);
    //        _arm.GetComponent<Renderer>().material.SetFloat("_Value", xx);
    //        _head.GetComponent<Renderer>().material.SetFloat("_Value", xx);
    //    }
    //    //化妆。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    //    public MakeupDef GetMakeupDef(MakeupPart makeup, int id)
    //    {
    //        if (AppRoot.MainUser.isman)
    //        {

    //            if (makeup == MakeupPart.Eye)
    //            {
    //                if (TableMgr.Instance.M_EyesDic != null && TableMgr.Instance.M_EyesDic.ContainsKey(id))
    //                    //Debug.Log(id);
    //                    return TableMgr.Instance.M_EyesDic[id];
    //            }
    //            if (makeup == MakeupPart.BaseColor)
    //            {
    //                if (TableMgr.Instance.M_BaseColorDic != null && TableMgr.Instance.M_BaseColorDic.ContainsKey(id))
    //                    //  Debug.Log(id);
    //                    return TableMgr.Instance.M_BaseColorDic[id];
    //            }
    //            if (makeup == MakeupPart.EyeBrow)
    //            {
    //                if (TableMgr.Instance.M_EyeBrowsDic != null && TableMgr.Instance.M_EyeBrowsDic.ContainsKey(id))
    //                    // Debug.Log(id);
    //                    return TableMgr.Instance.M_EyeBrowsDic[id];
    //            }
    //            if (makeup == MakeupPart.Eyeshadow)
    //            {
    //                if (TableMgr.Instance.M_EyeShadowsDic != null && TableMgr.Instance.M_EyeShadowsDic.ContainsKey(id))
    //                    //  Debug.Log(id);
    //                    return TableMgr.Instance.M_EyeShadowsDic[id];
    //            }
    //            if (makeup == MakeupPart.EyeLash)
    //            {
    //                if (TableMgr.Instance.M_EyeLashsDic != null && TableMgr.Instance.M_EyeLashsDic.ContainsKey(id))
    //                    //   Debug.Log(id);
    //                    return TableMgr.Instance.M_EyeLashsDic[id];
    //            }
    //            if (makeup == MakeupPart.FaceRed)
    //            {
    //                if (TableMgr.Instance.M_FaceRedsDic != null && TableMgr.Instance.M_FaceRedsDic.ContainsKey(id))
    //                    //  Debug.Log(id);
    //                    return TableMgr.Instance.M_FaceRedsDic[id];
    //            }
    //            if (makeup == MakeupPart.Mouth)
    //            {
    //                if (TableMgr.Instance.M_MouthsDic != null && TableMgr.Instance.M_MouthsDic.ContainsKey(id))
    //                    //    Debug.Log(id);
    //                    return TableMgr.Instance.M_MouthsDic[id];
    //            }
    //            if (makeup == MakeupPart.FaceTatoo)
    //            {
    //                if (TableMgr.Instance.M_FaceTatoosDic != null && TableMgr.Instance.M_FaceTatoosDic.ContainsKey(id))
    //                    //    Debug.Log(id);
    //                    return TableMgr.Instance.M_FaceTatoosDic[id];
    //            }
    //            if (makeup == MakeupPart.BodyTatoo)
    //            {
    //                if (TableMgr.Instance.M_BodyTatoosDic != null && TableMgr.Instance.M_BodyTatoosDic.ContainsKey(id))
    //                    //      Debug.Log(id);
    //                    return TableMgr.Instance.M_BodyTatoosDic[id];
    //            }
    //            if (makeup == MakeupPart.Finger)
    //            {
    //                if (TableMgr.Instance.M_FingersDic != null && TableMgr.Instance.M_FingersDic.ContainsKey(id))
    //                    //     Debug.Log(id);
    //                    return TableMgr.Instance.M_FingersDic[id];
    //            }



    //            return null;
    //        }
    //        else
    //        {
    //            if (makeup == MakeupPart.Eye)
    //            {
    //                if (TableMgr.Instance.W_EyesDic != null && TableMgr.Instance.W_EyesDic.ContainsKey(id))
    //                    //Debug.Log(id);
    //                    return TableMgr.Instance.W_EyesDic[id];
    //            }
    //            if (makeup == MakeupPart.BaseColor)
    //            {
    //                if (TableMgr.Instance.W_BaseColorDic != null && TableMgr.Instance.W_BaseColorDic.ContainsKey(id))
    //                    //  Debug.Log(id);
    //                    return TableMgr.Instance.W_BaseColorDic[id];
    //            }
    //            if (makeup == MakeupPart.EyeBrow)
    //            {
    //                if (TableMgr.Instance.W_EyeBrowsDic != null && TableMgr.Instance.W_EyeBrowsDic.ContainsKey(id))
    //                    // Debug.Log(id);
    //                    return TableMgr.Instance.W_EyeBrowsDic[id];
    //            }
    //            if (makeup == MakeupPart.Eyeshadow)
    //            {
    //                if (TableMgr.Instance.W_EyeShadowsDic != null && TableMgr.Instance.W_EyeShadowsDic.ContainsKey(id))
    //                    //  Debug.Log(id);
    //                    return TableMgr.Instance.W_EyeShadowsDic[id];
    //            }
    //            if (makeup == MakeupPart.EyeLash)
    //            {
    //                if (TableMgr.Instance.W_EyeLashsDic != null && TableMgr.Instance.W_EyeLashsDic.ContainsKey(id))
    //                    //   Debug.Log(id);
    //                    return TableMgr.Instance.W_EyeLashsDic[id];
    //            }
    //            if (makeup == MakeupPart.FaceRed)
    //            {
    //                if (TableMgr.Instance.W_FaceRedsDic != null && TableMgr.Instance.W_FaceRedsDic.ContainsKey(id))
    //                    //  Debug.Log(id);
    //                    return TableMgr.Instance.W_FaceRedsDic[id];
    //            }
    //            if (makeup == MakeupPart.Mouth)
    //            {
    //                if (TableMgr.Instance.W_MouthsDic != null && TableMgr.Instance.W_MouthsDic.ContainsKey(id))
    //                    //    Debug.Log(id);
    //                    return TableMgr.Instance.W_MouthsDic[id];
    //            }
    //            if (makeup == MakeupPart.FaceTatoo)
    //            {
    //                if (TableMgr.Instance.W_FaceTatoosDic != null && TableMgr.Instance.W_FaceTatoosDic.ContainsKey(id))
    //                    //    Debug.Log(id);
    //                    return TableMgr.Instance.W_FaceTatoosDic[id];
    //            }
    //            if (makeup == MakeupPart.BodyTatoo)
    //            {
    //                if (TableMgr.Instance.W_BodyTatoosDic != null && TableMgr.Instance.W_BodyTatoosDic.ContainsKey(id))
    //                    //      Debug.Log(id);
    //                    return TableMgr.Instance.W_BodyTatoosDic[id];
    //            }
    //            if (makeup == MakeupPart.Finger)
    //            {
    //                if (TableMgr.Instance.W_FingersDic != null && TableMgr.Instance.W_FingersDic.ContainsKey(id))
    //                    //     Debug.Log(id);
    //                    return TableMgr.Instance.W_FingersDic[id];
    //            }


    //            return null;
    //        }
    //    }

}
