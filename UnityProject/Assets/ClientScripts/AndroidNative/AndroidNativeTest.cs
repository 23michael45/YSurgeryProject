using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AndroidNativeTest : MonoBehaviour
{
    public Button mCalculateBtn;
    public Button mLoadBtn;

    string GetStreamingAssetsPath()
    {
        string path;
#if UNITY_EDITOR
        path = "file:" + Application.dataPath + "/StreamingAssets";
#elif UNITY_ANDROID
     path = "jar:file://"+ Application.dataPath + "!/assets/";
#elif UNITY_IOS
     path = "file:" + Application.dataPath + "/Raw";
#else
     //Desktop (Mac OS or Windows)
     path = "file:"+ Application.dataPath + "/StreamingAssets";
#endif

        return path;
    }

    // Start is called before the first frame update
    void Awake()
    {

        mCalculateBtn.onClick.AddListener(TestCallJavaCalculate);
        mLoadBtn.onClick.AddListener(TestCallJavaLoad);

    }
    public void TestCallJavaCalculate()
    {

        NativeToUnityParam param = new NativeToUnityParam();
        param.paramlist = new List<string>();
        param.paramlist.Add("xxx/xxx/x.obj");
        param.paramlist.Add("xxx/xxx/tex.jpg");
        string jsonStr = JsonUtility.ToJson(param);


        AndroidNativeInterface.Instance.CallFromUnity("CalculateLowPolyFace", jsonStr);
    }



    public void TestCallJavaLoad()
    {
        string jsonStr = "";
        NativeToUnityParam param = new NativeToUnityParam();

        param.paramlist = new List<string>();
        param.paramlist.Add("Model/obama53149_role.json");
        param.paramlist.Add("Model/obamaTexture.jpg");
        jsonStr = JsonUtility.ToJson(param);
        AndroidNativeInterface.Instance.CallFromUnity("LoadLowPolyFace", jsonStr);

        param.paramlist = new List<string>();
        param.paramlist.Add("Model/obama53149_deform.json");
        jsonStr = JsonUtility.ToJson(param);
        AndroidNativeInterface.Instance.CallFromUnity("LoadDeform", jsonStr);
    }
    
}
