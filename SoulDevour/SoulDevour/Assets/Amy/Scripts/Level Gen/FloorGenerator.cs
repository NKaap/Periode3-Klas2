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

    [Header("Generator")]
    [Space(8)]
    [Range(0, 100)]
    public float relativeDepthFactor = 20.0f;

    //[Range(100000, 999999)]
    public string seed = "";


   
    public Dictionary<Vector2, Room> FloorLayout;
    [Header("Room Info")]
    [Space(8)]
    public Vector2 RoomDimension = new Vector2(50, 50);


    [Header("Temporary Models")]
    [Space(8)]
    public GameObject Cube;
    public GameObject door;

    [Header("Walls & Prefab List")]
    [Space(8)]
    public List<Vector3> wallsGenerated = new List<Vector3>();

    public List<CustomArray> list = new List<CustomArray>();


    [Header("Room Types")]
    [Space(8)]
    public Material BossRoom;
    public Material ItemRoom;
    public Material ShopRoom;


    public RoomType roomType;


    /// <summary>
    /// Generate the floor on start
    /// </summary>
    void Start()
    {
        GenerateFloor();
    }

    /// <summary>
    /// <para>Destroys and generates the floor when a variable is changed</para>
    /// </summary>
    private void OnValidate()
    {
        DestroyFloor();
        GenerateFloor();
    }

    /// <summary>
    /// Destroys all rooms
    /// <para>Has to be a coroutine, as the OnValidate doesn't allow this</para>
    /// </summary>
    public void DestroyFloor()
    { 
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            StartCoroutine(Destroy(gameObject.transform.GetChild(i).gameObject));
        }
    }

    /// <summary>
    /// Generates the floor with all rooms
    /// </summary>
    public void GenerateFloor()
    {
        if (seed.Length != 8)
            return;

        wallsGenerated = new List<Vector3>();
        FloorLayout = new Dictionary<Vector2, Room>();

        //Sets the seed with a string (convert to hashcode to make it an int)
        Random.InitState(seed.Substring(0,8).ToUpper().GetHashCode());

        //Make the first room, and recursively generate the other rooms using AddRoom
        FloorLayout.Add(Vector2.zero, new Room(Vector2.zero, false, false, false, false));
        AddRoom(1, Vector2.zero);

        //Make the list for potential special rooms
        List<Vector2> specialRooms = new List<Vector2>();

        //Get all special rooms, where a special room is a room with only 1 door
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

        //Go through the list and set a random room in the list as 1 of the 3 special rooms
        int i = 0;
        for (; i < 3 && specialRooms.Count > 0; i++)
        {
            int randomIndex = Random.Range(0,specialRooms.Count);
            FloorLayout[specialRooms[randomIndex]].type = (RoomType)(i + 1);
            specialRooms.RemoveAt(randomIndex);
        }

        //If there aren't enough potential special rooms, generate extra
        Vector2[] offsets = {  new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0) };
        if (i < 3)
        {
            List<Vector2> possibleSpecialRooms = new List<Vector2>();

            //Get all spots where we can force a special room
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

            //Generate extra special rooms
            for (int room = i; room < 3; room++)
            {
                int randomIndex = Random.Range(0,possibleSpecialRooms.Count);
                bool north = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[0]);
                bool east = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[1]);
                bool south = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[2]);
                bool west = FloorLayout.ContainsKey(possibleSpecialRooms[randomIndex] + offsets[3]);

                FloorLayout.Add(possibleSpecialRooms[randomIndex], new Room(possibleSpecialRooms[randomIndex], north, east ,south, west));
                FloorLayout[possibleSpecialRooms[randomIndex]].type = (RoomType)(i + 1);
                
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

                possibleSpecialRooms.RemoveAt(randomIndex);
            }
        }
        
        //Generate all meshes for all rooms
        foreach (KeyValuePair<Vector2, Room> pair in FloorLayout)
        {
            pair.Value.GenerateMesh(RoomDimension, Cube, door, gameObject, ref wallsGenerated, list, BossRoom, ItemRoom, ShopRoom);
        }
    }

    /// <summary>
    /// Destroy the gameobject
    /// </summary>
    /// <param name="go">The gameobject to destroy</param>
    /// <returns>the IEnumerator</returns>
    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    /// <summary>
    /// <para>Add another room on the position given</para>
    /// <para>Also generate more rooms if the chance is high enough</para>
    /// </summary>
    /// <param name="depth">Amount of rooms between this room and the start</param>
    /// <param name="pos">Position of the room</param>
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


        public Room(Vector2 position, bool north, bool east, bool south, bool west, RoomType type = RoomType.Normal)
        {
            this.position = position;
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
            this.type = type;
        }

        /// <summary>
        /// Generate all parts of the room
        /// </summary>
        /// <param name="roomDimension">The size of the room</param>
        /// <param name="toInstantiate">The object for making the floor</param>
        /// <param name="Door">The object for making the door</param>
        /// <param name="parent">The object this mesh should be a child of</param>
        /// <param name="wallsGen">A reference of the list of walls already generated</param>
        /// <param name="customList">A list of all possible walls</param>
        /// <param name="boss">Material for the cube above the boss room</param>
        /// <param name="item">Material for the cube above the item room</param>
        /// <param name="shop">Material for the cube above the shop</param>
        public void GenerateMesh(Vector2 roomDimension, GameObject toInstantiate, GameObject Door, GameObject parent, ref List<Vector3> wallsGen, List<CustomArray> customList, Material boss, Material item, Material shop)
        {
            //Can be used as index for prefabs
            int index = 0;
            index += (north) ? 1 : 0;
            index += (east) ? 2 : 0;
            index += (south) ? 4 : 0;
            index += (west) ? 8 : 0;

            //Generate the floor
            toInstantiate.transform.localScale = new Vector3(roomDimension.x, 1, roomDimension.y);
            Vector3 worldPos = new Vector3(position.x * roomDimension.x, 0, position.y * roomDimension.y);
            GameObject instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
            instance.transform.parent = parent.transform;

            //Make a cube above special rooms to make it easily spottable
            if (type != RoomType.Normal)                                                                                        // verander floor per item room zodat models en rooms kloppen
            {
                toInstantiate.transform.localScale = new Vector3(10, 10, 10);
                worldPos = new Vector3(position.x * roomDimension.x, 20, position.y * roomDimension.y);
                instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
                instance.transform.parent = parent.transform;

                switch (type)
                {
                    case RoomType.Item:                                                                                         // item room, alle items in vitrine kasten etc. show welke je unlocked hebt
                        {
                            instance.GetComponent<MeshRenderer>().material = item;
                            break;
                        }
                    case RoomType.Shop:
                        {
                            instance.GetComponent<MeshRenderer>().material = shop;                                              // shop, kun je kiezen uit 4 items die je kunt kopen
                            break;
                        }
                    case RoomType.BossRoom:
                        {
                            instance.GetComponent<MeshRenderer>().material = boss;                                              // dark room, boss level.
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

            // Generate Rooms and Doors
            for (int i = 0; i < 4; i++) 
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

    //A custom array for gameobjects that is visible in inspector
    [System.Serializable]
    public class CustomArray
    {
        public GameObject[] objects;
    }
}
