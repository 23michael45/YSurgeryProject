using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ReadTable : MonoBehaviour
{
    


    public string ReadRoleJson()
    {
        string  JsonPath = "Table/Role.json";       
        string  RoleTexJson = LoadJson(JsonPath);
      //  Debug.Log("RoleTexJson:" + RoleTexJson);
        return RoleTexJson;
    }



    public string  ReadModelJson()
    {
        string JsonPath = "Table/Model.json";
        //User_Model ModelTexJson = new User_Model();
        string ModelTexJson = LoadJson(JsonPath);
      Debug.Log("ModelTexJson:" + ModelTexJson);
        return ModelTexJson;
    }


    public string ReadDeformJson()
    {
        string JsonPath = "Table/Deform.json";
        //User_Deform DeformTexJson = new User_Deform();
        string DeformTexJson = LoadJson(JsonPath) ;
     //   Debug.Log("DeformTexJson:" + DeformTexJson);
        return DeformTexJson;
    }


    public string ReadOrnamentJson()
    {
        string JsonPath = "Table/Ornament.json";
        //User_Ornaments OrnamentTexJson = new User_Ornaments();
        string OrnamentTexJson = LoadJson(JsonPath) ;
     //   Debug.Log("OrnamentTexJson:" + OrnamentTexJson);
        return OrnamentTexJson;
    }



    public string  ReadEnvironmentJson()
    {
        string JsonPath = "Table/Environment.json";      
      string   EnvironmentTexJson = LoadJson(JsonPath) ;
     //   Debug.Log("EnvironmentTexJson:"+EnvironmentTexJson);
        return EnvironmentTexJson;
    }




    string LoadJson(string loadPath)
    {
        string path = Path.Combine(Application.streamingAssetsPath, loadPath);
        if (File.Exists(path))
        {
            string jstr = File.ReadAllText(path);           
            return jstr;
            //return JsonUtility.FromJson<T>(jstr);

        }
        else {
            Debug.LogError("读取的文件不存在！");
            return null;
        }        
    }





}
