using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ActiveScene : MonoSingleton<ActiveScene>
{

    public GameObject fistpage ;  
    public GameObject maincamera ;
    public GameObject Stage;
    public Text loadText;
    public GameObject View_UI;
    public GameObject Edit_UI;

    public Button SaveButton;
    public Button BackButton;
    public GameObject Backwindow;
        
    public Button win_saveButton, win_saveAsButton, win_CancleButton, win_AbortButton;

    public LoadManager loadManager;

    

    public void Start()
    {

        BackButton.onClick.AddListener(BackFirstpage);
        SaveButton.onClick.AddListener(SaveButton_clk);
               
        win_saveButton.onClick.AddListener(win_saveButton_clk);
        win_saveAsButton.onClick.AddListener(win_saveAsButton_clk);
        win_CancleButton.onClick.AddListener(win_CancleButton_clk);
        win_AbortButton.onClick.AddListener(win_AbortButton_clk);

        loadManager = new LoadManager();
            }

    public  void  closeFirstpage() {       
        
        maincamera.SetActive(true );
        fistpage.SetActive(false);

    }
    
       

    public  void BackFirstpage()    {
#if AS_NATIVE_PROJECT
        
        Edit_UI.SetActive(false);
        Edit_UI.GetComponent<FouseFacePart>().NoneAreaTexture();
        

#else

        Backwindow.SetActive(true);
#endif    


        }






   public void win_saveButton_clk     (){

        loadText.text = "请上传照片";

        loadManager.SaveScencejson();        
        loadManager.SaveDeformJson();
        loadManager.SaveOrnamentjson();

        View_UI.SetActive(true);
        Edit_UI.SetActive(false);
        Backwindow.SetActive(false);
        Edit_UI.GetComponent<FouseFacePart>().NoneAreaTexture();

      
    }
    public void win_saveAsButton_clk   (){
        

        loadText.text = "请上传照片";
        loadManager.SaveScencejson();
        loadManager.SaveDeformAs();
        loadManager.SaveOrnamentjson();
        View_UI.SetActive(true);
        Edit_UI.SetActive(false);
        Backwindow.SetActive(false);
        Edit_UI.GetComponent<FouseFacePart>().NoneAreaTexture();
    }
    public void win_CancleButton_clk() {

        Backwindow.SetActive(false );
    }


    public void win_AbortButton_clk() {

        View_UI.SetActive(true);
        Edit_UI.SetActive(false);
        Backwindow.SetActive(false);

        Edit_UI.GetComponent<FouseFacePart>().NoneAreaTexture();
    }






    public void SaveButton_clk() {

        LoadManager loadManager = new LoadManager();

        loadManager.SaveScencejson();
        loadManager.SaveRolejson();        
        loadManager.SaveDeformJson();
        loadManager.SaveOrnamentjson();
    }
    







   }





