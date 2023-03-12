using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDriver : MonoBehaviour
{
    private MotionDriver _objMotionDriver;
    private MotionModule _myMotionModule;
    private ObjectItem _myObjectItem;
    private GameObject _eye;
    private int currentNumCell = -1;
    private int targetNumCell = -1;

    public bool CameraHere = false;
    public bool CameraOutside = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        _myMotionModule = GetComponent<MotionModule>();
        _myObjectItem = GetComponent<ObjectItem>();
        if (_myMotionModule == null)
        {
            Debug.Log("ATTANTION: MotionModule отсутствует");
        }
        if (this.CompareTag("Hero"))
        {
            SetCameraTo("Hero");
        }
    }

    void Update()
    {
        if (CameraHere)
        {
            if (Input.GetKey(KeyCode.W) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting)
            {
                if (CheckFreeDestinationCell())
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
            if (Input.GetKey(KeyCode.C))
            {
                GameObject mainCamera = GameObject.Find("Main Camera");
                Vector3 outside = transform.forward * 3f;
                outside.y = outside.y - 3f;
                if (!CameraOutside) outside = outside * -1f;
                mainCamera.transform.localPosition = mainCamera.transform.localPosition  + outside;
                CameraOutside = !CameraOutside;

            }
            if (Input.GetKey(KeyCode.Escape) && this.tag != "Hero")
            {
                SetCameraTo("Hero");
            }
        }
    }

    bool CheckFreeDestinationCell()
    {
        int nowCell = HexMetrics.GetPositionNumFromXY(transform.position.x, transform.position.z);
        int targetCell = HexGrid.cells[nowCell].neighbors[(int)_myObjectItem.direction];
        if (targetCell >= 0)
        {
            print("nowCell="+nowCell+"  targetCell="+targetCell+" direction="+_myObjectItem.direction);
            return true;
        }
        else
        {
            print("nowCell="+nowCell+"  targetCell="+targetCell+" direction="+_myObjectItem.direction);
            print("ATTANTION: впереди БЕЗДНА");
            return false;
        }
        
        
    }

    void SetCameraTo(string nameOfTheObject)
    {
        GameObject obj=GameObject.Find(nameOfTheObject);
        _eye = null;        
        if (obj ==true)
        {
            _objMotionDriver = obj.GetComponent<MotionDriver>();
            if (_objMotionDriver == true)
            {
                //GameObject mainCamera = GameObject.Find("Main Camera");
                foreach (Transform child in obj.transform)
                {
                    if (child.CompareTag("eye")) _eye = child.gameObject;
                }

                if (_eye == true)
                {
                        //mainCamera.transform.parent = null;
                        //UnityEngine.Camera.main.transform
                        UnityEngine.Camera.main.transform.SetParent(_eye.transform, false);
                        UnityEngine.Camera.main.transform.position = _eye.transform.position;
                        //mainCamera.transform.localPosition = eye.transform.localPosition;
                        UnityEngine.Camera.main.transform.rotation = _eye.transform.rotation;
                        CameraHere = false;
                        _objMotionDriver.CameraHere = true;

                }
                else print("ATTANTION: eye not found");
            }
            else print("ATTANTION: MotionDriver not found");
        }
    }
}
