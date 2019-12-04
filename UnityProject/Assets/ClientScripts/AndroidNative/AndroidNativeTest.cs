using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidNativeTest : MonoBehaviour
{
    AndroidNative mAndroidNativeUtils;
    // Start is called before the first frame update
    void Start()
    {
        mAndroidNativeUtils = new AndroidNative();

        
        NativeToUnityParam param = new NativeToUnityParam();
        param.paramlist = new List<string>();
        param.paramlist.Add("xxx/xxx/x.obj");
        param.paramlist.Add("xxx/xxx/tex.jpg");
        string jsonStr = JsonUtility.ToJson(param);

        mAndroidNativeUtils.CallFromUnity("CalculateLowPolyFace", jsonStr);
    }
    
    
}
