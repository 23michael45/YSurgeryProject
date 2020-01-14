using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AvatarManager : MonoBehaviour
{

    public enum AVATARPART
    {
        AP_HAIR,
        AP_TOP,
        AP_BOTTOM,
    }


    Dictionary<AVATARPART, GameObject> mPartDic = new Dictionary<AVATARPART, GameObject>();

    public static AvatarManager Instance;

    bool bLoading = false;
    GameObject mTempGo;

    private void Awake()
    {
        Instance = this;
    }

    public void StartLoad(AVATARPART part,string id)
    {
        StartCoroutine(Load(part, id));
    }

    IEnumerator Load(AVATARPART part,string id)
    {
        while(bLoading)
        {
            yield return 0;
        }

        bLoading = true;

        var opGo = Addressables.LoadAssetAsync<GameObject>(id);
        opGo.Completed += OnLoadDone;


        if (!mPartDic.ContainsKey(part))
        {
            mPartDic[part] = null;
        }

        while (!opGo.IsDone)
        {
            yield return 0;

        }

        if(mTempGo)
        {

            SkinnedMeshRenderer smr = mTempGo.GetComponentInChildren<SkinnedMeshRenderer>();

            GameObject oldGo = mPartDic[part];
            mPartDic[part] = mTempGo;

            if (oldGo != null)
            {
                var oldSMR = oldGo.GetComponentInChildren<SkinnedMeshRenderer>();
                smr.bones = oldSMR.bones;
                GameObject.Destroy(oldGo);
            }
            else
            {
                int boneNum = smr.bones.Length;
                Transform[] newBones = new Transform[boneNum];
                for(int i = 0; i < boneNum; i++)
                {
                    Transform bone = smr.bones[i];
                    if(DeformLeaderBoneManager.Instance.mBoneNameMap.ContainsKey(bone.name))
                    {
                        newBones[i] = DeformLeaderBoneManager.Instance.mBoneNameMap[bone.name];
                    }
                    else
                    {
                        Debug.LogError("Load Avatar Bone not Found:" + bone.name);
                    }
                }
                smr.bones = newBones;

            }

        }
        bLoading = false;
    }


    void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        mTempGo = GameObject.Instantiate(obj.Result);
        mTempGo.transform.parent = ModelDataManager.Instance.mLowGeometryTemplate.transform;
        mTempGo.transform.localScale = Vector3.one;
        mTempGo.transform.localRotation = Quaternion.identity;
        mTempGo.transform.localPosition = Vector3.zero;
    }

}
