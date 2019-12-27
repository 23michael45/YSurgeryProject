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
    private float step=100f;
    private float screenret;


    private void Start()
    {
        screenret = UnityEngine.Screen.height / 1450f;

        Editpos0 = EditFaceSwitch.transform.position;
        
        Makeup_pos0 = MakeupSwitch.transform.position;
        Cloth_pos0 = ClothsSwitch.transform.position;
        Ornament_pos0 = OrnamentsSwitch.transform.position;
        Act_pos0 = ActSwitch.transform.position;
       
        var  rect_main  = EditFaceSwitch.transform.GetComponent<RectTransform>();
        distane = rect_main.sizeDelta.y * (1 + (step / 66f)) * screenret;

        //Debug.Log(Editpos0);
        //Debug.Log(screenret);
        //Debug.Log(distane);


    }





    public void EditFaceSwitchclick()
    {

        if (EditFaceSwitch.isOn)
        {

            MakeupSwitch.transform.position = Makeup_pos0;
            ClothsSwitch.transform.position = Cloth_pos0;
            OrnamentsSwitch.transform.position = Ornament_pos0;
            ActSwitch.transform.position = Act_pos0;
        }

        else
        {
            
        }

    }







    public void MakeupSwitchclick()
    {
      
                if (MakeupSwitch.isOn)
        {
         MakeupSwitch.transform.position = Editpos0- new Vector3(0, distane,0);

           float  downpos0 =  MakeupArea.transform.GetComponent<RectTransform>().sizeDelta.y * screenret; 

            MakeupArea.transform.position=  Editpos0 - new Vector3(0, 2*distane+ downpos0/2, 0);
            ClothsSwitch.transform.position = Editpos0 - new Vector3(0, 2 * distane + downpos0+  distane, 0);
            OrnamentsSwitch.transform.position = Editpos0 - new Vector3(0, distane + downpos0 + 3*distane, 0);
            ActSwitch.transform.position = Editpos0 - new Vector3(0, distane + downpos0 + 4*distane, 0);

        }

        else
        {
    

        }

    }




    public void ClothsSwitchclick()
    {
       
       if (ClothsSwitch.isOn)
        {

            MakeupSwitch.transform.position = Editpos0 - new Vector3(0, distane, 0);
            ClothsSwitch.transform.position = Editpos0 - new Vector3(0,2*distane, 0);

            float downpos0 = ClothsArea.transform.GetComponent<RectTransform>().sizeDelta.y * screenret;
            ClothsArea.transform.position = Editpos0 - new Vector3(0,3 * distane + downpos0 / 2, 0);
            OrnamentsSwitch.transform.position = Editpos0 - new Vector3(0,3* distane + downpos0 + distane, 0);
            ActSwitch.transform.position = Editpos0 - new Vector3(0,3* distane + downpos0 + 2* distane, 0);


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
            MakeupSwitch.transform.position = Editpos0 - new Vector3(0, distane, 0);
            ClothsSwitch.transform.position = Editpos0 - new Vector3(0, 2 * distane, 0);
            OrnamentsSwitch.transform.position = Editpos0 - new Vector3(0, 3 * distane, 0);

            float downpos0 = OrnamentsArea.transform.GetComponent<RectTransform>().sizeDelta.y * screenret;
            OrnamentsArea.transform.position = Editpos0 - new Vector3(0,4 * distane + downpos0 / 2, 0);


            ActSwitch.transform.position = Editpos0 - new Vector3(0, 4 * distane + downpos0 +  distane, 0);

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
            MakeupSwitch.transform.position = Editpos0 - new Vector3(0, distane, 0);
            ClothsSwitch.transform.position = Editpos0 - new Vector3(0, 2 * distane, 0);
            OrnamentsSwitch.transform.position = Editpos0 - new Vector3(0, 3 * distane, 0);
            ActSwitch.transform.position = Editpos0 - new Vector3(0,4 * distane, 0);

            float downpos0 = ActArea.transform.GetComponent<RectTransform>().sizeDelta.y * screenret;
            ActArea.transform.position = Editpos0 - new Vector3(0,5 * distane + downpos0 / 2, 0);


        }

        else
        {
           // ActSwitch.transform.position = new Vector3(x0, Act_pos0, z0);

        }

    }





























}
