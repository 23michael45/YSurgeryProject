﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;

[Serializable]
public class LeaderBoneControlMap
{

    [Serializable]
    public class LeaderBoneControlPair
    {
        public string ToggleName;
        public string ToggleTextureName;
        public string AreaTextureName;
        public string leaderBoneName;
        public string leaderBoneSymName;
        public List<string> sliderNames = new List<string>();
  

        [NonSerialized]
        public Toggle toggleControl;
        [NonSerialized]
        public List<Slider> sliderControls = new List<Slider>();

    }

    [SerializeField]
    public List<LeaderBoneControlPair> pairList = new List<LeaderBoneControlPair>();


    [NonSerialized]
    public Dictionary<string, LeaderBoneControlMap.LeaderBoneControlPair> keyMap = new Dictionary<string, LeaderBoneControlMap.LeaderBoneControlPair>();
    [NonSerialized]
    public Dictionary<Slider, LeaderBoneControlMap.LeaderBoneControlPair> reverseSliderMap = new Dictionary<Slider, LeaderBoneControlMap.LeaderBoneControlPair>();


    [NonSerialized]
    public Dictionary<string, LeaderBoneControlMap.LeaderBoneControlPair> keyToggleMap = new Dictionary<string, LeaderBoneControlMap.LeaderBoneControlPair>();
    [NonSerialized]
    public Dictionary<Toggle, LeaderBoneControlMap.LeaderBoneControlPair> reverseToggleMap = new Dictionary<Toggle, LeaderBoneControlMap.LeaderBoneControlPair>();



    public static void GenJsonFile()
    {

        LeaderBoneControlMap initMap = new LeaderBoneControlMap();
        LeaderBoneControlMap.LeaderBoneControlPair pair = new LeaderBoneControlMap.LeaderBoneControlPair();


        pair.ToggleName = "ForeheadSwitch";
        pair.ToggleTextureName = "EditPartToggle/01shape/forehead";
        pair.AreaTextureName = "FaceAreaPNG/01Shape/forehead";
        pair.leaderBoneName = "face_forehead_Lf_joint1";
        pair.leaderBoneSymName = "face_forehead_Rt_joint1";
        pair.sliderNames.Add("foreheadItem_x");
        pair.sliderNames.Add("foreheadItem_y");
        pair.sliderNames.Add("foreheadItem_w");
        initMap.pairList.Add(pair);

        pair = new LeaderBoneControlMap.LeaderBoneControlPair();

        pair.ToggleName = "BISjawSwitch";
        pair.ToggleTextureName = "EditPartToggle/01shape/cheek";
        pair.AreaTextureName = "FaceAreaPNG/01Shape/BISjaw";
        pair.leaderBoneName = "face_temple_Lf_joint1";
        pair.leaderBoneSymName = "face_temple_Rt_joint1";
        pair.sliderNames.Add("BISjawItem_x");
        pair.sliderNames.Add("BISjawItem_y");
        pair.sliderNames.Add("BISjawItem_z");
        pair.sliderNames.Add("BISjawItem_w");
        initMap.pairList.Add(pair);
        initMap.Save();

    }

    public void InitControl(Transform sliderRoot,Transform toggleRoot)
    {

        keyMap.Clear();
        reverseSliderMap.Clear();

        Dictionary<string, Slider> tempSliderDic = new Dictionary<string, Slider>();
        Slider[] sliders = sliderRoot.gameObject.GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            tempSliderDic.Add(slider.name, slider);
        }

