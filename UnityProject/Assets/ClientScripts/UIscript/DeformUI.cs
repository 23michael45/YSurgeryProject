using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeformUI : MonoSingleton<DeformUI>
{
    public string Deformjson;
   
    // shape.........
    public Slider foreheadItem_x, foreheadItem_y, foreheadItem_w;
    public Slider TempleItem_x,TempleItem_y,TempleItem_w;
    public Slider BISjawItem_x, BISjawItem_y, BISjawItem_w;
    public Slider ChinItem_x, ChinItem_y, ChinItem_w;

    public Slider TopHead_x, TopHead_y, TopHead_w;//add

    private Vector4 ForeheadSwitch_data,
                    TempleSwitch_data,
                    BISjawSwitch_data,
                    ChinSwitch_data, 
                    TopHead_data;
    

    //face..........
    public Slider ApplemuscleItem_x, ApplemuscleItem_y, ApplemuscleItem_w;
    public Slider CheekbonesItem_x, CheekbonesItem_y, CheekbonesItem_w;
    public Slider FacialpartItem_x, FacialpartItem_y, FacialpartItem_w;

    public Slider MasseterMuscle_x, MasseterMuscle_y, MasseterMuscle_w;//add


    private Vector4 ApplemuscleSwitch_data,
                    CheekbonesSwitch_data,
                    FacialpartSwitch_data,
                    MasseterMuscle_data;
    

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
    public Slider EyeZero_x, EyeZero_y, EyeZero_z, EyeZero_w;//add

    public Slider EyecornerItem_x, EyecornerItem_y, EyecornerItem_w;
    public Slider UppereyelidItem_x, UppereyelidItem_y, UppereyelidItem_w;
    public Slider DoublefoldEyelidsItem_x, DoublefoldEyelidsItem_y, DoublefoldEyelidsItem_w;
    public Slider lowereyelidItem_x, lowereyelidItem_y, lowereyelidItem_w;
    public Slider  EyebagItem_y, EyebagItem_w;
    public Slider EyetailItem_x, EyetailItem_y, EyetailItem_w;
    public Slider BlackeyeItem_x, BlackeyeItem_y, BlackeyeItem_w;

    private Vector4 EyeZero_data,
                    EyecornerSwitch_data,
                    UppereyelidSwitch_data,
                    DoublefoldEyelidsSwitch_data,
                    lowereyelidSwitch_data,
                    EyebagSwitch_data,
                    EyetailSwitch_data,
                    BlackeyeSwitch_data;




    //nose.........
    public Slider NoseZero_x, NoseZero_y, NoseZero_z, NoseZero_w;//add

    public Slider UpperbridgeItem_x, UpperbridgeItem_y, UpperbridgeItem_w;
    public Slider InferiorbridgeItem_x, InferiorbridgeItem_y, InferiorbridgeItem_w;
    public Slider NoseheadItem_x, NoseheadItem_y, NoseheadItem_z, NoseheadItem_w;
    public Slider ColumellaNasiItem_x, ColumellaNasiItem_y, ColumellaNasiItem_w;
    public Slider NasalBaseItem_x, NasalBaseItem_y, NasalBaseItem_w;
    public Slider NoseWingItem_x, NoseWingItem_y, NoseWingItem_w;
    public Slider NostrilItem_x, NostrilItem_y, NostrilItem_w;    


    private Vector4 NoseZero_data,
                    UpperbridgeSwitch_data,
                    InferiorbridgeSwitch_data,
                    NoseheadSwitch_data,
                    ColumellaNasiSwitch_data,
                    NasalBaseSwitch_data,
                    NoseWingSwitch_data;




    //mouth.........
    public Slider MouthZero_x, MouthZero_y, MouthZero_z, MouthZero_w;//add

    public Slider upperlipItem_x, upperlipItem_y, upperlipItem_w;
    public Slider upperboneItem_x, upperboneItem_y, upperboneItem_w;
    public Slider downlipItem_x, downlipItem_y, downlipItem_w;
    public Slider downBoneItem_x, downBoneItem_y, downBoneItem_w;
    public Slider MiddleItem_x, MiddleItem_y, MiddleItem_w;
    public Slider cornerItem_x, cornerItem_y, cornerItem_w;

    private Vector4  MouthZero_data,
                     UplipSwitch_data,
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

        //////////////
        TopHead_x.onValueChanged.AddListener(TopHead_x_chg);
        TopHead_y.onValueChanged.AddListener(TopHead_y_chg);
        TopHead_w.onValueChanged.AddListener(TopHead_w_chg);


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

        ////////////////
        MasseterMuscle_x.onValueChanged.AddListener(MasseterMuscle_x_chg);
        MasseterMuscle_y.onValueChanged.AddListener(MasseterMuscle_y_chg);
        MasseterMuscle_w.onValueChanged.AddListener(MasseterMuscle_w_chg);

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
        //////////////////////
        EyeZero_x.onValueChanged.AddListener(EyeZero_x_chg);
        EyeZero_y.onValueChanged.AddListener(EyeZero_y_chg);
        EyeZero_z.onValueChanged.AddListener(EyeZero_z_chg);
        EyeZero_w.onValueChanged.AddListener(EyeZero_w_chg);


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
      

        //nose.........
        //////////////
        NoseZero_x .onValueChanged.AddListener(NoseZero_x_chg);
        NoseZero_y .onValueChanged.AddListener(NoseZero_y_chg);
        NoseZero_z .onValueChanged.AddListener(NoseZero_z_chg);
        NoseZero_w .onValueChanged.AddListener(NoseZero_w_chg);

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

      

        //mouth.........
        ////////////////////////////
        MouthZero_x .onValueChanged.AddListener(MouthZero_x_chg);
        MouthZero_y .onValueChanged.AddListener(MouthZero_y_chg);
        MouthZero_z .onValueChanged.AddListener(MouthZero_z_chg);
        MouthZero_w .onValueChanged.AddListener(MouthZero_w_chg);

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


        Load(AppRoot.MainUser.currentModel.deform);
    }
    public void Load(DeformJson deform)
    {
        //先重置值为0
        ZeroDeformData();
        SliderLoad();

        //预加载slider值
        AppRoot.MainUser.currentModel.deform = deform;
        DeformDataLoad();
        SliderLoad();


    }
    public void ZeroDeformData()
    {

        ForeheadSwitch_data = new Vector4();
        TempleSwitch_data = new Vector4();
        BISjawSwitch_data = new Vector4();
        ChinSwitch_data = new Vector4();
        TopHead_data = new Vector4();

        ApplemuscleSwitch_data = new Vector4();
        CheekbonesSwitch_data = new Vector4();
        FacialpartSwitch_data = new Vector4();
        MasseterMuscle_data = new Vector4();

        BrowbowSwitch_data = new Vector4();
        BrowHeadSwitch_data = new Vector4();
        BrowMiddleSwitch_data = new Vector4();
        BrowTailSwitch_data = new Vector4();


        EyeZero_data = new Vector4();
        EyecornerSwitch_data = new Vector4();
        UppereyelidSwitch_data = new Vector4();
        DoublefoldEyelidsSwitch_data = new Vector4();
        lowereyelidSwitch_data = new Vector4();
        EyebagSwitch_data = new Vector4();
        EyetailSwitch_data = new Vector4();

        NoseZero_data = new Vector4();
        UpperbridgeSwitch_data = new Vector4();
        InferiorbridgeSwitch_data = new Vector4();
        NoseheadSwitch_data = new Vector4();
        ColumellaNasiSwitch_data = new Vector4();
        NasalBaseSwitch_data = new Vector4();
        NoseWingSwitch_data = new Vector4();


        MouthZero_data = new Vector4();
        UplipSwitch_data = new Vector4();
        UpjawSwitch_data = new Vector4();
        DownLipSwitch_data = new Vector4();
        DownJawSwitch_data = new Vector4();
        PhiltrumSwitch_data = new Vector4();
        CornerSwitch_data = new Vector4();

        upperItemSwitch_data = new Vector4();
        topItemSwitch_data = new Vector4();
        downItemSwitch_data = new Vector4();

        NeckSwitch_data = new Vector4();
        ChestSwitch_data = new Vector4();
        WristSwitch_data = new Vector4();
        HipSwitch_data = new Vector4();
        LegSwitch_data = new Vector4();
        ArmSwitch_data = new Vector4();

        // Debug.Log(ForeheadSwitch_data);

    }

    public void DeformDataLoad()
    {
        var deformdata = AppRoot.MainUser.currentModel.deform;

        ForeheadSwitch_data = deformdata.shape.ForeheadSwitch;
        TempleSwitch_data = deformdata.shape.TempleSwitch;
        BISjawSwitch_data = deformdata.shape.BISjawSwitch;
        ChinSwitch_data = deformdata.shape.ChinSwitch;
        TopHead_data = deformdata.shape.TopHeadSwitch;

        ApplemuscleSwitch_data = deformdata.face .ApplemuscleSwitch;
        CheekbonesSwitch_data = deformdata.face .CheekbonesSwitch;
        FacialpartSwitch_data = deformdata.face .FacialpartSwitch;
        MasseterMuscle_data = deformdata.face.MasseterMuscle;

        BrowbowSwitch_data = deformdata.eyebrow .BrowbowSwitch;
        BrowHeadSwitch_data = deformdata.eyebrow.BrowHeadSwitch;
        BrowMiddleSwitch_data = deformdata.eyebrow.BrowMiddleSwitch;
        BrowTailSwitch_data = deformdata.eyebrow.BrowTailSwitch;


        EyeZero_data = deformdata.eye.EyeZeroSwitch;
        EyecornerSwitch_data = deformdata.eye.EyecornerSwitch;
        UppereyelidSwitch_data = deformdata.eye.UppereyelidSwitch;
        DoublefoldEyelidsSwitch_data = deformdata.eye.DoublefoldEyelidsSwitch;
        lowereyelidSwitch_data = deformdata.eye.lowereyelidSwitch;
        EyebagSwitch_data = deformdata.eye.EyebagSwitch;
        EyetailSwitch_data = deformdata.eye.EyetailSwitch ;

        NoseZero_data = deformdata.nose.NoseZeroSwitch;
        UpperbridgeSwitch_data = deformdata.nose .UpperbridgeSwitch;
        InferiorbridgeSwitch_data = deformdata.nose.InferiorbridgeSwitch;
        NoseheadSwitch_data = deformdata.nose.NoseheadSwitch;
        ColumellaNasiSwitch_data = deformdata.nose.ColumellaNasiSwitch ;
        NasalBaseSwitch_data = deformdata.nose.NasalBaseSwitch;
        NoseWingSwitch_data = deformdata.nose.NoseWingSwitch;
       

        MouthZero_data = deformdata.mouth.MouthZeroSwitch;
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

        TopHead_x.value = TopHead_data.x;
        TopHead_y.value = TopHead_data.y;
        TopHead_w.value = TopHead_data.w;



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

        MasseterMuscle_x.value = MasseterMuscle_data.x;
        MasseterMuscle_y.value = MasseterMuscle_data.y;
        MasseterMuscle_w.value = MasseterMuscle_data.w;



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

        EyeZero_x.value = EyeZero_data.x;
        EyeZero_y.value = EyeZero_data.y;
        EyeZero_z.value = EyeZero_data.z;
        EyeZero_w.value = EyeZero_data.w;

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

        NoseZero_x.value = NoseZero_data.x;
        NoseZero_y.value = NoseZero_data.y;
        NoseZero_z.value = NoseZero_data.z;
        NoseZero_w.value = NoseZero_data.w;

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
               

        //mouth.........

        MouthZero_x.value = MouthZero_data.x;
        MouthZero_y.value = MouthZero_data.y;
        MouthZero_z.value = MouthZero_data.z;
        MouthZero_w.value = MouthZero_data.w;

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
   
        AppRoot.MainUser.CURForeheadSwitch = ForeheadSwitch_data; 

    }
    public void foreheadItem_y_chg(float val)
    {
        ForeheadSwitch_data.y = val;
        AppRoot.MainUser.CURForeheadSwitch = ForeheadSwitch_data;
        //Debug.Log(val);
    }
    public void foreheadItem_z_chg(float val)
    {
        ForeheadSwitch_data.z = val;
        AppRoot.MainUser.CURForeheadSwitch = ForeheadSwitch_data;
        //Debug.Log(val);
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
        TempleSwitch_data.z = val;
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
        BISjawSwitch_data.z = val;
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
        ChinSwitch_data.z = val;
        AppRoot.MainUser.CURChinSwitch = ChinSwitch_data;
    }
    

    public void TopHead_x_chg(float val)
    {
        TopHead_data.x = val;
        AppRoot.MainUser.CURTopHead = TopHead_data;
    }
    public void TopHead_y_chg(float val)
    {
        TopHead_data.y = val;
        AppRoot.MainUser.CURTopHead = TopHead_data;
    }
    public void TopHead_w_chg(float val)
    {
        TopHead_data.z = val;
        AppRoot.MainUser.CURTopHead = TopHead_data;
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
        ApplemuscleSwitch_data.z = val;
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
        CheekbonesSwitch_data.z= val;
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
        FacialpartSwitch_data.z = val;
        AppRoot.MainUser.cur_FacialpartSwitch = FacialpartSwitch_data;
    }


    public void MasseterMuscle_x_chg(float val)
    {
        MasseterMuscle_data.x = val;
        AppRoot.MainUser.cur_MasseterMuscle = MasseterMuscle_data;
    }
    public void MasseterMuscle_y_chg(float val)
    {
        MasseterMuscle_data.y = val;
        AppRoot.MainUser.cur_MasseterMuscle = MasseterMuscle_data;
    }
    public void MasseterMuscle_w_chg(float val)
    {
        MasseterMuscle_data.z = val;
        AppRoot.MainUser.cur_MasseterMuscle = MasseterMuscle_data;
    }




    //eyebrow....

    public void BrowbowItem_x_chg(float val)
    {
        BrowbowSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowbowSwitch = BrowbowSwitch_data;
        //Debug.Log(val);
    }
    public void BrowbowItem_y_chg(float val)
    {
        BrowbowSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowbowSwitch = BrowbowSwitch_data;
    }
    public void BrowbowItem_w_chg(float val)
    {
        BrowbowSwitch_data.z = val;
        AppRoot.MainUser.cur_BrowbowSwitch = BrowbowSwitch_data;
    }


    public void BrowHeadItem_x_chg(float val)
    {
        BrowHeadSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = BrowHeadSwitch_data;
        //Debug.Log(val);
    }
    public void BrowHeadItem_y_chg(float val)
    {
        BrowHeadSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = BrowHeadSwitch_data;
    }
    public void BrowHeadItem_w_chg(float val)
    {
        BrowHeadSwitch_data.z  = val;
        AppRoot.MainUser.cur_BrowHeadSwitch = BrowHeadSwitch_data;
    }



    public void BrowMiddleItem_x_chg(float val)
    {
        BrowMiddleSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowMiddleSwitch_data;
        //Debug.Log(val);
    }
    public void BrowMiddleItem_y_chg(float val)
    {
        BrowMiddleSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowMiddleSwitch_data;
    }
    public void BrowMiddleItem_w_chg(float val)
    {
        BrowMiddleSwitch_data.z = val;
        AppRoot.MainUser.cur_BrowMiddleSwitch = BrowMiddleSwitch_data;
    }


    public void BrowTailItem_x_chg(float val)
    {
        BrowTailSwitch_data.x = val;
        AppRoot.MainUser.cur_BrowTailSwitch = BrowTailSwitch_data;
    }
    public void BrowTailItem_y_chg(float val)
    {
        BrowTailSwitch_data.y = val;
        AppRoot.MainUser.cur_BrowTailSwitch = BrowTailSwitch_data;
    }
    public void BrowTailItem_w_chg(float val)
    {
        BrowTailSwitch_data.z = val;
        AppRoot.MainUser.cur_BrowTailSwitch = BrowTailSwitch_data;
    }






    //eye.......

    
    public void EyeZero_x_chg(float val)
    {
        EyeZero_data.x = val;
        AppRoot.MainUser.cur_EyeZeroSwitch = EyeZero_data;
    }
    public void EyeZero_y_chg(float val)
    {
        EyeZero_data.y = val;
        AppRoot.MainUser.cur_EyeZeroSwitch = EyeZero_data;
    }
    public void EyeZero_z_chg(float val)
    {
        EyeZero_data.y = val;
        AppRoot.MainUser.cur_EyeZeroSwitch = EyeZero_data;
    }
    public void EyeZero_w_chg(float val)
    {
        EyeZero_data.z = val;
        AppRoot.MainUser.cur_EyeZeroSwitch = EyeZero_data;
    }
    

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
        EyecornerSwitch_data.z = val;
        AppRoot.MainUser.cur_EyecornerSwitch = EyecornerSwitch_data;
    }


    public void UppereyelidItem_x_chg(float val)
    {
        UppereyelidSwitch_data.x = val;
        AppRoot.MainUser.cur_UppereyelidSwitch = UppereyelidSwitch_data;
    }
    public void UppereyelidItem_y_chg(float val)
    {
        UppereyelidSwitch_data.y = val;
        AppRoot.MainUser.cur_UppereyelidSwitch = UppereyelidSwitch_data;
    }
    public void UppereyelidItem_w_chg(float val)
    {
        UppereyelidSwitch_data.z = val;
        AppRoot.MainUser.cur_UppereyelidSwitch = UppereyelidSwitch_data;
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
        DoublefoldEyelidsSwitch_data.z = val;
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
        lowereyelidSwitch_data.z = val;
        AppRoot.MainUser.cur_lowereyelidSwitch = lowereyelidSwitch_data;
    }



    
    public void EyebagItem_y_chg(float val)
    {
        EyebagSwitch_data.y = val;
        AppRoot.MainUser.cur_EyebagSwitch = EyebagSwitch_data;
    }
    public void EyebagItem_w_chg(float val)
    {
        EyebagSwitch_data.z = val;
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
        EyetailSwitch_data.z = val;
        AppRoot.MainUser.cur_EyetailSwitch = EyetailSwitch_data;
    }






    //nose.........
    
    public void NoseZero_x_chg(float val)
    {
        NoseZero_data.x = val;
        AppRoot.MainUser.cur_NoseZeroSwitch = NoseZero_data;
    }
    public void NoseZero_y_chg(float val)
    {
        NoseZero_data.y = val;
        AppRoot.MainUser.cur_NoseZeroSwitch = NoseZero_data;
    }

    public void NoseZero_z_chg(float val)
    {
        NoseZero_data.y = val;
        AppRoot.MainUser.cur_NoseZeroSwitch = NoseZero_data;
    }
    public void NoseZero_w_chg(float val)
    {
        NoseZero_data.z = val;
        AppRoot.MainUser.cur_NoseZeroSwitch = NoseZero_data;
    }


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
        UpperbridgeSwitch_data.z = val;
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
        InferiorbridgeSwitch_data.z = val;
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
        //Debug.Log(val);
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
        ColumellaNasiSwitch_data.z = val;
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
        NasalBaseSwitch_data.z = val;
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
        NoseWingSwitch_data.z = val;
        AppRoot.MainUser.cur_NoseWingSwitch = NoseWingSwitch_data;
    }





    //mouth..............................................................


    

 public void MouthZero_x_chg(float val)
    {
        MouthZero_data.x = val;
        AppRoot.MainUser.cur_MouthZeroSwitch = MouthZero_data;
    }
    public void MouthZero_y_chg(float val)
    {
        MouthZero_data.y = val;
        AppRoot.MainUser.cur_MouthZeroSwitch = MouthZero_data;
    }
    public void MouthZero_z_chg(float val)
    {
        MouthZero_data.z = val;
        AppRoot.MainUser.cur_MouthZeroSwitch = MouthZero_data;
    }
    public void MouthZero_w_chg(float val)
    {
        MouthZero_data.w = val;
        AppRoot.MainUser.cur_MouthZeroSwitch = MouthZero_data;
    }



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
        UplipSwitch_data.z = val;
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
        UpjawSwitch_data.z = val;
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
        DownLipSwitch_data.z = val;
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
        DownJawSwitch_data.z = val;
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
        PhiltrumSwitch_data.z = val;
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
        CornerSwitch_data.z = val;
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
