using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    // Kijken of er naast de plek van spawnen al rooms zitten in die dictionary, als dit meer dan 1 is moet je de kans op 0 zetten

    // Een variabele in Room eerst maken met als type RoomType, wat een enum is

    // als maar 1 neighbour, kans op boss room, of shop, of throphy room, of iets. 

    public enum RoomType
    {
        Normal,
        Item,
        Shop,
        BossRoom
    }

    [Range(0, 100)]
    public float relativeDepthFactor = 20.0f;
    //[Range(100000, 999999)]
    public string seed = "";

    public Dictionary<Vector2, Room> FloorLayout;
    public Vector2 RoomDimension = new Vector2(50, 50);
    public GameObject Cube;
    public GameObject door;

    public List<Vector3> wallsGenerated = new List<Vector3>();

    public List<CustomArray> list = new List<CustomArray>();

    public Material BossRoom;
    public Material ItemRoom;
    public Material ShopRoom;


    public RoomType roomType;


    // Start is called before the first frame update
    void Start()
    {
        GenerateFloor();
    }

    private void OnValidate()
    {
        DestroyFloor();
        GenerateFloor();
    }

    public void DestroyFloor()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            StartCoroutine(Destroy(gameObject.transform.GetChild(i).gameObject));
        }
    }

    public void GenerateFloor()
    {
        if (seed.Length != 8)
            return;

        wallsGenerated = new List<Vector3>();
        Random.InitState(seed.Substring(0,8).ToUpper().GetHashCode());
        Debug.Log(Random.state);
        FloorLayout = new Dictionary<Vector2, Room>();
        FloorLayout.Add(Vector2.zero, new Room(Vector2.zero, false, false, false, false));
        AddRoom(1, Vector2.zero);

        

        List<Vector2> specialRooms = new List<Vector2>();
        foreach (KeyValuePair<Vector2, Room> pair in FloorLayout)
        {
            int neighbours = FloorLayout.ContainsKey(pair.Key + new Vector2(1, 0)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pair.Key + new Vector2(-1, 0)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pair.Key + new Vector2(0, 1)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pair.Key + new Vector2(0, -1)) ? 1 : 0;

            if (neighbours == 1)
            {
                specialRooms.Add(pair.Key);
            }
        }
        int i = 0;
        for (; i < 3 && specialRooms.Count > 0; i++)
        {
            int randomIndex = Random.Range(0,specialRooms.Count);
            FloorLayout[specialRooms[randomIndex]].type = (RoomType)(i + 1);
            specialRooms.RemoveAt(randomIndex);
        }

        Vector2[] offsets = {  new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0) };
        if (i < 3)
        {
            List<Vector2> possibleSpecialRooms = new List<Vector2>();

            foreach (KeyValuePair<Vector2, Room> pair in FloorLayout)
            {
                for (int neighbour = 0; neighbour < 4; neighbour++)
                {
                    if (!FloorLayout.ContainsKey(pair.Key + offsets[neighbour]))
                    {
                        int amount = 0;
                        for (int second = 0; second < 4; second++)
                        {
                            amount += (FloorLayout.ContainsKey(pair.Key + offsets[neighbour] + offsets[second])) ? 1 : 0;
                        }
                        if (amount == 1)
                        {
                            possibleSpecialRooms.Add(pair.Key + offsets[neighbour]);
                        }
                    }

                    
                }

                
            }

            for (int room = i; room < 3; room++)
            {
                int randomIndex = Random.Range(0,possibleSpecialRooms.Count);
                bool north = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[0]);
                bool east = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[1]);
                bool south = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[2]);
                bool west = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[3]);

                FloorLayout.Add(possibleSpecialRooms[randomIndex], new Room(possibleSpecialRooms[randomIndex], north, east ,south, west));
                FloorLayout[possibleSpecialRooms[randomIndex]].type = (RoomType)(i + 1);
                Debug.Log("room");
                if (north)
                {
                    FloorLayout[possibleSpecialRooms[randomIndex] + offsets[0]].south = true;
                }
                if (east)
                {
                    FloorLayout[possibleSpecialRooms[randomIndex] + offsets[1]].west = true;
                }
                if (south)
                {
                    FloorLayout[possibleSpecialRooms[randomIndex] + offsets[2]].north = true;
                }
                if (west)
                {
                    FloorLayout[possibleSpecialRooms[randomIndex] + offsets[3]].east = true;
                }

                Debug.Log(possibleSpecialRooms[randomIndex].x + " " + possibleSpecialRooms[randomIndex].y);
                possibleSpecialRooms.RemoveAt(randomIndex);
            }
            Debug.Log("Generated Extra Rooms");
        }
        
        foreach (KeyValuePair<Vector2, Room> pair in FloorLayout)
        {
            pair.Value.GenerateMesh(RoomDimension, Cube, door, gameObject, ref wallsGenerated, list, BossRoom, ItemRoom, ShopRoom);

            if (pair.Key == new Vector2(-2, 0))
                Debug.Log("is in ");
            //Debug.Log(pair.Key.ToString());
        }

        Debug.Log(Random.seed);
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    void AddRoom(int depth, Vector2 pos)
    {
        float chance = 0.8f - depth / relativeDepthFactor;
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(0, -1)))
        {
            int neighbours = FloorLayout.ContainsKey(pos + new Vector2(0, -2)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(1, -1)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(-1, -1)) ? 1 : 0;
            if (neighbours == 0)
            {
                FloorLayout[pos].north = true;
                Vector2 nextPos = pos + new Vector2(0, -1);
                FloorLayout.Add(nextPos, new Room(nextPos, false, false, true, false));
                AddRoom(depth + 1, nextPos);
            }
        }
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(1, 0)))
        {
            int neighbours = FloorLayout.ContainsKey(pos + new Vector2(1, 1)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(1, -1)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(2, 0)) ? 1 : 0;
            if (neighbours == 0)
            {
                FloorLayout[pos].east = true;
                Vector2 nextPos = pos + new Vector2(1, 0);
                FloorLayout.Add(nextPos, new Room(nextPos, false, false, false, true));
                AddRoom(depth + 1, nextPos);
            }
        }
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(0, 1)))
        {
            int neighbours = FloorLayout.ContainsKey(pos + new Vector2(0, 2)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(1, 1)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(-1, 1)) ? 1 : 0;
            if (neighbours == 0)
            {
                FloorLayout[pos].south = true;
                Vector2 nextPos = pos + new Vector2(0, 1);
                FloorLayout.Add(nextPos, new Room(nextPos, true, false, false, false));
                AddRoom(depth + 1, nextPos);
            }
        }
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(-1, 0)))
        {
            int neighbours = FloorLayout.ContainsKey(pos + new Vector2(-2, 0)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(-1, 1)) ? 1 : 0;
            neighbours += FloorLayout.ContainsKey(pos + new Vector2(-1, -1)) ? 1 : 0;
            if (neighbours == 0)
            {
                FloorLayout[pos].west = true;
                Vector2 nextPos = pos + new Vector2(-1, 0);
                FloorLayout.Add(nextPos, new Room(nextPos, false, true, false, false));
                AddRoom(depth + 1, nextPos);
            }
        }
    }

    public class Room
    {
        public bool north, east, south, west;
        public Vector2 position;
        public RoomType type;


        public Room(Vector2 position, bool north, bool east, bool south, bool west)
        {
            this.position = position;
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
            this.type = RoomType.Normal;
        }

        public void GenerateMesh(Vector2 roomDimension, GameObject toInstantiate, GameObject Door, GameObject parent, ref List<Vector3> wallsGen, List<CustomArray> customList, Material boss, Material item, Material shop)
        {
            int index = 0;
            index += (north) ? 1 : 0;
            index += (east) ? 2 : 0;
            index += (south) ? 4 : 0;
            index += (west) ? 8 : 0;

            // Generate the floor
            toInstantiate.transform.localScale = new Vector3(roomDimension.x, 1, roomDimension.y);
            Vector3 worldPos = new Vector3(position.x * roomDimension.x, 0, position.y * roomDimension.y);
            GameObject instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
            instance.transform.parent = parent.transform;
            if (type != RoomType.Normal)
            {
                toInstantiate.transform.localScale = new Vector3(10, 10, 10);
                worldPos = new Vector3(position.x * roomDimension.x, 20, position.y * roomDimension.y);
                instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
                instance.transform.parent = parent.transform;

                switch (type)
                {
                    case RoomType.Item:
                        {
                            instance.GetComponent<MeshRenderer>().material = item;
                            break;
                        }
                    case RoomType.Shop:
                        {
                            instance.GetComponent<MeshRenderer>().material = shop;
                            break;
                        }
                    case RoomType.BossRoom:
                        {
                            instance.GetComponent<MeshRenderer>().material = boss;
                            break;
                        }
                }
            }
            Vector3[] wallScales = { new Vector3(roomDimension.x / 2.0f, 5, 1), new Vector3(1, 5, roomDimension.y / 2.0f) };
            Vector3[] wallOffsets = { new Vector3(0, 2.5f, -roomDimension.y / 4.0f), new Vector3(roomDimension.y / 4.0f, 2.5f, 0), new Vector3(0, 2.5f, roomDimension.y / 4.0f), new Vector3(-roomDimension.y / 4.0f, 2.5f, 0) };

            for (int i = 0; i < 4; i++) // Generating Walls
            {
                //toInstantiate.transform.localScale = wallScales[i%2];
                //worldPos = new Vector3(Position.x * roomDimension.x, 0, Position.y * roomDimension.y) + wallOffsets[i];
                //instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
                //instance.transform.parent = parent.transform;
            }

            bool[] directions = { north, east, south, west };
            Vector3[] roomScales = { new Vector3(1, 1, roomDimension.y), new Vector3(roomDimension.x, 1, 1) };
            Vector3[] roomOffsets = { new Vector3(0, 0, -roomDimension.y / 2.0f), new Vector3(roomDimension.x / 2.0f, 0, 0), new Vector3(0, 0, roomDimension.y / 2.0f), new Vector3(-roomDimension.x / 2.0f, 0, 0) };

            Vector3[] doorOffsets = { new Vector3(0, 2.5f, -roomDimension.y / 4.0f), new Vector3(roomDimension.x / 4.0f, 2.5f, 0), new Vector3(0, 2.5f, roomDimension.y / 4.0f), new Vector3(-roomDimension.x / 4.0f, 2.5f, 0) };

            for (int i = 0; i < 4; i++) // Generating Rooms and Doors
            {
                if (directions[i])
                {
                    int randomIndex = Random.Range(0, customList[2].objects.Length);
                    worldPos = new Vector3(position.x * roomDimension.x, customList[2].objects[randomIndex].transform.localScale.y, position.y * roomDimension.y) + roomOffsets[i];
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    rot.eulerAngles = new Vector3(0, (i % 2 == 1) ? 90.0f : 0, 0);
                    if (!wallsGen.Contains(worldPos))
                    {
                        instance = Instantiate(customList[2].objects[randomIndex], worldPos, rot);
                        instance.transform.parent = parent.transform;
                        wallsGen.Add(worldPos);
                    }


                    //Door.transform.localScale = new Vector3(4, 4, 4);
                    //worldPos = new Vector3(Position.x * roomDimension.x, 0, Position.y * roomDimension.y) + doorOffsets[i];
                    //instance = Instantiate(Door, worldPos, new Quaternion(0, 0, 0, 0));
                    //instance.transform.parent = parent.transform;
                }
                else
                {
                    int randomWall = Random.Range(0, 2);
                    int randomIndex = Random.Range(0, customList[randomWall].objects.Length);
                    worldPos = new Vector3(position.x * roomDimension.x, customList[randomWall].objects[randomIndex].transform.localScale.y, position.y * roomDimension.y) + roomOffsets[i];
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    rot.eulerAngles = new Vector3(0, (i % 2 == 1) ? 90.0f : 0, 0);
                    if (!wallsGen.Contains(worldPos))
                    {
                        instance = Instantiate(customList[randomWall].objects[randomIndex], worldPos, rot);
                        instance.transform.parent = parent.transform;
                        wallsGen.Add(worldPos);
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class CustomArray
    {
        public GameObject[] objects;
    }
}
