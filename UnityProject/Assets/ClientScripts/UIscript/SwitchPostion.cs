using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SwitchPostion : MonoBehaviour
{
    public Toggle EditFaceSwitch, MakeupSwitch, ClothsSwitch, OrnamentsSwitch, ActSwitch;

    public GameObject EditFaceArea, MakeupArea, ClothsArea, OrnamentsArea, ActArea;

    private Vector3  Editpos0,  Makeup_pos0, Makeup_pos1,

        Cloth_pos0, Cloth_pos1, Ornament_pos0, Ornament_pos1, Act_pos0, Act_pos1;

      

    private float distane;
    private float step=10f;
    private float screenret;
    float downpos0 = 33;
    private void Start()
    {
        screenret = 750f / 1450f;

        //Editpos0 = EditFaceSwitch.transform.GetComponent<RectTransform>().position;       
        //Makeup_pos0 = MakeupSwitch.transform.GetComponent<RectTransform>().position;
        //Cloth_pos0 = ClothsSwitch.transform.GetComponent<RectTransform>().position;
        //Ornament_pos0 = OrnamentsSwitch.transform.GetComponent<RectTransform>().position;
        //Act_pos0 = ActSwitch.transform.GetComponent<RectTransform>().position;

        Editpos0 = new Vector3(88, -300, 0);
        Makeup_pos0 = new Vector3(88, -792, 0);
        Cloth_pos0 = new Vector3(88, -862, 0);
        Ornament_pos0 = new Vector3(88, -932, 0);
        Act_pos0 = new Vector3(88, -1002, 0);

       // var rect_main  = EditFaceSwitch.transform.GetComponent<RectTransform>();
        // distane = rect_main.sizeDelta.y * (1 + (step / 66f)) * screenret;

        distane = 66f;
        Debug.Log(Editpos0);
        Debug.Log(screenret);
        Debug.Log(distane);


    }





    public void EditFaceSwitchclick()
    {

        if (EditFaceSwitch.isOn)
        {

            MakeupSwitch.transform.GetComponent<RectTransform>().position = Makeup_pos0;
            ClothsSwitch.transform.GetComponent<RectTransform>().position = Cloth_pos0;
            OrnamentsSwitch.transform.GetComponent<RectTransform>().position = Ornament_pos0;
            ActSwitch.transform.GetComponent<RectTransform>().position = Act_pos0;




         
        }

        else
        {
            
        }

    }







    public void MakeupSwitchclick()
    {
      
                if (MakeupSwitch.isOn)
        {
            MakeupSwitch.transform.GetComponent<RectTransform>().position = Editpos0- new Vector3(0, distane,0);

            MakeupArea.transform.GetComponent<RectTransform>().position =  Editpos0 - new Vector3(0, 2*distane+ downpos0/2, 0);
            ClothsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,3 * distane + downpos0, 0);
            OrnamentsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, downpos0 + 4*distane, 0);
            ActSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, downpos0 + 5*distane, 0);

            Debug.Log(MakeupSwitch.transform.GetComponent<RectTransform>().position);
            Debug.Log(MakeupArea.transform.GetComponent<RectTransform>().position);
            Debug.Log(ClothsSwitch.transform.GetComponent<RectTransform>().position);




        }

        else
        {
    

        }

    }




    public void ClothsSwitchclick()
    {
       
       if (ClothsSwitch.isOn)
        {

            MakeupSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, distane, 0);
            ClothsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,2*distane, 0);

          
            ClothsArea.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,3 * distane + downpos0 / 2, 0);
            OrnamentsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,3* distane + downpos0 + distane, 0);
            ActSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,3* distane + downpos0 + 2* distane, 0);


        }

        else
        {
           // ClothsSwitch.transform.position = new Vector3(x0, Cloth_pos0, z0); 

        }

    }



    public void OrnamentsSwitchclick()
    {
       

        if (OrnamentsSwitch.isOn)
        {
            MakeupSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, distane, 0);
            ClothsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, 2 * distane, 0);
            OrnamentsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, 3 * distane, 0);

         
            OrnamentsArea.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,4 * distane + downpos0 / 2, 0);


            ActSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, 4 * distane + downpos0 +  distane, 0);

        }

        else
        {

          //  OrnamentsSwitch.transform.position = new Vector3(x0, Ornament_pos0, z0);
        }

    }




    public void ActSwitchclick()
    {

    

        if (ActSwitch.isOn)
        {
            MakeupSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, distane, 0);
            ClothsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, 2 * distane, 0);
            OrnamentsSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0, 3 * distane, 0);
            ActSwitch.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,4 * distane, 0);

           
            ActArea.transform.GetComponent<RectTransform>().position = Editpos0 - new Vector3(0,5 * distane + downpos0 / 2, 0);


        }

        else
        {
           // ActSwitch.transform.position = new Vector3(x0, Act_pos0, z0);

        }

    }





























}
