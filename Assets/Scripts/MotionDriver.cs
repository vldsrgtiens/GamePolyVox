using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDriver : MonoBehaviour, ITake
{
    private MotionDriver _objMotionDriver;
    private MotionModule _myMotionModule;
    private ObjectItem _myObjectItem;
    private GameObject _eye;
    private GameObject _eAction;
    private int _reservedCells = -1;
    
    
    public bool cameraHere = false;
    public bool cameraOutside = false;
    
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
        if (cameraHere)
        {
            if (Input.GetKey(KeyCode.W) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting)
            {
                int targetNumCell = CheckFreeDestinationCell(true);
                if (targetNumCell>=0)
                    _myMotionModule.MoveForward(targetNumCell);
            }
            if (Input.GetKey(KeyCode.S) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting)
            {
                int targetNumCell = CheckFreeDestinationCell(false);
                if (targetNumCell>=0)
                    _myMotionModule.MoveBack(targetNumCell);
            }
            if (Input.GetKeyDown(KeyCode.D) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting) {
                _myMotionModule.RotateToRight();
            }
            if (Input.GetKeyDown(KeyCode.A) && _myMotionModule.mmStatus==GlobalVariables.MotionStatus.IsWaiting) {
                _myMotionModule.RotateToLeft();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && this.CompareTag("Hero")) {
                SetCameraTo("Robot_1");
                //Escape  
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                GameObject mainCamera = GameObject.Find("Main Camera");//UnityEngine.Camera.main.
                Vector3 outside = new Vector3(0f,4f,4f);
                //Vector3 outsideAngle = mainCamera.transform.eulerAngles
                outside.y = outside.y - 6f;
                if (!cameraOutside) outside = outside * -1f;
                //if (!cameraOutside) outside = outside * -1f;
                
                mainCamera.transform.localPosition = mainCamera.transform.localPosition  + outside;
                cameraOutside = !cameraOutside;
            }
            if (Input.GetKeyDown(KeyCode.Escape) && !this.CompareTag("Hero"))
            {
                SetCameraTo("Hero");
            }
        }

        TrackChangesIndicatorMM();
    }

    int CheckFreeDestinationCell(bool _driveForvard)
    {
        var targetCell = -1;
        if (_driveForvard)
        {
            targetCell = HexGrid.cells[_myObjectItem.CurrentCellPosition].neighbors[(int)(_myObjectItem.direction)];

        }
        else
        {
            targetCell = HexGrid.cells[_myObjectItem.CurrentCellPosition].neighbors[(int)(_myObjectItem.direction.RelativeOf(Direction.South))]; 
        }

        if (targetCell >= 0)
        {
            
            if (HexGrid.cells[targetCell].canMove == 2)
            {
                HexGrid.cells[targetCell].canMove = 0;
                ChangeColorHexCell(targetCell,new Color (255f, 0f, 0f, 128f));
                _reservedCells = _myObjectItem.CurrentCellPosition;
                HexGrid.cells[_reservedCells].canMove = 1;
                ChangeColorHexCell(_reservedCells,new Color (255f, 255f, 0f, 128f));
                
                print("Moving from currentCell["+_myObjectItem.CurrentCellPosition+"] to targetCell["+targetCell+"]");
                return targetCell;
            }
            else if (HexGrid.cells[targetCell].canMove == 1)
            {
                print("ATTANTION: клетка cells["+targetCell+"] ВРЕМЕННО занята, надо что то делать");
                return -1;
            }
            else
            {
                print("ATTANTION: клетка cells["+targetCell+"] НЕПРОХОДНАЯ, надо что то делать");
                return -1; 
            }
        }
        else
        {
            print("ATTANTION: впереди БЕЗДНА");
            return -1;
        }
    }

    void TrackChangesIndicatorMM()
    {
        if (_myMotionModule.statusChangeIndicator)
        {
            print("indicator: Status="+_myMotionModule.mmStatus+" beforStatus="+_myMotionModule.mmBeforeStatus);
            if (_myMotionModule.mmBeforeStatus == GlobalVariables.MotionStatus.IsBeforeTargetPosition && 
                _myMotionModule.mmStatus == GlobalVariables.MotionStatus.IsWaiting)
            {
                if (_reservedCells >= 0)
                {
                    HexGrid.cells[_reservedCells].canMove = 2; 
                    print("_reservedCells:"+HexGrid.cells[_reservedCells].canMove);
                    ChangeColorHexCell(_reservedCells,new Color (0f, 255f, 0f, 128f));
                    _reservedCells = -1;
                }
            }

            _myMotionModule.statusChangeIndicator = false;
        } 
    }

    private void ChangeColorHexCell(int _cellNum, Color _color)
    {
        print("befor getComponent");
        var cellRender = HexGrid.cells[_cellNum].myHexCanvas.GetComponent<MeshRenderer>();
        cellRender.material.color = _color;
        print("after getComponent");
    }

    private void SetCameraTo(string nameOfTheObject)
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
                        cameraHere = false;
                        _objMotionDriver.cameraHere = true;

                }
                else print("ATTANTION: eye not found");
            }
            else print("ATTANTION: MotionDriver not found");
        }
    }

    void ITake.ApplyTake(GameObject holder, Vector3 handPosition)
    {
        
    }
}
