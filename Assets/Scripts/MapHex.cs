using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapHex : MonoBehaviour
{
    public const int Height = 13;// h*2+2
    public const int Width = 11;//w*2+1
    int[,] Grid= { 
        { 0, 0, 0, 1, 0}, 
        { 1, 0, 0, 0, 1},
        { 1, 0, 0, 0, 1},
        { 1, 0, 0, 1, 1},
        { 1, 0, 1, 1, 0},
        { 1, 0, 0, 0, 1}
  
    };

    public static string[,] GridFull;

    void Awake()
    {
        GridFull = new string[Width, Height];
        string str="";
        
        for (int z = 0, i = 0; z < Height; z++) 
            for (int x = 0; x < Width; x++ )
                GridFull[x, z]="";



        for (int z = 0, i = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++ )
                {
                    if ((z % 2 != 0) && (x % 2 != 0) && Grid[z / 2, x / 2] == 1)
                    {
                        GridFull[x, z] = GridFull[x, z]+"000";
                        GridFull[x - 1, z+1] = GridFull[x - 1, z+1]+"NW";
                        GridFull[x, z + 1] = GridFull[x, z + 1]+"NN";
                        GridFull[x + 1, z + 1] = GridFull[x + 1, z + 1] +"NE";
                        GridFull[x-1, z] = GridFull[x-1, z]+"SW";
                        GridFull[x , z-1] = GridFull[x , z-1]+"SS";
                        GridFull[x+1, z] = GridFull[x+1, z]+"SE";
                    }
                }
        }
        
        

	

        
    }

    
}
