using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

    public Vector2 worldSize = new Vector3(4, 4);
    Room[,] rooms;
    public List<Vector2> takenPositions = new List<Vector2>();
    public int xSize, ySize;
    public int roomAmount = 10;

    public GameObject roomWhite;


    // Start is called before the first frame update
    void Start()
    {
        if(roomAmount >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            roomAmount = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        xSize = Mathf.RoundToInt(worldSize.x);
        ySize = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetDoors();
        DrawMap();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRooms()
    {
        rooms = new Room[xSize * 2, ySize * 2];
        rooms[xSize, ySize] = new Room(Vector2.zero, 1); // roomtype == 1 which is starting run
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkpos = Vector2.zero;

        // math
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        // ad them roomies

        for(int i = 0; i < roomAmount - 1; i++)
        {
            float randomPerc = ((float)i) / (((float)roomAmount - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            // grab a new position
            checkpos = NewPosition();

            if(NumberOfNeighbours(checkpos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkpos = SelectiveNewPosition();
                    iterations++;

                } while (NumberOfNeighbours(checkpos, takenPositions) > 1 && iterations < 100);
                if(iterations >= 50)
                {
                    print("Error");
                }
            }
            // final pos
            rooms[(int)checkpos.x + xSize, (int)checkpos.y + ySize] = new Room(checkpos, 0); // 0 == first new room
            takenPositions.Insert(0, checkpos);
        }

       
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5);
            bool positive = (Random.value < 0.5);
            if (upDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);

        } while (takenPositions.Contains(checkingPos) || x >= xSize || x < -xSize || y >= ySize || y < -ySize);
        return checkingPos;
        
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;

        Vector2 checkingPos =  Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;

            } while (NumberOfNeighbours(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5);
            bool positive = (Random.value < 0.5);

            if (upDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        
        } while (takenPositions.Contains(checkingPos) || x >= xSize || x< -xSize || y >= ySize || y< -ySize);
        if(inc >= 100)
        {
            print("could not find pos with only one neighbor");
        }
        return checkingPos;
    }

    int NumberOfNeighbours(Vector2 checkingPos, List<Vector2> usedPos)
    {
        int ret = 0;
        if(usedPos.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPos.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPos.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPos.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }

        return ret;
    }
    
    public void SetDoors()
    {
        for(int x = 0; x < ((xSize * 2)); x++)
        {
            for (int y = 0; y < ((ySize * 2)); y++)
            {
                if(rooms[x,y] == null)
                {
                    continue;
                }

                Vector2 gridpos = new Vector2(x, y);

                if(y - 1 < 0)
                {
                    rooms[x, y].doorDown = false;

                }
                else
                {
                    rooms[x, y].doorDown = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= ySize * 2)
                {
                    rooms[x, y].doorUp = false;

                }
                else
                {
                    rooms[x, y].doorDown = (rooms[x, y + 1] != null);
                }


                if (x - 1 < 0)
                {
                    rooms[x, y].doorLeft = false;

                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y ] != null);
                }
                if (x + 1 >= xSize * 2)
                {
                    rooms[x, y].doorRight = false;

                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y ] != null);
                }
            }
        }
    }

    public void DrawMap()
    {

    }

   
}
