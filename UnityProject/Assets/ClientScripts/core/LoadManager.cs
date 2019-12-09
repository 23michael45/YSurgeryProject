using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Threading;

//初始化及加载管理 加载场景、模型、

public class LoadManager : MonoSingleton<LoadManager>
{
    //模型基础信息
    public User_Model modeldata;
    public string TexturePath;
    public string ModelPath;
    public int Editable;
  
    public  GameObject face;

   
    private bool _sussface;
    private  Text loadtext;
    public Role cur_role;

    public SendMessage sendMessage;

    public  Slider ProcessingSlider;

    [HideInInspector]
    public bool bStageLoaded = false;
    [HideInInspector]
    public bool bBackgroundLoaded = false;
    [HideInInspector]
    public bool bButtomLoaded = false;


    public bool isdemo = true;

    public ReadTable readTable;

    public string Scencejson;
    public string Rolejson;
    public string Deformjson;
    public string Ornamentjson;
    public string modelstring;
    public string user;




    public void Start()
    {
        newUser();
        Debug.Log(Application.persistentDataPath);
    }


    public void LoadEnvironment(string EnvironmentJson ) {

        Debug.Log("加载环境");

    }


    public void LoadModel(string ModelString) {//加载模型

        DelData();//尝试删除场景中已有Face
        Debug.Log(ModelString);

        //  try {
        modeldata = JsonUtility.FromJson<User_Model>(ModelString);
            TexturePath = modeldata.TexturePath.ToString();
            Debug.Log(TexturePath);
            ModelPath = modeldata.ModelPath.ToString();
            Debug.Log(ModelPath);
            Editable = (int)float.Parse(modeldata.ModelPath.ToString());

            try
            {
                face = RuntimeLoadObj.RutimeLoadObj(ModelPath);

                try
                {
                    WWW w = new WWW(TexturePath);
                    Texture2D facetex = w.texture;

                    Material faematerial = new Material(Shader.Find("Unlit/Texture"));
                    faematerial.mainTexture = facetex;

                    if (face.transform.childCount != 0)
                    {
                        face.GetComponentInChildren<MeshRenderer>().material = faematerial;
                    }
                    else
                    { }
                }
                catch
                {
                    Debug.Log("无法加载贴图");
                }
            }
            catch
            {
                print("路径中无模型");
            }
        //} catch {

        //}

        Debug.Log("无法加载模型");
    }

    

    public void LoadEditMode(string ModelString) {//加载编辑器
        

        try {
            modeldata = JsonUtility.FromJson<User_Model>(ModelString);
            Editable = (int)float.Parse(modeldata.ModelPath.ToString());

            if (modeldata.Editable == 0)
            {

                Debug.Log("此模型不可编辑");
            }

            else if (modeldata.Editable == 1)
            {
                CalculateFullHead(face);//计算高模到低模

                AppRoot.MainUser.currentModel.Editable = 2; //最好在保存full模型的时候改变
            }
            else
            {
                
                LoadEditableRoleInfo(modeldata); //加载可编辑模型信息
            }

        } catch {

            Debug.Log("可编辑信息加载出错");
           
        }

        //newUser();//最终不需要此
    }


    public void newUser() {
        ///初始化可编辑模型信息。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
        AppRoot.MainUser = new User();
        AppRoot.MainUser.Init();

        AppRoot.MainDeform = new Deform();
       // Debug.Log(cur_role);

        if(cur_role)
        {
            AppRoot.MainRole = cur_role;
            Debug.Log(AppRoot.MainRole);
            AppRoot.MainRole.Init();
            ReadJsonTable();

        }


    }



    public void ReadJsonTable() {

        readTable = new ReadTable();
        Scencejson = readTable.ReadEnvironmentJson();
        Rolejson = readTable.ReadRoleJson();
        Deformjson = readTable.ReadDeformJson();
        Ornamentjson = readTable.ReadOrnamentJson();
        modelstring = readTable.ReadModelJson();

    }







    public void LoadEditableRoleInfo(User_Model modeldata)
    {

        //AppRoot.MainUser = new User();
        //AppRoot.MainUser.Init();
        //AppRoot.MainDeform = new Deform();


        try
        {
            AppRoot.MainUser.currentModel = modeldata;//加载总模型信息
            AppRoot.MainUser.currentModel.role = modeldata.role;//加载基础脸信息（包括基础骨骼位置和点位置）
            AppRoot.MainUser.currentModel.deform = modeldata.deform;// 加载已经编辑信息
            AppRoot.MainUser.currentModel.Ornament = modeldata.Ornament;// 加载全身基础模型

            //加载预制模型。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。

            int sex = AppRoot.MainUser.currentModel.role.sex;
            loadmainRole(sex);//分性别加载基础人体模型

        } catch {

            Debug.Log("user赋值出错");
           
        }


       

    }





