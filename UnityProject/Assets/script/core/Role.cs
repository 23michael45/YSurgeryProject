using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public enum ClothPart
{
    Coat = 0,
    Tops=1,
    Trousers = 2,
    Underwear=3,
    Shoe=4,
    Hair = 5,
    Hat =6,
    Count
}

public enum MakeupPart
{
    Eye = 1,
    BaseColor = 2,
    EyeBrow = 3,
    EyeLash = 4,
    Eyeshadow = 5,
    FaceRed = 6,
    Mouth = 7,
    FaceTatoo = 8,
    BodyTatoo = 9,
    Finger = 10,
    Count
}


public enum Actpart {
    Act=1,
    Count
}

public class Role : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] parts;
    public Texture2D[] Makeupparts;

    public Transform rootBoneParent;
    //public NumberRollView _HeightScrollView;
    //public NumberRollView _WeightScrollView;
    public GameObject _head, _body,_arm, _eyes, _eyelash, _finger, Rolein;

    public GameObject _faceplan;

    [HideInInspector]
    public int RoleID = 0;

    Animator animator = null;




    // 面部骨骼点
    private Vector3 _face_eyeBall_Lf_joint1,_EyeEnd_L,_face_eyeBall_Rt_joint1,_EyeEnd_R,_face_bridge_joint1,_face_bridge_joint2,_face_bridge_Lf_joint1,
