using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ListModel
{
    public int ModelID;
    public string shotcutImage;
    public string TexturePath;
    public string ModelPath;
    public int Editable;
    public int Sex;
    public string BodyPath;
}


[Serializable]
public class ListCloth
{
    public int ClothID;
    public int Sex;
    public string shotcutImage;
    public string ClothAsset;
}

[Serializable]
public class ListMakeup
{


    public int MakeupID;
    public int Sex;
    public string shotcutImage;
    public string MakeupAsset;

}

[Serializable]
public class ListScence
{
    public int ScenceID;
    public string shotcutImage;
    public string ScenceAsset;

}






