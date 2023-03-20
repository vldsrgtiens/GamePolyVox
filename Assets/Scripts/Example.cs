using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Direction drg = Direction.Nwest;
       print("Direction.North : "+ drg.Rotor(Direction.Swest)+" : "+ (float)((Direction.Angle)Direction.North).Between(65f)); 
       print("Direction.Neast "+ (Direction.Angle)Direction.Neast); 
       print("Direction.Seast "+ (Direction.Angle)Direction.Seast); 
       print("Direction.South "+ (Direction.Angle)Direction.South); 
       print("Direction.Swest "+ (Direction.Angle)Direction.Swest); 
       print("Direction.Nwest "+ (Direction.Angle)Direction.Nwest); 
    }


}
