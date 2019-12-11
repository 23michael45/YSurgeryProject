using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

//捏脸变形

public class Deform : MonoBehaviour
{


    
    private Vector3 Temple_cache = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 Temple_cache2 = new Vector3(0.0000f, 0.0000f, 0.0000f);

    private Vector3 BISjaw_cache = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 BISjaw_cache2 = new Vector3(0.0000f, 0.0000f, 0.0000f);

    private Vector3 BISjaw_cache3 = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 BISjaw_cache4 = new Vector3(0.0000f, 0.0000f, 0.0000f);
    
    private Vector3 Chin_cache  = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 Chin_cache2 = new Vector3(0.0000f, 0.0000f, 0.0000f);


    private Vector3 Chin4_cache  = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 Chin4_cache2 = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 check_cache  = new Vector3(0.0000f, 0.0000f, 0.0000f);
    private Vector3 check_cache2 = new Vector3(0.0000f, 0.0000f, 0.0000f);
    




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


    public float ratio=100f;


    public Dictionary<string, Transform> bones;
    public Dictionary<string, Vector3> bonespos;


    public void init()
    {
        bones = AppRoot.MainRole._bones;
        bonespos = AppRoot.MainRole.bonesPostion;
       
    }




    //面部调节//
    //脸型//shape deform.................................................................... 
    public void SetForeheadSwitch(Vector4 xx)  //额头
    {
     
        

        xx = xx / ratio;
        Debug.Log(xx);

        Vector3  offset = new Vector3(0, xx.x, xx.z);
        Vector3 offset_l = new Vector3(xx.y,-xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z );



        DeformBonePos("face_forehead_joint1", offset);
        DeformBonePos("face_forehead_joint2", offset);



        DeformBonePos("face_forehead_Lf_joint1", offset_l);
        DeformBonePos("face_forehead_Lf_joint2", offset_l);
        //DeformBonePos("face_forehead_Lf_joint4", offset_l);
        DeformBonePos("face_forehead_Lf_joint5", offset_l);



        DeformBonePos("face_forehead_Rt_joint1", offset_r);
        DeformBonePos("face_forehead_Rt_joint2", offset_r);
        //DeformBonePos("face_forehead_Rt_joint4", offset_r);
        DeformBonePos("face_forehead_Rt_joint5", offset_r);



        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
       // DeformBoneScale("face_forehead_joint1", Sacle);

        
    }
    public void SetTempleSwitch(Vector4  xx)//太阳穴
    {
       

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.x, xx.z);
        Vector3 offset_l = new Vector3(xx.y, -xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z);



        DeformBonePos("face_forehead_Lf_joint4", offset_l);
        //DeformBonePos("face_temple_Lf_joint3", offset_l);
        //DeformBonePos("face_temple_Lf_joint2", offset_l);
        //DeformBonePos("face_temple_Lf_joint004", offset_l);