        foreach (LeaderBoneControlMap.LeaderBoneControlPair onepair in pairList)
        {
            onepair.sliderControls.Clear();
            foreach (string slidername in onepair.sliderNames)
            {
                if (tempSliderDic.ContainsKey(slidername))
                {
                    Slider slider = tempSliderDic[slidername];
                    onepair.sliderControls.Add(slider);
                    reverseSliderMap.Add(slider, onepair);

                }
                else
                {
                    Debug.LogWarning("LeaderBoneSliderMap InitControl tempDic not contains : " + slidername);
                }
            }

            if(string.IsNullOrEmpty(onepair.leaderBoneName) || onepair.leaderBoneName == "null")
            {

                Debug.LogWarning(string.Format("onepair.leaderBoneName : {0}  is null contains : " , onepair.leaderBoneName));
            }
            else
            {

                if (keyMap.ContainsKey(onepair.leaderBoneName))
                {

                    Debug.LogError("LeaderBoneSliderMap keyMap already contains : " + onepair.leaderBoneName);
                }
                else
                {
                    keyMap.Add(onepair.leaderBoneName, onepair);

                }

            }
        }




        keyToggleMap.Clear();
        reverseToggleMap.Clear();

        Dictionary<string, Toggle> tempToggleDic = new Dictionary<string, Toggle>();
        Toggle[] toggles = toggleRoot.gameObject.GetComponentsInChildren<Toggle>(true);
        foreach (Toggle toggle in toggles)
        {
            if (tempToggleDic.ContainsKey(toggle.name))
            {
                Debug.LogWarning("FacePartInfoMap InitControl Already Contains :" + toggle.name);
            }
            else
            {
                tempToggleDic.Add(toggle.name, toggle);

            }
        }

        foreach (LeaderBoneControlMap.LeaderBoneControlPair onepair in pairList)
        {

            Toggle toggle = tempToggleDic[onepair.ToggleName];
            onepair.toggleControl = toggle;
            reverseToggleMap.Add(toggle, onepair);
            keyToggleMap.Add(onepair.ToggleName, onepair);
        }
    }
    public LeaderBoneControlMap.LeaderBoneControlPair FindPair(Slider slider)
    {
        if (reverseSliderMap.ContainsKey(slider))
        {

            return reverseSliderMap[slider];
        }
        else
        {
            Debug.LogWarning("LeaderBoneSliderMap reverseMap not contains key:" + slider.name);
            return null;
        }
    }
    public LeaderBoneControlMap.LeaderBoneControlPair FindPair(Toggle toggle)
    {
        if (reverseToggleMap.ContainsKey(toggle))
        {

            return reverseToggleMap[toggle];
        }
        else
        {
            Debug.LogWarning("LeaderBoneControlMap reverseToggleMap not contains key:" + toggle.name);
            return null;
        }
    }
    public LeaderBoneControlMap.LeaderBoneControlPair FindPairByBoneName(string leaderBoneName)
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
    public LeaderBoneControlMap.LeaderBoneControlPair FindPairByToggleName(string togglename)
    {
        if (keyToggleMap.ContainsKey(togglename))
        {

            return keyToggleMap[togglename];
        }
        else
        {
            Debug.LogWarning("LeaderBoneControlMap keyToggleMap not contains key:" + togglename);
            return null;
        }
    }


    public void Save()
    {
        string jstr = JsonUtility.ToJson(this);
        File.WriteAllText(Path.Combine(Application.dataPath, "Resources/LeaderBoneControlMap.bytes"), jstr);

    }
    public static LeaderBoneControlMap Load(Transform sliderRoot,Transform toggleRoot)
    {
        TextAsset ta = Resources.Load<TextAsset>("LeaderBoneControlMap");
        LeaderBoneControlMap map = JsonUtility.FromJson<LeaderBoneControlMap>(ta.text);
        map.InitControl(sliderRoot,toggleRoot);
        return map;
    }
}

public class PartDetailToggleMap
{

    public Dictionary<string, Toggle> partToggleDic = new Dictionary<string, Toggle>();
    public Dictionary<string, Toggle> detailFirstToggleDic = new Dictionary<string, Toggle>();
}


public class DeformUI : MonoBehaviour
{
    public static DeformUI Instance;

    public Transform sliderRoot;
    public Transform toggleRoot;


    public Button ResetBtn;
    public Button UndoBtn;
    public Button SaveBtn;

    
    string currentDetailToggleName;
    string currentPartToggleName;


