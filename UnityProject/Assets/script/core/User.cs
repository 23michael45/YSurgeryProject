using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

[Serializable]
public class User_Face
{
    public string faceID;
    public string name;
    public bool male;

    public string shortCutImage;
    public string Facemodel, Facetexture;   

    public Dictionary<string ,Vector3> _facelandmark; 
    
}// 每个角色的基础属性


[Serializable]
public class User_Deform
{
    public string AvatarId;
    [Serializable]
    public class Shape
    {
        public Vector4 ForeheadSwitch, TempleSwitch, BISjawSwitch, ChinSwitch;
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
                        lowereyelidSwitch, EyebagSwitch, EyetailSwitch, BlackeyeSwitch;
    }
    [Serializable]
    public class Nose
    {
        public Vector4 UpperbridgeSwitch, InferiorbridgeSwitch, NoseheadSwitch, ColumellaNasiSwitch,
                        NasalBaseSwitch, NoseWingSwitch, NostrilSwitch;
    }
    [Serializable]
    public class Mouth
    {
        public Vector4 UplipSwitch, UpjawSwitch, DownLipSwitch, DownJawSwitch, PhiltrumSwitch, CornerSwitch;
    }
    [Serializable]
    public class Chest
    {
        public Vector4 upperItemSwitch, topItemSwitch, downItemSwitch;
    }
    [Serializable]
    public class Body
    {
        public Vector4 NeckSwitch, ChestSwitch, WristSwitch, HipSwitch, LegSwitch, ArmSwitch,
                        ForeheadSwitch, BISjawSwitch, ChinSwitch;
    }

    public Shape shape;
    public Face face;
    public Eyebrow eyebrow;
    public Eye eye;
    public Nose nose;
    public Mouth mouth;
    public Chest chest;
    public Body body;


}// 每个角色的变形属性



[Serializable]
public class User_Model
{   
    public string ModelID;
    public int RoleTableID;

    public string ModelShortcutImg;
    public string Modelname;

    public bool Editable;

    public bool  isman;
    public int defaultface;  

    public User_Face face;
    public User_Deform deform;
}//多模特定义



[Serializable]
public class User_Profile
{
    public User_Model[] models;

}


public class User
{
    User_Profile profile;
    User_Model  currentModel;
    User_Deform currentDeform;
    User_Face   currentFace;
    
    public bool crrrentEditable;

    public string currentmodelID, currentFaceID, currentmodelName;

    private int modnumb = 3;
    private int facenumb = 3;
    private int model;


    private float ST_CURhigh, ST_CURWeight, ST_CURChestline, ST_CURCup, ST_CURWaistline, ST_CURHipline, ST_CURArmline,
                  ST_CURThigh, ST_CUR_SkincolorSecai, ST_CUR_SkincolorXianyan, ST_CUR_SkincolorMingan, ST_CUR_EyeballScale;

    private float ST_CUR_Shuanghe, ST_CUR_Xiaba, ST_CUR_Lianxia, ST_CUR_Quangu, ST_CUR_Pingguo, ST_CUR_Yaoji,
    ST_CUR_Xiabachang, ST_CUR_HeadKuan, ST_CUR_HeadBaoman, ST_CUR_HeadQianhou, ST_CUR_HeadFaji, ST_CUR_HeadTouding,
    ST_CUR_HeadTaiyangxue, ST_CUR_EyeScale, ST_CUR_EyeZuoyou, ST_CUR_EyeGaodi, ST_CUR_EyeShenqian, ST_CUR_EyeYanjiao,
    ST_CUR_EyeYanwei, ST_CUR_EyebrowScale, ST_CUR_EyebrowZuoyou, ST_CUR_EyebrowGaodi, ST_CUR_EyebrowShenqian,
    ST_CUR_EyebrowMeitou, ST_CUR_EyebrowMeiwei, ST_CUR_NoseKuan, ST_CUR_NoseShangxia, ST_CUR_NoseTingba,
    ST_CUR_NoseBitou, ST_CUR_NoseBiliang, ST_CUR_MouthGaodi, ST_CUR_MouthShenqian, ST_CUR_MouthKuandu,
    ST_CUR_MouthHoudu, ST_CUR_MouthShangchun, ST_CUR_MouthXiachun, ST_CUR_MouthZuijiao;

    private int ST_coat, ST_top, ST_trouser, ST_underwear, ST_shoe, ST_hair, ST_hat;




    public void Init(string modelList )
    {

        profile = new User_Profile();
        profile.models = new User_Model[3];


        currentModel = profile.models[0];
        //profile.models[0].face = new User_Face();
        profile.models[0].deform = new User_Deform();


        //crrrentEditable = profile.models[0].Editable;       

        //currentmodelID = profile.models[0].ModelID;
        //currentFaceID = profile.models[0].face.faceID;
        //currentModel = profile.models[0];

        //currentmodelName = profile.models[0].Modelname;       
        //currentModel.RoleTableID = profile.models[0].RoleTableID;

    }




