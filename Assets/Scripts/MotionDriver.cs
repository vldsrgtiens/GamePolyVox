using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDriver : MonoBehaviour
{
    private GameObject obj;
    private GameObject mainCamera;//MainCamera
    private GameObject eye;
    private MotionDriver objMotionDriver;
    private MotionModule _myMotionModule;

    public bool CameraHere = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        _myMotionModule = GetComponent<MotionModule>();
        if (_myMotionModule == null)
        {
            print("ATTANTION: MotionModule отсутствует");
        }
        if (this.tag == "Hero")
        {
            print("My name is "+transform.name);
            SetCameraTo("Hero");

        }
    }

    void Update()
    {
        if (CameraHere)
        {
            if (Input.GetKey(KeyCode.W) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting) {
                _myMotionModule.MoveForward( transform.position+transform.forward*(HexMetrics.innerRadius*2));
            }
            if (Input.GetKey(KeyCode.D) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting) {
                _myMotionModule.RotateToRight();
            }
            if (Input.GetKey(KeyCode.A) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting) {
                _myMotionModule.RotateToLeft();
            }
            if (Input.GetKey(KeyCode.Alpha1) && this.tag=="Hero") {
                SetCameraTo("Robot_1");
                //Escape  
            }

            if (Input.GetKey(KeyCode.Escape) && this.tag != "Hero")
            {
                SetCameraTo("Hero");
            }
        }
    }

    void SetCameraTo(string nameOfTheObject)
    {
        obj = null;
        obj=GameObject.Find(nameOfTheObject);
        
        if (obj ==true)
        {
            print("obj "+obj.name+" found "+obj.transform.position);
            objMotionDriver = obj.GetComponent<MotionDriver>();
            if (objMotionDriver==true) print("MotionDriver  found");   
            mainCamera = GameObject.Find("Main Camera");
            eye = null;
            
            foreach (Transform child in obj.transform)
            {
                if (child.tag=="eye")
                {
                     eye = child.gameObject;
                     print("eye found "+eye.transform.position);
                }
            }
            if (eye ==true)
            {
                if (mainCamera ==true)
                {
                    
                    print("camera found");
                    //mainCamera.transform.parent = null;
                    //UnityEngine.Camera.main.transform
                    mainCamera.transform.SetParent(eye.transform,false);
                    mainCamera.transform.position = eye.transform.position;
                    //mainCamera.transform.localPosition = eyeTransform.localPosition;
                    //mainCamera.transform.rotation = eyeTransform.rotation;
                    CameraHere = false;
                    objMotionDriver.CameraHere = true;
                    print("set parent camera to "+mainCamera.transform.parent.tag);
                    
                }
                else print("ATTANTION: camera not found");
            }
            else
            {
                print("ATTANTION: eye not found");
            }
        }
    }
}
