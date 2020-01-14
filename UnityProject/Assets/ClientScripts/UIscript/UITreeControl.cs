using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITreeControl : MonoBehaviour
{
    UITreeContentItem[] mContentItems;
    UITreeSwitchItem[] mSwitchItems;
    
    void Start()
    {
        mContentItems = gameObject.GetComponentsInChildren<UITreeContentItem>(true);
        mSwitchItems = gameObject.GetComponentsInChildren<UITreeSwitchItem>(true);



        for(int i = 0; i< mSwitchItems.Length;i++)
        {
            mSwitchItems[i].SetParentTree(this, mContentItems[i]);
            mContentItems[i].SetParentTree(this, mSwitchItems[i]);
            
        }

        if(gameObject.GetComponent<VerticalLayoutGroup>() == null)
        {
            VerticalLayoutGroup verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childControlHeight = false;
            verticalLayoutGroup.childControlWidth = false;
            verticalLayoutGroup.childForceExpandHeight = false;
            verticalLayoutGroup.childForceExpandWidth = false;

        }


        //ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
        //contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
    
}
