using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralDungeonGenerate : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tile groundTile;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tile wallTile;
    [SerializeField] private GameObject horizontalDoor;
    [SerializeField] private GameObject verticalDoor;
    [SerializeField] private Tilemap objectTilemap;
    [SerializeField] private Tile objectTile;
    //[SerializeField] private List<RuleTile> objectPlaceholderTile;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private float objectFillPercent;
    [SerializeField] private Tilemap lightTilemap;
    [SerializeField] private RuleTile spotLightTile;                                                                                  

    public List<Vector3Int> directions;

    [SerializeField] private int roomTotal;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private int roomIndex;
    [SerializeField] private int maxRoomNumber;

    [Header("Dungeon Size")]
    [SerializeField] private int minX;
    [SerializeField] private int maxX;
    [SerializeField] private int minY;
    [SerializeField] private int maxY;

    [System.Serializable] class Room
    {
        public Room()
        {
            roomDirections = new List<Vector3Int>();
        }

        public Vector3Int roomCenter;
        public Vector3Int roomSize;
        public List<Vector3Int> roomDirections;
    }

    private void Awake()
    {   
        directions.Add(Vector3Int.up);
        directions.Add(Vector3Int.right);
        directions.Add(Vector3Int.down);
        directions.Add(Vector3Int.left);
        isNotGenerate = false;
        GenerateDungeon();
    }

    bool isNotGenerate;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isNotGenerate)
    //    {
    //        roomTotal = Random.Range(5, 31);
    //        groundTilemap.ClearAllTiles();
    //        isNotGenerate = false;
    //        GenerateDungeon();
    //    }
    //}

    void GenerateDungeon()
    {
        Vector3Int roomCenter = (Vector3Int.zero + new Vector3Int(5, 5, 0)) / 2;
        Vector3Int roomSize = new Vector3Int(5, 5, 0);
        DrawRoom(Vector3Int.zero, roomSize);

        rooms = new List<Room>();

        Room baseRoom = new Room();
        baseRoom.roomCenter = roomCenter;
        lightTilemap.SetTile(roomCenter, spotLightTile);
        baseRoom.roomSize = roomSize;
        baseRoom.roomDirections = new List<Vector3Int>(directions);
        rooms.Add(baseRoom);

        maxX = 5;
        maxY = 5;
        minX = 0;
        minY = 0;

        roomIndex = 0;
        while (roomTotal > 0)
        {
            int roomNumber = Random.Range(1, maxRoomNumber + 1);
            //Debug.Log(roomNumber);
            for (int i = 0; i < roomNumber; i++)
            {
                GenerateRoom(rooms[roomIndex]);
                roomTotal--;
                if (roomTotal == 0)
                {
                    break;
                }
            }
            roomIndex++;
        }
        isNotGenerate = true;
    }

    void GenerateRoom(Room parentRoom)
    {
        //Debug.Log(parentRoom.roomCenter);
        int directionIndex = Random.Range(0, parentRoom.roomDirections.Count);
        Vector3Int direction = parentRoom.roomDirections[directionIndex];
        Vector3Int wayStartPoint = parentRoom.roomCenter + direction * parentRoom.roomSize / 2;
        Room newRoom = new Room();
        int roomSizeX = Random.Range(4, 14);
        if (roomSizeX % 2 == 0) roomSizeX++;
        int roomSizeY = Random.Range(4, 14);
        if (roomSizeY % 2 == 0) roomSizeY++;
        newRoom.roomSize = new Vector3Int(roomSizeX, roomSizeY, 0);
        int bonusLenght = 0;
        if (direction == Vector3Int.up)
        {
            bonusLenght = maxY - wayStartPoint.y;
        }
        else if (direction == Vector3Int.down)
        {
            bonusLenght = wayStartPoint.y - minY;
        }
        else if (direction == Vector3Int.right)
        {
            bonusLenght = maxX - wayStartPoint.x;
        }
        else if (direction == Vector3Int.left)
        {
            bonusLenght = wayStartPoint.x - minX;
        }
        if (bonusLenght != 0)
        {
            if (CheckSpaceForRoom(parentRoom.roomCenter + direction * parentRoom.roomSize / 2 + direction + direction * 3 + direction * newRoom.roomSize / 2 - newRoom.roomSize / 2, newRoom.roomSize))
            {
                bonusLenght = 2;
            }
        }
        Vector3Int wayEndPoint = parentRoom.roomCenter + direction * parentRoom.roomSize / 2 + direction + direction * (1 + bonusLenght);
        newRoom.roomCenter = wayEndPoint + direction * newRoom.roomSize / 2;
        newRoom.roomDirections = new List<Vector3Int>(directions);
        newRoom.roomDirections.Remove(direction * -1);
        Vector3Int roomStartPoint = newRoom.roomCenter - newRoom.roomSize / 2;
        Vector3Int roomEndPoint = roomStartPoint + newRoom.roomSize - Vector3Int.one;

        DrawCorridor(wayStartPoint, parentRoom.roomDirections[directionIndex], 1 + bonusLenght);
        DrawRoom(newRoom.roomCenter - newRoom.roomSize / 2, newRoom.roomSize);
        LightSetup(newRoom.roomCenter - newRoom.roomSize / 2, newRoom.roomSize);
        PlacingObjectInRoom(newRoom.roomCenter - newRoom.roomSize / 2, newRoom.roomSize);

        rooms.Add(newRoom);
        parentRoom.roomDirections.Remove(direction);
        if (roomStartPoint.x < minX)
        {
            minX = roomStartPoint.x;
        }
        if (roomStartPoint.y < minY)
        {
            minY = roomStartPoint.y;
        }
        if (roomEndPoint.x > maxX)
        {
            maxX = roomEndPoint.x;
        }
        if (roomEndPoint.y > maxY)
        {
            maxY = roomEndPoint.y;
        }
    }

    /*void GenerateRoom(Room parentRoom)
    {
        //Debug.Log(parentRoom.roomCenter);
        int directionIndex = Random.Range(0, parentRoom.roomDirections.Count);
        Vector3Int direction = parentRoom.roomDirections[directionIndex];
        Vector3Int wayStartPoint = parentRoom.roomCenter + direction * parentRoom.roomSize / 2;
        Room newRoom = new Room();
        int roomSizeX = Random.Range(4, 14);
        if (roomSizeX % 2 == 0) roomSizeX++;
        int roomSizeY = Random.Range(4, 14);
        if (roomSizeY % 2 == 0) roomSizeY++;
        newRoom.roomSize = new Vector3Int(roomSizeX, roomSizeY, 0);
        newRoom.roomCenter = wayStartPoint + direction * newRoom.roomSize / 2;
        newRoom.roomDirections = new List<Vector3Int>(directions);
        newRoom.roomDirections.Remove(direction * -1);
        Vector3Int roomStartPoint = newRoom.roomCenter - newRoom.roomSize / 2;
        Vector3Int roomEndPoint = roomStartPoint + newRoom.roomSize - Vector3Int.one;
        DrawRoom(newRoom.roomCenter - newRoom.roomSize / 2, newRoom.roomSize);

        rooms.Add(newRoom);
        parentRoom.roomDirections.Remove(direction);
        if (roomStartPoint.x < minX)
        {
            minX = roomStartPoint.x;
        }
        if (roomStartPoint.y < minY)
        {
            minY = roomStartPoint.y;
        }
        if (roomEndPoint.x > maxX)
        {
            maxX = roomEndPoint.x;
        }
        if (roomEndPoint.y > maxY)
        {
            maxY = roomEndPoint.y;
        }
    }
*/
    void DrawRoom(Vector3Int startPoint, Vector3Int roomSize)
    {
        for (int x = -1; x <= roomSize.x; x++)
        {
            for (int y = -1; y <= roomSize.y; y++)
            {
                if (x == -1 || x == roomSize.x || y ==  roomSize.y || y == -1)
                {
                    if (groundTilemap.GetTile(startPoint + new Vector3Int(x, y, 0)) == null)
                    {
                        wallTilemap.SetTile(startPoint + new Vector3Int(x, y, 0), wallTile);
                    }
                }
                else
                {
                    groundTilemap.SetTile(startPoint + new Vector3Int(x, y, 0), groundTile);
                }
            }
        }
    }

    void LightSetup(Vector3Int startPoint, Vector3Int roomSize)
    {
        for (int x = 2; x < roomSize.x; x += 3)
        {
            for (int y = 2; y < roomSize.y; y += 3)
            {
                lightTilemap.SetTile(startPoint + new Vector3Int(x, y, 0), spotLightTile);
            }
        }
    }

    void PlacingObjectInRoom(Vector3Int startPoint, Vector3Int roomSize)
    {
        int objectNumber = (int)(roomSize.x * roomSize.y * objectFillPercent);
        while (objectNumber > 0)
        {
            int randomObject = Random.Range(0, objects.Count);
            Vector3Int objectSize = objects[randomObject].GetComponent<ObjectSize>().objectSize;
            int randomX = Random.Range(0, roomSize.x + 1);
            int randomY = Random.Range(0, roomSize.y + 1);
            int randomAxis = -1;
            if (objects[randomObject].GetComponent<ObjectSize>().objectType == ObjectType.vertical)
            {
                randomAxis = Random.Range(0, 2);
            }
            else if (objects[randomObject].GetComponent<ObjectSize>().objectType == ObjectType.up) 
            {
                randomAxis = 3;
            }
            else if (objects[randomObject].GetComponent<ObjectSize>().objectType == ObjectType.down)
            {
                randomAxis = 2;
            }
            else if (objects[randomObject].GetComponent<ObjectSize>().objectType == ObjectType.right)
            {
                randomAxis = 1;
            }
            else if (objects[randomObject].GetComponent<ObjectSize>().objectType == ObjectType.left)
            {
                randomAxis = 0;
            }
            bool canPlaceObject = false;
            for (int i = 0; i < 3; i++)
            {
                switch (randomAxis)
                {
                    case -1:
                        break;
                    case 0: 
                        randomX = 0; 
                        break;
                    case 1:
                        randomX = roomSize.x - 1;
                        break;
                    case 2:
                        randomY = 0;
                        break;
                    case 3:
                        randomY = roomSize.y - 1;
                        break;
                }
                if (CheckSpaceForObject(startPoint + new Vector3Int(randomX, randomY, 0), objectSize)) {
                    canPlaceObject = true;
                    break;
                }
                else
                {
                    randomX = Random.Range(0, roomSize.x);
                    randomY = Random.Range(0, roomSize.y);
                }
            }
            if (canPlaceObject)
            {
                GameObject objectClone = Instantiate(objects[randomObject], objectTilemap.transform);
                objectClone.transform.position = objectTilemap.CellToWorld(startPoint + new Vector3Int(randomX, randomY, 0));
                //objectTilemap.SetTile(startPoint + new Vector3Int(randomX, randomY, 0), objectPlaceholderTile[randomObject]);
                DrawObject(startPoint + new Vector3Int(randomX, randomY, 0), objectSize);
            }
            objectNumber--;
        }
    }

    void DrawObject(Vector3Int startPoint, Vector3Int objectSize)
    {
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                //if (x == 0 && y == 0) continue;
                objectTilemap.SetTile(startPoint + new Vector3Int(x, y, 0), objectTile);
            }
        }
    }

    bool CheckSpaceForObject(Vector3Int startPoint, Vector3Int spaceSize)
    {
        for (int x = 0; x < spaceSize.x; x++)
        {
            for (int y = 0; y < spaceSize.y; y++)
            {
                if (groundTilemap.GetTile(startPoint + new Vector3Int(x, y)) != groundTile || objectTilemap.GetTile(startPoint + new Vector3Int(x, y)) != null) return false;
            }
        }
        return true;
    }

    void DrawCorridor(Vector3Int startPoint, Vector3Int direction, int lenght)
    {
        Vector3Int reverseDirection = new Vector3Int(direction.y, direction.x, 0);
        for (int i = 1; i <= lenght; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (j == -1 || j == 1)
                {
                    if (groundTilemap.GetTile(startPoint + (direction * i) + (reverseDirection * j)) == null)
                    {
                        wallTilemap.SetTile(startPoint + (direction * i) + (reverseDirection * j), wallTile);
                    }
                }
                else
                {
                    if ((i == 1 || i == lenght))
                    {
                        if (direction.x == 0)
                        {
                            GameObject doorClone = Instantiate(horizontalDoor);
                            doorClone.transform.position = groundTilemap.GetCellCenterWorld(startPoint + (direction * i));
                            doorClone.transform.parent = groundTilemap.transform;
                        }
                        else
                        {
                            GameObject doorClone = Instantiate(verticalDoor);
                            doorClone.transform.position = groundTilemap.GetCellCenterWorld(startPoint + (direction * i));
                            doorClone.transform.parent = groundTilemap.transform;
                        }
                    }
                    wallTilemap.SetTile(startPoint + (direction * i), null);
                    groundTilemap.SetTile(startPoint + (direction * i), groundTile);
                }
            }
        }
    }

    bool CheckSpaceForRoom(Vector3Int startPoint, Vector3Int spaceSize)
    {
        for (int x = -1; x <= spaceSize.x; x++)
        {
            for (int y = -1; y <= spaceSize.y; y++)
            {
                if (groundTilemap.GetTile(startPoint + new Vector3Int(x, y)) != null) return false;
            }
        }
        return true;
    }
}
