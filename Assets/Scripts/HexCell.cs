using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public float variableHC=555f;
    public int MyIndex = -1;
    public int canMove = 2; //0-не проходная клетка 1-не открытая клетка
                            //2-проходная свободна 3-проходная занята

    public int layer0=-1;
    public int layer1=-1;
    public int layer2=-1;
    
    public int[] neighbors = new int[6] { -1, -1, -1, -1, -1, -1 };
    



}
