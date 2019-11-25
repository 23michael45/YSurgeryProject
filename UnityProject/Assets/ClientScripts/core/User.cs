using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;




[Serializable]
public class User_Environment
{
    public int Background;
    public int Stage;
    public int Light;   

}// 环境设置




[Serializable]
public class User_Role
{
    public int AvatarId;
    public int DeformCount;
    public string name;
    public string shortCutImage;
    public string Facemodel;
    public string Facetexture;
    public int sex;
    public Dictionary<string, Vector3> Bones;
    public Dictionary<int, Vector3> MeshPoint;

}// 每个角色的基础属性



[Serializable]
public class User_Deform
{
    public string AvatarId;
    public int DeformNumber;
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
public class User_Ornaments
{
    public int ModelID;

    [Serializable]
    public class Makeup
    {
        public int EyeBrow, EyeShadow, EyeLash, Pupil, Foundation, Shadow, Lip, FaceTatoo, BodyTatoo;

    }

    [Serializable]
    public class Cloth
    {
        public int UpWear, DownWear, Coat, Suit, UnderWear, Shoe, Ornament;
    }

    [Serializable]
    public class Ornament
    {
        public int Hair, Glass, Hats, Jewellery;
    }

    public int Act;
    public Makeup makeup;
    public Cloth cloth;
    public Ornament ornament;

}




[Serializable]
public class User_Model
{   
    public string ModelID; 
    public string shotcutImage;

    public string TexturePath;
    public string ModelPath;
    public int  Editable;

    public User_Role role;
    public User_Deform deform;
    public User_Ornaments Ornament;

}


[Serializable]
public class User_Profile
{
    public User_Model model;
    public User_Environment environment;

}

[Serializable]
public class User
{
    public User_Profile currentProfile= new User_Profile();
    public User_Model currentModel= new User_Model();

    public bool crrrentEditable;

    public string currentmodelID, currentFaceID, currentmodelName;

    


    public void Init( )
    {
     

        currentProfile = new User_Profile();
        currentModel = new User_Model();
        //currentModel = currentProfile.model;

        Debug.Log(currentModel);

        currentModel.role= new User_Role();

        currentModel.deform = new User_Deform();

        currentModel.deform.shape = new User_Deform.Shape();
        currentModel.deform.face = new User_Deform.Face ();
        currentModel.deform.eyebrow = new User_Deform.Eyebrow ();
        currentModel.deform.eye = new User_Deform.Eye ();
        currentModel.deform.nose = new User_Deform.Nose ();
        currentModel.deform.mouth = new User_Deform.Mouth ();
        currentModel.deform.chest = new User_Deform.Chest();
        currentModel.deform.body = new User_Deform.Body();
        

        currentModel.Ornament = new User_Ornaments();
        currentModel.Ornament.makeup= new User_Ornaments.Makeup();
        currentModel.Ornament.cloth = new User_Ornaments.Cloth();
        currentModel.Ornament.ornament = new User_Ornaments.Ornament();
         
        Debug.Log(Application.streamingAssetsPath);



             
    }



    /// <summary>
    //face。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    /// </summary>

    public string shotcutImage
    {
        get { return currentModel.shotcutImage; }
        set { currentModel.shotcutImage = value; }
     }
    
    public string  TexturePath
    {
        get { return currentModel.TexturePath; }
        set { currentModel.TexturePath = value; }
    }

    public string ModelPath
    {
        get { return currentModel.ModelPath; }
        set { currentModel.ModelPath = value; }
    }
    
    public int Editable
    {
        get { return currentModel.Editable; }
        set { currentModel.Editable = value; }
    }


    /// <summary>
    /// baseface deform............
    /// </summary>

    public string faceshotImg
    {
        get { return currentModel.role.shortCutImage; }
        set { currentModel.role.shortCutImage = value; }
    }

    public string faceModel
    {
        get { return currentModel.role.Facemodel; }
        set { currentModel.role.Facemodel = value; }
    }

    public string facetexture
    {
        get { return currentModel.role.Facetexture; }
        set { currentModel.role.Facetexture = value; }
    }

    public Dictionary<string, Vector3> bones
    {
        get
        {
            if (currentModel.role.Bones == null)
            {
                currentModel.role.Bones = new Dictionary<string, Vector3>();
            }
            return currentModel.role.Bones;
        }
        set
        {
            currentModel.role.Bones = value;
        }
    }

    public Dictionary<int , Vector3> MeshPoint
    {
        get
        {
            if (currentModel.role.MeshPoint == null)
            {
                currentModel.role.MeshPoint = new Dictionary<int , Vector3>();
            }
            return currentModel.role.MeshPoint;
        }
        set
        {
            currentModel.role.MeshPoint = value;
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
            Vector4 var = currentModel.deform.eyebrow.BrowbowSwitch;
            AppRoot.MainDeform.SetBrowbowSwitch(var);
        }
    }
    public Vector4 cur_BrowHeadSwitch
    {
        get { return currentModel.deform.eyebrow.BrowHeadSwitch; }
        set
        {
            currentModel.deform.eyebrow.BrowHeadSwitch = value;
            Vector4 var = currentModel.deform.eyebrow.BrowHeadSwitch;
            AppRoot.MainDeform.SetBrowHeadSwitch(var);
        }
    }
    public Vector4 cur_BrowMiddleSwitch
    {
        get { return currentModel.deform.eyebrow.BrowMiddleSwitch; }
        set
        {
            currentModel.deform.eyebrow.BrowMiddleSwitch = value;
            Vector4 var = currentModel.deform.eyebrow.BrowMiddleSwitch;
            AppRoot.MainDeform.SetBrowMiddleSwitch(var);
        }
    }
    public Vector4 cur_BrowTailSwitch
    {
        get { return currentModel.deform.eyebrow.BrowTailSwitch; }
        set
        {
            currentModel.deform.eyebrow.BrowTailSwitch = value;
            Vector4 var = currentModel.deform.eyebrow.BrowTailSwitch;
            AppRoot.MainDeform.SetBrowTailSwitch(var);
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


}


