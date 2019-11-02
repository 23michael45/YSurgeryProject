using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeformUI : MonoBehaviour
{
    private static string Rolejson = "{\"id\":10001,\"name\":defort,\"shortCutImage\":url,\"Facemodel\":url,\"Facetexture\":url,\"assetbundle\":\"Role/Role01.data\",\"face_zero_pointy\":115,\"Head\":{\"x\":-0.03106061,\"y\":164.2536,\"z\":0.9138355},\"head_M_scale\":{\"x\":-0.06988601,\"y\":167.786,\"z\":0.8602932},\"face_bridge_joint1\":{\"x\":3.09301E-06,\"y\":175.9911,\"z\":11.7293},\"face_bridge_Lf_joint1\":{\"x\":-0.9499236,\"y\":175.8264,\"z\":10.97716},\"face_bridge_Rt_joint1\":{\"x\":0.950005,\"y\":175.8264,\"z\":10.97717},\"face_brow_Lf_joint0\":{\"x\":-3.129241,\"y\":178.2007,\"z\":9.09691},\"face_brow_Lf_joint1\":{\"x\":-1.092001,\"y\":177.455,\"z\":11.64652},\"face_brow_Lf_joint2\":{\"x\":-1.879,\"y\":177.9589,\"z\":11.66653},\"face_brow_Lf_joint3\":{\"x\":-3.476989,\"y\":178.429,\"z\":11.36254},\"face_brow_Lf_joint4\":{\"x\":-5.065667,\"y\":178.3396,\"z\":10.37992},\"face_brow_Lf_joint5\":{\"x\":-5.721003,\"y\":177.684,\"z\":9.39625},\"face_brow_Rt_joint0\":{\"x\":3.129001,\"y\":178.2007,\"z\":9.096904},\"face_brow_Rt_joint1\":{\"x\":1.091561,\"y\":177.4545,\"z\":11.64659},\"face_brow_Rt_joint2\":{\"x\":1.878593,\"y\":177.9589,\"z\":11.66642},\"face_brow_Rt_joint3\":{\"x\":3.477207,\"y\":178.4293,\"z\":11.36224},\"face_brow_Rt_joint4\":{\"x\":5.066276,\"y\":178.3392,\"z\":10.37989},\"face_brow_Rt_joint5\":{\"x\":5.721076,\"y\":177.6837,\"z\":9.396686},\"face_calvaria_joint1\":{\"x\":-9.291398E-07,\"y\":187.9351,\"z\":1.796443},\"face_check_Lf_joint1\":{\"x\":-3.921001,\"y\":171.8438,\"z\":10.59616},\"face_check_Lf_joint2\":{\"x\":-6.271156,\"y\":171.6296,\"z\":7.908502},\"face_check_Lf_joint3\":{\"x\":-5.754002,\"y\":173.3842,\"z\":9.413223},\"face_check_Lf_joint4\":{\"x\":-6.746998,\"y\":175.8751,\"z\":7.500438},\"face_check_Rt_joint1\":{\"x\":3.921101,\"y\":171.8438,\"z\":10.59619},\"face_check_Rt_joint2\":{\"x\":6.270997,\"y\":171.6289,\"z\":7.908637},\"face_check_Rt_joint3\":{\"x\":5.753587,\"y\":173.3842,\"z\":9.413235},\"face_check_Rt_joint4\":{\"x\":6.746596,\"y\":175.8751,\"z\":7.50043},\"face_chin_Lf_joint06\":{\"x\":-6.75265,\"y\":171.1242,\"z\":2.495654},\"face_chin_Lf_joint07\":{\"x\":-6.969841,\"y\":172.7989,\"z\":2.525677},\"face_chin_Lf_joint08\":{\"x\":-7.417036,\"y\":174.6945,\"z\":3.438395},\"face_chin_Lf_joint09\":{\"x\":-7.004397,\"y\":178.2036,\"z\":5.188602},\"face_chin_Rt_joint06\":{\"x\":6.752999,\"y\":171.1242,\"z\":2.495669},\"face_chin_Rt_joint07\":{\"x\":7.03447,\"y\":172.799,\"z\":1.745646},\"face_chin_Rt_joint08\":{\"x\":7.417002,\"y\":174.6945,\"z\":3.438406},\"face_chin_Rt_joint09\":{\"x\":7.004003,\"y\":178.2036,\"z\":5.188605},\"face_ear_Lf_joint1\":{\"x\":-8.803005,\"y\":176.896,\"z\":-0.360424},\"face_ear_Lf_joint2\":{\"x\":-8.015001,\"y\":172.6102,\"z\":0.8474249},\"face_ear_Rt_joint1\":{\"x\":8.802862,\"y\":176.896,\"z\":-0.3604299},\"face_ear_Rt_joint2\":{\"x\":8.014885,\"y\":172.6103,\"z\":0.847407},\"face_eyeLidsdown_Rt_joint0\":{\"x\":3.080077,\"y\":176.1436,\"z\":8.674256},\"face_eyeLids_Rt_joint1\":{\"x\":1.753003,\"y\":175.699,\"z\":10.58288},\"face_eyeLids_Rt_joint2\":{\"x\":4.491002,\"y\":176.084,\"z\":10.19255},\"face_eyeLidsdown_Rt_joint1\":{\"x\":2.309999,\"y\":175.707,\"z\":10.65555},\"face_eyeLidsdown_Rt_joint2\":{\"x\":3.135999,\"y\":175.611,\"z\":10.74658},\"face_eyeLidsdown_Rt_joint3\":{\"x\":3.979995,\"y\":175.7501,\"z\":10.51556},\"face_eyeLidsUp_Rt_joint1\":{\"x\":2.148997,\"y\":176.179,\"z\":10.68257},\"face_eyeLidsUp_Rt_joint2\":{\"x\":2.971997,\"y\":176.571,\"z\":10.89756},\"face_eyeLidsUp_Rt_joint3\":{\"x\":3.927994,\"y\":176.465,\"z\":10.72358},\"face_eyeLidsUp_Lf_joint0\":{\"x\":-3.079998,\"y\":176.1434,\"z\":8.675488},\"face_eyeLids_Lf_joint1\":{\"x\":-1.752706,\"y\":175.6986,\"z\":10.58289},\"face_eyeLids_Lf_joint2\":{\"x\":-4.490565,\"y\":176.084,\"z\":10.19265},\"face_eyeLidsdown_Lf_joint1\":{\"x\":-2.310321,\"y\":175.7067,\"z\":10.65521},\"face_eyeLidsdown_Lf_joint2\":{\"x\":-3.136392,\"y\":175.6109,\"z\":10.74658},\"face_eyeLidsdown_Lf_joint3\":{\"x\":-3.980224,\"y\":175.7503,\"z\":10.51509},\"face_eyeLidsUp_Lf_joint1\":{\"x\":-2.149466,\"y\":176.1795,\"z\":10.68241},\"face_eyeLidsUp_Lf_joint2\":{\"x\":-2.972111,\"y\":176.571,\"z\":10.89804},\"face_eyeLidsUp_Lf_joint3\":{\"x\":-3.927571,\"y\":176.4653,\"z\":10.72307},\"face_forehead_joint1\":{\"x\":3.043533E-06,\"y\":184.1034,\"z\":10.07327},\"face_forehead_joint2\":{\"x\":3.087189E-06,\"y\":180.4195,\"z\":11.55791},\"face_forehead_Lf_joint1\":{\"x\":-5.759186,\"y\":181.2156,\"z\":8.394597},\"face_forehead_Lf_joint2\":{\"x\":-2.952998,\"y\":183.9661,\"z\":9.557124},\"face_forehead_Lf_joint3\":{\"x\":2.952857,\"y\":183.9661,\"z\":9.55713},\"face_forehead_Lf_joint4\":{\"x\":-4.918997,\"y\":179.533,\"z\":9.886546},\"face_forehead_Lf_joint5\":{\"x\":-2.785998,\"y\":180.62,\"z\":10.8712},\"face_forehead_Rt_joint1\":{\"x\":5.759003,\"y\":181.2156,\"z\":8.394605},\"face_forehead_Rt_joint2\":{\"x\":4.918921,\"y\":179.5331,\"z\":9.886733},\"face_forehead_Rt_joint5\":{\"x\":2.78551,\"y\":180.6199,\"z\":10.87118},\"face_L_check_Bone002\":{\"x\":-3.355939,\"y\":174.2366,\"z\":10.73411},\"face_mouthLip_joint0\":{\"x\":1.463195E-06,\"y\":169.1591,\"z\":7.062027},\"face_mouthLip_up_joint0\":{\"x\":8.279312E-06,\"y\":169.119,\"z\":10.7814},\"face_mouthLip_Lf_joint1\":{\"x\":-2.495995,\"y\":169.1246,\"z\":10.91416},\"face_mouthLip_Lf_joint2\":{\"x\":-1.52699,\"y\":169.5303,\"z\":11.91455},\"face_mouthLip_Lf_joint4\":{\"x\":-0.5009084,\"y\":169.6685,\"z\":12.40108},\"face_mouthLip_Lf_joint5\":{\"x\":-0.8749924,\"y\":169.174,\"z\":11.6728},\"face_mouthLip_Rt_joint1\":{\"x\":2.495562,\"y\":169.125,\"z\":10.91453},\"face_mouthLip_Rt_joint2\":{\"x\":1.52724,\"y\":169.5297,\"z\":11.9141},\"face_mouthLip_Rt_joint4\":{\"x\":0.5010062,\"y\":169.6685,\"z\":12.40108},\"face_mouthLip_Rt_joint5\":{\"x\":0.8752034,\"y\":169.1745,\"z\":11.6728},\"face_mouthLip_up_joint1\":{\"x\":6.090704E-06,\"y\":169.5937,\"z\":12.35245},\"face_mouthLip_up_joint2\":{\"x\":8.311326E-06,\"y\":169.119,\"z\":11.83084},\"face_nose_bone\":{\"x\":2.321758E-06,\"y\":172.5203,\"z\":10.86334},\"face_bridge_joint2\":{\"x\":2.174711E-05,\"y\":174.2796,\"z\":12.65459},\"face_nose_joint0\":{\"x\":-1.629742E-05,\"y\":172.7507,\"z\":11.13091},\"face_nose_joint1\":{\"x\":1.724402E-07,\"y\":172.3463,\"z\":13.73164},\"face_nose_joint2\":{\"x\":-1.179214E-05,\"y\":171.0087,\"z\":12.26376},\"face_nosewing_Lf_joint1\":{\"x\":-1.808002,\"y\":171.7816,\"z\":11.30512},\"face_nosewing_Lf_joint2\":{\"x\":-0.6660699,\"y\":171.3235,\"z\":11.99141},\"face_nosewing_Lf_joint003\":{\"x\":-1.672692,\"y\":172.762,\"z\":11.37133},\"face_nosewing_Rt_joint1\":{\"x\":1.807781,\"y\":171.7817,\"z\":11.30511},\"face_nosewing_Rt_joint2\":{\"x\":0.6659926,\"y\":171.324,\"z\":11.99155},\"face_nosewing_Rt_joint003\":{\"x\":1.672996,\"y\":172.762,\"z\":11.37132},\"face_Orbicular_Lf_joint3\":{\"x\":-2.789996,\"y\":170.8133,\"z\":11.17449},\"face_Orbicular_Rt_joint3\":{\"x\":2.790135,\"y\":170.8132,\"z\":11.1745},\"face_R_check_Bone002\":{\"x\":3.355999,\"y\":174.2366,\"z\":10.73411},\"face_temple_Lf_joint1\":{\"x\":-7.609529,\"y\":177.9579,\"z\":2.07414},\"face_temple_Lf_joint2\":{\"x\":-5.870241,\"y\":177.8148,\"z\":-5.077478},\"face_temple_Lf_joint3\":{\"x\":-7.355746,\"y\":180.4319,\"z\":1.051772},\"face_temple_Lf_joint004\":{\"x\":-6.334001,\"y\":184.1743,\"z\":1.36458},\"face_temple_Rt_joint1\":{\"x\":7.61,\"y\":177.9579,\"z\":2.07414},\"face_temple_Rt_joint2\":{\"x\":5.869997,\"y\":177.8148,\"z\":-5.077506},\"face_temple_Rt_joint3\":{\"x\":7.356001,\"y\":180.4319,\"z\":1.051763},\"face_temple_Rt_joint004\":{\"x\":6.334459,\"y\":184.1743,\"z\":1.364595},\"face_tooth_down_joint1\":{\"x\":3.033347E-06,\"y\":168.4518,\"z\":9.732491},\"face_tooth_down_joint2\":{\"x\":-2.336767,\"y\":169.1046,\"z\":6.899241},\"face_tooth_down_joint3\":{\"x\":2.337001,\"y\":169.1041,\"z\":6.899104},\"face_tooth_up_joint1\":{\"x\":2.407002,\"y\":170.4814,\"z\":7.121576},\"face_tooth_up_joint2\":{\"x\":-2.406627,\"y\":170.4818,\"z\":7.121725},\"face_tooth_up_joint3\":{\"x\":3.05663E-06,\"y\":170.1621,\"z\":10.53025},\"Jaw\":{\"x\":-1.08048E-06,\"y\":169.1576,\"z\":2.980562},\"face_chin_joint1\":{\"x\":8.185452E-07,\"y\":165.1557,\"z\":10.41547},\"face_chin_joint2\":{\"x\":-1.648363,\"y\":165.0351,\"z\":9.056139},\"face_chin_joint3\":{\"x\":1.648001,\"y\":165.035,\"z\":9.056134},\"face_mouthLip_dn_joint0\":{\"x\":6.044138E-06,\"y\":169.0983,\"z\":10.77973},\"face_mouthLip_dn_joint1\":{\"x\":6.824121E-06,\"y\":168.3259,\"z\":11.96722},\"face_mouthLip_dn_joint2\":{\"x\":7.561903E-06,\"y\":169.0989,\"z\":11.6944},\"face_mouthLip_Lf_joint3\":{\"x\":-1.676994,\"y\":168.7363,\"z\":11.29481},\"face_mouthLip_Lf_joint6\":{\"x\":-0.6699941,\"y\":168.3568,\"z\":11.87726},\"face_mouthLip_Lf_joint7\":{\"x\":-1.022082,\"y\":169.098,\"z\":11.43438},\"face_mouthLip_Rt_joint3\":{\"x\":1.676874,\"y\":168.7363,\"z\":11.29455},\"face_mouthLip_Rt_joint6\":{\"x\":0.6697351,\"y\":168.3566,\"z\":11.85011},\"face_mouthLip_Rt_joint7\":{\"x\":1.021998,\"y\":169.1375,\"z\":11.44253},\"face_Orbicular_joint1\":{\"x\":-2.132583E-06,\"y\":166.516,\"z\":11.4135},\"face_Orbicular_Lf_joint1\":{\"x\":-2.917228,\"y\":167.7526,\"z\":10.1532},\"face_Orbicular_Lf_joint2\":{\"x\":-4.659649,\"y\":169.1383,\"z\":9.32726},\"face_Orbicular_Rt_joint1\":{\"x\":2.916999,\"y\":167.4295,\"z\":10.08203},\"face_Orbicular_Rt_joint2\":{\"x\":4.660022,\"y\":169.0593,\"z\":9.329073},\"face_TongueBack\":{\"x\":3.296009E-07,\"y\":169.6701,\"z\":6.525902},\"face_TongueTip\":{\"x\":1.533044E-06,\"y\":169.2017,\"z\":9.407843},\"Jaw_M\":{\"x\":-1.08048E-06,\"y\":169.1576,\"z\":2.980562},\"face_chin_Lf_joint02\":{\"x\":-3.626598,\"y\":166.6214,\"z\":8.284838},\"face_chin_Lf_joint03\":{\"x\":-4.674429,\"y\":167.3797,\"z\":6.22756},\"face_chin_Lf_joint04\":{\"x\":-5.563083,\"y\":168.3369,\"z\":5.225177},\"face_chin_Lf_joint05\":{\"x\":-6.330474,\"y\":169.7565,\"z\":3.664876},\"face_chin_Rt_joint02\":{\"x\":3.626997,\"y\":166.6214,\"z\":8.284838},\"face_chin_Rt_joint03\":{\"x\":4.673987,\"y\":167.3797,\"z\":6.227555},\"face_chin_Rt_joint04\":{\"x\":5.562984,\"y\":168.3369,\"z\":5.225165},\"face_chin_Rt_joint05\":{\"x\":6.329999,\"y\":169.7565,\"z\":3.664871},\"L_eye_Bone\":{\"x\":-3.079998,\"y\":176.1434,\"z\":8.675488},\"R_eye_Bone\":{\"x\":3.080077,\"y\":176.1436,\"z\":8.674256},\"neck_upper_scale\":{\"x\":-0.03106061,\"y\":164.2536,\"z\":0.9138206}}";


private static string Deformjson = "{\"AvatarId\": 101,\"Shape\":{\"ForeheadSwitch\":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"TempleSwitch\" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BISjawSwitch\" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ChinSwitch\" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Face\":{\"ApplemuscleSwitch \": {\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"CheekbonesSwitch \" : {\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"FacialpartSwitch \" : {\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Eyebrow\":{\"BrowbowSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BrowHeadSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BrowMiddleSwitch \":{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BrowTailSwitch\" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Eye\":{\"EyecornerSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"UppereyelidSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"DoublefoldEyelidsSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"lowereyelidSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"EyebagSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"EyetailSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BlackeyeSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Nose\":{\"UpperbridgeSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"InferiorbridgeSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NoseheadSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ColumellaNasiSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NasalBaseSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NoseWingSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"NostrilSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Mouth\":{\"UplipSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"UpjawSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"DownLipSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"DownJawSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"PhiltrumSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"CornerSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Chest\":{\"upperItemSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"topItemSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"downItemSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}},\"Body\":{\"NeckSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ChestSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"WristSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"HipSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"LegSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ArmSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ForeheadSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"BISjawSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0},\"ChinSwitch \" :{\"x\":0,\"y\":0,\"z\":0,\"w\":0}}}";


    // shape.........
    public Slider foreheadItem_x, foreheadItem_y, foreheadItem_w;
    public Slider TempleItem_x,TempleItem_y,TempleItem_w;
    public Slider BISjawItem_x, BISjawItem_y, BISjawItem_w;
    public Slider ChinItem_x, ChinItem_y, ChinItem_w;

    private Vector4 ForeheadSwitch_data,
                    TempleSwitch_data,
                    BISjawSwitch_data,
                    ChinSwitch_data;  

    //face..........
    public Slider ApplemuscleItem_x, ApplemuscleItem_y, ApplemuscleItem_w;
    public Slider CheekbonesItem_x, CheekbonesItem_y, CheekbonesItem_w;
    public Slider FacialpartItem_x, FacialpartItem_y, FacialpartItem_w;



    private Vector4 ApplemuscleSwitch_data,
                    CheekbonesSwitch_data,
                    FacialpartSwitch_data;
    

    //eyebrow....
    public Slider BrowbowItem_x, BrowbowItem_y, BrowbowItem_w;
    public Slider BrowHeadItem_x, BrowHeadItem_y, BrowHeadItem_w;
    public Slider BrowMiddleItem_x, BrowMiddleItem_y, BrowMiddleItem_w;
    public Slider BrowTailItem_x, BrowTailItem_y, BrowTailItem_w;

    private Vector4 BrowbowSwitch_data,
                    BrowHeadSwitch_data,
                    BrowMiddleSwitch_data,
                    BrowTailSwitch_data; 



    //eye.......
    public Slider EyecornerItem_x, EyecornerItem_y, EyecornerItem_w;
    public Slider UppereyelidItem_x, UppereyelidItem_y, UppereyelidItem_w;
    public Slider DoublefoldEyelidsItem_x, DoublefoldEyelidsItem_y, DoublefoldEyelidsItem_w;
    public Slider lowereyelidItem_x, lowereyelidItem_y, lowereyelidItem_w;
    public Slider  EyebagItem_y, EyebagItem_w;
    public Slider EyetailItem_x, EyetailItem_y, EyetailItem_w;
    public Slider BlackeyeItem_x, BlackeyeItem_y, BlackeyeItem_w;

    private Vector4 EyecornerSwitch_data,
                    UppereyelidSwitch_data,
                    DoublefoldEyelidsSwitch_data,
                    lowereyelidSwitch_data,
                    EyebagSwitch_data,
                    EyetailSwitch_data,
                    BlackeyeSwitch_data;




    //nose.........
    public Slider UpperbridgeItem_x, UpperbridgeItem_y, UpperbridgeItem_w;
    public Slider InferiorbridgeItem_x, InferiorbridgeItem_y, InferiorbridgeItem_w;
    public Slider NoseheadItem_x, NoseheadItem_y, NoseheadItem_z, NoseheadItem_w;
    public Slider ColumellaNasiItem_x, ColumellaNasiItem_y, ColumellaNasiItem_w;
    public Slider NasalBaseItem_x, NasalBaseItem_y, NasalBaseItem_w;
    public Slider NoseWingItem_x, NoseWingItem_y, NoseWingItem_w;
    public Slider NostrilItem_x, NostrilItem_y, NostrilItem_w;    


    private Vector4 UpperbridgeSwitch_data,
                    InferiorbridgeSwitch_data,
                    NoseheadSwitch_data,
                    ColumellaNasiSwitch_data,
                    NasalBaseSwitch_data,
                    NoseWingSwitch_data,
                    NostrilSwitch_data;




    //mouth.........
    public Slider upperlipItem_x, upperlipItem_y, upperlipItem_w;
    public Slider upperboneItem_x, upperboneItem_y, upperboneItem_w;
    public Slider downlipItem_x, downlipItem_y, downlipItem_w;
    public Slider downBoneItem_x, downBoneItem_y, downBoneItem_w;
    public Slider MiddleItem_x, MiddleItem_y, MiddleItem_w;
    public Slider cornerItem_x, cornerItem_y, cornerItem_w;

    private Vector4 UplipSwitch_data,
                     UpjawSwitch_data,
                     DownLipSwitch_data,
                     DownJawSwitch_data,
                     PhiltrumSwitch_data,
                     CornerSwitch_data;



    //cheset.......
    public Slider chestupperItem_x, chestupperItem_y, chestupperItem_w;
    public Slider chesttopItem_x, chesttopItem_y, chesttopItem_w;
    public Slider chestdownItem_x, chestdownItem_y, chestdownItem_w;

    private Vector4 upperItemSwitch_data,
                    topItemSwitch_data,
                    downItemSwitch_data;
    

    //body.............
    public Slider neckItem_x,  neckItem_w;
    public Slider chestItem_x, chestItem_w;
    public Slider wristItem_x, wristItem_w;
    public Slider hipItem_x, hipItem_w;
    public Slider LegItem_x, LegItem_w;
    public Slider ArmItem_x, ArmItem_w;

    private Vector4 NeckSwitch_data,
                    ChestSwitch_data,
                    WristSwitch_data,
                    HipSwitch_data,
                    LegSwitch_data,
                    ArmSwitch_data;



    public void Start()
    {
        foreheadItem_x.onValueChanged.AddListener(foreheadItem_x_chg);       
        foreheadItem_y.onValueChanged.AddListener(foreheadItem_y_chg);
        foreheadItem_w.onValueChanged.AddListener(foreheadItem_z_chg);

        TempleItem_x.onValueChanged.AddListener(TempleItem_x_chg);
        TempleItem_y.onValueChanged.AddListener(TempleItem_y_chg);
        TempleItem_w.onValueChanged.AddListener(TempleItem_w_chg);
                   
        BISjawItem_x.onValueChanged.AddListener(BISjawItem_x_chg);
        BISjawItem_y.onValueChanged.AddListener(BISjawItem_y_chg);
        BISjawItem_w.onValueChanged.AddListener(BISjawItem_w_chg);

        ChinItem_x.onValueChanged.AddListener(ChinItem_x_chg);
        ChinItem_y.onValueChanged.AddListener(ChinItem_y_chg);
        ChinItem_w.onValueChanged.AddListener(ChinItem_w_chg);

        //face..........
        ApplemuscleItem_x.onValueChanged.AddListener(ApplemuscleItem_x_chg);
        ApplemuscleItem_y.onValueChanged.AddListener(ApplemuscleItem_y_chg);
        ApplemuscleItem_w.onValueChanged.AddListener(ApplemuscleItem_w_chg);

        CheekbonesItem_x.onValueChanged.AddListener(CheekbonesItem_x_chg);
        CheekbonesItem_y.onValueChanged.AddListener(CheekbonesItem_y_chg);
        CheekbonesItem_w.onValueChanged.AddListener(CheekbonesItem_w_chg);

        FacialpartItem_x.onValueChanged.AddListener(FacialpartItem_x_chg);
        FacialpartItem_y.onValueChanged.AddListener(FacialpartItem_y_chg);
        FacialpartItem_w.onValueChanged.AddListener(FacialpartItem_w_chg);


        //eyebrow....
        BrowbowItem_x.onValueChanged.AddListener(BrowbowItem_x_chg);
        BrowbowItem_y.onValueChanged.AddListener(BrowbowItem_y_chg);
        BrowbowItem_w.onValueChanged.AddListener(BrowbowItem_w_chg);

        BrowHeadItem_x.onValueChanged.AddListener(BrowHeadItem_x_chg);
        BrowHeadItem_y.onValueChanged.AddListener(BrowHeadItem_y_chg);
        BrowHeadItem_w.onValueChanged.AddListener(BrowHeadItem_w_chg);


        BrowMiddleItem_x.onValueChanged.AddListener(BrowMiddleItem_x_chg);
        BrowMiddleItem_y.onValueChanged.AddListener(BrowMiddleItem_y_chg);
        BrowMiddleItem_w.onValueChanged.AddListener(BrowMiddleItem_w_chg);

        BrowTailItem_x.onValueChanged.AddListener(BrowTailItem_x_chg);
        BrowTailItem_y.onValueChanged.AddListener(BrowTailItem_y_chg);
        BrowTailItem_w.onValueChanged.AddListener(BrowTailItem_w_chg);

        //eye.......
        EyecornerItem_x.onValueChanged.AddListener(EyecornerItem_x_chg);
        EyecornerItem_y.onValueChanged.AddListener(EyecornerItem_y_chg);
        EyecornerItem_w.onValueChanged.AddListener(EyecornerItem_w_chg);

        UppereyelidItem_x.onValueChanged.AddListener(UppereyelidItem_x_chg);
        UppereyelidItem_y.onValueChanged.AddListener(UppereyelidItem_y_chg);
        UppereyelidItem_w.onValueChanged.AddListener(UppereyelidItem_w_chg);
       
        DoublefoldEyelidsItem_y.onValueChanged.AddListener(DoublefoldEyelidsItem_y_chg);
        DoublefoldEyelidsItem_w.onValueChanged.AddListener(DoublefoldEyelidsItem_w_chg);

        lowereyelidItem_x.onValueChanged.AddListener(lowereyelidItem_x_chg);
        lowereyelidItem_y.onValueChanged.AddListener(lowereyelidItem_y_chg);
        lowereyelidItem_w.onValueChanged.AddListener(lowereyelidItem_w_chg);

        EyebagItem_y.onValueChanged.AddListener(EyebagItem_y_chg);        
        EyebagItem_w.onValueChanged.AddListener(EyebagItem_w_chg);

        EyetailItem_x.onValueChanged.AddListener(EyetailItem_x_chg);
        EyetailItem_y.onValueChanged.AddListener(EyetailItem_y_chg);
        EyetailItem_w.onValueChanged.AddListener(EyetailItem_w_chg);
      
        BlackeyeItem_y.onValueChanged.AddListener(BlackeyeItem_y_chg);
        BlackeyeItem_w.onValueChanged.AddListener(BlackeyeItem_w_chg);

        //nose.........
        UpperbridgeItem_x.onValueChanged.AddListener(UpperbridgeItem_x_chg);
        UpperbridgeItem_y.onValueChanged.AddListener(UpperbridgeItem_y_chg);
        UpperbridgeItem_w.onValueChanged.AddListener(UpperbridgeItem_w_chg);

        InferiorbridgeItem_x.onValueChanged.AddListener(InferiorbridgeItem_x_chg);
        InferiorbridgeItem_y.onValueChanged.AddListener(InferiorbridgeItem_y_chg);
        InferiorbridgeItem_w.onValueChanged.AddListener(InferiorbridgeItem_w_chg);

        NoseheadItem_x.onValueChanged.AddListener(NoseheadItem_x_chg);
        NoseheadItem_y.onValueChanged.AddListener(NoseheadItem_y_chg);
        NoseheadItem_z.onValueChanged.AddListener(NoseheadItem_z_chg);
        NoseheadItem_w.onValueChanged.AddListener(NoseheadItem_w_chg);

        ColumellaNasiItem_x.onValueChanged.AddListener(ColumellaNasiItem_x_chg);
        ColumellaNasiItem_y.onValueChanged.AddListener(ColumellaNasiItem_y_chg);
        ColumellaNasiItem_w.onValueChanged.AddListener(ColumellaNasiItem_w_chg);

        NasalBaseItem_x.onValueChanged.AddListener(NasalBaseItem_x_chg);
        NasalBaseItem_y.onValueChanged.AddListener(NasalBaseItem_y_chg);
        NasalBaseItem_w.onValueChanged.AddListener(NasalBaseItem_w_chg);

        NoseWingItem_x.onValueChanged.AddListener(NoseWingItem_x_chg);
        NoseWingItem_y.onValueChanged.AddListener(NoseWingItem_y_chg);
        NoseWingItem_w.onValueChanged.AddListener(NoseWingItem_w_chg);

        NostrilItem_x.onValueChanged.AddListener(NostrilItem_x_chg);
        NostrilItem_y.onValueChanged.AddListener(NostrilItem_y_chg);
        NostrilItem_w.onValueChanged.AddListener(NostrilItem_w_chg);


        //mouth.........
        upperlipItem_x.onValueChanged.AddListener(upperlipItem_x_chg);
        upperlipItem_y.onValueChanged.AddListener(upperlipItem_y_chg);
        upperlipItem_w.onValueChanged.AddListener(upperlipItem_w_chg);

        upperboneItem_x.onValueChanged.AddListener(upperboneItem_x_chg);
        upperboneItem_y.onValueChanged.AddListener(upperboneItem_y_chg);
        upperboneItem_w.onValueChanged.AddListener(upperboneItem_w_chg);


        downlipItem_x.onValueChanged.AddListener(downlipItem_x_chg);
        downlipItem_y.onValueChanged.AddListener(downlipItem_y_chg);
        downlipItem_w.onValueChanged.AddListener(downlipItem_w_chg);


        downBoneItem_x.onValueChanged.AddListener(downBoneItem_x_chg);
        downBoneItem_y.onValueChanged.AddListener(downBoneItem_y_chg);
        downBoneItem_w.onValueChanged.AddListener(downBoneItem_w_chg);


        MiddleItem_x.onValueChanged.AddListener(MiddleItem_x_chg);
        MiddleItem_y.onValueChanged.AddListener(MiddleItem_y_chg);
        MiddleItem_w.onValueChanged.AddListener(MiddleItem_w_chg);


        cornerItem_x.onValueChanged.AddListener(cornerItem_x_chg);
        cornerItem_y.onValueChanged.AddListener(cornerItem_y_chg);
        cornerItem_w.onValueChanged.AddListener(cornerItem_w_chg);

        //cheset.......
        chestupperItem_x.onValueChanged.AddListener(chestupperItem_x_chg);
        chestupperItem_y.onValueChanged.AddListener(chestupperItem_y_chg);
        chestupperItem_w.onValueChanged.AddListener(chestupperItem_w_chg);

        chesttopItem_x.onValueChanged.AddListener(chesttopItem_x_chg);
        chesttopItem_y.onValueChanged.AddListener(chesttopItem_y_chg);
        chesttopItem_w.onValueChanged.AddListener(chesttopItem_w_chg);

        chestdownItem_x.onValueChanged.AddListener(chestdownItem_x_chg);
        chestdownItem_y.onValueChanged.AddListener(chestdownItem_y_chg);
        chestdownItem_w.onValueChanged.AddListener(chestdownItem_w_chg);


        //body.............
        neckItem_x.onValueChanged.AddListener(neckItem_x_chg);
        neckItem_w.onValueChanged.AddListener(neckItem_w_chg);

        chestItem_x.onValueChanged.AddListener(chestItem_x_chg);
        chestItem_w.onValueChanged.AddListener(chestItem_w_chg);

        wristItem_x.onValueChanged.AddListener(wristItem_x_chg);
        wristItem_w.onValueChanged.AddListener(wristItem_w_chg);

        hipItem_x.onValueChanged.AddListener(hipItem_x_chg);
        hipItem_w.onValueChanged.AddListener(hipItem_w_chg);

        LegItem_x.onValueChanged.AddListener(LegItem_x_chg);
        LegItem_w.onValueChanged.AddListener(LegItem_w_chg);

        ArmItem_x.onValueChanged.AddListener(ArmItem_x_chg);
        ArmItem_w.onValueChanged.AddListener(ArmItem_w_chg);



        //预加载slider值
        DeformDataLoad();
        SliderLoad();
        
    }




    public void DeformDataLoad()
    {
        var deformdata = JsonUtility.FromJson<User_Deform>(Deformjson);
        



        ForeheadSwitch_data = deformdata.shape.ForeheadSwitch;
        TempleSwitch_data = deformdata.shape.TempleSwitch;
        BISjawSwitch_data = deformdata.shape.BISjawSwitch;
        ChinSwitch_data = deformdata.shape.ChinSwitch;
        
        ApplemuscleSwitch_data = deformdata.face .ApplemuscleSwitch;
        CheekbonesSwitch_data = deformdata.face .CheekbonesSwitch;
        FacialpartSwitch_data = deformdata.face .FacialpartSwitch;
        
        BrowbowSwitch_data = deformdata.eyebrow .BrowbowSwitch;
        BrowHeadSwitch_data = deformdata.eyebrow.BrowHeadSwitch;
        BrowMiddleSwitch_data = deformdata.eyebrow.BrowMiddleSwitch;
        BrowTailSwitch_data = deformdata.eyebrow.BrowTailSwitch;
        
        EyecornerSwitch_data = deformdata.eye.EyecornerSwitch;
        UppereyelidSwitch_data = deformdata.eye.UppereyelidSwitch;
        DoublefoldEyelidsSwitch_data = deformdata.eye.DoublefoldEyelidsSwitch;
        lowereyelidSwitch_data = deformdata.eye.lowereyelidSwitch;
        EyebagSwitch_data = deformdata.eye.EyebagSwitch;
        EyetailSwitch_data = deformdata.eye.EyetailSwitch ;
        BlackeyeSwitch_data = deformdata.eye.BlackeyeSwitch;
        
        UpperbridgeSwitch_data = deformdata.nose .UpperbridgeSwitch;
        InferiorbridgeSwitch_data = deformdata.nose.InferiorbridgeSwitch;
        NoseheadSwitch_data = deformdata.nose.NoseheadSwitch;
        ColumellaNasiSwitch_data = deformdata.nose.ColumellaNasiSwitch ;
        NasalBaseSwitch_data = deformdata.nose.NasalBaseSwitch;
        NoseWingSwitch_data = deformdata.nose.NoseWingSwitch;
        NostrilSwitch_data = deformdata.nose.NostrilSwitch;
        
        
        UplipSwitch_data = deformdata.mouth.UplipSwitch ;
        UpjawSwitch_data = deformdata.mouth.UpjawSwitch ;
        DownLipSwitch_data = deformdata.mouth.DownLipSwitch ;
        DownJawSwitch_data = deformdata.mouth.DownJawSwitch;
        PhiltrumSwitch_data = deformdata.mouth.PhiltrumSwitch;
        CornerSwitch_data = deformdata.mouth.CornerSwitch;
        
        upperItemSwitch_data = deformdata.chest.upperItemSwitch;
        topItemSwitch_data = deformdata.chest.topItemSwitch;
        downItemSwitch_data = deformdata.chest.downItemSwitch;
        
        NeckSwitch_data = deformdata.body.NeckSwitch;
        ChestSwitch_data = deformdata.body.ChestSwitch;
        WristSwitch_data = deformdata.body.WristSwitch;
        HipSwitch_data = deformdata.body.HipSwitch;
        LegSwitch_data = deformdata.body.LegSwitch;
        ArmSwitch_data = deformdata.body.ArmSwitch;

       // Debug.Log(ForeheadSwitch_data);

    }




    public void SliderLoad()
    {

        foreheadItem_x.value = ForeheadSwitch_data.x;
        foreheadItem_y.value = ForeheadSwitch_data.y;
        foreheadItem_w.value = ForeheadSwitch_data.w;

        TempleItem_x.value = TempleSwitch_data.x;
        TempleItem_y.value = TempleSwitch_data.y;
        TempleItem_w.value = TempleSwitch_data.w;

        BISjawItem_x.value = BISjawSwitch_data.x;
        BISjawItem_y.value = BISjawSwitch_data.y;
        BISjawItem_w.value = BISjawSwitch_data.w;

        ChinItem_x.value = ChinSwitch_data.x;
        ChinItem_y.value = ChinSwitch_data.y;
        ChinItem_w.value = ChinSwitch_data.w;

        //face..........
        ApplemuscleItem_x.value = ApplemuscleSwitch_data.x;
        ApplemuscleItem_y.value = ApplemuscleSwitch_data.y;
        ApplemuscleItem_w.value = ApplemuscleSwitch_data.w;

        CheekbonesItem_x.value = CheekbonesSwitch_data.x;
        CheekbonesItem_y.value = CheekbonesSwitch_data.y;
        CheekbonesItem_w.value = CheekbonesSwitch_data.w;

        FacialpartItem_x.value = FacialpartSwitch_data.x;
        FacialpartItem_y.value = FacialpartSwitch_data.y;
        FacialpartItem_w.value = FacialpartSwitch_data.w;


        //eyebrow....
        BrowbowItem_x.value = BrowbowSwitch_data.x;
        BrowbowItem_y.value = BrowbowSwitch_data.y;
        BrowbowItem_w.value = BrowbowSwitch_data.w;

        BrowHeadItem_x.value = BrowHeadSwitch_data.x;
        BrowHeadItem_y.value = BrowHeadSwitch_data.y;
        BrowHeadItem_w.value = BrowHeadSwitch_data.w;


        BrowMiddleItem_x.value = BrowMiddleSwitch_data.x;
        BrowMiddleItem_y.value = BrowMiddleSwitch_data.y;
        BrowMiddleItem_w.value = BrowMiddleSwitch_data.w;

        BrowTailItem_x.value = BrowTailSwitch_data.x;
        BrowTailItem_y.value = BrowTailSwitch_data.y;
        BrowTailItem_w.value = BrowTailSwitch_data.w;

        //eye.......
        EyecornerItem_x.value = EyecornerSwitch_data.x;
        EyecornerItem_y.value = EyecornerSwitch_data.y;
        EyecornerItem_w.value = EyecornerSwitch_data.w;

        UppereyelidItem_x.value = UppereyelidSwitch_data.x;
        UppereyelidItem_y.value = UppereyelidSwitch_data.y;
        UppereyelidItem_w.value = UppereyelidSwitch_data.w;

        DoublefoldEyelidsItem_x.value = DoublefoldEyelidsSwitch_data.x;
        DoublefoldEyelidsItem_y.value = DoublefoldEyelidsSwitch_data.y;
        DoublefoldEyelidsItem_w.value = DoublefoldEyelidsSwitch_data.w;

        lowereyelidItem_x.value = lowereyelidSwitch_data.x;
        lowereyelidItem_y.value = lowereyelidSwitch_data.y;
        lowereyelidItem_w.value = lowereyelidSwitch_data.w;

      
        EyebagItem_y.value = EyebagSwitch_data.y;
        EyebagItem_w.value = EyebagSwitch_data.w;

        EyetailItem_x.value = EyetailSwitch_data.x;
        EyetailItem_y.value = EyetailSwitch_data.y;
        EyetailItem_w.value = EyetailSwitch_data.w;

        BlackeyeItem_x.value = BlackeyeSwitch_data.x;
        BlackeyeItem_y.value = BlackeyeSwitch_data.y;
        BlackeyeItem_w.value = BlackeyeSwitch_data.w;
        
        //nose.........
        UpperbridgeItem_x.value = UpperbridgeSwitch_data.x;
        UpperbridgeItem_y.value = UpperbridgeSwitch_data.y;
        UpperbridgeItem_w.value = UpperbridgeSwitch_data.w;

        InferiorbridgeItem_x.value = InferiorbridgeSwitch_data.x;
        InferiorbridgeItem_y.value = InferiorbridgeSwitch_data.y;
        InferiorbridgeItem_w.value = InferiorbridgeSwitch_data.w;

        NoseheadItem_x.value = NoseheadSwitch_data.x;
        NoseheadItem_y.value = NoseheadSwitch_data.y;
        NoseheadItem_z.value = NoseheadSwitch_data.z;
        NoseheadItem_w.value = NoseheadSwitch_data.w;

        ColumellaNasiItem_x.value = ColumellaNasiSwitch_data.x;
        ColumellaNasiItem_y.value = ColumellaNasiSwitch_data.y;
        ColumellaNasiItem_w.value = ColumellaNasiSwitch_data.w;

        NasalBaseItem_x.value = NasalBaseSwitch_data.x;
        NasalBaseItem_y.value = NasalBaseSwitch_data.y;
        NasalBaseItem_w.value = NasalBaseSwitch_data.w;

        NoseWingItem_x.value = NoseWingSwitch_data.x;
        NoseWingItem_y.value = NoseWingSwitch_data.y;
        NoseWingItem_w.value = NoseWingSwitch_data.w;

        NostrilItem_x.value = NostrilSwitch_data.x;
        NostrilItem_y.value = NostrilSwitch_data.y;
        NostrilItem_w.value = NostrilSwitch_data.w;


        //mouth.........
        upperlipItem_x.value = UplipSwitch_data.x;
        upperlipItem_y.value = UplipSwitch_data.y;
        upperlipItem_w.value = UplipSwitch_data.w;

        upperboneItem_x.value = UpjawSwitch_data.x;
        upperboneItem_y.value = UpjawSwitch_data.y;
        upperboneItem_w.value = UpjawSwitch_data.w;


        downlipItem_x.value = DownLipSwitch_data.x;
        downlipItem_y.value = DownLipSwitch_data.y;
        downlipItem_w.value = DownLipSwitch_data.w;


        downBoneItem_x.value = DownJawSwitch_data.x;
        downBoneItem_y.value = DownJawSwitch_data.y;
        downBoneItem_w.value = DownJawSwitch_data.w;


        MiddleItem_x.value = PhiltrumSwitch_data.x;
        MiddleItem_y.value = PhiltrumSwitch_data.y;
        MiddleItem_w.value = PhiltrumSwitch_data.w;


        cornerItem_x.value = CornerSwitch_data.x;
        cornerItem_y.value = CornerSwitch_data.y;
        cornerItem_w.value = CornerSwitch_data.w;

        //cheset.......
        chestupperItem_x.value = upperItemSwitch_data.x;
        chestupperItem_y.value = upperItemSwitch_data.y;
        chestupperItem_w.value = upperItemSwitch_data.w;

        chesttopItem_x.value = topItemSwitch_data.x;
        chesttopItem_y.value = topItemSwitch_data.y;
        chesttopItem_w.value = topItemSwitch_data.w;

        chestdownItem_x.value = downItemSwitch_data.x;
        chestdownItem_y.value = downItemSwitch_data.y;
        chestdownItem_w.value = downItemSwitch_data.w;


        //body.............
        neckItem_x.value = NeckSwitch_data.x;
        neckItem_w.value = NeckSwitch_data.w;

        chestItem_x.value = ChestSwitch_data.x;
        chestItem_w.value = ChestSwitch_data.w;

        wristItem_x.value = WristSwitch_data.x;
        wristItem_w.value = WristSwitch_data.w;

        hipItem_x.value = HipSwitch_data.x;
        hipItem_w.value = HipSwitch_data.w;

        LegItem_x.value = LegSwitch_data.x;
        LegItem_w.value = LegSwitch_data.w;

        ArmItem_x.value = ArmSwitch_data.x;
        ArmItem_w.value = ArmSwitch_data.w;

    }


    


    public void foreheadItem_x_chg( float val){       

     ForeheadSwitch_data.x = val;
     Debug.Log(ForeheadSwitch_data);

        AppRoot.MainUser.CURForeheadSwitch = ForeheadSwitch_data; 



    }
    public void foreheadItem_y_chg(float val)
    {
        ForeheadSwitch_data.y = val;
        AppRoot.MainUser.CURForeheadSwitch = ForeheadSwitch_data;
        Debug.Log(val);
    }
    public void foreheadItem_z_chg(float val)
    {
        ForeheadSwitch_data.w = val;
        AppRoot.MainUser.CURForeheadSwitch = ForeheadSwitch_data;
        Debug.Log(val);
    }
    

    public void TempleItem_x_chg(float val)
    {
        TempleSwitch_data.x = val;
        AppRoot.MainUser.CURTempleSwitch = TempleSwitch_data;
    }
    public void TempleItem_y_chg(float val)
    {
        TempleSwitch_data.y = val;
        AppRoot.MainUser.CURTempleSwitch = TempleSwitch_data;
    }
    public void TempleItem_w_chg(float val)
    {
        TempleSwitch_data.w = val;
        AppRoot.MainUser.CURTempleSwitch = TempleSwitch_data;
    }

    
    public void BISjawItem_x_chg(float val)
    {
        BISjawSwitch_data.x = val;
        AppRoot.MainUser.CURBISjawSwitch = BISjawSwitch_data;
    }
    public void BISjawItem_y_chg(float val)
    {
        BISjawSwitch_data.y = val;
        AppRoot.MainUser.CURBISjawSwitch = BISjawSwitch_data;
    }
    public void BISjawItem_w_chg(float val)
    {
        BISjawSwitch_data.w = val;
        AppRoot.MainUser.CURBISjawSwitch = BISjawSwitch_data;
    }



    public void ChinItem_x_chg(float val)
    {
        ChinSwitch_data.x = val;
        AppRoot.MainUser.CURChinSwitch = ChinSwitch_data;
    }
    public void ChinItem_y_chg(float val)
    {
        ChinSwitch_data.y = val;
        AppRoot.MainUser.CURChinSwitch = ChinSwitch_data;
    }
    public void ChinItem_w_chg(float val)
    {
        ChinSwitch_data.w = val;
        AppRoot.MainUser.CURChinSwitch = ChinSwitch_data;
    }






    //face..........

    public void ApplemuscleItem_x_chg(float val)
    {
        ApplemuscleSwitch_data.x = val;
        AppRoot.MainUser.cur_ApplemuscleSwitch = ApplemuscleSwitch_data;
    }
    public void ApplemuscleItem_y_chg(float val)
    {
        ApplemuscleSwitch_data.y = val;
        AppRoot.MainUser.cur_ApplemuscleSwitch = ApplemuscleSwitch_data;
    }
    public void ApplemuscleItem_w_chg(float val)
    {
        ApplemuscleSwitch_data.w = val;
        AppRoot.MainUser.cur_ApplemuscleSwitch = ApplemuscleSwitch_data;
    }


    public void CheekbonesItem_x_chg(float val)
    {
        CheekbonesSwitch_data.x = val;
        AppRoot.MainUser.cur_CheekbonesSwitch = CheekbonesSwitch_data;
    }
    public void CheekbonesItem_y_chg(float val)
    {
        CheekbonesSwitch_data.y = val;
        AppRoot.MainUser.cur_CheekbonesSwitch = CheekbonesSwitch_data;
    }
    public void CheekbonesItem_w_chg(float val)
    {
        CheekbonesSwitch_data.w = val;
        AppRoot.MainUser.cur_CheekbonesSwitch = CheekbonesSwitch_data;
    }



    public void FacialpartItem_x_chg(float val)
    {
        FacialpartSwitch_data.x = val;
        AppRoot.MainUser.cur_FacialpartSwitch = FacialpartSwitch_data;
    }
    public void FacialpartItem_y_chg(float val)
    {
        FacialpartSwitch_data.y = val;
        AppRoot.MainUser.cur_FacialpartSwitch = FacialpartSwitch_data;
    }
    public void FacialpartItem_w_chg(float val)
    {
        FacialpartSwitch_data.w = val;
        AppRoot.MainUser.cur_FacialpartSwitch = FacialpartSwitch_data;
    }







    //eyebrow....

    public void BrowbowItem_x_chg(float val)
    {
        BrowbowSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowbowSwitch = BrowbowSwitch_data;
    }
    public void BrowbowItem_y_chg(float val)
    {
        BrowbowSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowbowSwitch = BrowbowSwitch_data;
    }
    public void BrowbowItem_w_chg(float val)
    {
        BrowbowSwitch_data.w = val;
        AppRoot.MainUser.cur_BrowbowSwitch = BrowbowSwitch_data;
    }


    public void BrowHeadItem_x_chg(float val)
    {
        BrowHeadSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = BrowHeadSwitch_data;
    }
    public void BrowHeadItem_y_chg(float val)
    {
        BrowHeadSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = BrowHeadSwitch_data;
    }
    public void BrowHeadItem_w_chg(float val)
    {
        BrowHeadSwitch_data.w = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = BrowHeadSwitch_data;
    }



    public void BrowMiddleItem_x_chg(float val)
    {
        BrowMiddleSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowMiddleSwitch_data;
    }
    public void BrowMiddleItem_y_chg(float val)
    {
        BrowMiddleSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowMiddleSwitch_data;
    }
    public void BrowMiddleItem_w_chg(float val)
    {
        BrowMiddleSwitch_data.w = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowMiddleSwitch_data;
    }


    public void BrowTailItem_x_chg(float val)
    {
        BrowTailSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowTailSwitch_data;
    }
    public void BrowTailItem_y_chg(float val)
    {
        BrowTailSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowTailSwitch_data;
    }
    public void BrowTailItem_w_chg(float val)
    {
        BrowTailSwitch_data.w = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowTailSwitch_data;
    }






    //eye.......

    public void EyecornerItem_x_chg(float val)
    {
        EyecornerSwitch_data.x = val;
        AppRoot.MainUser.cur_EyecornerSwitch = EyecornerSwitch_data;
    }
    public void EyecornerItem_y_chg(float val)
    {
        EyecornerSwitch_data.y = val;
        AppRoot.MainUser.cur_EyecornerSwitch = EyecornerSwitch_data;
    }
    public void EyecornerItem_w_chg(float val)
    {
        EyecornerSwitch_data.w = val;
        AppRoot.MainUser.cur_EyecornerSwitch = EyecornerSwitch_data;
    }


    public void UppereyelidItem_x_chg(float val)
    {
        UppereyelidSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = UppereyelidSwitch_data;
    }
    public void UppereyelidItem_y_chg(float val)
    {
        UppereyelidSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = UppereyelidSwitch_data;
    }
    public void UppereyelidItem_w_chg(float val)
    {
        UppereyelidSwitch_data.w = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = UppereyelidSwitch_data;
    }



    public void DoublefoldEyelidsItem_x_chg(float val)
    {
        DoublefoldEyelidsSwitch_data.x = val;
        AppRoot.MainUser.cur_DoublefoldEyelidsSwitch = DoublefoldEyelidsSwitch_data;
    }
    public void DoublefoldEyelidsItem_y_chg(float val)
    {
        DoublefoldEyelidsSwitch_data.y = val;
        AppRoot.MainUser.cur_DoublefoldEyelidsSwitch = DoublefoldEyelidsSwitch_data;
    }
    public void DoublefoldEyelidsItem_w_chg(float val)
    {
        DoublefoldEyelidsSwitch_data.w = val;
        AppRoot.MainUser.cur_DoublefoldEyelidsSwitch = DoublefoldEyelidsSwitch_data;
    }


    public void lowereyelidItem_x_chg(float val)
    {
        lowereyelidSwitch_data.x = val;
        AppRoot.MainUser.cur_lowereyelidSwitch = lowereyelidSwitch_data;
    }
    public void lowereyelidItem_y_chg(float val)
    {
        lowereyelidSwitch_data.y = val;
        AppRoot.MainUser.cur_lowereyelidSwitch = lowereyelidSwitch_data;
    }
    public void lowereyelidItem_w_chg(float val)
    {
        lowereyelidSwitch_data.w = val;
        AppRoot.MainUser.cur_lowereyelidSwitch = lowereyelidSwitch_data;
    }



    
    public void EyebagItem_y_chg(float val)
    {
        EyebagSwitch_data.y = val;
        AppRoot.MainUser.cur_EyebagSwitch = EyebagSwitch_data;
    }
    public void EyebagItem_w_chg(float val)
    {
        EyebagSwitch_data.w = val;
        AppRoot.MainUser.cur_EyebagSwitch = EyebagSwitch_data;
    }



    public void EyetailItem_x_chg(float val)
    {
        EyetailSwitch_data.x = val;
        AppRoot.MainUser.cur_EyetailSwitch = EyetailSwitch_data;
    }
    public void EyetailItem_y_chg(float val)
    {
        EyetailSwitch_data.y = val;
        AppRoot.MainUser.cur_EyetailSwitch = EyetailSwitch_data;
    }
    public void EyetailItem_w_chg(float val)
    {
        EyetailSwitch_data.w = val;
        AppRoot.MainUser.cur_EyetailSwitch = EyetailSwitch_data;
    }


    public void BlackeyeItem_x_chg(float val)
    {
        BlackeyeSwitch_data.x = val;
        AppRoot.MainUser.cur_BlackeyeSwitch = BlackeyeSwitch_data;
    }
    public void BlackeyeItem_y_chg(float val)
    {
        BlackeyeSwitch_data.y = val;
        AppRoot.MainUser.cur_BlackeyeSwitch = BlackeyeSwitch_data;
    }
    public void BlackeyeItem_w_chg(float val)
    {
        BlackeyeSwitch_data.w = val;
        AppRoot.MainUser.cur_BlackeyeSwitch = BlackeyeSwitch_data;
    }



    //nose.........

    public void UpperbridgeItem_x_chg(float val)
    {
        UpperbridgeSwitch_data.x = val;
        AppRoot.MainUser.cur_UpperbridgeSwitch = UpperbridgeSwitch_data;
    }
    public void UpperbridgeItem_y_chg(float val)
    {
        UpperbridgeSwitch_data.y = val;
        AppRoot.MainUser.cur_UpperbridgeSwitch = UpperbridgeSwitch_data;
    }
    public void UpperbridgeItem_w_chg(float val)
    {
        UpperbridgeSwitch_data.w = val;
        AppRoot.MainUser.cur_UpperbridgeSwitch = UpperbridgeSwitch_data;
    }


    public void InferiorbridgeItem_x_chg(float val)
    {
        InferiorbridgeSwitch_data.x = val;
        AppRoot.MainUser.cur_InferiorbridgeSwitch = InferiorbridgeSwitch_data;
    }
    public void InferiorbridgeItem_y_chg(float val)
    {
        InferiorbridgeSwitch_data.y = val;
        AppRoot.MainUser.cur_InferiorbridgeSwitch = InferiorbridgeSwitch_data;
    }
    public void InferiorbridgeItem_w_chg(float val)
    {
        InferiorbridgeSwitch_data.w = val;
        AppRoot.MainUser.cur_InferiorbridgeSwitch = InferiorbridgeSwitch_data;
    }



    public void NoseheadItem_x_chg(float val)
    {
        NoseheadSwitch_data.x = val;
        AppRoot.MainUser.cur_NoseheadSwitch = NoseheadSwitch_data;
    }
    public void NoseheadItem_y_chg(float val)
    {
        NoseheadSwitch_data.y = val;
        AppRoot.MainUser.cur_NoseheadSwitch = NoseheadSwitch_data;
    }
    public void NoseheadItem_z_chg(float val)
    {
        NoseheadSwitch_data.z = val;
        AppRoot.MainUser.cur_NoseheadSwitch = NoseheadSwitch_data;
    }
    public void NoseheadItem_w_chg(float val)
    {
        NoseheadSwitch_data.w = val;
        AppRoot.MainUser.cur_NoseheadSwitch = NoseheadSwitch_data;
    }


    public void ColumellaNasiItem_x_chg(float val)
    {
        ColumellaNasiSwitch_data.x = val;
        AppRoot.MainUser.cur_ColumellaNasiSwitch = ColumellaNasiSwitch_data;
    }
    public void ColumellaNasiItem_y_chg(float val)
    {
        ColumellaNasiSwitch_data.y = val;
        AppRoot.MainUser.cur_ColumellaNasiSwitch = ColumellaNasiSwitch_data;
    }
    public void ColumellaNasiItem_w_chg(float val)
    {
        ColumellaNasiSwitch_data.w = val;
        AppRoot.MainUser.cur_ColumellaNasiSwitch = ColumellaNasiSwitch_data;
    }




    public void NasalBaseItem_x_chg(float val)
    {
        NasalBaseSwitch_data.x = val;
        AppRoot.MainUser.cur_NasalBaseSwitch = NasalBaseSwitch_data;
    }
    public void NasalBaseItem_y_chg(float val)
    {
        NasalBaseSwitch_data.y = val;
        AppRoot.MainUser.cur_NasalBaseSwitch = NasalBaseSwitch_data;
    }
    public void NasalBaseItem_w_chg(float val)
    {
        NasalBaseSwitch_data.w = val;
        AppRoot.MainUser.cur_NasalBaseSwitch = NasalBaseSwitch_data;
    }



    public void NoseWingItem_x_chg(float val)
    {
        NoseWingSwitch_data.x = val;
        AppRoot.MainUser.cur_NoseWingSwitch = NoseWingSwitch_data;
    }
    public void NoseWingItem_y_chg(float val)
    {
        NoseWingSwitch_data.y = val;
        AppRoot.MainUser.cur_NoseWingSwitch = NoseWingSwitch_data;
    }
    public void NoseWingItem_w_chg(float val)
    {
        NoseWingSwitch_data.w = val;
        AppRoot.MainUser.cur_NoseWingSwitch = NoseWingSwitch_data;
    }


    public void NostrilItem_x_chg(float val)
    {
        NostrilSwitch_data.x = val;
        AppRoot.MainUser.cur_NostrilSwitch = NostrilSwitch_data;
    }
    public void NostrilItem_y_chg(float val)
    {
        NostrilSwitch_data.y = val;
        AppRoot.MainUser.cur_NostrilSwitch = NostrilSwitch_data;
    }
    public void NostrilItem_w_chg(float val)
    {
        NostrilSwitch_data.w = val;
        AppRoot.MainUser.cur_NostrilSwitch = NostrilSwitch_data;
    }








    //mouth..............................................................



    public void upperlipItem_x_chg(float val)
    {
        UplipSwitch_data.x = val;
        AppRoot.MainUser.cur_UplipSwitch = UplipSwitch_data;
    }
    public void upperlipItem_y_chg(float val)
    {
        UplipSwitch_data.y = val;
        AppRoot.MainUser.cur_UplipSwitch = UplipSwitch_data;
    }
    public void upperlipItem_w_chg(float val)
    {
        UplipSwitch_data.w = val;
        AppRoot.MainUser.cur_UplipSwitch = UplipSwitch_data;
    }


    public void upperboneItem_x_chg(float val)
    {
        UpjawSwitch_data.x = val;
        AppRoot.MainUser.cur_UpjawSwitch = UpjawSwitch_data;
    }
    public void upperboneItem_y_chg(float val)
    {
        UpjawSwitch_data.y = val;
        AppRoot.MainUser.cur_UpjawSwitch = UpjawSwitch_data;
    }
    public void upperboneItem_w_chg(float val)
    {
        UpjawSwitch_data.w = val;
        AppRoot.MainUser.cur_UpjawSwitch = UpjawSwitch_data;
    }



    public void downlipItem_x_chg(float val)
    {
        DownLipSwitch_data.x = val;
        AppRoot.MainUser.cur_DownLipSwitch = DownLipSwitch_data;
    }
    public void downlipItem_y_chg(float val)
    {
        DownLipSwitch_data.y = val;
        AppRoot.MainUser.cur_DownLipSwitch = DownLipSwitch_data;
    }
    public void downlipItem_w_chg(float val)
    {
        DownLipSwitch_data.w = val;
        AppRoot.MainUser.cur_DownLipSwitch = DownLipSwitch_data;
    }


    public void downBoneItem_x_chg(float val)
    {
        DownJawSwitch_data.x = val;
        AppRoot.MainUser.cur_DownJawSwitch = DownJawSwitch_data;
    }
    public void downBoneItem_y_chg(float val)
    {
        DownJawSwitch_data.y = val;
        AppRoot.MainUser.cur_DownJawSwitch = DownJawSwitch_data;
    }
    public void downBoneItem_w_chg(float val)
    {
        DownJawSwitch_data.w = val;
        AppRoot.MainUser.cur_DownJawSwitch = DownJawSwitch_data;
    }
    

    public void MiddleItem_x_chg(float val)
    {
        PhiltrumSwitch_data.x = val;
        AppRoot.MainUser.cur_PhiltrumSwitch = PhiltrumSwitch_data;
    }
    public void MiddleItem_y_chg(float val)
    {
        PhiltrumSwitch_data.y = val;
        AppRoot.MainUser.cur_PhiltrumSwitch = PhiltrumSwitch_data;
    }
    public void MiddleItem_w_chg(float val)
    {
        PhiltrumSwitch_data.w = val;
        AppRoot.MainUser.cur_PhiltrumSwitch = PhiltrumSwitch_data;
    }



    public void cornerItem_x_chg(float val)
    {
        CornerSwitch_data.x = val;
        AppRoot.MainUser.cur_CornerSwitch = CornerSwitch_data;
    }
    public void cornerItem_y_chg(float val)
    {
        CornerSwitch_data.y = val;
        AppRoot.MainUser.cur_CornerSwitch = CornerSwitch_data;
    }
    public void cornerItem_w_chg(float val)
    {
        CornerSwitch_data.w = val;
        AppRoot.MainUser.cur_CornerSwitch = CornerSwitch_data;
    }




    //cheset...........................................





    public void chestupperItem_x_chg(float val)
    {
        upperItemSwitch_data.x = val;
        AppRoot.MainUser.cur_upperItemSwitch = upperItemSwitch_data;
    }
    public void chestupperItem_y_chg(float val)
    {
        upperItemSwitch_data.y = val;
        AppRoot.MainUser.cur_upperItemSwitch = upperItemSwitch_data;
    }
    public void chestupperItem_w_chg(float val)
    {
        upperItemSwitch_data.w = val;
        AppRoot.MainUser.cur_upperItemSwitch = upperItemSwitch_data;
    }


    public void chesttopItem_x_chg(float val)
    {
        topItemSwitch_data.x = val;
        AppRoot.MainUser.cur_topItemSwitch = topItemSwitch_data;
    }
    public void chesttopItem_y_chg(float val)
    {
        topItemSwitch_data.y = val;
        AppRoot.MainUser.cur_topItemSwitch = topItemSwitch_data;
    }
    public void chesttopItem_w_chg(float val)
    {
        topItemSwitch_data.w = val;
        AppRoot.MainUser.cur_topItemSwitch = topItemSwitch_data;
    }



    public void chestdownItem_x_chg(float val)
    {
        downItemSwitch_data.x = val;
        AppRoot.MainUser.cur_downItemSwitch = downItemSwitch_data;
    }
    public void chestdownItem_y_chg(float val)
    {
        downItemSwitch_data.y = val;
        AppRoot.MainUser.cur_downItemSwitch = downItemSwitch_data;
    }
    public void chestdownItem_w_chg(float val)
    {
        downItemSwitch_data.w = val;
        AppRoot.MainUser.cur_downItemSwitch = downItemSwitch_data;
    }






    /// <summary>
    /// 
    /// </summary>
    /// <param name="val"></param>
   ///body.............


    public void neckItem_x_chg(float val)
    {
        NeckSwitch_data.x = val;
        AppRoot.MainUser.cur_NeckSwitch = NeckSwitch_data;
    }  
    public void neckItem_w_chg(float val)
    {
        upperItemSwitch_data.w = val;
        AppRoot.MainUser.cur_upperItemSwitch = upperItemSwitch_data;
    }

    public void chestItem_x_chg(float val)
    {
        ChestSwitch_data.x = val;
        AppRoot.MainUser.cur_ChestSwitch = ChestSwitch_data;
    }
    public void chestItem_w_chg(float val)
    {
        ChestSwitch_data.w = val;
        AppRoot.MainUser.cur_ChestSwitch = ChestSwitch_data;
    }    

    public void wristItem_x_chg(float val)
    {
        WristSwitch_data.x = val;
        AppRoot.MainUser.cur_WristSwitch = WristSwitch_data;
    }
    public void wristItem_w_chg(float val)
    {
        WristSwitch_data.w = val;
        AppRoot.MainUser.cur_WristSwitch = WristSwitch_data;
    }
    
    public void hipItem_x_chg(float val)
    {
        HipSwitch_data.x = val;
        AppRoot.MainUser.cur_HipSwitch = HipSwitch_data;
    }
    public void hipItem_w_chg(float val)
    {
        HipSwitch_data.w = val;
        AppRoot.MainUser.cur_HipSwitch = HipSwitch_data;
    }


    public void LegItem_x_chg(float val)
    {
        LegSwitch_data.x = val;
        AppRoot.MainUser.cur_LegSwitch = LegSwitch_data;
    }
    public void LegItem_w_chg(float val)
    {
        LegSwitch_data.w = val;
        AppRoot.MainUser.cur_LegSwitch = LegSwitch_data;
    }

    
    public void ArmItem_x_chg(float val)
    {
        ArmSwitch_data.x = val;
        AppRoot.MainUser.cur_ArmSwitch = ArmSwitch_data;
    }
    public void ArmItem_w_chg(float val)
    {
        ArmSwitch_data.w = val;
        AppRoot.MainUser.cur_ArmSwitch = ArmSwitch_data;
    }










}
