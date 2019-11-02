using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class Deform : MonoBehaviour
{

    [HideInInspector]
    public GameObject[] parts;
    public Texture2D[] Makeupparts;

    public Transform rootBoneParent;  
    public GameObject _head, _body, _arm, _eyes, _eyelash, _finger, Rolein;
 

    [HideInInspector]
    public int RoleID = 0;
    Animator animator = null;


    private float pinguo_x = 0.000f;
    private float pinguo_y = 0.000f;
    private float pinguo_scal = 0.000f;


    private float eye_zuoyou = 0.000f;
    private float eye_gaodi = 0.000f;
    private float eye_shenqian = 0.000f;

    private float eyebrow_zuoyou = 0.000f;
    private float eyebrow_gaodi = 0.000f;
    private float eyebrow_shenqian = 0.000f;


    private float mouth_gaodi = 0.000f;
    private float mouth_shenqian = 0.000f;
    private float mouth_sclKUan = 1.000f;
    private float mouth_sckHou = 1.000f;

    private float face_xiabaKUan = 0.000f;
    private float face_xiabachang = 0.000f;


    private float nose_kuandu = 1.00f;
    private float nose_Tingba = 1.00f;


    private float SH_joint1, LJ_joint1;
    private float SH_joint2, LJ_joint2;
    private float SH_joint3, LJ_joint3;
    private float SH_joint4, LJ_joint4;
    private float QG_joint2;
    

    public Dictionary<string, Transform> _bones;


    public void Awake()
    {
        _bones = AppRoot.MainScene.MainRole._bones;
    }




    //面部调节//
    //脸型//shape deform.................................................................... 
    public void SetForeheadSwitch(Vector4 xx)  //双颌
    {

        SH_joint1 = xx.x;
        SH_joint2 = xx.x / 2;
        SH_joint3 = xx.x / 2;
        SH_joint4 = xx.x / 4;

        float SH_var01 = SH_joint1 + LJ_joint1;
        float SH_var02 = SH_joint2 + LJ_joint2;
        float SH_var03 = SH_joint3 + LJ_joint3;
        float SH_var04 = SH_joint4 + LJ_joint4;


        //bones["chin_Lf_joint1"].localPosition = _chin_Lf_joint1 + new Vector3(-xx, 0, 0);
        //bones["chin_Lf_joint2"].localPosition = _chin_Lf_joint07 + new Vector3(0, 0, -xx);
        //bones["chin_Lf_joint3"].localPosition = _chin_Lf_joint3 + new Vector3(-SH_var03, 0, 0);
        //bones["chin_Lf_joint4"].localPosition = _chin_Lf_joint4 + new Vector3(-SH_var04, 0, 0);


        //bones["chin_Rt_joint1"].localPosition = _chin_Rt_joint1 + new Vector3(xx, 0, 0);
        //bones["chin_Rt_joint2"].localPosition = _chin_Rt_joint2 + new Vector3(0, 0, xx);
        //bones["chin_Rt_joint3"].localPosition = _chin_Rt_joint3 + new Vector3(SH_var03, 0, 0);
        //bones["chin_Rt_joint4"].localPosition = _chin_Rt_joint4 + new Vector3(SH_var04, 0, 0);
                     
       
    }
    public void SetTempleSwitch(Vector4  xx)//脸颊
    {
        LJ_joint1 = xx.x / 2;
        LJ_joint3 = xx.x;
        LJ_joint4 = xx.x * 2 / 3;

        float LJ_var01 = SH_joint1 + LJ_joint1;
        float LJ_var03 = SH_joint3 + LJ_joint3;
        float LJ_var04 = SH_joint4 + LJ_joint4;


        //bones["chin_Lf_joint3"].localPosition = _chin_Lf_joint3 - new Vector3(xx, 0, 0);
        //bones["chin_Rt_joint3"].localPosition = _chin_Rt_joint3 + new Vector3(xx, 0, 0);

        //bones["mouthLip_Lf_joint3"].localPosition = _mouthLip_Lf_joint3 - new Vector3(xx, 0, 0);
        //bones["mouthLip_Rt_joint3"].localPosition = _mouthLip_Rt_joint3 + new Vector3(xx, 0, 0);

        //bones["chin_Lf_joint4"].localPosition = _chin_Lf_joint4 - new Vector3(xx, 0, 0);
        //bones["chin_Rt_joint4"].localPosition = _chin_Rt_joint4 + new Vector3(xx, 0, 0);

    }
    public void SetBISjawSwitch(Vector4  xx)//下巴
    {
        face_xiabaKUan = xx.x;
        //bones["chin_joint2"].localPosition = _chin_joint2 + new Vector3(-face_xiabaKUan, face_xiabachang, 0);
        //bones["chin_joint3"].localPosition = _chin_joint3 + new Vector3(face_xiabaKUan, face_xiabachang, 0);
        //Debug.Log(bones["chin_joint2"].localPosition);
    }
    public void SetChinSwitch(Vector4  xx)//下巴长
    {
        face_xiabachang = xx.x;
        //bones["chin_joint1"].localPosition = _chin_joint1 + new Vector3(0, face_xiabachang, 0);
        //bones["chin_joint2"].localPosition = _chin_joint2 + new Vector3(-face_xiabaKUan, face_xiabachang, 0);
        //bones["chin_joint3"].localPosition = _chin_joint3 + new Vector3(face_xiabaKUan, face_xiabachang, 0);

    }
    /// Face deform............
    public void SetApplemuscleSwitch(Vector4  xx)//颧骨
    {
        QG_joint2 = xx.x;

        float QG_var02 = SH_joint2 + QG_joint2;

        //bones["chin_Lf_joint2"].localPosition = _chin_Lf_joint07 - new Vector3(0, 0, QG_var02);
        //bones["chin_Rt_joint2"].localPosition = _chin_Rt_joint2 + new Vector3(0, 0, QG_var02);
        //bones["chin_Lf_joint5"].localPosition = _chin_Lf_joint5 - new Vector3(0, 0, xx / 2);
        //bones["chin_Rt_joint5"].localPosition = _chin_Rt_joint5 + new Vector3(0, 0, xx / 2);

        //Debug.Log(bones["chin_Lf_joint2"].localPosition);
    }
    public void SetCheekbonesSwitch(Vector4  xx)//苹果肌
    {
        pinguo_y = xx.x;

        if (pinguo_y < -0.1f)
        {

            pinguo_y = -0.1f;
        }


        //bones["cheek_Lf_joint1"].localScale = new Vector3(xx * 0.5f + 1, xx * 0.2f + 1, xx + 1);
        //bones["cheek_Rt_joint1"].localScale = new Vector3(xx * 0.5f + 1, xx * 0.2f + 1, xx + 1);

        //bones["cheek_Lf_joint1"].localPosition = _cheek_Lf_joint1 - new Vector3(0, 5f * (pinguo_x + pinguo_y), 0);
        //bones["cheek_Rt_joint1"].localPosition = _cheek_Rt_joint1 - new Vector3(0, 5f * (pinguo_x + pinguo_y), 0);
        //Debug.Log(bones["cheek_Lf_joint1"].localPosition);

    }
    public void SetFacialpartSwitch(Vector4  xx)//咬肌
    {
        float yaoji_y = xx.x;

        if (yaoji_y < -0.1f)
        {

            yaoji_y = -0.1f;
        }

        //bones["cheek_Lf_joint2"].localScale = new Vector3(xx + 1, xx * 0.2f + 1, xx + 1);
        //bones["cheek_Lf_joint3"].localScale = new Vector3(xx + 1, xx * 0.2f + 1, xx + 1);

        //bones["cheek_Lf_joint2"].localPosition = _cheek_Lf_joint2 - new Vector3(0, yaoji_y * 3f, 0);
        //bones["cheek_Rt_joint2"].localPosition = _cheek_Rt_joint2 - new Vector3(0, yaoji_y * 3f, 0);
        //Debug.Log(bones["cheek_Lf_joint2"].localPosition);

        //pinguo_scal = xx;
    }
    //额头..........................................................
    private float Forehead_kuan = 0.000f;
    private float Forehead_kuan2 = 0.000f;
    private float Forehaed_qianhou = 0.000f;
    private float Forehead_faji = 0.000f;


    private float FH_Top_Y = 0.000f;
    private float FH_TYX_X = 0.000f;


    // 眉毛..............................................
    public void SetBrowbowSwitch(Vector4  xx)   //
    {
        //bones["brow_Lf_joint1"].localScale = new Vector3(1, xx, 1);
       

    }
    public void SetBrowHeadSwitch(Vector4  xx)   //
    {
        eyebrow_zuoyou = xx.x;

        //bones["brow_Lf_joint0"].localPosition = _brow_Lf_joint0 + new Vector3(-eyebrow_gaodi, eyebrow_shenqian, eyebrow_zuoyou);  
    }

    public void SetBrowMiddleSwitch(Vector4  xx)   //
    {
        eyebrow_gaodi = xx.x;
        //bones["brow_Lf_joint0"].localPosition = _brow_Lf_joint0 + new Vector3(-eyebrow_gaodi, eyebrow_shenqian, eyebrow_zuoyou);
        //bones["brow_Rt_joint0"].localPosition = _brow_Rt_joint0 + new Vector3(-eyebrow_gaodi, eyebrow_shenqian, -eyebrow_zuoyou);
    }
    public void SetBrowTailSwitch(Vector4  xx)   //
    {
        //bones["brow_Rt_joint0"].localPosition = _brow_Rt_joint0 + new Vector3(-eyebrow_gaodi, eyebrow_shenqian, -eyebrow_zuoyou);

    }
   

    // 眼睛。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。

    public void SetEyecornerSwitch(Vector4  xx)   {


        xx.x = (xx.x - 1) / 2 + 1;

        //bones["face_eyeBall_Lf_joint1"].localScale = new Vector3(xx, xx, xx);
        //bones["face_eyeBall_Rt_joint1"].localScale = new Vector3(xx, xx, xx);

        //RoleDef selfDef = TableMgr.Instance.RoleDic[RoleID];
        //Vector3 eyeballround = ((selfDef._eyeBall_Lf_joint1 - selfDef.offset_face_eyeBall_Lf_joint1) - (selfDef._eyeLidsdown_Lf_joint2 - selfDef.offset_face_eyeBall_Rt_joint1)) / 100;
        //float eyemove = Mathf.Abs(eyeballround.y) * (xx - 1);
        //float eyemove = 0.5f * (xx - 1);

        //Debug.Log(selfDef._eyeBall_Lf_joint1);
        //Debug.Log(selfDef._eyeLidsdown_Lf_joint2);
        //Debug.Log(eyeballround.y);
        //Debug.Log(xx);

        //bones["face_eyeBall_Lf_joint1"].localPosition += new Vector3(eyemove, 0, 0);
        //bones["face_eyeBall_Rt_joint1"].localPosition += new Vector3(eyemove, 0, 0);


    }
    public void SetUppereyelidSwitch(Vector4  xx)   //
    {
        //bones["eyeLids_Lf_joint0"].localScale = new Vector3(xx, xx, 1);
        //bones["eyeLids_Rt_joint0"].localScale = new Vector3(xx, xx, 1);

        //bones["eyeBall_Lf_joint1"].localScale = new Vector3(xx, xx, 1);
        //bones["eyeBall_Rt_joint1"].localScale = new Vector3(xx, xx, 1);

        //float pinguo_s2 = xx;

        //if (pinguo_s2 < 1) { pinguo_s2 = 1; }


        //bones["cheek_Lf_joint1"].localScale = new Vector3((pinguo_scal + 1) * ((pinguo_s2 - 1) / 2 + 1), (pinguo_scal * 0.2f + 1) * ((pinguo_s2 - 1) / 2 + 1), (pinguo_scal + 1) * ((pinguo_s2 - 1) / 2 + 1));
        //bones["cheek_Rt_joint1"].localScale = new Vector3((pinguo_scal + 1) * ((pinguo_s2 - 1) / 2 + 1), (pinguo_scal * 0.2f + 1) * ((pinguo_s2 - 1) / 2 + 1), (pinguo_scal + 1) * ((pinguo_s2 - 1) / 2 + 1));


    }
    public void SetDoublefoldEyelidsSwitch(Vector4  xx)   //
    {
        //eye_zuoyou = xx;
        //bones["eyeLids_Lf_joint0"].localPosition = _eyeLids_Lf_joint0 + new Vector3(-eye_gaodi, -eye_shenqian, eye_zuoyou);
        //bones["eyeLids_Rt_joint0"].localPosition = _eyeLids_Rt_joint0 + new Vector3(-eye_gaodi, -eye_shenqian, -eye_zuoyou);

        //bones["eyeBall_Lf_joint1"].localPosition = _eyeBall_Lf_joint1 + new Vector3(-eye_gaodi, -eye_shenqian, eye_zuoyou);
        //bones["eyeBall_Rt_joint1"].localPosition = _eyeBall_Rt_joint1 + new Vector3(-eye_gaodi, -eye_shenqian, -eye_zuoyou);

    }
    public void SetlowereyelidSwitch(Vector4  xx)   //
    {
        //eye_gaodi = xx;

        //bones["eyeLids_Lf_joint0"].localPosition = _eyeLids_Lf_joint0 + new Vector3(-eye_gaodi, -eye_shenqian, eye_zuoyou);
        //bones["eyeLids_Rt_joint0"].localPosition = _eyeLids_Rt_joint0 + new Vector3(-eye_gaodi, -eye_shenqian, -eye_zuoyou);

        //bones["eyeBall_Lf_joint1"].localPosition = _eyeBall_Lf_joint1 + new Vector3(-eye_gaodi, -eye_shenqian, eye_zuoyou);
        //bones["eyeBall_Rt_joint1"].localPosition = _eyeBall_Rt_joint1 + new Vector3(-eye_gaodi, -eye_shenqian, -eye_zuoyou);

    }
    public void SetEyebagSwitch(Vector4 xx)   //
    {

        //eye_shenqian = xx;

        //if (eye_shenqian > 0.00015f) { eye_shenqian = 0.00015f; }

        //bones["eyeLids_Lf_joint0"].localPosition = _eyeLids_Lf_joint0 + new Vector3(-eye_gaodi, -eye_shenqian, eye_zuoyou);
    

    }
    public void SetEyetailSwitch(Vector4  xx)   //
    {

        //bones["eyeLids_Lf_joint1"].localPosition = _eyeLids_Lf_joint1 + new Vector3(0, xx, 0);
        //bones["eyeLids_Rt_joint1"].localPosition = _eyeLids_Rt_joint1 + new Vector3(0, xx, 0);

        //bones["eyeLidsdown_Lf_joint1"].localPosition = _eyeLidsdown_Lf_joint1 + new Vector3(0, xx / 2, 0);
        //bones["eyeLidsdown_Rt_joint1"].localPosition = _eyeLidsdown_Rt_joint1 + new Vector3(0, xx / 2, 0);

        //bones["eyeLidsUp_Lf_joint1"].localPosition = _eyeLidsUp_Lf_joint1 + new Vector3(0, xx / 2, 0);
        //bones["eyeLidsUp_Rt_joint1"].localPosition = _eyeLidsUp_Rt_joint1 + new Vector3(0, xx / 2, 0);



    }
    public void SetBlackeyeSwitch(Vector4  xx)   //
    {
        //bones["eyeLids_Lf_joint2"].localPosition = _eyeLids_Lf_joint2 + new Vector3(0, xx, 0);
        //bones["eyeLids_Rt_joint2"].localPosition = _eyeLids_Rt_joint2 + new Vector3(0, xx, 0);

        //bones["eyeLidsdown_Lf_joint3"].localPosition = _eyeLidsdown_Lf_joint3 + new Vector3(0, xx / 2, 0);
        //bones["eyeLidsdown_Rt_joint3"].localPosition = _eyeLidsdown_Rt_joint3 + new Vector3(0, xx / 2, 0);

        //bones["eyeLidsUp_Lf_joint3"].localPosition = _eyeLidsUp_Lf_joint3 + new Vector3(0, xx / 2, 0);
        //bones["eyeLidsUp_Rt_joint3"].localPosition = _eyeLidsUp_Rt_joint3 + new Vector3(0, xx / 2, 0);



    }
 
    //鼻子...................................................
    public void SetUpperbridgeSwitch(Vector4  xx)   //宽度
    {
        //nose_kuandu = xx;
        //bones["nose_joint0"].localScale = new Vector3(nose_kuandu, 1, nose_Tingba);
    }
    public void SetInferiorbridgeSwitch(Vector4  xx)   //上下
    {
        //bones["nose_joint0"].localPosition = _nose_joint0 + new Vector3(xx, 0, 0);

    }
    public void SetNoseheadSwitch(Vector4  xx)   //挺拔
    {
        //nose_Tingba = xx;
        //bones["nose_joint0"].localScale = new Vector3(nose_kuandu, 1, nose_Tingba);

    }
    public void SetColumellaNasiSwitch(Vector4  xx)   //鼻头
    {
        //bones["nose_joint1"].localScale = new Vector3(xx, xx, xx);
    }
    public void SetNasalBaseSwitch(Vector4  xx)   //鼻梁
    {

        //bones["bridge_joint2"].localPosition = _bridge_joint2 - new Vector3(0, xx, 0);
        //bones["bridge_joint1"].localPosition = _bridge_joint1 - new Vector3(0, xx / 3, 0);
        //bones["nose_joint1"].localPosition = _nose_joint1 - new Vector3(0, 0, -xx / 2);

    }

    public void SetNoseWingSwitch(Vector4 xx)   //鼻梁
    {

        //bones["bridge_joint2"].localPosition = _bridge_joint2 - new Vector3(0, xx, 0);
        //bones["bridge_joint1"].localPosition = _bridge_joint1 - new Vector3(0, xx / 3, 0);
        //bones["nose_joint1"].localPosition = _nose_joint1 - new Vector3(0, 0, -xx / 2);

    }
    public void SetNostrilSwitch(Vector4 xx)   //鼻梁
    {

        //bones["bridge_joint2"].localPosition = _bridge_joint2 - new Vector3(0, xx, 0);
        //bones["bridge_joint1"].localPosition = _bridge_joint1 - new Vector3(0, xx / 3, 0);
        //bones["nose_joint1"].localPosition = _nose_joint1 - new Vector3(0, 0, -xx / 2);

    }


    // 嘴巴//。。。。。。。。。。。。。。。。。。。。。。。。。。
    public void SetUplipSwitch(Vector4  xx)   //高度
    {
        //mouth_gaodi = xx;
        //bones["mouthLip_joint0"].localPosition = _mouthLip_joint0 + new Vector3(mouth_gaodi, mouth_shenqian, 0);

    }
    public void SetUpjawSwitch(Vector4  xx)   //深浅
    {
        //mouth_shenqian = xx;
        //bones["mouthLip_joint0"].localPosition = _mouthLip_joint0 + new Vector3(mouth_gaodi, mouth_shenqian, 0);
    }
    public void SetDownLipSwitch(Vector4  xx)   //宽度
    {
        //mouth_sclKUan = xx;
        //bones["mouthLip_joint0"].localScale = new Vector3(mouth_sclKUan, mouth_sckHou, 1);

    }
    public void SetDownJawSwitch(Vector4  xx)   //厚度
    {
        //mouth_sckHou = xx;
        //bones["mouthLip_joint0"].localScale = new Vector3(mouth_sclKUan, mouth_sckHou, 1);
    }
    public void SetPhiltrumSwitch(Vector4  xx)   //上唇
    {
        //bones["mouthLip_up_joint1"].localPosition = _mouthLip_up_joint1 + new Vector3(0, 0, xx);
        //bones["mouthLip_up_joint2"].localPosition = _mouthLip_up_joint2 + new Vector3(0, 0, xx);


    }
    public void SetCornerSwitch(Vector4  xx)   //下唇
    {
        //bones["mouthLip_dn_joint1"].localPosition = _mouthLip_dn_joint1 + new Vector3(0, 0, xx);
        //bones["mouthLip_dn_joint2"].localPosition = _mouthLip_dn_joint2 + new Vector3(0, 0, xx);        
    }

    // chest
    public void SetupperItemSwitch(Vector4 xx)   //上部
    {
        //bones["mouthLip_dn_joint1"].localPosition = _mouthLip_dn_joint1 + new Vector3(0, 0, xx);
        //bones["mouthLip_dn_joint2"].localPosition = _mouthLip_dn_joint2 + new Vector3(0, 0, xx);        
    }
    public void SettopItemSwitch(Vector4 xx)   //乳尖
    {
        //bones["mouthLip_dn_joint1"].localPosition = _mouthLip_dn_joint1 + new Vector3(0, 0, xx);
        //bones["mouthLip_dn_joint2"].localPosition = _mouthLip_dn_joint2 + new Vector3(0, 0, xx);        
    }
    public void SetdownItemSwitch(Vector4 xx)   //下部
    {
        //bones["mouthLip_dn_joint1"].localPosition = _mouthLip_dn_joint1 + new Vector3(0, 0, xx);
        //bones["mouthLip_dn_joint2"].localPosition = _mouthLip_dn_joint2 + new Vector3(0, 0, xx);        
    }

    //默认体型参数

    //public void SetBodyhigh(float xx)
    //{

    //    float highbase = AppRoot.MainUser.RoletableID;

    //    Vector3 scalebase = AppRoot.MainScene.MainRole.gameObject.transform.localScale;
    //    AppRoot.MainScene.MainRole.gameObject.transform.localScale = new Vector3(1, 1, 1) / highbase * xx;
    //    curhigh_ST = AppRoot.MainUser.CURhigh;
    //    curweight_ST = AppRoot.MainUser.CURWeight;
    //    AppRoot.MainUser.CURWeight = AppRoot.MainUser.CURWeight;

    //    Debug.Log(curweight_ST);

    //}//身高//
    //public void SetWeight(float xx)
    //{//体重//f

    //    float basehigh = AppRoot.MainUser._highbase;
    //    float baseweight = AppRoot.MainUser._Weightbase;

    //    float curhigh = AppRoot.MainUser.CURhigh + AppRoot.MainUser._highbase;
    //    float curweight = xx + AppRoot.MainUser._Weightbase;

    //    Debug.Log("_Weightbase" + AppRoot.MainUser._Weightbase);


    //    float standBMI = baseweight / (basehigh * basehigh / 10000);
    //    float popBMI = curweight / (2 * curhigh * curhigh / 10000);

    //    Debug.Log("standBMI" + standBMI);
    //    Debug.Log("popBMI " + popBMI);
    //    Debug.Log("curweight" + curweight);
    //    Debug.Log("CURhigh" + AppRoot.MainUser.CURhigh);
    //    Debug.Log("xx" + AppRoot.MainUser.CURWeight);
    //    float PS_Scal = popBMI / standBMI;


    //    Debug.Log("PS_Scal" + PS_Scal);

    //    AppRoot.MainUser.CURChestline = (PS_Scal - 1) * 3;
    //    AppRoot.MainUser.CURCup = PS_Scal - 1;
    //    AppRoot.MainUser.CURWaistline = (PS_Scal - 1) * 4.5f;
    //    AppRoot.MainUser.CURHipline = (PS_Scal - 1) * 3;
    //    AppRoot.MainUser.CURArmline = (PS_Scal - 1) * 3;
    //    AppRoot.MainUser.CURThigh = (PS_Scal - 1) * 3;


    //}
    public void SetNeckSwitch(Vector4 xx)
    {

        float scale_x = xx.w;
        float scale_y = 1f;
        float scale_z = xx.w;
        //  bones["Character1_Chest_scale00"].localScale = new Vector3(scale_x, scale_y, scale_z);

    }//胸围//
    public void SetChestSwitch(Vector4 xx)
    {

        float scale_x = xx.w;
        float scale_y = 1f;
        float scale_z = xx.w;

        //bones["Character1_Chest_scale00"].localScale = new Vector3(scale_x, scale_y, scale_z);


    }//胸围//    
    public void SetWristSwitch(Vector4 xx)  //腰围//
    {

        float scale_x = xx.w;
        float scale_y = 1f;
        float scale_z = xx.w;
        float scale_z2 = scale_z;

        //if (scale_z > 1)
        //{
        //    scale_z2 = (scale_z - 1) * 2 + 1;

        //}
        //else
        //{
        //    scale_z2 = (scale_z - 1) * 0.5f + 1;
        //}


        //bones["Character1_Waist00_joint00"].localScale = new Vector3(scale_x, scale_y, scale_z2);
        //bones["Character1_Waist_scale00_GRP1"].localScale = new Vector3(scale_x, scale_y, scale_z2);
        //bones["Character1_Waist2_scale00_GRP1"].localScale = new Vector3(scale_x, scale_y, scale_z);


    }
    public void SetHipSwitch(Vector4 xx)   //臀围//
    {
        float scale_x = xx.w;
        float scale_y = xx.w;
        float scale_z = 1f;

        //bones["Character1_Hips_scale00"].localScale = new Vector3(scale_x, scale_y, scale_z);


    }
    public void SetLegSwitch(Vector4 xx)   //腿围//
    {
        //float Thigh_b = AppRoot.MainUser._Thighbase;
        //float scale_xl = 1f;
        //float scale_yl = xx / Thigh_b;
        //float scale_zl = xx / Thigh_b;

        //bones["Character1_LeftUpLeg_scale"].localScale = new Vector3(scale_xl, scale_yl, scale_zl);

        //float scale_xr = 1f;
        //float scale_yr = xx / Thigh_b;
        //float scale_zr = xx / Thigh_b;

        //bones["Character1_RightUpLeg_scale"].localScale = new Vector3(scale_xr, scale_yr, scale_zr);



        //float scale_xls = 1f;
        //float scale_yls = (1 + (xx - Thigh_b) / (1.5f * Thigh_b));
        //float scale_zls = (1 + (xx - Thigh_b) / (1.5f * Thigh_b));

        //bones["Character1_LeftLeg_scale"].localScale = new Vector3(scale_xls, scale_yls, scale_zls);

        //float scale_xrs = 1f;
        //float scale_yrs = (1 + (xx - Thigh_b) / (1.5f * Thigh_b));
        //float scale_zrs = (1 + (xx - Thigh_b) / (1.5f * Thigh_b));

        //bones["Character1_RightLeg_scale_GRP1"].localScale = new Vector3(scale_xrs, scale_yrs, scale_zrs);


    }
    public void SetArmSwitch(Vector4 xx)   //臂围//
    {

        //float Armline_b = AppRoot.MainUser._Armlinebase;
        //float Thigh_b = AppRoot.MainUser._Thighbase;
        //float scale_xl = 1f;
        //float scale_yl = xx / Armline_b;
        //float scale_zl = xx / Armline_b;

        //bones["Character1_LeftArm_scale_GRP"].localScale = new Vector3(scale_xl, scale_yl, scale_zl);

        //float scale_xr = 1f;
        //float scale_yr = xx / Armline_b;
        //float scale_zr = xx / Armline_b;

        //bones["Character1_RightArm_scale_GRP"].localScale = new Vector3(scale_xr, scale_yr, scale_zr);




        //float scale_xlm = 1f;
        //float scale_ylm = (1 + (xx - Armline_b) / (1.5f * Armline_b));
        //float scale_zlm = (1 + (xx - Armline_b) / (1.5f * Armline_b));

        //bones["Character1_LeftForeArm05_scale_GRP"].localScale = new Vector3(scale_xlm, scale_ylm, scale_zlm);

        //float scale_xrm = 1f;
        //float scale_yrm = (1 + (xx - Armline_b) / (1.5f * Armline_b));
        //float scale_zrm = (1 + (xx - Armline_b) / (1.5f * Armline_b));

        //bones["Character1_RightForeArm05_scale_GRP1"].localScale = new Vector3(scale_xrm, scale_yrm, scale_zrm);



        //float scale_xls = 1f;
        //float scale_yls = (1 + (xx - Armline_b) / (1.5f * Armline_b));
        //float scale_zls = (1 + (xx - Armline_b) / (1.5f * Armline_b));

        //bones["Character1_LeftForeArm_scale_GRP"].localScale = new Vector3(scale_xls, scale_yls, scale_zls);

        //float scale_xrs = 1f;
        //float scale_yrs = (1 + (xx - Armline_b) / (1.5f * Armline_b));
        //float scale_zrs = (1 + (xx - Armline_b) / (1.5f * Armline_b));

        //bones["Character1_RightForeArm_scale_GRP"].localScale = new Vector3(scale_xrs, scale_yrs, scale_zrs);


    }




    /// <summary>
    ///  //重置面部......................................................................
    /// </summary>


    public void ResetBones()
    {
        RoleDef selfDef = TableMgr.Instance.RoleDic[RoleID];     
        FacebonesDef facebones = selfDef.facebones;
        
       
        
        //Type type = typeof(facebones);
        //FieldInfo[] fields = type.GetFields();
        //foreach (FieldInfo f in fields)
        //{
        //    Debug.Log("属性 " + f.Attributes + "  " + f + "=" + f.GetValue(facebones)); // Debug.Log(f.Name);

        //}

        // ChangposSize(bones["tooth_up_joint3"], selfDef._tooth_up_joint3);

    }


    void ChangposSize(Transform bons, Vector3 pos)
    {

        Vector3 pos0 = bons.localPosition;

        string pos00 = pos0.ToString("f4");

        Vector3 pos1 = new Vector3(pos.x * 0.01f, pos.y * 0.01f, pos.z * 0.01f);

        string pos11 = pos1.ToString("f4");

        bons.localPosition = pos1;
        /*
                if (pos00 != pos11) {
                    Debug.Log(bons);
                }
            */
    }

    /// <summary>
    ///  //重置面部......................................................................
    /// </summary>































}
