using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ReadTable : MonoBehaviour
{
    


    public string ReadRoleJson()
    {
        string  JsonPath = "Table/Role.json";       
        string  RoleTexJson = LoadJson<User_Role>(JsonPath).ToString();
        Debug.Log(RoleTexJson);
        return RoleTexJson;
    }



    public string  ReadModelJson()
    {
        string JsonPath = "Table/Model.json";
        //User_Model ModelTexJson = new User_Model();
        string ModelTexJson = LoadJson<User_Model>(JsonPath).ToString(); ;
        Debug.Log(ModelTexJson);
        return ModelTexJson;
    }


    public string ReadDeformJson()
    {
        string JsonPath = "Table/Deform.json";
        //User_Deform DeformTexJson = new User_Deform();
        string DeformTexJson = LoadJson<User_Deform>(JsonPath).ToString(); ;
        Debug.Log(DeformTexJson);
        return DeformTexJson;
    }


    public string ReadOrnamentJson()
    {
        string JsonPath = "Table/Ornament.json";
        //User_Ornaments OrnamentTexJson = new User_Ornaments();
        string OrnamentTexJson = LoadJson<User_Ornaments>(JsonPath).ToString(); ;
        Debug.Log(OrnamentTexJson);
        return OrnamentTexJson;
    }



    public string  ReadEnvironmentJson()
    {
        string JsonPath = "Table/Environment.json";
      //  User_Environment EnvironmentTexJson = new User_Environment();
      string   EnvironmentTexJson = LoadJson<User_Environment>(JsonPath).ToString(); ;
        Debug.Log(EnvironmentTexJson);
        return EnvironmentTexJson;
    }




    public void WriteJsonFile(string JsonPath) {



       // string jstr = StreamReader

    }


    T LoadJson<T>(string loadPath)
    {
        string path = Path.Combine(Application.streamingAssetsPath, loadPath);
        if (File.Exists(path))
        {
            string jstr = File.ReadAllText(path);

            Debug.Log(jstr);
            return JsonUtility.FromJson<T>(jstr);

        }
        else {
            Debug.LogError("读取的文件不存在！");
            return default(T);
        }        
    }





}
