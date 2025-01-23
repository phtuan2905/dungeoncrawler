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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isNotGenerate)
        {
            roomTotal = Random.Range(5, 31);
            groundTilemap.ClearAllTiles();
            isNotGenerate = false;
            GenerateDungeon();
        }
    }

    void GenerateDungeon()
    {
        Vector3Int roomCenter = (Vector3Int.zero + new Vector3Int(5, 5, 0)) / 2;
        Vector3Int roomSize = new Vector3Int(5, 5, 0);
        DrawRoom(Vector3Int.zero, roomSize);

        rooms = new List<Room>();

        Room baseRoom = new Room();
        baseRoom.roomCenter = roomCenter;
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
