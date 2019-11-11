using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Threading;

public class LoadScene : MonoSingleton<LoadScene>
{


    static public bool modelactive;
    static public bool isman;

    private string modellist = "{\"ModelID\": 1,\"shotcutImage\":url,\"TexturePath\":path,\"ModelPath\" :path,\"Editable\":1,},{\"ModelID\": 2,\"shotcutImage\":url,\"TexturePath\":path,\"ModelPath\" :path,\"Editable\":0,}";

    private bool _sussface;
    private  Text loadtext;

    public Role cur_role;


    public  Slider ProcessingSlider;

    [HideInInspector]
    public bool bStageLoaded = false;
    [HideInInspector]
    public bool bBackgroundLoaded = false;
    [HideInInspector]
    public bool bButtomLoaded = false;

    
    //[HideInInspector]
    //public Role MainRole;


    void Start()
    {


        //AppRoot.MainScene.SetStage(20001);
        //AppRoot.MainScene.SetBackground(30001);
        //AppRoot.MainScene.SetButtom(40001);
       // StartCoroutine(OnLoadedScene());

        // 创建User  （登录成功时调用）//
        AppRoot.MainUser = new User();
        AppRoot.MainUser.Init(modellist); 
        AppRoot.MainDeform = new Deform();

        LoadRole();

    }
    

    public void LoadRole(  ) {

        AppRoot.MainRole = cur_role;

    }




   // public void loadmainRole(int id)
   // {

   //     StartCoroutine(SetMainRole(id));

   // }



   // public IEnumerator SetMainRole(int id)
   // {
   //     yield return 0;
   //     if (AppRoot.MainRole != null && AppRoot.MainRole.RoleID == id)
   //         yield return 0;

   //     RoleDef role;
   //     if (TableMgr.Instance.RoleDic.TryGetValue(id, out role))
   //     {

   //         //GameObject prefab = ResourceMgr.Instance.LoadFromAssetBundle<GameObject>(role.assetbundle);
   //         // BaseAssetLoader.Instance.StartLoadAsset(role.assetbundle, OnLoaded_SetMainRole, id, AssetBundleLoadManager.m_FromHttp);
   //         yield return 0;

   //     }
   //     else
   //     {
   //         Debug.LogError("Role id: " + id + " doesn't exist!");
   //     }
   //     yield return 0;
   // }
   // public void OnLoaded_SetMainRole(object obj, object param)
   // {
   //     int id = (int)param;
   //     GameObject prefab = obj as GameObject;
   //     if (prefab == null)
   //     {
   //         Debug.LogError("Can't load asset : " + id);
   //         return;
   //     }
   //     GameObject go = Instantiate(prefab);
   //     SetMainRole(go.GetComponent<Role>(), id);

   //     StartCoroutine(LoadScene.Instance.OnLoadedRole(id));
   // }
   // public void SetMainRole(Role role, int id)
   // {
   //     if (role == null)
   //         Debug.LogError("Set Role failed!");
   //     if (AppRoot.MainRole != null)
   //         GameObject.DestroyImmediate(AppRoot.MainRole.gameObject);
       
   //     AppRoot.MainRole = role;
   //     AppRoot.MainRole.RoleID = id;
   //     AppRoot.MainRole.transform.parent = this.transform;
   // }






   // public IEnumerator OnLoadedScene()
   // {

   //     while (bStageLoaded == false || bBackgroundLoaded == false || bButtomLoaded == false)
   //     {
   //         yield return 0;
   //     }
   //     AppRoot.MainScene.Mainbuttom.transform.parent = AppRoot.MainScene.MainStage.Plat.transform;
   // }




   // public void OnManset()
   // {     
   //     StartCoroutine(SetMainRole(10001));
   //     AppRoot.MainUser.isman = true ;
   // }
   // public void OnWomanSet()
   // {  
   //     StartCoroutine(SetMainRole(10002));
   //     AppRoot.MainUser.isman = false;
   // }



   // public IEnumerator OnloadRolepart() {
   //     yield return 0;
   //     modelactive = true;
   //     AppRoot.MainRole.Rolein.transform.parent = AppRoot.MainScene.MainStage.Plat.transform;

   //    // AppRoot.MainUser.setdefaultbody();       
   //     //  AppRoot.MainUser.RoletableID = id;   
   //     yield return 0;

   //     if (AppRoot.MainUser.isman) {
       
   //       //   AppRoot.MainUser.CUR_coat = 10100000;
   //         yield return 0;
   //     }

   //     else {
   //       // AppRoot.MainUser.CUR_coat = 20100000;
   //         yield return 0;
   //     }       
   // }
   // public IEnumerator OnLoadedRole(int id)
   // {
   //     yield return 0;
   //     modelactive = true;
   //     AppRoot.MainRole.Rolein.transform.parent = AppRoot.MainScene.MainStage.Plat.transform;        
   //    // AppRoot.MainUser.setdefaultbody();
   //    // AppRoot.MainUser.CURhigh = 0.0f;
   //     AppRoot.MainUser.RoletableID = id;
        
   //     yield return 0;
   //     if (id == 10001)
   //     {

   //         AppRoot.MainUser.isman = true;
   //       //  AppRoot.MainUser.CUR_coat = 10100000;
   //        // AppRoot.MainUser.CUR_hair = 10300000;
   //         yield return 0;

   //     }
   //     else if (id == 10002)
   //     {
   //         AppRoot.MainUser.isman = false;
   //       //  AppRoot.MainUser.CUR_coat = 20100000;
   //       //  AppRoot.MainUser.CUR_hair = 20300000;
   //         yield return 0;
   //     }

      
   //     yield return 0;
   // }




   // public void OnbodyhighnextCLK()
   // {

   //   //  float _body = _HeightScrollView.GetValue();

   //     // AppRoot.MainScene.MainRole.SetBodyhigh(_body);

   //   ///  float _weight = _WeightScrollView.GetValue();
   //     // AppRoot.MainScene.MainRole.SetWeight(_weight);
   // }

   // public static bool _bJsonLoading = false;
   // public static bool _bLoading = false;
   //// static FP_Face _faceInfo;
   // IEnumerator WaitForLoading()
   // {
   //     while(_bLoading)
   //     {
   //         yield return 0;
   //     }
        
   // }


































}

