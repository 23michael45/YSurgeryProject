using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuntimeLoadObj : MonoBehaviour
{





    // Start is called before the first frame update
    public   void  Starttemp()
    {

       // string filepath = "C: /Users/Administrator/AppData/LocalLow/Meixuan/Meixuan/model/face/face.obj";
       // GameObject go = RutimeLoadObj(filepath);
     
    }


    public static GameObject RutimeLoadObj(string filePath )
    {


        
        GameObject gomesh = new OBJLoader().Load(filePath,false);
        GameObject stage = GameObject.Find("Stage");  

        gomesh.transform.SetParent(stage.transform);
        gomesh.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gomesh.transform.localPosition = new Vector3(0, 173f, 0);

        return gomesh;

    }










}
