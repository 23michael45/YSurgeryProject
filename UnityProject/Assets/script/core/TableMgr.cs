using System;
using System.IO;

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[Serializable]
public class TableDef
{
    public int id;
}



[Serializable]
public class FacebonesDef
{
    public Vector3 Head, head_M_scale, face_bridge_joint1, face_bridge_Lf_joint1, face_bridge_Rt_joint1, face_brow_Lf_joint0, face_brow_Lf_joint1, face_brow_Lf_joint2, face_brow_Lf_joint3, face_brow_Lf_joint4, face_brow_Lf_joint5, face_brow_Rt_joint0, face_brow_Rt_joint1, face_brow_Rt_joint2, face_brow_Rt_joint3, face_brow_Rt_joint4, face_brow_Rt_joint5, face_calvaria_joint1, face_check_Lf_joint1, face_check_Lf_joint2, face_check_Lf_joint3, face_check_Lf_joint4, face_check_Rt_joint1, face_check_Rt_joint2, face_check_Rt_joint3, face_check_Rt_joint4, face_chin_Lf_joint06, face_chin_Lf_joint07, face_chin_Lf_joint08, face_chin_Lf_joint09, face_chin_Rt_joint06, face_chin_Rt_joint07, face_chin_Rt_joint08, face_chin_Rt_joint09, face_ear_Lf_joint1, face_ear_Lf_joint2, face_ear_Rt_joint1, face_ear_Rt_joint2, face_eyeLidsdown_Rt_joint0, face_eyeLids_Rt_joint1, face_eyeLids_Rt_joint2, face_eyeLidsdown_Rt_joint1, face_eyeLidsdown_Rt_joint2, face_eyeLidsdown_Rt_joint3, face_eyeLidsUp_Rt_joint1, face_eyeLidsUp_Rt_joint2, face_eyeLidsUp_Rt_joint3, face_eyeLidsUp_Lf_joint0, face_eyeLids_Lf_joint1, face_eyeLids_Lf_joint2, face_eyeLidsdown_Lf_joint1, face_eyeLidsdown_Lf_joint2, face_eyeLidsdown_Lf_joint3, face_eyeLidsUp_Lf_joint1, face_eyeLidsUp_Lf_joint2, face_eyeLidsUp_Lf_joint3, face_forehead_joint1, face_forehead_joint2, face_forehead_Lf_joint1, face_forehead_Lf_joint2, face_forehead_Lf_joint3, face_forehead_Lf_joint4, face_forehead_Lf_joint5, face_forehead_Rt_joint1, face_forehead_Rt_joint2, face_forehead_Rt_joint5, face_L_check_Bone002, face_mouthLip_joint0, face_mouthLip_up_joint0, face_mouthLip_Lf_joint1, face_mouthLip_Lf_joint2, face_mouthLip_Lf_joint4, face_mouthLip_Lf_joint5, face_mouthLip_Rt_joint1, face_mouthLip_Rt_joint2, face_mouthLip_Rt_joint4, face_mouthLip_Rt_joint5, face_mouthLip_up_joint1, face_mouthLip_up_joint2, face_nose_bone, face_bridge_joint2, face_nose_joint0, face_nose_joint1, face_nose_joint2, face_nosewing_Lf_joint1, face_nosewing_Lf_joint2, face_nosewing_Lf_joint003, face_nosewing_Rt_joint1, face_nosewing_Rt_joint2, face_nosewing_Rt_joint003, face_Orbicular_Lf_joint3, face_Orbicular_Rt_joint3, face_R_check_Bone002, face_temple_Lf_joint1, face_temple_Lf_joint2, face_temple_Lf_joint3, face_temple_Lf_joint004, face_temple_Rt_joint1, face_temple_Rt_joint2, face_temple_Rt_joint3, face_temple_Rt_joint004, face_tooth_down_joint1, face_tooth_down_joint2, face_tooth_down_joint3, face_tooth_up_joint1, face_tooth_up_joint2, face_tooth_up_joint3, Jaw, face_chin_joint1, face_chin_joint2, face_chin_joint3, face_mouthLip_dn_joint0, face_mouthLip_dn_joint1, face_mouthLip_dn_joint2, face_mouthLip_Lf_joint3, face_mouthLip_Lf_joint6, face_mouthLip_Lf_joint7, face_mouthLip_Rt_joint3, face_mouthLip_Rt_joint6, face_mouthLip_Rt_joint7, face_Orbicular_joint1, face_Orbicular_Lf_joint1, face_Orbicular_Lf_joint2, face_Orbicular_Rt_joint1, face_Orbicular_Rt_joint2, face_TongueBack, face_TongueTip, Jaw_M, face_chin_Lf_joint02, face_chin_Lf_joint03, face_chin_Lf_joint04, face_chin_Lf_joint05, face_chin_Rt_joint02, face_chin_Rt_joint03, face_chin_Rt_joint04, face_chin_Rt_joint05, L_eye_Bone, R_eye_Bone, neck_upper_scale;
}



// 身体 //
[Serializable]
public class RoleDef : TableDef
{