    Stack<Snapshot> undoStatck = new Stack<Snapshot>();
    
    [NonSerialized]
    public LeaderBoneControlMap mLeaderBoneControlMap;




    public List<Toggle> partToggles;
    public List<Toggle> detailFirstToggles;
    PartDetailToggleMap mPartDetailToggleMap;

    Coroutine mDelayFaceNoMaskTexCoroutine;

    private void Awake()
    {
        Debug.Log("DeformUI Awake");
        Instance = this;
        gameObject.SetActive(false);

        InitLeaderBoneSliderMap();
    }

    void InitLeaderBoneSliderMap()
    {

#if DO_INITSAVE
        LeaderBoneControlMap.GenJsonFile();
#endif
        mLeaderBoneControlMap = LeaderBoneControlMap.Load(sliderRoot,toggleRoot);

    }
    public void Start()
    {
        mPartDetailToggleMap = new PartDetailToggleMap();
        bool firstPartToggle = true;
        
        for(int i = 0; i< partToggles.Count;i++)
        {
            Toggle partToggle = partToggles[i];
            Toggle detailFirstToggle = detailFirstToggles[i];

            mPartDetailToggleMap.partToggleDic[partToggle.name] = partToggle;
            mPartDetailToggleMap.detailFirstToggleDic[partToggle.name] = detailFirstToggle;

            if (firstPartToggle)
            {
                PartToggleChange(true, partToggle);
                firstPartToggle = false;
            }

            partToggle.onValueChanged.AddListener((bool b) =>
            {
                PartToggleChange(b, partToggle);
            });
        }

        foreach (var onepair in mLeaderBoneControlMap.pairList)
        {
            foreach (var slider in onepair.sliderControls)
            {
                slider.onValueChanged.AddListener((float v) => { OnItemValueChanged(v, slider); });

                Debug.Log("slider Add listener:" + slider.name);
                SliderDrag sg = slider.gameObject.GetComponent<SliderDrag>();
                if(sg == null)
                {
                    sg = slider.gameObject.AddComponent<SliderDrag>();
                }
                sg.onStartDrag.AddListener(OnItemStartDrag);
                sg.onEndDrag.AddListener(OnItemEndDrag); 
            }

        }
        bool firstDetailToggle = true;
        foreach (var onepair in DeformUI.Instance.mLeaderBoneControlMap.pairList)
        {
            if (firstDetailToggle)
            {
                DetailToggleItemChange(true, onepair);
                firstDetailToggle = false;
            }
            onepair.toggleControl.onValueChanged.AddListener((bool b) =>
            {
                DetailToggleItemChange(b, onepair);
            });
        }

        ResetBtn.onClick.AddListener(OnReset);
        UndoBtn.onClick.AddListener(OnUndo); UndoBtn.interactable = false;
        SaveBtn.onClick.AddListener(OnSave);
    }
    public void NoneAreaTexture()
    {
        string TexturePath = "FaceAreaPNG/noneTex";
        ModelDataManager.Instance.ChangeFaceArea(TexturePath);
        mDelayFaceNoMaskTexCoroutine = null;
    }

