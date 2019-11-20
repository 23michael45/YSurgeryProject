using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothPart
{
    Coat = 0,
    Tops = 1,
    Trousers = 2,
    Underwear = 3,
    Shoe = 4,
    Hair = 5,
    Hat = 6,
    Count
}

public enum MakeupPart
{
    Eye = 1,
    BaseColor = 2,
    EyeBrow = 3,
    EyeLash = 4,
    Eyeshadow = 5,
    FaceRed = 6,
    Mouth = 7,
    FaceTatoo = 8,
    BodyTatoo = 9,
    Finger = 10,
    Count
}


public class ChangPart : MonoBehaviour
{


    Role mainRole;


    //public ClothDef GetPartDef(ClothPart part, int id)
    //{
    //    if (AppRoot.MainUser.isman)
    //    {
    //        switch (part)
    //        {
    //            case ClothPart.Coat:
    //                if (TableMgr.Instance.M_CoatDic != null && TableMgr.Instance.M_CoatDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_CoatDic[id];
    //                break;

    //            case ClothPart.Tops:
    //                if (TableMgr.Instance.M_TopsDic != null && TableMgr.Instance.M_TopsDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_TopsDic[id];
    //                break;

    //            case ClothPart.Trousers:
    //                if (TableMgr.Instance.M_TrousersDic != null && TableMgr.Instance.M_TrousersDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_TrousersDic[id];
    //                break;
    //            case ClothPart.Underwear:
    //                if (TableMgr.Instance.M_UnderwearsDic != null && TableMgr.Instance.M_UnderwearsDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_UnderwearsDic[id];
    //                break;

    //            case ClothPart.Shoe:
    //                if (TableMgr.Instance.M_ShoesDic != null && TableMgr.Instance.M_ShoesDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_ShoesDic[id];
    //                break;

    //            case ClothPart.Hair:
    //                if (TableMgr.Instance.M_HairDic != null && TableMgr.Instance.M_HairDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_HairDic[id];
    //                break;
    //            case ClothPart.Hat:
    //                if (TableMgr.Instance.M_HatesDic != null && TableMgr.Instance.M_HatesDic.ContainsKey(id))
    //                    return TableMgr.Instance.M_HatesDic[id];
    //                break;

    //        }

    //        return null;

    //    }
    //    else
    //    {

    //        if (part == ClothPart.Coat)
    //        {
    //            if (TableMgr.Instance.W_CoatDic != null && TableMgr.Instance.W_CoatDic.ContainsKey(id))

    //                return TableMgr.Instance.W_CoatDic[id];
    //        }

    //        if (part == ClothPart.Tops)
    //        {
    //            if (TableMgr.Instance.W_TopsDic != null && TableMgr.Instance.W_TopsDic.ContainsKey(id))
    //                return TableMgr.Instance.W_TopsDic[id];
    //        }

    //        if (part == ClothPart.Trousers)
    //        {
    //            if (TableMgr.Instance.W_TrousersDic != null && TableMgr.Instance.W_TrousersDic.ContainsKey(id))
    //                return TableMgr.Instance.W_TrousersDic[id];

    //        }
    //        if (part == ClothPart.Underwear)
    //        {
    //            if (TableMgr.Instance.W_UnderwearsDic != null && TableMgr.Instance.W_UnderwearsDic.ContainsKey(id))
    //                return TableMgr.Instance.W_UnderwearsDic[id];
    //        }
    //        if (part == ClothPart.Shoe)
    //        {
    //            if (TableMgr.Instance.W_ShoesDic != null && TableMgr.Instance.W_ShoesDic.ContainsKey(id))
    //                return TableMgr.Instance.W_ShoesDic[id];
    //        }
    //        if (part == ClothPart.Hair)
    //        {
    //            if (TableMgr.Instance.W_HairDic != null && TableMgr.Instance.W_HairDic.ContainsKey(id))
    //                return TableMgr.Instance.W_HairDic[id];

    //        }
    //        if (part == ClothPart.Hat)
    //        {
    //            if (TableMgr.Instance.W_HatesDic != null && TableMgr.Instance.W_HatesDic.ContainsKey(id))
    //                return TableMgr.Instance.W_HatesDic[id];
    //        }

    //        return null;
    //    }
    //}
    //public void SetPart(ClothPart part, int id)
    //{
    //    ClothDef def = GetPartDef(part, id);
    //    // Debug.Log(def.name  );

    //    if (def != null)
    //    {

    //        BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoaded_SetPart, part, AssetBundleLoadManager.m_FromHttp);


    //    }
    //}
    //void OnLoaded_SetPart(object obj, object param)
    //{
    //    ClothPart part = (ClothPart)param;
    //    GameObject prefab = obj as GameObject;
    //    if (prefab == null)
    //    {
    //        return;
    //    }

    //    if (parts[(int)part] != null)
    //        GameObject.DestroyImmediate(parts[(int)part]);
    //    parts[(int)part] = Instantiate(prefab);
    //    parts[(int)part].transform.parent = this.transform;
    //    RepalceBones(parts[(int)part].GetComponent<SkinFBX>());
    //}


