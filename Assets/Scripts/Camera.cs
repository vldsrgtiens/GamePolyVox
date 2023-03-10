using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject obj;

    public GameObject eye;
    // Start is called before the first frame update
    void Start()
    {
        obj=GameObject.Find("Alien");
        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.tag == "shassi")
            {
                eye = child.gameObject;
                
            }
        }

        if (eye != null)
        {
            this.transform.SetParent(eye.transform, false);
            this.transform.localPosition = Vector3.zero;
            print("set parent camera");
            
        }
        else
        {
            print("eye not found");
        }
    }

    
}
