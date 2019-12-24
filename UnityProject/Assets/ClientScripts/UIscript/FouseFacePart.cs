using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FouseFacePart : MonoBehaviour
{
    public string Deformjson;

    // shape.........
    public Toggle foreheadItem;
    public Toggle TempleItem;
    public Toggle BISjawItem;
    public Toggle ChinItem;

    //face..........
    public Toggle ApplemuscleItem;
    public Toggle CheekbonesItem;
    public Toggle FacialpartItem;


    //eyebrow....
    public Toggle BrowbowItem;
    public Toggle BrowHeadItem;
    public Toggle BrowMiddleItem;
    public Toggle BrowTailItem;


    //eye.......
    public Toggle EyecornerItem;
    public Toggle UppereyelidItem;
    public Toggle DoublefoldEyelidsItem;
    public Toggle lowereyelidItem;
    public Toggle EyebagItem;
    public Toggle EyetailItem;
    public Toggle BlackeyeItem;

    //nose.........
    public Toggle UpperbridgeItem;
    public Toggle InferiorbridgeItem;
    public Toggle NoseheadItem;
    public Toggle ColumellaNasiItem;
    public Toggle NasalBaseItem;
    public Toggle NoseWingItem;
    public Toggle NostrilItem;

    //mouth.........
    public Toggle upperlipItem;
    public Toggle upperboneItem;
    public Toggle downlipItem;
    public Toggle downBoneItem;
    public Toggle MiddleItem;
    public Toggle cornerItem;

    //cheset.......
    public Toggle chestupperItem;
    public Toggle chesttopItem;
    public Toggle chestdownItem;

    //body.............
    public Toggle neckItem;
    public Toggle chestItem;
    public Toggle wristItem;
    public Toggle hipItem;
    public Toggle LegItem;
    public Toggle ArmItem;
    FaceAreaTextureChange faceAreaTextureChange;

    private void Awake()
    {

        faceAreaTextureChange = new FaceAreaTextureChange(); 
    }

    public void Start()
    {
        foreheadItem_chg(true);

        foreheadItem.onValueChanged.AddListener(foreheadItem_chg);
        TempleItem.onValueChanged.AddListener(TempleItem_chg);
        BISjawItem.onValueChanged.AddListener(BISjawItem_chg);
        ChinItem.onValueChanged.AddListener(ison => ChinItem_chg());
        //face..........
        ApplemuscleItem.onValueChanged.AddListener(ison => ApplemuscleItem_chg());
        CheekbonesItem.onValueChanged.AddListener(ison => CheekbonesItem_chg());
        FacialpartItem.onValueChanged.AddListener(ison => FacialpartItem_chg());
        //eyebrow....
        BrowbowItem.onValueChanged.AddListener(ison => BrowbowItem_chg());
        BrowHeadItem.onValueChanged.AddListener(ison => BrowHeadItem_chg());
        BrowMiddleItem.onValueChanged.AddListener(ison => BrowMiddleItem_chg());
        BrowTailItem.onValueChanged.AddListener(ison => BrowTailItem_chg());
        //eye.......
        EyecornerItem.onValueChanged.AddListener(ison => EyecornerItem_chg());
        UppereyelidItem.onValueChanged.AddListener(ison => UppereyelidItem_chg());
        DoublefoldEyelidsItem.onValueChanged.AddListener(ison => DoublefoldEyelidsItem_chg());
        lowereyelidItem.onValueChanged.AddListener(ison => lowereyelidItem_chg());
        EyebagItem.onValueChanged.AddListener(ison => EyebagItem_chg());
        EyetailItem.onValueChanged.AddListener(ison => EyetailItem_chg());
        BlackeyeItem.onValueChanged.AddListener(ison => BlackeyeItem_chg());
        //nose.........
        UpperbridgeItem.onValueChanged.AddListener(ison => UpperbridgeItem_chg());
        InferiorbridgeItem.onValueChanged.AddListener(ison => InferiorbridgeItem_chg());
        NoseheadItem.onValueChanged.AddListener(ison => NoseheadItem_chg());
        ColumellaNasiItem.onValueChanged.AddListener(ison => ColumellaNasiItem_chg());
        NasalBaseItem.onValueChanged.AddListener(ison => NasalBaseItem_chg());
        NoseWingItem.onValueChanged.AddListener(ison => NoseWingItem_chg());
        NostrilItem.onValueChanged.AddListener(ison => NostrilItem_chg());
        //mouth.........
        upperlipItem.onValueChanged.AddListener(ison => upperlipItem_chg());
        upperboneItem.onValueChanged.AddListener(ison => upperboneItem_chg());
        downlipItem.onValueChanged.AddListener(ison => downlipItem_chg());
        downBoneItem.onValueChanged.AddListener(ison => downBoneItem_chg());
        MiddleItem.onValueChanged.AddListener(ison => MiddleItem_chg());
        cornerItem.onValueChanged.AddListener(ison => cornerItem_chg());
        //cheset.......
        //chestupperItem.onValueChanged.AddListener(ison => chestupperItem_chg());
        //chesttopItem.onValueChanged.AddListener(ison => chesttopItem_chg());
        //chestdownItem.onValueChanged.AddListener(ison => chestdownItem_chg());
        ////body.............
        //neckItem.onValueChanged.AddListener(ison => neckItem_chg());
        //chestItem.onValueChanged.AddListener(ison => chestItem_chg());
        //wristItem.onValueChanged.AddListener(ison => wristItem_chg());
        //hipItem.onValueChanged.AddListener(ison => hipItem_chg());
        //LegItem.onValueChanged.AddListener(ison => LegItem_chg());
        //ArmItem.onValueChanged.AddListener(ison => ArmItem_chg());       

    }

    public void NoneAreaTexture() {
        string TexturePath = "FaceAreaPNG/noneTex";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);

    }

    void ToggleItemChange(bool b,string texturePath,string leaderboneLeft,string leaderboneRight)
    {
        if (b)
        {

            faceAreaTextureChange.ChangeFaceArea(texturePath);

            DeformLeaderBoneManager.Instance.StartEdit(leaderboneLeft);
            DeformLeaderBoneManager.Instance.StartEdit(leaderboneRight);

            //左右对称，所以设置一次slider值即可
            DeformUI.Instance.SetItemValueByLeaderBoneName(leaderboneLeft);
        }
        else
        {
            DeformLeaderBoneManager.Instance.StopEdit(leaderboneLeft);
            DeformLeaderBoneManager.Instance.StopEdit(leaderboneRight);

        }
    }

    public void foreheadItem_chg(bool b) {

        string texturePath = "FaceAreaPNG/01Shape/forehead";
        string leaderboneLeft = "face_forehead_Lf_joint1";
        string leaderboneRight = "face_forehead_Rt_joint1";
        ToggleItemChange(b, texturePath, leaderboneLeft, leaderboneRight);
    }

    

    public void TempleItem_chg(bool b)
    {
        string texturePath = "FaceAreaPNG/01Shape/Temple";
        string leaderboneLeft = "face_temple_Lf_joint1";
        string leaderboneRight = "face_temple_Rt_joint1";
        ToggleItemChange(b, texturePath, leaderboneLeft, leaderboneRight);

    }    
    public void BISjawItem_chg(bool b)
    {
        string texturePath = "FaceAreaPNG/01Shape/BISjaw";
        string leaderboneLeft = "face_chin_Lf_joint08";
        string leaderboneRight = "face_chin_Rt_joint08";
        ToggleItemChange(b, texturePath, leaderboneLeft, leaderboneRight);
  
    }
    public void ChinItem_chg()
    {
        string TexturePath = "FaceAreaPNG/01Shape/Chin";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }

    //face..........

    public void ApplemuscleItem_chg()
    {
        string TexturePath = "FaceAreaPNG/02Face/Applemuscle";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void CheekbonesItem_chg()
    {
        string TexturePath = "FaceAreaPNG/02Face/Cheekbones";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void FacialpartItem_chg()
    {
        string TexturePath = "FaceAreaPNG/02Face/Facialpart";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }

    //eyebrow....

    public void BrowbowItem_chg()
    {
        string TexturePath = "FaceAreaPNG/03Eyebrow/Browbow";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);       
        //Debug.Log(val);
    }
    public void BrowHeadItem_chg()
    {
        string TexturePath = "FaceAreaPNG/03Eyebrow/BrowHead";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);               //Debug.Log(val);
    }
    public void BrowMiddleItem_chg( )
    {
        string TexturePath = "FaceAreaPNG/03Eyebrow/BrowMiddle";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);               //Debug.Log(val);
    }
    public void BrowTailItem_chg( )
    {
        string TexturePath = "FaceAreaPNG/03Eyebrow/BrowTail";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }



    //eye.......

    public void EyecornerItem_chg( )
    {
        string TexturePath = "FaceAreaPNG/04Eye/Eyecorner";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void UppereyelidItem_chg( )
    {
        string TexturePath = "FaceAreaPNG/04Eye/Uppereyelid";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void DoublefoldEyelidsItem_chg()
    {
        string TexturePath = "FaceAreaPNG/04Eye/DoublefoldEyelids";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void lowereyelidItem_chg()
    {
        string TexturePath = "FaceAreaPNG/04Eye/lowereyelid";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }    
    public void EyebagItem_chg()
    {
        string TexturePath = "FaceAreaPNG/04Eye/Eyebag";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void EyetailItem_chg()
    {
        string TexturePath = "FaceAreaPNG/04Eye/Eyetail";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void BlackeyeItem_chg()
    {
        string TexturePath = "FaceAreaPNG/04Eye/Blackeye";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }



    //nose.........

    public void UpperbridgeItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/Upperbridge";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void InferiorbridgeItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/Inferiorbridge";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void NoseheadItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/Nosehead";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void ColumellaNasiItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/ColumellaNasi";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void NasalBaseItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/NasalBase";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void NoseWingItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/NoseWing";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void NostrilItem_chg()
    {
        string TexturePath = "FaceAreaPNG/05nose/Nostril";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }



    //mouth..............................................................

    public void upperlipItem_chg()
    {
        string TexturePath = "FaceAreaPNG/06Mouth/upperlip";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void upperboneItem_chg()
    {
        string TexturePath = "FaceAreaPNG/06Mouth/upperbone";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void downlipItem_chg()
    {
        string TexturePath = "FaceAreaPNG/06Mouth/downlip";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void downBoneItem_chg()
    {
        string TexturePath = "FaceAreaPNG/06Mouth/downBone";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }  
    public void MiddleItem_chg()
    {
        string TexturePath = "FaceAreaPNG/06Mouth/Middle";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }
    public void cornerItem_chg()
    {
        string TexturePath = "FaceAreaPNG/06Mouth/corner";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);
    }


    //cheset...........................................

    //public void chestupperItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}
    //public void chesttopItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}
    //public void chestdownItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val"></param>
   ///body.............


    //public void neckItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}  
    //public void chestItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}
    //public void wristItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}    
    //public void hipItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}
    //public void LegItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}    
    //public void ArmItem_chg()
    //{
    //    string TexturePath = "FaceAreaPNG/01Shape/BISjaw";
    //    faceAreaTextureChange.ChangeFaceArea(TexturePath);
    //}




}
