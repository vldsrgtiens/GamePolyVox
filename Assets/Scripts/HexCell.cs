using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public GameObject myHexCanvas;
    public int myIndex = -1;
    public Vector3 myPosition;
    public int canMove = 2; // 0-не проходная клетка
                            //1-проходная занята 2-проходная свободна

    public int layer0=-1;
    public int layer1=-1;
    public int layer2=-1;
    
    public int[] neighbors = new int[6] { -1, -1, -1, -1, -1, -1 };
    



}
