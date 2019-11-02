using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Threading;

public class LoadScene : MonoSingleton<LoadScene>
{


    static public bool modelactive;
    static public bool isman;

    public string modellist = "{\"ModelID\": 1,\"shotcutImage\":url,\"TexturePath\":path,\"ModelPath\" :path,\"Editable\":1,},{\"ModelID\": 2,\"shotcutImage\":url,\"TexturePath\":path,\"ModelPath\" :path,\"Editable\":0,}";

    private bool _sussface;
    private  Text loadtext;

    //public NumberRollView _HeightScrollView;
   // public NumberRollView _WeightScrollView;

    private GameObject FacebudingUI;    

    public  Slider ProcessingSlider;

    [HideInInspector]
    public bool bStageLoaded = false;
    [HideInInspector]
    public bool bBackgroundLoaded = false;
    [HideInInspector]
    public bool bButtomLoaded = false;

    Button womensetbuttton, mansetbutton;


    void Start()
    {


        //AppRoot.MainScene.SetStage(20001);
        //AppRoot.MainScene.SetBackground(30001);
        //AppRoot.MainScene.SetButtom(40001);

       // StartCoroutine(OnLoadedScene());


        // 创建User  （登录成功时调用）//
        AppRoot.MainUser = new User();
        AppRoot.MainUser.Init(modellist);
       
       
    }



    public IEnumerator OnLoadedScene()
    {

        while (bStageLoaded == false || bBackgroundLoaded == false || bButtomLoaded == false)
        {
            yield return 0;
        }
        AppRoot.MainScene.Mainbuttom.transform.parent = AppRoot.MainScene.MainStage.Plat.transform;
    }




    public void OnManset()
    {     
        StartCoroutine(AppRoot.MainScene.SetMainRole(10001));
        AppRoot.MainUser.isman = true ;
    }

    public void OnWomanSet()
    {  
        StartCoroutine(AppRoot.MainScene.SetMainRole(10002));
        AppRoot.MainUser.isman = false;
    }



    public IEnumerator OnloadRolepart() {
        yield return 0;
        modelactive = true;
        AppRoot.MainScene.MainRole.Rolein.transform.parent = AppRoot.MainScene.MainStage.Plat.transform;

       // AppRoot.MainUser.setdefaultbody();       
        //  AppRoot.MainUser.RoletableID = id;   
        yield return 0;

        if (AppRoot.MainUser.isman) {
       
          //   AppRoot.MainUser.CUR_coat = 10100000;
            yield return 0;
        }

        else {
          // AppRoot.MainUser.CUR_coat = 20100000;
            yield return 0;
        }       
    }



    public IEnumerator OnLoadedRole(int id)
    {
        yield return 0;
        modelactive = true;
        AppRoot.MainScene.MainRole.Rolein.transform.parent = AppRoot.MainScene.MainStage.Plat.transform;        
       // AppRoot.MainUser.setdefaultbody();
       // AppRoot.MainUser.CURhigh = 0.0f;
        AppRoot.MainUser.RoletableID = id;
        
        yield return 0;
        if (id == 10001)
        {

            AppRoot.MainUser.isman = true;
          //  AppRoot.MainUser.CUR_coat = 10100000;
           // AppRoot.MainUser.CUR_hair = 10300000;
            yield return 0;

        }
        else if (id == 10002)
        {
            AppRoot.MainUser.isman = false;
          //  AppRoot.MainUser.CUR_coat = 20100000;
          //  AppRoot.MainUser.CUR_hair = 20300000;
            yield return 0;
        }

      
        yield return 0;
    }




    public void OnbodyhighnextCLK()
    {

      //  float _body = _HeightScrollView.GetValue();

        // AppRoot.MainScene.MainRole.SetBodyhigh(_body);

      ///  float _weight = _WeightScrollView.GetValue();
        // AppRoot.MainScene.MainRole.SetWeight(_weight);
    }


    public static bool _bJsonLoading = false;
    public static bool _bLoading = false;
   // static FP_Face _faceInfo;
    IEnumerator WaitForLoading()
    {
        while(_bLoading)
        {
            yield return 0;
        }
        
    }


































}

