
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
    public Direction mmTargetDirection;
    private float _mmSpeed;
    private float _directionOfRotation = 1f;
    private float _oldRotation = 0f;

    Rigidbody _mmRigidbody;
    Transform _mmTransform;
    ObjectItem _mmObjectItem;
    Animator _animator;

    void Awake()
    {
        _mmRigidbody = GetComponent<Rigidbody>();
        _mmObjectItem = GetComponent<ObjectItem>();
        _animator = GetComponent<Animator>();
        _mmSpeed = _mmObjectItem.speed;
    }

    private void Update()
    {
         
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
            _animator.SetBool("Forward",false);
        } 

        //==============================
        
        if (mmStatus == GlobalVariables.MotionStatus.IsRotating )
        {
            float delta = MathF.Abs((Direction.Angle)mmTargetDirection - _oldRotation) ;
            float step = Time.deltaTime *  _directionOfRotation * _mmSpeed * 4;
            step = _directionOfRotation*2;
            if (delta <= Mathf.Abs(2*step)) 
            {
                changeStatus(GlobalVariables.MotionStatus.IsBeforeTargetRotation);
            }
            
            transform.Rotate(0f,step,0f);
            _oldRotation = transform.eulerAngles.y;
            print("delta="+delta+" step="+step);
        } 
        
        //==============================

        if (mmStatus == GlobalVariables.MotionStatus.IsBeforeTargetRotation)
        {
            Vector3 _rotate = transform.eulerAngles;
            //_rotate.y = (_dAngle)(mmTargetDirection)
            _rotate.y = (Direction.Angle) mmTargetDirection;
            transform.rotation = Quaternion.Euler(_rotate);
            _mmObjectItem.direction = mmTargetDirection;
            changeStatus(GlobalVariables.MotionStatus.IsWaiting);
            _animator.SetBool("Right",false);
            _animator.SetBool("Left",false);
        }
    }

    public void MoveForward(int targetCell)
    {
        mmTargetCellPosition = targetCell;
        changeStatus(GlobalVariables.MotionStatus.IsMoving);
        _animator.SetBool("Forward",true);
    }
        
    public void MoveBack(Rigidbody rb,Vector3 vector3To, float speed)
    {
        //rb.velocity = -1 * vector3To * speed;
        //is_moving = true;
    }

    public void RotateToLeft()
    {
        mmTargetDirection = _mmObjectItem.direction.RelativeOf(Direction.Nwest);
        _directionOfRotation = -1f;
        _oldRotation = (Direction.Angle)_mmObjectItem.direction;
        changeStatus(GlobalVariables.MotionStatus.IsRotating);
        print("_mmObjectItem.direction="+(Direction.Angle)_mmObjectItem.direction+" mmTargetDirection="+(Direction.Angle)mmTargetDirection);
        _animator.SetBool("Left",true);
        
    }
    public void RotateToRight()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = _mmObjectItem.direction.RelativeOf(Direction.Neast);
        _directionOfRotation = 1f;
        changeStatus(GlobalVariables.MotionStatus.IsRotating);
        print("_mmObjectItem.direction="+(Direction.Angle)_mmObjectItem.direction+" mmTargetDirection="+(Direction.Angle)mmTargetDirection);
        _animator.SetBool("Right",true);
        
    }

    public void changeStatus(GlobalVariables.MotionStatus newStatus)
    {
        mmBeforeStatus = mmStatus;
        mmStatus = newStatus;
        print(transform.tag+": changeStatus from "+mmBeforeStatus+" to "+mmStatus);
        statusChangeIndicator = true;
    }
    
    public void RotateTo(Direction target)
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = target;
        _oldRotation = GlobalVariables.angleRotate * _mmObjectItem.direction.GetHashCode();
        float t1 = Mathf.Abs((mmTargetDirection.GetHashCode() * GlobalVariables.angleRotate) - (_oldRotation));
        float t2 = Mathf.Abs(mmTargetDirection.GetHashCode() * GlobalVariables.angleRotate - (360f + _oldRotation));
        if (t1 < t2)
            _directionOfRotation = 1f;
        else
            _directionOfRotation = -1f;

    }
}
