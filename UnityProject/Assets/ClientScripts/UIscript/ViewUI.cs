using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewUI : MonoBehaviour
{


    public Button EditButton;
    public Button BoardButton;
    public Button CreatButton;

    public GameObject Edit_UI;
    public GameObject View_UI;
    public GameObject EnterButtonUI;


    public Button okbutton;



    public SendMessage SendMessage;





    public string Scencejson = "Scencejson";
    public string Rolejson = "Rolejson";
    public string Deformjson = "Deformjson";
    public string Ornamentjson = "Ornamentjson";
    public string modelstring = "modelstring";


    public void Start()
    {

        BoardButton.onClick.AddListener(BoardButton_CLK);
        EditButton.onClick.AddListener(EditButton_clk);
        CreatButton.onClick.AddListener(CreatButton_CLK);

        SendMessage = new SendMessage();

        okbutton.onClick.AddListener(okbutton_clk);


    }


    public void okbutton_clk() {

        okbutton.interactable = false;

    }





    public void EditButton_clk()
    {

        Edit_UI.SetActive(true);
        View_UI.SetActive(false);
        
        SendMessage.LoadEditMode(modelstring);
      

    }



    public void BoardButton_CLK()
    {

        SendMessage.LoadViewModel(modelstring);
    }



    public void CreatButton_CLK()
    {

        EnterButtonUI.SetActive(true);


    }

}
