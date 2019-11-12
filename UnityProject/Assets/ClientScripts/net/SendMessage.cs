using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;

public class SendMessage : MonoBehaviour
{
    AndroidNative mAndroidNativeUtils;

    public static string Rolejson="{\"id\": 10001,\"name\":defort,\"shortCutImage\":url,\"Facemodel\":url,\"Facetexture\":url,\"assetbundle\": \"Role/Role01.data\",\"face_zero_pointy\": 115,\"Head\":{ \"x\":-0.03106061,\"y\":164.2536,\"z\":0.9138355},\"head_M_scale\":{ \"x\":-0.06988601,\"y\":167.786,\"z\":0.8602932},\"face_bridge_joint1\":{ \"x\":3.09301E-06,\"y\":175.9911,\"z\":11.7293},\"face_bridge_Lf_joint1\":{ \"x\":-0.9499236,\"y\":175.8264,\"z\":10.97716},\"face_bridge_Rt_joint1\":{ \"x\":0.950005,\"y\":175.8264,\"z\":10.97717},\"face_brow_Lf_joint0\":{ \"x\":-3.129241,\"y\":178.2007,\"z\":9.09691},\"face_brow_Lf_joint1\":{ \"x\":-1.092001,\"y\":177.455,\"z\":11.64652},\"face_brow_Lf_joint2\":{ \"x\":-1.879,\"y\":177.9589,\"z\":11.66653},\"face_brow_Lf_joint3\":{ \"x\":-3.476989,\"y\":178.429,\"z\":11.36254},\"face_brow_Lf_joint4\":{ \"x\":-5.065667,\"y\":178.3396,\"z\":10.37992},\"face_brow_Lf_joint5\":{ \"x\":-5.721003,\"y\":177.684,\"z\":9.39625},\"face_brow_Rt_joint0\":{ \"x\":3.129001,\"y\":178.2007,\"z\":9.096904},\"face_brow_Rt_joint1\":{ \"x\":1.091561,\"y\":177.4545,\"z\":11.64659},\"face_brow_Rt_joint2\":{ \"x\":1.878593,\"y\":177.9589,\"z\":11.66642},\"face_brow_Rt_joint3\":{ \"x\":3.477207,\"y\":178.4293,\"z\":11.36224},\"face_brow_Rt_joint4\":{ \"x\":5.066276,\"y\":178.3392,\"z\":10.37989},\"face_brow_Rt_joint5\":{ \"x\":5.721076,\"y\":177.6837,\"z\":9.396686},\"face_calvaria_joint1\":{ \"x\":-9.291398E-07,\"y\":187.9351,\"z\":1.796443},\"face_check_Lf_joint1\":{ \"x\":-3.921001,\"y\":171.8438,\"z\":10.59616},\"face_check_Lf_joint2\":{ \"x\":-6.271156,\"y\":171.6296,\"z\":7.908502},\"face_check_Lf_joint3\":{ \"x\":-5.754002,\"y\":173.3842,\"z\":9.413223},\"face_check_Lf_joint4\":{ \"x\":-6.746998,\"y\":175.8751,\"z\":7.500438},\"face_check_Rt_joint1\":{ \"x\":3.921101,\"y\":171.8438,\"z\":10.59619},\"face_check_Rt_joint2\":{ \"x\":6.270997,\"y\":171.6289,\"z\":7.908637},\"face_check_Rt_joint3\":{ \"x\":5.753587,\"y\":173.3842,\"z\":9.413235},\"face_check_Rt_joint4\":{ \"x\":6.746596,\"y\":175.8751,\"z\":7.50043},\"face_chin_Lf_joint06\":{ \"x\":-6.75265,\"y\":171.1242,\"z\":2.495654},\"face_chin_Lf_joint07\":{ \"x\":-6.969841,\"y\":172.7989,\"z\":2.525677},\"face_chin_Lf_joint08\":{ \"x\":-7.417036,\"y\":174.6945,\"z\":3.438395},\"face_chin_Lf_joint09\":{ \"x\":-7.004397,\"y\":178.2036,\"z\":5.188602},\"face_chin_Rt_joint06\":{ \"x\":6.752999,\"y\":171.1242,\"z\":2.495669},\"face_chin_Rt_joint07\":{ \"x\":7.03447,\"y\":172.799,\"z\":1.745646},\"face_chin_Rt_joint08\":{ \"x\":7.417002,\"y\":174.6945,\"z\":3.438406},\"face_chin_Rt_joint09\":{ \"x\":7.004003,\"y\":178.2036,\"z\":5.188605},\"face_ear_Lf_joint1\":{ \"x\":-8.803005,\"y\":176.896,\"z\":-0.360424},\"face_ear_Lf_joint2\":{ \"x\":-8.015001,\"y\":172.6102,\"z\":0.8474249},\"face_ear_Rt_joint1\":{ \"x\":8.802862,\"y\":176.896,\"z\":-0.3604299},\"face_ear_Rt_joint2\":{ \"x\":8.014885,\"y\":172.6103,\"z\":0.847407},\"face_eyeLidsdown_Rt_joint0\":{ \"x\":3.080077,\"y\":176.1436,\"z\":8.674256},\"face_eyeLids_Rt_joint1\":{ \"x\":1.753003,\"y\":175.699,\"z\":10.58288},\"face_eyeLids_Rt_joint2\":{ \"x\":4.491002,\"y\":176.084,\"z\":10.19255},\"face_eyeLidsdown_Rt_joint1\":{ \"x\":2.309999,\"y\":175.707,\"z\":10.65555},\"face_eyeLidsdown_Rt_joint2\":{ \"x\":3.135999,\"y\":175.611,\"z\":10.74658},\"face_eyeLidsdown_Rt_joint3\":{ \"x\":3.979995,\"y\":175.7501,\"z\":10.51556},\"face_eyeLidsUp_Rt_joint1\":{ \"x\":2.148997,\"y\":176.179,\"z\":10.68257},\"face_eyeLidsUp_Rt_joint2\":{ \"x\":2.971997,\"y\":176.571,\"z\":10.89756},\"face_eyeLidsUp_Rt_joint3\":{ \"x\":3.927994,\"y\":176.465,\"z\":10.72358},\"face_eyeLidsUp_Lf_joint0\":{ \"x\":-3.079998,\"y\":176.1434,\"z\":8.675488},\"face_eyeLids_Lf_joint1\":{ \"x\":-1.752706,\"y\":175.6986,\"z\":10.58289},\"face_eyeLids_Lf_joint2\":{ \"x\":-4.490565,\"y\":176.084,\"z\":10.19265},\"face_eyeLidsdown_Lf_joint1\":{ \"x\":-2.310321,\"y\":175.7067,\"z\":10.65521},\"face_eyeLidsdown_Lf_joint2\":{ \"x\":-3.136392,\"y\":175.6109,\"z\":10.74658},\"face_eyeLidsdown_Lf_joint3\":{ \"x\":-3.980224,\"y\":175.7503,\"z\":10.51509},\"face_eyeLidsUp_Lf_joint1\":{ \"x\":-2.149466,\"y\":176.1795,\"z\":10.68241},\"face_eyeLidsUp_Lf_joint2\":{ \"x\":-2.972111,\"y\":176.571,\"z\":10.89804},\"face_eyeLidsUp_Lf_joint3\":{ \"x\":-3.927571,\"y\":176.4653,\"z\":10.72307},\"face_forehead_joint1\":{ \"x\":3.043533E-06,\"y\":184.1034,\"z\":10.07327},\"face_forehead_joint2\":{ \"x\":3.087189E-06,\"y\":180.4195,\"z\":11.55791},\"face_forehead_Lf_joint1\":{ \"x\":-5.759186,\"y\":181.2156,\"z\":8.394597},\"face_forehead_Lf_joint2\":{ \"x\":-2.952998,\"y\":183.9661,\"z\":9.557124},\"face_forehead_Lf_joint3\":{ \"x\":2.952857,\"y\":183.9661,\"z\":9.55713},\"face_forehead_Lf_joint4\":{ \"x\":-4.918997,\"y\":179.533,\"z\":9.886546},\"face_forehead_Lf_joint5\":{ \"x\":-2.785998,\"y\":180.62,\"z\":10.8712},\"face_forehead_Rt_joint1\":{ \"x\":5.759003,\"y\":181.2156,\"z\":8.394605},\"face_forehead_Rt_joint2\":{ \"x\":4.918921,\"y\":179.5331,\"z\":9.886733},\"face_forehead_Rt_joint5\":{ \"x\":2.78551,\"y\":180.6199,\"z\":10.87118},\"face_L_check_Bone002\":{ \"x\":-3.355939,\"y\":174.2366,\"z\":10.73411},\"face_mouthLip_joint0\":{ \"x\":1.463195E-06,\"y\":169.1591,\"z\":7.062027},\"face_mouthLip_up_joint0\":{ \"x\":8.279312E-06,\"y\":169.119,\"z\":10.7814},\"face_mouthLip_Lf_joint1\":{ \"x\":-2.495995,\"y\":169.1246,\"z\":10.91416},\"face_mouthLip_Lf_joint2\":{ \"x\":-1.52699,\"y\":169.5303,\"z\":11.91455},\"face_mouthLip_Lf_joint4\":{ \"x\":-0.5009084,\"y\":169.6685,\"z\":12.40108},\"face_mouthLip_Lf_joint5\":{ \"x\":-0.8749924,\"y\":169.174,\"z\":11.6728},\"face_mouthLip_Rt_joint1\":{ \"x\":2.495562,\"y\":169.125,\"z\":10.91453},\"face_mouthLip_Rt_joint2\":{ \"x\":1.52724,\"y\":169.5297,\"z\":11.9141},\"face_mouthLip_Rt_joint4\":{ \"x\":0.5010062,\"y\":169.6685,\"z\":12.40108},\"face_mouthLip_Rt_joint5\":{ \"x\":0.8752034,\"y\":169.1745,\"z\":11.6728},\"face_mouthLip_up_joint1\":{ \"x\":6.090704E-06,\"y\":169.5937,\"z\":12.35245},\"face_mouthLip_up_joint2\":{ \"x\":8.311326E-06,\"y\":169.119,\"z\":11.83084},\"face_nose_bone\":{ \"x\":2.321758E-06,\"y\":172.5203,\"z\":10.86334},\"face_bridge_joint2\":{ \"x\":2.174711E-05,\"y\":174.2796,\"z\":12.65459},\"face_nose_joint0\":{ \"x\":-1.629742E-05,\"y\":172.7507,\"z\":11.13091},\"face_nose_joint1\":{ \"x\":1.724402E-07,\"y\":172.3463,\"z\":13.73164},\"face_nose_joint2\":{ \"x\":-1.179214E-05,\"y\":171.0087,\"z\":12.26376},\"face_nosewing_Lf_joint1\":{ \"x\":-1.808002,\"y\":171.7816,\"z\":11.30512},\"face_nosewing_Lf_joint2\":{ \"x\":-0.6660699,\"y\":171.3235,\"z\":11.99141},\"face_nosewing_Lf_joint003\":{ \"x\":-1.672692,\"y\":172.762,\"z\":11.37133},\"face_nosewing_Rt_joint1\":{ \"x\":1.807781,\"y\":171.7817,\"z\":11.30511},\"face_nosewing_Rt_joint2\":{ \"x\":0.6659926,\"y\":171.324,\"z\":11.99155},\"face_nosewing_Rt_joint003\":{ \"x\":1.672996,\"y\":172.762,\"z\":11.37132},\"face_Orbicular_Lf_joint3\":{ \"x\":-2.789996,\"y\":170.8133,\"z\":11.17449},\"face_Orbicular_Rt_joint3\":{ \"x\":2.790135,\"y\":170.8132,\"z\":11.1745},\"face_R_check_Bone002\":{ \"x\":3.355999,\"y\":174.2366,\"z\":10.73411},\"face_temple_Lf_joint1\":{ \"x\":-7.609529,\"y\":177.9579,\"z\":2.07414},\"face_temple_Lf_joint2\":{ \"x\":-5.870241,\"y\":177.8148,\"z\":-5.077478},\"face_temple_Lf_joint3\":{ \"x\":-7.355746,\"y\":180.4319,\"z\":1.051772},\"face_temple_Lf_joint004\":{ \"x\":-6.334001,\"y\":184.1743,\"z\":1.36458},\"face_temple_Rt_joint1\":{ \"x\":7.61,\"y\":177.9579,\"z\":2.07414},\"face_temple_Rt_joint2\":{ \"x\":5.869997,\"y\":177.8148,\"z\":-5.077506},\"face_temple_Rt_joint3\":{ \"x\":7.356001,\"y\":180.4319,\"z\":1.051763},\"face_temple_Rt_joint004\":{ \"x\":6.334459,\"y\":184.1743,\"z\":1.364595},\"face_tooth_down_joint1\":{ \"x\":3.033347E-06,\"y\":168.4518,\"z\":9.732491},\"face_tooth_down_joint2\":{ \"x\":-2.336767,\"y\":169.1046,\"z\":6.899241},\"face_tooth_down_joint3\":{ \"x\":2.337001,\"y\":169.1041,\"z\":6.899104},\"face_tooth_up_joint1\":{ \"x\":2.407002,\"y\":170.4814,\"z\":7.121576},\"face_tooth_up_joint2\":{ \"x\":-2.406627,\"y\":170.4818,\"z\":7.121725},\"face_tooth_up_joint3\":{ \"x\":3.05663E-06,\"y\":170.1621,\"z\":10.53025},\"Jaw\":{ \"x\":-1.08048E-06,\"y\":169.1576,\"z\":2.980562},\"face_chin_joint1\":{ \"x\":8.185452E-07,\"y\":165.1557,\"z\":10.41547},\"face_chin_joint2\":{ \"x\":-1.648363,\"y\":165.0351,\"z\":9.056139},\"face_chin_joint3\":{ \"x\":1.648001,\"y\":165.035,\"z\":9.056134},\"face_mouthLip_dn_joint0\":{ \"x\":6.044138E-06,\"y\":169.0983,\"z\":10.77973},\"face_mouthLip_dn_joint1\":{ \"x\":6.824121E-06,\"y\":168.3259,\"z\":11.96722},\"face_mouthLip_dn_joint2\":{ \"x\":7.561903E-06,\"y\":169.0989,\"z\":11.6944},\"face_mouthLip_Lf_joint3\":{ \"x\":-1.676994,\"y\":168.7363,\"z\":11.29481},\"face_mouthLip_Lf_joint6\":{ \"x\":-0.6699941,\"y\":168.3568,\"z\":11.87726},\"face_mouthLip_Lf_joint7\":{ \"x\":-1.022082,\"y\":169.098,\"z\":11.43438},\"face_mouthLip_Rt_joint3\":{ \"x\":1.676874,\"y\":168.7363,\"z\":11.29455},\"face_mouthLip_Rt_joint6\":{ \"x\":0.6697351,\"y\":168.3566,\"z\":11.85011},\"face_mouthLip_Rt_joint7\":{ \"x\":1.021998,\"y\":169.1375,\"z\":11.44253},\"face_Orbicular_joint1\":{ \"x\":-2.132583E-06,\"y\":166.516,\"z\":11.4135},\"face_Orbicular_Lf_joint1\":{ \"x\":-2.917228,\"y\":167.7526,\"z\":10.1532},\"face_Orbicular_Lf_joint2\":{ \"x\":-4.659649,\"y\":169.1383,\"z\":9.32726},\"face_Orbicular_Rt_joint1\":{ \"x\":2.916999,\"y\":167.4295,\"z\":10.08203},\"face_Orbicular_Rt_joint2\":{ \"x\":4.660022,\"y\":169.0593,\"z\":9.329073},\"face_TongueBack\":{ \"x\":3.296009E-07,\"y\":169.6701,\"z\":6.525902},\"face_TongueTip\":{ \"x\":1.533044E-06,\"y\":169.2017,\"z\":9.407843},\"Jaw_M\":{ \"x\":-1.08048E-06,\"y\":169.1576,\"z\":2.980562},\"face_chin_Lf_joint02\":{ \"x\":-3.626598,\"y\":166.6214,\"z\":8.284838},\"face_chin_Lf_joint03\":{ \"x\":-4.674429,\"y\":167.3797,\"z\":6.22756},\"face_chin_Lf_joint04\":{ \"x\":-5.563083,\"y\":168.3369,\"z\":5.225177},\"face_chin_Lf_joint05\":{ \"x\":-6.330474,\"y\":169.7565,\"z\":3.664876},\"face_chin_Rt_joint02\":{ \"x\":3.626997,\"y\":166.6214,\"z\":8.284838},\"face_chin_Rt_joint03\":{ \"x\":4.673987,\"y\":167.3797,\"z\":6.227555},\"face_chin_Rt_joint04\":{ \"x\":5.562984,\"y\":168.3369,\"z\":5.225165},\"face_chin_Rt_joint05\":{ \"x\":6.329999,\"y\":169.7565,\"z\":3.664871},\"L_eye_Bone\":{ \"x\":-3.079998,\"y\":176.1434,\"z\":8.675488},\"R_eye_Bone\":{ \"x\":3.080077,\"y\":176.1436,\"z\":8.674256},\"neck_upper_scale\":{ \"x\":-0.03106061,\"y\":164.2536,\"z\":0.9138206}}";

public static string Deformjson="{\"AvatarId\": 101,\"Shape\":{\"ForeheadSwitch\"{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"TempleSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BISjawSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ChinSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Face\":{\"ApplemuscleSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"CheekbonesSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"FacialpartSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Eyebrow\":{\"BrowbowSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BrowHeadSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BrowMiddleSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BrowTailSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Eye\":{\"EyecornerSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"UppereyelidSwitch:{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"DoublefoldEyelidsSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"lowereyelidSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"EyebagSwitch:{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"EyetailSwitch:{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BlackeyeSwitch:{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Nose\":{\"UpperbridgeSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"InferiorbridgeSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NoseheadSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ColumellaNasiSwitch\" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NasalBaseSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NoseWingSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NostrilSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Mouth\":{\"UplipSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"UpjawSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"DownLipSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}, \"DownJawSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"PhiltrumSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"CornerSwitch\":{ \"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Chest\":{\"upperItemSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}, \"topItemSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"downItemSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Body\":{\"NeckSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ChestSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}, \"WristSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"HipSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"LegSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ArmSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ForeheadSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BISjawSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ChinSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0}}}";
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

               
    }

    //加载模型列表。。。。。。。。必须

    public void LoadModelList(string Modeljson) {




    }




    //进入编辑。。。。。。。。。

//加载初始模型信息

     public void LoadRolejson(string Rolejson)
    {


    }


    //加载最新一次编辑信息
    public void LoadDeformJson(string Deformjson ) {



    }

    //加载初始配饰信息

    //加载当前配饰信息   包含发型、服装、化妆
     public void LoadOrnamentjson(string Ornamentjson) {



      
    }




    //加载服装列表
    public void LoadClothList(string ClothListjson)
    {


    }



    //加载化妆信息
    public void LoadMakeupList(string MakeupListjson) {





    }
       

    //加载发型信息
    public void LoadHairListjson(string HairListjson)
    {



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






   