    public void Reload()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            var pair = mLeaderBoneControlMap.FindPair(slider);
            if (pair != null)
            {
                SeSliderValueByLeaderBoneName(pair);
            }
        }
    }

    void OnItemValueChanged(float val, Slider item)
    {
        var pair = mLeaderBoneControlMap.FindPair(item);


        Vector4 scale4 = DeformLeaderBoneManager.Instance.GetOffsetScale(pair.leaderBoneName);

        if (item.name.EndsWith("_w"))
        {
            float scale = scale4.w;
            float newScale = Mathf.Pow(scale, val);
            Debug.Log("OnItemValueChanged w scale :" + scale + ": val :" + val);
            DeformLeaderBoneManager.Instance.SetLeaderBoneScale(pair.leaderBoneName, newScale);
            if (!string.IsNullOrWhiteSpace(pair.leaderBoneSymName) && pair.leaderBoneSymName != "null")
            {
                DeformLeaderBoneManager.Instance.SetLeaderBoneScale(pair.leaderBoneSymName, newScale);
            }
        }
        else
        {
            Vector3 offsetL = DeformLeaderBoneManager.Instance.GetLeaderBonePositonOffset(pair.leaderBoneName);
            Vector3 offsetR = Vector3.zero;
            if (!string.IsNullOrWhiteSpace(pair.leaderBoneSymName) && pair.leaderBoneSymName != "null")
            {
                offsetR = DeformLeaderBoneManager.Instance.GetLeaderBonePositonOffset(pair.leaderBoneSymName);
            }

            offsetL = DeformLeaderBoneManager.Instance.BoneToWorldTransformVector(pair.leaderBoneName,offsetL);
            offsetL = DeformLeaderBoneManager.Instance.WorldToBodyRootTransformVector(offsetL);


            offsetR = DeformLeaderBoneManager.Instance.BoneToWorldTransformVector(pair.leaderBoneSymName,offsetR);
            offsetR = DeformLeaderBoneManager.Instance.WorldToBodyRootTransformVector(offsetR);

            if (item.name.EndsWith("_x"))
            {
                float scale = scale4.x;

                offsetL.x = val * scale;
                offsetR.x = -val * scale;

            }
            else if (item.name.EndsWith("_y"))
            {
                float scale = scale4.y;
                offsetL.y = val * scale;
                offsetR.y = val * scale;

            }
            else if (item.name.EndsWith("_z"))
            {

                float scale = scale4.z;
                offsetL.z = val * scale;
                offsetR.z = val * scale;

            }

            offsetL = DeformLeaderBoneManager.Instance.BodyRootToWorldTransformVector(offsetL);
            offsetL = DeformLeaderBoneManager.Instance.WorldToBoneTransformVector(pair.leaderBoneName, offsetL);

            offsetR = DeformLeaderBoneManager.Instance.BodyRootToWorldTransformVector(offsetR);
            offsetR = DeformLeaderBoneManager.Instance.WorldToBoneTransformVector(pair.leaderBoneSymName, offsetR);



            DeformLeaderBoneManager.Instance.SetLeaderBonePosition(pair.leaderBoneName, offsetL);

            if (!string.IsNullOrWhiteSpace(pair.leaderBoneSymName) && pair.leaderBoneSymName != "null")
            {
                DeformLeaderBoneManager.Instance.SetLeaderBonePosition(pair.leaderBoneSymName, offsetR);
            }
        }
    }
    void OnItemStartDrag(Slider item)
    {
        Debug.Log("OnItemStartDrag : " + item.name);
        undoStatck.Push(DeformLeaderBoneManager.Instance.TakeSnapshot(currentPartToggleName,currentDetailToggleName));
        UndoBtn.interactable = true;

        var pair = mLeaderBoneControlMap.FindPair(item);

        if(item.name.EndsWith("_w"))
        {
            DeformLeaderBoneManager.Instance.SetWorking(false);
        }
        else
        {

            DeformLeaderBoneManager.Instance.StartEdit(pair.leaderBoneName);
            DeformLeaderBoneManager.Instance.StartEdit(pair.leaderBoneSymName);
        }

    }
    void OnItemEndDrag(Slider item)
    {
        Debug.Log("OnItemEndDrag : " + item.name);

        var pair = mLeaderBoneControlMap.FindPair(item);
        if (item.name.EndsWith("_w"))
        {

            DeformLeaderBoneManager.Instance.ResetBindPose();
            DeformLeaderBoneManager.Instance.SetWorking(true);
        }
        else
        {
            DeformLeaderBoneManager.Instance.StopEdit(pair.leaderBoneName);
            DeformLeaderBoneManager.Instance.StopEdit(pair.leaderBoneSymName);
        }
    }

    public void SeSliderValueByLeaderBoneName(string leaderBoneName)
    {
        SeSliderValueByLeaderBoneName(mLeaderBoneControlMap.FindPairByBoneName(leaderBoneName));
    }

    public void SeSliderValueByLeaderBoneName(LeaderBoneControlMap.LeaderBoneControlPair pair)
    {
        Vector4 scale4 = DeformLeaderBoneManager.Instance.GetOffsetScale(pair.leaderBoneName); 
    

        foreach (var slider in pair.sliderControls)
        {
          
            if (slider.name.EndsWith("_w"))
            {

                var scale = DeformLeaderBoneManager.Instance.GetLeaderBoneScaleOffset(pair.leaderBoneName);
                float v = 0f;
                if (scale4.w == 1)
                {
                    v = 0;
                }
                else
                {
                    v = Mathf.Log(scale, scale4.w);
                }
                Debug.Log(string.Format("SetItemValueByLeaderBoneName ScaleOffset {0} Setup {1} v {2} boneName {3}" ,scale,scale4.w,v, pair.leaderBoneName));
                slider.value = v;
            }
            else
            {
                var offset = DeformLeaderBoneManager.Instance.GetLeaderBonePositonOffset(pair.leaderBoneName);

                if (slider.name.EndsWith("_x"))
                {

                    slider.value = offset.x / scale4.x;

                }
                else if (slider.name.EndsWith("_y"))
                {

                    slider.value = offset.y / scale4.y;

                }
                else if (slider.name.EndsWith("_z"))
                {

                    slider.value = offset.z / scale4.z;

                }
            }
        }

    }

    IEnumerator DelayFaceNoMask()
    {
        yield return new WaitForSeconds(2);
        NoneAreaTexture();
    }

    void PartToggleChange(bool b ,Toggle t)
    {
        if(b)
        {
            currentPartToggleName = t.name;
            var firstToggle = mPartDetailToggleMap.detailFirstToggleDic[t.name];
            firstToggle.isOn = false;//先false，以免出现原来为true，不执行事件的情况
            firstToggle.isOn = true;
        }
        else
        {

        }

    }

    void DetailToggleItemChange(bool b, LeaderBoneControlMap.LeaderBoneControlPair pair)
    {
        if (b)
        {
            currentDetailToggleName = pair.ToggleName;

            ModelDataManager.Instance.ChangeFaceArea(pair.AreaTextureName);


            //左右对称，所以设置一次slider值即可
            DeformUI.Instance.SeSliderValueByLeaderBoneName(pair.leaderBoneName);

            if(mDelayFaceNoMaskTexCoroutine != null)
            {
                StopCoroutine(mDelayFaceNoMaskTexCoroutine);
                mDelayFaceNoMaskTexCoroutine = null;
            }
            mDelayFaceNoMaskTexCoroutine = StartCoroutine(DelayFaceNoMask());
        }
        else
        {

        }
    }


    void OnReset()
    {
        AvatarManager.Instance.ClearAction();
        ModelDataManager.Instance.ResetRole();
        undoStatck.Clear();
        UndoBtn.interactable = false;
    }
    void OnUndo()
    {
        if(undoStatck.Count > 0)
        {
            var snapshot = undoStatck.Pop();
            DeformLeaderBoneManager.Instance.RestoreSnapshot(snapshot);
            Reload();

            if(currentDetailToggleName != snapshot.toggleName)
            {
                var pair = mLeaderBoneControlMap.FindPairByToggleName(snapshot.toggleName);
                pair.toggleControl.isOn = true;
            }

            if(mPartDetailToggleMap.partToggleDic.ContainsKey(snapshot.partName))
            {
                mPartDetailToggleMap.partToggleDic[snapshot.partName].isOn = true;
            }
            

            if(undoStatck.Count == 0)
            {
                UndoBtn.interactable = false;
            }
        }
    }
    void OnSave()
    {

    }

}