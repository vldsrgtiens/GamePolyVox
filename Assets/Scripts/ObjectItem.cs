using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    public bool canMove = true;
    public int CurrentCellPosition = 0;
    public Direction direction = Direction.North;
    
    public float speed = 5.0f;
    void Awake()
    {
        
    }

}
