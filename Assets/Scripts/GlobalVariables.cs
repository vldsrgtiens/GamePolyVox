using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public enum  TypePerson { Hero, Robot, Enemy };
    public enum  MotionStatus { IsWaiting, IsMoving, IsRotating, IsBeforeTargetPosition, IsBeforeTargetRotation, 
        isError, isPathBlocked, isPathTemporarilyBlocked, isLoadUnload  };

    public static float SpeedMax = 10f;










}