    //public ClothDef GetPartDef_M(ClothPart part, int id)
    //{
    //    switch (part)
    //    {
    //        case ClothPart.Coat:
    //            if (TableMgr.Instance.M_CoatDic != null && TableMgr.Instance.M_CoatDic.ContainsKey(id))
    //                return TableMgr.Instance.M_CoatDic[id];
    //            break;

    //        case ClothPart.Tops:
    //            if (TableMgr.Instance.M_TopsDic != null && TableMgr.Instance.M_TopsDic.ContainsKey(id))
    //                return TableMgr.Instance.M_TopsDic[id];
    //            break;

    //        case ClothPart.Trousers:
    //            if (TableMgr.Instance.M_TrousersDic != null && TableMgr.Instance.M_TrousersDic.ContainsKey(id))
    //                return TableMgr.Instance.M_TrousersDic[id];
    //            break;
    //        case ClothPart.Underwear:
    //            if (TableMgr.Instance.M_UnderwearsDic != null && TableMgr.Instance.M_UnderwearsDic.ContainsKey(id))
    //                return TableMgr.Instance.M_UnderwearsDic[id];
    //            break;

    //        case ClothPart.Shoe:
    //            if (TableMgr.Instance.M_ShoesDic != null && TableMgr.Instance.M_ShoesDic.ContainsKey(id))
    //                return TableMgr.Instance.M_ShoesDic[id];
    //            break;

    //        case ClothPart.Hair:
    //            if (TableMgr.Instance.M_HairDic != null && TableMgr.Instance.M_HairDic.ContainsKey(id))
    //                return TableMgr.Instance.M_HairDic[id];
    //            break;
    //        case ClothPart.Hat:
    //            if (TableMgr.Instance.M_HatesDic != null && TableMgr.Instance.M_HatesDic.ContainsKey(id))
    //                return TableMgr.Instance.M_HatesDic[id];
    //            break;

    //    }

    //    return null;
    //}
    //public void SetPart_M(ClothPart part, int id)
    //{
    //    ClothDef def = GetPartDef_M(part, id);

    //    if (def != null)
    //    {
    //        GameObject prefab = ResourceMgr.Instance.LoadFromAssetBundle<GameObject>(def.assetbundle);

    //        BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoaded_SetPart_M, part, AssetBundleLoadManager.m_FromHttp);

    //    }
    //}
    //void OnLoaded_SetPart_M(object obj, object param)
    //{
    //    ClothPart part = (ClothPart)param;
    //    GameObject prefab = obj as GameObject;
    //    if (prefab == null)
    //    {
    //        return;
    //    }

    //    if (parts[(int)part] != null)
    //        GameObject.DestroyImmediate(parts[(int)part]);
    //    parts[(int)part] = Instantiate(prefab);
    //    parts[(int)part].transform.parent = this.transform;
    //    RepalceBones(parts[(int)part].GetComponent<SkinFBX>());
    //}







    //    public SkinDef GetSkinDef(int id)
    //    {
    //        if (AppRoot.MainUser.isman)
    //        {
    //            if (TableMgr.Instance.M_skinDic != null && TableMgr.Instance.M_skinDic.ContainsKey(id))
    //            {
    //                return TableMgr.Instance.M_skinDic[id];
    //            }

    //            return null;
    //        }
    //        else {        

    //        if (TableMgr.Instance.W_skinDic  != null && TableMgr.Instance.W_skinDic.ContainsKey(id))
    //        {
    //            return TableMgr.Instance.W_skinDic[id];
    //        }

    //        return null;
    //        }
    //    }
    //    public void SetSkin(int id, object param)    {

    //        SkinDef def = GetSkinDef(id);

    //        int i = (int)param; 

    //        if (def != null)
    //        {
    //            if (i == 0)
    //            {
    //                BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle_face, ONFAceskinset);
    //            }
    //            else {
    //                BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle_body, ONBodyskinset);
    //            }
    //        }
    //    }
    //    void ONFAceskinset(object obj, object param) {
    //        Texture2D skin_face = obj as Texture2D;
    //      //  Debug.Log(skin_face);   
    //        _head.GetComponent<Renderer>().material.SetTexture("_Diffusemap", skin_face as Texture2D);   

    //    }
    //    void ONBodyskinset(object obj, object param)
    //    {
    //        Texture2D skin_body = obj as Texture2D;   
    //        _body.GetComponent<Renderer>().material.SetTexture("_Diffusemap", skin_body as Texture2D);
    //       _arm.GetComponent<Renderer>().material.SetTexture("_Diffusemap", skin_body as Texture2D);
    //    }

    //    void FaceButtonimage(string path,string faceid ) {

    //        AppRoot.MainUser.faceImgUrl = path;
    //        AppRoot.MainUser.faceshotImg = path;      
    //      //  Debug.Log(faceid);
    //        GameObject.Find("UI").GetComponent<ModelMgr>().SetModelButtonIMG(faceid, path);


    //    }













}