    /// <summary>
    //切换模特。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    /// </summary>
    /// <param name="modelId"></param>
    public void SaveModel(string modelId)
    {

        /* 
          int.TryParse(modelId.Substring(1, 1), out  model);
          int.TryParse(modelId.Substring(3, 1), out face);

          profile.models[model]  = currentModel;
          profile.models[model].face[face] = currentFace;
          profile.models[model].body=  currentBody ;
          */

    }
    public void SaveFace(string faceID)
    {
        /*     
        int.TryParse(faceID.Substring(1, 1), out model);
        int.TryParse(faceID.Substring(3, 1), out face);
        profile.models[model].face[face] = currentFace;     
        */
    }
    public void LoadRoleTable()
    {




    }
    //public void LoadModel(string modelID  ) {



    //    int.TryParse(modelID.Substring(1, 1), out model);
    //    int.TryParse(modelID.Substring(3, 1), out face);

    //    currentModel = profile.models[model];
    //    currentFace = profile.models[model].face[defaultface];
    //    currentBody = profile.models[model].body;
    //    currentModel.RoleTableID  =profile.models[model].RoleTableID ;

    ////加载角色表


    //    //如果没有，重建模特
    //    if (profile.models[model].body._high == 0)
    //    {
    //        setdefaultbody();
    //        ResetBody();
    //        ResetFace();
    //        //GameObject.Find("UI").GetComponent<UIcontrol>().ReModelUI();
    //        //GameObject.Find("UI").GetComponent<ModelMgr>().Remodelbool = true;
    //    }

    //    else {
    //        Debug.Log(currentModel.RoleTableID);
    //        //AppRoot.MainScene.loadmainRole(currentModel.RoleTableID);          
    //        loadModelvalue(model, defaultface);
    //        loadCloth(model, defaultface);
    //    }



    //  }



    //public void LoadFace(string faceID) {

    //    int.TryParse(faceID.Substring(1, 1), out model);
    //    int.TryParse(faceID.Substring(3, 1), out face);
    //    currentFace = profile.models[model].face[face];
    //    currentFaceID= profile.models[model].face[face].faceID;

    //    defaultface = face;

    //    Debug.Log(faceID);
    //    if (profile.models[model].face[face]._faceImgUrl == null)
    //    {
    //        GameObject.Find("UI").GetComponent<ModelMgr>().RePhoto();
    //    }
    //    else {

    //        AppRoot.MainScene.MainRole.LoadFace();
    //    }

    //}
    //public void SetModelName(string modelID,string name) {

    //    int.TryParse(modelID.Substring(1, 1), out model);   
    //    profile.models[model].Modelname = name;

    //}
    /// <summary>
    //切换模特。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    /// </summary>




    //重置基础身材。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    //public void setdefaultbody() {


    //    if (isman)
    //    {
    //        _highbase = 185.0f;
    //        _Weightbase = 70.0f;
    //        _Chestlinebase = 95.0f;
    //        _Cupbase = 2.0f;
    //        _Waistlinebase = 70.0f;
    //        _Hiplinebase = 90.0f;
    //        _Armlinebase = 35.0f;
    //        _Thighbase = 45.0f;
    //    }
    //    else {
    //        _highbase = 173.0f;
    //        _Weightbase = 55.0f;
    //        _Chestlinebase = 82.0f;
    //        _Cupbase = 10.0f;
    //        _Waistlinebase = 50.0f;
    //        _Hiplinebase = 90.0f;
    //        _Armlinebase = 26.0f;
    //        _Thighbase = 34.0f;
    //        }

    //}

    //当前角色身体参数。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。

    //public void loadModelvalue(int model,int face) {

    //    CURhigh =                 profile.models[model].body.slidervalue_High  ;
    //    CURWeight =               profile.models[model].body.slidervalue_weight ;
    //    CURChestline =            profile.models[model].body.slidervalue_Chestline ;
    //    CURCup =                  profile.models[model].body.slidervalue_Cup   ;
    //    CURWaistline =            profile.models[model].body.slidervalue_Waitline   ;
    //    CURHipline =              profile.models[model].body.slidervalue_Hipline   ;
    //    CURArmline =              profile.models[model].body.slidervalue_Armline  ;
    //    CURThigh =                profile.models[model].body.slidervalue_Thigh    ;
    //    CUR_SkincolorSecai =      profile.models[model].body.slidervalue_skin_h  ;
    //    CUR_SkincolorXianyan =    profile.models[model].body.slidervalue_skin_s   ;
    //    CUR_SkincolorMingan =     profile.models[model].body.slidervalue_skin_v ;
    //}


