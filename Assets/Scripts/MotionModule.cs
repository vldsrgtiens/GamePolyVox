
using System;
using System.Collections.Generic;
using UnityEngine;
//using Vector3 = UnityEngine.Vector3;

public class MotionModule : MonoBehaviour
{
    public GlobalVariables.MotionStatus mmStatus = GlobalVariables.MotionStatus.IsWaiting;
    public Vector3 mmTargetPosition = Vector3.zero;
    public Compass.TypeDirection mmTargetDirection;

    private float mmSpeed;
    private float directionOfRotation = 1f;
    private float oldRotation = 360f;

    Rigidbody mm_Rigidbody;
    Transform mm_Transform;
    ObjectItem mm_ObjectItem;

    void Awake()
    {
        mm_Rigidbody = GetComponent<Rigidbody>();
        mm_ObjectItem = GetComponent<ObjectItem>();

        mmSpeed = mm_ObjectItem.speed;

        //RotateTo(Compass.TypeDirection.SouthEast);
        
        Debug.Log("MotionDrive Awake() was called");
    }
    
    void FixedUpdate()
    {
        if (mmStatus == GlobalVariables.MotionStatus.IsMoving )
        {
            // вектор направления к цели. вектор надо нормализовать, чтобы скорость была постоянной, иначе она будет зависеть от расстояния.
            Vector3 heading = (mmTargetPosition - transform.position).normalized;
            float step = Time.deltaTime * mmSpeed;

            float dist_toTarget = Vector3.Distance(mmTargetPosition, transform.position + heading * step);
            
            if (dist_toTarget < step) 
            {
                mmStatus = GlobalVariables.MotionStatus.IsBeforeTargetPosition;
            }
            //print("position: "+transform.position);
            mm_Rigidbody.MovePosition(transform.position + heading * step);
        }
        
        //==============================

        if (mmStatus == GlobalVariables.MotionStatus.IsBeforeTargetPosition)
        {
            mm_Rigidbody.position = mmTargetPosition;
            mm_Rigidbody.velocity = Vector3.zero; 
            mmStatus = GlobalVariables.MotionStatus.IsWaiting;
        } 
        
        //==============================
        
        if (mmStatus == GlobalVariables.MotionStatus.IsWaiting)
        {
            //mmTargetPosition=transform.position+(2*(new Vector3(40f,0f,0f)-transform.position));
            //mmStatus = GlobalVariables.MotionStatus.isMoving;
        }
        
        //==============================
        
        if (mmStatus == GlobalVariables.MotionStatus.IsRotating )
        {
            //mmTargeDirectionV3 = Compass.DirectionToVector3(mmTargetDirection);
            
            float _delta = MathF.Abs((GlobalVariables.angleRotate * (int)mmTargetDirection) - oldRotation) ;

            float _rot = (mmSpeed / 5) * directionOfRotation;
            
            if (_delta < 2.0f) 
            {
                mmStatus = GlobalVariables.MotionStatus.IsBeforeTargetRotation;
            }

            transform.Rotate(0f,_rot,0f);
            oldRotation = transform.eulerAngles.y;
        }
        
        //==============================

        if (mmStatus == GlobalVariables.MotionStatus.IsBeforeTargetRotation)
        {
            Vector3 rotate = transform.eulerAngles;
            rotate.y = 60.0f * (int)mmTargetDirection;
            transform.rotation = Quaternion.Euler(rotate);
            mm_ObjectItem.direction = mmTargetDirection;
            mmStatus = GlobalVariables.MotionStatus.IsWaiting;
        }
    }

    public void MoveForward(Vector3 target)
    {
        mmTargetPosition = target;
        mmStatus=GlobalVariables.MotionStatus.IsMoving;
    }
        
    public void MoveBack(Rigidbody rb,Vector3 vector3To, float speed)
    {
        //rb.velocity = -1 * vector3To * speed;
        //is_moving = true;
    }

    public void RotateToLeft()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = Compass.RotateToLeft(mm_ObjectItem.direction);
        oldRotation = GlobalVariables.angleRotate * (int)mm_ObjectItem.direction;
        directionOfRotation = -1f;
        mmStatus = GlobalVariables.MotionStatus.IsRotating;
    }
    public void RotateToRight()
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = Compass.RotateToRight(mm_ObjectItem.direction);
        oldRotation = GlobalVariables.angleRotate * (int)mm_ObjectItem.direction;
        directionOfRotation = 1f;
        mmStatus = GlobalVariables.MotionStatus.IsRotating;
    }
    
    public void RotateTo(Compass.TypeDirection target)
    {
        //player.transform.RotateAround(player.transform.position,player.transform.up,_rotation_y*Time.deltaTime);
        mmTargetDirection = target;
        oldRotation = GlobalVariables.angleRotate * (int)mm_ObjectItem.direction;
        float t1 = Mathf.Abs(((int)mmTargetDirection * GlobalVariables.angleRotate) - (oldRotation));
        float t2 = Mathf.Abs((int)mmTargetDirection * GlobalVariables.angleRotate - (360f + oldRotation));
        if (t1 < t2)
            directionOfRotation = 1f;
        else
            directionOfRotation = -1f;
        mmStatus = GlobalVariables.MotionStatus.IsRotating;
    }
}
