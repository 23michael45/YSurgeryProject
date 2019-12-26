using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;

[Serializable]
public class FacePartInfoMap
{

    [Serializable]
    public class FacePartInfoPair
    {
        public string textureName;
        public string leaderBoneName;
        public string leaderBoneSymName;
        public string toggleName;

        [NonSerialized]
        public Toggle toggleControl;

    }

    [SerializeField]
    public List<FacePartInfoPair> pairList = new List<FacePartInfoPair>();


    [NonSerialized]
    public Dictionary<string, FacePartInfoMap.FacePartInfoPair> keyMap = new Dictionary<string, FacePartInfoMap.FacePartInfoPair>();
    [NonSerialized]
    public Dictionary<Toggle, FacePartInfoMap.FacePartInfoPair> reverseMap = new Dictionary<Toggle, FacePartInfoMap.FacePartInfoPair>();


    public static void GenJsonFile()
    {
        FacePartInfoMap initMap = new FacePartInfoMap();
        FacePartInfoMap.FacePartInfoPair pair = new FacePartInfoMap.FacePartInfoPair();
        pair.textureName = "FaceAreaPNG/01Shape/forehead";
        pair.leaderBoneName = "face_forehead_Lf_joint1";
        pair.leaderBoneSymName = "face_forehead_Rt_joint1";
        pair.toggleName = "foreheadItem";
        initMap.pairList.Add(pair);

        pair = new FacePartInfoMap.FacePartInfoPair();
        pair.textureName = "FaceAreaPNG/01Shape/Temple";
        pair.leaderBoneName = "face_temple_Lf_joint1";
        pair.leaderBoneSymName = "face_temple_Rt_joint1";
        pair.toggleName = "BISjawItem";
        initMap.pairList.Add(pair);
        initMap.Save();

    }

    public void InitControl(Transform root)
    {

        Dictionary<string, Toggle> tempDic = new Dictionary<string, Toggle>();
        Toggle[] toggles = root.gameObject.GetComponentsInChildren<Toggle>(true);
        foreach (Toggle toggle in toggles)
        {
            if (tempDic.ContainsKey(toggle.name))
            {
                Debug.LogWarning("FacePartInfoMap InitControl Already Contains :" + toggle.name);
            }
            else
            {
                tempDic.Add(toggle.name, toggle);

            }
        }

        keyMap.Clear();
        reverseMap.Clear();

        foreach (FacePartInfoMap.FacePartInfoPair onepair in pairList)
        {

            Toggle toggle = tempDic[onepair.toggleName];
            onepair.toggleControl = toggle;
            reverseMap.Add(toggle, onepair);
            keyMap.Add(onepair.toggleName, onepair);
        }
    }
    public FacePartInfoMap.FacePartInfoPair FindPair(Toggle toggle)
    {
        if (reverseMap.ContainsKey(toggle))
        {

            return reverseMap[toggle];
        }
        else
        {
            Debug.LogWarning("FacePartInfoMap reverseMap not contains key:" + toggle.name);
            return null;
        }
    }

    public FacePartInfoMap.FacePartInfoPair FindPair(string togglename)
    {
        if (keyMap.ContainsKey(togglename))
        {

            return keyMap[togglename];
        }
        else
        {
            Debug.LogWarning("FacePartInfoMap keyMap not contains key:" + togglename);
            return null;
        }
    }

    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Path.Combine(Application.dataPath, "Resources/FacePartToggleMap.bytes"), jstr);

    }
    public static FacePartInfoMap Load(Transform root)
    {
        TextAsset ta = Resources.Load<TextAsset>("FacePartToggleMap");
        FacePartInfoMap map = JsonUtility.FromJson<FacePartInfoMap>(ta.text);
        map.InitControl(root);
        return map;
    }
}

public class FouseFacePart : MonoBehaviour
{

    FacePartInfoMap mFacePartInfoMap;
    public Transform mRootTransform;

    // shape.........
    FaceAreaTextureChange faceAreaTextureChange;

    private void Awake()
    {

        Debug.Log("FouseFacePart Awake");
        faceAreaTextureChange = new FaceAreaTextureChange();
        InitFaceToggleMap();
    }

    void InitFaceToggleMap()
    {
#if DO_INITSAVE
        FacePartInfoMap.GenJsonFile();
#endif
        mFacePartInfoMap = FacePartInfoMap.Load(mRootTransform);

    }


    public void Start()
    {
        bool firstToggle = true;
        foreach (var onepair in mFacePartInfoMap.pairList)
        {
            if (firstToggle)
            {
                ToggleItemChange(true, onepair.textureName, onepair.leaderBoneName, onepair.leaderBoneSymName);
                firstToggle = false;
            }
            onepair.toggleControl.onValueChanged.AddListener((bool b) =>
            {
                ToggleItemChange(b, onepair.textureName, onepair.leaderBoneName, onepair.leaderBoneSymName);
            });
        }


    }

    public void NoneAreaTexture()
    {
        string TexturePath = "FaceAreaPNG/noneTex";
        faceAreaTextureChange.ChangeFaceArea(TexturePath);

    }

    void ToggleItemChange(bool b, string texturePath, string leaderboneLeft, string leaderboneRight)
    {
        if (b)
        {

            faceAreaTextureChange.ChangeFaceArea(texturePath);

            DeformLeaderBoneManager.Instance.StartEdit(leaderboneLeft);
            DeformLeaderBoneManager.Instance.StartEdit(leaderboneRight);

            //左右对称，所以设置一次slider值即可
            DeformUI.Instance.SetItemValueByLeaderBoneName(leaderboneLeft);
        }
        else
        {
            DeformLeaderBoneManager.Instance.StopEdit(leaderboneLeft);
            DeformLeaderBoneManager.Instance.StopEdit(leaderboneRight);

        }
    }




}
