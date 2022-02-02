using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [Range(0,100)]
    public float RelativeDepthFactor = 20.0f;
    [Range(100000, 999999)]
    public int Seed = 100000;

    public Dictionary<Vector2, Room> FloorLayout;
    public Vector2 RoomDimension = new Vector2(50, 50);
    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(Seed);
        FloorLayout = new Dictionary<Vector2, Room>();
        FloorLayout.Add(Vector2.zero, new Room(Vector2.zero, false, false, false, false));
        AddRoom(1, Vector2.zero);

        foreach (KeyValuePair<Vector2, Room> pair in FloorLayout)
        {
            pair.Value.GenerateMesh(RoomDimension, Cube, gameObject);
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            StartCoroutine(Destroy(gameObject.transform.GetChild(i).gameObject));
        }

        Random.InitState(Seed);
        FloorLayout = new Dictionary<Vector2, Room>();
        FloorLayout.Add(Vector2.zero, new Room(Vector2.zero, false, false, false,false));
        AddRoom(1, Vector2.zero);

        foreach (KeyValuePair<Vector2, Room> pair in FloorLayout)
        {
            pair.Value.GenerateMesh(RoomDimension, Cube, gameObject);
        }
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    void AddRoom(int depth, Vector2 pos)
    {
        float chance = 0.8f - depth / RelativeDepthFactor;
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(0, -1)))
        {
            FloorLayout[pos].North = true;
            Vector2 nextPos = pos + new Vector2(0, -1);
            FloorLayout.Add(nextPos, new Room(nextPos, false, false, true, false));
            AddRoom(depth + 1, nextPos);
        }
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(1, 0)))
        {
            FloorLayout[pos].East = true;
            Vector2 nextPos = pos + new Vector2(1, 0);
            FloorLayout.Add(nextPos, new Room(nextPos, false, false, false, true));
            AddRoom(depth + 1, nextPos);
        }
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(0, 1)))
        {
            FloorLayout[pos].South = true;
            Vector2 nextPos = pos + new Vector2(0, 1);
            FloorLayout.Add(nextPos, new Room(nextPos, true, false, false, false));
            AddRoom(depth + 1, nextPos);
        }
        if (Random.Range(0f, 1f) < chance && !FloorLayout.ContainsKey(pos + new Vector2(-1, 0)))
        {
            FloorLayout[pos].West = true;
            Vector2 nextPos = pos + new Vector2(-1, 0);
            FloorLayout.Add(nextPos, new Room(nextPos, false, true, false, false));
            AddRoom(depth + 1, nextPos);
        }
    }

    public class Room
    {
        public bool North, East, South, West;
        public Vector2 Position;

        public Room(Vector2 position, bool north, bool east, bool south, bool west)
        {
            Position = position;
            North = north;
            East = east;
            South = south;
            West = west;
        }

        public void GenerateMesh(Vector2 roomDimension, GameObject toInstantiate, GameObject parent)
        {
            int index = 0;
            index += (North) ? 1 : 0;
            index += (East) ? 2 : 0;
            index += (South) ? 4 : 0;
            index += (West) ? 8 : 0;

            // Generate the floor
            toInstantiate.transform.localScale = new Vector3(roomDimension.x / 2, 1, roomDimension.y/2);
            Vector3 worldPos = new Vector3(Position.x * roomDimension.x, 0, Position.y * roomDimension.y);
            GameObject instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
            instance.transform.parent = parent.transform;

            Vector3[] wallScales = { new Vector3(roomDimension.x / 2.0f, 5, 1), new Vector3(1,5, roomDimension.y / 2.0f)};
            Vector3[] wallOffsets = { new Vector3(0, 2.5f, -roomDimension.y / 4.0f), new Vector3(roomDimension.y / 4.0f, 2.5f, 0), new Vector3(0, 2.5f, roomDimension.y / 4.0f), new Vector3(-roomDimension.y / 4.0f, 2.5f, 0)};

            for (int i = 0; i < 4; i++) // Generating Walls
            {
                toInstantiate.transform.localScale = wallScales[i%2];
                worldPos = new Vector3(Position.x * roomDimension.x, 0, Position.y * roomDimension.y) + wallOffsets[i];
                instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
                instance.transform.parent = parent.transform;
            }

            bool[] directions = { North, East, South, West};
            Vector3[] roomScales = { new Vector3(1, 1, roomDimension.y) , new Vector3(roomDimension.x, 1, 1) };
            Vector3[] roomOffsets = { new Vector3(0, 0, -roomDimension.y / 2.0f) , new Vector3(roomDimension.x / 2.0f, 0, 0), new Vector3(0, 0, roomDimension.y / 2.0f), new Vector3(-roomDimension.x / 2.0f, 0, 0) };

            Vector3[] doorOffsets = { new Vector3(0, 2.5f, -roomDimension.y / 4.0f), new Vector3(roomDimension.x / 4.0f, 2.5f, 0), new Vector3(0, 2.5f, roomDimension.y / 4.0f), new Vector3(-roomDimension.x / 4.0f, 2.5f, 0) };

            for (int i = 0; i < 4; i++) // Generating Rooms and Doors
            {
                if (directions[i])
                {
                    toInstantiate.transform.localScale = roomScales[i%2];
                    worldPos = new Vector3(Position.x * roomDimension.x, 0, Position.y * roomDimension.y) + roomOffsets[i];
                    instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
                    instance.transform.parent = parent.transform;

                    toInstantiate.transform.localScale = new Vector3(4, 4, 4);
                    worldPos = new Vector3(Position.x * roomDimension.x, 0, Position.y * roomDimension.y) + doorOffsets[i];
                    instance = Instantiate(toInstantiate, worldPos, new Quaternion(0, 0, 0, 0));
                    instance.transform.parent = parent.transform;
                }
            }
        }
    }
}