    //当前角色服装参数。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    //public void loadCloth(int model, int face) {
    //    AppRoot.MainUser.CUR_coat = profile.models [model ].body.c_coat ;
    //    AppRoot.MainUser.CUR_top = profile.models[model].body.c_top ; 
    //    AppRoot.MainUser.CUR_trouser = profile.models[model].body.c_trousers ; 
    //    AppRoot.MainUser.CUR_underwear = profile.models[model].body.c_underwear ; 
    //    AppRoot.MainUser.CUR_shoe = profile.models[model].body.c_shoe ; 
    //    AppRoot.MainUser.CUR_hair = profile.models[model].body.c_hair ; 
    //    AppRoot.MainUser.CUR_hat = profile.models[model].body.c_hat ; 


    //}
    ////.................................................................需要储存数据






    // 

    //








    /// <summary>
    //face。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    /// </summary>


    public int RoletableID
    {
        get { return currentModel.RoleTableID; }
        set { currentModel.RoleTableID = value; }
    }


    public int defaultface
    {
        get { return currentModel.defaultface; }
        set { currentModel.defaultface = value; }
    }
    public bool isman
    {
        get { return currentModel.isman; }
        set { currentModel.isman = value; }
    }


    /// <summary>
    /// baseface deform............
    /// </summary>

    public string faceshotImg
    {
        get { return currentModel.face.shortCutImage; }
        set { currentModel.face.shortCutImage = value; }
    }

    public string faceModel
    {
        get { return currentModel.face.Facemodel; }
        set { currentModel.face.Facemodel = value; }
    }

    public string facetexture
    {
        get { return currentModel.face.Facetexture; }
        set { currentModel.face.Facetexture = value; }
    }

    public Dictionary<string, Vector3> facelandmark
    {
        get
        {
            if (currentModel.face._facelandmark == null)
            {
                currentModel.face._facelandmark = new Dictionary<string, Vector3>();
            }
            return currentModel.face._facelandmark;
        }
        set
        {
            currentModel.face._facelandmark = value;
        }
    }


    /// <summary>
    /// shape deform............
    /// </summary>
    public Vector4  CURForeheadSwitch
    {
        get { return currentModel.deform.shape.ForeheadSwitch; }
        set
        {
          currentModel.deform.shape.ForeheadSwitch = value;
           Vector4 var = currentModel.deform.shape.ForeheadSwitch;

          AppRoot.MainDeform.SetForeheadSwitch(var);
        }
    }
    public Vector4 CURTempleSwitch
    {
        get { return currentModel.deform.shape.TempleSwitch; }
        set
        {
            currentModel.deform.shape.TempleSwitch = value;
            Vector4 var = currentModel.deform.shape.TempleSwitch;
            AppRoot.MainDeform.SetTempleSwitch (var);
        }
    }
    public Vector4 CURBISjawSwitch
    {
        get { return currentModel.deform.shape.BISjawSwitch; }
        set
        {
            currentModel.deform.shape.BISjawSwitch = value;
            Vector4 var = currentModel.deform.shape.BISjawSwitch;
            AppRoot.MainDeform.SetBISjawSwitch (var);
        }
    }
    public Vector4 CURChinSwitch
    {
        get { return currentModel.deform.shape.ChinSwitch; }
        set
        {
            currentModel.deform.shape.ChinSwitch = value;
            Vector4 var = currentModel.deform.shape.ChinSwitch;
            AppRoot.MainDeform.SetChinSwitch (var);
        }
    }


    /// <summary>
    /// Face deform............
    /// </summary>
    public Vector4 cur_ApplemuscleSwitch
    {
        get { return currentModel.deform.face .ApplemuscleSwitch; }
        set
        {
            currentModel.deform.face .ApplemuscleSwitch = value;
            Vector4 var = currentModel.deform.face.ApplemuscleSwitch;
            AppRoot.MainDeform.SetApplemuscleSwitch (var);
        }
    }

    public Vector4 cur_CheekbonesSwitch
    {
        get { return currentModel.deform.face.CheekbonesSwitch; }
        set
        {
            currentModel.deform.face.CheekbonesSwitch = value;
            Vector4 var = currentModel.deform.face.CheekbonesSwitch;
            AppRoot.MainDeform.SetCheekbonesSwitch (var);
        }
    }
    public Vector4 cur_FacialpartSwitch
    {
        get { return currentModel.deform.face.FacialpartSwitch; }
        set
        {
            currentModel.deform.face.FacialpartSwitch = value;
            Vector4 var = currentModel.deform.face.FacialpartSwitch;
            AppRoot.MainDeform.SetFacialpartSwitch (var);
        }
    }

