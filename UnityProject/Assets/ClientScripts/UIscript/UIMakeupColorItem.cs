using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
public class UIMakeupColorItem : MonoBehaviour
{
    Button mButton;
    RawImage mIconImage;
    Vector3 HSV;
    MakeupColorConfig.MakeupColorItem mConfig;

    bool bTextureLoaded = false;
    bool bControllerLoaded = false;

    public void SetItemData(MakeupColorConfig.MakeupColorItem config)
    {
        mConfig = config;
    }

    private void Awake()
    {
        mButton = GetComponent<Button>();
        mIconImage = GetComponent<RawImage>();
    }
    
    void Start()
    {

        var opIcon = Addressables.LoadAssetAsync<string>(mConfig.HSV);
        opIcon.Completed += OnLoadIconDone;


        var opHSV = Addressables.LoadAssetAsync<string>(mConfig.HSV);
        opHSV.Completed += OnLoadHSVDone;

        mButton.onClick.AddListener(OnClick);
    }

    void OnLoadIconDone(AsyncOperationHandle<string> obj)
    {
        Vector3 v3 = Parse(obj.Result);
        Vector3 rgb = HSVConvertToRGB(v3);
        Color IconColor = new Color(rgb.x, rgb.y, rgb.z, 1); 
        mIconImage.color  = IconColor;
        bTextureLoaded = true;
    }
    void OnLoadHSVDone(AsyncOperationHandle<string> obj)
    {
        Vector3 v3 = Parse(obj.Result);
        Vector3 rgb = HSVConvertToRGB(v3);
        HSV = v3;
        bControllerLoaded = true;
    }
    private void OnDestroy()
    {
        mButton.onClick.RemoveListener(OnClick);
    }


    void OnClick()
    {
        if (bTextureLoaded && bControllerLoaded)
        {
            ModelDataManager.Instance.MakeupColor(mConfig.materialmember, HSV);
        }
    }


    Vector3 HSVConvertToRGB(Vector3 hsv)
    {
        float R, G, B;
        //float3 rgb;
        if (hsv.y == 0)
        {
            R = G = B = hsv.z;
        }
        else
        {
            hsv.x = hsv.x / 60.0f;
            int i = (int)hsv.x;
            float f = hsv.x - (float)i;
            float a = hsv.z * (1 - hsv.y);
            float b = hsv.z * (1 - hsv.y * f);
            float c = hsv.z * (1 - hsv.y * (1 - f));

            if (i == 0)
            {
                R = hsv.z; G = c; B = a;
            }
            else if (i == 1)
            {
                R = b; G = hsv.z; B = a;
            }
            else if (i == 2)
            {
                R = a; G = hsv.z; B = c;
            }
            else if (i == 3)
            {
                R = a; G = b; B = hsv.z;
            }
            else if (i == 4)
            {
                R = c; G = a; B = hsv.z;
            }
            else
            {
                R = hsv.z; G = a; B = b;
            }

        }

        Vector3 RGB = new Vector3(R, G, B);
        return RGB;
    }

    public Vector3 Parse(string name) {

        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }


}
