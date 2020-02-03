using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
public class UIActionItem : MonoBehaviour
{
    Button mButton;
    RawImage mIconImage;
    AnimatorOverrideController mOcerrideController;

    string mIconAddress;
    string mOverrideControllerAddress;

    bool bTextureLoaded = false;
    bool bControllerLoaded = false;

    public void SetItemData(ActionConfig.ActionItem config)
    {
        mIconAddress = config.icon;
        mOverrideControllerAddress = config.overridecontroller;
    }

    private void Awake()
    {
        mButton = GetComponent<Button>();
        mIconImage = GetComponent<RawImage>();
    }
    
    void Start()
    {

        var opIcon = Addressables.LoadAssetAsync<Texture>(mIconAddress);
        opIcon.Completed += OnLoadTextureDone;


        var opAction = Addressables.LoadAssetAsync<AnimatorOverrideController>(mOverrideControllerAddress);
        opAction.Completed += OnLoadControllerDone;

        mButton.onClick.AddListener(OnClick);
    }

    void OnLoadTextureDone(AsyncOperationHandle<Texture> obj)
    {
        mIconImage.texture = obj.Result;
        bTextureLoaded = true;
    }
    void OnLoadControllerDone(AsyncOperationHandle<AnimatorOverrideController> obj)
    {
        mOcerrideController = obj.Result;
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
            AvatarManager.Instance.PlayAction(mOcerrideController);
        }
    }
}