    /// <summary>
    /// Eyebrow deform............
    /// </summary>
    public Vector4 cur_BrowbowSwitch
    {
        get { return currentModel.deform.eyebrow .BrowbowSwitch; }
        set
        {
            currentModel.deform.eyebrow .BrowbowSwitch = value;
            Vector4 var = currentModel.deform.shape.ForeheadSwitch;
            AppRoot.MainDeform.SetForeheadSwitch(var);
        }
    }
    public Vector4 cur_BrowHeadSwitch
    {
        get { return currentModel.deform.eyebrow.BrowHeadSwitch; }
        set
        {
            currentModel.deform.eyebrow.BrowHeadSwitch = value;
            Vector4 var = currentModel.deform.shape.ForeheadSwitch;
            AppRoot.MainDeform.SetForeheadSwitch(var);
        }
    }
    public Vector4 cur_BrowMiddleSwitch
    {
        get { return currentModel.deform.eyebrow.BrowMiddleSwitch; }
        set
        {
            currentModel.deform.eyebrow.BrowMiddleSwitch = value;
            Vector4 var = currentModel.deform.shape.ForeheadSwitch;
            AppRoot.MainDeform.SetForeheadSwitch(var);
        }
    }
    public Vector4 cur_BrowTailSwitch
    {
        get { return currentModel.deform.eyebrow.BrowTailSwitch; }
        set
        {
            currentModel.deform.eyebrow.BrowTailSwitch = value;
            Vector4 var = currentModel.deform.shape.ForeheadSwitch;
            AppRoot.MainDeform.SetForeheadSwitch(var);
        }
    }

    /// <summary>
    /// Eye deform............
    /// </summary>
    public Vector4 cur_EyecornerSwitch
    {
        get { return currentModel.deform.eye .EyecornerSwitch; }
        set
        {
            currentModel.deform.eye .EyecornerSwitch = value;
            Vector4 var = currentModel.deform.eye.EyecornerSwitch;
            AppRoot.MainDeform.SetEyecornerSwitch(var);
        }
    }

    public Vector4 cur_UppereyelidSwitch
    {
        get { return currentModel.deform.eye.UppereyelidSwitch; }
        set
        {
            currentModel.deform.eye.UppereyelidSwitch = value;
            Vector4 var = currentModel.deform.eye.UppereyelidSwitch;
            AppRoot.MainDeform.SetUppereyelidSwitch(var);
        }
    }
    public Vector4 cur_DoublefoldEyelidsSwitch
    {
        get { return currentModel.deform.eye.DoublefoldEyelidsSwitch; }
        set
        {
            currentModel.deform.eye.DoublefoldEyelidsSwitch = value;
            Vector4 var = currentModel.deform.eye.DoublefoldEyelidsSwitch;
            AppRoot.MainDeform.SetDoublefoldEyelidsSwitch(var);
        }
    }
    public Vector4 cur_lowereyelidSwitch
    {
        get { return currentModel.deform.eye.lowereyelidSwitch; }
        set
        {
            currentModel.deform.eye.lowereyelidSwitch = value;
            Vector4 var = currentModel.deform.eye.lowereyelidSwitch;
            AppRoot.MainDeform.SetlowereyelidSwitch(var);
        }
    }
    public Vector4 cur_EyebagSwitch
    {
        get { return currentModel.deform.eye.EyebagSwitch; }
        set
        {
            currentModel.deform.eye.EyebagSwitch = value;
            Vector4 var = currentModel.deform.eye.EyebagSwitch;
            AppRoot.MainDeform.SetEyebagSwitch(var);
        }
    }
    public Vector4 cur_EyetailSwitch
    {
        get { return currentModel.deform.eye.EyetailSwitch; }
        set
        {
            currentModel.deform.eye.EyetailSwitch = value;
            Vector4 var = currentModel.deform.eye.EyetailSwitch;
            AppRoot.MainDeform.SetEyetailSwitch(var);
        }
    }

    public Vector4 cur_BlackeyeSwitch
    {
        get { return currentModel.deform.eye.BlackeyeSwitch; }
        set
        {
            currentModel.deform.eye.BlackeyeSwitch = value;
            Vector4 var = currentModel.deform.eye.BlackeyeSwitch;
            AppRoot.MainDeform.SetBlackeyeSwitch(var);
        }
    }