    public string name;
    public bool male;
    public string shortCutImage;

    public string Facemodel, Facetexture;

    public string assetbundle;  
    public float face_zero_pointy;    
       
    public FacebonesDef facebones;
    
}

// 变形 //
[Serializable]
public class DeformDef : TableDef
{
    public string AvatarId;
    [Serializable]
    public class Shape {
        public Vector4 ForeheadSwitch,TempleSwitch,BISjawSwitch, ChinSwitch;
    }

    [Serializable]
    public class Face
    {
        public Vector4 ApplemuscleSwitch, CheekbonesSwitch, FacialpartSwitch;
    }

    [Serializable]
    public class Eyebrow
    {
        public Vector4 BrowbowSwitch, BrowHeadSwitch, BrowMiddleSwitch, BrowTailSwitch;
    }


    [Serializable]
    public class Eye
    {
        public Vector4 EyecornerSwitch, UppereyelidSwitch, DoublefoldEyelidsSwitch, 
                        lowereyelidSwitch,EyebagSwitch,EyetailSwitch,BlackeyeSwitch;
    }

    [Serializable]
    public class Nose
    {
        public Vector4 UpperbridgeSwitch,InferiorbridgeSwitch,NoseheadSwitch,ColumellaNasiSwitch,
                        NasalBaseSwitch,NoseWingSwitch,NostrilSwitch;          
    }


    [Serializable]
    public class Mouth
    {
        public Vector4  UplipSwitch, UpjawSwitch,DownLipSwitch,DownJawSwitch,PhiltrumSwitch,CornerSwitch;
    }

    [Serializable]
    public class Chest
    {
        public Vector4 upperItemSwitch, topItemSwitch,downItemSwitch;
    }

    [Serializable]
    public class Body
    {
        public Vector4 NeckSwitch,ChestSwitch,WristSwitch,HipSwitch,LegSwitch,ArmSwitch,
                        ForeheadSwitch,BISjawSwitch,ChinSwitch;
    }

}


// 场景 //
public class ScenceDef : TableDef
{
    public string Stage_ID;
    public string Buttom_ID;  
    public string Background_ID; 

}




public class Config
{
    // public string 
}



public class TableMgr : MonoSingleton<TableMgr>
{
    // 身体 //
    public RoleDef[] Roles;
    public Dictionary<int, RoleDef> RoleDic;

    // 变形 //
    public DeformDef[] Deforms;
    public Dictionary<int, DeformDef> DeformsDic;


    // 环境 //
    public ScenceDef[] Scences;
    public Dictionary<int, ScenceDef> ScencesDic;
       



    string mTableBundle = "table/table";
   // LoadedAssetBundle mLoadedBundle = null;
    //AssetBundleLoadAssetAsynOperation.OnAssetBundleLoadedDelegate m_OnInit = null;
    IEnumerator LoadTableBundle(string bundlename)
    {
//#if UNITY_EDITOR
//        if (AssetBundleLoadManager.SimulateAssetBundleInEditor)
//        {

//        }
//        else
//        {
//            string error;
//            do
//            {
//                mLoadedBundle = AssetBundleLoadManager.Instance.GetLoadedAssetBundle(bundlename, out error);
                yield return 0;
//            } while (mLoadedBundle == null);
//        }
//#else
//                 string error;
//                    do
//                    {
//                        mLoadedBundle = AssetBundleLoadManager.Instance.GetLoadedAssetBundle(bundlename, out error);
//                        yield return 0;
//                    } while (mLoadedBundle == null);

//#endif

        // 身体 //
        Load<RoleDef>(out Roles, out RoleDic, "Role");

        // 场景 //
        Load<ScenceDef>(out Scences, out ScencesDic, "Scence");

        // 场景 //
        Load<DeformDef>(out Deforms, out DeformsDic, "Scence");


        //AssetBundleLoadManager.Instance.UnloadAssetBundle(bundlename,true);


        //if (m_OnInit != null)
        //{
        //     m_OnInit(null,null);
        //}
    }


    //public void Init(AssetBundleLoadAssetAsynOperation.OnAssetBundleLoadedDelegate cb)
    //{
    //    m_OnInit = cb;
    //    AssetBundleLoadManager.Instance.StartLoadAssetBundle(mTableBundle, AssetBundleLoadManager.m_FromHttp);
    //    StartCoroutine(LoadTableBundle(mTableBundle));

    //}


    void Load<T>(out T[] ret, out Dictionary<int, T> retDict, string path) where T : TableDef
    {
        try
        {
            //TextAsset ta = AssetBundleLoadManager.Instance.LoadAsset<TextAsset>(mTableBundle,path);
            
            //string json = ta.text;
            string json = "test";

            // ret = JsonUtility.ToObject<T[]>(json);

            ret = null;

            retDict = new Dictionary<int, T>();
            for (int i = 0; i < ret.Length; ++i)
            {
                retDict.Add(ret[i].id, ret[i]);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Exception while Load " + "\npu + ex");

          ret = null;
            retDict = null;
        }
    }


}
