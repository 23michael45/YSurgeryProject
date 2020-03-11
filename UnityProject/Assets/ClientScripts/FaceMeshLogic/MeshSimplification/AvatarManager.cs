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
[Serializable]
public class ActionConfig
{
    [Serializable]
    public class ActionItem
    {
        [SerializeField]
        public string icon;
        [SerializeField]
        public string overridecontroller;
    }
    [SerializeField]
    public List<ActionItem> list = new List<ActionItem>();

    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/../actionConfig.bytes", jstr);
    }
    public static ActionConfig Load(string jstr)
    {
        ActionConfig config = JsonUtility.FromJson<ActionConfig>(jstr);
        return config;
    }

}
[Serializable]
public class MakeupConfig
{

    [Serializable]
    public class MakeupItem
    {
        [SerializeField]
        public string icon;
        [SerializeField]
        public string materialmember;
        [SerializeField]
        public string texturename;
    }

    [SerializeField]
    public List<MakeupItem> list = new List<MakeupItem>();

    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/../makeupConfig.bytes", jstr);
    }
    public static MakeupConfig Load(string jstr)
    {
        MakeupConfig config = JsonUtility.FromJson<MakeupConfig>(jstr);
        return config;
    }

}


//化妆调色
[Serializable]
public class MakeupColorConfig
{

    [Serializable]
    public class MakeupColorItem
    {        
        [SerializeField]
        public string materialmember;
        [SerializeField]
        public string HSV;
    }

    [SerializeField]
    public List<MakeupColorItem> list = new List<MakeupColorItem>();

    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/../makeupColorConfig.bytes", jstr);
    }
    public static MakeupColorConfig Load(string jstr)
    {
        MakeupColorConfig config = JsonUtility.FromJson<MakeupColorConfig>(jstr);
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

    public void StartLoadAvatar(AVATARPART part,string id)
    {
        StartCoroutine(Load(part, id));
    }
    public void PlayAction(AnimatorOverrideController overrideController)
    {
        Animator animator = ModelDataManager.Instance.mLowGeometryTemplate.GetComponent<Animator>();
        animator.runtimeAnimatorController = overrideController;
        animator.Play("Act");
    }
    public void ClearAction()
    {
        Animator animator = ModelDataManager.Instance.mLowGeometryTemplate.GetComponent<Animator>();
        animator.runtimeAnimatorController = null;

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

    public List<SkinnedMeshRenderer> GetAllSkinnedMeshRenderer()
    {
        List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>();
        foreach (var pair in mPartDic)
        {
            GameObject go = pair.Value;
            SkinnedMeshRenderer smr = go.GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr != null)
            {
                list.Add(smr);
            }
        }
        return list;
    }
}