        DeformBonePos("face_forehead_Rt_joint4", offset_r);
        //DeformBonePos("face_temple_Rt_joint3", offset_r);
        //DeformBonePos("face_temple_Rt_joint2", offset_r);
        //DeformBonePos("face_temple_Rt_joint004", offset_r);
        

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        // DeformBoneScale("face_forehead_joint1", Sacle);

    }
    public void SetBISjawSwitch(Vector4  xx)//双颌
    {
        face_xiabaKUan = xx.x;


        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.x, xx.z);
        Vector3 offset_l = new Vector3(xx.y, -xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z);

        Vector3 offset_l2 = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r2 = new Vector3(xx.x, xx.y, xx.z);

        BISjaw_cache= new Vector3(xx.y/2, -xx.x/2, xx.z/2);
        Vector3 face_chin_Lf_joint08 = BISjaw_cache + Temple_cache;

        
        DeformBonePos("face_chin_Lf_joint08", face_chin_Lf_joint08);
        DeformBonePos("face_chin_Lf_joint07", offset_l);
        DeformBonePos("face_chin_Lf_joint06", offset_l);
        DeformBonePos("face_chin_Lf_joint05", offset_l2);
        DeformBonePos("face_chin_Lf_joint04", offset_l2);

        DeformBonePos("face_chin_Lf_joint03", offset_l2);


        //Chin4_cache = new Vector3(xx.y / 2, -xx.x / 2, xx.z / 2);
        //Vector3 face_check_Lf_joint4 = Chin4_cache + check_cache;
        //DeformBonePos("face_check_Lf_joint4", face_check_Lf_joint4);       
        //DeformBonePos("face_check_Lf_joint2", face_check_Lf_joint4);        


        BISjaw_cache3 = new Vector3(-xx.x/2, xx.y/2, xx.z/2);
        Vector3 face_chin_Lf_joint02 = Chin_cache + BISjaw_cache3;

        DeformBonePos("face_chin_Lf_joint02", face_chin_Lf_joint02);

        //DeformBonePos("face_ear_Lf_joint2", offset_l);
        //DeformBonePos("face_ear_Lf_joint1", offset_l);


        BISjaw_cache2 = new Vector3(xx.y / 2, -xx.x / 2, xx.z / 2);
        Vector3 face_chin_Rt_joint08 = BISjaw_cache2 + Temple_cache2;

        DeformBonePos("face_chin_Rt_joint08", face_chin_Rt_joint08);
        DeformBonePos("face_chin_Rt_joint07", offset_r);
        DeformBonePos("face_chin_Rt_joint06", offset_r);               
        DeformBonePos("face_chin_Rt_joint05", offset_r2);

        DeformBonePos("face_chin_Rt_joint04", offset_r2);
        DeformBonePos("face_chin_Rt_joint03", offset_r2);

        //Chin4_cache2 = new Vector3(xx.y / 2, -xx.x / 2, xx.z / 2);
        // Vector3 face_check_Rt_joint4 = Chin4_cache2 + check_cache2;
        //DeformBonePos("face_check_Rt_joint4", face_check_Rt_joint4);        
        //DeformBonePos("face_check_Rt_joint2", face_check_Rt_joint4);
               
        Chin_cache2 = new Vector3(xx.x/2, xx.y/2, xx.z/2);
        Vector3 face_chin_Rt_joint02 = Chin_cache2 + BISjaw_cache4;
        DeformBonePos("face_chin_Rt_joint02", face_chin_Rt_joint02);

        //DeformBonePos("face_ear_Rt_joint2", offset_r);
        //DeformBonePos("face_ear_Rt_joint1", offset_r);


        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        // DeformBoneScale("face_forehead_joint1", Sacle);

    }
    public void SetChinSwitch(Vector4  xx)//下巴
    {


        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, -xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, -xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, -xx.y, xx.z);


        Chin_cache= new Vector3(xx.x/2, xx.y/2, xx.z/2);
        Vector3 face_chin_Lf_joint02 = Chin_cache + BISjaw_cache3;

        DeformBonePos("face_chin_Lf_joint02", face_chin_Lf_joint02);
        DeformBonePos("face_chin_joint2", offset_l);

        Vector3 face_Orbicular_Lf_joint1 = new Vector3(-xx.x / 2, -xx.y / 2, xx.z / 2);
        DeformBonePos("face_Orbicular_Lf_joint1", face_Orbicular_Lf_joint1);




        Chin_cache2 = new Vector3(xx.x/2, -xx.y/2, xx.z/2);
        Vector3 face_chin_Rt_joint02 = Chin_cache2 + BISjaw_cache4;

        DeformBonePos("face_chin_Rt_joint02", face_chin_Rt_joint02);
        DeformBonePos("face_chin_joint3", offset_r);

        Vector3 face_Orbicular_Rt_joint1 = new Vector3(-xx.x / 2, -xx.y / 2, xx.z / 2);
        DeformBonePos("face_Orbicular_Rt_joint1", face_Orbicular_Rt_joint1);

        DeformBonePos("face_chin_joint1", offset);

        DeformBonePos("face_Orbicular_joint1", offset);


        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
      //  DeformBoneScale("face_forehead_joint1", Sacle);



    }
    public void SetTopHead(Vector4 xx)//头顶
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, -xx.y, xx.z);   
        DeformBonePos("face_calvaria_joint1", offset);


        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("face_calvaria_joint1", Sacle);

    }
    /// Face deform............
    public void SetApplemuscleSwitch(Vector4  xx)//苹果肌
    {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.x, xx.z);
        Vector3 offset_l = new Vector3(xx.y, -xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z);



        DeformBonePos("face_L_check_Bone002", offset_l); 
        DeformBonePos("face_R_check_Bone002", offset_r); 


       

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_L_check_Bone002", Sacle);
        DeformBoneScale("face_R_check_Bone002", Sacle);
       



    }
    public void SetCheekbonesSwitch(Vector4  xx)//颧骨
    {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.x, xx.z);
        Vector3 offset_l = new Vector3(xx.y, -xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z);


        //check_cache = offset_l;
        //Vector3 face_check_Lf_joint3 = Chin4_cache + check_cache;

        DeformBonePos("face_check_Lf_joint3", offset_l);


        //check_cache2 = offset_r;
        //Vector3 face_check_Rt_joint3 = Chin4_cache2 + check_cache2;
        DeformBonePos("face_check_Rt_joint3", offset_r);


        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
      
        DeformBoneScale("face_check_Lf_joint3", Sacle);
        DeformBoneScale("face_check_Rt_joint3", Sacle);

    }
    public void SetFacialpartSwitch(Vector4  xx)//脸部
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);



        DeformBonePos("face_check_Lf_joint1", offset_l);
        DeformBonePos("face_check_Lf_joint2", offset_l);

        //DeformBonePos("face_Orbicular_Lf_joint1", offset_l);
        //DeformBonePos("face_Orbicular_Lf_joint2", offset_l);
        //DeformBonePos("face_Orbicular_Lf_joint3", offset_l);
               

        DeformBonePos("face_check_Rt_joint1", offset_r);
        DeformBonePos("face_check_Rt_joint2", offset_r);

        //DeformBonePos("face_Orbicular_Rt_joint1", offset_r);        
        //DeformBonePos("face_Orbicular_Rt_joint2", offset_r);
        //DeformBonePos("face_Orbicular_Rt_joint3", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_check_Lf_joint1", Sacle);
        DeformBoneScale("face_check_Lf_joint2", Sacle);

        //DeformBoneScale("face_Orbicular_Lf_joint1", Sacle);
        //DeformBoneScale("face_Orbicular_Lf_joint2", Sacle);
        //DeformBoneScale("face_Orbicular_Lf_joint3", Sacle);

        DeformBoneScale("face_check_Rt_joint1", Sacle);
        DeformBoneScale("face_check_Rt_joint2", Sacle);

        //DeformBoneScale("face_Orbicular_Rt_joint1", Sacle);
        //DeformBoneScale("face_Orbicular_Rt_joint2", Sacle);
        //DeformBoneScale("face_Orbicular_Rt_joint3", Sacle);




    }

    public void SetMasseterMuscle(Vector4 xx)//咬肌
    {    
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.x, xx.z);
        Vector3 offset_l = new Vector3(xx.y, -xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z);

        Vector3 offset_l2 = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r2 = new Vector3(xx.x, xx.y, xx.z);

        BISjaw_cache = new Vector3(xx.y / 2, -xx.x / 2, xx.z / 2);
        Vector3 face_chin_Lf_joint08 = BISjaw_cache + Temple_cache;

        DeformBonePos("face_chin_Lf_joint05", offset_l2);
        DeformBonePos("face_chin_Lf_joint04", offset_l2);
        DeformBonePos("face_chin_Lf_joint03", offset_l2);

        BISjaw_cache3 = new Vector3(-xx.x / 2, xx.y / 2, xx.z / 2);
        Vector3 face_chin_Lf_joint02 = Chin_cache + BISjaw_cache3;

        DeformBonePos("face_chin_Lf_joint02", face_chin_Lf_joint02);

        BISjaw_cache2 = new Vector3(xx.y / 2, -xx.x / 2, xx.z / 2);
        Vector3 face_chin_Rt_joint08 = BISjaw_cache2 + Temple_cache2;

       
        DeformBonePos("face_chin_Rt_joint05", offset_r2);
        DeformBonePos("face_chin_Rt_joint04", offset_r2);
        DeformBonePos("face_chin_Rt_joint03", offset_r2);

        Chin_cache2 = new Vector3(xx.x / 2, xx.y / 2, xx.z / 2);
        Vector3 face_chin_Rt_joint02 = Chin_cache2 + BISjaw_cache4;
        DeformBonePos("face_chin_Rt_joint02", face_chin_Rt_joint02);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        // DeformBoneScale("face_forehead_joint1", Sacle);


    }
    private float Forehead_kuan = 0.000f;
    private float Forehead_kuan2 = 0.000f;
    private float Forehaed_qianhou = 0.000f;
    private float Forehead_faji = 0.000f;


    private float FH_Top_Y = 0.000f;
    private float FH_TYX_X = 0.000f;


    // 眉毛..............................................
    public void SetBrowbowSwitch(Vector4  xx)   //
    {
        xx = xx / ratio;
        //Debug.Log(xx);
        
        Vector3 offset_l = new Vector3(xx.y, -xx.x, xx.z);
        Vector3 offset_r = new Vector3(xx.y, xx.x, xx.z);


        DeformBonePos("face_brow_Lf_joint0", offset_l);       
        DeformBonePos("face_brow_Rt_joint0", offset_r);     


        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint0", Sacle);
        //DeformBoneScale("face_brow_Rt_joint0", Sacle);
     



    }
    public void SetBrowHeadSwitch(Vector4  xx)   //
    {
        xx = xx / ratio;
        //Debug.Log(xx);

       
        Vector3 offset_l = new Vector3(-xx.x,xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x ,xx.y, xx.z);

        DeformBonePos("face_brow_Lf_joint1", offset_l);
        DeformBonePos("face_brow_Lf_joint2", offset_l);

        DeformBonePos("face_brow_Rt_joint1", offset_r);
        DeformBonePos("face_brow_Rt_joint2", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_brow_Lf_joint1", Sacle);
        DeformBoneScale("face_brow_Lf_joint2", Sacle);
        DeformBoneScale("face_brow_Rt_joint1", Sacle);
        DeformBoneScale("face_brow_Rt_joint2", Sacle);

       // Debug.Log(xx);
    }

    public void SetBrowMiddleSwitch(Vector4  xx)   //
    {

        xx = xx / ratio;
        //Debug.Log(xx);


        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);



        DeformBonePos("face_brow_Lf_joint3", offset_l);       
        DeformBonePos("face_brow_Rt_joint3", offset_r);        

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_brow_Lf_joint3", Sacle);
        DeformBoneScale("face_brow_Rt_joint3", Sacle);
       // Debug.Log(xx);

    }
    public void SetBrowTailSwitch(Vector4  xx)   //
    {
        xx = xx / ratio;
        //Debug.Log(xx);


        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);


        DeformBonePos("face_brow_Lf_joint4", offset_l);
        DeformBonePos("face_brow_Lf_joint5", offset_l);

        DeformBonePos("face_brow_Rt_joint4", offset_r);
        DeformBonePos("face_brow_Rt_joint5", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_brow_Lf_joint4", Sacle);
        DeformBoneScale("face_brow_Lf_joint5", Sacle);
        DeformBoneScale("face_brow_Rt_joint4", Sacle);
        DeformBoneScale("face_brow_Rt_joint5", Sacle);

       // Debug.Log(xx);
    }


    // 眼睛。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。


    public void SetEyeZero(Vector4 xx) {


        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset_l = new Vector3(-xx.x, xx.z, xx.y);
        Vector3 offset_r = new Vector3(xx.x, xx.z, xx.y);

        DeformBonePos("face_eye_Lf_joint0", offset_l);
        DeformBonePos("face_eye_Rt_joint0", offset_r);       

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_eye_Lf_joint0", Sacle);
        DeformBoneScale("face_eye_Rt_joint0", Sacle);
    

    }

    public void SetEyecornerSwitch(Vector4  xx)   {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset_l = new Vector3(-xx.x, xx.z, xx.y);
        Vector3 offset_r = new Vector3(xx.x, xx.z, xx.y);

        DeformBonePos("face_eyeLids_Lf_joint1", offset_l); 
        DeformBonePos("face_eyeLids_Rt_joint1", offset_r);
      
        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_eyeLids_Lf_joint1", Sacle);
        DeformBoneScale("face_eyeLids_Rt_joint1", Sacle);
        

    }
    public void SetUppereyelidSwitch(Vector4  xx)   //
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset_l = new Vector3(-xx.x, xx.z, xx.y);
        Vector3 offset_r = new Vector3(xx.x, xx.z, xx.y);

        DeformBonePos("face_eyeLidsUp_Lf_joint1", offset_l);
        DeformBonePos("face_eyeLidsUp_Lf_joint2", offset_l);

        DeformBonePos("face_eyeLidsUp_Rt_joint1", offset_r);
        DeformBonePos("face_eyeLidsUp_Rt_joint2", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_eyeLidsUp_Lf_joint1", Sacle);
        DeformBoneScale("face_eyeLidsUp_Lf_joint2", Sacle);
        DeformBoneScale("face_eyeLidsUp_Rt_joint1", Sacle);
        DeformBoneScale("face_eyeLidsUp_Rt_joint2", Sacle);


    }
    public void SetDoublefoldEyelidsSwitch(Vector4  xx)   //
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        //Vector3 offset = new Vector3(0, xx.y, xx.z);
        //Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        //Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        //DeformBonePos("face_brow_Lf_joint1", offset_l);
        //DeformBonePos("face_brow_Lf_joint2", offset_l);

        //DeformBonePos("face_brow_Rt_joint1", offset_r);
        //DeformBonePos("face_brow_Rt_joint2", offset_r);

        //Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint1", Sacle);
        //DeformBoneScale("face_brow_Lf_joint2", Sacle);
        //DeformBoneScale("face_brow_Rt_joint1", Sacle);
        //DeformBoneScale("face_brow_Rt_joint2", Sacle);

    }
    public void SetlowereyelidSwitch(Vector4  xx)   //
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset_l = new Vector3(-xx.x, xx.z, xx.y);
        Vector3 offset_r = new Vector3(xx.x, xx.z, xx.y);

        DeformBonePos("face_eyeLidsdown_Lf_joint1", offset_l);
        DeformBonePos("face_eyeLidsdown_Lf_joint2", offset_l);

        DeformBonePos("face_eyeLidsdown_Rt_joint1", offset_r);
        DeformBonePos("face_eyeLidsdown_Rt_joint2", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_brow_Lf_joint1", Sacle);
        DeformBoneScale("face_brow_Lf_joint2", Sacle);
        DeformBoneScale("face_brow_Rt_joint1", Sacle);
        DeformBoneScale("face_brow_Rt_joint2", Sacle);

    }
    public void SetEyebagSwitch(Vector4 xx)   //
    {

        //xx = xx / ratio;
        ////Debug.Log(xx);

        //Vector3 offset = new Vector3(0, xx.y, xx.z);
        //Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        //Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        //DeformBonePos("face_brow_Lf_joint1", offset_l);
        //DeformBonePos("face_brow_Lf_joint2", offset_l);

        //DeformBonePos("face_brow_Rt_joint1", offset_r);
        //DeformBonePos("face_brow_Rt_joint2", offset_r);

        //Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint1", Sacle);
        //DeformBoneScale("face_brow_Lf_joint2", Sacle);
        //DeformBoneScale("face_brow_Rt_joint1", Sacle);
        //DeformBoneScale("face_brow_Rt_joint2", Sacle);

    }
    public void SetEyetailSwitch(Vector4  xx)   //
    {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset_l = new Vector3(-xx.x, xx.z, xx.y);
        Vector3 offset_r = new Vector3(xx.x , xx.z, xx.y);

        DeformBonePos("face_eyeLids_Lf_joint2", offset_l);     
        DeformBonePos("face_eyeLids_Rt_joint2", offset_r);
        

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_eyeLids_Lf_joint2", Sacle);
        DeformBoneScale("face_eyeLids_Rt_joint2", Sacle);
       


    }
    public void SetBlackeyeSwitch(Vector4  xx)   //
    {
        //xx = xx / ratio;
        ////Debug.Log(xx);

        //Vector3 offset = new Vector3(0, xx.y, xx.z);
        //Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        //Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        //DeformBonePos("face_brow_Lf_joint1", offset_l);
        //DeformBonePos("face_brow_Lf_joint2", offset_l);

        //DeformBonePos("face_brow_Rt_joint1", offset_r);
        //DeformBonePos("face_brow_Rt_joint2", offset_r);

        //Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint1", Sacle);
        //DeformBoneScale("face_brow_Lf_joint2", Sacle);
        //DeformBoneScale("face_brow_Rt_joint1", Sacle);
        //DeformBoneScale("face_brow_Rt_joint2", Sacle);

    }



    //鼻子...................................................

    public void SetNoseZeroSwitch(Vector4 xx)   //鼻全
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(xx.x, 0, xx.z);       
        DeformBonePos("face_nose_joint0", offset);
                
        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("face_nose_joint0", Sacle);
    }

    public void SetUpperbridgeSwitch(Vector4  xx)   //上鼻梁
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(xx.x, 0, xx.z);
        Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);
        DeformBonePos("face_bridge_joint1", offset);

        DeformBonePos("face_bridge_Lf_joint1", offset_l);
        DeformBonePos("face_bridge_Rt_joint1", offset_r);


        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("face_bridge_joint1", Sacle);
       
    }
    public void SetInferiorbridgeSwitch(Vector4  xx)   //下鼻梁
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(xx.x, xx.y, xx.z);    
        DeformBonePos("face_bridge_joint2", offset);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("face_bridge_joint2", Sacle);
      

    }
    public void SetNoseheadSwitch(Vector4  xx)   //鼻头
    {

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_nose_joint1", Sacle);

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(xx.x, xx.y, xx.z);      

        DeformBonePos("face_nose_joint1", offset);
      

    }
    public void SetColumellaNasiSwitch(Vector4  xx)   //鼻小柱
    {
        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);       
        DeformBoneScale("face_nose_joint2", Sacle);

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(xx.x , xx.y, xx.z);       
        DeformBonePos("face_nose_joint2", offset);
            
       
     
    }
    public void SetNasalBaseSwitch(Vector4  xx)   //鼻基底
    {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_nosewing_Lf_joint1", offset_l); 
        DeformBonePos("face_nosewing_Rt_joint1", offset_r);        

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_nosewing_Lf_joint1", Sacle);
        DeformBoneScale("face_nosewing_Rt_joint1", Sacle);
       

    }

    public void SetNoseWingSwitch(Vector4 xx)   //鼻翼
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_nosewing_Lf_joint003", offset_l); 
        DeformBonePos("face_nosewing_Rt_joint003", offset_r);
     
        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_nosewing_Lf_joint003", Sacle);
        DeformBoneScale("face_nosewing_Rt_joint003", Sacle);
   

    }
    public void SetNostrilSwitch(Vector4 xx)   //鼻孔
    {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_nosewing_Lf_joint2", offset_l);     
        DeformBonePos("face_nosewing_Rt_joint2", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_nosewing_Lf_joint2", Sacle);
        DeformBoneScale("face_nosewing_Rt_joint2", Sacle);       

    }



    // 嘴巴//。。。。。。。。。。。。。。。。。。。。。。。。。。

    public void SetMouthZeroSwitch(Vector4 xx)   //嘴全
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        DeformBonePos("face_mouthLip_joint0", offset); 
        
        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("face_mouthLip_joint0", Sacle);

    }

    public void SetUplipSwitch(Vector4  xx)   //上唇
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);


        DeformBonePos("face_mouthLip_up_joint1", offset);

        DeformBonePos("face_mouthLip_Lf_joint4", offset_l);
        DeformBonePos("face_mouthLip_Lf_joint2", offset_l);


        DeformBonePos("face_mouthLip_Rt_joint4", offset_r);
        DeformBonePos("face_mouthLip_Rt_joint2", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint1", Sacle);
        
    }
    public void SetUpjawSwitch(Vector4  xx)   //上颚
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_mouthLip_up_joint0", offset_l);       

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_mouthLip_up_joint0", Sacle);
      
    }
    public void SetDownLipSwitch(Vector4  xx)   //下唇
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);


        DeformBonePos("face_mouthLip_dn_joint1", offset);

        DeformBonePos("face_mouthLip_Lf_joint6", offset_l);
        DeformBonePos("face_mouthLip_Lf_joint3", offset_l);


        DeformBonePos("face_mouthLip_Rt_joint6", offset_r);
        DeformBonePos("face_mouthLip_Rt_joint3", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint1", Sacle);
    }
    public void SetDownJawSwitch(Vector4  xx)   //下颚
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_mouthLip_dn_joint0", offset_l);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_mouthLip_dn_joint0", Sacle);
    }
    public void SetPhiltrumSwitch(Vector4  xx)   //人中
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_Philtrum_joint0", offset);
        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_Philtrum_joint0", Sacle);
       

    }
    public void SetCornerSwitch(Vector4  xx)   //嘴角
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("face_mouthLip_Lf_joint1", offset_l);
        DeformBonePos("face_mouthLip_Rt_joint1", offset_r);
       

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("face_mouthLip_Lf_joint1", Sacle);
        DeformBoneScale("face_mouthLip_Rt_joint1", Sacle);
       
    }



    // chest.......................................................
    public void SetupperItemSwitch(Vector4 xx)   //上部
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(-xx.x, xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("Breast_LF_upper", offset_l);  
        DeformBonePos("Breast_RT_upper", offset_r);    

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        DeformBoneScale("Breast_LF_upper", Sacle);
        DeformBoneScale("Breast_RT_upper", Sacle);
       
    }
    public void SettopItemSwitch(Vector4 xx)   //乳尖
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        //Vector3 offset = new Vector3(0, xx.y, xx.z);
        //Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        //Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        //DeformBonePos("face_eyeLidsdown_Lf_joint1", offset_l);
        //DeformBonePos("face_eyeLidsdown_Lf_joint2", offset_l);

        //DeformBonePos("face_eyeLidsdown_Rt_joint1", offset_r);
        //DeformBonePos("face_eyeLidsdown_Rt_joint2", offset_r);

        //Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);

        //DeformBoneScale("face_brow_Lf_joint1", Sacle);
        //DeformBoneScale("face_brow_Lf_joint2", Sacle);
        //DeformBoneScale("face_brow_Rt_joint1", Sacle);
        //DeformBoneScale("face_brow_Rt_joint2", Sacle);
    }
    public void SetdownItemSwitch(Vector4 xx)   //下部
    {
        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("Breast_LF_lower", offset_l);
        DeformBonePos("Breast_RT_lower", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("Breast_LF_lower", Sacle);
        DeformBoneScale("Breast_RT_lower", Sacle);
     
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

        xx = xx / ratio;       

        Vector3 offset = new Vector3(xx.x, 0, 0);      

        DeformBonePos("Neck", offset);        
      
        Vector3 Sacle = new Vector3(xx.y, xx.z, xx.w);

        DeformBoneScale("Neck", Sacle);
       
  

    }//胸围//
    public void SetChestSwitch(Vector4 xx)
    {

        xx = xx / ratio;
        //Debug.Log(xx);

        Vector3 offset = new Vector3(0, xx.y, xx.z);
        Vector3 offset_l = new Vector3(xx.x, -xx.y, xx.z);
        Vector3 offset_r = new Vector3(xx.x, xx.y, xx.z);

        DeformBonePos("Breast_LF_scaleM", offset_l);
        DeformBonePos("Breast_RT_scaleM", offset_r);

        Vector3 Sacle = new Vector3(xx.w, xx.w, xx.w);
        DeformBoneScale("Breast_LF_scaleM", Sacle);
        DeformBoneScale("Breast_RT_scaleM", Sacle);


    }//胸围//    
    public void SetWristSwitch(Vector4 xx)  //腰围//
    {

        xx = xx / ratio;

        Vector3 offset = new Vector3(xx.x, 0, 0);

        DeformBonePos("chest_lower_ScaleM", offset);
        DeformBonePos("Waist_upper_ScaleM", offset);
        DeformBonePos("Spine1_M_ScaleM", offset);

        Vector3 Sacle = new Vector3(xx.y, xx.z, xx.w);

        DeformBoneScale("chest_lower_ScaleM", Sacle);
        DeformBoneScale("Waist_upper_ScaleM", Sacle);
        DeformBoneScale("Spine1_M_ScaleM", Sacle);

    }
    public void SetHipSwitch(Vector4 xx)   //臀围//
    {
        xx = xx / ratio;

        Vector3 offset = new Vector3(xx.x, 0, 0);

      
        DeformBonePos("Spine1_M_ScaleM", offset);

        Vector3 Sacle = new Vector3(xx.y, xx.z, xx.w);

     
        DeformBoneScale("Spine1_M_ScaleM", Sacle);

    }
    public void SetLegSwitch(Vector4 xx)   //腿围//
    {
        xx = xx / ratio;

        Vector3 offset = new Vector3(xx.x, 0, 0);


        DeformBonePos("Hip_L_scaleM", offset);
        DeformBonePos("Hip_R_scaleM", offset);
        DeformBonePos("Knee_L_ScaleM", offset);
        DeformBonePos("Knee_R_ScaleM", offset);


        Vector3 Sacle = new Vector3(xx.y, xx.z, xx.w);


        DeformBoneScale("Hip_L_scaleM", Sacle);
        DeformBoneScale("Hip_R_scaleM", Sacle);
        DeformBoneScale("Knee_L_ScaleM", Sacle);
        DeformBoneScale("Knee_R_ScaleM", Sacle);

    }
    public void SetArmSwitch(Vector4 xx)   //臂围//
    {

        xx = xx / ratio;

        Vector3 offset = new Vector3(xx.x, 0, 0);


        DeformBonePos("Shoulder_L_scaleM", offset);
        DeformBonePos("Shoulder_R_scaleM", offset);
        DeformBonePos("ForeArm_L_scaleM", offset);
        DeformBonePos("ForeArm_R_scaleM", offset);


        Vector3 Sacle = new Vector3(xx.y, xx.z, xx.w);


        DeformBoneScale("Shoulder_L_scaleM", Sacle);
        DeformBoneScale("Shoulder_R_scaleM", Sacle);
        DeformBoneScale("ForeArm_L_scaleM", Sacle);
        DeformBoneScale("ForeArm_R_scaleM", Sacle);


    }




    public void DeformBonePos(string bonename, Vector3 offset) {


     

        AppRoot.MainRole._bones[bonename].transform.localPosition = AppRoot.MainRole.bonesPostion[bonename] + offset;               

    }





    public void DeformBoneScale(string bonename, Vector3 scale) {
        AppRoot.MainRole._bones[bonename].transform.localScale = new Vector3(1f,1f,1f)+ scale ;

    }











    










    /// <summary>
    ///  //重置面部......................................................................
    /// </summary>


    public void ResetBones()
    {
       // RoleDef selfDef = TableMgr.Instance.RoleDic[RoleID];     
       // FacebonesDef facebones = selfDef.facebones;
        
       
        
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
