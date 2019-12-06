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
        
    }



    public void TestCallJavaLoad()
    {
    }
    
}
