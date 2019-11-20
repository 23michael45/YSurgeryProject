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

    public SendMessage SendMessage;


    public string Scencejson = "Scencejson";
    public string Rolejson = "Rolejson";
    public string Deformjson = "Deformjson";
    public string Ornamentjson = "Ornamentjson";
    public string modelstring = "modelstring";


    public void Start()
    {

        BoardButton.onClick.AddListener(delegate () { this.BoardButton_CLK(); });
        EditButton.onClick.AddListener(delegate () { this.EditButton_clk(); });
        CreatButton.onClick.AddListener(delegate () { this.CreatButton_CLK(); });

        SendMessage = new SendMessage();
    }



    public void EditButton_clk()
    {

        Edit_UI.SetActive(true);
        View_UI.SetActive(false);


        SendMessage.LoadRolejson(Rolejson);
        SendMessage.LoadScencejson(Scencejson);
        SendMessage.LoadDeformJson(Deformjson);
        SendMessage.LoadOrnamentjson(Ornamentjson);

    }



    public void BoardButton_CLK()
    {

        SendMessage.LoadOrnamentjson(modelstring);
    }



    public void CreatButton_CLK()
    {

        EnterButtonUI.SetActive(true);


    }

}
