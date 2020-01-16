using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
public class UIAvatarScrollView : MonoBehaviour
{
    public string mConfigAddress;
    public AvatarManager.AVATARPART mPart;
    public GameObject mItemPrefab;
    public Transform mContent;
    // Start is called before the first frame update
    void Start()
    {

        var opGo = Addressables.LoadAssetAsync<TextAsset>(mConfigAddress);
        opGo.Completed += OnLoadConfigDone;
    }

    void OnLoadConfigDone(AsyncOperationHandle<TextAsset> obj)
    {
        AvatarConfig config = AvatarConfig.Load(obj.Result.text);
        foreach(var itemConfig in config.list)
        {
            GameObject gonew = GameObject.Instantiate(mItemPrefab);
            gonew.transform.parent = mContent;
            gonew.transform.localScale = Vector3.one;

           UIAvatarItem item = gonew.GetComponent<UIAvatarItem>();
            item.SetItemData(itemConfig);

        }
    }
}
