using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;

[Serializable]
public class LeaderBoneSliderMap
{

    [Serializable]
    public class LeaderBoneSliderPair
    {
        public string leaderBoneName;
        public string leaderBoneSymName;
        public List<string> sliderNames = new List<string>();

        [NonSerialized]
        public List<Slider> sliderControls = new List<Slider>();

    }

    [SerializeField]
    public List<LeaderBoneSliderPair> pairList = new List<LeaderBoneSliderPair>();


    [NonSerialized]
    public Dictionary<string, LeaderBoneSliderMap.LeaderBoneSliderPair> keyMap = new Dictionary<string, LeaderBoneSliderMap.LeaderBoneSliderPair>();
    [NonSerialized]
    public Dictionary<Slider, LeaderBoneSliderMap.LeaderBoneSliderPair> reverseMap = new Dictionary<Slider, LeaderBoneSliderMap.LeaderBoneSliderPair>();


    public static void GenJsonFile()
    {

        LeaderBoneSliderMap initMap = new LeaderBoneSliderMap();
        LeaderBoneSliderMap.LeaderBoneSliderPair pair = new LeaderBoneSliderMap.LeaderBoneSliderPair();
        pair.leaderBoneName = "face_forehead_Lf_joint1";
        pair.leaderBoneSymName = "face_forehead_Rt_joint1";
        pair.sliderNames.Add("foreheadItem_x");
        pair.sliderNames.Add("foreheadItem_y");
        pair.sliderNames.Add("foreheadItem_w");
        initMap.pairList.Add(pair);

        pair = new LeaderBoneSliderMap.LeaderBoneSliderPair();
        pair.leaderBoneName = "face_temple_Lf_joint1";
        pair.leaderBoneSymName = "face_temple_Rt_joint1";
        pair.sliderNames.Add("BISjawItem_x");
        pair.sliderNames.Add("BISjawItem_y");
        pair.sliderNames.Add("BISjawItem_z");
        pair.sliderNames.Add("BISjawItem_w");
        initMap.pairList.Add(pair);
        initMap.Save();

    }

    public void InitControl(Transform root)
    {

        Dictionary<string, Slider> tempDic = new Dictionary<string, Slider>();
        Slider[] sliders = root.gameObject.GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            tempDic.Add(slider.name, slider);
            Debug.Log(slider.name);
        }

        keyMap.Clear();
        reverseMap.Clear();

        foreach (LeaderBoneSliderMap.LeaderBoneSliderPair onepair in pairList)
        {
            onepair.sliderControls.Clear();
            foreach (string slidername in onepair.sliderNames)
            {
                Slider slider = tempDic[slidername];
                onepair.sliderControls.Add(slider);
                reverseMap.Add(slider, onepair);
            }
            keyMap.Add(onepair.leaderBoneName, onepair);
        }
    }
    public LeaderBoneSliderMap.LeaderBoneSliderPair FindLeaderBoneSliderPair(Slider slider)
    {
        if(reverseMap.ContainsKey(slider))
        {

            return reverseMap[slider];
        }
        else
        {
            Debug.LogWarning("LeaderBoneSliderMap reverseMap not contains key:" + slider.name);
            return null;
        }
    }

    public LeaderBoneSliderMap.LeaderBoneSliderPair FindLeaderBoneSliderPair(string leaderBoneName)
    {
        if (keyMap.ContainsKey(leaderBoneName))
        {

            return keyMap[leaderBoneName];
        }
        else
        {
            Debug.LogWarning("LeaderBoneSliderMap keyMap not contains key:" + leaderBoneName);
            return null;
        }
    }

    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Path.Combine(Application.dataPath, "Resources/LeaderBoneSliderMap.bytes"),jstr);

    }
    public static LeaderBoneSliderMap Load(Transform root)
    {
        TextAsset ta = Resources.Load<TextAsset>("LeaderBoneSliderMap");
        LeaderBoneSliderMap map = JsonUtility.FromJson<LeaderBoneSliderMap>(ta.text);
        map.InitControl(root);
        return map;
    }
}

public class DeformUI : MonoBehaviour
{
    public static DeformUI Instance;
    public string Deformjson;

    public float ratio = 0.1f;

    LeaderBoneSliderMap mLeaderBoneSliderMap;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        InitLeaderBoneSliderMap();
    }

    void InitLeaderBoneSliderMap()
    {

#if INITSAVE
        LeaderBoneSliderMap.GenJsonFile();
#endif
        mLeaderBoneSliderMap = LeaderBoneSliderMap.Load(transform);

    }
    public void Start()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            slider.onValueChanged.AddListener((float v) => { OnItemValueChanged(v, slider); });
      
        }
    }
    
    public void Reload()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            var pair = mLeaderBoneSliderMap.FindLeaderBoneSliderPair(slider);
            if(pair != null)
            {
                SetItemValueByLeaderBoneName(pair);
            }
        }
    }

    void OnItemValueChanged(float v, Slider item)
    {
        float val = v / ratio;
        var pair = mLeaderBoneSliderMap.FindLeaderBoneSliderPair(item);

        var offsetL = DeformLeaderBoneManager.Instance.GetLeaderBonePositonOffset(pair.leaderBoneName);

        var offsetR = DeformLeaderBoneManager.Instance.GetLeaderBonePositonOffset(pair.leaderBoneSymName);
        
        if (item.name.EndsWith("_x"))
        {

            offsetL.x = val;
            offsetR.x = -val;

        }
        else if (item.name.EndsWith("_y"))
        {
            offsetL.y = val;
            offsetR.y = val;

        }
        else if (item.name.EndsWith("_z"))
        {

            offsetL.z = val;
            offsetR.z = val;

        }
        else if (item.name.EndsWith("_w"))
        {
            //to do scale

        }
        DeformLeaderBoneManager.Instance.SetLeaderBonePosition(pair.leaderBoneName, offsetL);
        DeformLeaderBoneManager.Instance.SetLeaderBonePosition(pair.leaderBoneSymName, offsetR);
    }


    public void SetItemValueByLeaderBoneName(string leaderBoneName)
    {
        SetItemValueByLeaderBoneName(mLeaderBoneSliderMap.FindLeaderBoneSliderPair(leaderBoneName));
    }

    public void SetItemValueByLeaderBoneName(LeaderBoneSliderMap.LeaderBoneSliderPair pair)
    {
        var offset = DeformLeaderBoneManager.Instance.GetLeaderBonePositonOffset(pair.leaderBoneName);
        offset = offset * ratio;

        foreach(var slider in pair.sliderControls)
        {
            if (slider.name.EndsWith("_x"))
            {

                slider.value = offset.x;

            }
            else if (slider.name.EndsWith("_y"))
            {

                slider.value = offset.y;

            }
            else if (slider.name.EndsWith("_z"))
            {

                slider.value = offset.z;

            }
            else if (slider.name.EndsWith("_w"))
            {

                //to do scale
            }
        }

    }


}
