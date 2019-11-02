using System.Collections;
using UnityEngine;



public class Scene : MonoBehaviour
{
    [HideInInspector]
    public Role MainRole;

    [HideInInspector]
    public Stage MainStage;
	public buttom Mainbuttom;
	public background Mainbackground;

    void Awake()
    {
        AppRoot.MainScene = this;

        Shader.EnableKeyword("UNITY_SPECCUBE_BOX_PROJECTION");
    }

    public void loadmainRole( int id ) {

        StartCoroutine(SetMainRole(id));
      
    }



    public IEnumerator SetMainRole(int id)
    {
        yield return 0;
        if (this.MainRole != null && this.MainRole.RoleID == id)
           yield  return 0;

        RoleDef role;
        if (TableMgr.Instance.RoleDic.TryGetValue(id, out role))        {

            //GameObject prefab = ResourceMgr.Instance.LoadFromAssetBundle<GameObject>(role.assetbundle);
           // BaseAssetLoader.Instance.StartLoadAsset(role.assetbundle, OnLoaded_SetMainRole, id, AssetBundleLoadManager.m_FromHttp);
            yield return 0;

        }
        else
        {
            Debug.LogError("Role id: " + id + " doesn't exist!");
        }
        yield return 0;
    }
    public void OnLoaded_SetMainRole(object obj,object param)
    {
        int id = (int)param;
        GameObject prefab = obj as GameObject;
        if (prefab == null)
        {
            Debug.LogError("Can't load asset : " + id);
            return;
        }
        GameObject go = Instantiate(prefab);
        SetMainRole(go.GetComponent<Role>(), id);

     StartCoroutine(LoadScene.Instance.OnLoadedRole(id));
    }
    public void SetMainRole(Role role, int id)
    {
        if (role == null)
            Debug.LogError("Set Role failed!");
        if (MainRole != null)
            GameObject.DestroyImmediate(MainRole.gameObject);
        MainRole = role;
        MainRole.RoleID = id;
        MainRole.transform.parent = this.transform;
    }

    public void SetStage(int stageID)
    {
        if (this.MainStage != null && this.MainStage.StageID == stageID)
            return;

        //StageDef def;


        //if (TableMgr.Instance.StageDic.TryGetValue(stageID, out def))
        //{
        //    //GameObject prefab = ResourceMgr.Instance.LoadFromAssetBundle<GameObject>(def.assetbundle);
        //   // BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoad_SetStage, stageID, AssetBundleLoadManager.m_FromHttp);
           
        //}
        //else
        //{
        //    Debug.LogError("Role id: " + stageID + " doesn't exist!");
        //}
    }


          
    void OnLoad_SetStage(object obj, object param)
    {
        int stageID = (int)param;
        GameObject prefab = obj as GameObject;
        if (prefab == null)
        {
            Debug.LogError("Can't load asset : " + stageID);
            return;
        }
        GameObject go = Instantiate(prefab);
        SetStage(go.GetComponent<Stage>(), stageID);

        LoadScene.Instance.bStageLoaded = true;
    }

    public void SetStage(Stage stage, int id)
    {
        if (stage == null)
           Debug.LogError("Set Stage failed!");

        if (MainStage != null)
            GameObject.DestroyImmediate(MainStage.gameObject);
        MainStage = stage;
        MainStage.StageID = id;
        MainStage.transform.parent = this.transform;
    }



	public void SetButtom(int buttomID)
	{
		if (this.Mainbuttom  != null && this.Mainbuttom.buttomID  == buttomID )
			return;
		
		//ButtomDef  def;
		//if (TableMgr.Instance.ButtomDic.TryGetValue(buttomID, out def))
		//{
		//	//GameObject prefab = ResourceMgr.Instance.LoadFromAssetBundle<GameObject>(def.assetbundle);

  //         // BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoad_SetButtom, buttomID, AssetBundleLoadManager.m_FromHttp);
          
		//}
		//else
		//{
		//	Debug.LogError("Role id: " + buttomID + " doesn't exist!");
		//}
	}
    void OnLoad_SetButtom(object obj, object param)
    {
        int buttomID = (int)param;
        GameObject prefab = obj as GameObject;
        if (prefab == null)
        {
            Debug.LogError("Can't load asset : " + buttomID);
            return;
        }
        GameObject go = Instantiate(prefab);
        SetButtom(go.GetComponent<buttom>(), buttomID);

        LoadScene.Instance.bButtomLoaded = true;
    }



    public void SetButtom(buttom  buttom , int id)
	{
		if (buttom == null)
			Debug.LogError("Set buttom failed!");

		if (Mainbuttom  != null)
			GameObject.DestroyImmediate(Mainbuttom .gameObject);
		Mainbuttom =buttom;
		Mainbuttom.buttomID = id;
		Mainbuttom.transform.parent = this.transform;
	}



	public void SetBackground(int backgroundID)
	{
		if (this.Mainbackground   != null && this.Mainbackground .backgroundID   == backgroundID  )
			return;

		//BackgroundDef   def;
		//if (TableMgr.Instance.BackgrounDic .TryGetValue(backgroundID , out def))
		//{
		//	//GameObject prefab = ResourceMgr.Instance.LoadFromAssetBundle<GameObject>(def.assetbundle);

  //         // BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoad_SetBackground, backgroundID, AssetBundleLoadManager.m_FromHttp);
           
		//}
		//else
		//{
		//	Debug.LogError("Role id: " + backgroundID  + " doesn't exist!");
		//}
	}

    void OnLoad_SetBackground(object obj, object param)
    {
        int backgroundID = (int)param;
        GameObject prefab = obj as GameObject;
        if (prefab == null)
        {
            Debug.LogError("Can't load asset : " + backgroundID);
            return;
        }
        GameObject go = Instantiate(prefab);
        SetBackground(go.GetComponent<background>(), backgroundID);

                LoadScene.Instance.bBackgroundLoaded = true;
    }



    public void SetBackground(background background , int id)
	{
		if (background == null)
			Debug.LogError("Set background failed!");

		if (Mainbackground  != null)
			GameObject.DestroyImmediate(Mainbackground .gameObject);
		Mainbackground=background ;
		Mainbackground.backgroundID  = id;
		Mainbackground.transform.parent = this.transform;
	}



}

