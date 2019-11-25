using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAreaTextureChange : MonoBehaviour
{
    
    
    public  void ChangeFaceArea(string TexturePath ){

        GameObject Role = AppRoot.MainRole.Rolein;
        Material Facematerial = Role.GetComponent<MeshRenderer>().material;

        Texture2D texture = Resources.Load(TexturePath) as Texture2D;
        Facematerial.SetTexture("_AreaTex", texture);
    }
       

}

