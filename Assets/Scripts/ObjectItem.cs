using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    public bool canMove = true;
    public Compass.TypeDirection direction = Compass.TypeDirection.North;
    public float speed = 5.0f;
    void Awake()
    {
        
    }

}