    /// <summary>
    /// Nose deform............
    /// </summary>
    public Vector4 cur_UpperbridgeSwitch
    {
        get { return currentModel.deform.nose .UpperbridgeSwitch; }
        set
        {
            currentModel.deform.nose.UpperbridgeSwitch = value;
            Vector4 var = currentModel.deform.nose.UpperbridgeSwitch;
            AppRoot.MainDeform.SetUpperbridgeSwitch(var);
        }
    }
    public Vector4 cur_InferiorbridgeSwitch
    {
        get { return currentModel.deform.nose.InferiorbridgeSwitch; }
        set
        {
            currentModel.deform.nose.InferiorbridgeSwitch = value;
            Vector4 var = currentModel.deform.nose.InferiorbridgeSwitch;
            AppRoot.MainDeform.SetInferiorbridgeSwitch(var);
        }
    }
    public Vector4 cur_NoseheadSwitch
    {
        get { return currentModel.deform.nose.NoseheadSwitch; }
        set
        {
            currentModel.deform.nose.NoseheadSwitch = value;
            Vector4 var = currentModel.deform.nose.NoseheadSwitch;
            AppRoot.MainDeform.SetNoseheadSwitch(var);
        }
    }
    public Vector4 cur_ColumellaNasiSwitch
    {
        get { return currentModel.deform.nose.ColumellaNasiSwitch; }
        set
        {
            currentModel.deform.nose.ColumellaNasiSwitch = value;
            Vector4 var = currentModel.deform.nose.ColumellaNasiSwitch;
            AppRoot.MainDeform.SetColumellaNasiSwitch(var);
        }
    }
    public Vector4 cur_NasalBaseSwitch
    {
        get { return currentModel.deform.nose.NasalBaseSwitch; }
        set
        {
            currentModel.deform.nose.NasalBaseSwitch = value;
            Vector4 var = currentModel.deform.nose.NasalBaseSwitch;
            AppRoot.MainDeform.SetNasalBaseSwitch(var);
        }
    }
    public Vector4 cur_NoseWingSwitch
    {
        get { return currentModel.deform.nose.NoseWingSwitch; }
        set
        {
            currentModel.deform.nose.NoseWingSwitch = value;
            Vector4 var = currentModel.deform.nose.NoseWingSwitch;
            AppRoot.MainDeform.SetNoseWingSwitch(var);
        }
    }
    public Vector4 cur_NostrilSwitch
    {
        get { return currentModel.deform.nose.NostrilSwitch; }
        set
        {
            currentModel.deform.nose.NostrilSwitch = value;
            Vector4 var = currentModel.deform.nose.NostrilSwitch;
            AppRoot.MainDeform.SetNostrilSwitch(var);
        }
    }


    /// <summary>
    /// Mouth deform............
    /// </summary>

    public Vector4 cur_UplipSwitch
    {
        get { return currentModel.deform.mouth .UplipSwitch; }
        set
        {
            currentModel.deform.mouth .UplipSwitch = value;
            Vector4 var = currentModel.deform.mouth.UplipSwitch;
            AppRoot.MainDeform.SetUplipSwitch(var);
        }
    }
    public Vector4 cur_UpjawSwitch
    {
        get { return currentModel.deform.mouth.UpjawSwitch; }
        set
        {
            currentModel.deform.mouth.UpjawSwitch = value;
            Vector4 var = currentModel.deform.mouth.UpjawSwitch;
            AppRoot.MainDeform.SetUpjawSwitch(var);
        }
    }
    public Vector4 cur_DownLipSwitch
    {
        get { return currentModel.deform.mouth.DownLipSwitch; }
        set
        {
            currentModel.deform.mouth.DownLipSwitch = value;
            Vector4 var = currentModel.deform.mouth.DownLipSwitch;
            AppRoot.MainDeform.SetDownLipSwitch(var);
        }
    }
    public Vector4 cur_DownJawSwitch
    {
        get { return currentModel.deform.mouth.DownJawSwitch; }
        set
        {
            currentModel.deform.mouth.DownJawSwitch = value;
            Vector4 var = currentModel.deform.mouth.DownJawSwitch;
            AppRoot.MainDeform.SetDownJawSwitch(var);
        }
    }
    public Vector4 cur_PhiltrumSwitch
    {
        get { return currentModel.deform.mouth.PhiltrumSwitch; }
        set
        {
            currentModel.deform.mouth.PhiltrumSwitch = value;
            Vector4 var = currentModel.deform.mouth.PhiltrumSwitch;
            AppRoot.MainDeform.SetPhiltrumSwitch(var);
        }
    }
    public Vector4 cur_CornerSwitch
    {
        get { return currentModel.deform.mouth.CornerSwitch; }
        set
        {
            currentModel.deform.mouth.CornerSwitch = value;
            Vector4 var = currentModel.deform.mouth.CornerSwitch;
            AppRoot.MainDeform.SetCornerSwitch(var);
        }
    }


    /// <summary>
    /// Chest deform............
    /// </summary>

    public Vector4 cur_upperItemSwitch
    {
        get { return currentModel.deform.chest .upperItemSwitch; }
        set
        {
            currentModel.deform.chest .upperItemSwitch = value;
            Vector4 var = currentModel.deform.chest.upperItemSwitch;
            AppRoot.MainDeform.SetupperItemSwitch(var);
        }
    }

