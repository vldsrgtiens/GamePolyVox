using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    
    public enum TypePerson { Hero, Robot, Enemy };
    public enum MotionStatus { IsWaiting, IsMoving, IsRotating, IsBeforeTargetPosition, IsBeforeTargetRotation, 
        IsError, IsPathBlocked, IsPathTemporarilyBlocked, IsLoadUnload  };
    public enum StatusActionExseption { IsOk, IsNotFound, IsToolDoesNotFit }

    public record StatusAction(List<StatusActionExseption> ExseptionList, [CanBeNull] Transform OutTargetTransform)
    {
        public List<StatusActionExseption> ExseptionList { get; set; }
        [CanBeNull] public Transform OutTargetTransform { get; set; }
    }
    
    
    

    public static float SpeedMax = 10f;










}
