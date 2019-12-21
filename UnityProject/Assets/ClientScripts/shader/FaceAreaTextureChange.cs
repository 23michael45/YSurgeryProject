using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAreaTextureChange : MonoBehaviour
{


    public void ChangeFaceArea(string TexturePath)
    {

        if (AppRoot.MainRole != null && AppRoot.MainRole.Rolein != null)
        {


            GameObject Role = AppRoot.MainRole.Rolein;
            GameObject head = Role.transform.Find("head001").gameObject;
            Material Facematerial = head.GetComponent<SkinnedMeshRenderer>().material;

            Debug.Log(TexturePath);
            Texture2D texture = Resources.Load(TexturePath) as Texture2D;

            Facematerial.SetTexture("_AreaTex", texture);
        }
    }

}

