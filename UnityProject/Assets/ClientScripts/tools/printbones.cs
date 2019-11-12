using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printbones : MonoBehaviour
{
    public Transform roofbone;
    public List<Transform> bones;
    private List<string> bonenames;

    private string bonename;



    private void Start()
    {

        Transform[] father = roofbone.gameObject.GetComponentsInChildren<Transform>();
           



            foreach (Transform child in father) {
            bones.Add(child);

            //jsonstring(child);

            if (child.gameObject.name != "Background" && child.gameObject.name != "Checkmark")
            {
                namestring(child);
            }




        }

        //Debug.Log(bones.Count);
        //Debug.Log(bonename);


    }



    void jsonstring( Transform child  ) {
        
        string x = "\"x\":" + child.position.x.ToString() + ",";
        string y = "\"y\":" + child.position.y.ToString() + ",";
        string z = "\"z\":" + child.position.z.ToString();

        bonename = bonename + "\"" + child.gameObject.name + "\"" + ":" + "{" + x + y + z + "},";
    }



    void UIstring(Transform child) {

        bonename = bonename + "\"" + child.gameObject.name + "\"" + ":0," ;



    }


    void namestring(Transform child)
    {

        bonename = bonename + child.gameObject.name + ",";



    }





}



