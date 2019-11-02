using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActiveScene : MonoBehaviour
{

    public  GameObject fistpage ;  
    public  GameObject maincamera ;
    public GameObject Stage;
    public Text loadText;
    public GameObject View_UI;
    public GameObject Edit_UI;



    public  void  closeFirstpage() {       
        
        maincamera.SetActive(true );
        fistpage.SetActive(false);

       

    }
    
       

    public  void openFirstpage()
    {
        DelData();
        //maincamera.SetActive(false);
        //fistpage.SetActive(true );

        View_UI.SetActive(true);
        Edit_UI.SetActive(false);

        loadText.text = "请上传照片";
    }


    void DelData() {
                
        Stage.transform .rotation = Quaternion.Euler(0, 0, 0);
        Stage.transform.position = new Vector3(0, 0, 0);
        maincamera.GetComponent<FreeView>().OnCameraHeadBtnClk();


        try
        {
            GameObject face = Stage.transform.Find("face").gameObject;
            DestroyImmediate(face);

        }
        catch {

            Debug.Log("场景无人模特");
        }

      



    }




}
