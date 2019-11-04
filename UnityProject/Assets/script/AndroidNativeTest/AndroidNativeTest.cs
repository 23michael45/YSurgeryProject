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
        mAndroidNativeUtils.CallFromUnity("AndroidNativeTest",1234);
    }
    
}
