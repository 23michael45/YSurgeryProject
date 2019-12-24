using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;




[Serializable]
public class User_Environment
{
    public int Background;
    public int Stage;
    public int Light;   

}// 环境设置





[Serializable]
public class User_Ornaments
{
    public User_Ornaments()
    {

        makeup = new Makeup();
        cloth = new Cloth();
       ornament = new Ornament();
    }

    public int ModelID;



    [Serializable]
    public class Makeup
    {
        public int EyeBrow, EyeShadow, EyeLash, Pupil, Foundation, Shadow, Lip, FaceTatoo, BodyTatoo;

    }

    [Serializable]
    public class Cloth
    {
        public int UpWear, DownWear, Coat, Suit, UnderWear, Shoe, Ornament;
    }

    [Serializable]
    public class Ornament
    {
        public int Hair, Glass, Hats, Jewellery;
    }

    public int Act;
    public Makeup makeup;
    public Cloth cloth;
    public Ornament ornament;

}




[Serializable]
public class User_Model
{   
    public User_Model()
    {
        role = new RoleJson();
        deform = new DeformJson();
        Ornament = new User_Ornaments();
    }



    public string ModelID; 
    public string shotcutImage;

    public string TexturePath;
    public string ModelPath;
    public int  Editable;

    public RoleJson role;
    public DeformJson deform;
    public User_Ornaments Ornament;

}


[Serializable]
public class User_Profile
{
    public User_Model model;
    public User_Environment environment;

}

[Serializable]
public class User
{
    public User_Profile currentProfile= new User_Profile();
    public User_Model currentModel= new User_Model();

    public bool crrrentEditable;

    public string currentmodelID, currentFaceID, currentmodelName;

    


    public void Init( )
    {
     
        currentProfile = new User_Profile();
        currentModel = new User_Model();
             
    }



    /// <summary>
    //face。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
    /// </summary>

    public string shotcutImage
    {
        get { return currentModel.shotcutImage; }
        set { currentModel.shotcutImage = value; }
     }
    
    public string  TexturePath
    {
        get { return currentModel.TexturePath; }
        set { currentModel.TexturePath = value; }
    }

    public string ModelPath
    {
        get { return currentModel.ModelPath; }
        set { currentModel.ModelPath = value; }
    }
    
    public int Editable
    {
        get { return currentModel.Editable; }
        set { currentModel.Editable = value; }
    }
    


}


