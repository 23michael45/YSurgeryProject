using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITreeContentItem : MonoBehaviour
{
    UITreeControl mParentTree;
    UITreeSwitchItem mSwitchItem;

    public void SetParentTree(UITreeControl parent, UITreeSwitchItem sitem)
    {
        mParentTree = parent;
        mSwitchItem = sitem;
    }

    private void Start()
    {
        //if (gameObject.GetComponent<ContentSizeFitter>() == null)
        //{
        //    ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
        //    contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        //}

        //if (gameObject.GetComponent<VerticalLayoutGroup>() == null)
        //{
        //    VerticalLayoutGroup verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
        //    verticalLayoutGroup.childControlHeight = false;
        //    verticalLayoutGroup.childControlWidth = false;
        //    verticalLayoutGroup.childForceExpandHeight = false;
        //    verticalLayoutGroup.childForceExpandWidth = false;
        //    verticalLayoutGroup.spacing = 10;
        //}
    }

}
