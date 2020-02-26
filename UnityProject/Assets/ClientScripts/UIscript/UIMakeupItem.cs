using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
public class UIMakeupItem : MonoBehaviour
{
    Button mButton;
    RawImage mIconImage;
    Texture mTexture;
    MakeupConfig.MakeupItem mConfig;

    bool bTextureLoaded = false;
    bool bControllerLoaded = false;

    public void SetItemData(MakeupConfig.MakeupItem config)
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

        var opIcon = Addressables.LoadAssetAsync<Texture>(mConfig.icon);
        opIcon.Completed += OnLoadIconDone;


        var opTexture = Addressables.LoadAssetAsync<Texture>(mConfig.texturename);
        opTexture.Completed += OnLoadTextureDone;

        mButton.onClick.AddListener(OnClick);
    }

    void OnLoadIconDone(AsyncOperationHandle<Texture> obj)
    {
        mIconImage.texture = obj.Result;
        bTextureLoaded = true;
    }
    void OnLoadTextureDone(AsyncOperationHandle<Texture> obj)
    {
        mTexture = obj.Result;
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
            ModelDataManager.Instance.Makeup(mConfig.materialmember,mTexture);
        }
    }
}