    public Vector4 cur_topItemSwitch
    {
        get { return currentModel.deform.chest.topItemSwitch; }
        set
        {
            currentModel.deform.chest.topItemSwitch = value;
            Vector4 var = currentModel.deform.chest.topItemSwitch;
            AppRoot.MainDeform.SettopItemSwitch(var);
        }
    }
    public Vector4 cur_downItemSwitch
    {
        get { return currentModel.deform.chest.downItemSwitch; }
        set
        {
            currentModel.deform.chest.downItemSwitch = value;
            Vector4 var = currentModel.deform.chest.downItemSwitch;
            AppRoot.MainDeform.SetdownItemSwitch(var);
        }
    }


    
    /// <summary>
    /// Body deform............
    /// </summary>

    public Vector4 cur_NeckSwitch
    {
        get { return currentModel.deform.body .NeckSwitch; }
        set
        {
            currentModel.deform.body .NeckSwitch = value;
            Vector4 var = currentModel.deform.body.NeckSwitch;
            AppRoot.MainDeform.SetNeckSwitch(var);
        }
    }
    public Vector4 cur_ChestSwitch
    {
        get { return currentModel.deform.body.ChestSwitch; }
        set
        {
            currentModel.deform.body.ChestSwitch = value;
            Vector4 var = currentModel.deform.body.ChestSwitch;
            AppRoot.MainDeform.SetChestSwitch (var);
        }
    }
    public Vector4 cur_WristSwitch
    {
        get { return currentModel.deform.body.WristSwitch; }
        set
        {
            currentModel.deform.body.WristSwitch = value;
            Vector4 var = currentModel.deform.body.WristSwitch;
            AppRoot.MainDeform.SetWristSwitch (var);
        }
    }
    public Vector4 cur_HipSwitch
    {
        get { return currentModel.deform.body.HipSwitch; }
        set
        {
            currentModel.deform.body.HipSwitch = value;
            Vector4 var = currentModel.deform.body.HipSwitch;
            AppRoot.MainDeform.SetHipSwitch (var);
        }
    }
    public Vector4 cur_LegSwitch
    {
        get { return currentModel.deform.body.LegSwitch; }
        set
        {
            currentModel.deform.body.NeckSwitch = value;
            Vector4 var = currentModel.deform.body.NeckSwitch;
            AppRoot.MainDeform.SetLegSwitch (var);
        }
    }
    public Vector4 cur_ArmSwitch
    {
        get { return currentModel.deform.body.ArmSwitch; }
        set
        {
            currentModel.deform.body.ArmSwitch = value;
            Vector4 var = currentModel.deform.body.ArmSwitch;
            AppRoot.MainDeform.SetArmSwitch (var);
        }
    }
  


    ////。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。储存数据。。。。。。。。


    //public float animatorslider
    //{
    //    get { return currentBody. animatorpecent; }
    //    set
    //    {
    //        currentBody. animatorpecent = value;

    //        AppRoot.MainScene.MainRole.animatorpercent(currentBody.animatorpecent);

    //    }
    //}



    //public void StoreFaceValue()
    //{
    //    ST_CUR_Shuanghe = AppRoot.MainUser.CUR_Shuanghe;
    //    ST_CUR_Xiaba = AppRoot.MainUser.CUR_Xiaba;
    //    ST_CUR_Lianxia = AppRoot.MainUser.CUR_Lianxia;
    //    ST_CUR_Quangu = AppRoot.MainUser.CUR_Quangu;
    //    ST_CUR_Pingguo = AppRoot.MainUser.CUR_Pingguo;
    //    ST_CUR_Yaoji = AppRoot.MainUser.CUR_Yaoji;
    //    ST_CUR_Xiabachang = AppRoot.MainUser.CUR_Xiabachang;
    //    ST_CUR_HeadKuan = AppRoot.MainUser.CUR_HeadKuan;
    //    ST_CUR_HeadBaoman = AppRoot.MainUser.CUR_HeadBaoman;
    //    ST_CUR_HeadQianhou = AppRoot.MainUser.CUR_HeadQianhou;
    //    ST_CUR_HeadFaji = AppRoot.MainUser.CUR_HeadFaji;
    //    ST_CUR_HeadTouding = AppRoot.MainUser.CUR_HeadTouding;
    //    ST_CUR_HeadTaiyangxue = AppRoot.MainUser.CUR_HeadTaiyangxue;
    //    ST_CUR_EyeScale = AppRoot.MainUser.CUR_EyeScale;
    //    ST_CUR_EyeZuoyou = AppRoot.MainUser.CUR_EyeZuoyou;
    //    ST_CUR_EyeGaodi = AppRoot.MainUser.CUR_EyeGaodi;
    //    ST_CUR_EyeShenqian = AppRoot.MainUser.CUR_EyeShenqian;
    //    ST_CUR_EyeYanjiao = AppRoot.MainUser.CUR_EyeYanjiao;
    //    ST_CUR_EyeYanwei = AppRoot.MainUser.CUR_EyeYanwei;
    //    ST_CUR_EyebrowScale = AppRoot.MainUser.CUR_EyebrowScale;
    //    ST_CUR_EyebrowZuoyou = AppRoot.MainUser.CUR_EyebrowZuoyou;
    //    ST_CUR_EyebrowGaodi = AppRoot.MainUser.CUR_EyebrowGaodi;
    //    ST_CUR_EyebrowShenqian = AppRoot.MainUser.CUR_EyebrowShenqian;
    //    ST_CUR_EyebrowMeitou = AppRoot.MainUser.CUR_EyebrowMeitou;
    //    ST_CUR_EyebrowMeiwei = AppRoot.MainUser.CUR_EyebrowMeiwei;
    //    ST_CUR_NoseKuan = AppRoot.MainUser.CUR_NoseKuan;
    //    ST_CUR_NoseShangxia = AppRoot.MainUser.CUR_NoseShangxia;
    //    ST_CUR_NoseTingba = AppRoot.MainUser.CUR_NoseTingba;
    //    ST_CUR_NoseBitou = AppRoot.MainUser.CUR_NoseBitou;
    //    ST_CUR_NoseBiliang = AppRoot.MainUser.CUR_NoseBiliang;
    //    ST_CUR_MouthGaodi = AppRoot.MainUser.CUR_MouthGaodi;
    //    ST_CUR_MouthShenqian = AppRoot.MainUser.CUR_MouthShenqian;
    //    ST_CUR_MouthKuandu = AppRoot.MainUser.CUR_MouthKuandu;
    //    ST_CUR_MouthHoudu = AppRoot.MainUser.CUR_MouthHoudu;
    //    ST_CUR_MouthShangchun = AppRoot.MainUser.CUR_MouthShangchun;
    //    ST_CUR_MouthXiachun = AppRoot.MainUser.CUR_MouthXiachun;
    //    ST_CUR_MouthZuijiao = AppRoot.MainUser.CUR_MouthZuijiao;
    //    ST_CUR_EyeballScale = AppRoot.MainUser.CUR_EyeballScale;