    public void CalculateFullHead(GameObject face) {//高低模变化



        // 将类
        /* string rojson=AppRoot.MainUser.currentModel.role.ToString();*/
        SendMessage sendMessage = new SendMessage();//第一次生成需要记录骨骼点位置
      // sendMessage.SaveRolejson(rojson);



    }






    public void loadmainRole(int sex)//加载基础人体模型
    {

        if (sex == 0)
        {

            AppRoot.MainRole = cur_role;

        }
        else {

            AppRoot.MainRole = cur_role;
        }

        AppRoot.MainRole.Init();
    }





    void DelData()//删除场景中的face
    {

        GameObject stage = GameObject.Find("Stage");
        stage.transform.rotation = Quaternion.Euler(0, 0, 0);
        stage.transform.position = new Vector3(0, 0, 0);
        Camera.main.GetComponent<FreeView>().OnCameraHeadBtnClk();

        try
        {
            GameObject face = stage.transform.Find("face").gameObject;
            DestroyImmediate(face);
            Debug.Log("已删除人脸");
        }
        catch
        {
            Debug.Log("场景无人脸");
        }

        try
        {
            GameObject role = stage.transform.Find("role").gameObject;
            DestroyImmediate(role);
            Debug.Log("已删除人模");

        }
        catch
        {

            Debug.Log("场景无人体");
        }


    }




    ////保存 各种json//////////////////////////////////////////////////////////
    ///


    public void SaveScencejson()
    {

        try
        {
            string Scenejson = JsonUtility.ToJson(AppRoot.MainUser.currentProfile.environment);

            sendMessage.SaveScencejson(Scenejson);

            Debug.Log(Scenejson);
        }
        catch {

            Debug.Log("环境配置不存在");
        }

        
    }





    //保存模型信息。。。。。。。。。   
    public void SaveRolejson()
    {
        try
        {
            string Rolejson = JsonUtility.ToJson(AppRoot.MainUser.currentModel.role);

            Debug.Log(Rolejson);
            sendMessage.SaveRolejson(Rolejson);

            if (isdemo == true)
            {
                string savepath = "Table/Role.json";
                SaveJson(savepath, Rolejson);
            }
        }
        catch
        {

            Debug.Log("Role配置不存在");
        }
        
    }



    //保存当前编辑信息
    public void SaveDeformJson()
    {
        try
        {
            string Deformjson = JsonUtility.ToJson(AppRoot.MainUser.currentModel.deform);

            sendMessage.SaveDeformAs(Deformjson);

            Debug.Log(Deformjson);

            if (isdemo == true)
            {
                string savepath = "Table/Deform.json";
                SaveJson(savepath, Deformjson);
            }

        }
        catch
        {

            Debug.Log("Deform配置不存在");
        }

      
    }



    //编辑另存为。。。。。。。
    public void SaveDeformAs()
    {
        try
        {
            AppRoot.MainUser.currentModel.deform.DeformNumber += 1;

            string Deformjson = JsonUtility.ToJson(AppRoot.MainUser.currentModel.deform);

            sendMessage.SaveDeformAs(Deformjson);           

            Debug.Log(Deformjson);

            if (isdemo == true)
            {
                string savepath = "Table/Deform.json";
                SaveJson(savepath, Deformjson);
            }

        }
        catch
        {

            Debug.Log("Deform配置不存在");
        }




       
    }




    //保存当前配饰信息  包含发型、服装、化妆
    public void SaveOrnamentjson()
    {

        try
        {
            string Ornamentjson = JsonUtility.ToJson(AppRoot.MainUser.currentModel.Ornament);
            sendMessage.SaveOrnamentjson(Ornamentjson);
            Debug.Log(Ornamentjson);

            if (isdemo == true) {
                string savepath = "Table/Ornament.json";
                SaveJson(savepath, Ornamentjson);
            } 


        }
        catch
        {
            Debug.Log("Ornament配置不存在");
        }
              
    }


    void SaveJson(string savePath, string obj)    {
       
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, savePath), obj);
    }


    T LoadJson<T>(string loadPath)
    {
        string jstr = File.ReadAllText(Path.Combine(Application.streamingAssetsPath , loadPath));
        return JsonUtility.FromJson<T>(jstr);

    }
















}

