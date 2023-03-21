using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    public bool canMove = true;
    public int CurrentCellPosition = 0;
    public Direction direction = Direction.North;
    
    public float speedPercent = 0.7f;
    void Awake()
    {
        
    }

}