    //}

    //public void ReloadCloth()
    //{
    //    AppRoot.MainUser.CUR_coat = ST_coat;
    //    AppRoot.MainUser.CUR_top = ST_top;
    //    AppRoot.MainUser.CUR_trouser = ST_trouser;
    //    AppRoot.MainUser.CUR_underwear = ST_underwear;
    //    AppRoot.MainUser.CUR_shoe = ST_shoe;
    //    AppRoot.MainUser.CUR_hair = ST_hair;
    //    AppRoot.MainUser.CUR_hat = ST_hat;

    //}
    //public void ResetBody()
    //{
    //   CURhigh = 0.0f;
    //   CURWeight = 0.0f;
    //   CURChestline = 0.0f;
    //   CURCup = 0.0f;
    //   CURWaistline = 0.0f;
    //   CURHipline = 0.0f;
    //   CURArmline = 0.0f;
    //   CURThigh = 0.0f;
    //   CUR_SkincolorSecai = 0.0f;
    //   CUR_SkincolorXianyan = 0.0f;
    //   CUR_SkincolorMingan = 0.0f;

    //}
    //public void ResetFace()
    //{
    //   CUR_Shuanghe = 0.0f;
    //   CUR_Xiaba = 0.0f;
    //   CUR_Lianxia = 0.0f;
    //   CUR_Quangu = 0.0f;
    //   CUR_Pingguo = 0.0f;
    //   CUR_Yaoji = 0.0f;
    //   CUR_Xiabachang = 0.0f;
    //   CUR_HeadKuan = 0.0f;
    //   CUR_HeadBaoman = 0.0f;
    //   CUR_HeadQianhou = 0.0f;
    //   CUR_HeadFaji = 0.0f;
    //   CUR_HeadTouding = 0.0f;
    //   CUR_HeadTaiyangxue = 0.0f;
    //   CUR_EyeScale = 0.0f;
    //   CUR_EyeZuoyou = 0.0f;
    //   CUR_EyeGaodi = 0.0f;
    //   CUR_EyeShenqian = 0.0f;
    //   CUR_EyeYanjiao = 0.0f;
    //   CUR_EyeYanwei = 0.0f;
    //   CUR_EyebrowScale = 0.0f;
    //   CUR_EyebrowZuoyou = 0.0f;
    //   CUR_EyebrowGaodi = 0.0f;
    //   CUR_EyebrowShenqian = 0.0f;
    //   CUR_EyebrowMeitou = 0.0f;
    //   CUR_EyebrowMeiwei = 0.0f;
    //   CUR_NoseKuan = 0.0f;
    //   CUR_NoseShangxia = 0.0f;
    //   CUR_NoseTingba = 0.0f;
    //   CUR_NoseBitou = 0.0f;
    //   CUR_NoseBiliang = 0.0f;
    //   CUR_MouthGaodi = 0.0f;
    //   CUR_MouthShenqian = 0.0f;
    //   CUR_MouthKuandu = 0.0f;
    //   CUR_MouthHoudu = 0.0f;
    //   CUR_MouthShangchun = 0.0f;
    //   CUR_MouthXiachun = 0.0f;
    //   CUR_MouthZuijiao = 0.0f;

