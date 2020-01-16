using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
public class UIAvatarItem : MonoBehaviour
{
    Button mButton;
    RawImage mIconImage;

    string mIconAddress;
    AvatarManager.AVATARPART mPart;
    string mModelAddress;

    public void SetItemData(AvatarConfig.AvatarItem config)
    {
        mIconAddress = config.icon;
        mModelAddress = config.model;

    }

    private void Awake()
    {
        mButton = GetComponent<Button>();
        mIconImage = GetComponent<RawImage>();
    }
    
    void Start()
    {

        var opGo = Addressables.LoadAssetAsync<Texture>(mIconAddress);
        opGo.Completed += OnLoadTextureDone;

        mButton.onClick.AddListener(OnClick);
    }

    void OnLoadTextureDone(AsyncOperationHandle<Texture> obj)
    {
        mIconImage.texture = obj.Result;
    }
    private void OnDestroy()
    {
        mButton.onClick.RemoveListener(OnClick);

    }


    void OnClick()
    {
        AvatarManager.Instance.StartLoad(mPart,mModelAddress);
    }
}
