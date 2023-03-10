using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public enum TypeDirection { North=0, NorthEast=1, SouthEast=2, South=3, SouthWest=4, NorthWest=5, NorthNorth=6}
    private  static Dictionary<int,TypeDirection> _orientation = new Dictionary<int,TypeDirection>()
    {
        [0]=TypeDirection.North,
        [1]=TypeDirection.NorthEast,
        [2]=TypeDirection.SouthEast,
        [3]=TypeDirection.South,
        [4]=TypeDirection.SouthWest,
        [5]=TypeDirection.NorthWest,
        [6]=TypeDirection.NorthNorth
    };
    

    public static TypeDirection RotateToLeft(TypeDirection nowDirection)
    {
        int indexDirection = (int) nowDirection;
        if (indexDirection == 0) indexDirection = 6;
        return _orientation[indexDirection-1];
    }
    
    public static TypeDirection RotateToRight(TypeDirection nowDirection)
    {
        int indexDirection = (int) nowDirection;
        if (indexDirection == 6) indexDirection = 0;
        return _orientation[indexDirection+1];
    }

    public static Vector3 DirectionToVector3(TypeDirection _direction)
    {
        int indexDirection = (int) _direction;
        return new Vector3(0f, (int) _direction * 60f, 0f);
    }
}