    //}

    //public string GetShapeJsonValue()
    //{
    //    LitJson.JsonData jsondata = new LitJson.JsonData();
    //    jsondata["CURhigh"] = CURhigh;
    //    jsondata["CURWeight"] = CURWeight;
    //    jsondata["CURChestline"] = CURChestline;
    //    jsondata["CURCup"] = CURCup;
    //    jsondata["CURWaistline"] = CURWaistline;
    //    jsondata["CURHipline"] = CURHipline;
    //    jsondata["CURArmline"] = CURArmline;
    //    jsondata["CURThigh"] = CURThigh;


    //    string jsonstring = jsondata.ToJson();

    //    return jsonstring;
    //}
    //public void SetShapeJsonValue(string jsonstring)
    //{
    //    LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(jsonstring);
    //    CURhigh = (float)jsondata["CURhigh"];
    //    CURWeight = (float)jsondata["CURWeight"];
    //    CURChestline = (float)jsondata["CURChestline"];
    //    CURCup = (float)jsondata["CURCup"];
    //    CURWaistline = (float)jsondata["CURWaistline"];
    //    CURHipline = (float)jsondata["CURHipline"];
    //    CURArmline = (float)jsondata["CURArmline"];
    //    CURThigh = (float)jsondata["CURThigh"];
    //}


    //public string GetSkinJsonValue()
    //{

    //    LitJson.JsonData jsondata = new LitJson.JsonData();
    //    jsondata["CUR_SkincolorBase"] = CUR_SkincolorBase;
    //    jsondata["CUR_SkincolorSecai"] = CUR_SkincolorSecai;
    //    jsondata["CUR_SkincolorXianyan"] = CUR_SkincolorXianyan;
    //    jsondata["CUR_SkincolorMingan"] = CUR_SkincolorMingan;

    //    string jsonstring = jsondata.ToJson();

    //    return jsonstring;
    //}

    //public void SetSkinJsonValue(string jsonstring)
    //{
    //    LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(jsonstring);
    //    CUR_SkincolorBase = (float)jsondata["CUR_SkincolorBase"];
    //    CUR_SkincolorSecai = (float)jsondata["CUR_SkincolorSecai"];
    //    CUR_SkincolorXianyan = (float)jsondata["CUR_SkincolorXianyan"];
    //    CUR_SkincolorMingan = (float)jsondata["CUR_SkincolorMingan"];

    //}


    //public string GetLandmarkJsonValue()
    //{
    //    LitJson.JsonData jsondata = new LitJson.JsonData();

    //    foreach(KeyValuePair<string,Vector3> kv in facelandmark)
    //    {
    //        jsondata[kv.Key] = GetVector3Json(kv.Value);
    //    }

    //    string jsonstring = jsondata.ToJson();
    //    return jsonstring;

    //}

    //public void SetLandmarkJsonValue(string jsonstring)
    //{
    //    LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(jsonstring);
    //    facelandmark.Clear();
    //    foreach (KeyValuePair<string, LitJson.JsonData> kv in jsondata.inst_object)
    //    {

    //        string key = (string)kv.Key;
    //        string value = (string)kv.Value;

    //        Vector3 v = SetVector3Json(value);
    //        facelandmark[key] = v;


    //    }


    //}

    //public string GetForHeadJsonValue()
    //{

    //    LitJson.JsonData jsondata = new LitJson.JsonData();
    //    jsondata["CUR_HeadKuan"] = CUR_HeadKuan;
    //    jsondata["CUR_HeadBaoman"] = CUR_HeadBaoman;
    //    jsondata["CUR_HeadQianhou"] = CUR_HeadQianhou;
    //    jsondata["CUR_HeadFaji"] = CUR_HeadFaji;
    //    jsondata["CUR_HeadTouding"] = CUR_HeadTouding;
    //    jsondata["CUR_HeadTaiyangxue"] = CUR_HeadTaiyangxue;

    //    string jsonstring = jsondata.ToJson();

    //    return jsonstring;
    //}

   

    //string GetVector3Json(Vector3 v)
    //{
    //    LitJson.JsonData jsondata = new LitJson.JsonData();
    //    jsondata["x"] = v.x;
    //    jsondata["y"] = v.y;
    //    jsondata["z"] = v.z;

    //    string jsonstring = jsondata.ToJson();

    //    return jsonstring;
    //}

    //Vector3 SetVector3Json(string jsonstring)
    //{ 

    //    Vector3 v = new Vector3();
    //    LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(jsonstring);
    //    v.x = (float)jsondata["x"];
    //    v.y = (float)jsondata["y"];
    //    v.z = (float)jsondata["z"];
    //    return v;
    //    //}
}


