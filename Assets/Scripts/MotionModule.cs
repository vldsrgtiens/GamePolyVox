
using System;
using System.Collections.Generic;
using UnityEngine;
//using Vector3 = UnityEngine.Vector3;

public class MotionModule : MonoBehaviour
{
    public GlobalVariables.MotionStatus mmStatus = GlobalVariables.MotionStatus.IsWaiting;
    public GlobalVariables.MotionStatus mmBeforeStatus = GlobalVariables.MotionStatus.IsWaiting;
    public bool statusChangeIndicator = false;
    public int mmTargetCellPosition = -1;
    public Compass.TypeDirection mmTargetDirection;
    private float _mmSpeed;
    private float _directionOfRotation = 1f;
    private float _oldRotation = 360f;

    Rigidbody _mmRigidbody;
    Transform _mmTransform;
    ObjectItem _mmObjectItem;

    void Awake()
    {
        _mmRigidbody = GetComponent<Rigidbody>();
        _mmObjectItem = GetComponent<ObjectItem>();
        _mmSpeed = _mmObjectItem.speed;
    }
    
    void FixedUpdate()
    {
        
        
        if (mmStatus == GlobalVariables.MotionStatus.IsWaiting)
        {
            
        }
        
        //==============================
        
        if (mmStatus == GlobalVariables.MotionStatus.IsMoving )
        {
            // вектор направления к цели. вектор надо нормализовать, чтобы скорость была постоянной, иначе она будет зависеть от расстояния.
            Vector3 heading = (HexMetrics.GetPositionCenterFromNum(mmTargetCellPosition) - transform.position).normalized;
            float step = Time.deltaTime * _mmSpeed;
            float dist_toTarget = Vector3.Distance(HexMetrics.GetPositionCenterFromNum(mmTargetCellPosition), transform.position + heading * step);
            if (dist_toTarget <= 2*step) 
            {
                changeStatus(GlobalVariables.MotionStatus.IsBeforeTargetPosition);
            }
            _mmRigidbody.MovePosition(transform.position + heading * step);
        }
        
        //==============================

        if (mmStatus == GlobalVariables.MotionStatus.IsBeforeTargetPosition)
        {
            _mmRigidbody.position = HexMetrics.GetPositionCenterFromNum(mmTargetCellPosition);
            _mmRigidbody.velocity = Vector3.zero;
            _mmObjectItem.CurrentCellPosition = mmTargetCellPosition;
            changeStatus(GlobalVariables.MotionStatus.IsWaiting);
        } 

        //==============================
        
        if (mmStatus == GlobalVariables.MotionStatus.IsRotating )
        {
            float _delta = MathF.Abs((GlobalVariables.angleRotate * (int)mmTargetDirection) - _oldRotation) ;
            float _rot = (_mmSpeed / 5) * _directionOfRotation;
            if (_delta < 1.5f) 
            {
                changeStatus(GlobalVariables.MotionStatus.IsBeforeTargetRotation);
            }
            transform.Rotate(0f,_rot,0f);
            _oldRotation = transform.eulerAngles.y;
        }
        
        //==============================

        if (mmStatus == GlobalVariables.MotionStatus.IsBeforeTargetRotation)
        {
            Vector3 _rotate = transform.eulerAngles;
            _rotate.y = 60.0f * (int)mmTargetDirection;
            transform.rotation = Quaternion.Euler(_rotate);
            _mmObjectItem.direction = mmTargetDirection;
            changeStatus(GlobalVariables.MotionStatus.IsWaiting);
        }
    }

    public void MoveForward(int targetCell)
    {
        mmTargetCellPosition = targetCell;
        changeStatus(GlobalVariables.MotionStatus.IsMoving);
    }
        
    public void MoveBack(Rigidbody rb,Vector3 vector3To, float speed)
    {
        //rb.velocity = -1 * vector3To * speed;
        //is_moving = true;
    }

    public void RotateToLeft()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = Compass.RotateToLeft(_mmObjectItem.direction);
        _oldRotation = GlobalVariables.angleRotate * (int)_mmObjectItem.direction;
        _directionOfRotation = -1f;
        changeStatus(GlobalVariables.MotionStatus.IsRotating);
    }
    public void RotateToRight()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = Compass.RotateToRight(_mmObjectItem.direction);
        _oldRotation = GlobalVariables.angleRotate * (int)_mmObjectItem.direction;
        _directionOfRotation = 1f;
        changeStatus(GlobalVariables.MotionStatus.IsRotating);
    }

    public void changeStatus(GlobalVariables.MotionStatus newStatus)
    {
        mmBeforeStatus = mmStatus;
        mmStatus = newStatus;
        print(transform.tag+": changeStatus from "+mmBeforeStatus+" to "+mmStatus);
        statusChangeIndicator = true;
    }
    
    public void RotateTo(Compass.TypeDirection target)
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = target;
        _oldRotation = GlobalVariables.angleRotate * (int)_mmObjectItem.direction;
        float t1 = Mathf.Abs(((int)mmTargetDirection * GlobalVariables.angleRotate) - (_oldRotation));
        float t2 = Mathf.Abs((int)mmTargetDirection * GlobalVariables.angleRotate - (360f + _oldRotation));
        if (t1 < t2)
            _directionOfRotation = 1f;
        else
            _directionOfRotation = -1f;

    }
}