_face_bridge_Rt_joint1,_face_brow_Lf_joint1,_face_brow_Lf_joint2,_face_brow_Lf_joint3,_face_brow_Rt_joint1,_face_brow_Rt_joint2,_face_brow_Rt_joint3,
_face_calvaria_joint1,_face_cheek_Lf_joint1,_face_cheek_Lf_joint2,_face_cheek_Rt_joint1,_face_cheek_RT_joint2,_face_chin_Lf_joint07,_face_chin_Lf_joint7,
_face_chin_Lf_joint08,_face_chin_Lf_joint09,_face_chin_Rt_joint07,_face_chin_Rt_joint7,_face_chin_Rt_joint08,_face_chin_Rt_joint09,_face_eyeLids_Lf_joint1,
_face_eyeLids_Lf_joint2,_face_eyeLidsdown_Lf_joint1,_face_eyeLidsdown_Lf_joint2,_face_eyeLidsdown_Lf_joint3,_face_eyeLidsUp_Lf_joint1,
_face_eyeLidsUp_Lf_joint2,_face_eyeLidsUp_Lf_joint3,_face_eyeLids_Rt_joint1,_face_eyeLids_Rt_joint2,_face_eyeLidsdown_Rt_joint1,
_face_eyeLidsdown_Rt_joint2,_face_eyeLidsdown_Rt_joint3,_face_eyeLidsUp_Rt_joint1,_face_eyeLidsUp_Rt_joint2,_face_eyeLidsUp_Rt_joint3,
_face_forehead_joint1,_face_forehead_joint2,_face_forehead_Lf_joint1,_face_forehead_Lf_joint2,_face_forehead_Lf_joint3,_face_forehead_Lf_joint4,
_face_forehead_Lf_joint5,_face_forehead_Lf_joint6,_face_forehead_Rt_joint1,_face_forehead_Rt_joint2,_face_chin_joint1,_face_chin_joint2,
_face_chin_joint3,_face_chin_Lf_joint02,_face_chin_Lf_joint03,_face_chin_Lf_joint04,_face_chin_Lf_joint05,_face_chin_Lf_joint06,_face_chin_Rt_joint02,
_face_chin_Rt_joint03,_face_chin_Rt_joint04,_face_chin_Rt_joint05,_face_chin_Rt_joint06,_face_tooth_down_joint1,_face_tooth_down_joint2,
_face_tooth_down_joint3,_face_mouthLip_dn_joint1,_face_mouthLip_dn_joint2,_face_mouthLip_Lf_joint6,_face_mouthLip_Lf_joint7,_face_mouthLip_Rt_joint6,
_face_mouthLip_Rt_joint7,_face_mouthLip_Lf_joint1,_face_mouthLip_Lf_joint2,_face_mouthLip_Lf_joint3,_face_mouthLip_Rt_joint1,_face_mouthLip_Rt_joint2,
_face_mouthLip_Rt_joint3,_face_mouthLip_Lf_joint4,_face_mouthLip_Lf_joint5,_face_mouthLip_Rt_joint4,_face_mouthLip_Rt_joint5,_face_mouthLip_up_joint1,
_face_mouthLip_up_joint2,_face_nose_joint1,_face_nose_joint2,_face_nosewing_Lf_joint1,_face_nosewing_Lf_joint2,_face_nosewing_Lf_joint003,
_face_nosewing_Rt_joint1,_face_nosewing_Rt_joint2,_face_nosewing_Rt_joint003,_face_temple_Lf_joint1,_face_temple_Lf_joint2,_face_temple_Lf_joint3,
_face_temple_Rt_joint1,_face_temple_Rt_joint2,_face_temple_Rt_joint3,_face_tooth_up_joint1,_face_tooth_up_joint2,_face_tooth_up_joint3;


    private float pinguo_x = 0.000f;                   
    private float pinguo_y = 0.000f;                  
    private float pinguo_scal = 0.000f;                 
                                                       

    private float eye_zuoyou = 0.000f;
    private float eye_gaodi = 0.000f;
    private float eye_shenqian = 0.000f;

    private float eyebrow_zuoyou = 0.000f;
    private float eyebrow_gaodi = 0.000f;
    private float eyebrow_shenqian = 0.000f;


    private float mouth_gaodi = 0.000f;
    private float mouth_shenqian = 0.000f;
    private float mouth_sclKUan = 1.000f;
    private float mouth_sckHou = 1.000f;

    private float face_xiabaKUan = 0.000f;
    private float face_xiabachang = 0.000f;


    private float nose_kuandu = 1.00f;
    private float nose_Tingba = 1.00f;
    

  private float  SH_joint1,LJ_joint1;
  private float  SH_joint2,LJ_joint2;
  private float  SH_joint3,LJ_joint3;
  private float  SH_joint4,LJ_joint4;
  private float  QG_joint2;



    //定义皮肤色卡

    public   Vector3[] _skincolors = new Vector3[]{
       (new Vector3(31f, 0.38f, 0.82f)),       (new Vector3(28f, 0.39f, 0.84f)),       (new Vector3(28f, 0.39f, 0.83f)),
       (new Vector3(30f, 0.40f, 0.84f )),      (new Vector3(31f, 0.40f, 0.83f)),       (new Vector3(29f, 0.39f, 0.82f)),
       (new Vector3(28f, 0.39f ,0.83f)),       (new Vector3(28f, 0.40f, 0.82f)),       (new Vector3(30f, 0.40f, 0.82f)),
       (new Vector3(31f, 0.40f, 0.81f)),       (new Vector3(29f, 0.39f, 0.82f)),       (new Vector3(30f, 0.41f, 0.81f)),
       (new Vector3(29f, 0.41f, 0.82f)),       (new Vector3(30f, 0.41f, 0.82f)),       (new Vector3(30f, 0.40f, 0.82f)),
       (new Vector3(30f, 0.41f, 0.81f)),       (new Vector3(28f, 0.40f, 0.82f)),       (new Vector3(26f, 0.40f, 0.83f)),
       (new Vector3(29f, 0.40f, 0.80f)),       (new Vector3(28f, 0.41f, 0.80f)),       (new Vector3(29f, 0.41f, 0.80f)),
       (new Vector3(30f, 0.41f, 0.80f)),       (new Vector3(31f, 0.41f, 0.79f)),       (new Vector3(29f, 0.40f, 0.80f)),
       (new Vector3(24f, 0.39f, 0.82f)),       (new Vector3(23f, 0.39f, 0.82f)),       (new Vector3(22f, 0.38f, 0.82f)),
       (new Vector3(21f, 0.37f, 0.82f)),       (new Vector3(30f, 0.43f, 0.78f)),       (new Vector3(28f, 0.42f, 0.78f)),
       (new Vector3(29f, 0.42f, 0.78f)),       (new Vector3(29f, 0.42f, 0.77f)),       (new Vector3(30f, 0.42f, 0.78f)),
       (new Vector3(29f, 0.41f, 0.78f)),       (new Vector3(26f, 0.41f, 0.80f)),       (new Vector3(22f, 0.41f, 0.80f)),
       (new Vector3(21f, 0.41f, 0.81f)),       (new Vector3(20f, 0.40f, 0.81f)),       (new Vector3(29f, 0.44f, 0.76f)),
       (new Vector3(27f, 0.44f, 0.78f)),       (new Vector3(28f, 0.43f, 0.76f)),       (new Vector3(30f, 0.44f, 0.76f)),
       (new Vector3(31f, 0.44f, 0.76f)),       (new Vector3(29f, 0.44f, 0.77f)),       (new Vector3(25f, 0.43f, 0.78f)),
       (new Vector3(21f, 0.43f, 0.80f)),       (new Vector3(22f, 0.44f, 0.80f)),       (new Vector3(29f, 0.46f, 0.74f)),
       (new Vector3(30f, 0.46f, 0.73f)),       (new Vector3(29f, 0.46f, 0.74f)),       (new Vector3(25f, 0.45f, 0.75f)),
       (new Vector3(21f, 0.45f, 0.76f)),       (new Vector3(20f, 0.45f, 0.78f)),       (new Vector3(20f, 0.43f, 0.78f)),
       (new Vector3(29f, 0.51f, 0.72f)),       (new Vector3(27f, 0.49f, 0.72f)),       (new Vector3(28f, 0.49f, 0.72f)),
       (new Vector3(29f, 0.48f, 0.71f)),       (new Vector3(31f, 0.48f, 0.70f)),       (new Vector3(28f, 0.53f, 0.69f)),
       (new Vector3(27f, 0.51f, 0.69f)),       (new Vector3(29f, 0.50f, 0.68f)),       (new Vector3(28f, 0.52f, 0.68f)),
       (new Vector3(20f, 0.48f, 0.71f)),       (new Vector3(27f, 0.54f, 0.65f)),       (new Vector3(27f, 0.53f, 0.65f)),
       (new Vector3(28f, 0.51f, 0.64f)),       (new Vector3(30f, 0.52f, 0.64f)),       (new Vector3(27f, 0.54f, 0.66f)),
       (new Vector3(22f, 0.54f, 0.68f)),       (new Vector3(21f, 0.53f, 0.68f)),       (new Vector3(27f, 0.57f, 0.61f)),
       (new Vector3(27f, 0.56f, 0.62f)),       (new Vector3(29f, 0.54f, 0.59f)),       (new Vector3(31f, 0.53f, 0.59f)),
       (new Vector3(27f, 0.57f, 0.62f)),       (new Vector3(23f, 0.57f, 0.63f)),       (new Vector3(22f, 0.54f, 0.63f)),
       (new Vector3(27f, 0.57f, 0.55f)),       (new Vector3(27f, 0.57f, 0.62f)),       (new Vector3(23f, 0.57f, 0.63f)),
       (new Vector3(22f, 0.54f, 0.63f)),       (new Vector3(27f, 0.57f, 0.55f)),       (new Vector3(27f, 0.57f, 0.56f)),
       (new Vector3(30f, 0.57f, 0.54f)),       (new Vector3(27f, 0.56f, 0.55f)),       (new Vector3(23f, 0.56f, 0.57f)),
       (new Vector3(26f, 0.56f, 0.55f)),       (new Vector3(27f, 0.56f, 0.49f)),       (new Vector3(27f, 0.55f, 0.50f)),
       (new Vector3(27f, 0.56f, 0.49f)),       (new Vector3(22f, 0.54f, 0.49f)),       (new Vector3(23f, 0.57f, 0.46f)),
       (new Vector3(28f, 0.53f, 0.42f)),       (new Vector3(26f, 0.54f, 0.41f)),       (new Vector3(24f, 0.49f, 0.41f)),
       (new Vector3(23f, 0.49f, 0.41f)),       (new Vector3(22f, 0.50f, 0.41f)),       (new Vector3(30f, 0.48f, 0.35f)),
       (new Vector3(27f, 0.44f, 0.34f)),       (new Vector3(26f, 0.47f, 0.35f)),};

    private float zero_px;
    private float zero_py;

    private float scale_1024_to_model;
    private string mobilepath;
    private float curhigh_ST = 0;
    private float curweight_ST =0;





    public Dictionary<string, Transform> _bones;
    public Dictionary<string, Transform> bones
    {
        get
        {
            if (_bones == null)
            {
                _bones = new Dictionary<string, Transform>();
                Transform[] boneArray = rootBoneParent.GetComponentsInChildren<Transform>();
                for (int i = 0; i < boneArray.Length; ++i)
                {
                    _bones.Add(boneArray[i].name, boneArray[i]);
                }
            }
            return _bones;
        }
    }

    void Awake()
    {
        parts = new GameObject[(int)ClothPart.Count];

        Makeupparts = new Texture2D[(int)MakeupPart.Count];

    }



    //    public animatorDef GetanimatorDef(int id)
    //    {

    //        if (AppRoot.MainUser.isman)
    //        {
    //            if (TableMgr.Instance.M_animatorDic != null && TableMgr.Instance.M_animatorDic.ContainsKey(id))
    //            {
    //                return TableMgr.Instance.M_animatorDic[id];
    //            }

    //            return null;
    //        }
    //        else
    //        {
    //            if (TableMgr.Instance.W_animatorDic != null && TableMgr.Instance.W_animatorDic.ContainsKey(id))
    //            {
    //                return TableMgr.Instance.W_animatorDic[id];
    //            }

    //            return null;
    //        }
    //}
    //    RuntimeAnimatorController oldRuntimeController;    

    //    public void  SetAnimator(int id)
    //    {
    //        animatorDef def = GetanimatorDef(id);
    //        if (def != null)
    //        {
    //             BaseAssetLoader.Instance.StartLoadAsset(def.assetbundle, OnLoadAnimator);       

    //        }
    //    }

    //    public void OnLoadAnimator(object obj, object param)
    //    {
    //        var mAnimationClips = obj as AnimationClip;

    //       // Debug.Log(mAnimationClips.name);
    //        StartCoroutine(loadmotion(mAnimationClips));

    //    }
    //    public IEnumerator loadmotion(AnimationClip  mAnimationClips ) {

    //        SetRendererEnable(false);

    //        Animator animator = Rolein.GetComponent<Animator>();

    //        AnimatorOverrideController tOverrideController;
    //        if (oldRuntimeController == null)
    //        {
    //            oldRuntimeController = animator.runtimeAnimatorController;
    //            tOverrideController = new AnimatorOverrideController();
    //            tOverrideController.runtimeAnimatorController = animator.runtimeAnimatorController;          
    //            tOverrideController.name = "overridecontroller";
    //            animator.runtimeAnimatorController = tOverrideController;

    //        }
    //        else
    //        {
    //            tOverrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
    //        }
    //        animator.applyRootMotion = true;
    //        yield return 0;
    //        if (AppRoot.MainUser.isman)
    //        {
    //            tOverrideController["stand_man"] = mAnimationClips;
    //           // Debug.Log(mAnimationClips.length);
    //            yield return 0;
    //            animator.Play("stand_man");
    //            yield return 0;
    //            tOverrideController["stand_man"] = mAnimationClips;
    //            yield return 0;

    //        }
    //        else {
    //            tOverrideController["stand_woman"] = mAnimationClips;
    //           // Debug.Log(mAnimationClips.length);
    //            yield return 0;
    //            animator.Play("tpos_woman");
    //            yield return 0;
    //            tOverrideController["stand_woman"] = mAnimationClips;
    //            yield return 0;
    //        }      
    //        AppRoot.MainUser. ReloadBodylValue();

    //        SetRendererEnable(true);

    //        //Debug.Log(AppRoot.MainScene.MainRole.transform.localScale);
    //    }

    //    public void SetRendererEnable(bool b)
    //    {
    //        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
    //        foreach(Renderer r in renderers)
    //        {
    //            //r.enabled = b;
    //        }
    //    }

    //    public void animatorcontrol(bool isplay) {
    //        Animator animator = Rolein.GetComponent<Animator>();
    //        if (isplay)
    //        {
    //            animator.speed = 1;
    //            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
    //            animator.Play(info.fullPathHash, 0, _CurrentStopPercent);

    //        }
    //        else {
    //            animator.speed = 0;
    //            animator.StopPlayback();
    //        }

    //    }
    //    float _CurrentStopPercent;
    //    public void animatorpercent(float percent) {

    //        Animator animator = Rolein.GetComponent<Animator>();
    //        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

    //        animator.Play(info.fullPathHash, 0, percent);

    //        _CurrentStopPercent = percent;
    //        Debug.Log(percent);
    //        animator.speed = 0;
    //    }





    //    void RepalceBones(SkinFBX s1)
    //    {
    //        if (s1 == null || s1.Meshes == null)
    //        {
    //            Debug.LogError("ReplaceBones. Invalid SkinFBX!");
    //            return;
    //        }

    //        for (int iMesh = 0; iMesh < s1.Meshes.Length; ++iMesh)
    //        {
    //            Transform[] tr1s = s1.Meshes[iMesh].bones;
    //            List<Transform> newBones = new List<Transform>();

    //            for (int i = 0; i < tr1s.Length; ++i)
    //            {
    //                if (bones.ContainsKey(tr1s[i].name))
    //                    newBones.Add(bones[tr1s[i].name]);
    //            }
    //            s1.Meshes[iMesh].bones = newBones.ToArray();
    //        }
    //    }



    //  
    //  
    //    public string GetCurrentFaceImagePath()
    //    {
    //        string faceId = AppRoot.MainUser.currentFaceID;
    //        string faceimagename = AppRoot.MainUser.currentFaceID.ToString() + ".jpg";

    //#if UNITY_ANDROID
    //        string FaceImagePath = Application.persistentDataPath + "/faceimage/" + faceimagename;

    //#elif UNITY_IPHONE
    //        string FaceImagePath = Application.persistentDataPath + "/faceimage/" + faceimagename;
    //#else
    //   string FaceImagePath = Application.persistentDataPath + "/faceimage/" + faceimagename;         
    //#endif

    //        return FaceImagePath;
    //    }

    //    public void LoadFace() {
    //        int storeanimat = AppRoot.MainUser.CUR_animtor;

    //        if (AppRoot.MainUser.isman)
    //        {

    //            AppRoot.MainUser.CUR_animtor = 15100000;

    //        }
    //        else
    //        {
    //            AppRoot.MainUser.CUR_animtor = 25100000;

    //        }

    //        string FaceImagePath = GetCurrentFaceImagePath();       

    //        Texture2D tex = new Texture2D(0, 0);
    //        tex.LoadImage(File.ReadAllBytes(FaceImagePath));
    //        _head.GetComponent<Renderer>().material.SetTexture("_Facemap", tex);
    //        ResetBones();

    //        LoadBone("face_eyeBall_Lf_joint1"       );   
    //        LoadBone("EyeEnd_L"                     );   
    //        LoadBone("face_eyeBall_Rt_joint1"       );   
    //        LoadBone("EyeEnd_R"                     );   
    //        LoadBone("face_bridge_joint1"           );   
    //        LoadBone("face_bridge_joint2"           );   
    //        LoadBone("face_bridge_Lf_joint1"        );   
    //        LoadBone("face_bridge_Rt_joint1"        );   
    //        LoadBone("face_brow_Lf_joint1"          );   
    //        LoadBone("face_brow_Lf_joint2"          );   
    //        LoadBone("face_brow_Lf_joint3"          );   
    //        LoadBone("face_brow_Rt_joint1"          );   
    //        LoadBone("face_brow_Rt_joint2"          );   
    //        LoadBone("face_brow_Rt_joint3"          );   
    //        LoadBone("face_calvaria_joint1"         );   
    //        LoadBone("face_cheek_Lf_joint1"         );   
    //        LoadBone("face_cheek_Lf_joint2"         );   
    //        LoadBone("face_cheek_Rt_joint1"         );   
    //        LoadBone("face_cheek_RT_joint2"         );   
    //        LoadBone("face_chin_Lf_joint07"         );   
    //        LoadBone("face_chin_Lf_joint7"          );   
    //        LoadBone("face_chin_Lf_joint08"         );   
    //        LoadBone("face_chin_Lf_joint09"         );   
    //        LoadBone("face_chin_Rt_joint07"         );   
    //        LoadBone("face_chin_Rt_joint7"          );   
    //        LoadBone("face_chin_Rt_joint08"         );   
    //        LoadBone("face_chin_Rt_joint09"         );   
    //        LoadBone("face_eyeLids_Lf_joint1"       );   
    //        LoadBone("face_eyeLids_Lf_joint2"       );   
    //        LoadBone("face_eyeLidsdown_Lf_joint1"   );   
    //        LoadBone("face_eyeLidsdown_Lf_joint2"   );   
    //        LoadBone("face_eyeLidsdown_Lf_joint3"   );   
    //        LoadBone("face_eyeLidsUp_Lf_joint1"     );   
    //        LoadBone("face_eyeLidsUp_Lf_joint2"     );   
    //        LoadBone("face_eyeLidsUp_Lf_joint3"     );   
    //        LoadBone("face_eyeLids_Rt_joint1"       );   
    //        LoadBone("face_eyeLids_Rt_joint2"       );   
    //        LoadBone("face_eyeLidsdown_Rt_joint1"   );   
    //        LoadBone("face_eyeLidsdown_Rt_joint2"   );   
    //        LoadBone("face_eyeLidsdown_Rt_joint3"   );   
    //        LoadBone("face_eyeLidsUp_Rt_joint1"     );   
    //        LoadBone("face_eyeLidsUp_Rt_joint2"     );   
    //        LoadBone("face_eyeLidsUp_Rt_joint3"     );   
    //        LoadBone("face_forehead_joint1"         );   
    //        LoadBone("face_forehead_joint2"         );   
    //        LoadBone("face_forehead_Lf_joint1"      );   
    //        LoadBone("face_forehead_Lf_joint2"      );   
    //        LoadBone("face_forehead_Lf_joint3"      );   
    //        LoadBone("face_forehead_Lf_joint4"      );   
    //        LoadBone("face_forehead_Lf_joint5"      );   
    //        LoadBone("face_forehead_Lf_joint6"      );   
    //        LoadBone("face_forehead_Rt_joint1"      );   
    //        LoadBone("face_forehead_Rt_joint2"      );   
    //        LoadBone("face_chin_joint1"             );   
    //        LoadBone("face_chin_joint2"             );   
    //        LoadBone("face_chin_joint3"             );   
    //        LoadBone("face_chin_Lf_joint02"         );   
    //        LoadBone("face_chin_Lf_joint03"         );   
    //        LoadBone("face_chin_Lf_joint04"         );   
    //        LoadBone("face_chin_Lf_joint05"         );   
    //        LoadBone("face_chin_Lf_joint06"         );   
    //        LoadBone("face_chin_Rt_joint02"         );   
    //        LoadBone("face_chin_Rt_joint03"         );   
    //        LoadBone("face_chin_Rt_joint04"         );   
    //        LoadBone("face_chin_Rt_joint05"         );   
    //        LoadBone("face_chin_Rt_joint06"         );   
    //        LoadBone("face_tooth_down_joint1"       );
    //        LoadBone("face_tooth_down_joint2"       );   
    //        LoadBone("face_tooth_down_joint3"       );   
    //        LoadBone("face_mouthLip_dn_joint1"      );   
    //        LoadBone("face_mouthLip_dn_joint2"      );   
    //        LoadBone("face_mouthLip_Lf_joint6"      );   
    //        LoadBone("face_mouthLip_Lf_joint7"      );   
    //        LoadBone("face_mouthLip_Rt_joint6"      );   
    //        LoadBone("face_mouthLip_Rt_joint7"      );   
    //        LoadBone("face_mouthLip_Lf_joint1"      );   
    //        LoadBone("face_mouthLip_Lf_joint2"      );   
    //        LoadBone("face_mouthLip_Lf_joint3"      );   
    //        LoadBone("face_mouthLip_Rt_joint1"      );   
    //        LoadBone("face_mouthLip_Rt_joint2"      );   
    //        LoadBone("face_mouthLip_Rt_joint3"      );   
    //        LoadBone("face_mouthLip_Lf_joint4"      );   
    //        LoadBone("face_mouthLip_Lf_joint5"      );   
    //        LoadBone("face_mouthLip_Rt_joint4"      );   
    //        LoadBone("face_mouthLip_Rt_joint5"      );   
    //        LoadBone("face_mouthLip_up_joint1"      );   
    //        LoadBone("face_mouthLip_up_joint2"      );   
    //        LoadBone("face_nose_joint1"             );   
    //        LoadBone("face_nose_joint2"             );   
    //        LoadBone("face_nosewing_Lf_joint1"      );   
    //        LoadBone("face_nosewing_Lf_joint2"      );   
    //        LoadBone("face_nosewing_Lf_joint003"    );   
    //        LoadBone("face_nosewing_Rt_joint1"      );   
    //        LoadBone("face_nosewing_Rt_joint2"      );   
    //        LoadBone("face_nosewing_Rt_joint003"    );   
    //        LoadBone("face_temple_Lf_joint1"        );   
    //        LoadBone("face_temple_Lf_joint2"        );   
    //        LoadBone("face_temple_Lf_joint3"        );   
    //        LoadBone("face_temple_Rt_joint1"        );   
    //        LoadBone("face_temple_Rt_joint2"        );   
    //        LoadBone("face_temple_Rt_joint3"        );   
    //        LoadBone("face_tooth_up_joint1"         );   
    //        LoadBone("face_tooth_up_joint2"         );
    //        LoadBone("face_tooth_up_joint3");

  
    //        OnPhotoGetClick();

    //        GameObject.Find("UI").GetComponent<LoadScene>()._sussface = true;
    //        GameObject.Find("UI").GetComponent<LoadScene>().facesuss();

    //        bool Remodelbool, Refacebool;
    //        Remodelbool = GameObject.Find("UI").GetComponent<ModelMgr>().Remodelbool;
    //        Refacebool = GameObject.Find("UI").GetComponent<ModelMgr>().Refacebool;

    //        AppRoot.MainUser.ReloadFaceValue();

    //        if (Remodelbool)
    //        {

    //            GameObject.Find("UI").GetComponent<UIcontrol>().ReModelfinish();
    //        }

    //        if (Refacebool)
    //        {
    //            GameObject.Find("UI").GetComponent<UIcontrol>().ReModelfinish();

    //        }

    //        GameObject.Find("UI").GetComponent<ModelMgr>().Remodelbool = false;
    //        GameObject.Find("UI").GetComponent<ModelMgr>().Refacebool = false;


    //          AppRoot.MainUser.CUR_animtor= storeanimat;

    //    }
    //    public void LoadBone(string bones) {           
    //       _bones [bones].localPosition = AppRoot.MainUser.facelandmark[bones];
    //    }   






    //    }
    //    void ChangposSize(Transform bons, Vector3 pos ) {

    //        Vector3 pos0= bons.localPosition;

    //        string pos00 = pos0.ToString("f4");

    //        Vector3 pos1 = new Vector3 ( pos.x * 0.01f,pos.y * 0.01f, pos.z * 0.01f)   ;

    //        string pos11 = pos1.ToString("f4");

    //        bons.localPosition = pos1;
    ///*
    //        if (pos00 != pos11) {
    //            Debug.Log(bons);
    //        }
    //    */                  
    //    }
    //   




}