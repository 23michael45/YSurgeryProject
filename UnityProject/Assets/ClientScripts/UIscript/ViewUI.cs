﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewUI : MonoBehaviour
{
    public static ViewUI Instance;


    public Button EditButton;
    public Button BoardButton;
    public Button CreatButton;
    public Button ReportButton;

    public GameObject Edit_UI;
    public GameObject View_UI;
    public GameObject EnterButtonUI;
    public GameObject ReportItem;

    public Button okbutton;


    public SendMessage SendMessage;
    public ReadTable readTable;


    public string Scencejson;
    public string Rolejson;
    public string Deformjson;
    public string Ornamentjson;
    public string modelstring;
    public string user;

    private void Awake()
    {
        Instance = this;
#if AS_NATIVE_PROJECT

        gameObject.SetActive(false);
    
#endif
    }

    public void Start()
    {
        readTable = new ReadTable();
        Scencejson = readTable.ReadEnvironmentJson();
        Rolejson = readTable.ReadRoleJson();
        Deformjson = readTable.ReadDeformJson();
        Ornamentjson = readTable.ReadOrnamentJson();
        modelstring = readTable.ReadModelJson();

        // Debug.Log(modelstring.ToString());

        BoardButton.onClick.AddListener(BoardButton_CLK);
        EditButton.onClick.AddListener(EditButton_clk);
        CreatButton.onClick.AddListener(CreatButton_CLK);
        ReportButton.onClick.AddListener(ReportButton_ClK);

        SendMessage = new SendMessage();

        okbutton.onClick.AddListener(okbutton_clk);


    }


    public void okbutton_clk()
    {

        okbutton.interactable = false;

    }





    public void EditButton_clk()
    {

        Edit_UI.SetActive(true);
        View_UI.SetActive(false);

        // SendMessage.LoadEditMode(modelstring);


    }

    public void BoardButton_CLK()
    {

        SendMessage.LoadViewModel(modelstring);
    }



    public void CreatButton_CLK()
    {

        EnterButtonUI.SetActive(true);


    }
    public void ReportButton_ClK() {

        ReportItem.SetActive(true);
    }



}
