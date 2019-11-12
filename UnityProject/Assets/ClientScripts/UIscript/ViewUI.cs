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



    public void Start()
    {

        EditButton.onClick.AddListener(delegate(){
            this.EditButton_clk();
        });
    }



    public void EditButton_clk() {

        Edit_UI.SetActive(true);
        View_UI.SetActive(false);

    }







}
