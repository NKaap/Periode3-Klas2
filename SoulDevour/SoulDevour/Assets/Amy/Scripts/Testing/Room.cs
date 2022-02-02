using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{

    public Vector3 gridPosition;
    public int roomType;
    public bool doorRight, doorLeft, doorUp, doorDown;


    public Room(Vector3 gridPos, int type)
    {
        gridPosition = gridPos;
        roomType = type;
    }
}
