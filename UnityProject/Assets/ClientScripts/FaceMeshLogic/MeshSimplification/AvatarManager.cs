using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public class AvatarConfig
{
    [Serializable]
    public class AvatarItem
    {
        [SerializeField]
        public string icon;
        [SerializeField]
        public string model;
    }
    [SerializeField]
    public List<AvatarItem> list = new List<AvatarItem>();

    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/../avatarConfig.bytes",jstr);
    }
    public static AvatarConfig Load(string jstr)
    {
        AvatarConfig config = JsonUtility.FromJson<AvatarConfig>(jstr);
        return config;
    }

}


public class AvatarManager : MonoBehaviour
{

    public enum AVATARPART
    {

        Makeup_Eyebrow,
        Makeup_Eyeshadow,
        Makeup_Eyelash,
        Makeup_Pupil,
        Makeup_Foundation,
        Makeup_Shadow,
        Makeup_Lip,
        Makeup_Tattoo,

        Cloth_Upper,
        Cloth_Down,
        Cloth_Suit,
        Cloth_Outter,
        Cloth_Underwear,
        Cloth_Shoes,
        Cloth_Ornaments,

        Ornaments_HAIR,
        Ornaments_Glass,
        Ornaments_Hats,
        Ornaments_Jewellery,        
        
    }


    Dictionary<AVATARPART, GameObject> mPartDic = new Dictionary<AVATARPART, GameObject>();

    public static AvatarManager Instance;

    bool bLoading = false;
    GameObject mTempGo;

    private void Awake()
    {
        Instance = this;
        AvatarConfig config = new AvatarConfig();
        AvatarConfig.AvatarItem item = new AvatarConfig.AvatarItem();
        item.icon = "1";
        item.model = "m1";
        config.list.Add(item);
        config.Save();



    }

    public void StartLoad(AVATARPART part,string id)
    {
        StartCoroutine(Load(part, id));
    }

    IEnumerator Load(AVATARPART part, string id)
    {
        while (bLoading)
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

        while (!opGo.IsDone || mTempGo == null)
        {
            yield return 0;

        }

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
            for (int i = 0; i < boneNum; i++)
            {
                Transform bone = smr.bones[i];
                if (DeformLeaderBoneManager.Instance.mBoneNameMap.ContainsKey(bone.name))
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

        mTempGo = null;
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
