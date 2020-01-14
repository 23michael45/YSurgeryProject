using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITreeSwitchItem : MonoBehaviour
{
    UITreeControl mParentTree;
    UITreeContentItem mContentItem;

    public GameObject mToggleObject;

    public void SetParentTree(UITreeControl parent, UITreeContentItem content)
    {
        mParentTree = parent;
        mContentItem = content;
    }


    void Start()
    {
        gameObject.GetComponent<Toggle>().onValueChanged.AddListener(OnToggle);

    }
    private void OnDestroy()
    {
        gameObject.GetComponent<Toggle>().onValueChanged.RemoveListener(OnToggle);

    }

    void　OnToggle(bool b)
    {
        mContentItem.gameObject.SetActive(b);
        mToggleObject.SetActive(b);
        if (b)
        {

        }
        else
        {

        }
    }
}
